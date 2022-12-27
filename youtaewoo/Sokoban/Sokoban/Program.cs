//초기 세팅
Console.ResetColor(); // 컬러를 초기화한다.
Console.CursorVisible = false; // 커서 숨기기
Console.Title = "미래의babaisyou"; // 타이틀을 설정한다.
Console.BackgroundColor = ConsoleColor.DarkGreen; // 배경색을 설정한다.
Console.ForegroundColor = ConsoleColor.White; //글꼴색을 설정한다.
Console.Clear(); // 출력된 모든 내용을 지운다.

// 플레이어 좌표 설정
int PlayerX = 0;
int PlayerY = 0; // 얘는 바깥에다 해줘야한다. while문 안에 있으면 이동이 안된다.

// 박스 좌표 설정
int BoxX = 5;
int BoxY = 10;

// 게임 루프 == 프레임(frame)
while (true)
{
    // 이전 프레임을 지운다.
    Console.Clear();
    //---------------------------------Render--------------------------------
    // 플레이어 출력하기
    
    Console.SetCursorPosition(PlayerX, PlayerY);
    Console.Write("B");

    // 박스 출력하기
    Console.SetCursorPosition(BoxX, BoxY);
    Console.Write("O");

    //------------------------------ProcessInput-----------------------------
    ConsoleKey key = Console.ReadKey().Key;

    //---------------------------------Update--------------------------------
    //오른쪽 화살표 키를 눌렀을 때를 먼저 한정해줄 것이다.
    if (key == ConsoleKey.RightArrow && PlayerX < 30)
    {
        //오른쪽으로 이동
        
        if (PlayerX == BoxX - 1 && PlayerY == BoxY)
        {
            PlayerX = Math.Min(PlayerX + 1, 29);
            BoxX = Math.Min(PlayerX + 1, 30);
        }
        else
            PlayerX = Math.Min(PlayerX + 1, 30);
    }
    if (key == ConsoleKey.LeftArrow && PlayerX > 0) 
    {
        
        if (PlayerX == BoxX + 1 && PlayerY == BoxY)
        {
            PlayerX = Math.Max(1, PlayerX - 1);
            BoxX = Math.Max(0, PlayerX - 1);
        }
        else
            PlayerX = Math.Max(0, PlayerX - 1);
    }
    if (key == ConsoleKey.DownArrow && PlayerY < 15)
    {
        
        if (PlayerX == BoxX && PlayerY == BoxY - 1)
        {
            PlayerY = Math.Min(PlayerY + 1, 14);
            BoxY = Math.Min(PlayerY + 1, 15);
        }
        else
            PlayerY = Math.Min(PlayerY + 1, 15);
    }
    if (key == ConsoleKey.UpArrow && PlayerY > 0) 
    {
        
        if (PlayerX == BoxX && PlayerY == BoxY + 1 )
        {
            PlayerY = Math.Max(1, PlayerY - 1);
            BoxY = Math.Max(0, PlayerY - 1);
        }
        else
            PlayerY = Math.Max(0, PlayerY - 1);
    }   

}