using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 inputMoveDirection = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputMoveDirection.z = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputMoveDirection.z = -1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputMoveDirection.x = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputMoveDirection.x = +1f;
        }

        float moveSpeed = 10f;

        Vector3 moveVector = transform.forward * inputMoveDirection.z + transform.right * inputMoveDirection.x;
        moveVector.y = 0; // keep the y level constant
        transform.position += moveVector * moveSpeed * Time.deltaTime;

        float yChange = 10f; // adjust the amount of change as you like

        if (Input.GetKey(KeyCode.E)) // e button
        {
            transform.position += Vector3.up * yChange * Time.deltaTime; // increase the y level of this object smoothly using deltatime
        }

        if (Input.GetKey(KeyCode.Q)) // q button
        {
            transform.position -= Vector3.up * yChange * Time.deltaTime; // decrease the y level of this object smoothly using deltatime
        }


        Vector3 rotationVector = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotationVector.y = +1f;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rotationVector.y = -1f;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rotationVector.x = +1f;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rotationVector.x = -1f;
        }

        float rotationSpeed = 100f;

        transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;
    }
}
