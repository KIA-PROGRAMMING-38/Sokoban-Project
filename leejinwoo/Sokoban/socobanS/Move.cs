using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;


namespace socobanS
{
    class Move
    {
        const int MIN_X = 1;
        const int MIN_Y = 1;
        const int MAX_X = 20;
        const int MAX_Y = 15;

        public static void MoveToLeftOfTarget(out int x, in int target) => x = Math.Max(MIN_X, target - 1);
        public static void MoveToRightOfTarget(out int x, in int target) => x = Math.Min(MAX_X, target + 1);
        public static void MoveToUpOfTarget(out int y, in int target) => y = Math.Max(MIN_Y, target - 1);
        public static void MoveToDownOfTarget(out int y, in int target) => y = Math.Min(MAX_Y, target + 1);


        public static void MoveBox(Player player, Box box)
        {
            switch (player.MoveDirection)
            {
                case Direction.Left:
                    MoveToLeftOfTarget(out box.X, in player.X);

                    break;
                case Direction.Right:
                    MoveToRightOfTarget(out box.X, in player.X);

                    break;
                case Direction.Up:
                    MoveToUpOfTarget(out box.Y, in player.Y);

                    break;
                case Direction.Down:
                    MoveToDownOfTarget(out box.Y, in player.Y);

                    break;
                default: // Error
                    ExitWithError($"[Error] 플레이어 방향 : {player.MoveDirection}");

                    break;
            }
        }

        public static void MovePlayer(ConsoleKey key, Player player)
        {

            if (key == ConsoleKey.A)
            {
                player.X = Math.Max(player.X - 2, MIN_X);
                player.MoveDirection = Direction.a;
            }
            if (key == ConsoleKey.S)
            {
                player.X = Math.Min(player.X + 2, MAX_X);
                player.MoveDirection = Direction.s;
            }
            if (key == ConsoleKey.LeftArrow)
            {
                MoveToLeftOfTarget(out player.X, in player.X);
                player.MoveDirection = Direction.Left;
            }

            if (key == ConsoleKey.RightArrow)
            {
                MoveToRightOfTarget(out player.X, in player.X);
                player.MoveDirection = Direction.Right;
            }

            if (key == ConsoleKey.UpArrow)
            {
                MoveToUpOfTarget(out player.Y, in player.Y);
                player.MoveDirection = Direction.Up;
            }

            if (key == ConsoleKey.DownArrow)
            {
                MoveToDownOfTarget(out player.Y, in player.Y);
                player.MoveDirection = Direction.Down;
            }
        }

        public static void ExitWithError(string errorMessage)
        {
            Console.Clear();
            Console.WriteLine(errorMessage);
            Environment.Exit(1);
        }

        public static string[] LoadStage(int stageNumber)
        {
            //1. 경로를 구성한다.
            string stageFilePath = Path.Combine("Assets", "Stage", $"Stage{stageNumber:D2}.txt");

            //2. 파일이 존재하는지 확인한다.
            if (false == File.Exists(stageFilePath))
            {
                ExitWithError($"스테이지 파일이 없습니다. 스테이지 번호({stageNumber})");
            }


            //3. 파일의 내용을 불러온다
            return File.ReadAllLines(stageFilePath);
        }

        public static void ParseStage(string[] stage, out Player player, out Box[] box, out Wall[] wall, out Goal[] goal)
        {
            string[] stageMetadata = stage[0].Split(" ");
            player = null;
            wall = new Wall[int.Parse(stageMetadata[0])];
            box = new Box[int.Parse(stageMetadata[1])];
            goal = new Goal[int.Parse(stageMetadata[2])];

            int wallIndex = 0;
            int boxIndex = 0;
            int goalIndex = 0;

            for (int y = 1; y < stage.Length; ++y)
            {
                for (int x = 0; x < stage[y].Length; ++x)
                {
                    switch (stage[y][x])
                    {
                        case ObjectSymbol.player:
                            player = new Player { X = x, Y = y };
                            break;
                        case ObjectSymbol.Wall:
                            wall[wallIndex] = new Wall { X = x, Y = y - 1 };
                            break;
                        case ObjectSymbol.box:
                            box[boxIndex] = new Box { X = x, Y = y - 1 };
                            break;
                        case ObjectSymbol.goal:
                            goal[goalIndex] = new Goal { X = x, Y = y - 1 };
                            break;
                        case ' ':
                            break;
                        default:
                            ExitWithError("스테이지 파일이 잘못되었습니다.");
                            break;
                    }
                }
            }

        }

    }
}



