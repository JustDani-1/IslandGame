using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    LoadGame gameloader = new LoadGame();
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
}
