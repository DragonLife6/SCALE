using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkAuraVariantCircle : DarkAuraCircleBase
{
    [SerializeField] float maxSize;
    [SerializeField] float speed;

    float coef = 0f;

    private void Update()
    {
        if(coef < maxSize)
        {
            coef += speed * Time.deltaTime;
            ChangeSize(coef);
        } else
        {
            Destroy(gameObject);
        }
    }

    public override void SetParameters(float newDamage, float newDelay, float newSize, float chance, float power)
    {
        damage = newDamage;
        delay = newDelay;
        size = newSize;

        critChance = chance;
        critPower = power;

        initialSize = new Vector3(0.7f, 0.7f, 1f);
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
