// 초기 세팅
using System.Reflection;

Console.ResetColor();                              // 컬러를 초기화한다
Console.CursorVisible = false;                     // 커서를 켜고 끄는 것, 불리언 타입이다
Console.Title = "홍성재의 파이어펀치";               // 타이틀 명을 바꾸어주는 이름
Console.BackgroundColor = ConsoleColor.DarkGreen;  // 배경색을 설정한다
Console.ForegroundColor = ConsoleColor.Yellow;     // 글꼴색을 설정한다
Console.Clear();                                   // 출력된 모든 내용을 지운다

// 플레이어 좌표 설정은 바깥에 해야한다
int playerX = 0;
int playerY = 0;

// 박스 구현
int boxX = 5;
int boxY = 3;

// 게임 루프 == 프레임(Frame)
while (true)
{
    Console.Clear();

    //-------------------------------------- Render -------------------------------------------------
    // 플레이어 출력하기
    Console.SetCursorPosition(playerX, playerY);
    Console.Write("P");

    Console.SetCursorPosition(boxX, boxY);
    Console.Write("O");

    //-------------------------------------- ProcessInput -------------------------------------------
    ConsoleKeyInfo inputKey = Console.ReadKey();
    ConsoleKey key = inputKey.Key;
    // 이것은 ConsoleKey inputKey = Console.ReadKey().Key; 와 같다

    //-------------------------------------- Update -------------------------------------------------
    // 오른쪽 화살표키를 눌렀을 때
    if (key == ConsoleKey.RightArrow && playerX < 31)
    {
        // 오른쪽으로 이동
        // playerX = Math.Min(playerX + 1, 30);
        playerX += 1;
        
        if (playerX == boxX && playerY == boxY)
        {
            boxX += 1;
        }
    }
    
    if (key == ConsoleKey.LeftArrow && playerX > -1)
    {
        // playerX = Math.Max(0, playerX - 1);
        playerX -= 1;

        if (playerX == boxX && playerY == boxY)
        {
            boxX -= 1;
        }
    }
    
    if (key == ConsoleKey.DownArrow)
    {
        // playerY = Math.Min(playerY + 1, 30);
        playerY += 1;

        if (playerX == boxX && playerY == boxY)
        {
            boxY += 1;
        }
    }
    
    if (key == ConsoleKey.UpArrow)
    {
        // playerY = Math.Max(0, playerY - 1);
        playerY -= 1;

        if (playerX == boxX && playerY == boxY)
        {
            boxY -= 1;
        }
    }
}