using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    internal class Mine
    {
        internal class Tunnel
        {
            public int InMainX;
            public int InMainY;
            public int InMineX;
            public int InMineY;
        }

        internal class Mineral
        {
            public int X;
            public int Y;
            public string Name;
        }
    }
}
