using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandProjectileScript : MonoBehaviour
{
    float damage = 0f;
    float size = 0f;

    float critChance = 0f;
    float critPower = 1f;

    private void Start()
    {
        Destroy(gameObject, 1.06f);
    }


    public void SetParameters(float dmg, float sz, float chance, float power)
    {
        damage = dmg;
        size = sz;

        critChance = chance;
        critPower = power;

        transform.localScale *= size;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Нанесення пошкодження гравцю при контакті
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.GetDamage(damage, critChance, critPower);
            }
        }
    }
}
