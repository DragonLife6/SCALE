using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class DirectedFireProjectile : DirectedProjectileBase
{
    Animator animator;
    public float explosionRadius = 2f;
    public int damageTicksCount = 5;

    List<Collider2D> colliders;
    float angle;

    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    public override void SetParameters(float dmg, float spd, float size, float chance, float power, Transform trgt)
    {
        damage = dmg;
        speed = spd;
        target = trgt;
        transform.localScale *= size;
        newSize = size;
        critChance = chance;
        critPower = power;

        moveDirection = (target.position - transform.position);
        angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        moveDirection = moveDirection.normalized;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        animator = GetComponent<Animator>();
        Destroy(gameObject, 10f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Нанесення пошкодження гравцю при контакті
        if (other.CompareTag("Enemy"))
        {
            speed = 0f;
            Explode();
        }
    }

    public IEnumerator FireDamageRoutine(float delay)
    {
        for (int i = 0; i < damageTicksCount; i++)
        {
            yield return new WaitForSeconds(delay / damageTicksCount);
            FireDamage();
        }

        Destroy(gameObject);
    }


    void FireDamage()
    {
        if (colliders.Count > 0)
        {
            foreach (Collider2D collider in colliders)
            {
                try
                {
                    if (collider.CompareTag("Enemy"))
                    {
                        collider.GetComponent<EnemyHealth>().GetDamage(damage / damageTicksCount, critChance, critPower);
                    }
                }
                catch
                {
                }

            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Explode()
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius * newSize).ToList();
        animator.Play("FireAnimation");

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                try
                {
                    collider.GetComponent<EnemyHealth>().GetDamage(damage, critChance, critPower);
                } catch
                {

                }
            }
        }
        

        StartCoroutine(FireDamageRoutine(4f));
    }
}
