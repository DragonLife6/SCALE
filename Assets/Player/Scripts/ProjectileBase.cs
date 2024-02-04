using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    float damage = 0f;
    public float moveSpeed = 7f;

    float critChance = 0f;
    float critPower = 1f;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        Vector3 moveDirection = transform.rotation * Vector3.up;

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }

    public void SetParameters(float dmg, float chance, float power, float newRotation)
    {
        damage = dmg;

        critChance = chance;
        critPower = power;

        transform.rotation = Quaternion.Euler(0f, 0f, newRotation);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.GetDamage(damage, critChance, critPower);
                Destroy(gameObject);
            }
        }
    }
}
