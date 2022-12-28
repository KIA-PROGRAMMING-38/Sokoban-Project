﻿// 초기 세팅
Console.ResetColor();                                   // 컬러를 초기화한다.
Console.CursorVisible = false;                          // 커서를 숨긴다.
Console.Title = "경이루 아카데미";                       // 타이틀을 설정한다.
Console.BackgroundColor = ConsoleColor.Magenta;         // 배경색을 설정한다.
Console.ForegroundColor = ConsoleColor.Yellow;         // 글꼴색을 설정한다.
Console.Clear();                                       // 출력된 모든 내용을 지운다.

int playerX = 0;
int playerY = 0;
int playerDirection = 0; // 1 : Up, 2 : Down, 3 : Left, 4 : Right

int boxX = 10;
int boxY = 5;

// 게임 루프 == 프레임(Frame)
while (true)
{
    Console.Clear();

    // -------------------------------------- Render ------------------------------------------------
    Console.SetCursorPosition(playerX, playerY);
    Console.Write("P");
    Console.SetCursorPosition(boxX, boxY);
    Console.Write("B");
    // -------------------------------------- ProcessInput ------------------------------------------------
    ConsoleKey playerKey = Console.ReadKey().Key; // ConsoleKeyInfo keyInfo = Console.ReadKey(); ConsoleKey key = keyInfo.Key;
    // -------------------------------------- Update ------------------------------------------------
    // 플레이어
    if (playerKey == ConsoleKey.UpArrow)
    {
        playerY = Math.Max(0, playerY - 1);
        playerDirection = 1;
    }
    if (playerKey == ConsoleKey.DownArrow)
    {
        playerY = Math.Min(playerY + 1, 20);
        playerDirection = 2;
    }
    if (playerKey == ConsoleKey.LeftArrow)
    {
        playerX = Math.Max(0, playerX - 1);
        playerDirection = 3;
    }
    if (playerKey == ConsoleKey.RightArrow)
    {
        playerX = Math.Min(playerX + 1, 30);
        playerDirection = 4;
    }
    // 박스
    if (playerX == boxX && playerY == boxY)
    {
        switch (playerDirection)
        {
            case 1:
                if (boxY == 0)
                {
                    playerY = 1;
                }
                else
                {
                    --boxY;
                }
                break;
            case 2:
                if (boxY == 20)
                {
                    playerY = 19;
                }
                else
                {
                    ++boxY;
                }
                break;
            case 3:
                if (boxX == 0)
                {
                    playerX = 1;
                }
                else
                {
                    --boxX;
                }
                break;
            case 4:
                if (boxX == 30)
                {
                    playerX = 29;
                }
                else
                {
                    ++boxX;
                }
                break;
            default:
                Console.Clear();
                Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다.");
                return;

        }
        
        //if (playerDirection == 1)
        //{
        //    if (playerY == 0 && boxY == 0)
        //    {
        //        ++playerY;
        //    }
        //    boxY = Math.Max(0, boxY - 1);
        //}
        //if (playerDirection == 2)
        //{
        //    if (playerY == 20 && boxY == 20)
        //    {
        //        --playerY;
        //    }
        //    boxY = Math.Min(boxY + 1, 20);
        //}
        //if (playerDirection == 3)
        //{
        //    if (playerX == 0 && boxX == 0)
        //    {
        //        ++playerX;
        //    }
        //    boxX = Math.Max(0, boxX - 1);
        //}
        //if (playerDirection == 4)
        //{
        //    if (playerX == 30 && boxX == 30)
        //    {
        //        --playerX;
        //    }
        //    boxX = Math.Min(boxX + 1, 30);
        //}
    }

}