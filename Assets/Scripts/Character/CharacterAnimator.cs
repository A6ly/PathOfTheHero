using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    Animator animator;

    bool move;
    bool attack;
    bool isDead;

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

    public void Flinch()
    {
        animator.SetTrigger("Flinch");
    }

    public void Dead()
    {
        isDead = true;
    }

    private void LateUpdate()
    {
        animator.SetBool("Move", move);
        animator.SetBool("Attack", attack);
        animator.SetBool("IsDead", isDead);

        if (attack == true)
        {
            attack = false;
        }
    }
}
