using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectedShotScriptable : AbilityBaseScript
{
    [SerializeField] float maxDistance = 5.0f;

    [SerializeField] GameObject basicProjectile;
    [SerializeField] GameObject lightningProjectile;
    [SerializeField] GameObject fireProjectile;
    GameObject projectile;
    [SerializeField] float[] damageOnLevel;
    [SerializeField] float[] speedOnLevel;
    [SerializeField] float[] delayOnLevel;
    [SerializeField] int[] projectilesNumOnLevel;
    [SerializeField] float[] projectilesSizeOnLevel;

    EnemyManager enemyManager;

    List<Transform> enemies = new List<Transform>();

    float newDamage = 0f;
    float newSpeed = 0f;
    float newDelay = 0f;
    float newSize = 0f;
    int newProjectilesNum = 0;

    public override void OnMaxLevel(int variant)
    {
        if (variant == 0)
        {
            newDelay /= 1.5f;
            newDamage *= 1.2f;
            newProjectilesNum++;
        }
        else if (variant == 1)
        {
            projectile = fireProjectile;
            newProjectilesNum = 2; // balance
        }
        else
        {
            projectile = lightningProjectile;
            newProjectilesNum = 3; // balance
        }
    }

    public override void UpdateAbility(int lvl)
    {
        newDamage = damageOnLevel[lvl - 1] * damageMultiplier;
        newSpeed = speedOnLevel[lvl - 1];
        newDelay = delayOnLevel[lvl - 1] * delayMultiplier;
        newSize = projectilesSizeOnLevel[lvl - 1] * sizeMultiplier;
        newProjectilesNum = projectilesNumOnLevel[lvl - 1] + countMultiplier;
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            enemies = enemyManager.GetClossestEnemies();
            for (int i = 0; i < newProjectilesNum; i++)
            {
                if (enemies.Count > i)
                {
                    if (Vector3.Distance(enemies[i].position, transform.position) <= maxDistance)
                    {
                        float randomOffset = Random.Range(0f, 0.1f);
                        DirectedProjectileBase fireball_instance = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<DirectedProjectileBase>();
                        fireball_instance.SetParameters(newDamage, newSpeed, newSize, critChanceMultiplier, critDamageMultiplier, enemies[i]);

                        yield return new WaitForSeconds(randomOffset);
                    }
                }
            }

            yield return new WaitForSeconds(newDelay);
        }
    }

    public override void Activate()
    {
        enemyManager = GameObject.Find("Enemy_manager").GetComponent<EnemyManager>();
        currentLevel = 1;
        projectile = basicProjectile;
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
