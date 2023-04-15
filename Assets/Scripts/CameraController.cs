using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float keyboardInputSensitivity = 1f;

    [SerializeField] Transform bottomLeftBorder;
    [SerializeField] Transform topRightBorder;

    Vector3 input;
    Vector3 pointOfOrigin;

    private void Update()
    {
        NullInput();
        MoveCameraInput();

        MoveCamera();
    }

    private void NullInput()
    {
        input.x = 0;
        input.y = 0;
        input.z = 0;
    }

    private void MoveCamera()
    {
        Vector3 position = transform.position;
        position += (input * Time.deltaTime);
        position.x = Mathf.Clamp(position.x, bottomLeftBorder.position.x, topRightBorder.position.x);
        position.z = Mathf.Clamp(position.z, bottomLeftBorder.position.z, topRightBorder.position.z);

        transform.position = position;
    }

    private void MoveCameraInput()
    {
        input.x += Input.GetAxisRaw("Horizontal") * keyboardInputSensitivity;
        input.z += Input.GetAxisRaw("Vertical") * keyboardInputSensitivity;
    }
}
