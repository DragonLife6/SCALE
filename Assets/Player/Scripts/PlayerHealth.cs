using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] AdsRewardedScript ads;

    public float maxHealth = 100f;
    private float health;
    private float currentMaxHealth;
    private float protectionCoef;
    private HitFlashScript flashScript;
    private Animator animator;
    public bool isAlive = true;
    private bool firstDeath = false;

    int protectionCount = 0;
    [SerializeField] GameObject[] protectionImages;

    AudioSource audioSource;
    [SerializeField] AudioClip heartBeatSoud;

    [SerializeField] GameObject deathMenu;
    [SerializeField] GameObject afterDeathMenu;
    [SerializeField] Collider2D playerCollider2D;

    [SerializeField] UI_SliderScript ui_healthBar;

    [SerializeField] TimeStoper timeStoper;

    // Start is called before the first frame update
    void Start()
    {
        protectionCoef = 0f;
        deathMenu.SetActive(false);
        currentMaxHealth = maxHealth;
        health = currentMaxHealth;
        ui_healthBar.UpdateSlider(health, currentMaxHealth);
        flashScript = GetComponent<HitFlashScript>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        Invoke(nameof(AdsLoading), 10f);
    }

    private void AdsLoading()
    {
        ads.LoadAd();
    }

    public void GetHealing(float healAmount)
    {
        health += healAmount;

        if (health > currentMaxHealth)
        {
            health = currentMaxHealth;
        }

        ui_healthBar.UpdateSlider(health, currentMaxHealth);
    }

    public void SetProtection(int count, int maxCount)
    {
        protectionCount += count;

        if (protectionCount > maxCount)
        {
            protectionCount = maxCount;
        }

        foreach (GameObject obj in protectionImages)
        {
            obj.SetActive(false);
        }

        if (protectionImages.Length >= protectionCount)
        {
            protectionImages[protectionCount - 1].SetActive(true);
        }
    }

    public void GetDamage(float damage)
    {
        if (protectionCount <= 0)
        {
            health -= damage * (1f - protectionCoef);
            ui_healthBar.UpdateSlider(health, currentMaxHealth);

            if (health < currentMaxHealth * 0.3f)
            {
                audioSource.PlayOneShot(heartBeatSoud, 0.5f);
            }

            flashScript.HitFlash();
            if (health <= 0)
            {
                if (!firstDeath)
                {
                    timeStoper.StopAllObjects();
                    deathMenu.SetActive(true);
                    playerCollider2D.enabled = false;
                    firstDeath = true;
                }
                else
                {
                    isAlive = false;
                    animator.SetTrigger("Death");
                    Invoke(nameof(StopTime), 3.3f);
                    afterDeathMenu.SetActive(true);
                }
            }
        } else
        {
            protectionImages[protectionCount - 1].SetActive(false);
            protectionCount--;
            if (protectionCount > 0)
            {
                protectionImages[protectionCount - 1].SetActive(true);
            }
        }
    }

    public void Respawn(bool isRespawning)
    {
        if (isRespawning)
        {
            health = currentMaxHealth;
            ui_healthBar.UpdateSlider(health, currentMaxHealth);
            playerCollider2D.enabled = true;
            timeStoper.ResumeAllObjects();
        } else
        {
            isAlive = false;
            animator.SetTrigger("Death");
            Invoke(nameof(StopTime), 3.3f);
        }
    }

    private void StopTime()
    {
        Time.timeScale = 0f;
    }

    public float GetCurrentMaxHealth()
    {
        return currentMaxHealth;
    }

    public void SetMaxHealth(float newMax)
    {
        maxHealth = newMax;
        currentMaxHealth = maxHealth;
        health = currentMaxHealth;

        ui_healthBar.UpdateSlider(health, currentMaxHealth);
    } 

    public void SetProtection(float coef)
    {
        protectionCoef = coef;
    }

    public void SetCurrentMaxHealth(float coef)
    {
        float tmp = currentMaxHealth;
        currentMaxHealth = maxHealth * coef;
        health += currentMaxHealth - tmp;

        ui_healthBar.UpdateSlider(health, currentMaxHealth);
    }
}
