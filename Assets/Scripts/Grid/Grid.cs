using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    Node[,] grid;

    public int width = 15;
    public int length = 15;

    [SerializeField] float cellSize = 1.5f;
    [SerializeField] LayerMask groundLayer;

    private void Awake()
    {
        GenerateGrid();
    }

    private void OnDrawGizmos()
    {
        if (grid == null)
        {
            for (int x = 0; x < length; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    Vector3 pos = GetWorldPosition(x, y);
                    Gizmos.DrawCube(pos, Vector3.one / 4);
                }
            }
        }
        else
        {
            for (int x = 0; x < length; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    Vector3 pos = GetWorldPosition(x, y, true);
                    Gizmos.color = grid[x, y].passable ? Color.white : Color.red;
                    Gizmos.DrawCube(pos, Vector3.one / 4);
                }
            }
        }
    }

    public Vector3 GetWorldPosition(int x, int y, bool elevation = false)
    {
        return new Vector3(x * cellSize, elevation == true ? grid[x, y].elevation : 0f, y * cellSize);
    }

    private void CalculateElevation()
    {
        for (int x = 0; x < length; x++)
        {
            for (int y = 0; y < width; y++)
            {
                Ray ray = new Ray(GetWorldPosition(x, y) + Vector3.up * 100f, Vector3.down);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, float.MaxValue, groundLayer))
                {
                    grid[x, y].elevation = hit.point.y;
                }
            }
        }
    }

    private void CheckPassableGround()
    {
        for (int x = 0; x < length; x++)
        {
            for (int y = 0; y < width; y++)
            {
                Vector3 worldPosition = GetWorldPosition(x, y);
                bool passable = Physics.CheckBox(worldPosition, Vector3.one / 2 * cellSize, Quaternion.identity, groundLayer);
                grid[x, y].passable = passable;
            }
        }
    }

    public bool CheckBoundry(Vector2Int positionOnGrid)
    {
        if (positionOnGrid.x < 0 || positionOnGrid.x >= length)
        {
            return false;
        }
        if (positionOnGrid.y < 0 || positionOnGrid.y >= width)
        {
            return false;
        }

        return true;
    }

    public bool CheckBoundry(int posX, int posY)
    {
        if (posX < 0 || posX >= length)
        {
            return false;
        }
        if (posY < 0 || posY >= width)
        {
            return false;
        }

        return true;
    }

    public bool CheckWalkable(int posX, int posY)
    {
        return grid[posX, posY].passable;
    }

    public bool CheckElevation(int fromPosX, int fromPosY, int toPosX, int toPosY, float maxClimb = 1.5f)
    {
        float fromElevation = grid[fromPosX, fromPosY].elevation;
        float toElevation = grid[toPosX, toPosY].elevation;

        float diff = Mathf.Abs(fromElevation - toElevation);

        return maxClimb > diff;
    }

    public bool CheckPlacedObject(Vector2Int positionOnGrid)
    {
        return GetPlacedObject(positionOnGrid) != null;
    }

    public void PlaceObject(Vector2Int positionOnGrid, GridObject gridObject)
    {
        if (CheckBoundry(positionOnGrid))
        {
            grid[positionOnGrid.x, positionOnGrid.y].gridObject = gridObject;
        }
    }

    public GridObject GetPlacedObject(Vector2Int gridPosition)
    {
        if (CheckBoundry(gridPosition))
        {
            GridObject gridObject = grid[gridPosition.x, gridPosition.y].gridObject;

            return gridObject;
        }

        return null;
    }

    private void GenerateGrid()
    {
        grid = new Node[length, width];

        for (int x = 0; x < length; x++)
        {
            for (int y = 0; y < width; y++)
            {
                grid[x, y] = new Node();
            }
        }

        CalculateElevation();
        CheckPassableGround();
    }

    public Vector2Int GetGridPosition(Vector3 worldPosition)
    {
        worldPosition.x += cellSize / 2;
        worldPosition.z += cellSize / 2;

        Vector2Int positionOnGrid = new Vector2Int((int)(worldPosition.x / cellSize), (int)(worldPosition.z / cellSize));

        return positionOnGrid;
    }

    public List<Vector3> ConvertPathNodesToWorldPositions(List<PathNode> path)
    {
        List<Vector3> worldPositions = new List<Vector3>();

        for (int i = 0; i < path.Count; i++)
        {
            worldPositions.Add(GetWorldPosition(path[i].posX, path[i].posY, true));
        }

        return worldPositions;
    }

    public void RemoveObject(Vector2Int positionOnGrid, GridObject gridObject)
    {
        if (CheckBoundry(positionOnGrid))
        {
            grid[positionOnGrid.x, positionOnGrid.y].gridObject = null;
        }
        else
        {
            Debug.Log("You trying to place the object outside the boundries!");
        }
    }
}
