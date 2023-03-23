using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadGame : MonoBehaviour
{
    public TMP_InputField input;

    private string SceneName = "GameScene";
    public void randSeed()
    {
        DataManager.Instance.seed = Random.Range(1, 99999);
        DataManager.Instance.mode = GameMode.randomSeed;
        loadScene();
    }

    public void setSeed()
    {
        DataManager.Instance.seed =int.Parse(input.text); //need to check if input is int
        DataManager.Instance.mode = GameMode.setSeed;
        loadScene();
    }
  

    public void loadScene()
    {
        SceneManager.LoadScene(SceneName);
    }
}
