using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto6_Smoker_
{
    class PlayScene : Scene
    {

        private Background bg;
        public List<Player> Players { get; protected set; }
        


        public PlayScene()
        {

        }

        protected virtual void LoadTextures()
        {
            GfxMgr.AddTexture("player", "Assets/player.png");
            
            GfxMgr.AddTexture("greenGlobe", "Assets/greenGlobe.png");

            GfxMgr.AddTexture("bg", "Assets/sky.png");

            GfxMgr.AddTexture("barFrame", "Assets/loadingBar_frame.png");
            GfxMgr.AddTexture("barBar", "Assets/loadingBar_bar.png");

            GfxMgr.AddTexture("nrgPowerUp", "Assets/broccoli.png");
            GfxMgr.AddTexture("earthTile", "Assets/Levels/earth.png");
        }
    
        public override void Start()
        {
            base.Start();
            
            Vector2 playerPos = new Vector2(Game.Window.Width * 0.5f, Game.Window.Height *0.5f);

            CameraMgr.Init(playerPos, new Vector2(Game.Window.Width * 0.5f, Game.Window.Height * 0.5f));
            CameraMgr.SetCameraLimits(Game.Window.Width*1.6f, CameraMgr.MainCamera.pivot.X, Game.Window.Height * 0.5f,330);
            CameraMgr.Follow = CameraMgr.FollowBehaviour.FollowPoint;

            CameraMgr.AddCamera("GUI", new Camera());
            CameraMgr.AddCamera("Sky", cameraSpeed: 0.02f);
            CameraMgr.AddCamera("Bg_0", cameraSpeed:0.15f);
            CameraMgr.AddCamera("Bg_1", cameraSpeed:0.2f);
            CameraMgr.AddCamera("Bg_2", cameraSpeed:0.9f);

            GfxMgr.Init();
            LoadTextures();

            FontMgr.Init();
            Font stdFont = FontMgr.AddFont("stdFont", "Assets/textSheet.png", 15, 32, 20, 20);
            Font comic = FontMgr.AddFont("comic", "Assets/comics.png", 10, 32, 61, 65);

            bg = new Background();

            Players = new List<Player>();
            Players.Add(new Player(playerPos));
            //Players.Add(new Player(playerPos + new Vector2(100, 0), 1));
            //Players[1].RIGHT = KeyCode.Right;
            //Players[1].LEFT = KeyCode.Left;
            //Players[1].JUMP = KeyCode.Up;


            BulletsMgr.Init();
            SpawnMgr.Init();

            Tile tile = new Tile(new Vector2(500, 300));

            for (int i = 1; i <= 4; i++)
            {
                Tile t = new Tile(new Vector2(tile.Position.X + i * tile.Height, tile.Position.Y));
            }

            Tile tile2 = new Tile(new Vector2(800, 500));

            for (int i = 1; i <= 4; i++)
            {
                Tile t = new Tile(new Vector2(tile2.Position.X + i * tile2.Height, tile2.Position.Y));
            }
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
            CameraMgr.TargetPoint = GetPointToFollow();
            CameraMgr.Update();

            bool isPlaying = false;
            for (int i = 0; i < Players.Count; i++)
            {
                if (Players[i].IsAlive)
                {
                    isPlaying = true;
                    break;
                }
                IsPlaying = isPlaying;
            }
        }
        public override Scene OnExit()
        {
            DrawMgr.Clear();
            UpdateMgr.Clear();
            PhysicsMgr.Clear();
            SpawnMgr.Clear();
            GfxMgr.Clear();

            CameraMgr.ResetCamera();

            for (int i = 0; i < Players.Count; i++)
            {
                Players[i].Destroy();
            }
            Players.Clear();

            bg = null;

            return base.OnExit();
        }
        public override void Input()
        {
            for (int i = 0; i < Players.Count; i++)
            {
                if (Players[i].IsAlive)
                {
                    Players[i].Input();
                }
            }
        }

        protected virtual Vector2 GetPointToFollow()
        {
            Vector2 point = Vector2.Zero;
            int counter = 0;
            for (int i = 0; i < Players.Count; i++)
            {
                if (Players[i].IsAlive)
                {
                    point += Players[i].Position;
                    counter++;
                }
            }
            point /= counter;
            return point;
        }
    }
}
