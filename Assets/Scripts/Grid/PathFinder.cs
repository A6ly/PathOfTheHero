using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    public int posX;
    public int posY;

    public float gValue;
    public float hValue;
    public float fValue
    {
        get
        {
            return gValue + hValue;
        }
    }

    internal PathNode parentNode;

    public PathNode(int posX, int posY)
    {
        this.posX = posX;
        this.posY = posY;
    }

    public void Clear()
    {
        gValue = 0f;
        hValue = 0f;
        parentNode = null;
    }
}

public class PathFinder : MonoBehaviour
{
    Grid grid;

    PathNode[,] pathNodes;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        if (grid == null)
        {
            grid = GetComponent<Grid>();
        }

        pathNodes = new PathNode[grid.length, grid.width];

        for (int x = 0; x < grid.length; x++)
        {
            for (int y = 0; y < grid.width; y++)
            {
                pathNodes[x, y] = new PathNode(x, y);
            }
        }
    }

    private int CalculateDistance(PathNode currentNode, PathNode target)
    {
        int distX = Mathf.Abs(currentNode.posX - target.posX);
        int distY = Mathf.Abs(currentNode.posY - target.posY);

        return 14 * Mathf.Min(distX, distY) + 10 * Mathf.Abs(distX - distY);
    }

    public void CalculateWalkableNodes(int startX, int startY, float range, ref List<PathNode> toHighlight)
    {
        PathNode startNode = pathNodes[startX, startY];

        List<PathNode> openList = new List<PathNode>();
        List<PathNode> closeList = new List<PathNode>();

        openList.Add(startNode);

        while (openList.Count > 0)
        {
            PathNode currentNode = openList[0];

            openList.Remove(currentNode);
            closeList.Add(currentNode);

            List<PathNode> adjacentNodes = new List<PathNode>();

            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    if (x == 0 && y == 0)
                    {
                        continue;
                    }

                    if (grid.CheckBoundry(currentNode.posX + x, currentNode.posY + y))
                    {
                        adjacentNodes.Add(pathNodes[currentNode.posX + x, currentNode.posY + y]);
                    }
                }
            }

            for (int i = 0; i < adjacentNodes.Count; i++)
            {
                if (closeList.Contains(adjacentNodes[i]) || !grid.CheckWalkable(adjacentNodes[i].posX, adjacentNodes[i].posY) || !grid.CheckElevation(currentNode.posX, currentNode.posY, adjacentNodes[i].posX, adjacentNodes[i].posY))
                {
                    continue;
                }

                float movementCost = currentNode.gValue + CalculateDistance(currentNode, adjacentNodes[i]);

                if (movementCost > range)
                {
                    continue;
                }

                if (!openList.Contains(adjacentNodes[i]) || movementCost < adjacentNodes[i].gValue)
                {
                    adjacentNodes[i].gValue = movementCost;
                    adjacentNodes[i].parentNode = currentNode;

                    if (!openList.Contains(adjacentNodes[i]))
                    {
                        openList.Add(adjacentNodes[i]);
                    }
                }
            }
        }

        if (toHighlight != null)
        {
            toHighlight.AddRange(closeList);
        }
    }

    public List<PathNode> TraceBackPath(int x, int y)
    {
        if (!grid.CheckBoundry(x, y))
        {
            return null;
        }

        List<PathNode> path = new List<PathNode>();
        PathNode currentNode = pathNodes[x, y];

        while (currentNode.parentNode != null)
        {
            path.Add(currentNode);
            currentNode = currentNode.parentNode;
        }

        return path;
    }

    public void Clear()
    {
        for (int x = 0; x < grid.length; x++)
        {
            for (int y = 0; y < grid.width; y++)
            {
                pathNodes[x, y].Clear();
            }
        }
    }
}
