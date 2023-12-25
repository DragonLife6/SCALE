using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulExplosionProjectile : MonoBehaviour
{
    float damage = 0f;
    [SerializeField] float moveSpeed = 7f;

    private void Start()
    {
        Destroy(gameObject, 15f);
    }

    private void Update()
    {
        Vector3 moveDirection = transform.rotation * Vector3.up;

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }

    public void SetParameters(float dmg, float newRotation)
    {
        damage = dmg;
        transform.rotation = Quaternion.Euler(0f, 0f, newRotation);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Нанесення пошкодження гравцю при контакті
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.GetDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
