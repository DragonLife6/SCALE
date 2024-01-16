using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageScriptable : AbilityBaseScript
{
    PlayersLvlUp player;
    [SerializeField] float[] damageOnLevel;
    float currentDamage = 0f;

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
        currentDamage = damageOnLevel[currentLevel - 1];
        player.SetBonusDamage(currentDamage);
    }
}
