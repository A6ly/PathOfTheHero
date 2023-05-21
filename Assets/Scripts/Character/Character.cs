using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using static Define;

public class Stat
{
    public string Name { get; private set; }
    public DamageType DamageType { get; private set; }
    public float MovementPoint { get; private set; }
    public int AttackRange { get; private set; }
    public int Strength { get; private set; }
    public int Intelligence { get; private set; }
    public int Defense { get; private set; }
    public int Resistance { get; private set; }
    public SkillType SkillType { get; private set; }
    public int SkillMpCost { get; private set; }
    public int SkillDamageRatio { get; private set; }
    public float CriticalChance { get; private set; }
    public float CriticalDamageRatio { get; private set; }
    public int MaxHp { get; private set; }
    public int MaxMp { get; private set; }

    int hp;
    int mp;

    public int Hp
    {
        get { return hp; }
        set { hp = Math.Clamp(value, 0, MaxHp); }
    }

    public int Mp
    {
        get { return mp; }
        set { mp = Math.Clamp(value, 0, MaxMp); }
    }

    public Stat(string name, DamageType damageType, float movementPoint, int attackRange, int strength, int intelligence, int defense, int resistance,
                SkillType skillType, int skillMpCost, int skillDamageRatio, float criticalChance, float criticalDamageRatio, int maxHp, int maxMp)
    {
        Name = name;
        DamageType = damageType;
        MovementPoint = movementPoint;
        AttackRange = attackRange;
        Strength = strength;    // Strength = strength + Data.Stat.strength;
        Intelligence = intelligence;
        Defense = defense;
        Resistance = resistance;
        SkillType = skillType;
        SkillMpCost = skillMpCost;
        SkillDamageRatio = skillDamageRatio;
        CriticalChance = criticalChance;
        CriticalDamageRatio = criticalDamageRatio;
        MaxHp = maxHp;
        MaxMp = maxMp;
        hp = MaxHp;
        mp = MaxMp;
    }
}

public class Character : MonoBehaviour
{
    public GridObject gridObject;
    public CharacterData characterData;
    public Stat stat;
    public StepSoundType stepSoundType;
    public AttackSoundType attackSoundType;
    public SkillSoundType skillSoundType;
    public bool isDead = false;

    CharacterAnimator characterAnimator;
    int bonusStat = 0;

    private void Awake()
    {
        if (gameObject.CompareTag("Player"))
        {
            bonusStat = Managers.Data.CalculateBonusStat();
        }

        stat = new Stat(characterData.CharacterName, characterData.ClassType, characterData.MovementPoint, characterData.AttackRange, characterData.Strength + bonusStat,
                        characterData.Intelligence + bonusStat, characterData.Defense + bonusStat, characterData.Resistance + bonusStat, characterData.SkillType, characterData.SkillMpCost,
                        characterData.SkillDamageRatio, characterData.CriticalChance, characterData.CriticalDamageRatio, characterData.MaxHp, characterData.MaxMp);
        gridObject = GetComponent<GridObject>();
        characterAnimator = GetComponent<CharacterAnimator>();
    }

    public int GetDamage()
    {
        int damage = 0;

        switch (stat.DamageType)
        {
            case DamageType.Physical:
                damage += stat.Strength;
                break;
            case DamageType.Magical:
                damage += stat.Intelligence;
                break;
        }

        return damage;
    }

    public int GetDefense(DamageType type)
    {
        int def = 0;

        switch (type)
        {
            case DamageType.Physical:
                def += stat.Defense;
                break;
            case DamageType.Magical:
                def += stat.Resistance;
                break;
        }

        return def;
    }

    private void Flinch()
    {
        characterAnimator.Flinch();
    }

    private IEnumerator Dead()
    {
        isDead = true;
        characterAnimator.Dead();
        Managers.Sound.Play("DeadEffect", SoundType.Effect);
        StageManager.Instance.StageGrid.DeleteGridObject(gridObject.positionOnGrid.x, gridObject.positionOnGrid.y);

        yield return new WaitForSeconds(3.0f);
        yield return transform.DOMove(new Vector3(transform.position.x, transform.position.y - 5.0f, transform.position.z), 5.0f).SetEase(Ease.Linear).WaitForCompletion();

        gameObject.SetActive(false);
    }

    private void CheckDead()
    {
        if (stat.Hp == 0)
        {
            StartCoroutine(Dead());
        }
        else
        {
            Flinch();
        }
    }

    public bool CheckSkillAvailability()
    {
        if (stat.Mp >= stat.SkillMpCost)
        {
            return true;
        }

        return false;
    }

    public void TakeDamage(int damage)
    {
        stat.Hp -= damage;
        CheckDead();
    }

    public void UseMp()
    {
        stat.Mp -= stat.SkillMpCost;
    }
}