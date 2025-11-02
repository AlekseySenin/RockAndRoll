using System.Collections;using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private List<GameObject> hearts;
    private void Awake()
    {
        LifeManager.OnLifesChanged += SetHearts;

    }

    private void OnDestroy()
    {
        LifeManager.OnLifesChanged -= SetHearts;
    }

    void SetHearts(int number)
    {
        if (number < hearts.Count)
        {
            for (int i = 0; i < hearts.Count - 1; i++)
            {
                hearts[i].SetActive(i < number);
            }
        }
    }
}
