using sokoban;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    struct Player
    {
        public int X;
        public int Y;
        public PLAYER_DIRECTION PlayerDir;
        public int PushedBoxId;
        public char Symbol;
    }
}
