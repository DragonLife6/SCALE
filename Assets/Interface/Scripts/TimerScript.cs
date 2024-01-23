using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    float seconds = 0.0f;
    int minutes = 0;
    TMP_Text timerText;

    public bool isStopping = false;

    private void Start()
    {
        seconds = 0f;
        minutes = 0;
        timerText = GetComponent<TMP_Text>();
    }


    void Update()
    {
        if (!isStopping)
        {
            seconds += Time.deltaTime;

            if (seconds >= 60)
            {
                seconds = 0.0f;
                minutes++;
            }

            string newText;
            string secondText;

            if (seconds < 10)
            {
                secondText = "0" + Mathf.FloorToInt(seconds).ToString();
            }
            else
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
    }

    public string GetCurrentTime()
    {
        return timerText.text;
    }

    public float GetCurrentSeconds()
    {
        return seconds + minutes * 60f;
    }

    public static string TimeToString(float s)
    {
        float sec = s % 60;
        int m = Mathf.FloorToInt((s - sec) / 60);

        string newText;
        string secondText;

        if (sec < 10)
        {
            secondText = "0" + Mathf.FloorToInt(sec).ToString();
        }
        else
        {
            secondText = Mathf.FloorToInt(sec).ToString();
        }

        if (m < 10)
        {
            newText = "0" + m.ToString() + ":" + secondText;
        }
        else
        {
            newText = m.ToString() + ":" + secondText;
        }

        return newText;
    }
}
