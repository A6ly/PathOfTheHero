using UnityEngine;
using UnityEngine.TextCore.Text;

public class GridMousePointer : MonoBehaviour
{
    public Vector2Int positionOnGrid;

    [SerializeField] GameObject marker;

    [SerializeField] float elevation = 2.0f;

    int groundLayerMask = (1 << (int)Define.Layer.Ground);

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, float.MaxValue, groundLayerMask))
        {
            Vector2Int hitPosition = StageManager.Instance.StageGrid.GetGridPosition(hit.point);
            marker.SetActive(true);

            if (hitPosition != positionOnGrid)
            {
                positionOnGrid = hitPosition;
                UpdateMarker();
                EffectManager.Instance.HighlightMarkerPointEffect(positionOnGrid);
            }
        }
        else
        {
            marker.SetActive(false);
            EffectManager.Instance.HideMarkerPointEffect();
        }
    }

    private void UpdateMarker()
    {
        if (StageManager.Instance.StageGrid.CheckBoundry(positionOnGrid))
        {
            Vector3 worldPosition = StageManager.Instance.StageGrid.GetWorldPosition(positionOnGrid.x, positionOnGrid.y, true);
            worldPosition.y += elevation;
            marker.transform.position = worldPosition;
        }
    }
}
