using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkProtectionAuraCircle : DarkAuraCircleBase
{
    PlayerHealth playerHealth;
    public int maxProtections = 2;
    public int protectionForTick = 1;

    public override void SetParameters(float newDamage, float newDelay, float newSize, float chance, float power)
    {
        damage = newDamage;
        delay = newDelay;
        size = newSize;

        critChance = chance;
        critPower = power;

        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        ChangeSize(size);

        StartCoroutine(ProtectionRestoreRoutine());
    }
    
    IEnumerator ProtectionRestoreRoutine()
    {
        while (true)
        {
            playerHealth.SetProtection(protectionForTick, maxProtections);

            yield return new WaitForSeconds(delay * 10);
        }
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
