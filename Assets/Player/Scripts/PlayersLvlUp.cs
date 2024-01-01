using System;
using System.Collections.Generic;
using UnityEngine;


public class PlayersLvlUp : MonoBehaviour
{
    int currentPlayerLevel;
    [SerializeField] float expForLevel;
    float currentExp;

    [SerializeField] GameObject[] passiveSpellPrefabs;
    [SerializeField] GameObject[] activeSpellPrefabs;
    List<AbilityBaseScript> allSpells= new List<AbilityBaseScript>();

    [SerializeField] UI_SliderScript ui_expirienceBar;
    [SerializeField] GameObject abilitiesManager;
    AbilitiesManagerScript abilitiesManagerScript;

    private PlayerHealth playerHealth;


    private void Start()
    {
        currentExp = 0;
        ui_expirienceBar.UpdateSlider(currentExp, expForLevel);

        abilitiesManagerScript = abilitiesManager.GetComponent<AbilitiesManagerScript>();
        playerHealth = GetComponent<PlayerHealth>();
        currentPlayerLevel = 1;
        foreach (var spell in activeSpellPrefabs)
        {
            allSpells.Add(Instantiate(spell, abilitiesManager.transform).GetComponent<AbilityBaseScript>());
        }
        foreach (var spell in passiveSpellPrefabs)
        {
            allSpells.Add(Instantiate(spell, abilitiesManager.transform).GetComponent<AbilityBaseScript>());
        }

        Shuffle(allSpells);
    }

     public void PlayerLevelUp()
    {
        if (playerHealth.isAlive)
        {
            currentPlayerLevel++;

            if (allSpells.Count < 3)
            {
                Debug.Log("Max Level");
                return;
            }
            AbilityBaseScript[] spells = { allSpells[0], allSpells[1], allSpells[2] };
            abilitiesManagerScript.ShowLevelUpMenu(spells);
        }
    }

    public void PlayerResetLevel(float procent)
    {
        currentExp = procent * expForLevel;
        ui_expirienceBar.UpdateSlider(currentExp, expForLevel);
        Shuffle(allSpells);
    }

    public void SkillLevelUp(int skillId)
    {
        bool maxLevel = allSpells[skillId].LevelUp();
        if(maxLevel)
        {
            allSpells.Remove(allSpells[skillId]);
        }
        Shuffle(allSpells);
    }

    private static void Shuffle<T>(List<T> array)
    {
        int n = array.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);

            T temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }

    public void AddExpirience(float exp)
    {
        currentExp += exp;
        if (currentExp >= expForLevel)
        {
            currentExp = 0;
            expForLevel *= 1.2f;
            PlayerLevelUp();
        }

        ui_expirienceBar.UpdateSlider(currentExp, expForLevel);
    }
}
