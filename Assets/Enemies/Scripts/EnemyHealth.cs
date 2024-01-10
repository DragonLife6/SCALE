using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private HitFlashScript flashScript;
    public float maxHealth = 20f;
    private float health;
    Animator animator;
    [SerializeField] GameObject[] soulPrefabs;
    [SerializeField] bool isRespawnable = true;
    EnemyManager enemyManager;
    public float lastDamageTime;
    public bool spawnedWithManager = true;

    // Start is called before the first frame update
    void Start()
    {
        lastDamageTime = 0f;
        health = maxHealth;
        flashScript = GetComponent<HitFlashScript>();
        animator = GetComponent<Animator>();
        enemyManager = GameObject.Find("Enemy_manager").GetComponent<EnemyManager>();

        if (!spawnedWithManager)
        {
            enemyManager.AddEnemy(transform);
        }

        if (isRespawnable)
        {
            float randomRespawnDelay = Random.Range(100, 300) / 10f;

            Invoke(nameof(Respawn), randomRespawnDelay);
        }
    }


    public void GetDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            DeathAndDestroy();
        }
        flashScript.HitFlash();
        lastDamageTime = Time.time;
    }

    private void Respawn()
    {
        Destroy(gameObject, 0.4f);
        animator.Play("SoulEnemyRespawn");
        enemyManager.SpawnEnemy(gameObject);
    }

    private void DeathAndDestroy()
    {
        Destroy(gameObject, 0.3f);
        animator.Play("SoulEnemyDeath");
        int randomPrefab = Random.Range(0, soulPrefabs.Length);
        Instantiate(soulPrefabs[randomPrefab], transform.position, Quaternion.identity);
    }
}
