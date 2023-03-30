using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour
{
    
    private string SceneName = "MenuScene";
    
    public void loadScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneName);
    }
}
