using UnityEngine;
using UnityEngine.UI;

public class StatusWindow : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI nameAndDamageTypeText;
    [SerializeField] Slider hpBar;
    [SerializeField] Slider mpBar;

    [SerializeField] CharacterStatValueText strValueText;
    [SerializeField] CharacterStatValueText intValueText;
    [SerializeField] CharacterStatValueText defValueText;
    [SerializeField] CharacterStatValueText regValueText;
    [SerializeField] CharacterStatValueText rangeValueText;

    public void UpdateStatus(Character character)
    {
        nameAndDamageTypeText.text = $"{character.stat.Name} [{character.stat.DamageType}]";
        hpBar.maxValue = character.stat.MaxHp;
        hpBar.value = character.stat.Hp;
        mpBar.maxValue = character.stat.MaxMp;
        mpBar.value = character.stat.Mp;

        strValueText.UpdateText(character.stat.Strength);
        intValueText.UpdateText(character.stat.Intelligence);
        defValueText.UpdateText(character.stat.Defense);
        regValueText.UpdateText(character.stat.Resistance);
        rangeValueText.UpdateText(character.stat.AttackRange);
    }
}
