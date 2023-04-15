using UnityEngine;

public class GridMousePointer : MonoBehaviour
{
    [SerializeField] GameObject marker;

    Grid targetGrid;
    LayerMask terrainLayerMask;
    Vector2Int positionOnGrid;
    float elevation = 2f;

    private void Start()
    {
        terrainLayerMask = 1 << LayerMask.NameToLayer("Terrain");
        targetGrid = FindObjectOfType<StageManager>().stageGrid;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, float.MaxValue, terrainLayerMask))
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
