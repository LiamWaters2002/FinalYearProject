using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public PlayerInput playerInput;
    private InputAction leftStickMovement;

    private void Awake()
    {

    }

    void Update()
    {
        Vector3 inputMoveDirection = new Vector3(0, 0, 0);
        Vector2 movement = playerInput.actions["Move"].ReadValue<Vector2>();
        Vector2 rotationOrHeight = playerInput.actions["Rotate"].ReadValue<Vector2>(); ;


        if (Input.GetKey(KeyCode.W) || movement.y > 0f)
        {
            inputMoveDirection.z = +1f;
        }
        if (Input.GetKey(KeyCode.S) || movement.y < 0f)
        {
            inputMoveDirection.z = -1f;
        }
        if (Input.GetKey(KeyCode.A) || movement.x < 0f)
        {
            inputMoveDirection.x = -1f;
        }
        if (Input.GetKey(KeyCode.D) || movement.x > 0f)
        {
            inputMoveDirection.x = +1f;
        }

        float moveSpeed = 10f;

        Vector3 moveVector = transform.forward * inputMoveDirection.z + transform.right * inputMoveDirection.x;
        moveVector.y = 0; // keep the y level constant
        transform.position += moveVector * moveSpeed * Time.deltaTime;

        float yChange = 10f; // adjust the amount of change as you like

        if (Input.GetKey(KeyCode.E) || rotationOrHeight.y > 0f) // e button
        {
            transform.position += Vector3.up * yChange * Time.deltaTime; // increase the y level of this object smoothly using deltatime
        }

        if (Input.GetKey(KeyCode.Q) || rotationOrHeight.y < 0f) // q button
        {
            transform.position -= Vector3.up * yChange * Time.deltaTime; // decrease the y level of this object smoothly using deltatime
        }


        Vector3 rotationVector = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.LeftArrow) || rotationOrHeight.x > 0f)
        {
            rotationVector.y = +1f;
        }
        if (Input.GetKey(KeyCode.RightArrow) || rotationOrHeight.x < 0f)
        {
            rotationVector.y = -1f;
        }
        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    rotationVector.x = +1f;
        //}
        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    rotationVector.x = -1f;
        //}

        float rotationSpeed = 100f;

        transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;
    }
}
