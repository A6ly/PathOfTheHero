using System.Collections.Generic;
using UnityEngine;

public class CommandInput : MonoBehaviour
{
    CommandManager commandManager;
    GridMousePointer gridMousePointer;
    BattleManager battleManager;
    GridObjectSelector gridObjectSelector;
    StageManager stageManager;

    private void Awake()
    {
        commandManager = GetComponent<CommandManager>();
        gridMousePointer = GetComponent<GridMousePointer>();
        battleManager = GetComponent<BattleManager>();
        gridObjectSelector = GetComponent<GridObjectSelector>();
    }

    private void Start()
    {
        stageManager = FindObjectOfType<StageManager>();
    }

    Define.CommandType currentCommand;
    bool isInputCommand;

    public void SetCommandType(Define.CommandType commandType)
    {
        currentCommand = commandType;
    }

    public void HighlightWalkableGround()
    {
        battleManager.CheckWalkableGround(gridObjectSelector.selected);
    }

    public void InitCommand()
    {
        isInputCommand = true;

        switch (currentCommand)
        {
            case Define.CommandType.Move:
                HighlightWalkableGround();
                break;
            case Define.CommandType.Attack:
                battleManager.CalculateAttackArea(gridObjectSelector.selected.GetComponent<GridObject>().positionOnGrid, gridObjectSelector.selected.attackRange);
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
            case Define.CommandType.Move:
                MoveCommandInput();
                break;
            case Define.CommandType.Attack:
                AttackCommandInput();
                break;
        }
    }

    private void StopCommandInput()
    {
        gridObjectSelector.Deselect();
        gridObjectSelector.enabled = true;
        isInputCommand = false;
    }

    private void MoveCommandInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!battleManager.CheckPlacedObject(gridMousePointer.positionOnGrid))
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
            stageManager.ClearMoveHighlight();
            stageManager.ClearPathFinder();
        }
    }

    private void AttackCommandInput()
    {
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
            stageManager.ClearAttackHighlight();
        }
    }
}