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
            if(currentLevel > maxLevel)
                return currentLevel > maxLevel;
            UpdateAbility(currentLevel);
        }
        
        return currentLevel > maxLevel;
    }

    public string GetName() { return abilityName; }
    public string GetDescriptionOnCurrentLevel() { return abilityDescriptions[currentLevel]; }
    public string GetDescriptionOnLevel(int level) { return abilityDescriptions[level]; }
    public int GetCurrentLevel() { return currentLevel; }
    public Sprite GetIcon() { return abilityIcon; }

    public abstract void UpdateAbility(int lvl);
    public abstract void Activate();
}
