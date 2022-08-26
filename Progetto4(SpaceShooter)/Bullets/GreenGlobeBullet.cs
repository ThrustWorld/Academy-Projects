using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto4_SpaceShooter_
{
    class GreenGlobeBullet : PlayerBullet
    {
        public GreenGlobeBullet() : base("greenGlobe",false)
        {
            Type = BulletType.GreenGlobe;

            
            velocity = new Vector2(500, 0);
            RigidBody.CollisionType = PhysicsMgr.CollisionType.PlayerBullet;
            RigidBody.AddCollisionType((uint)PhysicsMgr.CollisionType.Enemy);
            RigidBody.AddCollisionType((uint)PhysicsMgr.CollisionType.EnemyBullet);
        }
    }
}
