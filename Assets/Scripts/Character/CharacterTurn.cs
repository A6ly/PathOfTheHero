using UnityEngine;
using static Define;

public class CharacterTurn : MonoBehaviour
{
    public CharacterType characterType;

    public bool canMove;
    public bool canAttack;

    private void Start()
    {
        ResetTurn();
        AddToTurnManager();
    }

    public void ResetTurn()
    {
        canMove = true;
        canAttack = true;
    }

    public bool CheckEndTurn()
    {
        if (!canMove && !canAttack)
        {
            return true;
        }

        return false;
    }

    private void AddToTurnManager()
    {
        TurnManager.Instance.Add(this);
    }
}