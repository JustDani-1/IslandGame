using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour
{
    
    private string SceneName = "MenuScene";
    
    public void loadScene()
    {
        SceneManager.LoadScene(SceneName);
    }
}
