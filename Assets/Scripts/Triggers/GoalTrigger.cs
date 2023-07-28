using System;
using UnityEngine;

namespace Triggers
{
    public class GoalTrigger : MonoBehaviour
    {

        public GroundEnemyAI groundedEnemyAi;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out GroundedEnemy groundedEnemy))
            {
                if (groundedEnemy.Equals(groundedEnemyAi.GetGroundedEnemy()))
                {
                    groundedEnemyAi.goalTrigger = this;
                    Debug.Log("Enter in goal trigger");
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out GroundedEnemy groundedEnemy))
            {
                if (groundedEnemy.Equals(groundedEnemyAi.GetGroundedEnemy()))
                {
                    groundedEnemyAi.goalTrigger = null;
                }
            }
        }
    }
}