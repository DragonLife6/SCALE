using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SoulExplosionScriptable : AbilityBaseScript
{
    [SerializeField] GameObject[] baseProjectilePrefabs;
    [SerializeField] GameObject[] ricosheteProjectilePrefabs;
    [SerializeField] GameObject[] riftProjectilePrefabs;
    [SerializeField] GameObject riftCenterPrefab;

    GameObject[] projectilePrefabs;
    [SerializeField] float[] damageOnLevel;
    [SerializeField] float[] delayOnLevel;
    [SerializeField] int[] countOnLevel;
    [SerializeField] float[] speedOnLevel;
    [SerializeField] float[] sizeOnLevel;

    float newDamage;
    float newCount;
    float newDelay;
    float newSpeed;
    float newSize;

    bool isRift = false;

    public override void OnMaxLevel(int variant)
    {
        if (variant == 0)
        {
            newDelay /= 1.5f;
            newDamage *= 1.2f;
            newCount++;
        }
        else if (variant == 1)
        {
            projectilePrefabs = ricosheteProjectilePrefabs;
        }
        else
        {
            newDelay *= 1.5f;
            newDamage /= 2f;
            newCount = Mathf.CeilToInt(newCount / 1.5f);
            projectilePrefabs = riftProjectilePrefabs;
            isRift = true;
        }
    }

    public override void UpdateAbility(int lvl)
    {
        if (currentLevel < maxLevel)
        {
            newDamage = damageOnLevel[lvl - 1] * damageMultiplier;
            newCount = countOnLevel[lvl - 1] + countMultiplier;
            newDelay = delayOnLevel[lvl - 1] * delayMultiplier;
            newSpeed = speedOnLevel[lvl - 1];
            newSize = sizeOnLevel[lvl - 1] * sizeMultiplier;
        }
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            float projectileRotation;
            float randomOffset = Random.Range(0f, 90f);

            if(isRift)
            {
                Destroy(Instantiate(riftCenterPrefab, transform.position, Quaternion.identity), 3f);
            }
            for (int i = 0; i < newCount; i++)
            {
                projectileRotation = (360 / newCount) * i + randomOffset;
                int num = Random.Range(0, projectilePrefabs.Length);

                SoulExplosionProjectile projectile = Instantiate(projectilePrefabs[num], transform.position, Quaternion.identity).GetComponent<SoulExplosionProjectile>();
                projectile.SetParameters(newDamage, newSpeed, newSize, critChanceMultiplier, critDamageMultiplier, projectileRotation);
            }

            yield return new WaitForSeconds(newDelay);
        }
    }

    public override void Activate()
    {
        projectilePrefabs = baseProjectilePrefabs;
        currentLevel = 1;

        UpdateAbility(currentLevel);

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
