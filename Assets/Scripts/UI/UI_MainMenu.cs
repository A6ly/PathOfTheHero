using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Define;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] Slider userExpBar;
    [SerializeField] TextMeshProUGUI userExpText;
    [SerializeField] TextMeshProUGUI userLevelText;
    [SerializeField] GameObject settings;

    private void Start()
    {
        Managers.Sound.Play("MainBgm", SoundType.Bgm);
        userExpBar.maxValue = Managers.Data.UserData.UserMaxExp;
        userExpBar.value = Managers.Data.UserData.UserExp;
        userExpText.text = $"{Managers.Data.UserData.UserExp}/{Managers.Data.UserData.UserMaxExp}";
        userLevelText.text = $"{Managers.Data.UserData.UserLevel}";
    }

    public void PlayButton()
    {
        Managers.Sound.Play("Button01Effect", SoundType.Effect);
        SceneManager.LoadScene("StageSelect", LoadSceneMode.Single);
    }

    public void SettingsButton()
    {
        Managers.Sound.Play("Button01Effect", SoundType.Effect);
        settings.SetActive(true);
    }

    public void ExitButton()
    {
        Managers.Sound.Play("Button01Effect", SoundType.Effect);
        Managers.Data.Save();
        Application.Quit();
    }
}