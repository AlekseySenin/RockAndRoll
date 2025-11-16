using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class WorldSceneManager : MonoBehaviour
{
    [SerializeField] private WorldSceneData worldSceneData;
    [SerializeField] private static Action<SceneData> OnSceneDataChanged;
    public static WorldSceneManager Instance;
    //public static SceneData currentSceneData { get; private set; }
    public static Action<SceneData> OnSceneOpen;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            GameStarter.OnLevelLoad += SceneLoaded;
        }

        else
        {
            Destroy(this);
        }
        GameController.OnGameWin += LevelComplite;
        OnSceneDataChanged += FinishLevel;
        DontDestroyOnLoad(this);
        WorldSceneData loadedData = SaveSystem.LoadGameData();
        if (loadedData != null)
        {
            worldSceneData = loadedData;
        }
    }

    private void OnDestroy()
    {
            GameStarter.OnLevelLoad -= SceneLoaded;

    }

    private void SceneLoaded(int index)
    {
        SceneData currentSceneData = null;
        currentSceneData = worldSceneData.sceneDatas.Find(x => x.sceneIndex == index);
        if (currentSceneData != null)
        {
            OnSceneOpen.Invoke(currentSceneData);
        }
    }

    public static void ChangeSceneData(SceneData sceneData)
    {
        OnSceneDataChanged.Invoke(sceneData);
    }

    private void FinishLevel(SceneData sceneData)
    {
        worldSceneData.UpdateData(sceneData);
        SceneData nextScene = worldSceneData.sceneDatas.Find(x => x.sceneIndex == sceneData.sceneIndex + 1);
        if (nextScene != null)
        {
            worldSceneData.CurrentSceneDataIndex = worldSceneData.sceneDatas.FindIndex(x => x.sceneIndex == sceneData.sceneIndex + 1);
            nextScene.isOpened = true;
            worldSceneData.UpdateData(nextScene);
        }
        SaveSystem.SaveGameData(worldSceneData);
    }

    private static void LevelComplite()
    {
        SceneData sceneData = new SceneData();
        sceneData.gearsToColect = LevelController.bitsToCollect;
        sceneData.gearsColected = LevelController.bitsCollected;
        sceneData.sceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        sceneData.isOpened = true;
        ChangeSceneData(sceneData);
    }

    public SceneData CurrentSceneData()
    {
        return worldSceneData.sceneDatas[worldSceneData.CurrentSceneDataIndex];
    }

    public List<SceneData> ChaptersData(int index)
    {
        return worldSceneData.sceneDatas.FindAll(x => x.chapterIndex == index);        
    }

    public int NumberOfChapters()
    {
        int resoult = 0;
        foreach (var item in worldSceneData.sceneDatas)
        {
            if (item.chapterIndex > resoult)
            {
                resoult = item.chapterIndex;
            }
        }
        return resoult;
    }
}