using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Progetto4_SpaceShooter_
{
    class Enemy209 : Enemy
    {
        public Enemy209(Vector2 spritePosition) : base(spritePosition, "enemy1")
        {
            Type = EnemyType.Enemy209;
            bulletType = BulletType.RedLaser;
        }
    }
}
