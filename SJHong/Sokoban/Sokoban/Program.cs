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

            //------------------------ProcessInput-----------------

            ConsoleKey Key = Console.ReadKey().Key;


            //----------------------- Update ----------------------
            //오른쪽 화살표를 눌렀을 때

            if (Key == ConsoleKey.RightArrow)
            {
                playerX = Math.Min(playerX + 1, 15);
                if (playerX == boxX && playerY == boxY)
                {
                    if (boxX < 15)
                    {
                        boxX += 1;
                    }
                    else if (boxX == 15)
                    {
                        playerX -= 1;
                    }
                }

            }
            if (Key == ConsoleKey.LeftArrow)
            {
                playerX = Math.Max(0, playerX - 1);
                if (playerX == boxX && playerY == boxY)
                {
                    if (boxX > 0)
                    {
                        boxX -= 1;
                    }
                    else if (boxX == 0)
                    {
                        playerX += 1;
                    }
                }
            }

            if (Key == ConsoleKey.DownArrow)
            {
                playerY = Math.Min(playerY + 1, 15);
                if (playerX == boxX && playerY == boxY)
                {
                    if (boxY < 15)
                    {
                        boxY += 1;
                    }
                    else if (boxY == 15)
                    {
                        playerY -= 1;
                    }
                }
            }


            if (Key == ConsoleKey.UpArrow)
            {
                playerY = Math.Max(0, playerY - 1);
                if (playerX == boxX && playerY == boxY)
                {
                    if (boxY > 0)
                    {
                        boxY -= 1;
                    }
                    else if (boxY == 0)
                    {
                        playerY += 1;
                    }
                }
            }

        }
    }
}