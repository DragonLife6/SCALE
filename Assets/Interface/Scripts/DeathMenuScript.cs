using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DeathMenuScript : MonoBehaviour
{
    [SerializeField] TimerScript timer;
    [SerializeField] TMP_Text timerText;

    public void MainMenuPressed()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    // Start is called before the first frame update
    void Start()
    {
        timerText.text = timer.GetCurrentTime();
    }
}
