using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    internal class GameObject
    {
        static public Player player = new Player
        {
            X = 14,
            Y = 6,
            PlayerDir = GameSet.playerDir,
            PushedBoxId = 0,
            Symbol = '●',
            Color = ConsoleColor.White
        };

        static public Changer changer = new Changer
        {
            X = GameSet.MAP_MAX_X + 5,
            Y = GameSet.MAP_MAX_Y,
            Symbol = '◎',
            Color = ConsoleColor.White
        };

        static public Box[] boxes = new Box[]
        {
                new Box {X = 22, Y = 14, IsOnGoal = false , Symbol = ' ', Color = ConsoleColor.White},
                new Box {X = 23, Y = 14, IsOnGoal = false , Symbol = ' ', Color = ConsoleColor.DarkGreen},
                new Box {X = 24, Y = 14, IsOnGoal = false , Symbol = ' ', Color = ConsoleColor.DarkYellow},
                new Box {X = 25, Y = 14, IsOnGoal = false , Symbol = ' ', Color = ConsoleColor.Black}
        };

        static public ColorBox[] colorboxes = new ColorBox[]
        {
                new ColorBox {X = GameSet.MAP_MAX_X + 5 , Y = GameSet.MAP_MAX_Y - 1 , Symbol = '■', Color = ConsoleColor.White},
                new ColorBox {X = GameSet.MAP_MAX_X + 4 , Y = GameSet.MAP_MAX_Y , Symbol = '■', Color = ConsoleColor.DarkGreen},
                new ColorBox {X = GameSet.MAP_MAX_X + 5 , Y = GameSet.MAP_MAX_Y , Symbol = '■', Color = ConsoleColor.DarkYellow},
                new ColorBox {X = GameSet.MAP_MAX_X + 4 , Y = GameSet.MAP_MAX_Y - 1, Symbol = '■', Color = ConsoleColor.Black}
        };

        static public ExitPoint exitPoint = new ExitPoint
        {
            X = 5,
            Y = 6,
            Symbol = '○'
        };

        static public Wall[] walls = new Wall[]
        {
                new Wall {X = 1 , DefaultX = 1, Y = 2 , DefaultY = 2, Symbol = '#', Color = ConsoleColor.Black},
                new Wall {X = 2 , DefaultX = 2, Y = 2 , DefaultY = 2, Symbol = '#', Color = ConsoleColor.Black},
                new Wall {X = 2 , DefaultX = 2, Y = 1 , DefaultY = 1, Symbol = '#', Color = ConsoleColor.Black},

                new Wall {X = GameSet.MAP_MAX_X - 2 , DefaultX = GameSet.MAP_MAX_X - 2, Y = 1 , DefaultY = 1, Symbol = '#', Color = ConsoleColor.White},
                new Wall {X = GameSet.MAP_MAX_X - 1 , DefaultX = GameSet.MAP_MAX_X - 1, Y = 2 , DefaultY = 2, Symbol = '#', Color = ConsoleColor.White},
                new Wall {X = GameSet.MAP_MAX_X  , DefaultX = GameSet.MAP_MAX_X, Y = 3 , DefaultY = 3, Symbol = '#', Color = ConsoleColor.White},

                new Wall {X = 1 , DefaultX = 1, Y = GameSet.MAP_MAX_Y - 1 , DefaultY = GameSet.MAP_MAX_Y - 1, Symbol = '#', Color = ConsoleColor.DarkGreen},
                new Wall {X = 2 ,DefaultX = 2, Y = GameSet.MAP_MAX_Y ,DefaultY = GameSet.MAP_MAX_Y, Symbol = '#', Color = ConsoleColor.DarkGreen},
                new Wall {X = 2  ,DefaultX = 2, Y = GameSet.MAP_MAX_Y - 1 ,DefaultY = GameSet.MAP_MAX_Y - 1, Symbol = '#', Color = ConsoleColor.DarkGreen},

                new Wall {X = exitPoint.X + 1 ,DefaultX = exitPoint.X + 1, Y = exitPoint.Y ,DefaultY =  exitPoint.Y, Symbol = '#', Color = ConsoleColor.DarkYellow},
                new Wall {X = exitPoint.X - 1 ,DefaultX = exitPoint.X - 1, Y = exitPoint.Y ,DefaultY =  exitPoint.Y, Symbol = '#', Color = ConsoleColor.DarkYellow},
                new Wall {X = exitPoint.X ,DefaultX = exitPoint.X, Y = exitPoint.Y - 1 ,DefaultY = exitPoint.Y - 1, Symbol = '#', Color = ConsoleColor.DarkYellow},
                new Wall {X = exitPoint.X ,DefaultX = exitPoint.X, Y = exitPoint.Y + 1 ,DefaultY = exitPoint.Y + 1, Symbol = '#', Color = ConsoleColor.DarkYellow}
        };

        static public Goal[] goals = new Goal[]
        {
                new Goal { X = 5 , Y = 2, Symbol = '□' , InSymbol = '▣', Color = ConsoleColor.White},
                new Goal { X = 16 , Y = 10, Symbol = '□' , InSymbol = '▣', Color = ConsoleColor.DarkGreen},
                new Goal { X = 5 , Y = 10, Symbol = '□' , InSymbol = '▣', Color = ConsoleColor.DarkYellow},
                new Goal { X = 16 , Y = 2, Symbol = '□' , InSymbol = '▣', Color = ConsoleColor.Black}
        };

        static public PointItem[] pointItems = new PointItem[]
        {
                new PointItem {X = 1, Y = 1, Symbol = '*'},
                new PointItem {X = GameSet.MAP_MAX_X, Y = 1, Symbol = '*'},
                new PointItem {X = 1, Y = GameSet.MAP_MAX_Y, Symbol = '*'},
                new PointItem {X = player.X - 2, Y = player.Y, Symbol = '*'}
        };

        static public HorizonItem[] horizonItem = new HorizonItem[]
        {
               new HorizonItem {X = GameSet.MAP_MAX_X - 1 , Y = 1, Symbol = '↔', Color = ConsoleColor.Red},
               new HorizonItem {X = 1 , Y = GameSet.MAP_MAX_Y - 2, Symbol = '↔', Color = ConsoleColor.Red},
               new HorizonItem {X = 3 , Y = GameSet.MAP_MAX_Y, Symbol = '↔', Color = ConsoleColor.Red},
               new HorizonItem {X = player.X - 1 , Y = player.Y, Symbol = '↔', Color = ConsoleColor.Red}
        };

        static public VerticalItem[] verticalItem = new VerticalItem[]
        {
                new VerticalItem {X = GameSet.MAP_MAX_X , Y = 2, Symbol = '↕', Color = ConsoleColor.Red},
                new VerticalItem {X = 3 , Y = GameSet.MAP_MAX_Y, Symbol = '↕', Color = ConsoleColor.Red},
                new VerticalItem {X = 3 , Y = GameSet.MAP_MAX_Y - 1, Symbol = '↕', Color = ConsoleColor.Red},
                new VerticalItem {X = 2 , Y = GameSet.MAP_MAX_Y - 2, Symbol = '↕', Color = ConsoleColor.Red}
        };

        static public int boxLength = boxes.Length;
        static public int wallLength = walls.Length;
        static public int goalLength = goals.Length;
        static public int itemLength = horizonItem.Length;
        static public int colorBoxLength = colorboxes.Length;
        static public int pItemLength = pointItems.Length;
        static public int hFunction = 0;
        static public int vFunction = 0;
        static public int point = 0;
        static public int move = 0;
        static public int moveLimit = 10;


        static public bool[] isBoxOnGoal = new bool[boxLength];
        static public bool clearJudge = true;
        static public bool hChangeDir = false;
        static public bool vChangeDir = false;
    }
}
