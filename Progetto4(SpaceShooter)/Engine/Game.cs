using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto4_SpaceShooter_
{
    static class Game
    {
        public static Window Window;
        public static Scene CurrentScene { get; private set; }
        public static float DeltaTime { get { return Window.deltaTime; } }
        public static float Gravity = 60;

        public static void Init()
        {
            Window = new Window(1200, 700, "SpaceShooter",false);
            TitleScene titleScene = new TitleScene("Assets/aivBG.png", KeyCode.Space);
            WelcomeScene welcomeScene = new WelcomeScene();
            PlayScene playScene = new PlayScene();
            GameOverScene gameOverScene = new GameOverScene();
            titleScene.NextScene = welcomeScene;
            welcomeScene.NextScene = playScene;
            playScene.NextScene = gameOverScene;
            gameOverScene.NextScene = playScene;

            CurrentScene = titleScene;
        }

        public static int GetRandomScreenY(int offSet = 0)
        {
            return RandomGenerator.GetRandom(offSet, Window.Height-offSet);

        }

        public static void Play()
        {
            CurrentScene.Start();

            while (Window.opened)
            {
                if(Window.GetKey(KeyCode.Esc))
                    return;
                if (!CurrentScene.IsPlaying)
                {
                    //change scene
                    Scene newScene = CurrentScene.OnExit();

                    GC.Collect();//explicit call to Garbage Collector

                    if(newScene != null)
                    {
                        newScene.Start();
                        CurrentScene = newScene;
                    }
                    else
                    {
                        return;
                    }
                }
                //INPUT
                CurrentScene.Input();
                //UPDATE
                CurrentScene.Update();
                //DRAW
                CurrentScene.Draw();
                
                Window.Update();
            }
        }
    }
}
