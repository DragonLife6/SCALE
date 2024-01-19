using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBaloonScriptable : AbilityBaseScript
{
    [SerializeField] GameObject projectile;
    [SerializeField] float[] damageOnLevel;
    [SerializeField] float[] explosionDelayOnLevel;
    [SerializeField] float[] spawnDelayOnLevel;
    [SerializeField] float[] radiusOnLevel;

    float newDamage = 0f;
    float newSpawnDelay = 0f;
    float newExplodeDelay = 0f;
    float newRadius = 0f;

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
        UpdateAbility(currentLevel);

        StartCoroutine(Shoot());
    }
}
