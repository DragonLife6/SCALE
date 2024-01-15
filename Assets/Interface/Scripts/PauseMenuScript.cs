using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject confirmDialog;

    [SerializeField] TMP_Text[] dataFields;

    [SerializeField] PlayersLvlUp player;
 
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0.0f;

        var playerData = player.SendPlayerData();

        dataFields[0].text = $"{Mathf.CeilToInt(playerData.Item1)}";
        dataFields[1].text = $"{playerData.Item2:0%}";
        dataFields[2].text = $"{playerData.Item3:0%}";
        dataFields[3].text = $"{playerData.Item4:0%}";
        dataFields[4].text = $"{playerData.Item5:0%}";
        dataFields[5].text = $"{playerData.Item6:0%}";
        dataFields[6].text = $"{playerData.Item7:0%}";
        dataFields[7].text = $"{playerData.Item8:0.0}";
        dataFields[8].text = $"{playerData.Item9}";
        dataFields[9].text = $"{playerData.Item10}";

        pauseMenu.SetActive(true);
    }

    public void SurrenderPressed()
    {
        confirmDialog.SetActive(true);
    }

    public void ConfirmPressed()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }

    public void CancelPressed()
    {
        confirmDialog.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
    }
}
