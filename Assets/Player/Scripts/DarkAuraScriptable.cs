using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class DarkAuraScriptable : AbilityBaseScript
{
    [SerializeField] GameObject circlePrefab;
    [SerializeField] GameObject backgroundPrefab;
    DarkAuraCircle circle;
    SpriteRenderer background;

    [SerializeField] float[] damageOnLevel;
    [SerializeField] float[] delayOnLevel;
    [SerializeField] float[] sizeOnLevel;

    [SerializeField] Sprite[] bgSprites;

    public float newDamage;
    float newDelay;
    float newSize;


    public override void Activate()
    {
        background = Instantiate(backgroundPrefab, transform).GetComponent<SpriteRenderer>();
        circle = Instantiate(circlePrefab, transform).GetComponent<DarkAuraCircle>();
        
        currentLevel = 1;
        UpdateAbility(currentLevel);
    }

    public override void UpdateAbility(int lvl)
    {
        newDamage = damageOnLevel[lvl - 1] * damageMultiplier;
        newDelay = delayOnLevel[lvl - 1];
        newSize = sizeOnLevel[lvl - 1] * sizeMultiplier;
        
        circle.SetParameters(newDamage, newDelay, newSize, critChanceMultiplier, critDamageMultiplier);
        
        if(bgSprites.Length >= lvl)
        {
            background.sprite = bgSprites[lvl - 1];
        }
    }
}
