using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto4_SpaceShooter_
{
    abstract class EnemyBullet : Bullet
    {
        public EnemyBullet(string textureName, bool createRect=true) : base(textureName, createRect)
        {
            velocity = new Vector2(-500, 0);      
            RigidBody.CollisionType = PhysicsMgr.CollisionType.EnemyBullet;
            RigidBody.AddCollisionType((uint)PhysicsMgr.CollisionType.Player);

        }

        public override void OnCollide(GameObject other)
        {
            if (other is Player)
            {
                BulletsMgr.RestoreBullet(this);
                // Damage the player
                ((Player)other).AddDamage(damage);
            }
        }
    }
}
