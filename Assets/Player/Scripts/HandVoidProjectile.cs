using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandVoidProjectile : HandProjectileBase
{
    public float damageDelay = 0.5f;

    private void Start()
    {
        Destroy(gameObject, 3f);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        // Нанесення пошкодження гравцю при контакті
        if (other.CompareTag("Enemy"))
        {
            AddForceToEnemy(other.gameObject, 0.5f);
            DealDamage(other.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (Time.time - other.GetComponent<EnemyHealth>().lastDamageTime > damageDelay)
            {
                DealDamage(other.gameObject);
            }
        }
    }

    void AddForceToEnemy(GameObject enemy, float force)
    {
        Vector3 direction = transform.position - enemy.transform.position;
        direction.Normalize();

        enemy.GetComponent<EnemyMovement>().moveSpeedReduced = force;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            AddForceToEnemy(other.gameObject, 1f);
            other.GetComponent<EnemyHealth>().lastDamageTime = 0f;
        }
    }

    private void DealDamage(GameObject enemy)
    {
        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            try { enemyHealth.GetDamage(damage / 4f, critChance, critPower); } catch { }
        }
    }
}
