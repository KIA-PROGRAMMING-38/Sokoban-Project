// 초기 세팅
Console.ResetColor(); // 컬러를 초기화한다
Console.CursorVisible = false; // 커서를 안보이게 해줌(콘솔에서는 입력하려는 표시인거임)
Console.Title = "소코반 게임!"; // 콘솔창의 제목을 입력
Console.BackgroundColor = ConsoleColor.Cyan; // 배경색을 바꿔줌
Console.ForegroundColor = ConsoleColor.Red; // 글씨의 색을 바꿔줌
Console.Clear(); // 출력된 모든 내용을 지운다

int playerX = 0;
int playerY = 0;

int boxX = 2;
int boxY = 2;
// 가로 15, 세로 10
 
// 게임 루프 == 프레임(Frame)
while(true)
{

    Console.Clear();

    // ------------------------------------------ render -----------------------------------------------
    // 플레이어 출력하기
    
    Console.SetCursorPosition(playerX, playerY);
    Console.Write("R");

    
    Console.SetCursorPosition(boxX, boxY);
    Console.Write("B");


    // ------------------------------------------ ProcessInput -----------------------------------------

    ConsoleKey key = Console.ReadKey().Key;



    // ------------------------------------------ Update -----------------------------------------------
    
    if (key == ConsoleKey.RightArrow)
    {
        // 오른쪽으로 이동
        playerX = Math.Min(playerX + 1, 20);


        if(boxX == playerX && boxY==playerY)
        {
            boxX = playerX + 1;

            if(20<boxX)
            {
                boxX = boxX - 1;
                playerX = boxX - 1;
                
            }
            
        }

        

    }
    
    if (key == ConsoleKey.LeftArrow )
    {
        playerX = Math.Max(0, playerX - 1);

        if (boxX == playerX && boxY == playerY)
        {
            boxX = playerX -1;

            if (boxX < 0)
            {
                boxX = boxX + 1;
                playerX = boxX + 1;

            }

        }
    }

    if (key == ConsoleKey.DownArrow)
    {
        playerY = Math.Min(playerY+1, 15);

        if (boxX == playerX && boxY == playerY)
        {
            boxY = playerY + 1;

            if (15 < boxY)
            {
                boxY = boxY - 1;
                playerY = boxY - 1;

            }

        }
    }

    if (key == ConsoleKey.UpArrow)
    {
        playerY = Math.Max(0, playerY - 1);

        if (boxX == playerX && boxY == playerY)
        {
            boxY = playerY - 1;

            if (boxY < 0)
            {
                boxY = boxY + 1;
                playerY = boxY + 1;

            }
        }
    }


}


