using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Command
{
    public Character character;
    public Vector2Int selectedGrid;
    public CommandType commandType;

    public Command(Character character, Vector2Int selectedGrid, CommandType commandType)
    {
        this.character = character;
        this.selectedGrid = selectedGrid;
        this.commandType = commandType;
    }

    public List<PathNode> path;
    public GridObject target;
}

public class CommandManager : MonoBehaviour
{
    static CommandManager instance;
    public static CommandManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CommandManager>();
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

    Command currentCommand;

    private void Update()
    {
        if (currentCommand != null)
        {
            ExecuteCommand();
        }
    }

    private void ExecuteCommand()
    {
        switch (currentCommand.commandType)
        {
            case CommandType.Move:
                MoveCommand();
                break;
            case CommandType.Attack:
                AttackCommand();
                break;
            case CommandType.Skill:
                SkillCommand();
                break;
        }
    }

    private void MoveCommand()
    {
        Character character = currentCommand.character;
        character.GetComponent<CharacterController>().Move(currentCommand.path);
        character.GetComponent<CharacterTurn>().canMove = false;

        currentCommand = null;
        StageManager.Instance.ClearPathFinder();
        StageManager.Instance.ClearMoveHighlight();
    }

    private void AttackCommand()
    {
        Character character = currentCommand.character;
        character.GetComponent<CharacterController>().Attack(currentCommand.target);
        character.GetComponent<CharacterTurn>().canAttack = false;
        //victoryConditionManager.CheckPlayerVictory();

        currentCommand = null;
        StageManager.Instance.ClearAttackHighlight();
    }

    private void SkillCommand()
    {
        Character character = currentCommand.character;
        character.GetComponent<CharacterController>().Skill(currentCommand.target);
        character.GetComponent<CharacterTurn>().canSkill = false;
        //victoryConditionManager.CheckPlayerVictory();

        currentCommand = null;
        StageManager.Instance.ClearAttackHighlight();
    }

    public void AddMoveCommand(Character character, Vector2Int selectedGrid, List<PathNode> path)
    {
        currentCommand = new Command(character, selectedGrid, CommandType.Move);
        currentCommand.path = path;
    }

    public void AddAttackCommand(Character attacker, Vector2Int selectGrid, GridObject target)
    {
        currentCommand = new Command(attacker, selectGrid, CommandType.Attack);
        currentCommand.target = target;
    }

    public void AddSkillCommand(Character attacker, Vector2Int selectGrid, GridObject target)
    {
        currentCommand = new Command(attacker, selectGrid, CommandType.Skill);
        currentCommand.target = target;
    }
}