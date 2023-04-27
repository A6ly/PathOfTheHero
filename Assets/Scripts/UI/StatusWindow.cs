using UnityEngine;
using static Define;

public class StatusWindow : MonoBehaviour
{
    [SerializeField] GridObjectSelector gridObjectSelector;
    [SerializeField] GameObject characterStatusWindow;
    [SerializeField] GameObject monsterStatusWindow;

    Character currentCharacter;
    CharacterTurn currentCharacterTurn;

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
                currentCharacterTurn = currentCharacter.GetComponent<CharacterTurn>();
                //statusPanel.UpdateStatus(currentCharacter);
            }
        }
        else
        {
            if (gridObjectSelector.hoverOverCharacter != null)
            {
                currentCharacter = gridObjectSelector.hoverOverCharacter;
                currentCharacterTurn = currentCharacter.GetComponent<CharacterTurn>();
                OpenWindow();

                return;
            }
        }
    }

    public void OpenWindow()
    {
        if (currentCharacterTurn.characterType == CharacterType.Player)
        {
            characterStatusWindow.SetActive(true);
            isActive = true;
        }
        else if (currentCharacterTurn.characterType == CharacterType.Enemy)
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