using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using Aiv.Fast2D;

namespace Progetto4_SpaceShooter_
{
    class GameObject:IUpdatable,IDrawable
    {
        protected Texture texture;
        protected Sprite sprite;
        protected DrawLayer layer;

        public RigidBody RigidBody;
        protected Vector2 velocity
        {
            get
            { 
                if (RigidBody != null)
                {
                    return RigidBody.Velocity;
                }
                return Vector2.Zero;
            }
            set
            {
                if (RigidBody != null)
                {
                    RigidBody.Velocity = value;
                }
            }
        }

        public Vector2 Position
        {
            get
            {
                if (RigidBody != null)
                {
                    return RigidBody.Position;
                }
                return sprite.position;
            }
            set
            {
                sprite.position = value;
                if (RigidBody != null)
                {
                    RigidBody.Position = value;
                }
            }
        }
        public float Width { get { return sprite.Width; } }
        public float Height { get { return sprite.Height; } }
        public bool IsActive { get; set; }

        public DrawLayer Layer { get { return layer; } }


        public GameObject(Vector2 position, string textureName, DrawLayer layer=DrawLayer.Playground, int width=0, int height=0)
        {
            texture = GfxMgr.GetTexture(textureName);
            sprite = new Sprite(width > 0 ? width : texture.Width, height > 0 ? height : texture.Height);
            velocity = Vector2.Zero;
            sprite.position = position;
            sprite.pivot = new Vector2(sprite.Width * 0.5f, sprite.Height * 0.5f);

            this.layer = layer;

            UpdateMgr.AddItem(this);
            DrawMgr.AddItem(this);
        }

        public virtual void Draw()
        {
            if (IsActive)
            {
                sprite.DrawTexture(texture);
            }
        }

        public virtual void Destroy()
        {
            sprite = null;
            texture = null;
            UpdateMgr.RemoveItem(this);
            DrawMgr.RemoveItem(this);
            if(RigidBody != null)
            {
                PhysicsMgr.RemoveItem(RigidBody);
            }
        }

        public virtual void Update()
        {
            if (IsActive && RigidBody != null)
            {
                sprite.position = RigidBody.Position;
            }
        }

        public virtual void OnCollide(GameObject other)
        {
            
        }
    }
}
