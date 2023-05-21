using TMPro;
using UnityEngine;

public class CharacterStatValueText : MonoBehaviour
{
    TextMeshProUGUI valueText;

    private void Awake()
    {
        valueText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateText(int val)
    {
        valueText.text = val.ToString();
    }
}