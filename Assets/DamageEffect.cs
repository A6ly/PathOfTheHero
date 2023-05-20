using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class DamageEffect : MonoBehaviour
{
    TextMeshPro textMeshPro;
    Poolable poolable;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshPro>();
        poolable = GetComponent<Poolable>();
    }

    public IEnumerator PlayDamageEffect(Vector3 targetPos, string damage)
    {
        transform.position = new Vector3(targetPos.x, targetPos.y + 2.0f, targetPos.z);
        textMeshPro.text = damage;
        textMeshPro.DOFade(1.0f, 0.15f);

        yield return new WaitForSeconds(3.0f);
        yield return textMeshPro.DOFade(0.0f, 1.0f).WaitForCompletion();

        Managers.Pool.Push(poolable);
    }
}