//초기 세팅
Console.ResetColor(); //컬러를 초기화한다.
Console.CursorVisible = false; // 커서를 없애준다.
Console.Title = "소코반"; //타이틀 이름
Console.BackgroundColor = ConsoleColor.Red; // 배경색을 설정한다.
Console.ForegroundColor = ConsoleColor.White; // 글꼴 색을 설정한다.
Console.Clear(); // 출력된 모든 내용을 지운다.

//플레이어 좌표설정
int playerX = 0;
int playerY = 0;
int objectX = 5;
int objectY = 5;
int playerDirection = 0; // 1 : Left, 2 :  Right 3 : Up 4 : Down




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
        playerDirection = 2;
        
    }
    // 왼쪽 화살표 키를 눌렀을 때
    if (key == ConsoleKey.LeftArrow)
    {
        playerX = Math.Max(0, playerX - 1);
        playerDirection = 1;
        
    }
    // 위쪽 화살표 키를 눌렀을 때
    if (key == ConsoleKey.UpArrow)
    {
        playerY = Math.Max(0, playerY - 1);
        playerDirection = 3;
    }
    // 아래로 움질일 때
    if (key == ConsoleKey.DownArrow)
    {
        playerY = Math.Min(playerY + 1, 10);
        playerDirection = 4;
    }
    // 박스 업데이트
    // 플레이어가 이동한 후
    if(playerX == objectX && playerY == objectY)
    {
        // 박스를 움직이자
        switch (playerDirection)
        {
            case 1:
                if(objectX == 0)
                {
                    playerX = 1;
                }
                else
                {
                    objectX -= 1;
                }
                break;
            case 2:
                if(objectX == 15)
                {
                    playerX = 14;
                }
                else
                {
                    objectX += 1;
                }
                break;
            case 3:
                if(objectY == 0)
                {
                    playerY += 1;
                }
                else
                {
                    objectY -= 1;
                }
                break;
            case 4:
                if(objectY == 10)
                {
                    playerY = 9;
                }
                else
                {
                    objectY += 1;
                }
                break;
            default:
                Console.Clear();
                Console.WriteLine("[Error] 플레이어의 이동 방향이 잘못되었습니다. {playerDirection}");
                return; // 프로그램을 종류
        }

        
        // 플레이어가 어느 방향에서 왔는지에 따라 박스의 위치가 달라짐.
        
    }
   
        
    
    
    
}



