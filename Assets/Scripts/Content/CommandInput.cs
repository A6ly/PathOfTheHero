using System.Collections.Generic;
using UnityEngine;
using static Define;

public class CommandInput : MonoBehaviour
{
    [SerializeField] GridMousePointer gridMousePointer;
    [SerializeField] GridObjectSelector gridObjectSelector;

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
                BattleManager.Instance.CalculateWalkableGround(gridObjectSelector.selected);
                break;
            case CommandType.Attack:
                BattleManager.Instance.CalculateAttackArea(gridObjectSelector.selected.GetComponent<GridObject>().positionOnGrid, gridObjectSelector.selected.stat.AttackRange, gridObjectSelector.selected.tag);
                break;
            case CommandType.Skill:
                BattleManager.Instance.CalculateAttackArea(gridObjectSelector.selected.GetComponent<GridObject>().positionOnGrid, gridObjectSelector.selected.stat.AttackRange, gridObjectSelector.selected.tag);
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
            case CommandType.Skill:
                SkillCommandInput();
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
                List<PathNode> path = BattleManager.Instance.GetPath(gridMousePointer.positionOnGrid);

                if (path != null && path.Count > 0)
                {
                    CommandManager.Instance.AddMoveCommand(gridObjectSelector.selected, gridMousePointer.positionOnGrid, path);
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
        if(BattleManager.Instance.CheckNoAttackablePosition())
        {
            StopCommandInput();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (BattleManager.Instance.CheckAttackable(gridMousePointer.positionOnGrid))
            {
                GridObject gridObject = BattleManager.Instance.GetAttackTarget(gridMousePointer.positionOnGrid);

                if (gridObject != null)
                {
                    CommandManager.Instance.AddAttackCommand(gridObjectSelector.selected, gridMousePointer.positionOnGrid, gridObject);
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

    private void SkillCommandInput()
    {
        if (BattleManager.Instance.CheckNoAttackablePosition())
        {
            StopCommandInput();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (BattleManager.Instance.CheckAttackable(gridMousePointer.positionOnGrid))
            {
                GridObject gridObject = BattleManager.Instance.GetAttackTarget(gridMousePointer.positionOnGrid);

                if (gridObject != null)
                {
                    CommandManager.Instance.AddSkillCommand(gridObjectSelector.selected, gridMousePointer.positionOnGrid, gridObject);
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