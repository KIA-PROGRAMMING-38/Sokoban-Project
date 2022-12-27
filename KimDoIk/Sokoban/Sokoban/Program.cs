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

//박스 좌표 설정
int BoxX = 1;
int BoxY = 5;

// 게임루프 == 프레임
// 가로 15, 세로 10으로 제한
while (true)
{
    //이전 프레임 지운다.
    Console.Clear();

    //-------------------------------Render-------------------------------------------
    // 플레이어 출력
    Console.SetCursorPosition(playerX, playerY); // 커서의 위치를 세팅해준다. (글을 쓸때 깜빡깜빡하는 것을 커서라고 합니다.)
    Console.Write("K");

    // 박스 출력하기
    Console.SetCursorPosition(BoxX, BoxY);
    Console.Write("B");

    //------------------------------ProcessInput-------------------------------------------
    ConsoleKey Key = Console.ReadKey().Key; //키를 입력받기 위해서 만든 명령어
    


    //----------------------------------Update-------------------------------------------
    //오른쪽 화살표 키를 눌렀을 때
    if(Key == ConsoleKey.RightArrow)
    {
        // 오른쪽으로 이동
        // K가 B를 오른쪽으로 밀었을때 오른쪽으로 간다.
        if(playerX == BoxX-1 && playerY == BoxY)
        {
            BoxX += 1;
        }
        playerX = Math.Min(playerX + 1, 14);
        BoxX = Math.Min(BoxX + 1, 15);
    }

    if(Key == ConsoleKey.LeftArrow)
    {
        //왼쪽으로 이동
        // K가 B를 왼쪽으로 밀었을때 왼쪽으로 간다.
        if (playerX == BoxX + 1 && playerY == BoxY)
        {
            BoxX -= 1;
        }
        playerX = Math.Max(1, playerX -1);
        BoxX = Math.Max(0, BoxX - 1);
    }

    if(Key == ConsoleKey.UpArrow)
    {
        //위로 이동
        // K가 B를 위쪽으로 밀었을때 위쪽으로 간다.
        if(playerX == BoxX && playerY == BoxY + 1)
        {
            BoxY -= 1;
        }
        playerY = Math.Max(1, playerY - 1);
       

    }

    if(Key == ConsoleKey.DownArrow)
    {
        //아래로 이동
        // K가 B를 아래쪽으로 밀었을때 아래쪽으로 간다.
        if (playerX == BoxX && playerY == BoxY - 1)
        {
            BoxY += 1;
        }
        playerY = Math.Min(playerY + 1, 9);
        

    }

 
  
   
}