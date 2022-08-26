using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Progetto6_Smoker_
{
    abstract class Shape
    {
        protected RigidBody rigidBody;
        private Vector2 relativePosition;

        public Vector2 Position { get { return rigidBody.Position + relativePosition; } }

        public Shape(Vector2 offset, RigidBody rigidBody)
        {
            this.rigidBody = rigidBody;
            relativePosition = offset;
        }

        public abstract bool Contains(Vector2 point);
    }
}
