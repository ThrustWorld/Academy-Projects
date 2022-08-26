using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto4_SpaceShooter_
{
    class BlueLaserBullet : PlayerBullet
    {
        public BlueLaserBullet() : base("blueLaser")
        {
            Type = BulletType.BlueLaser;
        }
    }
}
