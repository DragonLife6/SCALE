using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningScriptable : AbilityBaseScript
{
    [SerializeField] GameObject basicProjectile;
    [SerializeField] GameObject explosiveProjectile;
    [SerializeField] GameObject secondLayerProjectile;

    GameObject projectile;
    [SerializeField] float[] damageOnLevel;
    [SerializeField] float[] speedOnLevel;
    [SerializeField] float[] radiusOnLevel;
    [SerializeField] float[] sizeOnLevel;
    [SerializeField] int[] countOnLevel;

    List<GameObject> projectiles = new List<GameObject>();

    [SerializeField] GameObject secondLayerPrefab;
    Transform secondLayer;

    float newDamage;
    float newSpeed;
    float newRadius;
    float newSize;
    int newCount;

    int bonusCount = 0;
    float bonusRadius = 0f;

    public override void OnMaxLevel(int variant)
    {
        if (variant == 0)
        {
            bonusCount += 5;
            bonusRadius = newRadius / 3f;
        }
        else if (variant == 1)
        {
            projectile = explosiveProjectile;
        }
        else
        {
            newDamage /= 1.5f;
            newSize /= 2f;
            projectile = secondLayerProjectile;
            CreateSecondLayer();
        }

        SpawnObjects();
    }

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

        for (int i = 0; i < (newCount + bonusCount); i++)
        {
            GameObject newProjectile = Instantiate(projectile, transform);
            projectiles.Add(newProjectile);
            projectiles[i].GetComponent<SpinningProjectile>().SetParameters(newDamage, newSize, critChanceMultiplier, critDamageMultiplier);

            float x = transform.position.x + (newRadius + bonusRadius) * Mathf.Cos((360 / (newCount + bonusCount)) * i * Mathf.Deg2Rad);
            float y = transform.position.y + (newRadius + bonusRadius) * Mathf.Sin((360 / (newCount + bonusCount)) * i * Mathf.Deg2Rad);

            projectiles[i].transform.position = new Vector3(x, y, transform.position.z);
            projectiles[i].transform.rotation = Quaternion.Euler(0, 0, (360 / (newCount + bonusCount)) * i);
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

    void CreateSecondLayer()
    {
        secondLayer = Instantiate(secondLayerPrefab, transform.parent).GetComponent<Transform>();

        for (int i = 0; i < (newCount + bonusCount); i++)
        {
            GameObject newProjectile = Instantiate(projectile, secondLayer);

            newProjectile.GetComponent<SpinningProjectile>().SetParameters(newDamage, newSize, critChanceMultiplier, critDamageMultiplier);

            float x = transform.position.x + ((newRadius + bonusRadius) * 2) * Mathf.Cos((360 / (newCount + bonusCount)) * i * Mathf.Deg2Rad);
            float y = transform.position.y + ((newRadius + bonusRadius) * 2) * Mathf.Sin((360 / (newCount + bonusCount)) * i * Mathf.Deg2Rad);

            newProjectile.transform.position = new Vector3(x, y, transform.position.z);
            newProjectile.transform.rotation = Quaternion.Euler(0, 0, (360 / (newCount + bonusCount)) * i);
        }

        StartCoroutine(RotateSecondLayer());
    }

    private IEnumerator RotateSecondLayer()
    {
        while (true)
        {
            secondLayer.Rotate(Vector3.forward * newSpeed * Time.deltaTime * -1);

            yield return null;
        }
    }

    public override void Activate()
    {
        currentLevel = 1;
        projectile = basicProjectile;

        UpdateAbility(currentLevel);
        StartCoroutine(RotateObjectCoroutine());
    }
}
