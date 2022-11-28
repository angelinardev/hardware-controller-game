using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class gyroMove : MonoBehaviour
{
    public SerialController serialController;

    string message;
    public string[] strData = new string[7]; //4 inputs
    public string[] strData_received = new string[7];
    public float qw, qx, qy, qz, ax;

    int b1, b2, b3;

    SerialPort stream = new SerialPort("COM4", 115200);

    
    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        //serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
        stream.Open();
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


            transform.rotation = new Quaternion(-qx, -qz, -qy, qw);

             //movement
             // prevent 
           /*  if (Mathf.Abs(ax) - 1 < 0) ax = 0;
            if (Mathf.Abs(ay) - 1 < 0) ay = 0;
            if (Mathf.Abs(az) - 1 < 0) az = 0; */

            
            /* curr_offset_x += qx;
            curr_offset_y += 0;
            curr_offset_z +=qy; // The IMU module have value of z axis of 16600 caused by gravity */

            transform.position += new Vector3(qy, 0, qx)*speed;

            //handle button inputs
            ButtonDetection();

        }   
    }

    void ButtonDetection()
    {

    }
}
