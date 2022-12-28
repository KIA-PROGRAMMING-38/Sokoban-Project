// 초기 세팅
Console.ResetColor();                       // 컬러를 초기화한다.
Console.CursorVisible = false;              // 커서를 보이지 않게 해준다. 맥은 좀 보일수도..
Console.Title = "Sokoban";                  // 제목을 정해준다.
Console.BackgroundColor = ConsoleColor.Blue; // 백그라운드 색깔을 설정해준다.
Console.ForegroundColor = ConsoleColor.DarkGray; // 글꼴 색깔 지정.
Console.Clear(); // 출력된 모든 내용을 지운다.


// 플레이어 좌표 설정
int playerX = 0;
int playerY = 0;

//self
int boxX = 5;
int boxY = 5;

int playerDirection = 0;


// 게임 루프 == 프레임(Frame)
while (true)

{   // 이전 프레임을 지운다.
    Console.Clear();
    // ---------------------Render----------------

    Console.SetCursorPosition(playerX, playerY);
    Console.Write("H");

    // self
    Console.SetCursorPosition(boxX, boxY);
    Console.Write("O");

    // ---------------------ProcessInput-----------
    ConsoleKey key = Console.ReadKey().Key;
    // ---------------------Update-----------------

    if (key == ConsoleKey.LeftArrow)
    {
        playerX = Math.Max(0, playerX - 1);
        playerDirection = 1;
        //if (boxX > 0)
        //{
        //    if (playerX == boxX && playerY == boxY)
        //    {
        //        boxX -= 1;
        //    }
        //}
        //else if (playerX == boxX && playerY == boxY)
        //{
        //    playerX += 1;
        //}
    }

    // 오른쪽 화살표키를 눌렀을 때
    if (key == ConsoleKey.RightArrow)
    {
        // 오른쪽으로 이동 => 누를때마다 1씩 이동한다.
        playerX = Math.Min(playerX + 1, 15);
        playerDirection = 2;
        //if (boxX < 15)
        //{
        //    if (playerX == boxX && playerY == boxY)
        //    {
        //        boxX += 1;
        //    }
        //}
        //else if (playerX == boxX && playerY == boxY)
        //{

        //    playerX -= 1;
        //}
    }

    if (key == ConsoleKey.UpArrow)
    {
        playerY = Math.Max(0, playerY - 1);
        playerDirection = 3;
        //if (boxY > 0)
        //{
        //    if (playerX == boxX && playerY == boxY)
        //    {
        //        boxY -= 1;
        //    }
        //}
        //else if (playerX == boxX && playerY == boxY)
        //{
        //    playerY += 1;
        //}
    }

    if (key == ConsoleKey.DownArrow)
    {
        playerY = Math.Min(playerY + 1, 10);
        playerDirection = 4;
        //if (boxY < 10)
        //{
        //    if (playerX == boxX && playerY == boxY)
        //    {
        //        boxY += 1;
        //    }
        //}
        //else if (playerX == boxX && playerY == boxY)
        //{
        //    playerY -= 1;
        //}
    }

    //박스 update
    if (playerX == boxX && playerY == boxY)
    {
        switch (playerDirection)
        {
            case 1: //플레이어가 왼쪽으로 이동
               if (boxX == 0)
                {
                    playerX = 1;
                }
                else
                {
                    boxX -= 1;
                }
                break;
            case 2: //플레이어가 오른쪽으로 이동
                if (boxX == 15)
                {
                    playerX = 14;
                }
                else
                {
                    boxX += 1;
                }
                break;
            case 3: //플레이어가 위로 이동
                if (boxY == 0)
                {
                    playerY = 1;
                }
                else
                {
                    boxY -= 1;
                }
                break;
            case 4:
                if (boxY == 10)
                {
                    playerY = 9;
                }
                else
                {
                    boxY += 1;
                }
                break;
        }
    }


}

//int x = 20;
//int y = 3;
//Console.SetCursorPosition(x, y);
//Console.Write("H"); // 개행 하면 안된다.
//ConsoleKeyInfo keyInfo = Console.ReadKey();
//Console.WriteLine(keyInfo.Key);




