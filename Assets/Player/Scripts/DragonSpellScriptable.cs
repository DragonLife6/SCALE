using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonSpellScriptable : AbilityBaseScript
{
    PlayerMovement player;
    GameObject projectile;
    [SerializeField] GameObject basicProjectile;
    [SerializeField] GameObject forwardProjectile;
    [SerializeField] GameObject archProjectile;
    [SerializeField] float[] damageOnLevel;
    [SerializeField] float[] sizeOnLevel;
    [SerializeField] float[] spawnDelayOnLevel;
    [SerializeField] float[] speedOnLevel;

    float newDamage = 0f;
    float newSize = 0f;
    float newSpawnDelay = 0f;
    float newSpeed = 0f;
    int count = 1;

    bool isSecondVariant = false;
    bool isThirdVariant = false;

    public override void OnMaxLevel(int variant)
    {
        if (variant == 0)
        {
            newSize *= 1.5f;
        } else if (variant == 1)
        {
            count++;
            isSecondVariant = true;
            projectile = forwardProjectile;
        } else
        {
            isThirdVariant = true;
            projectile = archProjectile;
        }
    }

    public override void UpdateAbility(int lvl)
    {
        newDamage = damageOnLevel[lvl - 1] * damageMultiplier;
        newSize = sizeOnLevel[lvl - 1] * sizeMultiplier;
        newSpawnDelay = spawnDelayOnLevel[lvl - 1] * delayMultiplier;
        newSpeed = speedOnLevel[lvl - 1];
    }

    private IEnumerator Shoot()
    {
        float projectileRotation = 0;
        while (true)
        {
            for (int i = 0; i < count; i++)
            {
                if(isSecondVariant || isThirdVariant)
                {
                    projectileRotation = Random.Range(0f, 360f);
                } else
                {
                    if (player.getMovementAngle() != 0)
                    {
                        projectileRotation = player.getMovementAngle();
                    }
                }

                DragonSpellProjectile fireball_instance = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<DragonSpellProjectile>();
                fireball_instance.SetParameters(newDamage, newSpeed, newSize, critChanceMultiplier, critDamageMultiplier, projectileRotation);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(newSpawnDelay);
        }
    }

    public override void Activate()
    {
        currentLevel = 1;
        projectile = basicProjectile;
        UpdateAbility(currentLevel);
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();

        StartCoroutine(Shoot());
    }

    public override void StopSpell(bool isStopping)
    {
        if (isStopping)
        {
            StopAllCoroutines();
        }
        else
        {
            if (currentLevel >= 1)
            {
                StartCoroutine(Shoot());
            }
        }
    }
}
