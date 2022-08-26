using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Progetto4_SpaceShooter_
{
    class Rect: Shape
    {
        public float Width { get; private set; }
        public float Height { get; private set; }

        public float HalfWidth { get { return Width * 0.5f;  } }
        public float HalfHeight { get { return Height * 0.5f; } }


        public Rect(Vector2 offset, RigidBody rigidBody, float width, float height) 
            : base(offset, rigidBody)
        {
            Width = width;
            Height = height;
        }

        public bool Collides(Rect other)
        {
            float deltaX = Math.Abs(other.Position.X - Position.X);
            float deltaY = Math.Abs(other.Position.Y - Position.Y);
            return deltaX <= HalfWidth + other.HalfWidth && deltaY <= HalfHeight + other.HalfHeight;
        }

        public bool Collides(Circle circle)
        {
            float left = Position.X - HalfWidth;
            float right = Position.X + HalfWidth;
            float top = Position.Y - HalfHeight;
            float bottom = Position.Y + HalfHeight;

            //searching for the nearest point to the circle center
            float nearestX = Math.Max(left, Math.Min(circle.Position.X, right));
            float nearestY = Math.Max(top, Math.Min(circle.Position.Y, bottom));

            //check collision

            float deltaX = circle.Position.X - nearestX;
            float deltaY = circle.Position.Y - nearestY;

            return ((deltaX * deltaX + deltaY * deltaY) <= circle.Radius * circle.Radius);
        }

        public override bool Contains(Vector2 p)
        {
            return p.X >= Position.X - HalfWidth && p.X <= Position.X + HalfWidth &&
                   p.Y <= Position.Y + HalfHeight && p.Y >= Position.Y - HalfHeight;
        }
    }
}
