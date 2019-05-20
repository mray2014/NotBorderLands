using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mike4ruls.Player
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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (Physics.Raycast(transform.position, Vector3.up * -1, transform.localScale.x + 0.4f))
                {
                    myRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                }
            }
        }
        void TrackMouse()
        {
            float xAxis = Input.GetAxis("Mouse X");
            float yAxis = Input.GetAxis("Mouse Y") * -1;

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
