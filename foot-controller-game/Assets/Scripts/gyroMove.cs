using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.UI;

public class gyroMove : MonoBehaviour
{
    public SerialController serialController;

    string message;
    public string[] strData = new string[7]; //4 inputs
    public string[] strData_received = new string[7];
    public float qw, qx, qy, qz;

    int b1, b2, b3;

    SerialPort stream = new SerialPort("COM4", 115200);

  public float jump = 5f;
    
    public float speed;

     private float distanceToGround;
     bool isGrounded = true;
      Rigidbody rb;

      PauseMenu menu;

      bool isSpeeding = false;

       private Vector3 targetVelocityLerp;
        public float sensitivity, snappiness, maxForce;

    public Image speedImage;
    // Start is called before the first frame update
    void Start()
    {
        //serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
        stream.Open();
        distanceToGround = GetComponent<Collider>().bounds.extents.y;
         rb = GetComponent<Rigidbody>();
         menu = GetComponent<PauseMenu>();
         speedImage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //reading the data
        //message = serialController.ReadSerialMessage();

        //split any commas
        //strData = message.Split(',');

        message = stream.ReadLine();
        strData = message.Split(',');
        //Debug.Log(message);

        //Debug.Log(strData.Length);

        if (strData[0] != "" && strData[1] != "" && strData[2] != "" && strData[3] != "")//make sure data are ready, not empty
        {
            //copy to received data
            strData_received[0] = strData[0];
            strData_received[1] = strData[1];
            strData_received[2] = strData[2];
            strData_received[3] = strData[3];
            strData_received[4] = strData[4];
            strData_received[5] = strData[5];
            strData_received[6] = strData[6];
            /* strData_received[4] = strData[4];
            strData_received[5] = strData[5];
            strData_received[6] = strData[6]; */
         
            //convert to floats from the received strings
            qw = float.Parse(strData_received[0]);
            qx = float.Parse(strData_received[1]);
            qy = float.Parse(strData_received[2]);
            qz = float.Parse(strData_received[3]);
            b1 = int.Parse(strData_received[4]);
            b2 = int.Parse(strData_received[5]);
            b3 = int.Parse(strData_received[6]);
           /*  ax = float.Parse(strData_received[4]);
            ay = float.Parse(strData_received[5]);
            az = float.Parse(strData_received[6]); */


            //set our own rotation to be equal to a quaternion defined by these new values
            //reverse left/right and up/down
            //transform.rotation = new Quaternion(-qy, -qz, qx, qw);

            rb.rotation = new Quaternion(-qx, -qz, -qy, qw);
            //transform.rotation = new Quaternion(-qx, -qz, -qy, qw);
            //transform.Rotate(new Vector3(-qx, -qz, -qy));

             //movement
             // prevent 
           /*  if (Mathf.Abs(ax) - 1 < 0) ax = 0;
            if (Mathf.Abs(ay) - 1 < 0) ay = 0;
            if (Mathf.Abs(az) - 1 < 0) az = 0; */

            
            /* curr_offset_x += qx;
            curr_offset_y += 0;
            curr_offset_z +=qy; // The IMU module have value of z axis of 16600 caused by gravity */

             //handle button inputs
            ButtonDetection();
            Speed();

            //rb.Move(rb.position + new Vector3(qy, 0, qx)*speed, new Quaternion(-qx, -qz, -qy, qw));
            //rb.velocity = new Vector3(qy, 0, qx)*speed;
            //rb.position += new Vector3(qy, 0, qx)*speed*sensitivity;
            //Move();
            //physics calcs
            //isGrounded = Physics.Raycast(transform.position, -Vector3.up, distanceToGround);

        }   
    }
    private void FixedUpdate() {
        
            rb.velocity = new Vector3(qy, 0, qx)*speed;
            //Move();
            //physics calcs
            isGrounded = Physics.Raycast(transform.position, -Vector3.up, distanceToGround);
    }
    void Speed()
    {
        //calculate speed
            if (isSpeeding)
            {    speed = 3.0f;
                speedImage.enabled = true;
            }
            else
            {    speed = 1.0f;
                speedImage.enabled = false;
            }
    }

    void ButtonDetection()
    {
        //pause button
        if (b1 ==1)
         {   Debug.Log("Button 1 pressed");
            menu.ChangeUI();
         }
        //speed button
        if (b2 ==1)
         {   Debug.Log("Button 2 pressed");
            isSpeeding = !isSpeeding;
         }

        //jump button
        if (b3 ==1)
         {   
            Debug.Log("Button 3 pressed");
            Jump();
         }

    }
    private void Jump()
    {
        //if(isGrounded)
        {
           // rb.velocity = new Vector2(rb.velocity.x, jump);
            //rb.velocity = new Vector3(qy, jump, qx)*speed;
            rb.AddForce(new Vector3(0, jump, 0), ForceMode.VelocityChange);
            isGrounded = false;
            Debug.Log("We jumped");
        }
    }
    void Move()
    {
        //find target velocity
        Vector3 currentVelocity = rb.velocity;
        Vector3 targetVelocity = new Vector3(qy, 0, qx);
        targetVelocity *=speed;

        //align direction
        targetVelocity = transform.TransformDirection(targetVelocity);
        targetVelocityLerp = Vector3.Lerp(targetVelocityLerp, targetVelocity, snappiness); 

        //calculate forces
         Vector3 velocityChange = (targetVelocityLerp - currentVelocity); 
        velocityChange = new Vector3(velocityChange.x, 0, velocityChange.z);

        //limit force
        Vector3.ClampMagnitude(velocityChange, maxForce);

        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }
}
