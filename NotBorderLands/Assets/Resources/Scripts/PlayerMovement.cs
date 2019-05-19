using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float speed = 10;
    public float maxLookUpAngle;
    public float minLookDownAngle;
    [Range(1,10)]
    public float xAxisSensitivity = 10;
    [Range(1, 10)]
    public float yAxisSensitivity = 10;


    Camera playerCamera;
	// Use this for initialization
	void Start () {
        playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }
	
	// Update is called once per frame
	void Update () {
        CheckKeyboardInput();
        TrackMouse();
    }
    void CheckKeyboardInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += (transform.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += (-transform.right * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += (-transform.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += (transform.right * speed * Time.deltaTime);
        }
    }
    void TrackMouse()
    {
        float xAxis = Input.GetAxis("Mouse X");
        float yAxis = Input.GetAxis("Mouse Y") * -1;

        transform.Rotate(0,xAxis * xAxisSensitivity, 0);
        playerCamera.transform.Rotate(yAxis * yAxisSensitivity, 0, 0);

        float eulerRotX = playerCamera.transform.rotation.eulerAngles.x;

        eulerRotX = (eulerRotX > 180) ? eulerRotX - 360 : eulerRotX;

        eulerRotX *= -1;
        Debug.Log(eulerRotX);
        if (eulerRotX > maxLookUpAngle || eulerRotX < minLookDownAngle)
        {
            playerCamera.transform.Rotate(-yAxis * yAxisSensitivity, 0, 0);
        }
    }
}
