using UnityEngine;

public class StatusWindow : MonoBehaviour
{
    [SerializeField] GridObjectSelector gridObjectSelector;
    [SerializeField] GameObject characterStatusWindow;
    [SerializeField] GameObject monsterStatusWindow;

    Character currentCharacter;

    bool isActive;

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
        if (currentCharacter.CompareTag("Player"))
        {
            characterStatusWindow.SetActive(true);
            isActive = true;
        }
        else if (currentCharacter.CompareTag("Monster"))
        {
            monsterStatusWindow.SetActive(true);
            isActive = true;
        }
    }

    public void CloseWindow()
    {
        characterStatusWindow.SetActive(false);
        monsterStatusWindow.SetActive(false);
        isActive = false;
    }
}