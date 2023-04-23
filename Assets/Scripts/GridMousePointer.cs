using UnityEngine;

public class GridMousePointer : MonoBehaviour
{
    public Vector2Int positionOnGrid;

    [SerializeField] GameObject marker;

    [SerializeField] float elevation = 2.0f;

    Grid targetGrid;

    int groundLayerMask = (1 << (int)Define.Layer.Ground);

    private void Start()
    {
        targetGrid = FindObjectOfType<StageManager>().stageGrid;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, float.MaxValue, groundLayerMask))
        {
            Vector2Int hitPosition = targetGrid.GetGridPosition(hit.point);
            marker.SetActive(true);

            if (hitPosition != positionOnGrid)
            {
                positionOnGrid = hitPosition;
                UpdateMarker();
            }
        }
        else
        {
            marker.SetActive(false);
        }
    }

    private void UpdateMarker()
    {
        if (targetGrid.CheckBoundry(positionOnGrid))
        {
            Vector3 worldPosition = targetGrid.GetWorldPosition(positionOnGrid.x, positionOnGrid.y, true);
            worldPosition.y += elevation;
            marker.transform.position = worldPosition;
        }
    }
}
