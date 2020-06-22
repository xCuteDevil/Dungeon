using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeeleCombat : MonoBehaviour
{
    public EnemyAI enemyAI;

    public Transform attackPos;

    public int meeleDamage;
    private bool isAttacking;

    // Player's layer
    public LayerMask playerLayer;

    IEnumerator AttackMeeleForGivenTime()
    {
        if (isAttacking)
        {
            enemyAI.isNextToPlayer = true;
            yield break;
        }
        else
        {
            enemyAI.isNextToPlayer = true;
            isAttacking = true;
           
            Collider2D[] playerToDamage = Physics2D.OverlapCircleAll(attackPos.position, enemyAI.meeleAttackDistance, playerLayer);
            foreach (var player in playerToDamage)
            {
                Debug.Log("Attacked");
                player.gameObject.GetComponent<PlayerController>().TakeHit(meeleDamage); //.GetComponent<PlayerController>().TakeHit(meeleDamage);
            }

            yield return new WaitForSeconds(enemyAI.meeleAttackAnimationDuration);

            isAttacking = false;
            enemyAI.isNextToPlayer = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, enemyAI.meeleAttackDistance);
    }

    void Start()
    {
        
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        if (enemyAI.GetPlayerToEnemyDistance() <= enemyAI.meeleAttackDistance && !enemyAI.isDead)
        { 
            StartCoroutine(AttackMeeleForGivenTime());
        }
    }
}
