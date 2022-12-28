// 초기 세팅
Console.ResetColor(); // 컬러를 초기화한다
Console.CursorVisible = false; // 커서를 안보이게 해줌(콘솔에서는 입력하려는 표시인거임)
Console.Title = "소코반 게임!"; // 콘솔창의 제목을 입력
Console.BackgroundColor = ConsoleColor.Cyan; // 배경색을 바꿔줌
Console.ForegroundColor = ConsoleColor.Red; // 글씨의 색을 바꿔줌
Console.Clear(); // 출력된 모든 내용을 지운다




// == && || , = == 




int playerX = 0;
int playerY = 0;
int playerDirection = 0; // 0: None, 1: 왼쪽으로 이동중이였다(Left), 2: Right, 3: Up, 4: Down
// 이동 방향 => 

int boxX = 2;
int boxY = 2;
// 가로 15, 세로 10

// 게임 루프 == 프레임(Frame)
while (true)
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
    // 플레이어 좌표 구하기
    if (key == ConsoleKey.RightArrow)
    {
        // 오른쪽으로 이동
        playerX = Math.Min(playerX + 1, 15);
        playerDirection= 2;
    }

    if (key == ConsoleKey.LeftArrow)
    {
        playerX = Math.Max(0, playerX - 1);
        playerDirection= 1;
    }

    if (key == ConsoleKey.DownArrow)
    {
        playerY = Math.Min(playerY + 1, 15);
        playerDirection= 4;
    }
    
    if (key == ConsoleKey.UpArrow)
    {
        playerY = Math.Max(0, playerY - 1);
        playerDirection= 3;
    }
    




    // 플레이어가 이동한 좌표를 다 구한 후!

    if(playerX == boxX && playerY == boxY) // 이동하고보니 박스가 있네?
    {
        // 박스를 움직여주기
        // 플레이어가 어디서 왔는지 모름!
        // 플레이어가 온 방향에 따라서 박스의 위치가 달라짐. 1)왼쪽으로 이동중일떄 2)오른쪽으로 이동중일때 3)위로 이동중 4)아래로 이동중
        switch(playerDirection)
        {
            case 1: // 왼쪽으로 이동중일때
                if (boxX == 0) // 박스가 왼쪽 끝에 있다면?
                {
                    playerX = 1;
                }
                else
                {
                    boxX = boxX - 1;
                }
                break;
            case 2: // 오른쪽으로 이동중일 때
                if(boxX==15)
                {
                    playerX = 14;
                }
                else
                {
                    boxX = boxX + 1;
                }
                
                break;
            case 3: // 위로 이동중일때
                if(boxY == 0)
                {
                    playerY = 1;
                }
                else
                {
                    boxY = boxY - 1;
                }
                break;
            case 4: // 아래로 이동중 일때
                if(boxY == 15)
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
                Console.WriteLine($"플레이어의 이동방향이 잘못되었습니다. {playerDirection}");
                return;

        }

    }



   




}


