using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace Progetto6_Smoker_
{
    class Player : Actor
    {
        protected float jumpSpeed;
        protected float fireDelay = 0.25f;
        protected float jumpDelay = 1.9f;

        protected bool isJumpPressed;
        protected int joystickIndex = -1;

        protected Bar nrgBar;

        protected TextObject textPoints;

        protected int points;
        public int Points { get { return points; } protected set { points = value; textPoints.Text = points.ToString(); } }

        public int PlayerID { get; protected set; }

        public KeyCode RIGHT;
        public KeyCode LEFT;
        public KeyCode JUMP;
        public KeyCode SHOOT;


        public Player(Vector2 spritePosition, int playerId = 0) : base(spritePosition, "player", 58, 58)
        {
            speed = 300;
            jumpSpeed = 900;
            cannonOffset = new Vector2(sprite.Width * 0.70f, sprite.Height * 0.3f);
            bulletType = BulletType.GreenGlobe;
            RigidBody.CollisionType = PhysicsMgr.CollisionType.Player;

            IsActive = true;

            nrgBar = new Bar(new Vector2(20, 20));

            nrgBar.SetPosition(new Vector2(nrgBar.Position.X + playerId * (nrgBar.Width + 150), nrgBar.Position.Y));

            textPoints = new TextObject(new Vector2(nrgBar.Position.X + nrgBar.Width + 15, nrgBar.Position.Y), "0", FontMgr.GetFont("stdFont"));

            maxEnergy = 100;
            ResetEnergy();

            PlayerID = playerId;
            
            RIGHT = KeyCode.D;
            LEFT = KeyCode.A;
            JUMP = KeyCode.W;
            
            
            //if (PlayerID < Game.ConnectedJoysticks)
            //{
            //    joystickIndex = PlayerID;
            //}
            //else
            //{
            //    RIGHT = KeyCode.D;
            //    LEFT = KeyCode.A;
            //    JUMP = KeyCode.W;
            //}
        }

        protected override void FixPosition()
        {
            base.FixPosition();

            Vector2 cameraDist = RigidBody.Position - CameraMgr.MainCamera.position;

            float maxHorizontalDist = Game.Window.Width * 0.5f - sprite.pivot.X;

            if(Math.Abs(cameraDist.X) > maxHorizontalDist)
            {
                if(Math.Sign(RigidBody.Velocity.X)== Math.Sign(cameraDist.X))
                {
                    RigidBody.SetXVelocity(0);
                }
                RigidBody.Position.X = CameraMgr.MainCamera.position.X + (Math.Sign(cameraDist.X) * maxHorizontalDist);
            }
        }

        public virtual void SetNrgBarPosition(Vector2 newPos)
        {
            nrgBar.SetPosition(newPos);
        }

        protected override void SetEnergy(float newEnergy)
        {
            base.SetEnergy(newEnergy);
            nrgBar.Scale(energy / maxEnergy);
        }

        

        public override void Update()
        {
            if (IsActive)
            {
                //check screen boundaries
                
                base.Update();

                //Console.WriteLine(Position);
            }

        }

        public virtual void Jump()
        {
            RigidBody.SetYVelocity(-jumpSpeed);
            RigidBody.IsGravityAffected = true;
        }

        public void Input()
        {
            if (joystickIndex >= 0)
            {//use joystick

                //JUMP:
                if (Game.Window.JoystickA(joystickIndex))
                {
                    if (!isJumpPressed)
                    {
                        isJumpPressed = true;
                        Jump();
                    }
                }
                else if (isJumpPressed)
                {
                    isJumpPressed = false;
                }

                //MOVEMENTS:

                Vector2 analogDirection = Game.Window.JoystickAxisLeft(joystickIndex);

                RigidBody.SetXVelocity(analogDirection.X * speed);

                if (analogDirection.X > 0.2f)
                {
                    sprite.FlipX = false;
                }
                else if (analogDirection.X < -0.2f)
                {
                    sprite.FlipX = true;
                }

                //SHOOT:

                Vector2 shootDir = Game.Window.JoystickAxisRight(joystickIndex);

                if (shootDir.Length!=0 && fireCounter <= 0)
                {
                    if (Shoot(bulletType, shootDir.Normalized() * bulletSpeed))
                    {
                        textureOffsetX = (int)sprite.Width;
                        fireCounter = fireDelay;
                    }
                }
                else if (shootDir.Length==0.0f)//analog not "pressed"
                {
                    textureOffsetX = 0;
                }

                Console.WriteLine(analogDirection);

                if (Game.Window.JoystickRight(joystickIndex))
                {
                    //Right
                    RigidBody.SetXVelocity(speed);
                    sprite.FlipX = false;
                }
                else if (Game.Window.JoystickLeft(joystickIndex))
                {
                    //Left
                    RigidBody.SetXVelocity(-speed);
                    sprite.FlipX = true;
                }
                /*else
                {
                    RigidBody.SetXVelocity(0);
                }*/
            }
            else
            {//use keyboard
                //horizontal inputs
                if (Game.Window.GetKey(RIGHT))
                {
                    //Right
                    RigidBody.SetXVelocity(speed);
                    sprite.FlipX = false;
                }
                else if (Game.Window.GetKey(LEFT))
                {
                    //Left
                    RigidBody.SetXVelocity(-speed);
                    sprite.FlipX = true;
                }
                else
                {
                    RigidBody.SetXVelocity(0);
                }

                //vertical inputs
                if (Game.Window.GetKey(JUMP) && jumpCounter <= 0)
                {//Up
                    if (!isJumpPressed)
                    {
                        isJumpPressed = true;
                        Jump();
                    }
                    jumpCounter = jumpDelay;
                }
                else if (isJumpPressed)
                {
                    isJumpPressed = false;
                }


                if (Game.Window.mouseLeft && fireCounter <= 0)
                {
                    //world mouse position
                    Vector2 mousePosition = Game.Window.mousePosition + CameraMgr.MainCamera.position - CameraMgr.MainCamera.pivot;
                    Vector2 bulletVelocity = (mousePosition - Position).Normalized() * bulletSpeed;

                    if (Shoot(bulletType, bulletVelocity))
                    {
                        textureOffsetX = (int)sprite.Width;
                        fireCounter = fireDelay;
                    }
                }
                else if (!Game.Window.mouseLeft)
                {
                    textureOffsetX = 0;
                }
            }

        }

        protected virtual bool Shoot(BulletType type, Vector2 bulletVelocity)
        {
            Bullet bullet = BulletsMgr.GetBullet(type);
            if (bullet != null)
            {
                bullet.Shoot(Position, this, bulletVelocity);
                return true;
            }
            return false;
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
