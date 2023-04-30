using UnityEngine;

public class GridObjectSelector : MonoBehaviour
{
    GridMousePointer gridMousePointer;
    CommandMenu commandMenu;

    private void Awake()
    {
        gridMousePointer = GetComponent<GridMousePointer>();
        commandMenu = GetComponent<CommandMenu>();
    }

    public Character selected;
    public Character hoverOverCharacter;

    GridObject hoverOverGridObject;

    bool isSelected;

    Vector2Int positionOnGrid = new Vector2Int(-1, -1);

    private void Update()
    {
        if (positionOnGrid != gridMousePointer.positionOnGrid)
        {
            OnMouseOverObject();
        }

        SelectObject();
        DeselectObject();
    }

    private void LateUpdate()
    {
        if (selected != null)
        {
            if (isSelected == false)
            {
                selected = null;
            }
        }
    }

    private void OnMouseOverObject()
    {
        positionOnGrid = gridMousePointer.positionOnGrid;
        hoverOverGridObject = StageManager.Instance.StageGrid.GetPlacedObject(positionOnGrid);

        if (hoverOverGridObject != null)
        {
            hoverOverCharacter = hoverOverGridObject.GetComponent<Character>();
        }
        else
        {
            hoverOverCharacter = null;
        }
    }

    private void UpdateMenu()
    {
        if (selected != null)
        {
            commandMenu.OpenMenu(selected.GetComponent<CharacterTurn>());
        }
        else
        {
            commandMenu.CloseMenu();
        }
    }

    private void SelectObject()
    {
        OnMouseOverObject();

        if (Input.GetMouseButtonDown(0))
        {
            if (hoverOverCharacter != null && selected == null && TurnManager.Instance.CheckCurrentTurn(hoverOverCharacter.GetComponent<CharacterTurn>()))
            {
                selected = hoverOverCharacter;
                isSelected = true;

                if (hoverOverCharacter.CompareTag("Player"))
                {
                    UpdateMenu();
                }
            }
        }
    }

    private void DeselectObject()
    {
        if (Input.GetMouseButtonDown(1))
        {
            selected = null;

            commandMenu.CloseMenu();
        }
    }

    public void Deselect()
    {
        isSelected = false;
    }
}