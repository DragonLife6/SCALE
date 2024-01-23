using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RestartCanvasScript : MonoBehaviour
{
    [SerializeField] int maxTime = 10;
    [SerializeField] TMP_Text timerText;
    [SerializeField] GameObject deathScreen;
    [SerializeField] PlayerHealth player;
    int currentTime;

    private void Start()
    {
        currentTime = maxTime;
        timerText.text = currentTime.ToString();

        Invoke(nameof(StartWithDelay), 1f);
    }

    private void StartWithDelay()
    {
        StartCoroutine(UpdateTimer());
    }

    public void OnConfirmPressed()
    {
        // show advertisement

        StopAllCoroutines();
        gameObject.SetActive(false);
        player.Respawn(true);
    }

    public void OnCancelPressed()
    {
        deathScreen.SetActive(true);
        gameObject.SetActive(false);
        player.Respawn(false);
    }

    private IEnumerator UpdateTimer()
    {
        while (true)
        {
            currentTime--;

            if (currentTime < 0)
            {
                OnCancelPressed();
                break;
            }

            timerText.text = currentTime.ToString();

            yield return new WaitForSeconds(1f);
        }
    }
}
