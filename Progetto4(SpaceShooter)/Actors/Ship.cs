using OpenTK;
using Aiv.Fast2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto4_SpaceShooter_
{
    abstract class Ship : GameObject
    {
        protected Vector2 cannonOffset;
        protected float maxEnergy;
        protected float fireCounter;
        protected BulletType bulletType;
        protected float energy;


        public float Energy { get { return energy; } protected set { SetEnergy(value); } }

        public bool IsAlive { get { return Energy > 0; } }
        
        protected virtual void SetEnergy(float newEnergy)
        {
            if (newEnergy < 0)
            {
                newEnergy = 0;
            }
            else if (newEnergy > maxEnergy)
            {
                newEnergy = maxEnergy;
            }

            energy = newEnergy;
        }

        public Ship(Vector2 spritePosition,string textureName) : base(spritePosition, textureName)
        {
            RigidBody = new RigidBody(spritePosition, this);
        }

        public virtual void SetBulletType(BulletType bType)
        {
            bulletType = bType;
        }
        
        public virtual bool AddDamage(float damage)
        {
            Energy -= damage;
            if (Energy<=0)
            {
                OnDie();
                return true;
            }
            return false;
        }

        protected abstract void OnDie();

        public virtual void ResetEnergy()
        {
            Energy = maxEnergy;
        }
        
        public virtual void Reset()
        {
            IsActive = true;
            ResetEnergy();
            
        }

        public override void Update()
        {
            //update ship position
            base.Update();

            //update cooldown
            if (fireCounter > 0)
            {
                fireCounter -= Game.DeltaTime;
            }

        }

        protected bool Shoot(BulletType type)
        {
           Bullet bullet = BulletsMgr.GetBullet(type);
           if( bullet!=null)
           {
                Vector2 bulletPos = sprite.position + cannonOffset;
                bullet.Shoot(bulletPos, this);
                return true;
           }
           return false;
        }

    }
}
