using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto6_Smoker_
{
    interface IDrawable
    {
        DrawLayer Layer { get; }
        void Draw();
    }
}
