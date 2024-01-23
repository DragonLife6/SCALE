using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] GameObject optionsCanvas;
    [SerializeField] GameObject upgradeCanvas;

    [SerializeField] TMP_Text maxTimeText;
    [SerializeField] TMP_Text lastTimeText;


    public string targetSceneName = "SampleScene";

    private void Start()
    {
        string maxTime = TimerScript.TimeToString(PlayerPrefs.GetFloat("MaxTime"));
        string lastTime = TimerScript.TimeToString(PlayerPrefs.GetFloat("LastTime"));

        maxTimeText.text = maxTime;
        lastTimeText.text = lastTime;
    }

    public void OptionsPressed()
    {
        optionsCanvas.SetActive(true);
        gameObject.SetActive(false);
    }

    public void UpgradePressed()
    {
        upgradeCanvas.SetActive(true);
        gameObject.SetActive(false);
    }

    public void StartSelectedScene()
    {
        // Відкриття сцени за назвою
        SceneManager.LoadScene(targetSceneName);
    }
}
