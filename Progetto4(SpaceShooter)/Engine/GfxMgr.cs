using Aiv.Fast2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto4_SpaceShooter_
{
    static class GfxMgr
    {
        private static Dictionary<string, Texture> textures;

        public static void Init()
        {
            textures = new Dictionary<string, Texture>();
        }

        public static Texture AddTexture(string textureName, string texturePath)
        {
            Texture t = new Texture(texturePath);
            textures.Add(textureName, t);
            return t;
        }

        public static Texture GetTexture(string textureName)
        {
            if (textures.ContainsKey(textureName))
            {
                return textures[textureName];
            }

            return null;
        }

        public static void Clear()
        {
            textures.Clear();
        }
    }
}
