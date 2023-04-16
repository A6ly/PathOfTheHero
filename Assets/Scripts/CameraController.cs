using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 15.0f;
    [SerializeField] float zoomSpeed = 7.5f;
    [SerializeField] float minZoom = 9.5f;
    [SerializeField] float maxZoom = 20.0f;

    private void Update()
    {
        MoveCamera();
        ZoomCamera();
    }

    private void MoveCamera()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 direction = Vector3.zero;

        if (horizontalInput == -1.0f)
        {
            direction = new Vector3(-1.0f, 0.0f, -1.0f);
        }
        else if (horizontalInput == 1.0f)
        {
            direction = new Vector3(1.0f, 0.0f, 1.0f);
        }
        else if (verticalInput == -1.0f)
        {
            direction = new Vector3(1.0f, 0.0f, -1.0f);
        }
        else if (verticalInput == 1.0f)
        {
            direction = new Vector3(-1.0f, 0.0f, 1.0f);
        }

        transform.position += direction.normalized * moveSpeed * Time.deltaTime;
    }

    private void ZoomCamera()
    {
        float zoomInput = Input.GetAxis("Mouse ScrollWheel") * -1.0f;
        float zoomDistance = Mathf.Clamp(transform.position.y + zoomInput * zoomSpeed, minZoom, maxZoom);

        transform.position = new Vector3(transform.position.x, zoomDistance, transform.position.z);
    }
}
