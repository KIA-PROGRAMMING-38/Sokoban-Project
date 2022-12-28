﻿// 초기 세팅
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

// map 설정 => 가로 15(가로가 조금 더 짧아서 더 길게 설정해주는것) 세로 10

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

    // 오른쪽 화살표키를 눌렀을 때
    if (key == ConsoleKey.RightArrow)
    {
        // 오른쪽으로 이동 => 누를때마다 1씩 이동한다.
        playerX = Math.Min(playerX + 1, 15);
        if (boxX < 15)
        {
            if (playerX == boxX && playerY == boxY)
            {
                boxX += 1;
            }
        }
        else if (playerX == boxX && playerY == boxY)
        {

            playerX -= 1;
        }
    }

    if (key == ConsoleKey.LeftArrow)
    {
        playerX = Math.Max(0, playerX - 1);
        if (boxX > 0)
        {
            if (playerX == boxX && playerY == boxY)
            {
                boxX -= 1;
            }
        }
        else if (playerX == boxX && playerY == boxY)
        {
            playerX += 1;
        }
    }

    if (key == ConsoleKey.DownArrow)
    {
        playerY = Math.Min(playerY + 1, 10);
        if (boxY < 10)
        {
            if (playerX == boxX && playerY == boxY)
            {
                boxY += 1;
            }
        }
        else if (playerX == boxX && playerY == boxY)
        {
            playerY -= 1;
        }
    }

    if (key == ConsoleKey.UpArrow)
    {
        playerY = Math.Max(0, playerY - 1);
        if (boxY > 0)
        {
            if (playerX == boxX && playerY == boxY)
            {
                boxY -= 1;
            }
        }
        else if (playerX == boxX && playerY == boxY)
        {
            playerY += 1;
        }
    }

    //self 박스 update



}

//int x = 20;
//int y = 3;
//Console.SetCursorPosition(x, y);
//Console.Write("H"); // 개행 하면 안된다.
//ConsoleKeyInfo keyInfo = Console.ReadKey();
//Console.WriteLine(keyInfo.Key);




