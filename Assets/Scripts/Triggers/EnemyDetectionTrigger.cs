
using UnityEngine;

namespace Triggers
{
    public class EnemyDetectionTrigger : MonoBehaviour
    {
        [SerializeField] private Character character;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out GroundedEnemy enemy))
            {
                character.groundedEnemy = enemy;
            }
           
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out GroundedEnemy enemy))
            {
                character.groundedEnemy = null;
            }
          
        }
    }
}
