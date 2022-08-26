using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Progetto4_SpaceShooter_
{
    class FireGlobeBullet : EnemyBullet
    {
        protected float elapsedTime;//used to store elapsed time in order to use it with Cosine
        protected float ySpeed;
        protected float rotSpeed;
        public FireGlobeBullet() : base("fireGlobe",false)
        {
            Type = BulletType.FireGlobe;
            elapsedTime = 0;
            ySpeed = 540;
            rotSpeed = 5;

            velocity = new Vector2(-500, 0);
            RigidBody.CollisionType = PhysicsMgr.CollisionType.EnemyBullet;
            RigidBody.AddCollisionType((uint)PhysicsMgr.CollisionType.Player);

        }

        public override void Update()
        {
            if (IsActive)
            {
                elapsedTime += Game.DeltaTime;
                RigidBody.SetYVelocity((float)Math.Cos(elapsedTime * 10) * ySpeed);//10 is the multiplier used to modify frequency. ySpeed modifies amplitude

                sprite.Rotation -= Game.DeltaTime * rotSpeed;

                base.Update();
            }
        }

        public override void Shoot(Vector2 bulletPosition, Ship shooter)
        {
            elapsedTime = 0;
            base.Shoot(bulletPosition, shooter);
        }

    }
}
