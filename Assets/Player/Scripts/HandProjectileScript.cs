using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandProjectileScript : MonoBehaviour
{
    float damage = 0f;
    float size = 0f;

    private void Start()
    {
        Destroy(gameObject, 1.06f);
    }


    public void SetParameters(float dmg, float spd)
    {
        damage = dmg;
        size = spd;

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
                enemyHealth.GetDamage(damage);
            }
        }
    }
}
