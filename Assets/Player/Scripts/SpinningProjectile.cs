using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SpinningProjectile : MonoBehaviour
{
    protected float damage = 0f;
    protected float size = 1f;

    protected float critChance = 0f;
    protected float critPower = 1f;

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
        OnTrigger(other);
    }

    protected virtual void OnTrigger(Collider2D other)
    {
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
