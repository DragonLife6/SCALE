using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFieldBase : MonoBehaviour
{
    float damage;
    public float delay = 0.5f;
    float size;
    Vector3 initialSize = Vector3.zero;

    float critChance = 0f;
    float critPower = 1f;

    public void SetParameters(float newDamage, float newSize, float chance, float power)
    {
        damage = newDamage;
        size = newSize;

        critChance = chance;
        critPower = power;

        ChangeSize(size);
    }

    private void ChangeSize(float newSize)
    {
        if (initialSize == Vector3.zero)
        {
            initialSize = transform.localScale;
        }
        transform.localScale = initialSize * newSize;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            DealDamage(other.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (Time.time - other.GetComponent<EnemyHealth>().lastDamageTime > delay)
            {
                DealDamage(other.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealth>().lastDamageTime = 0f;
        }
    }

    private void DealDamage(GameObject enemy)
    {
        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            try { enemyHealth.GetDamage(damage, critChance, critPower); } catch {}
        }
    }
}
