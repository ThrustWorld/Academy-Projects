using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lander_B
{

    class Ship
    {
        private Vector2 Velocity;
        private Sprite sprite;
        private Texture texture;
        private float speed = 250;
        private float rotSpeed = 1;
        private float safeRotDelta = MathHelper.DegreesToRadians(15);
        private bool rocketsAreOn;

        //flames
        private Texture[] flames;
        private Texture currentFrame;
        private int currentFrameIndex = 0;
        private Sprite flame;
        private float flamesFrameDuration = 0.04f;
        private float flamesAnimCounter = 0;


        public int Width { get { return (int)sprite.Height; } }
        public int Height { get { return (int)sprite.Width; } }

        public Vector2 Position { get { return sprite.position; } set { sprite.position = value; } }

        public Vector2 Forward { get { return new Vector2((float)Math.Cos(sprite.Rotation), (float)Math.Sin(sprite.Rotation)); } }

        public bool IsCrashed { get; private set; }
        public bool IsLanded { get; private set; }


        public Ship()
        {
            texture = new Texture("Assets/rocket_right.png");
            sprite = new Sprite(texture.Width, texture.Height);
            sprite.pivot = new Vector2(sprite.Width / 2, sprite.Height / 2);
            sprite.EulerRotation = -90;

            flames = new Texture[4];

            for (int i = 0; i < flames.Length; i++)
            {
                flames[i] = new Texture("Assets/fire_right_" + i + ".png");
            }
            flame = new Sprite(flames[0].Width, flames[0].Height);
            flame.pivot = new Vector2(flame.Width, flame.Height / 2);
            currentFrame = flames[0];

        }

        public void Update()
        {
            if (IsCrashed)
                return;

            //apply gravity
            if (!IsLanded)
                Velocity.Y += Game.Gravity * Game.DeltaTime;

            //update sprite position
            sprite.position += Velocity * Game.DeltaTime;

            int landing = CheckLanding();

            if (landing < 0)
            {
                IsCrashed = true;
                //Velocity = Vector2.Zero;
            }
            else if (landing == 1)
            {
                IsLanded = true;
                //Velocity = Vector2.Zero;
            }

            ComputeAnim();

            flame.position = Position - (Forward * Height / 2.4f);
            flame.Rotation = sprite.Rotation;
        }

        private void ComputeAnim()
        {
            flamesAnimCounter += Game.DeltaTime;
            if (flamesAnimCounter >= flamesFrameDuration)
            {//next frame
                flamesAnimCounter = 0;
                currentFrameIndex = (currentFrameIndex + 1) % flames.Length;
                currentFrame = flames[currentFrameIndex];
            }
        }

        private int CheckLanding()
        {
            Console.WriteLine(Velocity.Length);
            float basePosY = sprite.position.Y + Height / 2;
            if (basePosY > Game.Window.Height - 5 && basePosY < Game.Window.Height + 2)
            {


                if (sprite.Rotation > -MathHelper.PiOver2 - safeRotDelta && sprite.Rotation < -MathHelper.PiOver2 + safeRotDelta)
                {
                    //rotation is ok
                    if (Velocity.Length < 50)
                    {
                        Velocity = Vector2.Zero;
                        Console.WriteLine("Landed!");
                        return 1;
                    }
                }
                Velocity = Vector2.Zero;
                Console.WriteLine("Crashed!");
                return -1;
            }
            else
            {
                IsLanded = false;
            }

            return 0;//not landed
        }

        public void Input()
        {
            if (Game.Window.GetKey(KeyCode.Up))
            {
                Velocity += Forward * (speed * Game.Window.deltaTime);
                rocketsAreOn = true;
            }
            else
            {
                rocketsAreOn = false;
            }

            if (Game.Window.GetKey(KeyCode.Right))
            {
                sprite.Rotation += rotSpeed * Game.Window.deltaTime;
            }
            else if (Game.Window.GetKey(KeyCode.Left))
            {
                sprite.Rotation -= rotSpeed * Game.Window.deltaTime;
            }


        }

        public void Draw()
        {
            if (rocketsAreOn)
                flame.DrawTexture(currentFrame);
            sprite.DrawTexture(texture);
        }

    }
}
