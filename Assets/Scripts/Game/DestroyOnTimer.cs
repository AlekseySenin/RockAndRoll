using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTimer : MonoBehaviour
{
    [SerializeField] private float timeToDie;

    void Start()
    {
        Destroy(gameObject, timeToDie);
    }
}
