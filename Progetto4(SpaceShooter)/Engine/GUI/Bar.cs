using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace Progetto4_SpaceShooter_
{
    class Bar : GameObject
    {
        protected Sprite innerBar;
        protected Texture innerBarTexture;
        protected Vector2 offset;
        protected int barScaledWidth;

        public Bar(Vector2 position, string barTextureName = "barBar", string textureName = "barFrame") : base(position, textureName, DrawLayer.GUI)
        {
            sprite.pivot = Vector2.Zero;
            IsActive = true;

            innerBarTexture = GfxMgr.GetTexture(barTextureName);
            innerBar = new Sprite(innerBarTexture.Width, innerBarTexture.Height);

            //offset = new Vector2((sprite.Width - innerBar.Width) * 0.5f, (sprite.Height - innerBar.Height) * 0.5f);
            offset = new Vector2(4, 4);

            innerBar.position = Position + offset;

            Scale(1);
        }

        public virtual void Scale(float scale)
        {
            innerBar.scale.X = scale;
            barScaledWidth = (int)(innerBarTexture.Width * scale);
        }

        public override void Draw()
        {
            base.Draw();
            innerBar.DrawTexture(innerBarTexture,0,0, barScaledWidth, (int)innerBar.Height);
        }
    }
}
