using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static Sokoban.GameObject;

namespace Sokoban
{
    internal class GameScene
    {
        public static Player player = new Player();

        public static Wall[] walls = new Wall[default];

        public static Box[] boxes = new Box[default];

        public static Goal[] goals = new Goal[default];

        public static ExitPoint exitPoint = new ExitPoint();

        public static Changer changer = new Changer();

        public static ColorBox[] colorBoxes = new ColorBox[default];

        public static VerticalItem[] verticalItems = new VerticalItem[default];

        public static HorizonItem[] horizonItems = new HorizonItem[default];

        public static PlayerHp[] playerHps = new PlayerHp[default];

        public static Trap[] traps = new Trap[default];

        public static PointItem[] pointItems = new PointItem[default];

        public static ColorWall[] colorWalls = new ColorWall[default];
     

        public static void Stage01()
        {
            string[] stage = File.ReadAllLines(Path.Combine("Assets", "Stage", $"Stage01.txt"));
            int wallLength = int.Parse(stage[stage.Length - 1]);

            walls = new Wall[wallLength];

            player = new Player
            {
                X = 25,
                Y = 10,
                Color = ConsoleColor.White,
                PlayerDir = GameSet.playerDir,
                PushedBoxId = 0,
                Symbol = '●'
            };

            boxes = new Box[]
            {
                new Box {X = 30,
                Y = 8,
                Color = ConsoleColor.White,
                Symbol = '■',
                IsOnGoal = false }
            };

            goals = new Goal[]
            {
                new Goal {X = 42,
                Y = 7,
                Color = ConsoleColor.White,
                InSymbol = '▣',
                Symbol = '□' }
            };

            exitPoint = new ExitPoint
            {
                X = 42,
                Y = 11,
                Symbol = '○'
            };

            colorWalls = new ColorWall[8]
            {
            new ColorWall {X = exitPoint.X + 1, Y = exitPoint.Y, color = ConsoleColor.White , DefaultX = exitPoint.X + 1, DefaultY = exitPoint.Y, Symbol = '#'},
            new ColorWall {X = exitPoint.X + 1, Y = exitPoint.Y + 1, color = ConsoleColor.White , DefaultX = exitPoint.X + 1, DefaultY = exitPoint.Y + 1, Symbol = '#'},
            new ColorWall {X = exitPoint.X - 1, Y = exitPoint.Y, color = ConsoleColor.White , DefaultX = exitPoint.X - 1, DefaultY = exitPoint.Y, Symbol = '#'},
            new ColorWall {X = exitPoint.X - 1, Y = exitPoint.Y + 1, color = ConsoleColor.White , DefaultX = exitPoint.X - 1, DefaultY = exitPoint.Y + 1, Symbol = '#'},
            new ColorWall {X = exitPoint.X, Y = exitPoint.Y + 1, color = ConsoleColor.White , DefaultX = exitPoint.X , DefaultY = exitPoint.Y + 1, Symbol = '#'},
            new ColorWall {X = exitPoint.X, Y = exitPoint.Y - 1, color = ConsoleColor.White , DefaultX = exitPoint.X , DefaultY = exitPoint.Y - 1, Symbol = '#'},
            new ColorWall {X = exitPoint.X + 1, Y = exitPoint.Y - 1, color = ConsoleColor.White , DefaultX = exitPoint.X + 1, DefaultY = exitPoint.Y - 1, Symbol = '#'},
            new ColorWall {X = exitPoint.X - 1, Y = exitPoint.Y - 1, color = ConsoleColor.White , DefaultX = exitPoint.X - 1, DefaultY = exitPoint.Y - 1, Symbol = '#'}
            };

            while (clearJudge)
            {
                // --------------------------------------------- Render -------------------------------------------------------

                Render.RenderStage(1);
                Render.RenderGoal();
                Render.RenderPlayer();
                Render.RenderExit();
                Render.RenderChange();
                Render.RenderColorWall();
                
                // --------------------------------------------- ProcessInput -------------------------------------------------

                Input.InputKey();

                // --------------------------------------------- Update -------------------------------------------------------


                Move.Right();
                Move.Left();
                Move.Up();
                Move.Down();


                
                OnColision.WithPlayerBox();
                OnColision.WithBoxWall();
                OnColision.WithBoxBox();
                OnColision.WithPlayerWall();
                OnColision.WithColorWall();

                GameRule.GoalInJudge();
                GameRule.JudgeClear();
                GameRule.OpenWall();

            }



        }

