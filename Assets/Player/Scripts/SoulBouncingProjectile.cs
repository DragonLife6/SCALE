using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoulBouncingProjectile : SoulExplosionProjectile
{
    [SerializeField] GameObject partsPrefab;
    public bool isOriginal = true;
    public Collider2D damageCollider;

    public override void OnTriggerVirtual(Collider2D other)
    {
        if (other.TryGetComponent<EnemyHealth>(out var enemyHealth))
        {
            try { enemyHealth.GetDamage(damage, critChance, critPower); } catch { }

            if(isOriginal)
            {
                GameObject projectile = Instantiate(partsPrefab, transform.position, Quaternion.identity);
                projectile.GetComponent<SoulBouncingProjectile>().SetCollision();
                projectile.GetComponent<SoulExplosionProjectile>().SetParameters(damage / 2, moveSpeed, size / 1.5f, critChance, critPower, transform.rotation.eulerAngles.z + 90);

                projectile = Instantiate(partsPrefab, transform.position, Quaternion.identity);
                projectile.GetComponent<SoulBouncingProjectile>().SetCollision();
                projectile.GetComponent<SoulExplosionProjectile>().SetParameters(damage / 2, moveSpeed, size / 1.5f, critChance, critPower, transform.rotation.eulerAngles.z - 90);
            }

            Destroy(gameObject);
        }
    }

    public void SetCollision()
    {
        isOriginal = false;
        damageCollider.enabled = false;

        Invoke(nameof(ResetCollision), 0.1f);
    }

    private void ResetCollision()
    {
        damageCollider.enabled = true;
    }

    public override void Movement()
    {
        Vector3 moveDirection = transform.rotation * Vector3.up;

        transform.Translate(moveSpeed * Time.deltaTime * moveDirection, Space.World);
    }
}
