using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private HitFlashScript flashScript;
    public float maxHealth = 20f;
    private float health;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        flashScript = GetComponent<HitFlashScript>();
    }


    public void GetDamage(float damage)
    {
        health -= damage;
        flashScript.HitFlash();
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