        public static void Stage02()
        {
            string[] stage = File.ReadAllLines(Path.Combine("Assets", "Stage", $"Stage02.txt"));
            int wallLength = int.Parse(stage[stage.Length - 1]);
            clearJudge = true;
            moveLimit = 1;
           
            walls = new Wall[wallLength];

            player = new Player
            {
                X = 25,
                Y = 10,
                Color = ConsoleColor.White,
                PlayerDir = GameSet.playerDir,
                PushedBoxId = 0,
                Symbol = '●'
            };

            boxes = new Box[]
            {
                new Box {X = 22, Y = 12, Color = ConsoleColor.White, Symbol = '■', IsOnGoal = false },
                new Box {X = 26, Y = 7, Color = ConsoleColor.DarkYellow, Symbol = '■', IsOnGoal = false }

            };

            goals = new Goal[]
            {
                new Goal {X = 42, Y = 7, Color = ConsoleColor.White, InSymbol = '▣', Symbol = '□' },

                new Goal {X = 13, Y = 13, Color = ConsoleColor.DarkYellow, InSymbol = '▣', Symbol = '□' }
            };

            exitPoint = new ExitPoint
            {
                X = 32,
                Y = 10,
                Symbol = '○'
            };

            colorWalls = new ColorWall[]
            {
                new ColorWall {X = exitPoint.X + 1, Y = exitPoint.Y, color = ConsoleColor.White , DefaultX = exitPoint.X + 1, DefaultY = exitPoint.Y, Symbol = '#'},
                new ColorWall {X = exitPoint.X - 1, Y = exitPoint.Y, color = ConsoleColor.White , DefaultX = exitPoint.X - 1, DefaultY = exitPoint.Y, Symbol = '#'},
                new ColorWall {X = exitPoint.X, Y = exitPoint.Y + 1, color = ConsoleColor.White , DefaultX = exitPoint.X , DefaultY = exitPoint.Y + 1, Symbol = '#'},
                new ColorWall {X = exitPoint.X, Y = exitPoint.Y - 1, color = ConsoleColor.White , DefaultX = exitPoint.X , DefaultY = exitPoint.Y - 1, Symbol = '#'},

                new ColorWall {X = exitPoint.X + 2, Y = exitPoint.Y, color = ConsoleColor.DarkYellow , DefaultX = exitPoint.X + 2, DefaultY = exitPoint.Y, Symbol = '#'},
                new ColorWall {X = exitPoint.X - 2, Y = exitPoint.Y, color = ConsoleColor.DarkYellow , DefaultX = exitPoint.X - 2, DefaultY = exitPoint.Y, Symbol = '#'},
                new ColorWall {X = exitPoint.X, Y = exitPoint.Y + 2, color = ConsoleColor.DarkYellow , DefaultX = exitPoint.X , DefaultY = exitPoint.Y + 2, Symbol = '#'},
                new ColorWall {X = exitPoint.X, Y = exitPoint.Y - 2, color = ConsoleColor.DarkYellow , DefaultX = exitPoint.X , DefaultY = exitPoint.Y - 2, Symbol = '#'},
                new ColorWall {X = exitPoint.X + 1, Y = exitPoint.Y + 1, color = ConsoleColor.DarkYellow , DefaultX = exitPoint.X + 1, DefaultY = exitPoint.Y + 1, Symbol = '#'},
                new ColorWall {X = exitPoint.X - 1, Y = exitPoint.Y + 1, color = ConsoleColor.DarkYellow , DefaultX = exitPoint.X - 1, DefaultY = exitPoint.Y + 1, Symbol = '#'},
                new ColorWall {X = exitPoint.X + 1, Y = exitPoint.Y - 1, color = ConsoleColor.DarkYellow , DefaultX = exitPoint.X + 1, DefaultY = exitPoint.Y - 1, Symbol = '#'},
                new ColorWall {X = exitPoint.X - 1, Y = exitPoint.Y - 1, color = ConsoleColor.DarkYellow , DefaultX = exitPoint.X - 1, DefaultY = exitPoint.Y - 1, Symbol = '#'}

            };

            colorBoxes = new ColorBox[]
            {
                new ColorBox { X = player.X - 10, Y = player.Y - 1, Color = ConsoleColor.White, Symbol = '■'},
                new ColorBox { X = player.X - 9, Y = player.Y - 1, Color = ConsoleColor.DarkYellow, Symbol = '■'}
            };

            changer = new Changer
            {
                X = colorBoxes[0].X, Y = colorBoxes[0].Y, Color = ConsoleColor.White, Symbol = '◎'
            };

            while (clearJudge)
            {
                // --------------------------------------------- Render -------------------------------------------------------

                Render.RenderStage(2);
                Render.RenderGoal();
                Render.RenderPlayer();
                Render.RenderExit();
                Render.RenderChange();
                Render.RenderColorWall();
                Render.RenderColorBox();
                Render.RenderChanger();


                // --------------------------------------------- ProcessInput -------------------------------------------------

                Input.InputKey();

                // --------------------------------------------- Update -------------------------------------------------------


                Move.Right();
                Move.Left();
                Move.Up();
                Move.Down();


                OnColision.WithPlayerBox();
                OnColision.WithBoxWall();
                OnColision.WithBoxBox();
                OnColision.WithPlayerWall();
                OnColision.WithColorWall();
                GameRule.ChangePlayerColor();

                GameRule.GoalInJudge();
                GameRule.JudgeClear();
                GameRule.OpenWall();

            }
        }
    }
}




