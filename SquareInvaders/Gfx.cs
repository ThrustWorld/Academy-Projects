using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace Progetto2_SquareInvaders_
{
    static class Gfx
    {
       
        public static Window Window { get; private set; }
        public static int ScreenEdge = 20;

        static Gfx()
        {
            Window = new Window(800, 800, "SquareInvaders", PixelFormat.RGB);
        }

        //public static void Init()
        //{
        //    Window = new Window(800, 800, "Square Invaders", PixelFormat.RGB);
        //}

        //public static void PutPixel(int x, int y, byte r, byte g, byte b)
        //{
        //    //if pixel is outside the screen don't draw it
        //    if (x < 0 || y < 0 || x >= Window.width || y >= Window.height)
        //    {
        //        return;
        //    }
        //    //compute pixel index (Red)
        //    int index = 3 * (y * Window.width + x);
        //    Window.bitmap[index] = r;
        //    Window.bitmap[index + 1] = g;
        //    Window.bitmap[index + 2] = b;
        //}

        public static void PutPixel(int x, int y, byte r, byte g, byte b, byte a = 255)
        {
            if (x < 0 || x >= Window.width || y < 0 || y >= Window.height)
            {
                return;
            }

            // converto to 0..1 domain
            float alpha = a / 255.0f;
            float invAlpha = 1 - alpha;

            int index = (y * Window.width + x) * 3;

            // alpha blending
            Window.bitmap[index] = (byte)((r * alpha) + (Window.bitmap[index] * invAlpha));
            Window.bitmap[index + 1] = (byte)((g * alpha) + (Window.bitmap[index + 1] * invAlpha));
            Window.bitmap[index + 2] = (byte)((b * alpha) + (Window.bitmap[index + 2] * invAlpha));
        }

        public static void DrawSprite(Sprite sprite, int x, int y)
        {
            for (int spriteY = 0; spriteY < sprite.height; spriteY++)
            {
                for (int spriteX = 0; spriteX < sprite.width; spriteX++)
                {
                    int index = (spriteY * sprite.width + spriteX) * 4;
                    byte r = sprite.bitmap[index];
                    byte g = sprite.bitmap[index + 1];
                    byte b = sprite.bitmap[index + 2];
                    byte a = sprite.bitmap[index + 3];
                    PutPixel(x + spriteX, y + spriteY, r, g, b, a);
                }
            }
        }

        public static void DrawHorizontalLine(int x, int y, int width, byte r, byte g, byte b)
        {
            for (int i = 0; i < width; i++)
            {
                PutPixel(x + i, y, r, g, b);
            }
        }

        public static void DrawVerticalLine(int x, int y, int height, byte r, byte g, byte b, byte a = 255)
        {
            for (int i = 0; i < height; i++)
            {
                PutPixel(x, y + i, r, g, b, a);
            }
        }

        public static void DrawVerticalLine(int x, int y, int height, Color c)
        {
            DrawVerticalLine(x, y, height, c.R, c.G, c.B);
        }

        public static void DrawHorizontalLine(int x, int y, int width, Color c)
        {
            DrawHorizontalLine(x, y, width, c.R, c.G, c.B);
        }

        public static void DrawRect(int x, int y, int width, int height, Color c)
        {
            for (int i = 0; i < width; i++)
            {
                DrawVerticalLine(x + i, y, height, c.R, c.G, c.B, c.A);
            }
        }

        public static void ClearScreen()
        {
            for (int i = 0; i < Window.bitmap.Length; i++)
            {
                Window.bitmap[i] = 0;
            }
        }
    }
}
