using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gyroPositionMove : MonoBehaviour
{
    public SerialController serialController;

    string message;
    public string[] strData = new string[4]; //4 inputs
    public string[] strData_received = new string[4];
    public float qw, qx, qy, qz;
    private float movement = 5f;
    // Start is called before the first frame update
    void Start()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
    }

    // Update is called once per frame
    void Update()
    {
        //reading the data
        message = serialController.ReadSerialMessage();

        //split any commas
        strData = message.Split(',');
        if (strData[0] != "" && strData[1] != "" && strData[2] != "" && strData[3] != "")//make sure data are ready, not empty
        {
            //copy to received data
            strData_received[0] = strData[0];
            strData_received[1] = strData[1];
            strData_received[2] = strData[2];
            strData_received[3] = strData[3];

            //convert to floats from the received strings
            qw = float.Parse(strData_received[0]);
            qx = float.Parse(strData_received[1]);
            qy = float.Parse(strData_received[2]);
            qz = float.Parse(strData_received[3]);

            //set position to the defined values that occur when controller moves 

            if (qx > 0)
            {
                transform.position = new Vector3(qx, 0, 0);
            }
            if (qx < 0)
            {
                transform.position = new Vector3(-qx, 0, 0);
            }
            if (qy > 0)
            {
                transform.position = new Vector3(0, qy, 0);
            }
            if (qy < 0)
            {
                transform.position = new Vector3(0, -qy, 0);
            }

        }
    }
}
