using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto4_SpaceShooter_
{
    enum BulletType { BlueLaser, RedLaser, FireGlobe, GreenGlobe, Last}


    static class BulletsMgr
    {
        private static int numBullets;

        private static Queue<Bullet>[] bullets;


        public static void Init()
        {
            numBullets = 15;
            bullets = new Queue<Bullet>[(int)BulletType.Last];

            for (int i = 0; i < bullets.Length; i++)
            {
                bullets[i] = new Queue<Bullet>(numBullets);
                for (int j = 0; j < numBullets; j++)
                {
                    switch (i)
                    {
                        case 0:
                            bullets[i].Enqueue(new BlueLaserBullet());
                            break;
                        case 1:
                            bullets[i].Enqueue(new RedLaserBullet());
                            break;
                        case 2:
                            bullets[i].Enqueue(new FireGlobeBullet());
                            break;
                        case 3:
                            bullets[i].Enqueue(new GreenGlobeBullet());
                            bullets[i].Enqueue(new GreenGlobeBullet());
                            break;
                    }
                }
            }


        }

        public static Bullet GetBullet(BulletType type)
        {
            int queueIndex = (int)type;
            if(bullets[queueIndex].Count > 0)
            {
                return bullets[queueIndex].Dequeue();
            }
            return null;
            
        }

        public static void RestoreBullet(Bullet bullet)
        {
            bullet.IsActive = false;
            bullets[(int)bullet.Type].Enqueue(bullet);
        }
    }



   
}
