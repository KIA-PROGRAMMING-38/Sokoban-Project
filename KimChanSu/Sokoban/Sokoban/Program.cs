// 초기 세팅
using System.Reflection;

Console.ResetColor();                           // 컬러를 초기화한다.
Console.CursorVisible = false;                  // 커서를 숨긴다.
Console.Title = "Sokoban";                      // 타이틀을 설정한다.
Console.BackgroundColor = ConsoleColor.Blue;    // 배경색을 설정한다.
Console.ForegroundColor = ConsoleColor.Gray;    // 글꼴색을 설정한다.
Console.Clear();                                // 출력된 모든 내용을 지운다.

// 플레이어 좌표 설정
int playerX = 0;
int playerY = 0;
int playerDirection = 0; // 0 : None, 1 : Left, 2 : Right, 3 : Up, 4 : Down 내가 정한 규칙

//박스 좌표 설정
int boxX = 5;
int boxY = 5;

// 가로 15 세로 10
// 게임 루프 == 프레임(Frame)


while (true)
{
    // 이전 프레임을 지운다.
    Console.Clear();

    // -------------------------- Render -------------------------------------
    // 플레이어 출력하기
    Console.SetCursorPosition(playerX, playerY);
    Console.Write("P");

    //박스 출력하기
    Console.SetCursorPosition(boxX, boxY);
    Console.Write("B");

    // -------------------------- ProcessInput -------------------------------
    ConsoleKey key = Console.ReadKey().Key;

    // -------------------------- Update -------------------------------------


    if (key == ConsoleKey.LeftArrow)
    {
        // 왼쪽 방향키 눌렀을 때
        // 왼쪽으로 이동
        playerX = Math.Max(0, playerX - 1);
        playerDirection = 1;
    }
    
    if (key == ConsoleKey.RightArrow)
    {
        // 오른쪽 화살표키를 눌렀을 때
        // 오른쪽으로 이동
        playerX = Math.Min(playerX + 1, 15);
        playerDirection = 2;
    }

    if (key == ConsoleKey.UpArrow)
    {
        // 위쪽 방향키 눌렀을 때
        // 위로 이동
        playerY = Math.Max(0, playerY - 1);
        playerDirection = 3;
    }

    if (key == ConsoleKey.DownArrow)
    {
        // 아래쪽 방향키 눌렀을 때
        // 아래로 이동
        playerY = Math.Min(playerY + 1, 10);
        playerDirection = 4;
    }

    // ----------------------------------------

    //박스 업데이트
    // 플레이어가 이동한 후
    if (playerX == boxX && playerY == boxY)
    {
        switch (playerDirection)
        {
            case 1: // 플레이어가 왼쪽으로 이동중
                if (boxX == 0) //박스가 왼쪽 끝에 있다면
                {
                    playerX = 1;
                }
                else
                {
                    boxX = Math.Max(0, boxX - 1);
                }
                
                break;
            case 2: // 플레이어가 오른쪽으로 이동중
                if (boxX == 15)
                {
                    playerX = 14;
                }
                else
                {
                    boxX = Math.Min(boxX + 1 , 15);
                }
                
                break;
            case 3: // 플레이어가 위쪽으로 이동중
                if (boxY == 0)
                {
                    playerY = 1;
                }
                else
                {
                    boxY = boxY - 1;
                }
                
                break;
            case 4: // 플레이어가 아래쪽으로 이동중
                if (boxY == 10)
                {
                    playerY = 9;
                }
                else
                {
                    boxY = boxY + 1;
                }
                
                break;
            default: //
                Console.Clear();
                Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다. {playerDirection}");

                return; // 프로그램 종료
        }
        // 박스를 움직여줘야함.
        // 플레이어가 어느 방향에서 왔는지에 따라 박스의 위치가 달라짐.
    }
}
