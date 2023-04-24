using UnityEngine;

public class CharacterTurn : MonoBehaviour
{
    public bool canMove;
    public bool canAttack;

    private void Start()
    {
        ResetTurn();
    }

    public void ResetTurn()
    {
        canMove = true;
        canAttack = true;
    }
}