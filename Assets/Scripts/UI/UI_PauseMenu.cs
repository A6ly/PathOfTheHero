using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Define;

public class UI_PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject resume;

    public void RetryButton()
    {
        Time.timeScale = 1f;
        Managers.Sound.Play("Button01Effect", SoundType.Effect);
        SceneManager.LoadScene("BattleEssential", LoadSceneMode.Single);
        SceneManager.LoadScene(StageManager.Instance.CurrentStageName, LoadSceneMode.Additive);
    }

    public void ResumeButton()
    {
        Managers.Sound.Play("Button01Effect", SoundType.Effect);
        resume.SetActive(true);
        gameObject.SetActive(false);
    }

    public void QuitButton()
    {
        Managers.Sound.Play("Button01Effect", SoundType.Effect);
        SceneManager.LoadScene("StageSelect", LoadSceneMode.Single);
    }
}
