using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    Grid targetGrid;
    PathFinder pathFinder;
    GridHighlight moveHighlight;
    GridHighlight attackHighlight;

    List<Vector2Int> attackPosition;

    private void Start()
    {
        StageManager stageManager = FindObjectOfType<StageManager>();
        targetGrid = stageManager.stageGrid;
        pathFinder = stageManager.pathFinder;
        moveHighlight = stageManager.moveHighlight;
        attackHighlight = stageManager.attackHighlight;
    }

    public void CheckWalkableGround(Character character)
    {
        GridObject gridObject = character.GetComponent<GridObject>();
        List<PathNode> walkableNodes = new List<PathNode>();
        pathFinder.Clear();
        pathFinder.CalculateWalkableNodes(gridObject.positionOnGrid.x, gridObject.positionOnGrid.y, character.movementPoints, ref walkableNodes);
        moveHighlight.Hide();
        moveHighlight.Highlight(walkableNodes);
    }

    public List<PathNode> GetPath(Vector2Int from)
    {
        List<PathNode> path = pathFinder.TraceBackPath(from.x, from.y);

        if (path == null || path.Count == 0)
        {
            return null;
        }

        path.Reverse();

        return path;
    }

    public bool CheckPlacedObject(Vector2Int positionOnGrid)
    {
        return targetGrid.CheckPlacedObject(positionOnGrid);
    }

    public void CalculateAttackArea(Vector2Int characterPositionOnGrid, int attackRange, bool selfTargetable = false)
    {
        if (attackPosition == null)
        {
            attackPosition = new List<Vector2Int>();
        }
        else
        {
            attackPosition.Clear();
        }

        for (int x = -attackRange; x <= attackRange; x++)
        {
            for (int y = -attackRange; y <= attackRange; y++)
            {
                if (Mathf.Abs(x) + Mathf.Abs(y) > attackRange)
                {
                    continue;
                }

                if (!selfTargetable)
                {
                    if (x == 0 && y == 0)
                    {
                        continue;
                    }
                }

                if (targetGrid.CheckBoundry(characterPositionOnGrid.x + x, characterPositionOnGrid.y + y))
                {
                    attackPosition.Add(new Vector2Int(characterPositionOnGrid.x + x, characterPositionOnGrid.y + y));
                }
            }
        }

        attackHighlight.Highlight(attackPosition);
    }

    public GridObject GetAttackTarget(Vector2Int positionOnGrid)
    {
        GridObject target = targetGrid.GetPlacedObject(positionOnGrid);

        return target;
    }

    public bool CheckAttackable(Vector2Int positionOnGrid)
    {
        return attackPosition.Contains(positionOnGrid);
    }
}