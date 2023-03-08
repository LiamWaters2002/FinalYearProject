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
        transform.position += moveVector * moveSpeed * Time.deltaTime;




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

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            moveSpeed = 0.01f;
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            transform.position += moveSpeed * new Vector3(-touchDeltaPosition.x, 0, -touchDeltaPosition.y);
        }
    }

}
