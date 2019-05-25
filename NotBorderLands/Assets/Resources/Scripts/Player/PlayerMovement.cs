using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mike4ruls.General.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        // Public Vars 
        public float speed = 10;
        public float jumpForce = 4;
        public float maxLookUpAngle;
        public float minLookDownAngle;
        [Range(1, 10)]
        public float xAxisSensitivity = 10;
        [Range(1, 10)]
        public float yAxisSensitivity = 10;

        // Private Vars 
        private Rigidbody myRigidbody;
        private Camera playerCamera;
        // Use this for initialization
        void Start()
        {
            playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<Camera>();
            myRigidbody = GetComponent<Rigidbody>();
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Update is called once per frame
        void Update()
        {
            CheckKeyboardInput();
            TrackMouse();
        }
        void CheckKeyboardInput()
        {
            Vector3 dir = Vector3.zero;

            if (Input.GetAxis("LeftStickX") != 0 || Input.GetAxis("LeftStickY") != 0)
            {
                dir += transform.right * Input.GetAxis("LeftStickX");
                dir += transform.forward * Input.GetAxis("LeftStickY");
            }

            if (Input.GetKey(KeyCode.W))
            {
                dir += transform.forward;
            }
            if (Input.GetKey(KeyCode.A))
            {
                dir += -transform.right;
            }
            if (Input.GetKey(KeyCode.S))
            {
                dir += -transform.forward;
            }
            if (Input.GetKey(KeyCode.D))
            {
                dir += transform.right;
            }
            if (Input.GetButtonDown("Jump"))
            {
                if (Physics.Raycast(transform.position, Vector3.up * -1, transform.localScale.x + 0.4f))
                {
                    myRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                }
            }

            transform.position += dir * speed *Time.deltaTime;
        }
        void TrackMouse()
        {
            float xAxis = Input.GetAxis("Mouse X");
            float yAxis = Input.GetAxis("Mouse Y") * -1;

            if (xAxis == 0)
            {
                xAxis = Input.GetAxis("RightStickX");
            }
            if (yAxis == 0)
            {
                yAxis = Input.GetAxis("RightStickY") * -1;
            }

            transform.Rotate(0, xAxis * xAxisSensitivity, 0);
            playerCamera.transform.Rotate(yAxis * yAxisSensitivity, 0, 0);

            float eulerRotX = playerCamera.transform.rotation.eulerAngles.x;

            eulerRotX = (eulerRotX > 180) ? eulerRotX - 360 : eulerRotX;

            eulerRotX *= -1;
            if (eulerRotX > maxLookUpAngle || eulerRotX < minLookDownAngle)
            {
                playerCamera.transform.Rotate(-yAxis * yAxisSensitivity, 0, 0);
            }
        }
    }
}
