using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : SpinningProjectile
{
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] GameObject circlePrefab;
    GameObject explosionParticle;
    GameObject circleParticle;
    float explosionRadius = 1f;

    Collider2D projectileCollider;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        projectileCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public IEnumerator RestoreRoutine(float delay)
    {
        yield return new WaitForSeconds(delay * 0.8f);

        Color notTransperentColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
        spriteRenderer.color = notTransperentColor;

        yield return new WaitForSeconds(delay * 0.2f);

        projectileCollider.enabled = true;
    }

    protected override void OnTrigger(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Explode();
        }
    }

    void Explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius * size);
        

        // Нанесення пошкодження ворогам
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                try
                {
                    collider.GetComponent<EnemyHealth>().GetDamage(damage, critChance, critPower);
                } catch { }
            }
        }

        Color transperentColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
        spriteRenderer.color = transperentColor;
        projectileCollider.enabled = false;

        explosionParticle = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        circleParticle = Instantiate(circlePrefab, transform.position, Quaternion.identity);

        explosionParticle.transform.localScale *= (size / 2);
        circleParticle.transform.localScale *= size;

        explosionParticle.GetComponent<ParticleSystem>().Play();
        circleParticle.GetComponent<ParticleSystem>().Play();


        StartCoroutine(RestoreRoutine(4f));
    }
}
