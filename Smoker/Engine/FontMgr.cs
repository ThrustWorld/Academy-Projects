﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto6_Smoker_
{
    static class FontMgr
    {
        private static Dictionary<string, Font> fonts;
        private static Font defaultFont;

        public static void Init()
        {
            fonts = new Dictionary<string, Font>();
            defaultFont = null;
        }

        public static Font AddFont(string fontName, string texturePath, int numColumns, int firstCharacterASCIIvalue, int charWidth, int charHeight)
        {
            Font f = new Font(fontName, texturePath, numColumns, firstCharacterASCIIvalue, charWidth, charHeight);
            fonts.Add(fontName, f);
            if(defaultFont == null)
            {
                defaultFont = f;
            }
            return f;
        }

        public  static Font GetFont(string fontName)
        {
            if(fonts.ContainsKey(fontName))
            {
                return fonts[fontName];
            }

            return defaultFont;
        }
    }
}
