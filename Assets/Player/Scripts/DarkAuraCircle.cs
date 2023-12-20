using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkAuraCircle : MonoBehaviour
{
    float damage;
    float delay;
    Vector3 initialSize = Vector3.zero;


    public void SetParameters(float newDamage, float newDelay, float newSize)
    {
        damage = newDamage;
        delay = newDelay;
        ChangeSize(newSize);
    }

    private void ChangeSize(float newSize)
    {
        if (initialSize == Vector3.zero)
        {
            initialSize = transform.localScale;
        }
        transform.localScale = initialSize * newSize;
        //size = newSize;
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
            enemyHealth.GetDamage(damage);
        }
    }
}
