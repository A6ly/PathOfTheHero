using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UI_Settings : MonoBehaviour
{
    [SerializeField] Slider effectSoundBar;
    [SerializeField] Slider BgmSoundBar;

    private void Start()
    {
        effectSoundBar.value = Managers.Data.UserData.EffectVolume;
        BgmSoundBar.value = Managers.Data.UserData.BgmVolume;

        effectSoundBar.onValueChanged.AddListener(SetEffectVolume);
        BgmSoundBar.onValueChanged.AddListener(SetBgmVolume);
    }

    public void CloseButton()
    {
        Managers.Sound.Play("Button01Effect", SoundType.Effect);
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
}
