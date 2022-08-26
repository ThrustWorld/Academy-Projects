using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto6_Smoker_
{
    enum BulletType { GreenGlobe, RedGlobe, Last }


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
                            bullets[i].Enqueue(new GreenGlobeBullet());
                            break;
                        case 1:
                            bullets[i].Enqueue(new RedGlobeBullet());
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
