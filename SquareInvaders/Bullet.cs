using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace Progetto2_SquareInvaders_
{
    class Bullet
    {
        public Vector2 Position;
        public static int HalfWidth { get; private set; }
        public static int HalfHeight { get; private set; }
        public static float SpeedY;
        public static Color SpriteColor;

        public bool IsActive;

        public Bullet()
        {
            Position = new Vector2(0, 0);
            HalfWidth = 5;
            HalfHeight = 10;
            SpeedY = -380;//pixels per second
            SpriteColor = new Color(200);
            IsActive = false;
        }

        public void Draw()
        {
            Gfx.DrawRect((int)Position.X - HalfWidth,
                (int)Position.Y - HalfHeight, HalfWidth * 2,
                HalfHeight * 2, SpriteColor);
        }

        public void Update()
        {
            Position.Y += SpeedY * Gfx.Window.deltaTime;

            if (Position.Y + HalfHeight < 0)
            {
                IsActive = false;
            }
        }
    }
}
