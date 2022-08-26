using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace Progetto2_SquareInvaders_
{
    static class Game
    {
        private static Sprite heart;


        public static float Gravity = 400;
        public static Player Player { get; private set; }
        public static int Score { get; private set; }

        private static Sprite sprite;

        public static void Init()
        {
            Player = new Player(new Vector2(Gfx.Window.width / 2, Gfx.Window.height - 30));
            heart = new Sprite("Assets/heart.png");
        }

        private static void DrawGUI()
        {
            int posX = 20;
            int posY = Gfx.Window.height - heart.height - 5; 
            for (int i = 0; i < Player.NumLives; i++)
            {
                Gfx.DrawSprite(heart, posX, posY);
                posX += heart.width + 1;
            }
        }

        public static void Play()
        {
            //init game
            Init();

            while (Gfx.Window.opened)
            {
               if(Player.NumLives <= 0)
               {
                    break;
               }
                Gfx.ClearScreen();
                //INPUT
                if (Gfx.Window.GetKey(KeyCode.Esc))
                {
                    return;
                }
                Player.CheckInput();
                //UPDATE    
                Enemies.Update();
                Player.Update();
                Enemies.CheckPlayerCollision();
                //DRAW
                Player.Draw();
                Enemies.Draw();
                DrawGUI();
                Gfx.Window.Blit();
            }
            Console.ReadLine();
        }

        /*public static void Lives()
        {
            int hearts = 3;
            for (int i = 0; i < hearts; i++)
            {
                sprite = new Sprite("Assets/" + i + "heart.png");
            }
        }*/
    }
}
