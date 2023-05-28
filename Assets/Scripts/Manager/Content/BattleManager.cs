using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Define;

public class BattleManager : MonoBehaviour
{
    static BattleManager instance;
    public static BattleManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BattleManager>();
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    List<Vector2Int> attackPosition;

    [SerializeField] GameObject rangeNotificationText;

    public void CalculateWalkableGround(Character character, bool isHighlight = true)
    {
        GridObject gridObject = character.GetComponent<GridObject>();
        List<PathNode> walkableNodes = new List<PathNode>();
        StageManager.Instance.PathFinder.Clear();
        StageManager.Instance.PathFinder.CalculateWalkableNodes(gridObject.positionOnGrid.x, gridObject.positionOnGrid.y, character.stat.MovementPoint, ref walkableNodes);

        if (isHighlight)
        {
            StageManager.Instance.MoveHighlight.Hide();
            StageManager.Instance.MoveHighlight.Highlight(walkableNodes);
        }
    }

    public List<PathNode> GetPath(Vector2Int from)
    {
        List<PathNode> path = StageManager.Instance.PathFinder.TraceBackPath(from.x, from.y);

        if (path == null || path.Count == 0)
        {
            return null;
        }

        path.Reverse();

        return path;
    }

    public void CalculateAttackArea(Vector2Int characterPositionOnGrid, int range, string tag, bool selfTargetable = false)
    {
        if (attackPosition == null)
        {
            attackPosition = new List<Vector2Int>();
        }
        else
        {
            attackPosition.Clear();
        }

        for (int x = -range; x <= range; x++)
        {
            for (int y = -range; y <= range; y++)
            {
                if (Mathf.Abs(x) + Mathf.Abs(y) > range)
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

                if (StageManager.Instance.StageGrid.CheckBoundry(characterPositionOnGrid.x + x, characterPositionOnGrid.y + y))
                {
                    if (StageManager.Instance.StageGrid.CheckAttackTarget(characterPositionOnGrid.x + x, characterPositionOnGrid.y + y, tag))
                    {
                        attackPosition.Add(new Vector2Int(characterPositionOnGrid.x + x, characterPositionOnGrid.y + y));
                    }
                }
            }
        }

        StageManager.Instance.AttackHighlight.Highlight(attackPosition);
    }

    public GridObject GetAttackTarget(Vector2Int positionOnGrid)
    {
        GridObject target = StageManager.Instance.StageGrid.GetPlacedObject(positionOnGrid);

        return target;
    }

    public bool CheckAttackable(Vector2Int positionOnGrid)
    {
        return attackPosition.Contains(positionOnGrid);
    }

    public bool CheckNoAttackablePosition()
    {
        if (!attackPosition.Any())
        {
            Managers.Sound.Play("NoticeEffect", SoundType.Effect);

            DOTween.Sequence()
            .AppendCallback(() => rangeNotificationText.SetActive(true))
            .AppendInterval(3f)
            .AppendCallback(() => rangeNotificationText.SetActive(false));

            return true;
        }

        return false;
    }
}