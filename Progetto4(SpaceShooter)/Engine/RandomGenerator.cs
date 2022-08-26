using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto4_SpaceShooter_
{
    static class RandomGenerator
    {
        private static Random random;

        static RandomGenerator()
        {
            random = new Random();
        }

        public static int GetRandom(int min,int max)
        {
            return random.Next(min, max);
        }
    }
}
