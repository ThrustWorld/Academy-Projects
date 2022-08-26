using OpenTK;
using Aiv.Fast2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto6_Smoker_
{
    abstract class Actor : GameObject
    {
        protected float speed = 300;
        protected Vector2 cannonOffset;
        protected float maxEnergy;
        protected float fireCounter;
        protected float jumpCounter;
        protected BulletType bulletType;
        protected float energy;
        protected float bulletSpeed = 600;


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

        public Actor(Vector2 spritePosition,string textureName, int w = 0, int h = 0) : base(spritePosition, textureName, width: w, height: h)
        {
            RigidBody = new RigidBody(spritePosition, this);
            RigidBody.IsGravityAffected = true;
            RigidBody.AddCollisionType((uint)PhysicsMgr.CollisionType.Tile);

        }

        protected virtual void FixPosition()
        {
            if (RigidBody.Position.Y + sprite.pivot.Y >= Game.Window.Height)//went out to the bottom
            {
                //place actor to the bottom of the screen
                RigidBody.Position.Y = Game.Window.Height - sprite.pivot.Y;
                RigidBody.IsGravityAffected = false;
                RigidBody.SetYVelocity(0);
            }
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
            FixPosition();

            base.Update();

            //update cooldown
            if (fireCounter > 0)
            {
                fireCounter -= Game.DeltaTime;
            }

            if (jumpCounter > 0)
            {
                jumpCounter -= Game.DeltaTime;
            }
        }

        //protected virtual bool Shoot(BulletType type)
        //{
        //   Bullet bullet = BulletsMgr.GetBullet(type);
        //   if( bullet!=null)
        //   {
        //        Vector2 bulletPos = sprite.position + cannonOffset;
        //        bullet.Shoot(bulletPos, this);
        //        return true;
        //   }
        //   return false;
        //}

        public override void OnCollide(Collision collisionInfo)
        {
            if(collisionInfo.Collider is Tile)
            {
                OnWallCollides(collisionInfo);
            }

        }

        protected virtual void OnWallCollides(Collision collisionInfo)
        {
            if(collisionInfo.Delta.X < collisionInfo.Delta.Y)
            {
                //horizontal collision
                if(RigidBody.Position.X<collisionInfo.Collider.Position.X)
                {
                    collisionInfo.Delta.X = -collisionInfo.Delta.X;
                }
                Position = new Vector2(Position.X + collisionInfo.Delta.X, Position.Y);
                RigidBody.SetXVelocity(0);
            }
            else
            {
                //vertical collision
                if(RigidBody.Position.Y<collisionInfo.Collider.Position.Y)
                {
                    collisionInfo.Delta.Y = -collisionInfo.Delta.Y;
                }
                Position = new Vector2(Position.X, Position.Y + collisionInfo.Delta.Y);
                RigidBody.SetYVelocity(0);
            }
        }

    }
}
