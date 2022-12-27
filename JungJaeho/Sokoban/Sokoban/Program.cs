// 초기세팅
Console.ResetColor();                               // 컬러 초기화
Console.CursorVisible = false;                      // 커서 비활성화
Console.Title = "던전앤파이터";                     // 타이틀 설정
Console.BackgroundColor = ConsoleColor.DarkGray;        // 배경색 설정
Console.ForegroundColor = ConsoleColor.DarkCyan;    // 전경색 설정
Console.Clear();                                    // 콘솔창 클리어
/*-------------------------------------------------*/



int mapWidth = 35;
int mapHeight = 10;

#region Player
int playerX = 0;
int playerY = 0;
int prevPlayerX = 0;
int prevPlayerY = 0;


char player = 'Д';
#endregion

#region Box
int prevBoxPosX = 0;
int prevBoxPosY = 0;
int boxPosX = 5;
int boxPosY = 5;
char box = '▥'; //▥ Д
#endregion

// 힌트
// 게임 루프 == 프레임(Frame)
while (true)
{
    Console.Clear();
    /*---------------------- Render ---------------------- */
    Console.SetCursorPosition(playerX, playerY);
    Console.Write(player);
    Console.SetCursorPosition(boxPosX, boxPosY);
    Console.Write(box);
    /*------------------- ProcessInput ------------------- */
    ConsoleKey key = Console.ReadKey().Key;


    /*---------------------- Update ---------------------- */
    
    prevPlayerX = playerX;
    prevPlayerY = playerY;
    prevBoxPosX = boxPosX;
    prevBoxPosY = boxPosY;
    if (key == ConsoleKey.RightArrow)
    {
        playerX = Math.Min(playerX + 1, mapWidth);
    }
    if (key == ConsoleKey.LeftArrow)
    {
        playerX = Math.Max(playerX - 1, 0);
    }
    if (key == ConsoleKey.DownArrow)
    {
        playerY = Math.Min(playerY + 1, mapHeight);
    }
    if (key == ConsoleKey.UpArrow)
    {
        playerY = Math.Max(playerY - 1, 0);
    }

    // BOX
    if (playerX == boxPosX && playerY == boxPosY)
    {
        boxPosX += playerX - prevPlayerX;
        boxPosY += playerY - prevPlayerY;
        if (boxPosX > mapWidth || boxPosY > mapHeight || boxPosY < 0 || boxPosX < 0)
        {
            boxPosX = prevBoxPosX;
            boxPosY = prevBoxPosY;
            playerX = prevPlayerX;
            playerY = prevPlayerY;
        }
    }

}
