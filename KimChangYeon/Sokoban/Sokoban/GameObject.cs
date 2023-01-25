using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    internal class GameObject
    {
        public class Wall
        {
            public int X;
            public int Y;
        }

        public class Player
        {
            public int X;
            public int Y;
            public GameSet.PLAYER_DIRECTION PlayerDir;
            public int PushedBoxId;
            public char Symbol;
            public ConsoleColor Color;
        }

        public class Changer
        {
            public int X;
            public int Y;
            public char Symbol;
            public ConsoleColor Color;
        }

        public class Box
        {
            public int X;
            public int Y;
            public bool IsOnGoal;
            public char Symbol;
            public ConsoleColor Color;
        }

        public class ColorBox
        {
            public int X;
            public int Y;
            public char Symbol;
            public ConsoleColor Color;
        }

        public class ExitPoint
        {
            public int X;
            public int Y;
            public char Symbol;
        }

        public class Goal
        {
            public int X;
            public int Y;
            public char Symbol;
            public char InSymbol;
            public ConsoleColor Color;
        }

        public class PointItem
        {
            public int X;
            public int Y;
            public char Symbol;
        }

        public class HorizonItem
        {
            public int X;
            public int Y;
            public char Symbol;
            public ConsoleColor Color;
        }

        public class VerticalItem
        {
            public int X;
            public int Y;
            public char Symbol;
            public ConsoleColor Color;
        }

        public class PlayerHp
        {
            public char Hp;
            public char LoseHp;
        }

        public class Trap
        {
            public int X;
            public int Y;
        }

        public class ColorWall
        {
            public int X;
            public int DefaultX;
            public int Y;
            public int DefaultY;
            public char Symbol;
            public ConsoleColor color;
        }



       
        public static int point = 0;
        public static int move = 0;
        public static int moveLimit = 0;
        public static int playerHpNumber = 5;


        public static bool[] isBoxOnGoal = new bool[GameScene.boxes.Length];
        public static bool clearJudge = true;
        public static bool hChangeDir = false;
        public static bool vChangeDir = false;
    }
}
