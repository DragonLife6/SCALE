using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBaloonProjectile : MonoBehaviour
{
    Animator animator;

    [SerializeField] ParticleSystem explosionParticle;
    [SerializeField] ParticleSystem explosionCircle;
    [SerializeField] Transform explosionPosition;

    AudioSource audioSource;
    [SerializeField] AudioClip firstSound;
    [SerializeField] AudioClip secondSound;

    protected float damage = 0f;
    protected float size = 1f;
    float explosionDelay = 1f;
    float explosionRadius = 1f;

    protected float critChance = 0f;
    protected float critPower = 1f;

    public void SetParameters(float dmg, float sz, float radius, float delay, float chance, float power)
    {
        damage = dmg;
        size = sz;
        explosionDelay = delay;
        explosionRadius = radius;

        transform.localScale *= size;

        critChance = chance;
        critPower = power;

        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        audioSource.PlayOneShot(firstSound);

        Invoke(nameof(StartAnimation), explosionDelay);
    }

    void StartAnimation()
    {
        animator.Play("Explode");
        Invoke(nameof(StartExplosion), 0.07f);
    }

    public virtual void StartExplosion()
    {
        ExplosionEffects();
    }

    protected void ExplosionEffects()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPosition.position, explosionRadius * size);

        audioSource.PlayOneShot(secondSound);

        // Нанести пошкодження ворогам
        foreach (Collider2D collider in colliders)
        {
            EnemyHealth enemy = collider.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                try
                {
                    enemy.GetDamage(damage, critChance, critPower);
                } catch { }
            }
        }

        explosionParticle.transform.localScale *= (explosionRadius * size / 2);
        explosionParticle.Play();

        explosionCircle.transform.localScale *= explosionRadius * size;
        explosionCircle.Play();

        Destroy(gameObject, 0.5f);
    }
}
