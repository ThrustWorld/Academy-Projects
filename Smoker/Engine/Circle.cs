using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Progetto6_Smoker_
{
    class Circle: Shape
    {

        public float Radius { get; private set; }

        public Circle(Vector2 offset, RigidBody rigidBody, float radius)
            : base(offset, rigidBody)
        {
            Radius = radius;
        }

        public override bool Contains(Vector2 point)
        {
            // check if the point is contained in the circle
            Vector2 dist = Position - point;
            return dist.Length <= Radius;
        }

        public bool Collides(Circle other)
        {
            // find the distance between the circles
            Vector2 dist = Position - other.Position;
            // return if distance <= sum of radiuses
            return dist.Length <= Radius + other.Radius;
        }
    }
}
