using System;
using System.Collections.Generic;
using System.Text;

namespace Sokoban
{
    internal struct Wall
    {
        public int X;
        public int Y;
        public string Image;
        public ConsoleColor Color;
        public bool isActive;  // 현재 벽이 활성화되어있는가( default = true )..
    }
}
