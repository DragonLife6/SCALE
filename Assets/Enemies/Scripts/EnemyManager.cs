using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int maxEnemies = 1;
    public float spawnDistance = 20.0f;
    public float spawnInterval = 1.0f;

    private Transform player;
    private List<Transform> enemies = new List<Transform>();

    void Start()
    {
        player = GameObject.Find("Player").transform;
        InvokeRepeating("SpawnEnemies", 0, spawnInterval);
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < maxEnemies; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            Transform newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity).transform;
            enemies.Add(newEnemy);
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        int angle = Random.Range(0, 360);

        Vector3 playerPosition = player.position;
        Vector2 randomOffset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * spawnDistance;
        Vector3 spawnPosition = playerPosition + new Vector3(randomOffset.x, randomOffset.y, 0);
        return spawnPosition;
    }

    public void DeleteEnemy(Transform enemy)
    {
        enemies.Remove(enemy);
    }

    public List<Transform> GetClossestEnemies()
    {
        SortList();
        return enemies;
    }

    void SortList()
    {
        int minId;
        int startLength = enemies.Count;
        List<Transform> newList = new List<Transform>();
        for (int i = 0; i < startLength; i++)
        {
            minId = 0;
            for (int j = 1; j < enemies.Count; j++)
            {
                if (GetDistance(enemies[j]) < GetDistance(enemies[minId])) {
                    minId = j;
                }
            }

            newList.Add(enemies[minId]);
            enemies.Remove(enemies[minId]);
        }

        enemies = newList;
    }

    float GetDistance(Transform transform)
    {
        return Vector3.Distance(player.position, transform.position);
    }
}
