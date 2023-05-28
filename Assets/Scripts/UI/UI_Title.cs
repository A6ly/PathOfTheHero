using UnityEngine;
using UnityEngine.SceneManagement;
using static Define;

public class UI_Title : MonoBehaviour
{
    private void Start()
    {
        Managers.Sound.Play("MainBgm", SoundType.Bgm);
    }

    public void StartButton()
    {
        Managers.Sound.Play("Button01Effect", SoundType.Effect);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}