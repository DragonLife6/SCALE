using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float health;
    private float currentMaxHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentMaxHealth = maxHealth;
        health = currentMaxHealth;
    }

    
    public void GetDamage(float damage)
    {
        health -= damage;
        Debug.Log(health);
        if(health < 0) {
            Debug.Log("Death!");
        }
    }
}
