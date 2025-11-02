using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController
{
    public static Action OnGameWin;
    public static Action OnNextLevelLoad = GameWin;
    public static Action OnGameLose = GameLose;
    public static Action OnGameStart = GameStart;
    public static Action OnGameRestart = GameRestart;
    public static Action OnMainMenuOpened;
    public static int Difficulty;
    public static int gameRuned = 0;
    public static int levelsComplite = 0;
    public static bool CanPlay;
    public static float SceneObjectEventDelay { get; set; } = 0.7f;
    public static int Score;
    public static GameType gameType = GameType.Regular;

    private static void GameStart()
    {
        CanPlay = true;
    }

    private static void GameWin()
    {
        levelsComplite++;
    }

    private static void GameLose()
    {
        CanPlay = false;
        Difficulty = 0;
        gameRuned++;
        CanPlay = false;
    }

    private static void GameRestart()
    {
        Score = 0;
        levelsComplite = 0;
    }
}

[Serializable]
public enum GameType
{
    Regular, TimeAttack
}