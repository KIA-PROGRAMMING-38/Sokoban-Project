// 초기 세팅
using System.Reflection;

Console.ResetColor();                              // 컬러를 초기화한다
Console.CursorVisible = false;                     // 커서를 켜고 끄는 것, 불리언 타입이다
Console.Title = "홍성재의 파이어펀치";               // 타이틀 명을 바꾸어주는 이름
Console.BackgroundColor = ConsoleColor.DarkGreen;  // 배경색을 설정한다
Console.ForegroundColor = ConsoleColor.Yellow;     // 글꼴색을 설정한다
Console.Clear();                                   // 출력된 모든 내용을 지운다

// 플레이어 좌표 설정은 바깥에 해야한다
int playerAX = 0;
int playerAY = 0;

// 박스 구현
int boxX = 10;
int boxY = 6;

// 게임 루프 == 프레임(Frame)
while (true)
{
    Console.Clear();

    //-------------------------------------- Render -------------------------------------------------
    // 플레이어 출력하기
    Console.SetCursorPosition(playerAX, playerAY);
    Console.Write("A");

    // 박스 출력하기
    Console.SetCursorPosition(boxX, boxY);
    Console.Write("O");

    //-------------------------------------- ProcessInput -------------------------------------------
    ConsoleKeyInfo inputKey = Console.ReadKey();
    ConsoleKey key = inputKey.Key;
    // 이것은 ConsoleKey inputKey = Console.ReadKey().Key; 와 같다

    //-------------------------------------- Update -------------------------------------------------
    if (key == ConsoleKey.RightArrow)
    {
        playerAX = Math.Min(playerAX + 1, 30);
        
        if (playerAX == boxX && playerAY == boxY)
        {
            boxX = Math.Min(boxX + 1, 30);

            if (boxX == 30)
            {
                playerAX = 29;
            }
        }
    }
    
    if (key == ConsoleKey.LeftArrow && playerAX > 0)
    {
        playerAX = Math.Max(0, playerAX - 1);

        if (playerAX == boxX && playerAY == boxY)
        {
            boxX = Math.Max(0, boxX - 1);

            if (boxX == 0)
            {
                playerAX = 1;
            }
        }
    }
    
    if (key == ConsoleKey.DownArrow && playerAY < 20)
    {
        playerAY = Math.Min(playerAY + 1, 20);

        if (playerAX == boxX && playerAY == boxY)
        {
            boxY = Math.Min(playerAY + 1, 20);

            if (boxY == 20)
            {
                playerAY = 19;
            }
        }
    }
    
    if (key == ConsoleKey.UpArrow && playerAY > 0)
    {
        playerAY = Math.Max(0, playerAY - 1);

        if (playerAX == boxX && playerAY == boxY)
        {
            boxY = Math.Max(0, playerAY - 1);

            if (boxY == 0)
            {
                playerAY = 1;
            }
        }
    }
}