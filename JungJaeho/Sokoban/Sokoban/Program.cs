#region 초기세팅
Console.ResetColor();                               // 컬러 초기화
Console.CursorVisible = false;                      // 커서 비활성화
Console.Title = "던전앤파이터";                     // 타이틀 설정
Console.BackgroundColor = ConsoleColor.DarkGray;    // 배경색 설정
Console.ForegroundColor = ConsoleColor.DarkCyan;    // 전경색 설정
Console.Clear();                                    // 콘솔창 클리어
/*-------------------------------------------------*/
#endregion


#region 맵세팅
ConsoleColor DefaultForegroundColor = Console.ForegroundColor;
int mapMinWidth = 0;
int mapMinHeight = 0;
int mapMaxWidth = 35;
int mapMaxHeight = 10;
char wallIcon = 'Π';
ConsoleColor wallColor = ConsoleColor.Red;
#endregion


#region Player
int playerX = 2;
int playerY = 2;
ConsoleColor playerColor = ConsoleColor.DarkCyan;

char player = 'Д';
#endregion

#region Box
int boxPosX = 5;
int boxPosY = 5;
char box = '▥'; //▥ Д
ConsoleColor boxColor = ConsoleColor.Black;
#endregion


// 게임 루프 == 프레임(Frame)
while (true)
{
    Console.Clear();

    #region Render
    /*---------------------- Render ---------------------- */
    Console.ForegroundColor = wallColor;

    // 벽
    for (int i = 0; i <= mapMaxWidth; i++)
    {
        Console.SetCursorPosition(i, mapMinHeight);
        Console.Write(wallIcon);
        Console.SetCursorPosition(i, mapMaxHeight);
        Console.Write(wallIcon);
    }
    for (int i = 0; i <= mapMaxHeight; i++)
    {
        Console.SetCursorPosition(mapMinWidth, i);
        Console.Write(wallIcon);
        Console.SetCursorPosition(mapMaxWidth, i);
        Console.Write(wallIcon);
    }

    Console.SetCursorPosition(playerX, playerY);
    Console.ForegroundColor = playerColor;
    Console.Write(player);

    Console.SetCursorPosition(boxPosX, boxPosY);
    Console.ForegroundColor = boxColor;
    Console.Write(box);

    Console.ForegroundColor = DefaultForegroundColor;
    #endregion

    #region ProcessInput
    /*------------------- ProcessInput ------------------- */
    ConsoleKey key = Console.ReadKey().Key;
    #endregion

    #region Update
    /*---------------------- Update ---------------------- */

    int prevPlayerX = playerX;
    int prevPlayerY = playerY;
    int prevBoxPosX = boxPosX;
    int prevBoxPosY = boxPosY;

    // Player
    if (key == ConsoleKey.RightArrow)
    {
        playerX = Math.Min(playerX + 1, mapMaxWidth - 1);
    }
    if (key == ConsoleKey.LeftArrow)
    {
        playerX = Math.Max(playerX - 1, mapMinWidth + 1);
    }
    if (key == ConsoleKey.DownArrow)
    {
        playerY = Math.Min(playerY + 1, mapMaxHeight - 1);
    }
    if (key == ConsoleKey.UpArrow)
    {
        playerY = Math.Max(playerY - 1, mapMinHeight + 1);
    }

    // BOX
    if (playerX == boxPosX && playerY == boxPosY)
    {
        boxPosX += playerX - prevPlayerX;
        boxPosY += playerY - prevPlayerY;
        // 박스 범위 나갈 때
        if (boxPosX > mapMaxWidth-1 || boxPosY > mapMaxHeight-1 || boxPosY < mapMinHeight+1 || boxPosX < mapMinWidth+1)
        {
            boxPosX = prevBoxPosX;
            boxPosY = prevBoxPosY;
            playerX = prevPlayerX;
            playerY = prevPlayerY;
        }
    }

    #endregion

}
