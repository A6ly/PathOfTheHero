using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Define;

public class UI_StageSelect : MonoBehaviour
{
    Sprite completeStageButton;
    Sprite defaultStageButton;
    Sprite lockStageButton;
    StageButton[] buttons;

    private void Awake()
    {
        completeStageButton = Addressables.LoadAssetAsync<Sprite>("CompleteButtonUI").WaitForCompletion();
        defaultStageButton = Addressables.LoadAssetAsync<Sprite>("DefaultStageButtonUI").WaitForCompletion();
        lockStageButton = Addressables.LoadAssetAsync<Sprite>("LockStageButtonUI").WaitForCompletion();
    }

    private void Start()
    {
        Managers.Sound.Play("MainBgm", SoundType.Bgm);
        LoadButtons();
    }

    private void LoadButtons()
    {
        buttons = GetComponentsInChildren<StageButton>(true);

        foreach (StageButton button in buttons)
        {
            if (button.stageNum < Managers.Data.UserData.CurrentStage)
            {
                button.GetComponent<Button>().interactable = true;
                button.GetComponent<Image>().sprite = completeStageButton;
            }
            else if (button.stageNum == Managers.Data.UserData.CurrentStage)
            {
                button.GetComponent<Button>().interactable = true;
                button.GetComponent<Image>().sprite = defaultStageButton;
            }
            else
            {
                button.GetComponent<Button>().interactable = false;
                button.GetComponent<Image>().sprite = lockStageButton;
            }
        }
    }

    public void MainMenuButton()
    {
        Managers.Sound.Play("Button01Effect", SoundType.Effect);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void LoadStageButton(string stageName)
    {
        Managers.Sound.Play("Button01Effect", SoundType.Effect);
        SceneManager.LoadScene("BattleEssential", LoadSceneMode.Single);
        SceneManager.LoadScene(stageName, LoadSceneMode.Additive);
    }

    private void Clear()
    {
        if (completeStageButton != null)
        {
            Addressables.Release(completeStageButton);
        }

        if (defaultStageButton != null)
        {
            Addressables.Release(defaultStageButton);
        }

        if (lockStageButton != null)
        {
            Addressables.Release(lockStageButton);
        }
    }

    private void OnDestroy()
    {
        Clear();
    }
}
