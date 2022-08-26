using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Progetto6_Smoker_
{
    class FollowEnemy : Enemy
    {
        public FollowEnemy(Vector2 spritePosition, string textureName = "player") : base(spritePosition, textureName, 58,58)
        {
            Type = EnemyType.FollowEnemy;
        }

        public override void Update()
        {
            base.Update();

            if (target!=null && target.IsActive)
            {
                Vector2 velocity = (target.Position - Position).Normalized() * speed;

                RigidBody.SetXVelocity(velocity.X);
            }
        }
    }
}
