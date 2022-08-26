using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Progetto4_SpaceShooter_
{
    abstract class PowerUp : GameObject
    {
        protected Player attachedPlayer;
        public PowerUp(string textureName) : base(Vector2.Zero, textureName, DrawLayer.Foreground)
        {
            RigidBody = new RigidBody(Position, this, createBoudingBox: false);
            RigidBody.CollisionType = PhysicsMgr.CollisionType.PowerUp;
            RigidBody.AddCollisionType((uint)PhysicsMgr.CollisionType.Player);

            RigidBody.Velocity = new Vector2(-300, 0);
        }

        public virtual void OnAttach(Player p)
        {
            attachedPlayer = p;
        }

        public virtual void OnDetach()
        {
            attachedPlayer = null;
            SpawnMgr.Restore(this);
        }

        public override void Update()
        {
            if (IsActive)
            {
                base.Update();
                if(Position.X+sprite.pivot.X < 0)
                {
                    SpawnMgr.Restore(this);
                }
                else
                {
                    sprite.Rotation -= Game.DeltaTime;
                }
            }
            
        }
        public override void OnCollide(GameObject other)
        {
            OnAttach((Player)other);
        }

        public virtual void Reset()
        {
            IsActive = true;
            sprite.Rotation = 0;
        }
    }
}
