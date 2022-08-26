using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace Progetto4_SpaceShooter_
{
    class Bullet : GameObject
    {
        protected float damage = 25;

        public BulletType Type { get; protected set; }

        public Ship Owner { get; set; }


        public Bullet(string textureName, bool createRect=true) : base(Vector2.Zero, textureName, DrawLayer.Foreground)
        {
            RigidBody = new RigidBody(Vector2.Zero, this,null,null,createRect);
        }

        public virtual void Shoot(Vector2 bulletPosition, Ship shooter)
        {
            RigidBody.Position = bulletPosition;
            Owner = shooter;
            IsActive = true;
        }

        public virtual void Shoot(Vector2 bulletPosition, Ship shooter, Vector2 shootVelocity)
        {
            velocity = shootVelocity;
            Shoot(bulletPosition, shooter);
        }

        public override void Update()
        {
            if (IsActive)
            {
                base.Update();
                if (sprite.position.X - sprite.pivot.X >= Game.Window.Width || sprite.position.X + sprite.pivot.X < 0)
                {
                    // bullet is outside the screen
                    BulletsMgr.RestoreBullet(this);
                }
            }
        }


    }
}
