using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBaloonScriptable : AbilityBaseScript
{
    [SerializeField] GameObject basicProjectile;
    [SerializeField] GameObject secondProjectile;
    [SerializeField] GameObject thirdProjectile;
    [SerializeField] float[] damageOnLevel;
    [SerializeField] float[] explosionDelayOnLevel;
    [SerializeField] float[] spawnDelayOnLevel;
    [SerializeField] float[] radiusOnLevel;

    GameObject projectile;

    float newDamage = 0f;
    float newSpawnDelay = 0f;
    float newExplodeDelay = 0f;
    float newRadius = 0f;

    public override void OnMaxLevel(int variant)
    {
        if(variant == 0)
        {
            newRadius = radiusOnLevel[currentLevel - 1] * 1.5f;
        } else if(variant == 1)
        {
            projectile = secondProjectile;
        } else
        {
            projectile = thirdProjectile;
        }
    }

    public override void UpdateAbility(int lvl)
    {
        newDamage = damageOnLevel[lvl - 1] * damageMultiplier;
        newSpawnDelay = spawnDelayOnLevel[lvl - 1] * delayMultiplier;
        newExplodeDelay = explosionDelayOnLevel[lvl - 1];
        newRadius = radiusOnLevel[lvl - 1];
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            float randomXOffset = Random.Range(0.1f, 0.3f);
            int randomXDirection = Random.Range(0, 2);
            
            if(randomXDirection == 0)
            {
                randomXDirection = -1;
            }
            Vector3 newPosition = new Vector3(transform.position.x + randomXOffset * randomXDirection, transform.position.y + 0.5f, transform.position.z);

            ExplosiveBaloonProjectile fireball_instance = Instantiate(projectile, newPosition, Quaternion.identity).GetComponent<ExplosiveBaloonProjectile>();
            fireball_instance.SetParameters(newDamage, sizeMultiplier, newRadius, newExplodeDelay, critChanceMultiplier, critDamageMultiplier);
            
            yield return new WaitForSeconds(newSpawnDelay);
        }
    }

    public override void Activate()
    {
        currentLevel = 1;
        projectile = basicProjectile;
        UpdateAbility(currentLevel);

        StartCoroutine(Shoot());
    }

    public override void StopSpell(bool isStopping)
    {
        if(isStopping)
        {
            StopAllCoroutines();
        } else
        {
            if (currentLevel >= 1)
            {
                StartCoroutine(Shoot());
            }
        }
    }
}
