using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto4_SpaceShooter_
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

        public override void  OnCollide(GameObject other)
        {
            BulletsMgr.RestoreBullet(this);
            if (other is EnemyBullet)
            {
                BulletsMgr.RestoreBullet((EnemyBullet) other);
            }
            else if (other is Enemy)
            {
                if (((Enemy)other).AddDamage(damage))
                {
                    //((PlayScene)Game.CurrentScene).Player.AddPoints(5); //solo se single player
                    ((Player)Owner).AddPoints(((Enemy)other).Value);
                }
            }
        }
    }
}
