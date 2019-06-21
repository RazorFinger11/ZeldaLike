using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffFog : MonoBehaviour
{
    ParticleSystem fog;

    // Update is called once per frame
    void Start()
    {
        fog = GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        ParticleSystem.EmissionModule em = fog.emission;
        em.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        ParticleSystem.EmissionModule em = fog.emission;
        em.enabled = false;
    }
}
