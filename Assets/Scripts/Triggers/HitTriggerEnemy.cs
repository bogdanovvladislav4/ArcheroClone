using Guns;
using UnityEngine;


public class HitTriggerEnemy : MonoBehaviour
{
    [SerializeField] private GroundedEnemy groundedEnemy;
    [SerializeField] private GameObject hitEffectPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Bullet bullet))
        {
            if (bullet.getOwner().GetType() == typeof(Character))
            {
                groundedEnemy.TakeDamage(bullet.getDamage());
                GameObject hitEffect = Instantiate(hitEffectPrefab);
                hitEffect.transform.position = bullet.transform.position;
                Destroy(bullet.gameObject);
                Destroy(hitEffect, 5);
            }
        }
    }
    
}