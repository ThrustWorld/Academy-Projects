using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace Progetto4_SpaceShooter_
{
    class Player : Ship
    {
        protected float speed;
        protected float fireDelay = 0.2f;

        protected Bar nrgBar;

        protected TextObject textPoints;

        protected int points;
        public int Points { get { return points; } protected set { points = value; textPoints.Text = points.ToString();  } }

        public Player(Vector2 spritePosition) : base(spritePosition, "player")
        {
            speed = 150;
            cannonOffset = new Vector2(sprite.Width * 0.70f, sprite.Height * 0.3f);
            bulletType = BulletType.BlueLaser;
            RigidBody.CollisionType = PhysicsMgr.CollisionType.Player;

            IsActive = true;

            nrgBar = new Bar(new Vector2(20, 20));

            textPoints = new TextObject(new Vector2(nrgBar.Position.X + nrgBar.Width + 15, nrgBar.Position.Y), "0", FontMgr.GetFont("stdFont"));

            maxEnergy = 100;
            ResetEnergy();



        }

        protected override void SetEnergy(float newEnergy)
        {
            base.SetEnergy(newEnergy);
            nrgBar.Scale(energy / maxEnergy);
        }

        private void FixPosition()
        {
            if (RigidBody.Position.X - sprite.pivot.X < 0)//went out to the left
            {
                RigidBody.Position.X = sprite.pivot.X;
            }
            else if (RigidBody.Position.X + sprite.pivot.X >= Game.Window.Width)//went out to the right
            {
                RigidBody.Position.X = Game.Window.Width - sprite.pivot.X;
            }

            if (RigidBody.Position.Y - sprite.pivot.Y < 0)//went out to the top
            {
                RigidBody.Position.Y = sprite.pivot.Y;
            }
            else if (RigidBody.Position.Y + sprite.pivot.Y >= Game.Window.Height)//went out to the bottom
            {
                RigidBody.Position.Y = Game.Window.Height - sprite.pivot.Y;
            }
        }

        public override void Update()
        {
            if (IsActive)
            {
                //check screen boundaries
                FixPosition();
                base.Update();

                Console.WriteLine(Energy);
            }

        }

        protected bool TripleShoot()
        {
            Bullet[] bullets = new GreenGlobeBullet[3];

            for (int i = 0; i < bullets.Length; i++)
            {
                Bullet bullet = BulletsMgr.GetBullet(BulletType.GreenGlobe);

                if (bullet == null)
                {
                    for (int j = 0; j < i; j++)
                    {
                        BulletsMgr.RestoreBullet(bullets[j]);
                    }

                    return false;
                }

                bullets[i] = bullet;
            }

            Vector2 bulletPos = sprite.position + cannonOffset;
            Vector2 shootingVel = new Vector2(500, -400);

            for (int i = 0; i < bullets.Length; i++)
            {
                bullets[i].Shoot(bulletPos, this, shootingVel);
                shootingVel.Y += 400;
            }

            return true;
        }

        public void Input()
        {
            //horizontal inputs
            if (Game.Window.GetKey(KeyCode.D))
            {
                //Right
                RigidBody.SetXVelocity(speed);
            }
            else if (Game.Window.GetKey(KeyCode.A))
            {
                //Left
                RigidBody.SetXVelocity(-speed);
            }
            else
            {
                RigidBody.SetXVelocity(0);
            }

            //vertical inputs
            if (Game.Window.GetKey(KeyCode.W))
            {
                //Up
                RigidBody.SetYVelocity(-speed);
            }
            else if (Game.Window.GetKey(KeyCode.S))
            {
                //Down
                RigidBody.SetYVelocity(speed);
            }
            else
            {
                RigidBody.SetYVelocity(0);
            }

            if (Game.Window.GetKey(KeyCode.Space) && fireCounter <= 0)
            {
                bool hasShot = false;
                if (bulletType != BulletType.GreenGlobe)
                {
                    hasShot = Shoot(bulletType);
                }
                else
                {
                    hasShot = TripleShoot();
                }

                if (hasShot)
                {
                    fireCounter = fireDelay;
                }
            }
            /*else if (Game.Window.GetKey(KeyCode.M) && fireCounter <= 0)
            {
                if (TripleShoot())
                {
                    fireCounter = fireDelay;
                }
            }*/

            //if(Game.Window.GetKey(KeyCode.Space))
            //{
            //    if (!isFirePressed)
            //    {
            //        Shoot();
            //        isFirePressed = true;
            //    }


            //}
            //else if(isFirePressed)
            //{
            //    isFirePressed = false;
            //}
        }

        protected override void OnDie()
        {
            IsActive = false;
        }

        public void AddPoints(int amount)
        {
            Points += amount;
        }

    }
}
