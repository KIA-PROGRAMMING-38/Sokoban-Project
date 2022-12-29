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

            const int InitialboxX = 11;
            const int InitialboxY = 11;

            const int InitialgoalX = 12;
            const int InitialgoalY = 7;

            const int InitialBlockX = 10;
            const int InitialBlockY = 10;

            const int MaxX = 20;
            const int MinX = 1;
            const int MaxY = 16;
            const int MinY = 1;

            int playerX = InitialplayerX;
            int playerY = InitialplayerY;

            int boxX = InitialboxX;
            int boxY = InitialboxY;

            int goalX = InitialgoalX;
            int goalY = InitialgoalY;

            int blockX = InitialBlockX;
            int blockY = InitialBlockY;

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
                Console.SetCursorPosition(boxX, boxY);
                Console.Write(BoxShape);
                Console.SetCursorPosition(goalX, goalY);
                Console.Write(GoalShape);
                Console.SetCursorPosition(blockX, blockY);
                Console.Write(Block);

                Console.SetCursorPosition(LeftEnd, UpEnd);
                for (int count = 0; count <= RightEnd - LeftEnd; ++count)
                {
                    Console.Write(wall);
                }
                Console.SetCursorPosition(MinX - 1, MinY - 1);
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
                if (boxX == playerX && boxY == playerY)
                {
                    switch (playerDirection)
                    {
                        case Direction.Right:
                            if (boxX == MaxX)
                            {
                                playerX = MaxX - 1;
                            }
                            else
                            {
                                ++boxX;
                            }
                            break;
                        case Direction.Left:
                            if (boxX == MinX)
                            {
                                playerX = MinX + 1;
                            }
                            else
                            {
                                --boxX;
                            }
                            break;
                        case Direction.Down:
                            if (boxY == MaxY)
                            {
                                playerY = MaxY - 1;
                            }
                            else
                            {
                                ++boxY;
                            }
                            break;
                        case Direction.Up:
                            if (boxY == MinY)
                            {
                                playerY = MinY + 1;
                            }
                            else
                            {
                                --boxY;
                            }
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine($"[Error] 플레이어의 이동방향이 잘못되었습니다. {playerDirection}");

                            return;
                    }

                }
                if (blockX == playerX && blockY == playerY)
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
                if (blockX == boxX && blockY == boxY)
                {
                    switch (playerDirection)
                    {
                        case Direction.Right:
                            --playerX;
                            --boxX;
                            break;
                        case Direction.Left:
                            ++playerX;
                            ++boxX;
                            break;
                        case Direction.Down:
                            --playerY;
                            --boxY;
                            break;
                        case Direction.Up:
                            ++playerY;
                            ++boxY;
                            break;
                    }
                }

                if (boxX == goalX && boxY == goalY)
                {
                    Console.Clear();
                    Console.WriteLine("Game Clear!");
                    break;
                }
            }
        }
    }
}