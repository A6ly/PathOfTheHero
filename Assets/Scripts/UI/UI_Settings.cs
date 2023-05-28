using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UI_Settings : MonoBehaviour
{
    [SerializeField] Slider effectSoundBar;
    [SerializeField] Slider BgmSoundBar;
    [SerializeField] Button englishButton;
    [SerializeField] Button koreanButton;

    private void Start()
    {
        effectSoundBar.value = Managers.Data.UserData.EffectVolume;
        BgmSoundBar.value = Managers.Data.UserData.BgmVolume;

        effectSoundBar.onValueChanged.AddListener(SetEffectVolume);
        BgmSoundBar.onValueChanged.AddListener(SetBgmVolume);

        englishButton.onClick.AddListener(() =>
        {
            Managers.Sound.Play("Button01Effect", SoundType.Effect);
            SetLocalization(0);
        });

        koreanButton.onClick.AddListener(() =>
        {
            Managers.Sound.Play("Button01Effect", SoundType.Effect);
            SetLocalization(1);
        });

        SetLanguageButtons();
    }

    public void CloseButton()
    {
        Managers.Sound.Play("Button01Effect", SoundType.Effect);
        gameObject.SetActive(false);
    }

    public void SaveButton()
    {
        Managers.Sound.Play("Button01Effect", SoundType.Effect);
        Managers.Data.Save();
        gameObject.SetActive(false);
    }

    private void SetEffectVolume(float volume)
    {
        Managers.Sound.SetEffectVolume(volume);
    }

    private void SetBgmVolume(float volume)
    {
        Managers.Sound.SetBgmVolume(volume);
    }

    private void SetLanguageButtons()
    {
        if (Managers.Data.UserData.CurrentLanguage == 0)
        {
            englishButton.interactable = false;
            koreanButton.interactable = true;
        }
        else if (Managers.Data.UserData.CurrentLanguage == 1)
        {
            englishButton.interactable = true;
            koreanButton.interactable = false;
        }
    }

    private void SetLocalization(int index)
    {
        Managers.Data.SetLocalization(index);
        SetLanguageButtons();
    }
}
