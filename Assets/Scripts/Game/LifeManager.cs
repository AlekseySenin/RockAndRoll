using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    [SerializeField] private int maxLifeCount;
    [SerializeField] private int lifesOnStart;
    public int lifeCount { get; private set; }
    public static bool CanAddLife;
    public static Action OnLifeReceived;
    public static Action OnLifeTacken;

    public static Action<int> OnLifesChanged;

    public static void ReceiveLife()
    {
        OnLifeReceived?.Invoke();

    }

    public static void TakeLife()
    {
        OnLifeTacken?.Invoke();
    }

    private void Awake()
    {
        OnLifeReceived += AddLife;
        OnLifeTacken += RemoveLife;
        GameController.OnGameStart += ResetLives;
        lifeCount = lifesOnStart;
        CanAddLife = lifeCount < maxLifeCount;


    }

    private void OnDestroy()
    {
        OnLifeReceived -= AddLife;
        OnLifeTacken -= RemoveLife;
        GameController.OnGameRestart -= ResetLives;

    }

    public void AddLife()
    {
        CanAddLife = lifeCount < maxLifeCount;
        if (CanAddLife)
        {
            lifeCount++;
            OnLifesChanged.Invoke(lifeCount);
        }
        CanAddLife = lifeCount < maxLifeCount;
    }

    public void RemoveLife()
    {

        lifeCount--;
        CanAddLife = lifeCount < maxLifeCount;
        OnLifesChanged.Invoke(lifeCount);
        if (lifeCount <= 0)
        {
            GameController.OnGameLose?.Invoke();
        }
    }

    private void ResetLives()
    {
        lifeCount = lifesOnStart;
        OnLifesChanged?.Invoke(lifeCount);
        CanAddLife = lifeCount < maxLifeCount;
    }
}
