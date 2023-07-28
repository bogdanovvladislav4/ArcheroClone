using Interfeces;
using UnityEngine;

namespace Guns
{
    public class Bullet : MonoBehaviour
    {
        private IAttack owner;

        private int damage;

        public void setOwnerBullet(IAttack owner)
        {
            this.owner = owner;
        }

        public void setDamage(int damage)
        {
            this.damage = damage;
        }

        public IAttack getOwner()
        {
            return owner;
        }

        public int getDamage()
        {
            return damage;
        }
    }
}