using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto2_SquareInvaders_
{
    struct Vector2
    {
        public float X;
        public float Y;

        public float Length
        {
            get { return (float)Math.Sqrt(X * X + Y * Y); }
        }

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Vector2 Add(Vector2 v2)
        {
            return new Vector2(X + v2.X, Y + v2.Y);
        }

        public Vector2 Sub(Vector2 v2)
        {
            return new Vector2(X - v2.X, Y - v2.Y);
        }

        public Vector2 Normalized()
        {
            float length = Length;

            return new Vector2(X / length, Y / length);
        }

        public void Normalize()
        {
            float length = Length;
            X /= length;
            Y /= length;
        }
    }
}
