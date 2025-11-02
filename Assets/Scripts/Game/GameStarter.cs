using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    public UnityEngine.UI.Button startButton;
    public Scene scene;
    public static GameStarter Instance;
    private int levelIndex;
    public static Action<int> OnLevelLoad;

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void IsStartingScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void StartScene(int index)
    {
        levelIndex = index < SceneManager.sceneCountInBuildSettings ? index : 1 ;
        
        OnLevelLoad.Invoke(index);
        IsStartingScene(index);
        
    }

    public void StartGame()
    {
        StartScene(1);
    }
}
