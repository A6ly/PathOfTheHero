using UnityEngine;
using static Define;

public class TurnManager : MonoBehaviour
{
    private static TurnManager instance;
    public static TurnManager Instance { get { return instance; } }

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] CharacterContainer playerContainer;
    [SerializeField] CharacterContainer enemyContainer;

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
                //opponentForceContainer.GetComponent<BattleAI>().StartBattle();
                break;
            case CharacterType.Enemy:
                currentTurn = CharacterType.Player;
                break;
        }

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
}