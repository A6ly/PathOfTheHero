using UnityEngine;

public class StatusWindowController : MonoBehaviour
{
    [SerializeField] GridObjectSelector gridObjectSelector;
    [SerializeField] StatusWindow characterStatusWindow;

    Character currentCharacter;

    bool isActive;

    private void Update()
    {
        if (!TurnManager.Instance.isEndStage)
        {
            OnMouseOverObject();
        }
    }

    private void OnMouseOverObject()
    {
        if (isActive)
        {
            characterStatusWindow.UpdateStatus(currentCharacter);

            if (gridObjectSelector.hoverOverCharacter == null)
            {
                CloseWindow();

                return;
            }

            if (gridObjectSelector.hoverOverCharacter != currentCharacter)
            {
                currentCharacter = gridObjectSelector.hoverOverCharacter;
                characterStatusWindow.UpdateStatus(currentCharacter);
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
        characterStatusWindow.gameObject.SetActive(true);
        isActive = true;
        characterStatusWindow.UpdateStatus(currentCharacter);
    }

    public void CloseWindow()
    {
        characterStatusWindow.gameObject.SetActive(false);
        isActive = false;
    }
}