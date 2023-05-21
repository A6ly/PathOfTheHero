using DG.Tweening;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using static Define;

public class TurnManager : MonoBehaviour
{
    static TurnManager instance;
    public static TurnManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TurnManager>();
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

    public bool isEndStage;

    [SerializeField] GridObjectSelector gridObjectSelector;
    [SerializeField] CharacterContainer playerContainer;
    [SerializeField] CharacterContainer enemyContainer;
    [SerializeField] GameObject endTurnButton;
    [SerializeField] GameObject stageClearWindow;
    [SerializeField] GameObject gameOverWindow;
    [SerializeField] LevelUpWindow levelUpWindow;
    [SerializeField] TMPro.TextMeshProUGUI currentTurnText;

    BattleAI battleAI;

    CharacterType currentTurn;

    private void Start()
    {
        battleAI = enemyContainer.GetComponent<BattleAI>();
        currentTurn = CharacterType.Player;

        UpdateTextOnScreen();
    }

    public void Add(CharacterTurn character)
    {
        if (character.characterType == CharacterType.Player)
        {
            playerContainer.Add(character);
        }
        else if (character.characterType == CharacterType.Enemy)
        {
            enemyContainer.Add(character);
        }
    }

    private void ResetTurnToContainer()
    {
        switch (currentTurn)
        {
            case CharacterType.Player:
                playerContainer.ResetTurn();
                break;
            case CharacterType.Enemy:
                enemyContainer.ResetTurn();
                break;
        }
    }

    public void NextTurn()
    {
        switch (currentTurn)
        {
            case CharacterType.Player:
                currentTurn = CharacterType.Enemy;
                endTurnButton.SetActive(false);
                gridObjectSelector.Deselect();
                ResetTurnToContainer();
                battleAI.StartBattle();
                break;
            case CharacterType.Enemy:
                currentTurn = CharacterType.Player;
                endTurnButton.SetActive(true);
                gridObjectSelector.Deselect();
                ResetTurnToContainer();
                break;
        }

        UpdateTextOnScreen();
    }

    public bool CheckCurrentTurn(CharacterTurn character)
    {
        if (character.characterType == currentTurn)
        {
            return true;
        }

        return false;
    }

    public void CheckEndTurn()
    {
        switch (currentTurn)
        {
            case CharacterType.Player:
                if (enemyContainer.CheckAllDead())
                {
                    isEndStage = true;
                    stageClearWindow.SetActive(true);
                    Managers.Sound.StopBgm();
                    Managers.Data.ClearStage(StageManager.Instance.CurrentStageNum);
                    if (Managers.Data.AddExp(StageManager.Instance.CurrentStageExp))
                    {
                        levelUpWindow.gameObject.SetActive(true);
                        levelUpWindow.UpdateLevelText(Managers.Data.UserData.UserLevel);
                        Managers.Sound.Play("LevelUpEffect", SoundType.Effect);
                    }
                    else
                    {
                        Managers.Sound.Play("StageClearEffect", SoundType.Effect);
                    }

                    Managers.Data.Save();
                }
                else if (playerContainer.CheckEndTurn())
                {
                    NextTurn();
                }
                break;
            case CharacterType.Enemy:
                if (playerContainer.CheckAllDead())
                {
                    isEndStage = true;
                    gameOverWindow.SetActive(true);
                    Managers.Sound.StopBgm();
                    Managers.Sound.Play("GameOverEffect", SoundType.Effect);
                }
                break;
        }
    }

    private void UpdateTextOnScreen()
    {
        currentTurnText.text = $"{currentTurn}\nTurn";
    }

    public void EnableEndTurnButton()
    {
        endTurnButton.SetActive(true);
    }

    public void DisableEndTurnButton()
    {
        endTurnButton.SetActive(false);
    }
}