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

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        flashScript = GetComponent<HitFlashScript>();
        animator = GetComponent<Animator>();
    }


    public void GetDamage(float damage)
    {
        health -= damage;
        flashScript.HitFlash();
        if (health <= 0)
        {
            DeathAndDestroy();
        }
    }

    private void DeathAndDestroy()
    {
        Destroy(gameObject, 0.3f);
        animator.Play("SoulEnemyDeath");
        int randomPrefab = Random.Range(0, soulPrefabs.Length);
        Instantiate(soulPrefabs[randomPrefab], transform.position, Quaternion.identity);
    }
}
