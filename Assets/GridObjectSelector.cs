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

    public GridObject hoverOverGridObject;
    public GridObject selected;

    Grid targetGrid;

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

    private void OnMouseOverObject()
    {
        positionOnGrid = gridMousePointer.positionOnGrid;
        hoverOverGridObject = targetGrid.GetPlacedObject(positionOnGrid);
    }

    private void SelectObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (hoverOverGridObject != null)
            {
                selected = hoverOverGridObject;

                if (selected.CompareTag("Character"))
                {
                    commandMenu.OpenPanel(selected.GetComponent<CharacterTurn>());
                }
                else if (selected.CompareTag("Monster"))
                {

                }
            }
        }
    }

    private void DeselectObject()
    {
        if (Input.GetMouseButtonDown(1))
        {
            selected = null;
        }
    }
}
