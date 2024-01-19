using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBaloonProjectile : MonoBehaviour
{
    Animator animator;

    [SerializeField] ParticleSystem explosionParticle;
    [SerializeField] ParticleSystem explosionCircle;
    [SerializeField] Transform explosionPosition;

    float damage = 0f;
    float size = 1f;
    float explosionDelay = 1f;
    float explosionRadius = 1f;

    float critChance = 0f;
    float critPower = 1f;

    public void SetParameters(float dmg, float sz, float radius, float delay, float chance, float power)
    {
        damage = dmg;
        size = sz;
        explosionDelay = delay;
        explosionRadius = radius;

        transform.localScale *= size;

        critChance = chance;
        critPower = power;

        animator = GetComponent<Animator>();

        Invoke(nameof(StartAnimation), explosionDelay);
    }

    void StartAnimation()
    {
        animator.Play("Explode");
        Invoke(nameof(StartExplosion), 0.07f);
    }

    void StartExplosion()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPosition.position, explosionRadius * size);
        

        // Нанести пошкодження ворогам
        foreach (Collider2D collider in colliders)
        {
            EnemyHealth enemy = collider.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.GetDamage(damage, critChance, critPower);
            }
        }

        explosionParticle.transform.localScale *= (explosionRadius * size / 2);
        explosionParticle.Play();

        explosionCircle.transform.localScale *= explosionRadius * size;
        explosionCircle.Play();

        Destroy(gameObject, 0.5f);
    }
}
