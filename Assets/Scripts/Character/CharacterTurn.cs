using UnityEngine;
using static Define;

public class CharacterTurn : MonoBehaviour
{
    Character character;

    public CharacterType characterType;

    public bool canMove;
    public bool canAttack;
    public bool canSkill;

    private void Start()
    {
        character = GetComponent<Character>();
        ResetTurn();
        AddToTurnManager();
    }

    public void ResetTurn()
    {
        canMove = true;
        canAttack = true;
        canSkill = character.CheckSkillAvailability();
    }

    public bool CheckEndTurn()
    {
        if (!canMove && !canAttack && !canSkill)
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