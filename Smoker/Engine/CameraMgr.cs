using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace Progetto6_Smoker_
{
    struct CameraLimits
    {
        public float MaxX;
        public float MinX;
        public float MaxY;
        public float MinY;

        public CameraLimits(float maxX, float minX, float maxY, float minY)
        {
            MaxX = maxX;
            MinX = minX;
            MaxY = maxY;
            MinY = minY;
        }
    }

    static class CameraMgr
    {
        public enum FollowBehaviour{Default, FollowTarget, FollowPoint }

        private static GameObject target;
        private static Dictionary<string, Tuple<Camera, float>> cameras;

        public static Camera MainCamera { get; private set; }
        public static GameObject Target { get { return target; } set { target = value; } }
        public static CameraLimits CameraLimits;
        public static FollowBehaviour Follow { get; set; }
        public static Vector2 TargetPoint { get; set; }
        public static float HalfDiagonal { get; private set; }

        public static void Init()
        {
            Init(Vector2.Zero, Vector2.Zero);
        }

        public static void Init(Vector2 position, Vector2 pivot)
        {
            if(MainCamera == null) // la istanzio solo se non c'è già;
            {
                MainCamera = new Camera(position.X, position.Y);
                MainCamera.pivot = pivot;
            }

            cameras = new Dictionary<string, Tuple<Camera, float>>();

            HalfDiagonal = (float)Math.Sqrt(Game.Window.Width * Game.Window.Width + Game.Window.Height * Game.Window.Height) * 0.5f;
            
        }

        public static void ResetCamera()
        {
            MainCamera.position = Vector2.Zero;
            MainCamera.pivot = Vector2.Zero;
            target = null;
            Follow = FollowBehaviour.Default;

            //reset limits
            CameraLimits.MinY = 0;
            CameraLimits.MinX = 0;
        }

        public static void AddCamera(string cameraName, Camera camera = null, float cameraSpeed = 0.0f)
        {
            if (camera == null)
            {
                camera = new Camera(MainCamera.position.X, MainCamera.position.Y);
                camera.pivot = MainCamera.pivot;
            }

            cameras[cameraName] = new Tuple<Camera, float>(camera, cameraSpeed);
        }

        public static Camera GetCamera(string cameraName)
        {
            if (cameras.ContainsKey(cameraName))
            {
                return cameras[cameraName].Item1;
            }
            return null;
        }

        public static void Update()
        {
            Vector2 oldCameraPos = MainCamera.position;
            Vector2 pointToFollow = Vector2.Zero;
            bool cameraMoved = false;
            if(Follow == FollowBehaviour.FollowTarget)
            {
                if (target != null)
                {
                    pointToFollow = target.Position;
                    cameraMoved = true;
                }
            }
            else if (Follow == FollowBehaviour.FollowPoint)
            {
                pointToFollow = TargetPoint;
                cameraMoved = true;
            }

            if (cameraMoved)
            {
                MainCamera.position = Vector2.Lerp(MainCamera.position, pointToFollow, Game.DeltaTime * 10);
                FixPosition();

                Vector2 cameraDelta = MainCamera.position - oldCameraPos;

                foreach (var cam in cameras)
                {
                    //camera += delta * speed
                    cam.Value.Item1.position += cameraDelta * cam.Value.Item2;
                }
            }
        }

        private static void FixPosition()
        {
            if (MainCamera.position.Y < CameraLimits.MinY)
            {
                MainCamera.position.Y = CameraLimits.MinY;
            }
            else if (MainCamera.position.Y > CameraLimits.MaxY)
            {
                MainCamera.position.Y = CameraLimits.MaxY;
            }

            if (MainCamera.position.X < CameraLimits.MinX)
            {
                MainCamera.position.X = CameraLimits.MinX;
            }
            else if (MainCamera.position.X > CameraLimits.MaxX)
            {
                MainCamera.position.X = CameraLimits.MaxX;
            }
        }

        public static void SetCameraLimits(float maxX, float minX, float maxY, float minY)
        {
            CameraLimits = new CameraLimits(maxX, minX, maxY, minY);
        }
    }
}
