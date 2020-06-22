using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrowFireing : MonoBehaviour
{
    public GameObject arrowPrefab;

    public float projectileSpeed;
    Vector2 shootingDirection;

    public float reloadCooldown = PlayerController.rangedAttackSpeed;
    private bool isReloading;

    void Shoot()
    {
        shootingDirection = new Vector2(PlayerAiming.mousePos.x, PlayerAiming.mousePos.y).normalized;
                    
            GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);

            arrow.GetComponent<Rigidbody2D>().velocity = shootingDirection * projectileSpeed;
            arrow.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);

            Destroy(arrow, 2.0f);                     
    }

    IEnumerator ShootAndReload()
    {
        if (isReloading)
        {
            yield break;
        }
        else
        {
            isReloading = true;
            Shoot();
            yield return new WaitForSeconds(reloadCooldown);
            isReloading = false;
        }
    }

    void Update()
    {
        if (PlayerController.CanAttack() && Input.GetKeyDown(KeyCode.Mouse1))
        {
            StartCoroutine(ShootAndReload());
        }
    }
}
