using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace Progetto2_SquareInvaders_
{
    struct Color
    {
        public byte R;
        public byte G;
        public byte B;
        public byte A;

        public Color(byte r = 0, byte g = 0, byte b = 0, byte a = 255)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Game.Play();
        }
    }
}
