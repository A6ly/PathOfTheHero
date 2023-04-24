using UnityEngine;

public class GridObjectSelector : MonoBehaviour
{
    GridMousePointer gridMousePointer;
    CommandMenu commandMenu;
    StatusWindow statusWindow;

    private void Awake()
    {
        gridMousePointer = GetComponent<GridMousePointer>();
        commandMenu = GetComponent<CommandMenu>();
        statusWindow = GetComponent<StatusWindow>();
    }

    public Character selected;
    public Character hoverOverCharacter;

    GridObject hoverOverGridObject;
    Grid targetGrid;

    bool isSelected;

    Vector2Int positionOnGrid = new Vector2Int(-1, -1);

    private void Start()
    {
        targetGrid = FindObjectOfType<StageManager>().stageGrid;
    }

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
        hoverOverGridObject = targetGrid.GetPlacedObject(positionOnGrid);

        if (hoverOverGridObject != null)
        {
            hoverOverCharacter = hoverOverGridObject.GetComponent<Character>();
        }
        else
        {
            hoverOverCharacter = null;
        }
    }

    private void SelectObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (hoverOverCharacter != null && selected == null)
            {
                selected = hoverOverCharacter;
                isSelected = true;

                commandMenu.OpenMenu(selected.GetComponent<CharacterTurn>());
                statusWindow.OpenWindow(selected.GetComponent<Character>());
            }
        }
    }

    private void DeselectObject()
    {
        if (Input.GetMouseButtonDown(1))
        {
            selected = null;

            commandMenu.CloseMenu();
            statusWindow.CloseWindow();
        }
    }

    public void Deselect()
    {
        isSelected = false;
    }
}
