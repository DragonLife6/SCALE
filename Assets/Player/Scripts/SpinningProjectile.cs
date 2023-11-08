using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SpinningProjectile : MonoBehaviour
{
    float damage = 0f;
    float radius = 1f;

    public void SetParameters(float dmg, float rds)
    {
        damage = dmg;
        radius = rds;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Нанесення пошкодження гравцю при контакті
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.GetDamage(damage);
            }
        }
    }
}
