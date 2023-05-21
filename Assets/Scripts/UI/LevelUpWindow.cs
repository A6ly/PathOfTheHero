using TMPro;
using UnityEngine;

public class LevelUpWindow : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;

    public void UpdateLevelText(int userLevel)
    {
        levelText.text = $"{userLevel}";
    }
}