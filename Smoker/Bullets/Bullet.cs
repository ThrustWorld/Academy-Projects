using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace Progetto6_Smoker_
{
    class Bullet : GameObject
    {
        protected float damage = 25;

        public BulletType Type { get; protected set; }

        public Actor Owner { get; set; }


        public Bullet(string textureName, bool createRect=true) : base(Vector2.Zero, textureName, DrawLayer.Foreground)
        {
            RigidBody = new RigidBody(Vector2.Zero, this,null,null,createRect);
            RigidBody.AddCollisionType((uint)PhysicsMgr.CollisionType.Tile);
        }

        public virtual void Shoot(Vector2 bulletPosition, Actor shooter)
        {
            RigidBody.Position = bulletPosition;
            Owner = shooter;
            IsActive = true;
        }

        public virtual void Shoot(Vector2 bulletPosition, Actor shooter, Vector2 shootVelocity)
        {
            velocity = shootVelocity;
            Shoot(bulletPosition, shooter);
        }

        public override void Update()
        {
            if (IsActive)
            {
                base.Update();
                Vector2 deltaCamera = RigidBody.Position - CameraMgr.MainCamera.position;
                if (deltaCamera.Length > CameraMgr.HalfDiagonal )
                {
                    // bullet is outside the screen
                    BulletsMgr.RestoreBullet(this);
                }
            }
        }

        public override void OnCollide(Collision collisionInfo)
        {
            if(collisionInfo.Collider is Tile)
            {
                if(IsActive)
                    BulletsMgr.RestoreBullet(this);
            }
        }


    }
}
