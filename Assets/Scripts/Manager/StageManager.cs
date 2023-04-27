using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }

    [SerializeField] private Grid stageGrid;
    [SerializeField] private PathFinder pathFinder;
    [SerializeField] private GridHighlight moveHighlight;
    [SerializeField] private GridHighlight attackHighlight;

    public Grid StageGrid { get { return stageGrid; } }
    public PathFinder PathFinder { get { return pathFinder; } }
    public GridHighlight MoveHighlight { get { return moveHighlight; } }
    public GridHighlight AttackHighlight { get { return attackHighlight; } }

    private void Awake()
    {
        Instance = this;
    }

    public void ClearPathFinder()
    {
        pathFinder.Clear();
    }

    public void ClearMoveHighlight()
    {
        moveHighlight.Hide();
    }

    public void ClearAttackHighlight()
    {
        attackHighlight.Hide();
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}