using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxExpirienceDecScriptable : AbilityBaseScript
{
    PlayersLvlUp player;
    [SerializeField] int[] bonusExpirience;

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
        player.SetBonusExpirienceProcent(bonusExpirience[lvl - 1] / 100f);
    }
}
