using System;
using System.Collections;
using System.Collections.Generic;
using Guns;
using Interfeces;
using UnityEngine;

public class Shotgun : MonoBehaviour, IGun
{
    [SerializeField] private List<ParticleSystem> shotParticles;
    [SerializeField] private GameObject startBulletPosition;
    
    public void ShotEffects()
    {
        foreach (var shotParticle in shotParticles)
        {
            shotParticle.gameObject.SetActive(true);
        }
    }
    
    public GameObject GetStartBulletPos()
    {
        return startBulletPosition;
    }


    public void StopShotEffects()
    {
        foreach (var shotParticle in shotParticles)
        {
            shotParticle.gameObject.SetActive(false);
        }
    }
}
