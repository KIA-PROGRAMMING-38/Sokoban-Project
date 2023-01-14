using System;
using System.Collections.Generic;
using System.Text;

namespace Sokoban
{
    internal struct Goal
    {
        public int X;
        public int Y;
        public string Image;
        public ConsoleColor Color;
        public ConsoleColor GoalInColor;
        public bool isGoalIn;
    }
}
