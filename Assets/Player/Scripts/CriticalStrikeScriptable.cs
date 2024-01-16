using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalStrikeScriptable : AbilityBaseScript
{
    PlayersLvlUp player;
    [SerializeField] float[] critChanceOnLevel;
    [SerializeField] float[] critPowerOnLevel;
    float currentChance = 0f;
    float currentPower = 0f;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayersLvlUp>();
    }

    public override void Activate()
    {
        currentLevel = 1;
        UpdateAbility(currentLevel);
    }

    public override void UpdateAbility(int lvl)
    {
        currentChance = critChanceOnLevel[currentLevel - 1];
        currentPower = critPowerOnLevel[currentLevel - 1];
        player.SetCritDamage(currentChance, currentPower);
    }
}
