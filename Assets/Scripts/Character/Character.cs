using UnityEngine;
using static Define;

public class Character : MonoBehaviour
{
    public float movementPoints = 50f;
    public int attackRange = 1;

    public DamageType damageType;

    public int GetDamage()
    {
        int damage = 0;

        switch (damageType)
        {
            case DamageType.Physical:
                damage += 10;
                break;
            case DamageType.Magic:
                damage += 10;
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
                def += 10;
                break;
            case DamageType.Magic:
                def += 10;
                break;
        }

        return def;
    }
}