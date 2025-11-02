using System;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleObject : MonoBehaviour
{
    [SerializeField] private GameObject particleSystem;
    public Action OnCollected;

    private void Start()
    {
        OnCollected += CreateParticles;
    }

    private void CreateParticles()
    {
        GameObject particles = Instantiate(particleSystem);
        particles.transform.position = transform.position;
    }

    public void Collect()
    {
        OnCollected.Invoke();
    }
}
