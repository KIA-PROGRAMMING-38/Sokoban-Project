// 초기 세팅
Console.ResetColor();                                   // 컬러를 초기화한다.
Console.CursorVisible = false;                          // 커서를 숨긴다.
Console.Title = "경이루 아카데미";                       // 타이틀을 설정한다.
Console.BackgroundColor = ConsoleColor.Magenta;         // 배경색을 설정한다.
Console.ForegroundColor = ConsoleColor.Yellow;         // 글꼴색을 설정한다.
Console.Clear();                                       // 출력된 모든 내용을 지운다.

int playerX = 2;
int playerY = 2;
int boxX = 15;
int boxY = 10;

// 게임 루프 == 프레임(Frame)
while (true)
{
    Console.Clear(); // 이전 프레임을 지운다.

    // -------------------------------------- Render ------------------------------------------------
    // 플레이어 출력하기
    Console.SetCursorPosition(playerX, playerY);
    Console.Write("P");
    // 박스 출력하기
    Console.SetCursorPosition(boxX, boxY);
    Console.WriteLine("B");
    // -------------------------------------- ProcessInput ------------------------------------------------
    ConsoleKey playerKey = Console.ReadKey().Key; // ConsoleKeyInfo keyInfo = Console.ReadKey(); ConsoleKey key = keyInfo.Key;
    // -------------------------------------- Update ------------------------------------------------
    if (playerKey == ConsoleKey.UpArrow) // 위쪽 화살표키를 눌렀을 때
    {
        if(playerX == boxX && playerY == boxY + 1)
        {
            if(playerY == 3 && boxY == 2)
            {
                continue;
            }
            boxY = Math.Max(2, boxY - 1);
        }
        playerY = Math.Max(2, playerY - 1); // 위로 이동
    }

    if (playerKey == ConsoleKey.DownArrow) // 아래쪽 화살표키를 눌렀을 때
    {
        if (playerX == boxX && playerY == boxY - 1)
        {
            if (playerY == 19 && boxY == 20)
            {
                continue;
            }
            boxY = Math.Min(boxY + 1, 20);
        }
            playerY = Math.Min(playerY + 1, 20); // 아래로 이동
    }

    if (playerKey == ConsoleKey.LeftArrow) // 왼쪽 화살표키를 눌렀을 때
    {
        if (playerX == boxX + 1 && playerY == boxY)
        {
            if (playerX == 3 && boxX == 2)
            {
                continue;
            }
            boxX = Math.Max(2, boxX - 1);
        }
            playerX = Math.Max(2, playerX - 1); // 왼쪽으로 이동
    }

    if (playerKey == ConsoleKey.RightArrow) // 오른쪽 화살표키를 눌렀을 때
    {
        if (playerX == boxX - 1 && playerY == boxY)
        {
            if (playerX == 29 && boxX == 30)
            {
                continue;
            }
            boxX = Math.Min(boxX + 1, 30);
        }
            playerX = Math.Min(playerX + 1, 30); // 오른쪽으로 이동
    }
}
