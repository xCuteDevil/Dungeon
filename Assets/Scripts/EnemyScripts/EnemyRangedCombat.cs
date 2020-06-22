using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedCombat : MonoBehaviour
{
    public EnemyAI enemyAI;

    public float projectileSpeed;
    public Vector2 shootingDirection;

    IEnumerator AttackRangedForGivenTime()
    {
        if (enemyAI.isShooting)
        {
            yield break;
        }
        else
        {
            if (enemyAI.GetPlayerToEnemyDistance() > enemyAI.meeleAttackDistance && !enemyAI.isHit)
            {
                enemyAI.isShooting = true;

                shootingDirection = new Vector2(enemyAI.target.position.x, enemyAI.target.position.y);
                shootingDirection.Normalize();

                if (enemyAI.enemyType.Contains("Bow"))
                {
                    GameObject enemyProjectile = Instantiate(enemyAI.redArrowPrefab, transform.position, Quaternion.identity);

                    enemyProjectile.GetComponent<Rigidbody2D>().velocity = shootingDirection * projectileSpeed;
                    enemyProjectile.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);

                    Destroy(enemyProjectile, 2.0f);
                }
                else if (enemyAI.enemyType.Contains("Cobra"))
                {
                    GameObject enemyProjectile = Instantiate(enemyAI.cobraSpitPrefab, transform.position, Quaternion.identity);

                    enemyProjectile.GetComponent<Rigidbody2D>().velocity = shootingDirection * projectileSpeed;
                    enemyProjectile.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);

                    Destroy(enemyProjectile, 2.0f);
                }
                yield return new WaitForSeconds(enemyAI.rangedAttackAnimationDuration);

                enemyAI.isShooting = false;
            }
            else
            {
                yield break;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
