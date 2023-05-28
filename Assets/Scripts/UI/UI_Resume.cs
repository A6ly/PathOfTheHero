using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using static Define;

public class UI_Resume : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countText;

    private void OnEnable()
    {
        StartCoroutine(ResumeStart());
    }

    IEnumerator ResumeStart()
    {
        int countTime = 3;

        while (countTime >= 0)
        {
            Managers.Sound.Play("TickEffect", SoundType.Effect);

            countText.text = $"{countTime}";

            yield return new WaitForSecondsRealtime(1.0f);

            countTime--;
        }

        Time.timeScale = 1f;
        Managers.Sound.ResumeBgm();
        CameraManager.Instance.ResumeBattle();
        DOTween.PlayAll();

        gameObject.SetActive(false);

        yield break;
    }
}
