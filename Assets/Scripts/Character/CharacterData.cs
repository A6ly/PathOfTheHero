using UnityEngine;
using static Define;

[CreateAssetMenu]
public class CharacterData : ScriptableObject
{
    [SerializeField] string characterName = "Nameless";
    [SerializeField] DamageType damageType;
    [SerializeField] float movementPoint = 50.0f;
    [SerializeField] int attackRange = 1;
    [SerializeField] int skillRange = 1;
    [SerializeField] int strength = 10;
    [SerializeField] int intelligence = 10;
    [SerializeField] int defense = 1;
    [SerializeField] int resistance = 1;
    [SerializeField] SkillType skillType;
    [SerializeField] int skillMpCost = 50;
    [SerializeField] int skillDamageRatio = 3;
    [SerializeField] float criticalChance = 0.1f;
    [SerializeField] float criticalDamageRatio = 1.5f;
    [SerializeField] int maxHp = 100;
    [SerializeField] int maxMp = 100;

    public string CharacterName { get { return characterName; } }
    public DamageType ClassType { get { return damageType; } }
    public float MovementPoint { get { return movementPoint; } }
    public int AttackRange { get { return attackRange; } }
    public int SkillRange { get { return skillRange; } }
    public int Strength { get { return strength; } }
    public int Intelligence { get { return intelligence; } }
    public int Defense { get { return defense; } }
    public int Resistance { get { return resistance; } }
    public SkillType SkillType { get { return skillType; } }
    public int SkillMpCost { get { return skillMpCost; } }
    public int SkillDamageRatio { get { return skillDamageRatio; } }
    public float CriticalChance { get { return criticalChance; } }
    public float CriticalDamageRatio { get { return criticalDamageRatio; } }
    public int MaxHp { get { return maxHp; } }
    public int MaxMp { get { return maxMp; } }
}
