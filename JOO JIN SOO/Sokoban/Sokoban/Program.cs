

// 초기 세팅
using System.Reflection;

Console.ResetColor();                              // 컬러를 초기화한다
Console.CursorVisible = false;                     // 커서를 켜고 끄는 것, 불리언 타입이다
Console.Title = "홍성재의 파이어펀치";               // 타이틀 명을 바꾸어주는 이름
Console.BackgroundColor = ConsoleColor.Darkgit Green;  // 배경색을 설정한다
Console.ForegroundColor = ConsoleColor.Yellow;     // 글꼴색을 설정한다
Console.Clear();                                   // 출력된 모든 내용을 지운다

// 플레이어 좌표 설정은 바깥에 해야한다
int playerAX = 0;
int playerAY = 0;
int playerDirection = 0; // 0: None, 1: Left, 2: Right, 3: Up, 4: Down
// 이동 방향 > 

// 박스 구현
int boxX = 10;
int boxY = 6;


// 게임 루프 == 프레임(Frame)
while (true)
{
    Console.Clear();

    //-------------------------------------- Render -------------------------------------------------
    // 플레이어 출력하기
    Console.SetCursorPosition(playerAX, playerAY);
    Console.Write("A");

    // 박스 출력하기
    Console.SetCursorPosition(boxX, boxY);
    Console.Write("O");

    //-------------------------------------- ProcessInput -------------------------------------------
    ConsoleKeyInfo inputKey = Console.ReadKey();
    ConsoleKey key = inputKey.Key;
    // 이것은 ConsoleKey inputKey = Console.ReadKey().Key; 와 같다

    //-------------------------------------- Update -------------------------------------------------
    // 플레이어 업데이트

    if (key == ConsoleKey.LeftArrow && playerAX > 0)
    {
        playerAX = Math.Max(0, playerAX - 1);
        playerDirection = 1;
    }

    if (key == ConsoleKey.RightArrow)
    {
        playerAX = Math.Min(playerAX + 1, 30);
        playerDirection = 2;
    }

    if (key == ConsoleKey.UpArrow && playerAY > 0)
    {
        playerAY = Math.Max(0, playerAY - 1);
        playerDirection = 3;
    }
    
    if (key == ConsoleKey.DownArrow && playerAY < 20)
    {
        playerAY = Math.Min(playerAY + 1, 20);
        playerDirection = 4;
    }


    // 박스 업데이트
    // 플레이어가 이동한 후


    if (playerAX == boxX && playerAY == boxY)
    {
        switch (playerDirection)
        {
            // 박스를 움직여주면 됨
            case 1: // 플레이어가 왼쪽으로 이동 중
                if (boxX == 0)
                {
                    playerAX = 1;
                }
                else
                {
                    boxX = boxX - 1;
                }
                break;
            case 2: // 플레이어가 오른쪽으로 이동 중
                if (boxX == 30)
                {
                    playerAX = 29;
                }
                else 
                {
                    boxX = boxX + 1;
                }
                break;
            case 3: // 플레이어가 위쪽으로 이동 중
                if (boxY == 0)
                {
                    playerAY = 1;
                }
                else
                {
                    boxY = boxY - 1;
                }
                break;
            case 4: // 플레이어가 아래쪽으로 이동 중
                if (boxY == 0)
                {
                    playerAY = 19;
                }
                else
                {
                    boxY = boxY + 1;
                }
                break;
            default: // 예외 처리
                Console.Clear();
                Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다 {playerDirection}");

                break;
        }

        // 플레이어가 어느 방향에서 왔는지에 따라 박스의 위치가 달라짐
        // 4가지 경우의 수
        // 1. 플레이어가 박스를 오른쪽으로 밀고 있을 때
        // 2. 플레이어가 박스를 왼쪽으로 밀고 있을 때
        // 3. 플레이어가 박스를 위쪽으로 밀고 있을 때
        // 4. 플레이어가 박스를 아래쪽으로 밀고 있을 때
    }






}