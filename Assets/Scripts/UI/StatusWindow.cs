using UnityEngine;

public class StatusWindow : MonoBehaviour
{
    [SerializeField] GameObject characterStatusWindow;
    [SerializeField] GameObject monsterStatusWindow;

    GridObjectSelector gridObjectSelector;
    Character currentCharacter;

    bool isActive;

    private void Start()
    {
        gridObjectSelector = GetComponent<GridObjectSelector>();
    }

    private void Update()
    {
        OnMouseOverObject();
    }

    private void OnMouseOverObject()
    {
        if (isActive)
        {
            if (gridObjectSelector.hoverOverCharacter == null)
            {
                CloseWindow();

                return;
            }

            if (gridObjectSelector.hoverOverCharacter != currentCharacter)
            {
                currentCharacter = gridObjectSelector.hoverOverCharacter;
                //statusPanel.UpdateStatus(currentCharacter);
            }
        }
        else
        {
            if (gridObjectSelector.hoverOverCharacter != null)
            {
                currentCharacter = gridObjectSelector.hoverOverCharacter;
                OpenWindow();

                return;
            }
        }
    }

    public void OpenWindow()
    {
        characterStatusWindow.SetActive(true);
        isActive = true;
    }

    public void CloseWindow()
    {
        characterStatusWindow.SetActive(false);
        isActive = false;
    }
}