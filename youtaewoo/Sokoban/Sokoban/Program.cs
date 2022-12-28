//초기 세팅
Console.ResetColor(); // 컬러를 초기화한다.
Console.CursorVisible = false; // 커서 숨기기
Console.Title = "미래의babaisyou"; // 타이틀을 설정한다.
Console.BackgroundColor = ConsoleColor.DarkGreen; // 배경색을 설정한다.
Console.ForegroundColor = ConsoleColor.White; //글꼴색을 설정한다.
Console.Clear(); // 출력된 모든 내용을 지운다.

// 플레이어 좌표 설정
int playerX = 0;
int playerY = 0; // 얘는 바깥에다 해줘야한다. while문 안에 있으면 이동이 안된다.
int playerDirection = 0;
// 박스 좌표 설정
int boxX = 5;
int boxY = 10;

// 게임 루프 == 프레임(frame)
while (true)
{
    // 이전 프레임을 지운다.
    Console.Clear();
    //---------------------------------Render--------------------------------
    // 플레이어 출력하기
    
    Console.SetCursorPosition(playerX, playerY);
    Console.Write("B");

    // 박스 출력하기
    Console.SetCursorPosition(boxX, boxY);
    Console.Write("O");

    //------------------------------ProcessInput-----------------------------
    ConsoleKey key = Console.ReadKey().Key;

    //---------------------------------Update--------------------------------
    //오른쪽 화살표 키를 눌렀을 때를 먼저 한정해줄 것이다.
    if (key == ConsoleKey.RightArrow && playerX < 15) //오른쪽 이동  
    {                  
            playerX = Math.Min(playerX + 1, 15);
            playerDirection = 2;
    }

    if (key == ConsoleKey.LeftArrow && playerX > 0) //왼쪽 이동
    {        
            playerX = Math.Max(0, playerX - 1);
            playerDirection = 1;
    }
    if (key == ConsoleKey.DownArrow && playerY < 15) //아래 이동
    {        
            playerY = Math.Min(playerY + 1, 15);
            playerDirection = 4;
    }
    if (key == ConsoleKey.UpArrow && playerY > 0) //위 이동
    {        
            playerY = Math.Max(0, playerY - 1);
            playerDirection = 3;
    }   
    // 박스 업데이트
    if (playerX == boxX && playerY == boxY) // 플레이어가 이동하고 나니 박스가 있는 것
    {
        //박스를 움직여주면 된다.
        switch(playerDirection)
        {
            case 1: // 박스 왼쪽
                if (boxX == 0)
                {
                    playerX = 1;
                }
                else
                {
                    boxX = boxX - 1;
                }
                break;
            case 2: // 박스 오른쪽
                if (boxX == 15)
                {
                    playerX = 14;
                }
                else
                {
                    boxX = boxX + 1;
                }
                break;
            case 3: // 박스 위쪽
                if (boxY == 0)
                {
                    playerY = 1;
                }
                else
                {
                    boxY = boxY - 1;
                }
                break;
            case 4: // 박스 아래쪽
                if (boxY == 15)
                {
                    playerY = 14;
                }
                else
                {
                    boxY = boxY + 1;
                }
                break;
            default:
                Console.Clear();
                Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다.");
                return;
        }
    }
}