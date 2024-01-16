using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectedShotScriptable : AbilityBaseScript
{
    [SerializeField] GameObject projectile;
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
                    DirectedShotProjectile fireball_instance = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<DirectedShotProjectile>();
                    fireball_instance.SetParameters(newDamage, newSpeed, newSize, critChanceMultiplier, critDamageMultiplier, enemies[i]);
                }
            }

            yield return new WaitForSeconds(newDelay);
        }
    }

    public override void Activate()
    {
        enemyManager = GameObject.Find("Enemy_manager").GetComponent<EnemyManager>();
        currentLevel = 1;
        UpdateAbility(currentLevel);

        StartCoroutine(Shoot());
    }
}
