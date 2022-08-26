using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto4_SpaceShooter_
{
    class PlayScene : Scene
    {

        private Background bg;
        public Player Player { get; private set; }


        public PlayScene()
        {

        }

        protected virtual void LoadTextures()
        {
            GfxMgr.AddTexture("player", "Assets/player_ship.png");

            GfxMgr.AddTexture("enemy1", "Assets/enemy_ship.png");
            GfxMgr.AddTexture("enemy2", "Assets/redEnemy_ship.png");

            GfxMgr.AddTexture("blueLaser", "Assets/blueLaser.png");
            GfxMgr.AddTexture("fireGlobe","Assets/fireGlobe.png");
            GfxMgr.AddTexture("greenGlobe", "Assets/greenGlobe.png");

            GfxMgr.AddTexture("bg", "Assets/spaceBg.png");

            GfxMgr.AddTexture("nrgPowerUp", "Assets/powerUp_battery.png");
            GfxMgr.AddTexture("triplePowerUp", "Assets/powerUp_triple.png");

            GfxMgr.AddTexture("barFrame", "Assets/loadingBar_frame.png");
            GfxMgr.AddTexture("barBar", "Assets/loadingBar_bar.png");
        }
    
        public override void Start()
        {
            base.Start();

            GfxMgr.Init();
            LoadTextures();

            FontMgr.Init();
            Font stdFont = FontMgr.AddFont("stdFont", "Assets/textSheet.png", 15, 32, 20, 20);
            Font comic = FontMgr.AddFont("comic", "Assets/comics.png", 10, 32, 61, 65);

            bg = new Background();
            Player = new Player(new Vector2(200, Game.Window.Height / 2));

            BulletsMgr.Init();
            SpawnMgr.Init();
        }
        public override void Draw()
        {
            DrawMgr.Draw();
        }
        public override void Update()
        {
            SpawnMgr.Update();
            PhysicsMgr.Update();
            UpdateMgr.Update();
            PhysicsMgr.CheckCollisions();
            if(!Player.IsAlive)
            {
                IsPlaying = false;
            }
        }
        public override Scene OnExit()
        {
            DrawMgr.Clear();
            UpdateMgr.Clear();
            PhysicsMgr.Clear();
            SpawnMgr.Clear();
            GfxMgr.Clear();

            Player = null;
            bg = null;

            return base.OnExit();
        }
        public override void Input()
        {
            Player.Input();

        }
    }
}
