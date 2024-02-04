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
    float damageFromAbilitie, chanceFromAbilitie, powerFromAbilitie;
    int count;

    [SerializeField] GameObject[] passiveSpellPrefabs;
    [SerializeField] GameObject[] activeSpellPrefabs;
    List<AbilityBaseScript> allSpells= new();
    List<AbilityBaseScript> allDeletedSpells = new();

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
                critChance = 0.1f,
                critPower = 1.5f,
                moveSpeed = 1f
            };

            string json = JsonUtility.ToJson(playerData);
            File.WriteAllText(playerDataPath, json);

            Debug.Log("Saving!");
            LoadPlayerData();
        }
    }

    public (float, float, float, float, float, float, float, float, int, int) SendPlayerData()
    {
        return (playerHealth.GetCurrentMaxHealth(), 
            damage * damageFromAbilitie, 
            expBonusProcent + basicBonusProcent, 
            size, 
            delay, 
            critChance + chanceFromAbilitie, 
            critDamage * powerFromAbilitie, 
            playerMovement.moveSpeed, 
            count, 
            currentPlayerLevel);
    }

    public void SetCritDamage(float chance, float power)
    {
        chanceFromAbilitie = chance; // From 0
        powerFromAbilitie = power; // From 1

        UpdateSpells();
    }

    public void SetBonusDamage(float coef)
    {
        damageFromAbilitie = 1f + coef;

        UpdateSpells();
    }

    private void UpdateSpells()
    {
        foreach (var spell in allSpells)
        {
            if (!spell.isPassive)
            {
                spell.SetBaseParams(damage * damageFromAbilitie, size, delay, count, critChance + chanceFromAbilitie, critDamage * powerFromAbilitie);
            }
        }

        if (allDeletedSpells.Count > 0)
        {
            foreach (var spell in allDeletedSpells)
            {
                if (!spell.isPassive)
                {
                    spell.SetBaseParams(damage * damageFromAbilitie, size, delay, count, critChance + chanceFromAbilitie, critDamage * powerFromAbilitie);
                }
            }
        }
    }

    public void StopAll(bool isStopping)
    {
        foreach (var spell in allSpells)
        {
            if (!spell.isPassive)
            {
                spell.StopSpell(isStopping);
            }
        }

        if (allDeletedSpells.Count > 0)
        {
            foreach (var spell in allDeletedSpells)
            {
                if (!spell.isPassive)
                {
                    spell.StopSpell(isStopping);
                }
            }
        }
    }

    private void Start()
    {
        playerDataPath = Path.Combine(Application.persistentDataPath, "playerData.json");
        playerHealth = GetComponent<PlayerHealth>();
        playerMovement = GetComponent<PlayerMovement>();
        LoadPlayerData();

        damageFromAbilitie = 1f;
        chanceFromAbilitie = 0f;
        powerFromAbilitie = 1f;
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
        currentPlayerLevel--;
        currentExpForLevel /= 1.15f;

        currentExp = procent * currentExpForLevel;
        ui_expirienceBar.UpdateSlider(currentExp, currentExpForLevel);
        Shuffle(allSpells);
    }
    public void SkillMaxLevelUp(int skillId, int skillIdVariant)
    {
        allSpells[skillId].OnMaxLevel(skillIdVariant);
        
        allDeletedSpells.Add(allSpells[skillId]);
        allSpells.Remove(allSpells[skillId]);

        Shuffle(allSpells);
    }
    

    public void SkillLevelUp(int skillId)
    {
        bool maxLevel = allSpells[skillId].LevelUp();
        if(maxLevel && allSpells[skillId].isPassive)
        {
            allDeletedSpells.Add(allSpells[skillId]);
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
