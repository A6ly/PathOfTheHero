using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 15.0f;
    [SerializeField] float zoomSpeed = 7.5f;
    [SerializeField] float minZoom = 9.5f;
    [SerializeField] float maxZoom = 20.0f;
    [SerializeField] float minXLimit = 2.725f;
    [SerializeField] float maxXLimit = 22.405f;
    [SerializeField] float minZLimit = -1.905f;
    [SerializeField] float maxZLimit = 17.775f;

    [SerializeField] Vector3 defaultPosition = new Vector3(15.5f, 15.5f, 5.0f);

    private void Update()
    {
        MoveCamera();
        ZoomCamera();
    }

    private void MoveCamera()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = defaultPosition;
        }

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

        Vector3 newPosition = transform.position + direction.normalized * moveSpeed * Time.deltaTime;

        float xLimit = Mathf.Clamp(newPosition.x, minXLimit, maxXLimit);
        float zLimit = Mathf.Clamp(newPosition.z, minZLimit, maxZLimit);

        transform.position = new Vector3(xLimit, transform.position.y, zLimit);
    }

    private void ZoomCamera()
    {
        float zoomInput = Input.GetAxis("Mouse ScrollWheel") * -1.0f;
        float zoomDistance = Mathf.Clamp(transform.position.y + zoomInput * zoomSpeed, minZoom, maxZoom);

        transform.position = new Vector3(transform.position.x, zoomDistance, transform.position.z);
    }
}
