using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto4_SpaceShooter_
{
    class RedLaserBullet : EnemyBullet
    {
        public RedLaserBullet() : base("blueLaser")
        {
            Type = BulletType.RedLaser;
            sprite.SetMultiplyTint(1, 0.3f, 0.3f, 1);
        }
    }
}
