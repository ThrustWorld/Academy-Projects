using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto6_Smoker_
{
    static class Game
    {
        public static Window Window;
        public static Scene CurrentScene { get; private set; }
        public static float DeltaTime { get { return Window.deltaTime; } }
        public static float Gravity = 980;

        public static int ConnectedJoysticks { get; private set; }

        public static void Init()
        {
            Window = new Window(1200, 700, "Smoker",false);
            PlayScene playScene = new PlayScene();
            playScene.NextScene = null;
            //TitleScene titleScene = new TitleScene("Assets/aivBG.png", KeyCode.Return);
            //GameOverScene gameOverScene = new GameOverScene();
            //titleScene.NextScene = playScene;
            //playScene.NextScene = gameOverScene;
            //gameOverScene.NextScene = playScene;

            CurrentScene = playScene;

            //joysticks config
            for (int i = 0; i < Window.Joysticks.Length; i++)
            {
                if(Window.Joysticks[i] != null)
                {
                    ConnectedJoysticks++;
                }
            }
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
