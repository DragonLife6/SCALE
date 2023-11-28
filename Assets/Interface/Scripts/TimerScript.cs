using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    float seconds = 0.0f;
    int minutes = 0;
    TMP_Text timerText;

    private void Start()
    {
        seconds = 0f;
        minutes = 0;
        timerText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        seconds += Time.deltaTime;

        if(seconds >= 60)
        {
            seconds = 0.0f;
            minutes++;
        }

        string newText;
        string secondText;

        if(seconds < 10) 
        {
            secondText = "0" + Mathf.FloorToInt(seconds).ToString();
        } else
        {
            secondText = Mathf.FloorToInt(seconds).ToString();
        }

        if (minutes < 10)
        {
            newText = "0" + minutes.ToString() + ":" + secondText;
        }
        else
        {
            newText = minutes.ToString() + ":" + secondText;
        }

        timerText.SetText(newText);
    }

    public string GetCurrentTime()
    {
        return timerText.text;
    }
}
