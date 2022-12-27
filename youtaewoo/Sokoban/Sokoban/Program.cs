//초기 세팅
Console.ResetColor(); // 컬러를 초기화한다.
Console.CursorVisible = false; // 커서 숨기기
Console.Title = "미래의babaisyou"; // 타이틀을 설정한다.
Console.BackgroundColor = ConsoleColor.DarkGreen; // 배경색을 설정한다.
Console.ForegroundColor = ConsoleColor.White; //글꼴색을 설정한다.
Console.Clear(); // 출력된 모든 내용을 지운다.


int PlayerX = 0;
int PlayerY = 0; // 얘는 바깥에다 해줘야한다. while문 안에 있으면 이동이 안된다.
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

    // 벽 만들기
    string[,] wall = new string[30, 15];
    int wallindexX = 0;
    int wallindexY = 0;
    if (wallindexX == 30 || wallindexY == 15)
    {
        wall[wallindexX, wallindexY] = "W";
        Console.WriteLine(wall);
    }
    else
        wall[wallindexX, wallindexY] = "";
        


    //------------------------------ProcessInput-----------------------------
    ConsoleKey key = Console.ReadKey().Key;

    //---------------------------------Update--------------------------------
    //오른쪽 화살표 키를 눌렀을 때를 먼저 한정해줄 것이다.
    if (key == ConsoleKey.RightArrow && PlayerX < 30)
    {
        //오른쪽으로 이동
        PlayerX += 1;
        if (PlayerX == BoxX && PlayerY == BoxY)
        {
            BoxX = PlayerX + 1;
        }
    }
    if (key == ConsoleKey.LeftArrow && PlayerX > 0) 
    {
        PlayerX -= 1;
        if (PlayerX == BoxX && PlayerY == BoxY)
        {
            BoxX = PlayerX - 1;
        }
    }
    if (key == ConsoleKey.DownArrow && PlayerY < 15)
    {
        PlayerY += 1;
        if (PlayerX == BoxX && PlayerY == BoxY)
        {
            BoxY = PlayerY + 1;
        }
    }
    if (key == ConsoleKey.UpArrow && PlayerY > 0) 
    {
        PlayerY -= 1;
        if (PlayerX == BoxX && PlayerY == BoxY)
        {
            BoxY = PlayerY - 1;
        }
    }
    

}