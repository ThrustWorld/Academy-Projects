using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Progetto6_Smoker_
{
    class ShootingEnemy : Enemy
    {
        protected float minDist = 350;

        public ShootingEnemy(Vector2 spritePosition) : base(spritePosition, "player", 58, 58)
        {
            Type = EnemyType.ShootingEnemy;
            bulletType = BulletType.RedGlobe;

            sprite.SetMultiplyTint(1, 0.2f, 0.2f, 1);
        }

        public override void Update()
        {
            base.Update();
            if (target != null && target.IsActive)
            {
                Vector2 targetDist = target.Position - Position;
                float xVelocity = 0;

                if (targetDist.Length <=minDist)//near, so must stop
                {
                    if (fireCounter <= 0 && Shoot(bulletType))
                    {
                        ResetFireCounter();
                    }
                }
                else//far, so follows target
                {
                    xVelocity = (targetDist).Normalized().X * speed;
                }

                RigidBody.SetXVelocity(xVelocity);


                
            }
            
        }

        protected virtual bool Shoot(BulletType type)
        {
            Bullet bullet = BulletsMgr.GetBullet(type);
            if (bullet != null)
            {
                //world mouse position
                Vector2 bulletVelocity = (target.Position - Position).Normalized() * bulletSpeed;

                bullet.Shoot(Position, this, bulletVelocity);
                return true;
            }
            return false;
        }
    }
}
