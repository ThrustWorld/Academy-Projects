using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace Progetto2_SquareInvaders_
{
    class Alien
    {
        private Pixel[] pixels;
        public int VisiblePixels;
        public static float SpeedX { get; set; }
        private static Color color;
        private static int numColumns;
        private static int numRows;
        private float nextShoot;

        public static int Width { get; private set; }
        public static int Height { get; private set; }
        public Vector2 Position;
        public bool IsVisible;
        public bool IsAlive;
        public bool CanShoot;


        public Alien(Vector2 position)
        {

            Width = 55;
            Height = 40;
            color = new Color(0, 255);
            Position = position;
            IsAlive = true;
            IsVisible = true;
            CanShoot = false;
            numColumns = 11;
            numRows = 8;
            nextShoot = RandomGenerator.GetRandom(1, 15);

            byte[] pixelArr = {  0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, //Disegno dell'alieno
                                 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0,
                                 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0,
                                 0, 1, 1, 0, 1, 1, 1, 0, 1, 1, 0,
                                 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                                 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1,
                                 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1,
                                 0, 0, 0, 1, 1, 0, 1, 1, 0, 0, 0
            };
            int numPixels = 0;
            for (int i = 0; i < pixelArr.Length; i++)
            {
                if (pixelArr[i] == 1) //incremento di un pixel dove la'rray è uguale a 1
                {
                    numPixels++;
                }
            }
            VisiblePixels = numPixels;


            pixels = new Pixel[numPixels];
            int pixelWidth = Width / numColumns;
            Vector2 pixelPos = Position;
            int pixelIndex = 0;
            for (int i = 0; i < pixelArr.Length; i++)
            {
                if (pixelArr[i] == 1)
                {
                    pixels[pixelIndex] = new Pixel(pixelPos, pixelWidth, color);
                    pixelIndex++;
                }

                if ((i + 1) % numColumns != 0)
                {
                    pixelPos.X += pixelWidth;
                }
                else
                {
                    //last column
                    pixelPos.X = Position.X;
                    pixelPos.Y += pixelWidth;
                }

            }

        }

        public void Draw()
        {
            //Gfx.DrawRect((int) Position.X, (int) Position.Y, Width, Height, color);
            for (int i = 0; i < pixels.Length; i++)
            {
                if (CanShoot)
                {
                    pixels[i].Draw(new Color(255));
                }
                else
                {
                    pixels[i].Draw();

                }
            }
        }

        public void OnDie()
        {
            IsAlive = false;
            Vector2 alienCenter = new Vector2(Position.X + Width / 2, Position.Y + Height / 2);
            for (int i = 0; i < pixels.Length; i++)
            {
                Vector2 pixelVel = pixels[i].Position.Sub(alienCenter).Normalized();
                pixelVel.X *= RandomGenerator.GetRandom(80, 650);
                pixelVel.Y *= RandomGenerator.GetRandom(80, 650);

                pixels[i].Velocity = pixelVel;
            }
        }

        public bool Update(ref int xOverflow)
        {
            float deltaX = Gfx.Window.deltaTime * SpeedX;

            Translate(new Vector2(deltaX, 0));

            int maxX = (int)Position.X + Width;
            int maxEdge = Gfx.Window.width - Gfx.ScreenEdge;

            if (maxX >= maxEdge)
            {
                xOverflow = maxEdge - maxX;
                return true;
            }
            else if (Position.X <= Gfx.ScreenEdge)
            {
                xOverflow = Gfx.ScreenEdge - (int)Position.X;
                return true;
            }

            if (CanShoot)
            {
                nextShoot -= Gfx.Window.deltaTime;
                if (nextShoot <= 0)
                {
                    nextShoot = RandomGenerator.GetRandom(1, 15);
                    Shoot();
                }
            }
            return false;
        }

        public bool Shoot()
        {
            AlienBullet bullet = Enemies.GetBullet();

            if (bullet != null)
            {
                Vector2 bulletPos = new Vector2(Position.X + Alien.Width / 2, Position.Y + Alien.Height + 5 + AlienBullet.HalfHeight);
                bullet.Position = bulletPos;
                bullet.IsActive = true;
                return true;
            }
            return false;
        }

        public void UpdatePixels()
        {
            for (int i = 0; i < pixels.Length; i++)
            {
                if (pixels[i].IsVisible)
                {
                    pixels[i].Update();

                    if (!pixels[i].IsVisible)
                    {
                        VisiblePixels--;
                        if (VisiblePixels == 0)
                        {
                            IsVisible = false;
                        }
                    }
                }

            }
        }



        public void Translate(Vector2 translation)
        {
            Position.X += translation.X;
            Position.Y += translation.Y;

            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i].Translate(translation);

            }
        }

    }
}
