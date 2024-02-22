using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSpellController : AbilityBaseScript
{
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject voidProjectile;
    [SerializeField] GameObject secondProjectile;
    [SerializeField] GameObject handBase;
    [SerializeField] float[] damageOnLevel;
    [SerializeField] float[] sizeOnLevel;
    [SerializeField] float[] delayOnLevel;

    float newDamage;
    float newSize;
    float newDelay;

    int newCount;
    int currentHands = 0;
    List<GameObject> spawnedHands = new List<GameObject>();

    Vector2 target;
    Vector3 targetPosition;

    public override void OnMaxLevel(int variant)
    {
        if (variant == 0)
        {
            newDelay /= 1.2f;
            newDamage *= 1.2f;
            newCount++;
            SpawnNewHand();
        }
        else if (variant == 1)
        {
            projectile = secondProjectile;
        }
        else
        {
            projectile = voidProjectile;

            foreach (var hand in spawnedHands)
            {
                Destroy(hand);
            }

            newCount -= 2;
        }
    }

    private void SpawnNewHand()
    {
        currentHands++;

        Quaternion rotation;
        float z = Random.Range(-70, 20);

        if (currentLevel % 2 == 0)
            rotation = Quaternion.Euler(0, 0, z);
        else
            rotation = Quaternion.Euler(0, 180, z);
        GameObject newHand = Instantiate(handBase, transform);
        newHand.transform.rotation = rotation;
        spawnedHands.Add(newHand);
    }

    public override void UpdateAbility(int lvl)
    {
        newDamage = damageOnLevel[lvl - 1] * damageMultiplier;
        newSize = sizeOnLevel[lvl - 1] * sizeMultiplier;
        newDelay = delayOnLevel[lvl - 1] * delayMultiplier;
        newCount = lvl;

        if (currentHands < newCount)
        {
            SpawnNewHand();
        }
    }

    private IEnumerator Shoot()
    {
        while (true)
        { 
            for (int i = 0; i < newCount; i++)
            {
                target = Random.insideUnitCircle * 5;
                targetPosition = new Vector3(transform.position.x + target.x, transform.position.y + target.y, 0);

                HandProjectileBase hand_instance = Instantiate(projectile, targetPosition, Quaternion.identity).GetComponent<HandProjectileBase>();
                hand_instance.SetParameters(newDamage, newSize, critChanceMultiplier, critDamageMultiplier);
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
