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
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
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
}
