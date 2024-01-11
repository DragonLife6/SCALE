using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class DarkAuraScriptable : AbilityBaseScript
{
    [SerializeField] GameObject circlePrefab;
    [SerializeField] GameObject backgroundPrefab;
    DarkAuraCircle circle;
    SpriteRenderer background;
    [SerializeField] float damage = 5f;
    [SerializeField] float delay = 1f;
    [SerializeField] float size = 1f;
    [SerializeField] Sprite[] bgSprites;

    float newDamage;
    float newSize;
    float newDelay;

    private void Start()
    {
        // Спільні параметри
        // damageMultiplier, sizeMultiplier, delayMultiplier, countMultiplier, critChanceMultiplier, critDamageMultiplier

        damage *= damageMultiplier;
        size *= sizeMultiplier;
    }


    public override void Activate()
    {
        background = Instantiate(backgroundPrefab, transform).GetComponent<SpriteRenderer>();
        circle = Instantiate(circlePrefab, transform).GetComponent<DarkAuraCircle>();
        newDamage = damage;
        newSize = size;
        newDelay = delay;
        currentLevel = 1;

        circle.SetParameters(newDamage, newDelay, newSize);
    }

    public override void UpdateAbility(int lvl)
    {
        newSize = size * (1f + 0.2f * lvl);
        circle.SetParameters(newDamage, newDelay, newSize);
        if(bgSprites.Length >= lvl)
        {
            background.sprite = bgSprites[lvl - 1];
        }
    }
}
