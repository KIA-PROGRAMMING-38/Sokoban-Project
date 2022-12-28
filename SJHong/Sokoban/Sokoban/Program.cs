﻿using System;
class Program
{
    enum Direction
    {   
        None,
        Right,
        Left,
        Down,
        Up
    }

    enum Size
    {
        None,
        RightEnd = 15,
        LeftEnd = 0,
        DownEnd = 10,
        UpEnd = 0
    }
    static void Main()
    {
        Console.ResetColor();
        Console.CursorVisible = false;
        Console.Title = "소코반";
        Console.BackgroundColor = ConsoleColor.DarkCyan;
        Console.ForegroundColor = ConsoleColor.White;
        Console.Clear();

        int playerX = 0;
        int playerY = 0;
        Direction playerDirection = default;

        int boxX = 5;
        int boxY = 5;

        int goalX = 12;
        int goalY = 7;
        while (true)
        {
            Console.Clear();
            //이전 프레임을 지우는 것
            //------------------------Render-----------------------

            Console.SetCursorPosition(playerX, playerY);
            Console.Write("P");
            Console.SetCursorPosition(boxX, boxY);
            Console.Write("N");
            Console.SetCursorPosition(goalX, goalY);
            Console.Write("G");
            //------------------------ProcessInput-----------------

            ConsoleKey Key = Console.ReadKey().Key;


            //----------------------- Update ----------------------

            if (Key == ConsoleKey.RightArrow)
            {
                playerX = Math.Min(playerX + 1, (int)Size.RightEnd);
                playerDirection = Direction.Right;

            }

            if (Key == ConsoleKey.LeftArrow)
            {
                playerX = Math.Max((int)Size.LeftEnd, playerX - 1);
                playerDirection = Direction.Left;

            }

            if (Key == ConsoleKey.DownArrow)
            {
                playerY = Math.Min(playerY + 1, (int)Size.DownEnd);
                playerDirection = Direction.Down;

            }

            if (Key == ConsoleKey.UpArrow)
            {
                playerY = Math.Max((int)Size.UpEnd, playerY - 1);
                playerDirection = Direction.Up;

            }
            if (boxX == playerX && boxY == playerY)
            {
                switch (playerDirection)
                {
                    case Direction.Right:
                        if (boxX == (int)Size.RightEnd)
                        {
                            playerX = (int)Size.RightEnd - 1;
                        }
                        else
                        {
                            ++boxX;
                        }
                        break;
                    case Direction.Left:
                        if (boxX == (int)Size.LeftEnd)
                        {
                            playerX = (int)Size.LeftEnd + 1;
                        }
                        else
                        {
                            --boxX;
                        }
                        break;
                    case Direction.Down:
                        if (boxY == (int)Size.DownEnd)
                        {
                            playerY = (int)Size.DownEnd - 1;
                        }
                        else
                        {
                            ++boxY;
                        }
                        break;
                    case Direction.Up:
                        if (boxY == (int)Size.UpEnd)
                        {
                            playerY = (int)Size.UpEnd + 1;
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
            if (boxX == goalX && boxY == goalY)
            {
                Console.Clear();
                Console.WriteLine("Game Clear!");
                break;
            }
        }
    }
}