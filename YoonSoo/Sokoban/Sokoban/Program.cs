// 초기
using System.Runtime.CompilerServices;

Console.ResetColor();                              // 컬러를 초기화한다.
Console.CursorVisible = false;                     // 커서를 숨긴다.
Console.Title = "어우석 바보";                      // 타이틀을 설정한다.
Console.BackgroundColor = ConsoleColor.DarkGreen;  // 배경을 설정한다.
Console.ForegroundColor = ConsoleColor.Black;      //글꼴을 설정한다.
Console.Clear();                                   // 출려된 모든 내용을 지운다.

int playerX = 0;
int playerY = 0;

int boxX = 1;
int boxY = 1;


// 가로 15 새로 10
// 게임 루프 == 프레임(Frame)
while (true)
{
    // 이전 프레임을 지운다.
    Console.Clear();


    // -----------------------------------------------------Render-----------------------------------------------------
    // 플레이어 출력하기
    Console.SetCursorPosition(playerX, playerY);
    Console.Write("P");

    // 박스 출력하기
    Console.SetCursorPosition(boxX, boxY);
    Console.Write("B");

    // --------------------------------------------------ProcessINput--------------------------------------------------
    ConsoleKey key = Console.ReadKey().Key;


    // -----------------------------------------------------Update-----------------------------------------------------
    // 오른쪽 화살표키를 눌렀을 때
    if (key == ConsoleKey.RightArrow)
    {
        playerX = Math.Min(playerX + 1, 15);
        if (playerX == boxX && playerY == boxY)
        {
            boxX = playerX + 1;
        }
    }

    if (key == ConsoleKey.DownArrow)
    {
        playerY = Math.Min(playerY + 1, 15);
        if (playerY == boxY && playerX == boxX)
        {
            boxY = playerY + 1;
        }
    }


    if (key == ConsoleKey.UpArrow)
    {
        playerY = Math.Max(0, playerY - 1);
        if (playerY == boxY && playerX == boxX)
        {
            boxY = playerY - 1;
        }
    }


    if (key == ConsoleKey.LeftArrow)
    {
        playerX = Math.Max(0, playerX - 1);
        if (playerX == boxX && playerY == boxY)
        {
            boxX = playerX - 1;
        }
    }

}