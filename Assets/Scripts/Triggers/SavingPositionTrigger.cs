using System;
using UnityEngine;

namespace Triggers
{
    public class SavingPositionTrigger : MonoBehaviour
    {
        public GroundedEnemy groundedEnemy;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out GroundedEnemy enemy))
            {
                if (groundedEnemy.Equals(enemy))
                {
                    groundedEnemy.SavingPositionTrigger = this;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out GroundedEnemy enemy))
            {
                if (groundedEnemy.Equals(enemy))
                {
                    groundedEnemy.SavingPositionTrigger = null;
                }
            }
        }
    }
}