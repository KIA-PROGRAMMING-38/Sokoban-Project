using KMH_Sokoban;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sokoban
{
    internal struct Box
    {
        public enum State
        {
            Idle,
            Move,
            GrabByPlayer
        }

        public int X;
        public int Y;
        public int PrevX;
        public int PrevY;
        public string Image;
        public ConsoleColor Color;
        public State CurState;
        public int DirX;
        public int DirY;
    }
}
