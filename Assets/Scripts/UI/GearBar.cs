using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GearBar : MonoBehaviour
{
    [SerializeField] private List<Image> gears;
    [SerializeField] Color regularColor;
    [SerializeField] Color colectedColor;

    private void Awake()
    {
        LevelController.OnSetUp += Setup;
        LevelController.OnItemCollected += CollectGear;
    }

    private void Setup()
    {
        for (int i = 0; i < gears.Count; i++)
        {
            gears[i].gameObject.SetActive(i < LevelController.bitsToCollect);
            gears[i].color = regularColor;
        }
    }

    private void CollectGear()
    {
        for (int i = 0; i < LevelController.bitsCollected; i++)
        {
            if (i < gears.Count)
            {
                gears[i].color = colectedColor;
            }
        }
    }
}
