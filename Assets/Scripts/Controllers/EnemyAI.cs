using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public string enemyType;
    
    private Vector2 movementVector;
    public Transform target;

    // Enemy statistics:
    public HealthSystem healthSystem = new HealthSystem(200);
    public float enemyDefaultMovementSpeed;
    private float enemyMovementSpeed;
    public float meeleAttackAnimationDuration;
    public float meeleAttackDistance;
    public float rangedAttackAnimationDuration;

    public GameObject redArrowPrefab;
    public GameObject cobraSpitPrefab;

    //Enemy states:
    public bool isShooting;
    public bool isHit;
    private bool isHitAnimationPlayed;
    public bool isDead;
    public bool isNextToPlayer;

    private void SetTarget()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public Vector2 GetMovementVector()
    {
        movementVector = Vector2.MoveTowards(transform.position, target.position, enemyMovementSpeed * Time.deltaTime);
        return movementVector;
    }

    public float GetPlayerToEnemyDistance()
    {
        float playerToEnemyDistance = Vector2.Distance(transform.position, target.position);
        return playerToEnemyDistance;
    }

    public float GetEnemyFacingXDirection()
    {
        if (!isDead)
        {
            Vector2 direction = target.position - transform.position;
            direction.Normalize();
            return direction.x;
        }
        Vector2 currentDirection = transform.position;
        return currentDirection.x;
    }

    private void MoveTowardsPlayer()
    {
        transform.position = GetMovementVector();
    }

    public IEnumerator StopsWhenHit() {

        Animator animator = gameObject.GetComponent<Animator>();

        if (isHitAnimationPlayed)
        {
            yield break;
        }
        else
        {
            isHitAnimationPlayed = true;
            isHit = true;
            animator.Play("Take Hit");
            enemyMovementSpeed = 0;
            yield return new WaitForSeconds(0.5f);
            isHitAnimationPlayed = false;
            isHit = false;
            enemyMovementSpeed = enemyDefaultMovementSpeed;
        }
    }

    public void TakeHit(int damage)
    {
        StartCoroutine("StopsWhenHit");
        healthSystem.Damage(damage);
        
    }

    public void Die()
    {
        isDead = true;
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<CapsuleCollider2D>());
        enemyMovementSpeed = 0;
        Destroy(gameObject, 2.0f);
    }

    void Start()
    {
        enemyMovementSpeed = enemyDefaultMovementSpeed;
        SetTarget();
    }

    void Update()
    {
        if (!isDead && healthSystem.GetHealth() <= 0)
        {
            Die();
        }

        if (GetPlayerToEnemyDistance() >= meeleAttackDistance && !isDead)
        {
            MoveTowardsPlayer();
        }
    }
}
