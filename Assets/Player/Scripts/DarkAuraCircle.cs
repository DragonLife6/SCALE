using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkAuraCircle : DarkAuraCircleBase
{
    public override void SetParameters(float newDamage, float newDelay, float newSize, float chance, float power)
    {
        damage = newDamage;
        delay = newDelay;
        size = newSize;

        critChance = chance;
        critPower = power;

        ChangeSize(size);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Нанесення пошкодження гравцю при контакті
        if (other.CompareTag("Enemy"))
        {
            DealDamage(other.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Перевірка інтервалу для повторного нанесення пошкодження
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
