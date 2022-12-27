// 초기 세팅
Console.ResetColor();
Console.CursorVisible = false; // 커서를 숨긴다. 블리언 타입입니다.
Console.Title = "김도익 훈남"; // 타이틀을 설정한다.
Console.BackgroundColor = ConsoleColor.DarkBlue; // 배경색을 설정한다.
Console.ForegroundColor = ConsoleColor.White; // 글꼴색을 설정한다.
Console.Clear(); // 출력된 모든 내용을 지운다.

// 플레이어 좌표 설정
int playerX = 0;
int playerY = 0;
int BoxX = 3;
int BoxY = 3;

//게임루프 == 프레임
while (true)
{
    //이전 프레임 지운다.
    Console.Clear();

    //-------------------------------Render-------------------------------------------
    // 플레이어 출력
    Console.SetCursorPosition(playerX, playerY); // 커서의 위치를 세팅해준다. (글을 쓸때 깜빡깜빡하는 것을 커서라고 합니다.)
    Console.Write("K");

    Console.SetCursorPosition(BoxX, BoxY);
    Console.Write("B");

    //------------------------------ProcessInput-------------------------------------------
    ConsoleKey Key = Console.ReadKey().Key;
    ConsoleKey B = Console.ReadKey().Key;


    //----------------------------------Update-------------------------------------------
    //오른쪽 화살표 키를 눌렀을 때
    if(Key == ConsoleKey.RightArrow)
    {
        //오른쪽으로 이동
        playerX = Math.Min(playerX + 1, 15);
    }

    if(Key == ConsoleKey.LeftArrow)
    {
        //왼쪽으로 이동
        playerX = Math.Max(0, playerX -1);
    }

    if(Key == ConsoleKey.UpArrow)
    {
        playerY = Math.Max(0, playerY - 1);

    }

    if(Key == ConsoleKey.DownArrow)
    {
        playerY = Math.Min(playerY + 1, 10);
    }

 
}