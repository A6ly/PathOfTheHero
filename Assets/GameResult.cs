using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameResult : MonoBehaviour
{
    public void NextStage()
    {
        string NextStageName = $"Stage{StageManager.Instance.CurrentStageNum + 1:D2}";

        SceneManager.LoadScene("BattleEssential", LoadSceneMode.Single);
        SceneManager.LoadScene(NextStageName, LoadSceneMode.Additive);
    }

    public void RetryStage()
    {
        SceneManager.LoadScene("BattleEssential", LoadSceneMode.Single);
        SceneManager.LoadScene(StageManager.Instance.CurrentStageName, LoadSceneMode.Additive);
    }

    public void Exit()
    {
        DOTween.CompleteAll();
        DOTween.KillAll();
        SceneManager.LoadScene("StageSelect", LoadSceneMode.Single);
    }
}