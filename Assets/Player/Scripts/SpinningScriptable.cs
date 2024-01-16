using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningScriptable : AbilityBaseScript
{
    [SerializeField] GameObject projectile;
    [SerializeField] float[] damageOnLevel;
    [SerializeField] float[] speedOnLevel;
    [SerializeField] float[] radiusOnLevel;
    [SerializeField] float[] sizeOnLevel;
    [SerializeField] int[] countOnLevel;

    List<GameObject> projectiles = new List<GameObject>();

    float newDamage;
    float newSpeed;
    float newRadius;
    float newSize;
    int newCount;

    public override void UpdateAbility(int lvl)
    {
        newDamage = damageOnLevel[lvl - 1] * damageMultiplier;
        newSpeed = speedOnLevel[lvl - 1];
        newRadius = radiusOnLevel[lvl - 1];
        newSize = sizeOnLevel[lvl - 1] * sizeMultiplier;
        newCount = countOnLevel[lvl - 1] + countMultiplier;

        SpawnObjects();
    }

    private void SpawnObjects()
    {
        if (projectiles.Count > 0)
        {
            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                GameObject tmp = projectiles[i];
                projectiles.RemoveAt(i);
                Destroy(tmp);
            }
        }

        for (int i = 0; i < newCount; i++)
        {
            GameObject newProjectile = Instantiate(projectile, transform);
            projectiles.Add(newProjectile);
            projectiles[i].GetComponent<SpinningProjectile>().SetParameters(newDamage, newSize, critChanceMultiplier, critDamageMultiplier);

            float x = transform.position.x + newRadius * Mathf.Cos((360 / newCount) * i * Mathf.Deg2Rad);
            float y = transform.position.y + newRadius * Mathf.Sin((360 / newCount) * i * Mathf.Deg2Rad);

            projectiles[i].transform.position = new Vector3(x, y, transform.position.z);
            projectiles[i].transform.rotation = Quaternion.Euler(0, 0, (360 / newCount) * i);
        }
    }

    private IEnumerator RotateObjectCoroutine()
    {
        while (true)
        {
            transform.Rotate(Vector3.forward * newSpeed * Time.deltaTime); // Обертання об'єкта

            yield return null;
        }
    }

    public override void Activate()
    {
        currentLevel = 1;

        UpdateAbility(currentLevel);
        StartCoroutine(RotateObjectCoroutine());
    }
}
