using Aiv.Fast2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Progetto4_SpaceShooter_
{
    class Background : GameObject
    {
        private Sprite tailSprite;
        private float speedX;

        public Background() : base(Vector2.Zero, "bg", DrawLayer.Background)
        {
            sprite.pivot = Vector2.Zero;
            tailSprite = new Sprite(texture.Width, texture.Height);
            speedX = -300;
            IsActive = true;
        }

        public override void Update()
        {
            sprite.position.X += speedX * Game.DeltaTime;
            tailSprite.position.X = sprite.position.X + sprite.Width;

            if (sprite.position.X < -sprite.Width)
            {
                //head is fully outside
                sprite.position.X = tailSprite.position.X + tailSprite.Width;
                //swap sprites
                Sprite tmp = sprite;
                sprite = tailSprite;
                tailSprite = tmp;

            }
        }

        public override void Draw()
        {
            if (IsActive)
            {
                base.Draw();
                tailSprite.DrawTexture(texture);
            }

        }
    }
}
