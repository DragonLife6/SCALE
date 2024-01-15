using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using UnityEngine;


public class PlayersLvlUp : MonoBehaviour
{
    string playerDataPath;

    int currentPlayerLevel;
    [SerializeField] float expForLevel;
    float currentExp;
    float currentExpForLevel;
    float expBonusProcent = 0f;
    float basicBonusProcent = 0f;
    float damage, size, delay, critChance, critDamage;
    int count;

    [SerializeField] GameObject[] passiveSpellPrefabs;
    [SerializeField] GameObject[] activeSpellPrefabs;
    List<AbilityBaseScript> allSpells= new();

    [SerializeField] UI_SliderScript ui_expirienceBar;
    [SerializeField] GameObject abilitiesManager;
    AbilitiesManagerScript abilitiesManagerScript;

    private PlayerHealth playerHealth;
    private PlayerMovement playerMovement;

    private void LoadPlayerData()
    {
        if (File.Exists(playerDataPath))
        {
            string json = File.ReadAllText(playerDataPath);
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);

            damage = playerData.damage;
            size = playerData.size;
            delay = playerData.delay;
            critChance = playerData.critChance;
            critDamage = playerData.critPower;
            count = playerData.count;

            playerHealth.SetMaxHealth(playerHealth.maxHealth * playerData.maxHealth);
            playerMovement.moveSpeed *= playerData.moveSpeed;

            basicBonusProcent = playerData.expBonus;

        } else
        {
            PlayerData playerData = new()
            {
                startLevel = 0, // not realised
                maxHealth = 1f,
                damage = 1f,
                expBonus = 0f,
                size = 1f,
                delay = 1f,
                count = 0,
                critChance = 0.1f, // not realised
                critPower = 1.5f, // not realised
                moveSpeed = 1f
            };

            string json = JsonUtility.ToJson(playerData);
            File.WriteAllText(playerDataPath, json);

            Debug.Log("Saving!");
        }
    }

    public (float, float, float, float, float, float, float, float, int, int) SendPlayerData()
    {
        return (playerHealth.GetCurrentMaxHealth(), damage, expBonusProcent + basicBonusProcent, size, delay, critChance, critDamage, playerMovement.moveSpeed, count, currentPlayerLevel);
    }

    private void Start()
    {
        playerDataPath = Path.Combine(Application.persistentDataPath, "playerData.json");
        playerHealth = GetComponent<PlayerHealth>();
        playerMovement = GetComponent<PlayerMovement>();
        LoadPlayerData();

        currentExp = 0;
        currentExpForLevel = expForLevel;
        expBonusProcent = 0;
        ui_expirienceBar.UpdateSlider(currentExp, currentExpForLevel);

        abilitiesManagerScript = abilitiesManager.GetComponent<AbilitiesManagerScript>();
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
        currentExp += exp * (1 + expBonusProcent + basicBonusProcent);
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
