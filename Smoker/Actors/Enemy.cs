using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Progetto6_Smoker_
{
    enum EnemyType { FollowEnemy, ShootingEnemy, Last }

    abstract class Enemy : Actor
    {
        protected Actor target;

        public EnemyType Type { get; protected set; }

        public int Value { get; protected set; }

        public uint ID { get; set; }

        public Enemy(Vector2 spritePosition, string textureName="player", int w=0, int h=0) : base(spritePosition, textureName, w, h)
        {
            cannonOffset = new Vector2(-sprite.Width * 0.70f, sprite.Height * 0.3f);
            sprite.FlipX = true;
            velocity = new Vector2(-90, 0);

            RigidBody.CollisionType = PhysicsMgr.CollisionType.Enemy;
            RigidBody.AddCollisionType((uint)PhysicsMgr.CollisionType.Player);
            RigidBody.AddCollisionType((uint)PhysicsMgr.CollisionType.Enemy);
            RigidBody.IsGravityAffected = true;

            ResetFireCounter();

            maxEnergy = 100;
            ResetEnergy();

            Value = 5;

            target = ((PlayScene)Game.CurrentScene).Players[0];

        }

        public override void Update()
        {
            if (IsActive)
            {
                base.Update();
                Vector2 deltaCamera = RigidBody.Position - CameraMgr.MainCamera.position;
                if (deltaCamera.Length > CameraMgr.HalfDiagonal)
                {
                    SpawnMgr.Restore(this);
                    return;
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
            RigidBody.IsGravityAffected = true;
        }
        
        protected override void OnDie()
        {
            SpawnMgr.Restore(this);
        }

        public override void OnCollide(Collision collisionInfo)
        {
            base.OnCollide(collisionInfo);

            if(collisionInfo.Collider is Player)
            {
                ((Player)collisionInfo.Collider).AddDamage(20);
                OnDie();
            }
            else if(collisionInfo.Collider is Enemy)
            {
                if (((Enemy)collisionInfo.Collider).ID < ID)
                {
                    OnWallCollides(collisionInfo);
                }
            }
        }
    }
}
