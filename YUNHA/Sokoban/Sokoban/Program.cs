// 초기 세팅
Console.ResetColor();                                   // 컬러를 초기화한다.
Console.CursorVisible = false;                          // 커서를 숨긴다.
Console.Title = "Sokoban";                              // 타이틀을 설정한다.
Console.BackgroundColor = ConsoleColor.DarkBlue;        // 배경색을 설정한다.
Console.ForegroundColor = ConsoleColor.Yellow;          // 글꼴색을 설정한다.
Console.Clear();                                        // 출력된 모든 내용을 지운다.

// 플레이어 좌표 설정
int playerX = 0, playerY = 0;
int boxX = 5, boxY = 5;

// 가로 15, 세로 10
// 게임 루프 == 프레임(Frame)
while (true)
{
    // 이전 프레임을 지운다.
    Console.Clear();

    // -------------------------------------Render------------------------------------- 
    // 플레이어 출력하기
    Console.SetCursorPosition(playerX, playerY);
    Console.Write("K");
    Console.SetCursorPosition(boxX, boxY);
    Console.Write("B");


    // ----------------------------------ProcessInput---------------------------------- 
    ConsoleKey key = Console.ReadKey().Key;


    // -------------------------------------Update------------------------------------- 
    // 오른쪽 키를 눌렀을 때
    if (key == ConsoleKey.RightArrow)
    {
        // 오른쪽으로 이동
        if(playerX == boxX - 1 && playerY == boxY)
        {
            playerX = Math.Min(playerX + 1, 14);
            boxX = Math.Min(boxX + 1, 15);
        }
        else
        {
            playerX = Math.Min(playerX + 1, 15);
        }

    }
    // 아래쪽 키를 눌렀을 때
    if (key == ConsoleKey.DownArrow)
    {
        // 아래로 이동
        if(playerY == boxY - 1 && playerX == boxX)
        {
            playerY = Math.Min(playerY + 1, 9);
            boxY = Math.Min(boxY + 1, 10);
        }
        else
        {
            playerY = Math.Min(playerY + 1, 10);
        }

    }
    // 왼쪽 키를 눌렀을 때
    if (key == ConsoleKey.LeftArrow)
    {
        // 왼쪽으로 이동
        if(playerX == boxX + 1 && playerY == boxY)
        {
            playerX = Math.Max(1, playerX - 1);
            boxX = Math.Max(0, boxX - 1);
        }
        else
        {
            playerX = Math.Max(0, playerX - 1);
        }

    }
    // 위쪽 키를 눌렀을 때
    if (key == ConsoleKey.UpArrow)
    {
        // 위로 이동
        if(playerY == boxY + 1 && playerX == boxX)
        {
            playerY = Math.Max(1, playerY - 1);
            boxY = Math.Max(0, boxY - 1);
        }
        else
        {
            playerY = Math.Max(0, playerY - 1);
        }
    }


}
