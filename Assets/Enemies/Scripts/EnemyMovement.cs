using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public float damage = 10f;
    public bool deathOnCollision = false;
    public float attackDistance = 1f;
    EnemyManager enemyManager;
    Vector3 initialScale;

    private Transform player;

    void Start()
    {
        initialScale = transform.localScale;
        player = GameObject.Find("Player").transform;
        enemyManager = GameObject.Find("Enemy_manager").GetComponent<EnemyManager>();
    }

    void Update()
    {
        Vector3 moveDirection = (player.position - transform.position); ;
        if (deathOnCollision)
        {
            transform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime);
        } else
        {
            float distanceToPlayer = moveDirection.magnitude;

            if (distanceToPlayer > attackDistance)
            {
                transform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime);
            }
            else
            {
                //animator.SetTrigger("Attack");
            }
        }
        if(moveDirection.x < 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(initialScale.x * -1, initialScale.y, initialScale.z);
        } else if(moveDirection.x > 0 && transform.localScale.x < 0)
        {
            transform.localScale = initialScale;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // ��������� ����������� ������ ��� �������
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.GetDamage(damage);
            }

            if (deathOnCollision)
            {
                Destroy(gameObject);
            }
        }
    }


    private void OnDestroy()
    {
        enemyManager.DeleteEnemy(transform);
    }
}
