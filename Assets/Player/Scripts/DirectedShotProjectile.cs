using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DirectedShotProjectile : MonoBehaviour
{
    float damage = 0f;
    float speed = 0f;

    float critChance = 0f;
    float critPower = 1f;

    Transform target;
    Vector3 moveDirection;

    private void Start()
    {
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.Translate(moveDirection * speed * Time.deltaTime);

            if (moveDirection.x < 0 && transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 1f);
            }
            else if (moveDirection.x > 0 && transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 1f);
            }
        }
    }

    public void SetParameters(float dmg, float spd, float size, float chance, float power, Transform trgt)
    {
        damage = dmg; 
        speed = spd;
        target = trgt;
        transform.localScale *= size;
        critChance = chance;
        critPower = power;

        moveDirection = (target.position - transform.position).normalized;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // ��������� ����������� ������ ��� �������
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.GetDamage(damage, critChance, critPower);
            }

            Destroy(gameObject);
        }
    }
}
