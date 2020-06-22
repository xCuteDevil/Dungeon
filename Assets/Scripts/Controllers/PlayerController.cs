using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    Vector3 movementVector;

    public HealthSystem healthSystem = new HealthSystem(100);

    //Player stats:
    public float defaultMovementSpeed;
    private float movementSpeed;

    public static int rangedDamage = 20;
    public static float rangedAttackSpeed = 1.0f;

    public static int meeleDamage = 20;
    public static float meeleAttackSpeed = 1.0f;

    public float hitStunTime = 0.5f;

    //Player state:
    public static bool isHit;
    public static bool isDead;

    private void Animate()
    {
        animator.SetFloat("Horizontal", movementVector.x);
        animator.SetFloat("Vertical", movementVector.y);
        animator.SetFloat("Magnitude", movementVector.magnitude);
    }

    public static bool CanAttack()
    {
        if (isHit == false && isDead == false)
        {
            return true;
        } else
        {
            return false;
        }
    }

    private Vector3 SetMovementInputVector() 
    {
        movementVector = new Vector3(Input.GetAxisRaw("Horizontal"),  Input.GetAxisRaw("Vertical"), 0.0f);
        return movementVector;
    }

    private void Move(Vector3 movementVector)
    {
        transform.position = transform.position + movementVector * Time.deltaTime * movementSpeed;
    }

    public void TakeHit(int damage)
    {
        StartCoroutine(StunnedByHit());
        healthSystem.Damage(damage);
    }

    public IEnumerator StunnedByHit()
    {
        if (isHit)
        {
            yield break;
        } else
        {
            isHit = true;
            movementSpeed = 0;
            yield return new WaitForSeconds(hitStunTime);
            isHit = false;
            movementSpeed = defaultMovementSpeed;
        }
    }

    void Start()
    {
        movementSpeed = defaultMovementSpeed;
        Cursor.visible = false;
    }

    void Update()
    {
        SetMovementInputVector();
        Debug.Log("Player health: " + healthSystem.GetHealth());
    }

    void FixedUpdate()
    {
        Animate();
        Move(SetMovementInputVector());                
    }
}
