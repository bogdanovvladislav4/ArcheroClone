using System.Collections;
using System.Collections.Generic;
using Guns;
using Interfeces;
using UnityEngine;

public class Rifle : MonoBehaviour, IGun
{
    [SerializeField] private List<ParticleSystem> shotParticles;

    [SerializeField] private Light shotLight;
    [SerializeField] private GameObject startBulletPosition;

    public void ShotEffects()
    {
        foreach (var shotParticleSystem in shotParticles)
        {
            shotParticleSystem.gameObject.SetActive(true);
        }
        shotLight.gameObject.SetActive(true);
    }

    public GameObject GetStartBulletPos()
    {
        return startBulletPosition;
    }

    public void StopShotEffects()
    {
        foreach (var shotParticleSystem in shotParticles)
        {
            shotParticleSystem.gameObject.SetActive(false);
        }
        shotLight.gameObject.SetActive(false);
    }
}
