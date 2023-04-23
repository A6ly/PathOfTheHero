using UnityEngine;

public class GridObject : MonoBehaviour
{
    public Grid targetGrid;
    public Vector2Int positionOnGrid;

    private void Start()
    {
        if (targetGrid == null)
        {
            targetGrid = FindObjectOfType<StageManager>().stageGrid;
        }

        positionOnGrid = targetGrid.GetGridPosition(transform.position);
        targetGrid.PlaceObject(positionOnGrid, this);
        transform.position = targetGrid.GetWorldPosition(positionOnGrid.x, positionOnGrid.y, true);
    }
}