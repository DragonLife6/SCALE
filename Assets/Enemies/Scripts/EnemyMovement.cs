using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public float damage = 10f;
    public bool deathOnCollision = false;
    EnemyManager enemyManager;

    private Transform player;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        enemyManager = GameObject.Find("Enemy_manager").GetComponent<EnemyManager>();
    }

    void Update()
    {
        Vector3 moveDirection = (player.position - transform.position).normalized;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        if(moveDirection.x < 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-0.1f, 0.1f, 1f);
        } else if(moveDirection.x > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(0.1f, 0.1f, 1f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Нанесення пошкодження гравцю при контакті
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.GetDamage(damage);
            }

            if (deathOnCollision)
            {
                enemyManager.DeleteEnemy(transform);
                Destroy(gameObject);
            }
        }
    }
}
