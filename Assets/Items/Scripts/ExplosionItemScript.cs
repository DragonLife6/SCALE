using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionItemScript : MonoBehaviour
{
    [SerializeField] float explosionRadius = 5f;
    [SerializeField] float explosionDamage = 20f;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // ������� ������ �� ��'����, �����!
            Explode();
        }
    }

    void Explode()
    {
        // ����������� ��� ������ � ������� �����
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        // ��������� ����������� �������
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                // ��������� ����������� �������
                collider.GetComponent<EnemyHealth>().GetDamage(explosionDamage);
            }
        }

        //��������� ��'���� ���� ������
        Destroy(gameObject);
    }
}
