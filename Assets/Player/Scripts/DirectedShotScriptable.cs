using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectedShotScriptable : AbilityBaseScript
{
    [SerializeField] GameObject projectile;
    [SerializeField] float damage = 10f;
    [SerializeField] float speed = 10f;
    [SerializeField] float delay = 2f;
    [SerializeField] int projectilesNum = 1;
    EnemyManager enemyManager;

    List<Transform> enemies = new List<Transform>();

    float newDamage;
    float newSpeed;
    float newDelay;

    private void Start()
    {
        damage *= damageMultiplier;
        delay *= delayMultiplier;
        projectilesNum += countMultiplier;
    }

    public override void UpdateAbility(int lvl)
    {
        newDamage = damage * lvl;


    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            enemies = enemyManager.GetClossestEnemies();
            for (int i = 0; i < projectilesNum; i++)
            {
                if (enemies.Count > i)
                {
                    DirectedShotProjectile fireball_instance = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<DirectedShotProjectile>();
                    fireball_instance.SetParameters(newDamage, newSpeed, enemies[i]);
                }
            }

            yield return new WaitForSeconds(newDelay);
        }
    }

    public override void Activate()
    {
        enemyManager = GameObject.Find("Enemy_manager").GetComponent<EnemyManager>();

        newDamage = damage;
        newSpeed = speed;
        newDelay = delay;
        currentLevel = 1;

        StartCoroutine(Shoot());
    }
}
