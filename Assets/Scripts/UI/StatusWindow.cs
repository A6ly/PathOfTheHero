using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using static Define;

public class StatusWindow : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] Image damageTypeIcon;
    [SerializeField] Slider hpBar;
    [SerializeField] Slider mpBar;

    [SerializeField] CharacterStatValueText strValueText;
    [SerializeField] CharacterStatValueText intValueText;
    [SerializeField] CharacterStatValueText defValueText;
    [SerializeField] CharacterStatValueText regValueText;
    [SerializeField] CharacterStatValueText rangeValueText;

    Sprite physicalTypeIcon;
    Sprite magicalTypeIcon;

    private void Awake()
    {
        physicalTypeIcon = Addressables.LoadAssetAsync<Sprite>("PhysicalTypeIconUI").WaitForCompletion();
        magicalTypeIcon = Addressables.LoadAssetAsync<Sprite>("MagicalTypeIconUI").WaitForCompletion();

        gameObject.SetActive(false);
    }

    public void UpdateStatus(Character character)
    {
        if (character.CompareTag("Player"))
        {
            nameText.text = $"[<color=#9BBFEA>{character.stat.Name}</color>]";
        }
        else
        {
            nameText.text = $"[<color=#FF60DB>{character.stat.Name}</color>]";
        }
        
        hpBar.maxValue = character.stat.MaxHp;
        hpBar.value = character.stat.Hp;
        mpBar.maxValue = character.stat.MaxMp;
        mpBar.value = character.stat.Mp;

        switch (character.stat.DamageType)
        {
            case DamageType.Physical:
                damageTypeIcon.sprite = physicalTypeIcon;
                break;
            case DamageType.Magical:
                damageTypeIcon.sprite = magicalTypeIcon;
                break;
        }

        strValueText.UpdateText(character.stat.Strength);
        intValueText.UpdateText(character.stat.Intelligence);
        defValueText.UpdateText(character.stat.Defense);
        regValueText.UpdateText(character.stat.Resistance);
        rangeValueText.UpdateText(character.stat.AttackRange);
    }

    private void Clear()
    {
        if (physicalTypeIcon != null)
        {
            Addressables.Release(physicalTypeIcon);
        }

        if (magicalTypeIcon != null)
        {
            Addressables.Release(magicalTypeIcon);
        }
    }

    private void OnDestroy()
    {
        Clear();
    }
}
