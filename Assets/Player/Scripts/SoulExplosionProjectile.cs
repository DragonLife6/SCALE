using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulExplosionProjectile : MonoBehaviour
{
    protected float damage = 0f;
    protected float moveSpeed = 7f;
    protected float size = 1f;
    protected float critChance = 0f;
    protected float critPower = 1f;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        Movement();
    }

    public virtual void Movement()
    {
        Vector3 moveDirection = transform.rotation * Vector3.up;

        transform.Translate(moveSpeed * Time.deltaTime * moveDirection, Space.World);
    }

    public void SetParameters(float dmg, float spd, float sz, float chance, float power, float newRotation)
    {
        damage = dmg;
        moveSpeed = spd;
        size = sz;
        transform.localScale *= size;

        critChance = chance;
        critPower = power;

        transform.rotation = Quaternion.Euler(0f, 0f, newRotation);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            OnTriggerVirtual(other);
        }
    }

    public virtual void OnTriggerVirtual(Collider2D other)
    {
        if (other.TryGetComponent<EnemyHealth>(out var enemyHealth))
        {
            try { enemyHealth.GetDamage(damage, critChance, critPower); } catch { }

            Destroy(gameObject);
        }
    }
}
