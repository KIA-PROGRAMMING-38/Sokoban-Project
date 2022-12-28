// 초기 세팅
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
}