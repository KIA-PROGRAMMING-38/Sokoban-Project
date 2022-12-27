//초기 세팅
Console.ResetColor(); //컬러를 초기화한다.
Console.CursorVisible = false; // 커서를 없애준다.
Console.Title = "소코반"; //타이틀 이름
Console.BackgroundColor = ConsoleColor.Red; // 배경색을 설정한다.
Console.ForegroundColor = ConsoleColor.White; // 글꼴 색을 설정한다.
Console.Clear(); // 출력된 모든 내용을 지운다.

int playerX = 0;
int playerY = 0;
int objectX = 5;
int objectY = 5;
int moveX = 0;
int moveY = 0;




//게임 루프 == 프레임(Frame)
while (true)
{
    Console.Clear();
    //맵 가로 15 세로 10
    // ------------------------------ Render ------------------------------
    // 플레이어 출력하기
    Console.SetCursorPosition(playerX, playerY);
    Console.Write("L");
    // 박스 출력하기
    Console.SetCursorPosition(objectX, objectY);
    Console.Write("O");
    
    
    

    // ------------------------------ ProcessInput ------------------------------
    ConsoleKey key = Console.ReadKey().Key;

    // ------------------------------ Update ------------------------------
    
    // 오른쪽 화살표 키를 눌렀을 때
    if (key == ConsoleKey.RightArrow)
    {
        
        playerX = Math.Min(playerX + 1, 15);
        if(objectX == playerX && objectY == playerY)
        {
            if (objectX < 15)
                objectX += 1;
            else
                playerX -= 1;
        }
    }
    // 왼쪽 화살표 키를 눌렀을 때
    if (key == ConsoleKey.LeftArrow)
    {
        playerX = Math.Max(0, playerX - 1);
        if(objectX == playerX && objectY == playerY)
        {
            if (0 < objectX) //.......
                objectX -= 1;
            else
                playerX += 1;
        }
    }
    // 위쪽 화살표 키를 눌렀을 때
    if (key == ConsoleKey.UpArrow)
    {
        playerY = Math.Max(0, playerY - 1);
        if(objectY == playerY && objectX == playerX)
        {
            if (0 < objectY)
                objectY -= 1;
            else
                playerY += 1;
        }
    }
    // 아래로 움질일 때
    if(key == ConsoleKey.DownArrow)
    {
        playerY = Math.Min(playerY + 1, 10);
        if (objectY == playerY && objectX == playerX)
        {
            if (objectY < 10)
                objectY += 1;
            else
                playerY -= 1;
        }
    }
   
        
    
    
    
}



