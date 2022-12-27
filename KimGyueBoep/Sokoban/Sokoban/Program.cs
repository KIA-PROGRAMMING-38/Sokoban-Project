// 초기 세팅
using System.Runtime.CompilerServices;

Console.ResetColor();                                 // 컬러를 초기화한다.
Console.CursorVisible = false;                        // 커서를 숨긴다.
Console.Title = "소코반 프로젝트";                     // 타이틀을 설정한다.
Console.BackgroundColor = ConsoleColor.Green;         // 배경색을 설정한다.
Console.ForegroundColor = ConsoleColor.Red;           // 글꼴색을 설정한다.
Console.Clear();                                     // 출력된 모든 내용을 지운다.

// 플레이어 좌표 설정
int playerX = 0;
int playerY = 0;

int BoxX = 2;
int BoxY = 1;
// 가로 15, 세로 10
// 게임 루프 == 프레임(Frame)
while (true)
{
    Console.Clear();
    // ---------------------------------- Render ----------------------------------
    // 플레이어 출력하기
    Console.SetCursorPosition(playerX, playerY);
    Console.Write('▶');
    Console.SetCursorPosition(BoxX, BoxY);
    Console.Write('■');

    // ---------------------------------- ProcessInput ----------------------------------
    ConsoleKey key = Console.ReadKey().Key;

    // ---------------------------------- Update ----------------------------------
    // 오른쪽 화살표키를 눌렀을 때
    if (key == ConsoleKey.RightArrow)
    {
        // 오른쪽으로 이동
        playerX = Math.Min(playerX + 1, 15);
    }
    if (key == ConsoleKey.LeftArrow)
    {
        playerX = Math.Max(0, playerX - 1);
    }
    if (key == ConsoleKey.DownArrow)
    {
        playerY = Math.Min(playerY + 1, 10);
    }
    if (key == ConsoleKey.UpArrow)
    {
        playerY = Math.Max(0, playerY - 1);
    }

    if (playerX == BoxX && playerY == BoxY && key == ConsoleKey.RightArrow)
    {
        BoxX++;
        if (BoxX > 15)
        {
            BoxX = 15;
            playerX -= 1;
        }
    }
    if (playerX == BoxX && playerY == BoxY && key == ConsoleKey.LeftArrow)
    {
        BoxX--;
        if(BoxX < 0)
        {
            BoxX = 0;
            playerX += 1;
        }
    }
    if (playerY == BoxY && playerX == BoxX && key == ConsoleKey.DownArrow)
    {
        BoxY++;
        if (BoxY > 10)
        {
            BoxY = 10;
            playerY -= 1;
        }
    }
    if (playerY == BoxY && playerX == BoxX && key == ConsoleKey.UpArrow)
    {
        BoxY--;
        if (BoxY < 0)
        {
            BoxY = 0;
            playerY += 1;
        }
    }



}