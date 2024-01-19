using System.Collections;
using UnityEngine;

public class HandSpellController : AbilityBaseScript
{
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject handBase;
    [SerializeField] float[] damageOnLevel;
    [SerializeField] float[] sizeOnLevel;
    [SerializeField] float[] delayOnLevel;

    float newDamage;
    float newSize;
    float newDelay;

    int newCount;
    int currentHands = 0;

    Vector2 target;
    Vector3 targetPosition;


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

                HandProjectileScript hand_instance = Instantiate(projectile, targetPosition, Quaternion.identity).GetComponent<HandProjectileScript>();
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
}
