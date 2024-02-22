using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulRiftExplosionProjectile : SoulExplosionProjectile
{
    public float damageDelay = 0.5f;

    private void Start()
    {
        Destroy(gameObject, 3f);
    }

    public override void Movement()
    {
        return;
    }

    public override void OnTriggerVirtual(Collider2D other)
    {
        if (other.TryGetComponent<EnemyHealth>(out var enemyHealth))
        {
            try { enemyHealth.GetDamage(damage, critChance, critPower); } catch { }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Перевірка інтервалу для повторного нанесення пошкодження
            if (Time.time - other.GetComponent<EnemyHealth>().lastDamageTime > damageDelay)
            {
                OnTriggerVirtual(other);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            try
            {
                other.GetComponent<EnemyHealth>().lastDamageTime = 0f;
            } catch { }
        }
    }
}
