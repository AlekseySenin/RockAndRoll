using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private int particleCount;

    private void Start()
    {
        if (particles == null)
        {
            particles = GetComponent<ParticleSystem>();
        }
        Player.OnCollision += EmmitInPlace;
    }

    private void OnDestroy()
    {
        Player.OnCollision -= EmmitInPlace;
    }

    void EmmitInPlace(Vector3 position)
    {
        particles.transform.position = position;
        particles.Emit(particleCount);
    }
}
