using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionItemScript : MonoBehaviour
{
    [SerializeField] float explosionRadius = 5f;
    [SerializeField] float explosionDamage = 20f;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Гравець підійшов до об'єкта, вибух!
            Explode();
        }
    }

    void Explode()
    {
        // Знаходження усіх ворогів у певному радіусі
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        // Нанесення пошкодження ворогам
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                // нанесення пошкодження ворогам
                collider.GetComponent<EnemyHealth>().GetDamage(explosionDamage);
            }
        }

        //видалення об'єкта після вибуху
        Destroy(gameObject);
    }
}
