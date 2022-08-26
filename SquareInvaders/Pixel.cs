using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace Progetto2_SquareInvaders_
{
    class Pixel
    {
        public Vector2 Position;

        public Vector2 Velocity;
        public int Width { get; private set; }
        public Color SpriteColor;
        public bool IsVisible;

        public Pixel(Vector2 pos, int width, Color color)
        {
            Position = pos;
            Width = width;
            SpriteColor = color;
            IsVisible = true;

        }

        public void Draw()
        {
            Gfx.DrawRect((int)Position.X, (int)Position.Y, Width, Width, SpriteColor);
        }

        public void Draw(Color c)
        {
            Gfx.DrawRect((int)Position.X, (int)Position.Y, Width, Width, c);
        }

        public void Update()
        {
            Velocity.Y += Game.Gravity * Gfx.Window.deltaTime;

            //int alpha= (int)(SpriteColor.A -(255.0f * Gfx.Window.deltaTime));


            //if (alpha <= 0)
            //{
            //    SpriteColor.A = 0;

            //    IsVisible = false;
            //    return;
            //}
            //else
            //{
            //    SpriteColor.A = (byte)alpha;
            //}


            Position.X += Velocity.X * Gfx.Window.deltaTime;
            Position.Y += Velocity.Y * Gfx.Window.deltaTime;

            if (Position.X >= Gfx.Window.width || Position.X + Width < 0 || Position.Y >= Gfx.Window.height)
            {
                IsVisible = false;
            }

        }

        public void Translate(Vector2 translation)
        {
            Position.X += translation.X;
            Position.Y += translation.Y;
        }
    }

}
