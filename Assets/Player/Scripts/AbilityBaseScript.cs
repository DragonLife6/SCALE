using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AbilityBaseScript : MonoBehaviour
{
    [SerializeField] string abilityName;
    [SerializeField] string[] abilityDescriptions;
    [SerializeField] int maxLevel = 7;
    [SerializeField] Sprite abilityIcon;
    protected int currentLevel;

    public bool isPassive = false;

    protected float damageMultiplier;
    protected float sizeMultiplier;
    protected float delayMultiplier;
    protected int countMultiplier;
    protected float critChanceMultiplier;
    protected float critDamageMultiplier;


    private void Start()
    {
        currentLevel = 0;
    }

    public bool LevelUp()
    {
        if (currentLevel == 0)
        {
            Activate();
        }
        else
        {
            currentLevel++;
            UpdateAbility(currentLevel);
            if(currentLevel >= maxLevel)
                return currentLevel >= maxLevel;
        }
        
        return currentLevel > maxLevel;
    }

    public void SetBaseParams(float damage, float size, float delay, int count, float critChance, float critDamage)
    {
        damageMultiplier = damage;
        sizeMultiplier = size;
        delayMultiplier = delay;
        countMultiplier = count;
        critChanceMultiplier = critChance;
        critDamageMultiplier = critDamage;
        
        if (currentLevel > 0)
        {
            UpdateAbility(currentLevel);
        }
    }

    public string GetName() { return abilityName; }
    public string GetDescriptionOnCurrentLevel() { return abilityDescriptions[currentLevel]; }
    public string GetDescriptionOnLevel(int level) { return abilityDescriptions[level]; }
    public int GetCurrentLevel() { return currentLevel; }
    public Sprite GetIcon() { return abilityIcon; }

    public abstract void UpdateAbility(int lvl);
    public abstract void Activate();
}
