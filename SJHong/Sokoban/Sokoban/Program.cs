using System;

namespace sokoban
{
    enum Direction
    {
        None,
        Right,
        Left,
        Down,
        Up
    }
    class Program
    {

        static void Main()
        {

            Console.ResetColor();
            Console.CursorVisible = false;
            Console.Title = "소코반";
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();

            const int InitialplayerX = 7;
            const int InitialplayerY = 7;
            Direction playerDirection = default;

            const int Initialbox0X = 11;
            const int Initialbox0Y = 11;
            const int Initialbox1X = 20;
            const int Initialbox1Y = 20;
            const int Initialbox2X = 15;
            const int Initialbox2Y = 15;
            const int BoxNum = 3;

            const int Initialgoal0X = 12;
            const int Initialgoal0Y = 7;
            const int Initialgoal1X = 20;
            const int Initialgoal1Y = 12;
            const int Initialgoal2X = 17;
            const int Initialgoal2Y = 10;
            int GoalNum = 3;

            const int InitialBlock0X = 10;
            const int InitialBlock0Y = 10;
            const int InitialBlock1X = 10;
            const int InitialBlock1Y = 11;
            const int InitialBlock2X = 10;
            const int InitialBlock2Y = 12;
            const int BlockNum = 3;

            const int MaxX = 25;
            const int MinX = 5;
            const int MaxY = 25;
            const int MinY = 5;

            int playerX = InitialplayerX;
            int playerY = InitialplayerY;


            int[] boxX = new int[BoxNum];
            boxX[0] = Initialbox0X;
            boxX[1] = Initialbox1X;
            boxX[2] = Initialbox2X;

            int[] boxY = new int[BoxNum];
            boxY[0] = Initialbox0Y;
            boxY[1] = Initialbox1Y;
            boxY[2] = Initialbox2Y;

            int[] goalX = new int[GoalNum];
            goalX[0] = Initialgoal0X;
            goalX[1] = Initialgoal1X;
            goalX[2] = Initialgoal2X;

            int[] goalY = new int[GoalNum];
            goalY[0] = Initialgoal0Y;
            goalY[1] = Initialgoal1Y;
            goalY[2] = Initialgoal2Y;

            int[] blockX = new int[BlockNum];
            blockX[0] = InitialBlock0X;
            blockX[1] = InitialBlock1X;
            blockX[2] = InitialBlock2X;

            int[] blockY = new int[BlockNum];
            blockY[0] = InitialBlock0Y;
            blockY[1] = InitialBlock1Y;
            blockY[2] = InitialBlock2Y;


            const int RightEnd = MaxX + 1;
            const int LeftEnd = MinX - 1;
            const int DownEnd = MaxY + 1;
            const int UpEnd = MinY - 1;


            const string playerShape = "P";
            const string BoxShape = "B";
            const string GoalShape = "G";
            const string Block = "$";
            const string wallside = "|";
            const string wall = "-";



            while (true)
            {
                Console.Clear();
                //이전 프레임을 지우는 것
                //------------------------Render-----------------------

                Console.SetCursorPosition(playerX, playerY);
                Console.Write(playerShape);

                for (int count = 0; count < BoxNum; ++count)
                {
                    Console.SetCursorPosition(boxX[count], boxY[count]);
                    Console.Write(BoxShape);

                }

                for (int count = 0; count < GoalNum; ++count)
                {
                    Console.SetCursorPosition(goalX[count], goalY[count]);
                    Console.Write(GoalShape);
                }

                for (int count = 0; count < BlockNum; ++count)
                {
                    Console.SetCursorPosition(blockX[count], blockY[count]);
                    Console.Write(Block);
                }


                Console.SetCursorPosition(LeftEnd, UpEnd);
                for (int count = 0; count <= RightEnd - LeftEnd; ++count)
                {
                    Console.Write(wall);
                }
                Console.SetCursorPosition(LeftEnd, UpEnd);
                for (int count = 0; count <= DownEnd - UpEnd; ++count)
                {
                    Console.WriteLine(wallside);
                    Console.SetCursorPosition(LeftEnd, UpEnd + count);
                }
                Console.SetCursorPosition(LeftEnd, DownEnd);
                for (int count = 0; count <= RightEnd - LeftEnd; ++count)
                {
                    Console.Write(wall);
                    Console.SetCursorPosition(LeftEnd + count, DownEnd);
                }
                Console.SetCursorPosition(RightEnd, UpEnd);
                for (int count = 0; count <= DownEnd - UpEnd; ++count)
                {
                    Console.WriteLine(wallside);
                    Console.SetCursorPosition(RightEnd, UpEnd + count);
                }
                //------------------------ProcessInput-----------------

                ConsoleKey Key = Console.ReadKey().Key;

                //----------------------- Update ----------------------

                if (Key == (ConsoleKey.RightArrow))
                {
                    playerX = Math.Min(playerX + 1, MaxX);
                    playerDirection = Direction.Right;

                }

                if (Key == ConsoleKey.LeftArrow)
                {
                    playerX = Math.Max(MinX, playerX - 1);
                    playerDirection = Direction.Left;
                }

                if (Key == ConsoleKey.DownArrow)
                {
                    playerY = Math.Min(playerY + 1, MaxY);
                    playerDirection = Direction.Down;
                }

                if (Key == ConsoleKey.UpArrow)
                {
                    playerY = Math.Max(MinY, playerY - 1);
                    playerDirection = Direction.Up;

                }

                for (int count = 0; count < BoxNum; ++count)
                {
                    if (boxX[count] == playerX && boxY[count] == playerY)
                    {
                        switch (playerDirection)
                        {
                            case Direction.Right:
                                if (boxX[count] == MaxX)
                                {
                                    playerX = MaxX - 1;
                                }
                                else
                                {
                                    ++boxX[count];
                                }
                                break;
                            case Direction.Left:
                                if (boxX[count] == MinX)
                                {
                                    playerX = MinX + 1;
                                }
                                else
                                {
                                    --boxX[count];
                                }
                                break;
                            case Direction.Down:
                                if (boxY[count] == MaxY)
                                {
                                    playerY = MaxY - 1;
                                }
                                else
                                {
                                    ++boxY[count];
                                }
                                break;
                            case Direction.Up:
                                if (boxY[count] == MinY)
                                {
                                    playerY = MinY + 1;
                                }
                                else
                                {
                                    --boxY[count];
                                }
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine($"[Error] 플레이어의 이동방향이 잘못되었습니다. {playerDirection}");

                                return;
                        }

                    }
                }
                // 박스와 플레이어 충돌
                for (int count = 0; count < BlockNum; ++count)
                {
                    if (blockX[count] == playerX && blockY[count] == playerY)
                    {
                        switch (playerDirection)
                        {
                            case Direction.Right:
                                --playerX;
                                break;
                            case Direction.Left:
                                ++playerX;
                                break;
                            case Direction.Down:
                                --playerY;
                                break;
                            case Direction.Up:
                                ++playerY;
                                break;
                        }
                    }
                }
                //장애물과 플레이어 충돌
                for (int count1 = 0; count1 < BoxNum; ++count1)
                {
                    for (int count2 = 0; count2 < BlockNum; ++count2)
                    {
                        if (boxX[count1] == blockX[count2] && boxY[count1] == blockY[count2])
                        {
                            switch (playerDirection)
                            {
                                case Direction.Right:
                                    --playerX;
                                    --boxX[count1];
                                    break;
                                case Direction.Left:
                                    ++playerX;
                                    ++boxX[count1];
                                    break;
                                case Direction.Down:
                                    --playerY;
                                    --boxY[count1];
                                    break;
                                case Direction.Up:
                                    ++playerY;
                                    ++boxY[count1];
                                    break;
                            }
                        }
                    }
                }
                //장애물과 박스 충돌

                for (int count1 = 0; count1 < BoxNum - 1; ++count1)
                {
                    for (int count2 = count1 + 1; count2 < BoxNum; ++count2)
                    {
                        if (boxX[count1] == boxX[count2] && boxY[count1] == boxY[count2])
                        {
                            switch (playerDirection)
                            {
                                case Direction.Right:
                                    --playerX;
                                    --boxX[count1];
                                    break;
                                case Direction.Left:
                                    ++playerX;
                                    ++boxX[count1];
                                    break;
                                case Direction.Down:
                                    --playerY;
                                    --boxY[count1];
                                    break;
                                case Direction.Up:
                                    ++playerY;
                                    ++boxY[count1];
                                    break;
                            }
                        }
                    }
                }
                //박스끼리 충돌

                if (boxX[0] == goalX[0] && boxY[0] == goalY[0])
                {
                    if (boxX[1] == goalX[1] && boxY[1] == goalY[1])
                    {
                        if (boxX[2] == goalX[2] && boxY[2] == goalY[2])
                        {
                            Console.Clear();
                            Console.WriteLine("Game Clear! Press E to exit.");
                            break;
                        }
                    }
                    if (boxX[1] == goalX[2] && boxY[1] == goalY[2])
                    {
                        if (boxX[2] == goalX[1] && boxY[2] == goalY[1])
                        {
                            Console.Clear();
                            Console.WriteLine("Game Clear! Press E to exit.");
                            break;
                        }
                    }

                }
                if (boxX[0] == goalX[1] && boxY[0] == goalY[1])
                {
                    if (boxX[1] == goalX[0] && boxY[1] == goalY[0])
                    {
                        if (boxX[2] == goalX[2] && boxY[2] == goalY[2])
                        {
                            Console.Clear();
                            Console.WriteLine("Game Clear! Press E to exit.");
                            break;
                        }
                    }
                    if (boxX[1] == goalX[2] && boxY[1] == goalY[2])
                    {
                        if (boxX[2] == goalX[0] && boxY[2] == goalY[0])
                        {
                            Console.Clear();
                            Console.WriteLine("Game Clear! Press E to exit.");
                            break;
                        }
                    }

                }
                if (boxX[0] == goalX[2] && boxY[0] == goalY[2])
                {
                    if (boxX[1] == goalX[1] && boxY[1] == goalY[1])
                    {
                        if (boxX[2] == goalX[0] && boxY[2] == goalY[0])
                        {
                            Console.Clear();
                            Console.WriteLine("Game Clear! Press E to exit.");
                            break;
                        }
                    }
                    if (boxX[1] == goalX[0] && boxY[1] == goalY[0])
                    {
                        if (boxX[2] == goalX[1] && boxY[2] == goalY[1])
                        {
                            Console.Clear();
                            Console.WriteLine("Game Clear! Press E to exit.");
                            break;
                        }
                    }
                }
                //클리어 조건
            }
            while (true)
            {
                ConsoleKey Key = Console.ReadKey().Key;
                if (Key == ConsoleKey.E)
                {
                    return;
                }
            }
        }
    }
}

