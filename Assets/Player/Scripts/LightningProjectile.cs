using UnityEngine;

public class LightningProjectile : DirectedProjectileBase
{
    Animator animator;

    public override void SetParameters(float dmg, float spd, float size, float chance, float power, Transform trgt)
    {
        damage = dmg;
        speed = spd;
        target = trgt;
        transform.localScale *= size;
        critChance = chance;
        critPower = power;

        animator = GetComponent<Animator>();
        transform.position = trgt.position;
        animator.Play("LightningAnimation");
        Destroy(gameObject, 0.5f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Нанесення пошкодження гравцю при контакті
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                try { enemyHealth.GetDamage(damage, critChance, critPower); } catch {}
            }
        }
    }
}
