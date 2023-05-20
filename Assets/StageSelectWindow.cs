using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelectWindow : MonoBehaviour
{
    Button[] buttons;

    private void Start()
    {
        LoadButtons();
    }

    private void LoadButtons()
    {
        buttons = GetComponentsInChildren<Button>(true);

        foreach (Button button in buttons)
        {
            if (button.GetComponent<StageButton>().stageNum <= Managers.Data.UserData.CurrentStage)
            {
                button.interactable = true;
            }
            else
            {
                button.interactable = false;
            }
        }
    }

    public void LoadStage(string stageName)
    {
        SceneManager.LoadScene("BattleEssential", LoadSceneMode.Single);
        SceneManager.LoadScene(stageName, LoadSceneMode.Additive);
    }
}
