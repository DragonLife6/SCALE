using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AbilityBaseScript : MonoBehaviour
{
    [SerializeField] string abilityName;
    [SerializeField] string abilityDescription;
    [SerializeField] int maxLevel = 7;
    protected int currentLevel;


    private void Start()
    {
        currentLevel = 0;
    }

    public bool LevelUp()
    {
        currentLevel++;
        UpdateAbility(currentLevel);
        return currentLevel < maxLevel;
    }

    public abstract void UpdateAbility(int lvl);
    public abstract void Activate();
}
