using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Progetto4_SpaceShooter_
{
    enum EnemyType { Enemy209, EnemyRed, Last }

    abstract class Enemy : Ship
    {
        public EnemyType Type { get; protected set; }

        public int Value { get; protected set; }

        public Enemy(Vector2 spritePosition, string textureName) : base(spritePosition, textureName)
        {
            cannonOffset = new Vector2(-sprite.Width * 0.70f, sprite.Height * 0.3f);
            sprite.FlipX = true;
            velocity = new Vector2(-90, 0);

            RigidBody.CollisionType = PhysicsMgr.CollisionType.Enemy;
            RigidBody.AddCollisionType((uint)PhysicsMgr.CollisionType.Player);

            ResetFireCounter();

            maxEnergy = 100;
            ResetEnergy();

            Value = 5;

        }

        public override void Update()
        {
            if (IsActive)
            {
                base.Update();

                if (sprite.position.X + sprite.pivot.X < 0)
                {
                    SpawnMgr.Restore(this);
                    return;
                }

                if (fireCounter <= 0 && Shoot(bulletType))
                {
                    ResetFireCounter();
                }
            }
        }
        protected virtual void ResetFireCounter()
        {
            fireCounter = RandomGenerator.GetRandom(1, 3);
        }

        public override void Reset()
        {
            base.Reset();
            ResetFireCounter();
        }
        
        protected override void OnDie()
        {
            SpawnMgr.Restore(this);
        }

        public override void OnCollide(GameObject other)
        {
            if(other is Player)
            {
                ((Player)other).AddDamage(20);
                OnDie();
            }
        }
    }
}
