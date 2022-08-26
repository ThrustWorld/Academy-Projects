using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lander_B
{
    static class Game
    {
        public static Window Window;

        private static Ship ship;
        private static Sprite bg;
        private static Texture bgTexture;

        public static float DeltaTime { get { return Window.deltaTime; } }
        public static float Gravity = 60;

        public static void Init()
        {
            Window = new Window(1000, 800, "Lander");
            ship = new Ship();
            ship.Position = new Vector2(Window.Width / 2, Window.Height / 2);
            bgTexture = new Texture("Assets/mars.jpg");
            bg = new Sprite(bgTexture.Width, bgTexture.Height);
            bg.position = new Vector2(-600, -250);
        }

        public static void Play()
        {
            while (Window.opened)
            {
                if (Window.GetKey(KeyCode.Esc))
                    return;
                //INPUT
                ship.Input();
                //UPDATE
                ship.Update();
                //DRAW
                bg.DrawTexture(bgTexture);
                ship.Draw();

                Window.Update();
            }
        }
    }
}
