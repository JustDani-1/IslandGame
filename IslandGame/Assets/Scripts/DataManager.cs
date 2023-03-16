using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameMode
{
    randomSeed,
    setSeed
}

public class DataManager : MonoBehaviour
{
    //data
    public int seed;
    public GameMode mode;

    //singleton

    public static DataManager Instance { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if(Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }
}
