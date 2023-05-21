using UnityEngine;
using static Define;

public class StageManager : MonoBehaviour
{
    static StageManager instance;
    public static StageManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<StageManager>();
            }

            return instance;
        }
    }

    [SerializeField] Grid stageGrid;
    [SerializeField] PathFinder pathFinder;
    [SerializeField] GridHighlight moveHighlight;
    [SerializeField] GridHighlight attackHighlight;

    public Grid StageGrid { get { return stageGrid; } }
    public PathFinder PathFinder { get { return pathFinder; } }
    public GridHighlight MoveHighlight { get { return moveHighlight; } }
    public GridHighlight AttackHighlight { get { return attackHighlight; } }
    public string CurrentStageName { get; private set; }
    public int CurrentStageNum { get; private set; }
    public int CurrentStageExp { get; private set; }

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

        CurrentStageName = gameObject.scene.name;
        CurrentStageNum = int.Parse(CurrentStageName.Substring(CurrentStageName.Length - 2));
        CurrentStageExp = CurrentStageNum * 500;
    }

    private void Start()
    {
        Managers.Sound.Play("Battle01Bgm", SoundType.Bgm);
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