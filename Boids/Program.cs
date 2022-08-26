using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace Progetto8_Boids_
{
    class Program
    {
        private static float spawnCounter;
        private  const float spawnDelay = 0.3f;


        public static Window Window;
        public static float DeltaTime { get { return Window.deltaTime; } }

        public static List<Boid> Boids;

        

        static void Main(string[] args)
        {
            Window = new Window(1280, 720, "Boids", false);
            Boids = new List<Boid>();

            while (Window.opened)
            {
                Window.SetVSync(true);

                if (Window.GetKey(KeyCode.Esc))
                {
                    return;
                }
                
                spawnCounter -= DeltaTime;
                if (Window.mouseLeft && spawnCounter <= 0)
                {
                    //Input
                    Boids.Add(new Boid(Window.mousePosition));
                    spawnCounter = spawnDelay;
                }

                for (int i = 0; i < Boids.Count; i++)
                {
                    //Update
                    Boids[i].Update();
                }

                for (int i = 0; i < Boids.Count; i++)
                {
                    //Draw
                    Boids[i].Draw();
                }

                Window.Update();
            }
        }
    }
}
