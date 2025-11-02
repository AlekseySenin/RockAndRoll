using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorldSceneData 
{
    public List<SceneData> sceneDatas;
    public int NumberOfChapters;
    public int CurrentSceneDataIndex;

    public void UpdateData(SceneData sceneData)
    {
        SceneData data = sceneDatas.Find(x => ((x.sceneIndex == sceneData.sceneIndex)));
        if (data != null)
        {
            data.Set(sceneData);
        }
    }

}
