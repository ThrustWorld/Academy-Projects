using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto6_Smoker_
{
    abstract class EnemyBullet : Bullet
    {
        public EnemyBullet(string textureName, bool createRect=true) : base(textureName, createRect)
        {   
            RigidBody.CollisionType = PhysicsMgr.CollisionType.EnemyBullet;
            RigidBody.AddCollisionType((uint)PhysicsMgr.CollisionType.Player);

        }

        public override void OnCollide(Collision collisionInfo)
        {
            base.OnCollide(collisionInfo);

            if (!IsActive)
            {
                return;
            }

            if (collisionInfo.Collider is Player)
            {
                BulletsMgr.RestoreBullet(this);
                // Damage the player
                ((Player)collisionInfo.Collider).AddDamage(damage);
            }
        }
    }
}
