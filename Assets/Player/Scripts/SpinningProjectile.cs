using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SpinningProjectile : MonoBehaviour
{
    float damage = 0f;
    float size = 1f;

    float critChance = 0f;
    float critPower = 1f;

    public void SetParameters(float dmg, float sz, float chance, float power)
    {
        damage = dmg;
        size = sz;

        transform.localScale *= size;

        critChance = chance;
        critPower = power;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Нанесення пошкодження гравцю при контакті
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.GetDamage(damage, critChance, critPower);
            }
        }
    }
}
