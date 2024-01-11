using System;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;


public class PlayersLvlUp : MonoBehaviour
{
    int currentPlayerLevel;
    [SerializeField] float expForLevel;
    float currentExp;
    float currentExpForLevel;
    float expBonusProcent;
    float damage, size, delay, critChance, critDamage;
    int count;

    [SerializeField] GameObject[] passiveSpellPrefabs;
    [SerializeField] GameObject[] activeSpellPrefabs;
    List<AbilityBaseScript> allSpells= new();

    [SerializeField] UI_SliderScript ui_expirienceBar;
    [SerializeField] GameObject abilitiesManager;
    AbilitiesManagerScript abilitiesManagerScript;

    private PlayerHealth playerHealth;

    private void LoadPlayerData()
    {
        if (System.IO.File.Exists("playerData.json"))
        {
            string json = System.IO.File.ReadAllText("playerData.json");
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);

            damage = playerData.damage;
            size = playerData.size;
            delay = playerData.delay;
            critChance = playerData.critChance;
            critDamage = playerData.critPower;
            count = playerData.count;

        } else
        {
            PlayerData playerData = new()
            {
                startLevel = 0,
                maxHealth = 100f,
                damage = 1f,
                expBonus = 0f,
                size = 1f,
                delay = 1f,
                count = 0,
                critChance = 0.1f,
                critPower = 1.5f,
                moveSpeed = 1f
            };

            string json = JsonUtility.ToJson(playerData);
            System.IO.File.WriteAllText("playerData.json", json);


            Debug.Log("Saving!");
        }
    }


    private void Start()
    {
        LoadPlayerData();

        Debug.Log("Damage: " + damage);
        Debug.Log("Size: " + size);
        Debug.Log("Delay: " + delay);
        Debug.Log("Crit Chance: " + critChance);
        Debug.Log("Count: " + count);

        currentExp = 0;
        currentExpForLevel = expForLevel;
        expBonusProcent = 0;
        ui_expirienceBar.UpdateSlider(currentExp, currentExpForLevel);

        abilitiesManagerScript = abilitiesManager.GetComponent<AbilitiesManagerScript>();
        playerHealth = GetComponent<PlayerHealth>();
        currentPlayerLevel = 1;
        foreach (var spell in activeSpellPrefabs)
        {
            AbilityBaseScript newSpell = Instantiate(spell, abilitiesManager.transform).GetComponent<AbilityBaseScript>();
            newSpell.SetBaseParams(damage, size, delay, count, critChance, critDamage);
            allSpells.Add(newSpell);
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
        currentExp = procent * currentExpForLevel;
        ui_expirienceBar.UpdateSlider(currentExp, currentExpForLevel);
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
        currentExp += exp * (1 + expBonusProcent);
        if (currentExp >= currentExpForLevel)
        {
            currentExp -= currentExpForLevel;
            currentExpForLevel *= 1.15f;
            if (currentExp < 0)
            {
                currentExp = 0;
            }
            else if (currentExp >= currentExpForLevel)
            {
                AddExpirience(0);
            }
            PlayerLevelUp();
        }

        ui_expirienceBar.UpdateSlider(currentExp, currentExpForLevel);
    }

    public void SetBonusExpirienceProcent(float procent)
    {
        expBonusProcent = procent;
    }
}
