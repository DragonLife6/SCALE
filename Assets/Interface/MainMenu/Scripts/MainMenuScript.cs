using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    public string targetSceneName = "SampleScene";

    public void StartSelectedScene()
    {
        // Відкриття сцени за назвою
        SceneManager.LoadScene(targetSceneName);
    }
}
