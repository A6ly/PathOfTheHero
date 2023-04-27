using UnityEngine;

public class GridObject : MonoBehaviour
{
    public Vector2Int positionOnGrid;

    private void Start()
    {
        positionOnGrid = StageManager.Instance.StageGrid.GetGridPosition(transform.position);
        StageManager.Instance.StageGrid.PlaceObject(positionOnGrid, this);
        transform.position = StageManager.Instance.StageGrid.GetWorldPosition(positionOnGrid.x, positionOnGrid.y, true);
    }
}