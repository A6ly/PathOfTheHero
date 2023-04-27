using UnityEngine;

public class CommandMenu : MonoBehaviour
{
    [SerializeField] CommandInput commandInput;
    [SerializeField] GameObject commandMenu;
    [SerializeField] GameObject moveButton;
    [SerializeField] GameObject attackButton;

    CharacterTurn character;

    public void OpenMenu(CharacterTurn characterTurn)
    {
        if (TurnManager.Instance.CheckCurrentTurn(characterTurn))
        {
            character = characterTurn;

            commandMenu.SetActive(true);
            moveButton.SetActive(character.canMove);
            attackButton.SetActive(character.canAttack);
        }
    }

    public void CloseMenu()
    {
        character = null;

        commandMenu.SetActive(false);
    }

    public void MoveCommand()
    {
        if (character.canMove)
        {
            commandInput.SetCommandType(Define.CommandType.Move);
            commandInput.InitCommand();
            CloseMenu();
        }
    }

    public void AttackCommand()
    {
        if (character.canAttack)
        {
            commandInput.SetCommandType(Define.CommandType.Attack);
            commandInput.InitCommand();
            CloseMenu();
        }
    }
}