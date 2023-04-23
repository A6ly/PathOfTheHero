using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    Animator animator;

    bool move;
    bool attack;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void StartMoving()
    {
        move = true;
    }

    public void StopMoving()
    {
        move = false;
    }

    public void Attack()
    {
        attack = true;
    }

    private void LateUpdate()
    {
        animator.SetBool("Move", move);
        animator.SetBool("Attack", attack);

        if (attack == true)
        {
            attack = false;
        }
    }
}
