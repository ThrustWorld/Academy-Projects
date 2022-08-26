using Aiv.Fast2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto6_Smoker_
{

    class TitleScene : Scene
    {
        protected KeyCode exitKey;
        protected Sprite image;
        protected Texture texture;
        protected string texturePath;

        public TitleScene(string texturePath, KeyCode exitKey )
        {
            this.texturePath = texturePath;
            this.exitKey = exitKey;
        }

        public override void Start()
        {
            image = new Sprite(Game.Window.Width, Game.Window.Height);
            texture = new Texture(texturePath);
            base.Start();
        }

        public override void Draw()
        {
            image.DrawTexture(texture);
        }

        public override Scene OnExit()
        {
            image = null;
            texture = null;
            return base.OnExit();
        }

        public override void Input()
        {
            if(Game.Window.GetKey(exitKey))
            {
                IsPlaying = false;
            }
        }
    }
}
