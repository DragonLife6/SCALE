using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionItemScript : MonoBehaviour
{
    [SerializeField] float explosionRadius = 5f;
    [SerializeField] float explosionDamage = 20f;

    FadingImageScript whiteFading;
    Animator animator;

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
        animator = GetComponent<Animator>();
        whiteFading = GameObject.Find("FadingImage").GetComponent<FadingImageScript>();

        // Знаходження усіх ворогів у певному радіусі
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        animator.Play("explosionAnim");
        whiteFading.FadeWhite(0.3f);
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
        Destroy(gameObject, 0.5f);
    }
}
