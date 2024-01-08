using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] GameObject optionsCanvas;


    public string targetSceneName = "SampleScene";

    public void OptionsPressed()
    {
        optionsCanvas.SetActive(true);
        gameObject.SetActive(false);
    }

    public void StartSelectedScene()
    {
        // Відкриття сцени за назвою
        SceneManager.LoadScene(targetSceneName);
    }
}
