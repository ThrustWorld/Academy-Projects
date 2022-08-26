using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace Progetto2_SquareInvaders_
{
    class AlienBullet
    {
        public Vector2 Position;
        public static int HalfWidth { get; private set; }
        public static int HalfHeight { get; private set; }
        public static float SpeedY;
        public static Color SpriteColor;

        //sprites
        private Sprite[] frames;
        private Sprite sprite;
        private int numFrames = 2;
        private int frameIndex = 0;
        private float frameDuration = 0.2f;
        private float frameCounter = 0;


        public bool IsActive;

        public AlienBullet()
        {
            Position = new Vector2(0, 0);

            frames = new Sprite[numFrames];

            for (int i = 0; i < numFrames; i++)
            {
                frames[i] = new Sprite("Assets/alienBullet_" + i + ".png");
                Console.WriteLine(i);
            }

            sprite = frames[frameIndex];

            HalfWidth = sprite.width / 2;
            HalfHeight = sprite.height / 2;
            SpeedY = 600;//pixels per second
            SpriteColor = new Color(0, 255);
            IsActive = false;

        }

        public void Draw()
        {
            Gfx.DrawSprite(sprite, (int)Position.X - HalfWidth, (int)Position.Y - HalfHeight);
        }

        /*public static bool CheckAlienBulletsPlayerCollision(Player p)
        {
            float PlayerRadius = Player.HalfWidth * 2;
           if()
        }*/

        public void Update()
        {
            Position.Y += SpeedY * Gfx.Window.deltaTime;

            if (Position.Y - HalfHeight >= Gfx.Window.height)
            {
                IsActive = false;
            }
            else
            {
                frameCounter += Gfx.Window.deltaTime;
                if (frameCounter >= frameDuration)
                {
                    frameCounter = 0;
                    frameIndex++;
                    if (frameIndex == numFrames)
                    {
                        frameIndex = 0;
                    }

                    sprite = frames[frameIndex];
                }
            }
        }
    }
}
