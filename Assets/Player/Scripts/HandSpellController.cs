using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSpellController : AbilityBaseScript
{
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject handBase;
    [SerializeField] float damage = 10f;
    [SerializeField] float size = 1f;
    [SerializeField] float delay = 2f;
    [SerializeField] int projectilesNum = 1;

    float newDamage;
    float newSize;
    float newDelay;

    Vector2 target;
    Vector3 targetPosition;

    private void Start()
    {
        // Спільні параметри
        // damageMultiplier, sizeMultiplier, delayMultiplier, countMultiplier, critChanceMultiplier, critDamageMultiplier

        damage *= damageMultiplier;
        size *= sizeMultiplier;
        delay *= delayMultiplier;
    }

    private void SpawnNewHand()
    {
        Quaternion rotation;
        float z = Random.Range(-70, 20);
        float y = Random.Range(-1, 1);
        if (currentLevel % 2 == 0)
            rotation = Quaternion.Euler(0, 0, z);
        else
            rotation = Quaternion.Euler(0, 180, z);
        GameObject newHand = Instantiate(handBase, transform);
        newHand.transform.rotation = rotation;
    }

    public override void UpdateAbility(int lvl)
    {
        projectilesNum = lvl;
        newDamage = damage * lvl;
        newSize = size + 0.1f * lvl;
        newDelay = delay - 0.1f * lvl;

        SpawnNewHand();
    }

    private IEnumerator Shoot()
    {
        while (true)
        { 
            for (int i = 0; i < projectilesNum; i++)
            {
                target = Random.insideUnitCircle * 5;
                targetPosition = new Vector3(transform.position.x + target.x, transform.position.y + target.y, 0);

                HandProjectileScript hand_instance = Instantiate(projectile, targetPosition, Quaternion.identity).GetComponent<HandProjectileScript>();
                hand_instance.SetParameters(newDamage, newSize);
                float y = 0;
                if (i % 2 == 1)
                    y = -1;
                hand_instance.transform.rotation *= new Quaternion(0, y, 0, 0);

                yield return new WaitForSeconds(Random.Range(0.0f, 0.5f));
            }

            yield return new WaitForSeconds(newDelay);
        }
    }

    public override void Activate()
    {
        newDamage = damage;
        newSize = size;
        newDelay = delay;
        currentLevel = 1;

        SpawnNewHand();
        StartCoroutine(Shoot());
    }
}
