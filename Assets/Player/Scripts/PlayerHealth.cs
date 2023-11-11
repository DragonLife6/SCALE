using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float health;
    private float currentMaxHealth;
    private HitFlashScript flashScript;

    // Start is called before the first frame update
    void Start()
    {
        currentMaxHealth = maxHealth;
        health = currentMaxHealth;
        flashScript = GetComponent<HitFlashScript>();
    }

    
    public void GetDamage(float damage)
    {
        health -= damage;
        flashScript.HitFlash();
        if (health <= 0) {
            Debug.Log("Death!");
        }
    }

    public void SetMaxHealth(float coef)
    {
        float tmp = currentMaxHealth;
        currentMaxHealth = maxHealth * coef;
        health += currentMaxHealth - tmp;
    }
}
