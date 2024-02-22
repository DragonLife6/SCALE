using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using TMPro;
using UnityEngine;

[System.Serializable]
public class UpgradesData
{
    public int[] paramsUpgradesCount = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
}

public class UpdateMenuScript : MonoBehaviour
{
    string playerDataPath, updatesDataPath;

    [SerializeField] GameObject mainMenuCanvas;
    [SerializeField] TMP_Text balanceText;
    UpgradesData upgradesLoadedData;

    [SerializeField] int[] paramsMaximumUpgrades;

    [SerializeField] int currentBalance;
    [SerializeField] int upgradeCost;

    private void LoadUpgradesData()
    {
        if (File.Exists(updatesDataPath))
        {
            string jsonData = File.ReadAllText(updatesDataPath);
            upgradesLoadedData = JsonUtility.FromJson<UpgradesData>(jsonData);
        }
        else
        {
            upgradesLoadedData = new UpgradesData();
            string json = JsonUtility.ToJson(upgradesLoadedData);
            File.WriteAllText(updatesDataPath, json);

            Debug.Log("Saving!");
        }
    }

    public void ResetUpgrades()
    {
        for (int i = 0; i < 8; i++)
        {
            currentBalance += upgradeCost * upgradesLoadedData.paramsUpgradesCount[i];
            upgradesLoadedData.paramsUpgradesCount[i] = 0;
        }

        UpdateUI();
        SaveUpgradesData();
        SavePlayerData();
    }

    private void SavePlayerData()
    {
        PlayerData playerData = new()
        {
            startLevel = 0,
            maxHealth = 1.0f + upgradesLoadedData.paramsUpgradesCount[0] * 0.1f,
            damage = 1f + upgradesLoadedData.paramsUpgradesCount[1] * 0.1f,
            size = 1f + upgradesLoadedData.paramsUpgradesCount[2] * 0.1f,
            delay = 1f - upgradesLoadedData.paramsUpgradesCount[3] * 0.05f,
            critChance = 0.1f + upgradesLoadedData.paramsUpgradesCount[4] * 0.05f,
            critPower = 1.5f + upgradesLoadedData.paramsUpgradesCount[5] * 0.1f,
            moveSpeed = 1f + upgradesLoadedData.paramsUpgradesCount[6] * 0.1f,
            count = 0 + upgradesLoadedData.paramsUpgradesCount[7],
            expBonus = 0f
        };

        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(playerDataPath, json);


        Debug.Log("Saving Player data!");
    }

    private void SaveUpgradesData()
    {
        PlayerPrefs.SetInt("PlayerBalance", currentBalance);

        string json = JsonUtility.ToJson(upgradesLoadedData);
        File.WriteAllText(updatesDataPath, json);

        Debug.Log("Saving!");
    }

    private void Awake()
    {
        playerDataPath = Path.Combine(Application.persistentDataPath, "playerData.json");
        updatesDataPath = Path.Combine(Application.persistentDataPath, "upgradesData.json");

        currentBalance = PlayerPrefs.GetInt("PlayerBalance");
        LoadUpgradesData();
        UpdateUI();
    }

    public void ReturnPressed()
    {
        SaveUpgradesData();
        SavePlayerData();
        mainMenuCanvas.SetActive(true);
        gameObject.SetActive(false);
    }

    public void AddMoney()
    {
        currentBalance += 1000;
        UpdateUI();
    }

    private void UpdateUI()
    {
        balanceText.text = currentBalance.ToString();
    }

    public void UpgradeCharacterisctic(int buttonNum)
    {
        if (currentBalance >= upgradeCost)
        {
            if (upgradesLoadedData.paramsUpgradesCount[buttonNum] < paramsMaximumUpgrades[buttonNum])
            {
                currentBalance -= upgradeCost;
                UpdateUI();
                upgradesLoadedData.paramsUpgradesCount[buttonNum]++;
                SaveUpgradesData();
                SavePlayerData();
                Debug.Log("Button " + buttonNum + " Lvl: " + upgradesLoadedData.paramsUpgradesCount[buttonNum]);
            } else
            {
                Debug.Log("Max level!");
            }
        }
    }
}
