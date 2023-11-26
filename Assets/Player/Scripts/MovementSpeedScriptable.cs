using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSpeedScriptable : AbilityBaseScript
{
    PlayerMovement player;
    [SerializeField] float[] moveSpeedOnLevel;
    float currentMoveSpeed = 1f;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    public override void Activate()
    {
        currentLevel = 1;
        UpdateAbility(currentLevel);
    }

    public override void UpdateAbility(int lvl)
    {
        currentMoveSpeed = moveSpeedOnLevel[currentLevel];
        player.AddMoveSpeed(currentMoveSpeed);
    }
}
