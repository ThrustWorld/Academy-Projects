using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto6_Smoker_
{
    class RedGlobeBullet : EnemyBullet
    {
        public RedGlobeBullet() : base("greenGlobe", false)
        {
            Type = BulletType.RedGlobe;
            sprite.SetMultiplyTint(1, 0.1f, 0.2f, 1);
        }
    }
}
