using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectionPassiveScriptable : AbilityBaseScript
{
    PlayerHealth player;
    [SerializeField] float[] protectionOnLevel;
    float currentProtection = 0f;

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
        currentProtection = protectionOnLevel[currentLevel - 1];
        player.SetProtection(currentProtection);
    }
}
