using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Progetto6_Smoker_
{
    class Tile : GameObject
    {
        public Tile(Vector2 position, string textureName="earthTile", DrawLayer layer = DrawLayer.Playground, int width = 0, int height = 0) : base(position, textureName, layer, width, height)
        {
            RigidBody = new RigidBody(position, this);
            RigidBody.CollisionType = PhysicsMgr.CollisionType.Tile;
            IsActive = true;
        }
    }
}
