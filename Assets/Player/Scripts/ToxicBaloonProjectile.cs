using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicBaloonProjectile : ExplosiveBaloonProjectile
{
    [SerializeField] GameObject damageFieldPrefab;
    [SerializeField] float destroyTime = 3f;

    public override void StartExplosion()
    {
        SpawnDamageField();
        ExplosionEffects();
    }

    void SpawnDamageField()
    {
        DamageFieldBase projectile = Instantiate(damageFieldPrefab, transform.position, Quaternion.identity).GetComponent<DamageFieldBase>();
        projectile.SetParameters(damage / destroyTime, size, critChance, critPower);

        Destroy(projectile.gameObject, destroyTime);
    }
}
