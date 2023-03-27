using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    LoadGame gameloader = new LoadGame();
    LoadMenu menuLoader = new LoadMenu();
    public AudioSource soundtrack;

    public void Awake()
    {
        soundtrack.volume = DataManager.Instance.sound;
    }
    public void resetGame()
    {
        if(DataManager.Instance.mode == GameMode.randomSeed)
        {
            gameloader.randSeed();
        }
        if (DataManager.Instance.mode == GameMode.setSeed)
        {
            gameloader.loadScene();
        }
    }

    public void loadMenuScene()
    {
        menuLoader.loadScene();
    }
    
}
