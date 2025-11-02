using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SceneData
{
    public int sceneIndex;
    public bool isOpened;
    public int gearsToColect;
    public int gearsColected;
    public int chapterIndex;
    public int relativeSceneIndex;

    public void Set(SceneData other)
    {
        sceneIndex = other.sceneIndex;
        isOpened = other.isOpened;
        gearsToColect = other.gearsToColect;
        gearsColected = other.gearsColected;
    }
}
