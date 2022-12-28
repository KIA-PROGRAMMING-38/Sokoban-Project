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

    //-------------------------------Render------------------------------------------- //플레이어가 무슨 일이 일어났는지 볼 수 있도록 게임을 그립니다
    // 플레이어 출력
    Console.SetCursorPosition(playerX, playerY); // 커서의 위치를 세팅해준다. (글을 쓸때 깜빡깜빡하는 것을 커서라고 합니다.)
    Console.Write("K");

    // 박스 출력하기
    Console.SetCursorPosition(BoxX, BoxY);
    Console.Write("B");

    //------------------------------ProcessInput------------------------------------------- // 마지막 호출 이후 발생한 모든 사용자 입력을 처리합니다.
    ConsoleKey Key = Console.ReadKey().Key; //키를 입력받기 위해서 만든 명령어



    //----------------------------------Update------------------------------------------- // 게임 시뮬레이션을 한 단계 진행합니다.
    //오른쪽 화살표 키를 눌렀을 때
    if (Key == ConsoleKey.RightArrow)
    {
        playerX = Math.Min(playerX + 1, 15); // Math.Min(A,B) A와 B중 더 작은 놈을 고르겠다.(동일 값 포함)



        // 오른쪽으로 이동
        // player와 Box의 x,y 좌표가 같을 때, box의 x좌표는 player보다 1칸 더 많다.
        if (playerX == BoxX && playerY == BoxY)
        {
            //p(0,0)b(1,0)
            BoxX = playerX + 1;

            //box가 제한선에 도달하면 player가 box뒤에 멈춘다.
            if (BoxX > 15)
            {
                BoxX = playerX;
                playerX = BoxX - 1;
            }

        }


    }

    if (Key == ConsoleKey.LeftArrow)
    {
        playerX = Math.Max(0, playerX - 1); // Math.Max(A, B) A와 B중 더 큰놈을 고르겠다.
        //왼쪽으로 이동
        // player와 Box의 x,y 좌표가 같을 때, box의 x좌표는 player보다 1칸 더 적어야한다.
        if (playerX == BoxX && playerY == BoxY)
        {
            //b(0,0)p(1,0)
            BoxX = playerX - 1;

            //box가 제한선에 도달하면 player가 box뒤에 멈춘다.
            if (BoxX < 0)
            {
                BoxX = playerX;
                playerX = BoxX + 1;
            }
        }


    }

    if (Key == ConsoleKey.UpArrow)
    {
        playerY = Math.Max(0, playerY - 1);
        //위로 이동
        // player와 Box의 x,y 좌표가 같을 때, box의 y좌표는 player보다 1칸 더 적어야한다.
        if (playerX == BoxX && playerY == BoxY)
        {
            // b(0,1)
            // p(0,2)
            BoxY = playerY - 1;

            //box가 제한선에 도달하면 player가 box뒤에 멈춘다.
            if (BoxY < 0)
            {
                BoxY = playerY;
                playerY = BoxY + 1;
            }
        }




    }

    if (Key == ConsoleKey.DownArrow)
    {
        playerY = Math.Min(playerY + 1, 10);

        //아래로 이동
        // player와 Box의 x,y 좌표가 같을 때, box의 y좌표는 player보다 1칸 더 많아야한다.
        if (playerX == BoxX && playerY == BoxY)
        {
            // p(0,1)
            // b(0,2)
            BoxY = playerY + 1;

            //box가 제한선에 도달하면 player가 box뒤에 멈춘다.
            if (BoxY > 10)
            {
                BoxY = playerY;
                playerY = BoxY - 1;
            }
        }



    }
}