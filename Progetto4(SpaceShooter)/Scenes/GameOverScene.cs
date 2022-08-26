using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;

namespace Progetto4_SpaceShooter_
{
    class GameOverScene : TitleScene
    {
        public GameOverScene() : base("Assets/gameOverBg.png", KeyCode.Y)
        {
        }

        public override void Input()
        {
            base.Input();
            if (IsPlaying && Game.Window.GetKey(KeyCode.N))
            {
                IsPlaying = false;
                NextScene = null;
            }
        }
    }
}
