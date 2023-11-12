using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpirienceCollectorScriptable : AbilityBaseScript
{
    PlayersLvlUp player;
    CircleCollider2D collider2d;
    [SerializeField] float[] colliderRadiusOnLevel;
    float currentRadius = 0.5f;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayersLvlUp>();
        collider2d = GetComponent<CircleCollider2D>();   
    }

    public override void Activate()
    {
        currentLevel = 1;
        UpdateAbility(currentLevel);
    }

    public override void UpdateAbility(int lvl)
    {
        currentRadius = colliderRadiusOnLevel[currentLevel];
        collider2d.radius = currentRadius;
    }

    public void AddExpirience(float amount)
    {
        player.AddExpirience(amount);
    }
}
