using UnityEngine;

namespace Triggers
{
    public class CharacterDetectedTrigger : MonoBehaviour
    {
        [SerializeField] private GroundedEnemy groundedEnemy;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Character character))
            {
                groundedEnemy.character = character;
            }
            
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Character character))
            {
                groundedEnemy.character = null;
            }
        }
    }
}