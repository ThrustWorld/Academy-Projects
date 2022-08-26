using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto2_SquareInvaders_
{
    static class Enemies
    {
        static private Alien[] aliens;
        static private AlienBullet[] alienBullets;
        static private int alienDistX;
        static private int verticalStep = 10;
        static private int numAliens = 32;
        static private int aliensPerRow = 8;

        static public int NumAlives { get; private set; }

        static Enemies()
        {
            Alien.SpeedX = 250;
            aliens = new Alien[numAliens];
            NumAlives = aliens.Length;
            int startX = Gfx.ScreenEdge + 5;
            int posX = startX;
            int posY = 10;
            alienDistX = 10;


            for (int i = 0; i < aliens.Length; i++)
            {
                aliens[i] = new Alien(new Vector2(posX, posY));
                if (i >= numAliens - aliensPerRow)
                {
                    aliens[i].CanShoot = true;

                }
                if ((i + 1) % aliensPerRow != 0)
                {
                    posX += Alien.Width + alienDistX;
                }
                else
                {
                    posX = startX;
                    posY += Alien.Height + alienDistX;
                }

            }

            alienBullets = new AlienBullet[aliensPerRow];
            for (int i = 0; i < alienBullets.Length; i++)
            {
                alienBullets[i] = new AlienBullet();
            }
        }

        public static void Update()
        {
            int xOverflow = 0;
            bool mustReposition = false;
            for (int i = 0; i < aliens.Length; i++)
            {
                if (aliens[i].IsAlive)
                {
                    if (aliens[i].Update(ref xOverflow) && mustReposition == false)
                    {
                        mustReposition = true;
                    }
                }
                else if (aliens[i].IsVisible)
                {//dead but visible
                    aliens[i].UpdatePixels();
                }
            }

            //handle reposition
            if (mustReposition)
            {
                for (int i = 0; i < aliens.Length; i++)
                {
                    if (aliens[i].IsAlive)
                    {
                        aliens[i].Translate(new Vector2(xOverflow, verticalStep));
                    }
                }

                Alien.SpeedX = -Alien.SpeedX;

            }

            //update bullets
            for (int i = 0; i < alienBullets.Length; i++)
            {
                if (alienBullets[i].IsActive)
                {
                    alienBullets[i].Update();
                }
            }
        }

        public static  AlienBullet GetBullet()
        {
            for (int i = 0; i < alienBullets.Length; i++)
            {
                if (!alienBullets[i].IsActive)
                {
                    return alienBullets[i];
                }
            }
            return null;
        }

        public static void CheckPlayerCollision()
        {
            for (int i = 0; i < alienBullets.Length; i++)
            {
                if (alienBullets[i].IsActive)
                {
                    Vector2 playerTobullet = alienBullets[i].Position.Sub(Game.Player.Position);
                    if(playerTobullet.Length <= Player.HalfWidth + AlienBullet.HalfWidth)
                    {
                        //collision
                        alienBullets[i].IsActive = false;
                        if (Game.Player.OnHit())
                        {
                            //he's dead
                            return;
                        }
                    }
                }
            }
        }

        public static bool CheckAliensBulletsCollision(Bullet b)
        {
            //perform collision between every alive aliens and the given bullet

            float alienRadius = Alien.Width / 2;

            for (int i = 0; i < aliens.Length; i++)
            {
                if (aliens[i].IsAlive)
                {
                    Vector2 alienPivot = new Vector2(aliens[i].Position.X + alienRadius, aliens[i].Position.Y + Alien.Height / 2);
                    Vector2 vDistance = alienPivot.Sub(b.Position);
                    if (vDistance.Length <= Bullet.HalfWidth + alienRadius)
                    {
                        aliens[i].OnDie();
                        NumAlives--;
                        Alien.SpeedX += 10 * Math.Sign(Alien.SpeedX);
                        //search prev shooting alien
                        if (aliens[i].CanShoot)
                        {
                            int prevAlienIndex = i;
                            do
                            {
                                prevAlienIndex -= aliensPerRow;
                            }
                            while (prevAlienIndex >= 0 && !aliens[prevAlienIndex].IsAlive);

                            if (prevAlienIndex >= 0)
                            {
                                aliens[prevAlienIndex].CanShoot = true;
                            }
                        }
                        return true;
                    }
                }
            }
            return false;
        }

        public static void Draw()
        {
            for (int i = 0; i < aliens.Length; i++)
            {
                if (aliens[i].IsVisible)
                {
                    aliens[i].Draw();
                }
            }

            //update bullets
            for (int i = 0; i < alienBullets.Length; i++)
            {
                if (alienBullets[i].IsActive)
                {
                    alienBullets[i].Draw();
                }
            }
        }
        public static int GetNumAlives()
        {
            return NumAlives;
        }
    }
}
