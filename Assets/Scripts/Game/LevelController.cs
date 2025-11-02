using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    public static LevelController Instance;
    public static bool LevelFinished;
    public static int bitsToCollect;
    public static int bitsCollected = 0;

    public static Action OnItemCollected;
    public static Action OnSetUp;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            GameController.OnGameStart += Setup;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Setup()
    {
        bitsToCollect = GameObject.FindObjectsOfType<CollectibleObject>().Length;
        bitsCollected = 0;
        OnSetUp.Invoke();
    }

    public static void CollectItem()
    {
        bitsCollected++;
        OnItemCollected?.Invoke();
    }
}
