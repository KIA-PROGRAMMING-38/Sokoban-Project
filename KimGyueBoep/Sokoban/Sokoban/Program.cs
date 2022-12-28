// 초기 세팅
using System.Runtime.CompilerServices;

Console.ResetColor();                                 // 컬러를 초기화한다.
Console.CursorVisible = false;                        // 커서를 숨긴다.
Console.Title = "소코반 프로젝트";                     // 타이틀을 설정한다.
Console.BackgroundColor = ConsoleColor.Green;         // 배경색을 설정한다.
Console.ForegroundColor = ConsoleColor.Red;           // 글꼴색을 설정한다.
Console.Clear();                                     // 출력된 모든 내용을 지운다.

// 플레이어 좌표 설정
int playerX = 0;
int playerY = 0;
// int playerDirection = 0; // 0 : NONE, 1 : Left, 2 : Right, 3 : Up, 4 : Down

int box_X = 2;
int box_Y = 1;
// 가로 15, 세로 10
// 게임 루프 == 프레임(Frame)
while (true)
{
    Console.Clear();
    // ---------------------------------- Render ----------------------------------
    // 플레이어 출력하기
    Console.SetCursorPosition(playerX, playerY);
    Console.Write('▶');
    Console.SetCursorPosition(box_X, box_Y);
    Console.Write('■');

    // ---------------------------------- ProcessInput ----------------------------------
    ConsoleKey key = Console.ReadKey().Key;

    // ---------------------------------- Update ----------------------------------
    // 오른쪽 화살표키를 눌렀을 때
    if (key == ConsoleKey.RightArrow)
    {
        // 오른쪽으로 이동
        playerX = Math.Min(playerX + 1, 15);
        // playerDirection = 2;
    }
    if (key == ConsoleKey.LeftArrow)
    {
        playerX = Math.Max(0, playerX - 1);
        // playerDirection = 1;
    }
    if (key == ConsoleKey.DownArrow)
    {
        playerY = Math.Min(playerY + 1, 10);
        // playerDirection = 4;
    }
    if (key == ConsoleKey.UpArrow)
    {
        playerY = Math.Max(0, playerY - 1);
        // playerDirection = 3;
    }

    // 박스 업데이트
    // 플레이어가 이동한 후
    if (playerX == box_X && playerY == box_Y && key == ConsoleKey.RightArrow)
    {
        box_X++;
        if (box_X > 15)
        {
            box_X = 15;
            playerX -= 1;
        }
    }
    if (playerX == box_X && playerY == box_Y && key == ConsoleKey.LeftArrow)
    {
        box_X--;
        if (box_X < 0)
        {
            box_X = 0;
            playerX += 1;
        }
    }
    if (playerY == box_Y && playerX == box_X && key == ConsoleKey.DownArrow)
    {
        box_Y++;
        if (box_Y > 10)
        {
            box_Y = 10;
            playerY -= 1;
        }
    }
    if (playerY == box_Y && playerX == box_X && key == ConsoleKey.UpArrow)
    {
        box_Y--;
        if (box_Y < 0)
        {
            box_Y = 0;
            playerY += 1;
        }
    }

    //if (playerX == Box_X && playerY == Box_Y)
    //{
    //    switch (playerDirection)
    //    {
    //        case 1: // 왼쪽
    //            if (Box_X == 0)
    //            {
    //                playerX = 1;
    //            }
    //            else
    //            {
    //                Box_X = Box_X - 1;
    //            }
    //            break;
    //        case 2: // 오른쪽
    //            if (Box_X == 15)
    //            {
    //                playerX = 14;
    //            }
    //            else
    //            {
    //                Box_X = Box_X + 1;
    //            }
    //            break;
    //        case 3: // 위
    //            if (Box_Y == 0)
    //            {
    //                playerY = 1;
    //            }
    //            else
    //            {
    //                Box_Y = Box_Y - 1;
    //            }
    //            break;
    //        case 4: // 아래
    //            if (Box_Y == 10)
    //            {
    //                playerY = 9;
    //            }
    //            else
    //            {
    //                Box_Y = Box_Y + 1;
    //            }
    //            break;
    //        default:
    //            Console.Clear();
    //            Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다. {playerDirection}");

    //            return; // 프로그램 종료
    //    }
    //}
}