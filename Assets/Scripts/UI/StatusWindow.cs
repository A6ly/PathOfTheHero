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
    [SerializeField] TextMeshProUGUI hpValueText;
    [SerializeField] TextMeshProUGUI mpValueText;
    [SerializeField] TextMeshProUGUI strValueText;
    [SerializeField] TextMeshProUGUI intValueText;
    [SerializeField] TextMeshProUGUI defValueText;
    [SerializeField] TextMeshProUGUI resValueText;
    [SerializeField] TextMeshProUGUI attackRangeValueText;
    [SerializeField] TextMeshProUGUI skillRangeValueText;

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
            nameText.text = $"[<color=#00EBFB>{character.stat.Name}</color>]";
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

        hpValueText.text = $"{character.stat.Hp}/{character.stat.MaxHp}";
        mpValueText.text = $"{character.stat.Mp}/{character.stat.MaxMp}";
        strValueText.text = $"{character.stat.Strength}";
        intValueText.text = $"{character.stat.Intelligence}";
        defValueText.text = $"{character.stat.Defense}";
        resValueText.text = $"{character.stat.Resistance}";
        attackRangeValueText.text = $"{character.stat.AttackRange}";
        skillRangeValueText.text = $"{character.stat.SkillRange}";
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
