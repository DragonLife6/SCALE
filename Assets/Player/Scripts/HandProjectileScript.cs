using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandProjectileScript : HandProjectileBase
{
    
    private void Start()
    {
        Destroy(gameObject, 1.06f);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        // Нанесення пошкодження гравцю при контакті
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                try { enemyHealth.GetDamage(damage, critChance, critPower); } catch {}
            }
        }
    }
}
