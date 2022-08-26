using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace Progetto8_Boids_
{
    class Boid
    {
        protected Sprite sprite;
        protected static Texture texture;
        protected static float steerSpeed = 4;
        protected static float alignmentRadius = 300;
        protected static float cohesionRadius = 200;
        protected static float separationRadius = 70;
        protected static float halfAngle = MathHelper.DegreesToRadians(150);
        protected static float alignmentWeight = 1;
        protected static float cohesionWeight  = 1;
        protected static float separationWeight = 8;

        public Vector2 Velocity { get; protected set; }
        public  Vector2 Position { get { return sprite.position; } protected set { sprite.position = value; } }

        public Vector2 Forward
        {
            get { return new Vector2((float)Math.Cos(sprite.Rotation), (float)Math.Sin(sprite.Rotation)); } // view direction

            set { sprite.Rotation = (float)Math.Atan2(value.Y, value.X); }
        }

        public float Speed = 150;

        public Boid(Vector2 position)
        {
            if(texture == null)
            {
                texture = new Texture("Assets/boid.png");
            }

            sprite = new Sprite(texture.Width, texture.Height);
            sprite.pivot = new Vector2(sprite.Width * 0.5f, sprite.Height * 0.5f);
            Position = position;

            float randomX = RandomGenerator.GetRandomFloat() * (RandomGenerator.GetRandom(0, 2) == 0 ? 1.0f : -1.0f);
            float randomY = RandomGenerator.GetRandomFloat() * (RandomGenerator.GetRandom(0, 2) == 0 ? 1.0f : -1.0f);

            Velocity = new Vector2(randomX, randomY).Normalized() * Speed;

        }

        public virtual void FixPosition()
        {
            if (sprite.position.X + sprite.pivot.X < 0) //Left
            {
                sprite.position.X = (Program.Window.Width - 1) + sprite.pivot.X;
            }
            else if (sprite.position.X - sprite.pivot.X > Program.Window.Width) //Right
            {
                sprite.position.X = -sprite.pivot.X;
            }

            if(sprite.position.Y  + sprite.pivot.Y < 0) // Up
            {
                sprite.position.Y = (Program.Window.Height - 1) + sprite.pivot.Y;
            }
            else if (sprite.position.Y - sprite.pivot.Y> Program.Window.Height) //Bottom
            {
                sprite.position.Y = -sprite.pivot.Y; 
            }
        }

        public virtual bool IsVisible(Vector2 position, float radius, float halfAngle, out Vector2 distance)
        {
            distance = position - Position;
            if(distance.Length <= radius)
            {
                float angle = (float)Math.Acos(Vector2.Dot(Forward, distance.Normalized()));
                if(angle <= halfAngle)
                {
                    return true;
                }
            }
            return false;
        }

        public Vector2 GetAlignment()
        {
            Vector2 alignment = Vector2.Zero;
            Vector2 distance;
            int counter = 0;
            for (int i = 0; i < Program.Boids.Count; i++)
            {
                if(Program.Boids[i] == this)
                {
                    continue;
                }
                if (IsVisible(Program.Boids[i].Position,alignmentRadius,halfAngle, out distance))
                {
                    counter++;
                    alignment += Program.Boids[i].Velocity;
                }

            }

            if(counter != 0)
            {
                return (alignment / counter).Normalized();           
            }
           
            return alignment;
        }

        public Vector2 GetCohesion()
        {
            Vector2 cohesion = Vector2.Zero;
            Vector2 distance;
            int counter = 0;

            for (int i = 0; i < Program.Boids.Count; i++)
            {
                if (Program.Boids[i] == this)
                {
                    continue;
                }

                if (IsVisible(Program.Boids[i].Position, cohesionRadius, halfAngle, out distance))
                {
                    counter++;
                    cohesion += Program.Boids[i].Velocity;
                }
            }

            if (counter > 0)
            {
                cohesion /= counter;
                cohesion = cohesion - Position;
                cohesion.Normalize();
            }

            return cohesion;
            //zero, if nobody's close, or cohesion normalized
        }

        public Vector2 GetSeparation()
        {
            Vector2 separation = Vector2.Zero;
            Vector2 distance;
            int counter = 0;

            for (int i = 0; i < Program.Boids.Count; i++)
            {
                if (Program.Boids[i] == this)
                {
                    continue;
                }

                if (IsVisible(Program.Boids[i].Position, separationRadius, halfAngle, out distance))
                {
                    counter++;
                    separation += distance;
                }

            }

            if(counter > 0)
            {
                separation = -(separation / counter).Normalized();
            }
            return separation;
        }

        public virtual void Update()
        {
            Vector2 alignment = GetAlignment() * alignmentWeight;
            Vector2 cohesion = GetCohesion() * cohesionWeight;
            Vector2 separation = GetSeparation() * separationWeight;

            Vector2 result = alignment + cohesion + separation;

            if(result != Vector2.Zero)
            {
                Velocity = Vector2.Lerp(Velocity, result.Normalized() * Speed, Program.DeltaTime * steerSpeed).Normalized() * Speed;
            }

            sprite.position += Velocity * Program.DeltaTime;
            FixPosition();
            Forward = Velocity;
        }

        public virtual void Draw()
        {
            sprite.DrawTexture(texture);
        }
    }
}
