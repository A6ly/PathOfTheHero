using UnityEngine;

public class StageManager : MonoBehaviour
{
    public Grid stageGrid;
    public PathFinder pathFinder;
    public GridHighlight moveHighlight;
    public GridHighlight attackHighlight;

    private void Awake()
    {
        stageGrid = GetComponent<Grid>();
        pathFinder = GetComponent<PathFinder>();
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
}