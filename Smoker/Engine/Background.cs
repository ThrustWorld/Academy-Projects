using Aiv.Fast2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Progetto6_Smoker_
{
    class Background : GameObject
    {
        private Sprite tailSprite;

        protected Sprite[] sprites;
        protected Texture[] textures;        

        public Background() : base(Vector2.Zero, "bg", DrawLayer.Background)
        {
            sprite.pivot = Vector2.Zero;
            sprite.Camera = CameraMgr.GetCamera("Sky");
            tailSprite = new Sprite(texture.Width, texture.Height);
            tailSprite.position.X = sprite.Width;
            tailSprite.Camera = sprite.Camera;
            IsActive = true;

            textures = new Texture[4];
            sprites = new Sprite[textures.Length*2];

            for (int i = 0; i < textures.Length; i++)
            {
                textures[i] = new Texture("Assets/bg_" + i + ".png");
                sprites[i] = new Sprite(textures[i].Width, textures[i].Height);
                int cloneIndex = i + textures.Length;
                sprites[cloneIndex] = new Sprite(textures[i].Width, textures[i].Height);
                sprites[cloneIndex].position.X = sprites[i].Width;

                if (i >= 2)
                {
                    sprites[i].position.Y = 60;
                    sprites[cloneIndex].position.Y = 60;
                }
                if (i < textures.Length - 1)
                {
                    Camera cam = CameraMgr.GetCamera("Bg_" + i);
                    sprites[i].Camera = cam;
                    sprites[cloneIndex].Camera = cam;
                }
            }


        }

        //public override void Update()
        //{
        //    sprite.position.X += speedX * Game.DeltaTime;
        //    tailSprite.position.X = sprite.position.X + sprite.Width;

        //    if (sprite.position.X < -sprite.Width)
        //    {
        //        //head is fully outside
        //        sprite.position.X = tailSprite.position.X + tailSprite.Width;
        //        //swap sprites
        //        Sprite tmp = sprite;
        //        sprite = tailSprite;
        //        tailSprite = tmp;

        //    }
        //}

        public override void Draw()
        {
            if (IsActive)
            {
                base.Draw();
                tailSprite.DrawTexture(texture);

                //draw bg layers
                for (int i = 0; i < sprites.Length; i++)
                {
                    sprites[i].DrawTexture(textures[i % textures.Length]);
                }
            }

        }
    }
}
