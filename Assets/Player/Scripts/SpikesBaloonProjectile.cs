using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesBaloonProjectile : ExplosiveBaloonProjectile
{
    [SerializeField] GameObject spikePrefab;

    public override void StartExplosion()
    {
        SpawnSpikes(8);
        ExplosionEffects();
    }

    void SpawnSpikes(int count)
    {
        float projectileRotation;
        float randomOffset = Random.Range(0f, 90f);
        for (int i = 0; i < count; i++)
        {
            projectileRotation = (360 / count) * i + randomOffset;

            ProjectileBase projectile = Instantiate(spikePrefab, transform.position, Quaternion.identity).GetComponent<ProjectileBase>();
            projectile.SetParameters(damage/count, critChance, critPower, projectileRotation);
        }
    }
}
