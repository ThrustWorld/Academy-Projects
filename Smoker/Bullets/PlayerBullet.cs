using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto6_Smoker_
{
    class PlayerBullet : Bullet
    {
        public PlayerBullet(string textureName = "blueLaser", bool createRect = true) : base(textureName, createRect)
        {
            velocity = new Vector2(500, 0);
            RigidBody.CollisionType = PhysicsMgr.CollisionType.PlayerBullet;
            RigidBody.AddCollisionType((uint)PhysicsMgr.CollisionType.Enemy);
            RigidBody.AddCollisionType((uint)PhysicsMgr.CollisionType.EnemyBullet);

            //RigidBody.SetCollisionMask((uint)PhysicsMgr.CollisionType.Enemy | (uint)PhysicsMgr.CollisionType.EnemyBullet);
        }

        public override void OnCollide(Collision collisionInfo)
        {
            base.OnCollide(collisionInfo);

            if (IsActive)
            {
                BulletsMgr.RestoreBullet(this);
            }            

            if (collisionInfo.Collider is EnemyBullet)
            {
                BulletsMgr.RestoreBullet((EnemyBullet)collisionInfo.Collider);
            }
            else if (collisionInfo.Collider is Enemy)
            {
                if (((Enemy)collisionInfo.Collider).AddDamage(damage))
                {
                    ((Player)Owner).AddPoints(((Enemy)collisionInfo.Collider).Value);
                }
            }
        }
    }
}
