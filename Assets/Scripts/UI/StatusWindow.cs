using UnityEngine;

public class StatusWindow : MonoBehaviour
{
    [SerializeField] GameObject characterStatusWindow;
    [SerializeField] GameObject monsterStatusWindow;

    public void OpenWindow(Character character)
    {
        characterStatusWindow.SetActive(true);
    }

    //public void OpenWindow(Monster monster)
    //{
    //    monsterStatusWindow.SetActive(true);
    //}

    public void CloseWindow()
    {
        characterStatusWindow.SetActive(false);
        monsterStatusWindow.SetActive(false);
    }
}
