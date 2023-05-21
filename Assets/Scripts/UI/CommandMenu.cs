using UnityEngine;
using static Define;

public class CommandMenu : MonoBehaviour
{
    [SerializeField] CommandInput commandInput;
    [SerializeField] GameObject commandMenu;
    [SerializeField] GameObject moveButton;
    [SerializeField] GameObject attackButton;
    [SerializeField] GameObject skillButton;

    CharacterTurn character;

    public void OpenMenu(CharacterTurn characterTurn)
    {
        character = characterTurn;

        commandMenu.SetActive(true);
        moveButton.SetActive(character.canMove);
        attackButton.SetActive(character.canAttack);
        skillButton.SetActive(character.canSkill);
    }

    public void CloseMenu()
    {
        character = null;

        commandMenu.SetActive(false);
    }

    public void MoveButton()
    {
        Managers.Sound.Play("Button01Effect", SoundType.Effect);

        commandInput.SetCommandType(CommandType.Move);
        commandInput.InitCommand();
        CloseMenu();
    }

    public void AttackButton()
    {
        Managers.Sound.Play("Button01Effect", SoundType.Effect);

        commandInput.SetCommandType(CommandType.Attack);
        commandInput.InitCommand();
        CloseMenu();
    }

    public void SkillButton()
    {
        Managers.Sound.Play("Button01Effect", SoundType.Effect);

        commandInput.SetCommandType(CommandType.Skill);
        commandInput.InitCommand();
        CloseMenu();
    }

    public void CancelButton()
    {
        Managers.Sound.Play("Button01Effect", SoundType.Effect);
    }

    public void EndTurnButton()
    {
        Managers.Sound.Play("Button01Effect", SoundType.Effect);
    }
}