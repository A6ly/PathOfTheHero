using UnityEngine;

public class CharacterTurn : MonoBehaviour
{
    public Define.CharacterType characterType;

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

    private void AddToTurnManager()
    {
        TurnManager.Instance.Add(this);
    }
}