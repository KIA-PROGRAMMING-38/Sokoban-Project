using System;
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

        int playerX = 0;
        int playerY = 0;
        int playerDirection = 0; 

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
                playerX = Math.Min(playerX + 1, 15);
                playerDirection = 1;

            }

            if (Key == ConsoleKey.LeftArrow)
            {
                playerX = Math.Max(0, playerX - 1);
                playerDirection = 2;

            }

            if (Key == ConsoleKey.DownArrow)
            {
                playerY = Math.Min(playerY + 1, 15);
                playerDirection = 3;

            }

            if (Key == ConsoleKey.UpArrow)
            {
                playerY = Math.Max(0, playerY - 1);
                playerDirection = 4;

            }
            if (boxX == playerX && boxY == playerY)
            {
                switch(playerDirection)
                {
                    case 1:
                        if(boxX == 15)
                        {
                            playerX = 14;
                        }
                        else
                        {
                            ++boxX;
                        }
                        break;
                    case 2:
                        if(boxX == 0)
                        {
                            playerX = 1;
                        }
                        else
                        {
                            --boxX;
                        }
                        break;
                    case 3:
                        if(boxY == 15)
                        {
                            playerY = 14;
                        }
                        else
                        {
                            ++boxY;
                        }
                        break;
                    case 4:
                        if(boxY == 0)
                        {
                            playerY = 1;
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