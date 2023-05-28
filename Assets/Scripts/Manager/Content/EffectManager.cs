using UnityEngine;
using static Define;

public class EffectManager : MonoBehaviour
{
    static EffectManager instance;
    public static EffectManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<EffectManager>();
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    [SerializeField] GameObject markerPoint;
    [SerializeField] GameObject playerPoint;
    [SerializeField] GameObject enemyPoint;
    [SerializeField] GameObject playerDamageEffect;
    [SerializeField] GameObject enemyDamageEffect;
    [SerializeField] GameObject electroSlash;
    [SerializeField] GameObject stoneSlash;
    [SerializeField] GameObject meteors;
    [SerializeField] GameObject blizzard;

    public void HighlightMarkerPointEffect(Vector2Int positionOnGrid)
    {
        Vector3 position = StageManager.Instance.StageGrid.GetWorldPosition(positionOnGrid.x, positionOnGrid.y, true);
        position += Vector3.up * 0.2f;

        markerPoint.transform.position = position;
        markerPoint.SetActive(true);
    }

    public void HideMarkerPointEffect()
    {
        markerPoint.SetActive(false);
    }

    public void HighlightPointEffect(Vector2Int positionOnGrid, string tag)
    {
        Vector3 position = StageManager.Instance.StageGrid.GetWorldPosition(positionOnGrid.x, positionOnGrid.y, true);
        position += Vector3.up * 0.2f;

        if (tag == "Player")
        {
            playerPoint.transform.position = position;
            playerPoint.SetActive(true);
        }
        else if (tag == "Enemy")
        {
            enemyPoint.transform.position = position;
            enemyPoint.SetActive(true);
        }
    }

    public void HidePointEffect(string tag)
    {
        if (tag == "Player")
        {
            playerPoint.SetActive(false);
        }
        else if (tag == "Enemy")
        {
            enemyPoint.SetActive(false);
        }
    }

    public void PlaySkillEffect(Vector3 pos, Vector3 targetPos, SkillType SkillType)
    {
        Poolable skillEffect = null;

        switch (SkillType)
        {
            case SkillType.None:
                break;
            case SkillType.ElectroSlash:
                skillEffect = Managers.Pool.Pop(electroSlash, gameObject.transform);
                skillEffect.transform.position = new Vector3(pos.x, pos.y + 0.5f, pos.z);
                skillEffect.transform.LookAt(targetPos);
                break;
            case SkillType.StoneSlash:
                skillEffect = Managers.Pool.Pop(stoneSlash, gameObject.transform);
                skillEffect.transform.position = new Vector3(pos.x, pos.y + 0.5f, pos.z);
                skillEffect.transform.LookAt(targetPos);
                break;
            case SkillType.Meteors:
                skillEffect = Managers.Pool.Pop(meteors, gameObject.transform);
                skillEffect.transform.position = new Vector3(targetPos.x, targetPos.y + 0.5f, targetPos.z);
                break;
            case SkillType.Blizzard:
                skillEffect = Managers.Pool.Pop(blizzard, gameObject.transform);
                skillEffect.transform.position = new Vector3(targetPos.x, targetPos.y + 0.5f, targetPos.z);
                break;
        }

        skillEffect.GetComponent<ParticleSystem>().Play();
    }

    public void PlayDamageEffect(Vector3 targetPos, string damage, string tag, bool isCritical = false)
    {
        if (tag == "Player")
        {
            Poolable damageEffect = Managers.Pool.Pop(playerDamageEffect, gameObject.transform);
            StartCoroutine(damageEffect.GetComponent<DamageEffect>().PlayDamageEffect(targetPos, damage, isCritical));
        }
        else if (tag == "Enemy")
        {
            Poolable damageEffect = Managers.Pool.Pop(enemyDamageEffect, gameObject.transform);
            StartCoroutine(damageEffect.GetComponent<DamageEffect>().PlayDamageEffect(targetPos, damage, isCritical));
        }
    }
}