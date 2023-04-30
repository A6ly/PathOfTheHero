using UnityEngine;
using static Define;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] GridObjectSelector gridObjectSelector;
    [SerializeField] CharacterContainer playerContainer;
    [SerializeField] CharacterContainer enemyContainer;
    [SerializeField] GameObject endTurnButton;
    BattleAI battleAI;

    private void Start()
    {
        battleAI = enemyContainer.GetComponent<BattleAI>();
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

    CharacterType currentTurn;

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
                battleAI.StartBattle();
                break;
            case CharacterType.Enemy:
                currentTurn = CharacterType.Player;
                endTurnButton.SetActive(true);
                break;
        }

        gridObjectSelector.Deselect();
        ResetTurnToContainer();
    }

    public bool CheckCurrentTurn(CharacterTurn character)
    {
        if (character.characterType == currentTurn)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}