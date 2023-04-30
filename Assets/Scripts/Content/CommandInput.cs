using System.Collections.Generic;
using UnityEngine;
using static Define;

public class CommandInput : MonoBehaviour
{
    [SerializeField] GridMousePointer gridMousePointer;
    [SerializeField] GridObjectSelector gridObjectSelector;
    [SerializeField] BattleManager battleManager;
    [SerializeField] CommandManager commandManager;

    CommandType currentCommand;
    bool isInputCommand;

    public void SetCommandType(CommandType commandType)
    {
        currentCommand = commandType;
    }

    public void InitCommand()
    {
        isInputCommand = true;

        switch (currentCommand)
        {
            case CommandType.Move:
                battleManager.CalculateWalkableGround(gridObjectSelector.selected);
                break;
            case CommandType.Attack:
                battleManager.CalculateAttackArea(gridObjectSelector.selected.GetComponent<GridObject>().positionOnGrid, gridObjectSelector.selected.attackRange, gridObjectSelector.selected.tag);
                break;
        }
    }

    private void Update()
    {
        if (!isInputCommand)
        {
            return;
        }

        switch (currentCommand)
        {
            case CommandType.Move:
                MoveCommandInput();
                break;
            case CommandType.Attack:
                AttackCommandInput();
                break;
        }
    }

    private void StopCommandInput()
    {
        gridObjectSelector.Deselect();
        isInputCommand = false;
    }

    private void MoveCommandInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!StageManager.Instance.StageGrid.CheckPlacedObject(gridMousePointer.positionOnGrid))
            {
                List<PathNode> path = battleManager.GetPath(gridMousePointer.positionOnGrid);

                if (path != null && path.Count > 0)
                {
                    commandManager.AddMoveCommand(gridObjectSelector.selected, gridMousePointer.positionOnGrid, path);
                    StopCommandInput();
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            StopCommandInput();
            StageManager.Instance.ClearMoveHighlight();
            StageManager.Instance.ClearPathFinder();
        }
    }

    private void AttackCommandInput()
    {
        if(battleManager.CheckNoAttackablePosition())
        {
            StopCommandInput();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (battleManager.CheckAttackable(gridMousePointer.positionOnGrid))
            {
                GridObject gridObject = battleManager.GetAttackTarget(gridMousePointer.positionOnGrid);

                if (gridObject != null)
                {
                    commandManager.AddAttackCommand(gridObjectSelector.selected, gridMousePointer.positionOnGrid, gridObject);
                    StopCommandInput();
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            StopCommandInput();
            StageManager.Instance.ClearAttackHighlight();
        }
    }
}