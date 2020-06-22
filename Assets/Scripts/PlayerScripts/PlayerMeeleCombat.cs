using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeeleCombat : MonoBehaviour
{
    public float timeBetweenAttacks;
    public float startTimeBetweenAttacks = PlayerController.meeleAttackSpeed;

    public Transform attackPos;
    public static float attackRange = 0.8f;

    private bool isDuringMeeleAttack;

    public LayerMask whatIsEnemies;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    private IEnumerator AttackEnemiesInRange()
    {
        if (isDuringMeeleAttack)
        {
            yield break;
        }
        else
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);

            isDuringMeeleAttack = true;

            foreach (var enemy in enemiesToDamage)
            {
                enemy.GetComponent<EnemyAI>().TakeHit(PlayerController.meeleDamage);
            }

            yield return new WaitForSeconds(PlayerController.meeleAttackSpeed);
            isDuringMeeleAttack = false;
        }
    }

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (PlayerController.CanAttack() && Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(AttackEnemiesInRange());
        }
    }
}
