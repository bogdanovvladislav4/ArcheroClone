using UnityEngine;

namespace Interfeces
{
    public interface IGun
    {
        public void ShotEffects();

        public void StopShotEffects();

        public GameObject GetStartBulletPos();
    }
}