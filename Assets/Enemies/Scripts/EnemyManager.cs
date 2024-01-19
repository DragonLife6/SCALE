using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] bool testMode = false;

    [SerializeField] float[] newSpawnIntervals;
    [SerializeField] int[] enemySpawnCounts;
    [SerializeField] int[] minEnemyLevels;
    [SerializeField] int[] maxEnemyLevels;

    int currentDifficultyIndex = 0;
    float nextParameterChangeTime = 0f;
    [SerializeField] float changeInterval = 30f;

    [SerializeField] GameObject[] enemyPrefabs;
    int maxEnemies = 1;
    float spawnInterval = 1.0f;

    public float spawnDistance = 20.0f;
    public float maxDistance = 20.0f;

    int minEnemyLvl = 0;
    int maxEnemyLvl = 1;

    private Transform player;
    private List<Transform> enemies = new List<Transform>();

    void Start()
    {
        player = GameObject.Find("Player").transform;
        if (!testMode)
        {
            InvokeRepeating("SpawnEnemies", 0.1f, spawnInterval);
        }
        nextParameterChangeTime = Time.time + changeInterval;

        maxEnemies = enemySpawnCounts[currentDifficultyIndex];
        spawnInterval = newSpawnIntervals[currentDifficultyIndex];
        minEnemyLvl = minEnemyLevels[currentDifficultyIndex];
        maxEnemyLvl = maxEnemyLevels[currentDifficultyIndex];
    }

    private void ChangeParameters()
    {
        currentDifficultyIndex++;

        if (newSpawnIntervals.Length <= currentDifficultyIndex)
        {
            maxEnemies++;
            spawnInterval *= 0.9f;
        } else
        {
            maxEnemies = enemySpawnCounts[currentDifficultyIndex];
            spawnInterval = newSpawnIntervals[currentDifficultyIndex];
            minEnemyLvl = minEnemyLevels[currentDifficultyIndex];
            maxEnemyLvl = maxEnemyLevels[currentDifficultyIndex];
        }
    }

    private void Update()
    {
        if (Time.time >= nextParameterChangeTime)
        {
            ChangeParameters();
            nextParameterChangeTime = Time.time + changeInterval;
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            if (GetDistance(enemies[i]) > maxDistance)
            {
                GameObject enemyRef = enemies[i].gameObject;
                Destroy(enemyRef);
                SpawnEnemy(enemyRef);
            }
        }
    }

    public void AddEnemy(Transform newEnemy)
    {
        enemies.Add(newEnemy);
    }

    public void SpawnEnemy(GameObject enemyRef)
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        Transform newEnemy = Instantiate(enemyRef, spawnPosition, Quaternion.identity).transform;
        enemies.Add(newEnemy);
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < maxEnemies; i++)
        {
            if(maxEnemyLvl > enemyPrefabs.Length || maxEnemyLvl < minEnemyLvl)
            {
                maxEnemyLvl = enemyPrefabs.Length;
            }
            int randomNum = Random.Range(minEnemyLvl, maxEnemyLvl);
            Vector3 spawnPosition = GetRandomSpawnPosition();
            Transform newEnemy = Instantiate(enemyPrefabs[randomNum], spawnPosition, Quaternion.identity).transform;
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
        if (transform != null)
        {
            return Vector3.Distance(player.position, transform.position);
        } else
        {
            return 0f;
        }
    }
}
