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
    StageManager stageManager;

    private void Start()
    {
        stageManager = FindObjectOfType<StageManager>();
    }

    public Command currentCommand;

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
        }
    }

    private void MoveCommand()
    {
        Character character = currentCommand.character;
        character.GetComponent<CharacterController>().Move(currentCommand.path);
        character.GetComponent<CharacterTurn>().canMove = false;

        currentCommand = null;
        stageManager.ClearPathFinder();
        stageManager.ClearMoveHighlight();
    }

    private void AttackCommand()
    {
        Character character = currentCommand.character;
        character.GetComponent<CharacterController>().Attack(currentCommand.target);
        character.GetComponent<CharacterTurn>().canAttack = false;
        //victoryConditionManager.CheckPlayerVictory();

        currentCommand = null;
        stageManager.ClearAttackHighlight();
    }

    public void AddMoveCommand(Character character, Vector2Int selectedGrid, List<PathNode> path)
    {
        currentCommand = new Command(character, selectedGrid, CommandType.Move);
        currentCommand.path = path;
    }

    internal void AddAttackCommand(Character attacker, Vector2Int selectGrid, GridObject target)
    {
        currentCommand = new Command(attacker, selectGrid, CommandType.Attack);
        currentCommand.target = target;
    }
}