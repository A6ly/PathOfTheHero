using DG.Tweening;
using UnityEngine;
using static Define;

public class UI_PlayGame : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void PauseButton()
    {
        Managers.Sound.Play("Button01Effect", SoundType.Effect);
        Managers.Sound.PauseBgm();
        CameraManager.Instance.PauseBattle();
        DOTween.PauseAll();
        Time.timeScale = 0f;

        pauseMenu.gameObject.SetActive(true);
    }
}
