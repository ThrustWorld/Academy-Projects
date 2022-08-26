using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Progetto4_SpaceShooter_
{
    class EnemyRed : Enemy
    {
        public EnemyRed(Vector2 spritePosition) : base(spritePosition, "enemy2")
        {
            Type = EnemyType.EnemyRed;
            cannonOffset.Y = 0;
            bulletType = BulletType.FireGlobe;

            Value = 15;
        }
    }
}
