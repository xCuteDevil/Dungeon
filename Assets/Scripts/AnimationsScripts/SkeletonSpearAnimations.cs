using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSpearAnimations : MonoBehaviour
{
    public Animator animator;
    public EnemyAI meeleEnemyAI;

    private void Animate()
    {
        animator.SetFloat("Horizontal", meeleEnemyAI.GetEnemyFacingXDirection());
        animator.SetFloat("Magnitude", meeleEnemyAI.GetMovementVector().magnitude);
        animator.SetBool("IsHit", meeleEnemyAI.isHit);
        animator.SetBool("IsDead", meeleEnemyAI.isDead);
        animator.SetBool("IsNextToPlayer", meeleEnemyAI.isNextToPlayer);
    }

    void Update()
    {
        Animate();
    }
}
