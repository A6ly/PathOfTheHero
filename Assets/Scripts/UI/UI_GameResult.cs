using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Define;

public class UI_GameResult : MonoBehaviour
{
    public void NextStage()
    {
        Managers.Sound.Play("Button01Effect", SoundType.Effect);

        string NextStageName = $"Stage{StageManager.Instance.CurrentStageNum + 1:D2}";

        SceneManager.LoadScene("BattleEssential", LoadSceneMode.Single);
        SceneManager.LoadScene(NextStageName, LoadSceneMode.Additive);
    }

    public void RetryStage()
    {
        Managers.Sound.Play("Button01Effect", SoundType.Effect);
        SceneManager.LoadScene("BattleEssential", LoadSceneMode.Single);
        SceneManager.LoadScene(StageManager.Instance.CurrentStageName, LoadSceneMode.Additive);
    }

    public void Exit()
    {
        Managers.Sound.Play("Button01Effect", SoundType.Effect);
        DOTween.KillAll();
        SceneManager.LoadScene("StageSelect", LoadSceneMode.Single);
    }
}