using UnityEngine;

public class CommandMenu : MonoBehaviour
{
    [SerializeField] GameObject commandPanel;
    [SerializeField] GameObject moveButton;
    [SerializeField] GameObject attackButton;
    [SerializeField] GameObject endTurnButton;

    GridObjectSelector gridObjectSelector;

    public void OpenPanel(CharacterTurn characterTurn)
    {
        commandPanel.SetActive(true);
    }
}
