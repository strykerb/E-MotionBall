/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroControls : MonoBehaviour
{

	private bool gyroEnabled;

	private Gyroscope gyro;

	public float speed;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        gyroEnabled = EnableGyro();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = gyro.attitude
        float moveVertical = Input.GetAxis ("Vertical");
    }

    private bool EnableGyro(){
    	if(SystemInfo.supportsGyroscope)
    	{
    		gyro = Input.gyro;
    		gyro.enabled = true;
    		return true;
    	}
    	return false;
    }
}
*/