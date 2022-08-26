using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto4_SpaceShooter_
{
    static class SpawnMgr
    {
        private static Queue<Enemy>[] enemies;
        private static float enemiesSpawnCounter;

        private static List<PowerUp> powerUps;
        private static float powerUpsSpawnCounter;

        public static void Init()
        {
            enemiesSpawnCounter = RandomGenerator.GetRandom(3, 4);
            int numEnemies = 15;
            enemies = new Queue<Enemy>[(int)EnemyType.Last];
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i] = new Queue<Enemy>(numEnemies);

                for (int j = 0; j < numEnemies; j++)
                {
                    switch (i)
                    {
                        case 0:
                            enemies[i].Enqueue(new Enemy209(Vector2.Zero));
                            break;
                        case 1:
                            enemies[i].Enqueue(new EnemyRed(Vector2.Zero));
                            break;
                    }
                    
                }
            }

            powerUpsSpawnCounter = RandomGenerator.GetRandom(1, 5);
            powerUps = new List<PowerUp>();
            powerUps.Add(new EnergyPowerUp());
            powerUps.Add(new TripleShootPowerUp());
            powerUps.Add(new TripleShootPowerUp());

        }

        public static void Clear()
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].Clear();
            }

            powerUps.Clear();
        }

        
        public static void Update()
        {
            //enemies' spawning
            enemiesSpawnCounter -= Game.DeltaTime;
            if (enemiesSpawnCounter <= 0)
            {
                //must spawn enemy1
                int rand = RandomGenerator.GetRandom(0,100);
                int enemyType = 0;
                if (rand >= 70)
                {
                    enemyType = 1;
                }

                if (enemies[enemyType].Count > 0)
                {
                    enemiesSpawnCounter = RandomGenerator.GetRandom(1, 2);
                    Enemy e = enemies[enemyType].Dequeue();

                    float randomYpos = Game.GetRandomScreenY((int)(e.Height*0.8f));

                    Vector2 randomPos = new Vector2(Game.Window.Width + e.Width*0.5f, randomYpos);
                    e.Position = randomPos;
                    e.Reset();
                    
                }
            }

            powerUpsSpawnCounter -= Game.DeltaTime;
            if(powerUpsSpawnCounter<= 0)
            {
                powerUpsSpawnCounter = RandomGenerator.GetRandom(5, 15);
                if(powerUps.Count>0)
                {
                    int randomIndex = RandomGenerator.GetRandom(0, powerUps.Count);
                    PowerUp p = powerUps[randomIndex];
                    powerUps.RemoveAt(randomIndex);
                    p.Reset();
                    p.Position= new Vector2(Game.Window.Width + p.Width * 0.5f, Game.GetRandomScreenY((int)(p.Height*0.5f)));
                }
            }
        }

        public static void Restore(Enemy item)
        {
            item.IsActive = false;
            enemies[(int)item.Type].Enqueue(item);
        }

        public static void Restore(PowerUp item)
        {
            item.IsActive = false;
            powerUps.Add(item);
        }
    }
}
