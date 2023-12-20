using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SoulExplosionScriptable : AbilityBaseScript
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float damage = 5f;
    [SerializeField] float delay = 2f;
    [SerializeField] int projectilesCount = 2;
    [SerializeField] Sprite[] projectileSprites;

    float newDamage;
    float newCount;
    float newDelay;

    public override void UpdateAbility(int lvl)
    {
        newCount = projectilesCount * lvl;
        newDamage = damage * lvl;
        newDelay = delay - 0.1f * lvl;
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            float projectileRotation;
            float randomOffset = Random.Range(0f, 90f);
            for (int i = 0; i < newCount; i++)
            {
                projectileRotation = (360 / newCount) * i + randomOffset;
                
                SoulExplosionProjectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<SoulExplosionProjectile>();
                projectile.SetParameters(newDamage, projectileRotation);
            }

            yield return new WaitForSeconds(newDelay);
        }
    }

    public override void Activate()
    {
        newDamage = damage;
        newCount = projectilesCount;
        newDelay = delay;
        currentLevel = 1;

        StartCoroutine(Shoot());
    }
}
