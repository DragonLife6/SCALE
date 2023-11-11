using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpellScriptable : AbilityBaseScript
{
    PlayerHealth player;
    [SerializeField] float[] maxHealthOnLevel;
    float currentMaxHealth = 0f;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    public override void Activate()
    {
        currentLevel = 1;
        UpdateAbility(currentLevel);
    }

    public override void UpdateAbility(int lvl)
    {
        currentMaxHealth = maxHealthOnLevel[currentLevel];
        player.SetMaxHealth(currentMaxHealth);
    }
}
