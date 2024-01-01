using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    public string targetSceneName = "SampleScene";

    public void StartSelectedScene()
    {
        // ³������� ����� �� ������
        SceneManager.LoadScene(targetSceneName);
    }
}
