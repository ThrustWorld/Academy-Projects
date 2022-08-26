using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace Progetto4_SpaceShooter_
{
    class WelcomeScene : TitleScene
    {
        private Texture texture;
        private Sprite sprite;

        public WelcomeScene() : base("Assets/welcomeBg.jpg", KeyCode.Return)
        {
            texture = new Texture("Assets/pressEnter.png");
            sprite = new Sprite(texture.Width, texture.Height);
            sprite.position = new Vector2(Game.Window.Width / 2 - 400, Game.Window.Height / 2);
        }

        public override void Draw()
        {
            base.Draw();
            sprite.DrawTexture(texture);
        }
    }
}
