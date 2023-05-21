using UnityEngine;

public class SkillEffect : MonoBehaviour
{
    ParticleSystem particle;
    Poolable poolable;

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
        poolable = GetComponent<Poolable>();
    }

    private void Update()
    {
        if (!particle.isPlaying)
        {
            Managers.Pool.Push(poolable);
        }
    }
}
