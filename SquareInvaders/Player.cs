using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace Progetto2_SquareInvaders_
{
    class Player
    {
        private Sprite sprite;

        public Vector2 Position;

        //offsets
        public static int HalfWidth;
        public static int CannonOffsetY;

        public bool IsFirePressed;

        public static float SpeedX;
        public float CurrentSpeedX;

        public static Color SpriteColor;

        //Bullets
        public Bullet[] Bullets;

        public int NumLives { get; private set; }
       

        public Player(Vector2 startPos)
        {
            sprite = new Sprite("Assets/player.png");

            NumLives = 3;
            Position = startPos;
            HalfWidth = 40;
            CannonOffsetY = -(sprite.height + 5);
            SpriteColor = new Color(0, 0, 200);
            SpeedX = 800;
            CurrentSpeedX = 0;
            IsFirePressed = false;
            

            //bullets
            Bullets = new Bullet[2];

            for (int i = 0; i < Bullets.Length; i++)
            {
                Bullets[i] = new Bullet();
            }
        }

        public bool OnHit()
        {
            NumLives--;
            if(NumLives == 0)
            {
                return true;
            }
            return false;
        }

        public void Draw()
        {

            //Gfx.DrawRect((int)Position.X - HalfWith, (int)Position.Y - HalfBaseHeight, HalfWith * 2, HalfBaseHeight * 2, SpriteColor);
            //Gfx.DrawRect((int)Position.X - HalfCannonWidth, (int)Position.Y - HalfBaseHeight - CannonHeight, HalfCannonWidth * 2, CannonHeight, SpriteColor);

            Gfx.DrawSprite(sprite, (int)Position.X - sprite.width / 2, (int)Position.Y - sprite.height);

            //draw active bullets
            for (int i = 0; i < Bullets.Length; i++)
            {
                if (Bullets[i].IsActive)
                    Bullets[i].Draw();
            }
        }

        public void Update()
        {
            if (CurrentSpeedX != 0)
            {
                Position.X += CurrentSpeedX * Gfx.Window.deltaTime;

                if (Position.X + HalfWidth >= Gfx.Window.width - Gfx.ScreenEdge)
                {
                    Position.X = Gfx.Window.width - HalfWidth - Gfx.ScreenEdge;
                }
                else if (Position.X - HalfWidth < Gfx.ScreenEdge)
                {
                    Position.X = HalfWidth + Gfx.ScreenEdge;
                }
            }

            //bullets update
            for (int i = 0; i < Bullets.Length; i++)
            {
                if (Bullets[i].IsActive)
                {
                    Bullets[i].Update();

                    if (Bullets[i].IsActive)
                    {
                        //it's still active
                        //handle collision with alien
                        if (Enemies.CheckAliensBulletsCollision(Bullets[i]))
                        {
                            Bullets[i].IsActive = false;

                            //TODO:
                            //if collides player gains points
                        }
                    }
                }
            }
        }

        public bool Shoot()
        {

            for (int i = 0; i < Bullets.Length; i++)
            {
                if (!Bullets[i].IsActive)
                {
                    Bullets[i].IsActive = true;
                    Bullets[i].Position.X = Position.X;
                    Bullets[i].Position.Y = Position.Y + CannonOffsetY - Bullet.HalfHeight;

                    return true;
                }
            }
            return false;
        }

        public void CheckInput()
        {
            if (Gfx.Window.GetKey(KeyCode.Right))
            {
                CurrentSpeedX = SpeedX;
            }
            else if (Gfx.Window.GetKey(KeyCode.Left))
            {
                CurrentSpeedX = -SpeedX;
            }
            else
            {
                CurrentSpeedX = 0;
            }

            if (Gfx.Window.GetKey(KeyCode.Space))
            {
                if (!IsFirePressed)
                {
                    Shoot();
                    IsFirePressed = true;
                }
            }
            else if (IsFirePressed)
            {
                IsFirePressed = false;
            }

        }
    }
}
