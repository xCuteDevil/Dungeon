using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyAI enemyAI = other.gameObject.GetComponent<EnemyAI>();
            enemyAI.TakeHit(PlayerController.rangedDamage);
            Destroy(gameObject);
        }
    }
}
