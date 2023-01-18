using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    internal class Player
    {
        public int X;
        public int Y;
        public int PastX;
        public int PastY;
        public Direction MoveDirection;
        public Grab GrabOnOff;
        public PortalNum PortalNum;
        public int PushedBoxIndex;
        public int GrabedBoxIndex;
    }
}
