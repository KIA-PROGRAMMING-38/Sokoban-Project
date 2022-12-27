// 초기 세팅
using System.Reflection;

Console.ResetColor();                           // 컬러를 초기화한다.
Console.CursorVisible = false;                  // 커서를 숨긴다.
Console.Title = "Sokoban";                      // 타이틀을 설정한다.
Console.BackgroundColor = ConsoleColor.Blue;    // 배경색을 설정한다.
Console.ForegroundColor = ConsoleColor.Yellow;  // 글꼴색을 설정한다.
Console.Clear();                                // 출력된 모든 내용을 지운다.

// 플레이어 좌표 설정
int playerX = 0;
int playerY = 0;


//박스 좌표 설정
int boxX = 5;
int boxY = 5;

// 가로 15 세로 10
// 게임 루프 == 프레임(Frame)


while (true)
{
    // 이전 프레임을 지운다.
    Console.Clear();

    // -------------------------- Render -------------------------------------
    // 플레이어 출력하기
    Console.SetCursorPosition(playerX,playerY);
    Console.Write("P");

    //박스 출력하기
    Console.SetCursorPosition(boxX, boxY);
    Console.Write("B");

    // -------------------------- ProcessInput -------------------------------
    ConsoleKey key = Console.ReadKey().Key;

    // -------------------------- Update -------------------------------------
    // 오른쪽 화살표키를 눌렀을 때
    if (key == ConsoleKey.RightArrow)
    {
        // 오른쪽으로 이동
        playerX = Math.Min(playerX + 1, 15);
    }
    
    if (key == ConsoleKey.LeftArrow)
    {
        // 왼쪽으로 이동
        playerX = Math.Max(0, playerX - 1);
    }

    if (key == ConsoleKey.UpArrow)
    {
        // 위로 이동
        playerY = Math.Max(0, playerY - 1);
    }
    
    if (key == ConsoleKey.DownArrow)
    {
        // 아래로 이동
        playerY = Math.Min(playerY + 1, 10);
    }

    // ----------------------------------------

    if (playerX == boxX && playerY == boxY)
    {
        if (key == ConsoleKey.RightArrow)
        {
            boxX = Math.Min(boxX + 1, 15);   
        }
    }

    if (playerX == boxX && playerY == boxY)
    {
        if (key == ConsoleKey.LeftArrow)
        {
            boxX = Math.Max(0, boxX - 1);
        }
    }

    if (playerX == boxX && playerY == boxY)
    {
        if (key == ConsoleKey.UpArrow)
        {
            boxY = Math.Max(0, boxY - 1);
        }
    }

    if (playerX == boxX && playerY == boxY)
    {
        if (key == ConsoleKey.DownArrow)
        {
            boxY = Math.Min(boxY + 1, 10);
        }

    }
}