using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float health;
    private float currentMaxHealth;
    private HitFlashScript flashScript;
    private Animator animator;
    public bool isAlive = true;

    [SerializeField] GameObject deathMenu;
    [SerializeField] Collider2D collider2D;

    [SerializeField] UI_SliderScript ui_healthBar;

    // Start is called before the first frame update
    void Start()
    {
        deathMenu.SetActive(false);
        currentMaxHealth = maxHealth;
        health = currentMaxHealth;
        ui_healthBar.UpdateSlider(health, currentMaxHealth);
        flashScript = GetComponent<HitFlashScript>();
        animator = GetComponent<Animator>();
    }

    
    public void GetDamage(float damage)
    {
        health -= damage;
        ui_healthBar.UpdateSlider(health, currentMaxHealth);

        flashScript.HitFlash();
        if (health <= 0) {
            isAlive = false;
            animator.SetTrigger("Death");

            collider2D.enabled = false;
            deathMenu.SetActive(true);

            Invoke(nameof(StopTime), 3.3f);
        }
    }

    private void StopTime()
    {
        Time.timeScale = 0f;
    }

    public void SetMaxHealth(float coef)
    {
        float tmp = currentMaxHealth;
        currentMaxHealth = maxHealth * coef;
        health += currentMaxHealth - tmp;

        ui_healthBar.UpdateSlider(health, currentMaxHealth);
    }
}
