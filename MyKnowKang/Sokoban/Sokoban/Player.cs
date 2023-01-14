using System;
using System.Collections.Generic;
using System.Text;

namespace Sokoban
{
    internal struct Player
    {
        public enum ActionKind
        {
            None,
            Move,
            Grab,
            Kick,
        }

        public static int[] ACTION_USE_MP = new int[4]
        {
            0, 1, 3, 5
        };

        public int X;
        public int Y;
        public int PrevX;
        public int PrevY;
        public string Image;
        public ConsoleColor Color;
        public ActionKind actionKind;

        public int Hp;
        public int Mp;
    }
}
