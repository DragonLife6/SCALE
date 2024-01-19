using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonSpellScriptable : AbilityBaseScript
{
    [SerializeField] GameObject projectile;
    [SerializeField] float[] damageOnLevel;
    [SerializeField] float[] sizeOnLevel;
    [SerializeField] float[] spawnDelayOnLevel;
    [SerializeField] float[] speedOnLevel;

    float newDamage = 0f;
    float newSize = 0f;
    float newSpawnDelay = 0f;
    float newSpeed = 0f;

    public override void UpdateAbility(int lvl)
    {
        newDamage = damageOnLevel[lvl - 1] * damageMultiplier;
        newSize = sizeOnLevel[lvl - 1] * sizeMultiplier;
        newSpawnDelay = spawnDelayOnLevel[lvl - 1] * delayMultiplier;
        newSpeed = speedOnLevel[lvl - 1];
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            float projectileRotation = Random.Range(0f, 360f);
            DragonSpellProjectile fireball_instance = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<DragonSpellProjectile>();
            fireball_instance.SetParameters(newDamage, newSpeed, newSize, critChanceMultiplier, critDamageMultiplier, projectileRotation);

            yield return new WaitForSeconds(newSpawnDelay);
        }
    }

    public override void Activate()
    {
        currentLevel = 1;
        UpdateAbility(currentLevel);

        StartCoroutine(Shoot());
    }
}
