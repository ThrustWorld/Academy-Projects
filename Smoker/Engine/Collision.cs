using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
namespace Progetto6_Smoker_
{
    enum CollisionType
    {
        None,
        RectsIntersection
    }
    struct Collision
    {
        public Vector2 Delta;
        public GameObject Collider;
        public CollisionType Type;
    }
}
