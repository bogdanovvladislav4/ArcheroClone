using Guns;
using UnityEngine;

namespace Triggers
{
    public class HitTriggerCharacter : MonoBehaviour
    {
        [SerializeField] private Character character;
        [SerializeField] private GameObject hitEffectPrefab;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Bullet bullet))
            {
                if (bullet.getOwner().GetType() == typeof(GroundedEnemy))
                {
                    character.TakeDamage(bullet.getDamage());
                    Destroy(bullet.gameObject);
                    GameObject hitEffect = Instantiate(hitEffectPrefab);
                    hitEffect.transform.position = bullet.transform.position;
                    Destroy(bullet.gameObject);
                    Destroy(hitEffect, 5);
                }
            }
        }
    }
}