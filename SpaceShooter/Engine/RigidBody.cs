using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Progetto4_SpaceShooter_
{
    class RigidBody
    {
        public Vector2 Position;
        public Vector2 Velocity { get; set; }
        public Circle BoundingCircle { get; set; }
        public Rect BoundingBox { get; set; }
        public GameObject GameObject { get; private set; }

        private uint collisionMask;
        public PhysicsMgr.CollisionType CollisionType { get; set; }

        public bool IsGravityAffected { get; set; }
        public bool IsCollisionsAffected { get; set; }

        public RigidBody(Vector2 position, GameObject owner, Circle boundingCircle = null, 
                         Rect boundingBox = null,  bool createBoudingBox=true)
        {
            Position = position;
            GameObject = owner;

            IsCollisionsAffected = true;

            if (boundingCircle == null)
            {
                float radius = (float)Math.Sqrt(owner.Width * owner.Width + owner.Height * owner.Height) * 0.5f;
                boundingCircle = new Circle(Vector2.Zero, this, radius);
            }
            BoundingCircle = boundingCircle;

            if (boundingBox == null && createBoudingBox)
            {
                boundingBox = new Rect(Vector2.Zero, this, GameObject.Width, GameObject.Height);
            }
            BoundingBox = boundingBox;

            PhysicsMgr.AddItem(this);
        }

        public void SetXVelocity(float x)
        {
            Velocity = new Vector2(x, Velocity.Y);
        }

        public void SetYVelocity(float y)
        {
            Velocity = new Vector2(Velocity.X, y);
        }

        public void AddVelocity(float x, float y)
        {
            Velocity += new Vector2(x, y);
        }


        public void SetCollisionMask(uint value)
        {
            collisionMask = value;
        }

        public void AddCollisionType(uint value)
        {
            // CollisionMask = CollisionMask | value;
            collisionMask |= value;
        }

        public bool CheckCollision(uint value)
        {
            return (collisionMask & value) != 0;
        }

        public bool Collides(RigidBody other)
        {
            if (BoundingCircle.Collides(other.BoundingCircle)) 
            {//circles collide
                if (BoundingBox != null && other.BoundingBox != null)
                {//rect vs rect
                    return BoundingBox.Collides(other.BoundingBox);
                }
                else
                {
                    if(BoundingBox != null)
                    {//my rect vs other's circle
                        return BoundingBox.Collides(other.BoundingCircle);
                    }
                    if(other.BoundingBox != null)
                    {//other's rect vs my circle
                        return other.BoundingBox.Collides(BoundingCircle);
                    }
                }
                return true;
            }
            return false;
        }

        public void Update()
        {
            if (IsGravityAffected)
            {
                AddVelocity(0, Game.Gravity * Game.DeltaTime);
            }
            Position += Velocity * Game.DeltaTime;
        }
    }
}
