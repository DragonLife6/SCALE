using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningScriptable : AbilityBaseScript
{
    [SerializeField] GameObject projectile;
    [SerializeField] float damage = 5f;
    [SerializeField] float speed = 30f;
    [SerializeField] float radius = 3f;
    [SerializeField] int projectilesNum = 1;

    [SerializeField] GameObject target;

    List<GameObject> projectiles = new List<GameObject>();

    float newDamage;
    float newSpeed;
    float newRadius;

    public override void UpdateAbility(int lvl)
    {
        projectilesNum = lvl + 1;

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

        for (int i = 0; i < projectilesNum; i++)
        {
            GameObject newProjectile = Instantiate(projectile, transform);
            projectiles.Add(newProjectile);
            projectiles[i].GetComponent<SpinningProjectile>().SetParameters(newDamage, 1f + 0.1f * projectilesNum);

            float x = projectiles[i].transform.position.x + newRadius * Mathf.Sin((360 / projectilesNum) * i * Mathf.Deg2Rad);
            float y = projectiles[i].transform.position.y + newRadius * Mathf.Cos((360 / projectilesNum) * i * Mathf.Deg2Rad);

            projectiles[i].transform.position = new Vector3(x, y, transform.position.z);
            projectiles[i].transform.rotation = Quaternion.Euler(0, 0, 90 + (360 / projectilesNum) * i);
        }
    }

    private IEnumerator RotateObjectCoroutine()
    {
        while (true) // Безкінечний цикл для постійного обертання
        {
            transform.Rotate(Vector3.forward * newSpeed * Time.deltaTime); // Обертання об'єкта

            yield return null; // Перерву між кадрами
        }
    }

    public override void Activate()
    {
        newDamage = damage;
        newSpeed = speed;
        newRadius = radius;

        SpawnObjects();
        StartCoroutine(RotateObjectCoroutine());
    }
}
