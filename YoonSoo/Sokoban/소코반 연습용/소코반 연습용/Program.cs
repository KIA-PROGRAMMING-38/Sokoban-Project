using System;
using System.Runtime.InteropServices;

namespace Sokoban
{
    enum Direction // 방향을 저장하는 타입
    {
        None,
        Left,
        Right,
        Up,
        Down,
    }

    class Sokoban
    {
        static void Main()
        {
            // 초기 세팅
            Console.ResetColor();
            Console.CursorVisible = false;
            Console.Title = "라면 뿌셔 먹는 윤하";
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();


           

            // 기호 상수 정의
            const int DIRECTION_NONE = 0;
            const int DIRECTION_LEFT = 0;
            const int DIRECTION_RIGHT = 1;
            const int DIRECTION_UP = 2;
            const int DIRECTION_DOWN = 3;

            // 플레아어 위치를 저장하기 위한 변수
            int playerX = 0;
            int playerY = 0;

            // 플레이어의 이동 방향을 저장하기 위한 변수
            Direction playerMoveDirection = Direction.None;

            // 박스 위치를 저장하기 위한 변수
            int boxX = 7;
            int boxY = 7;

            // 벽의 위치를 저장하는 변수
            int wallX = 10;
            int wallY = 10;




            // 게임 루프 구성
            while (true)
            {
                // ------------------------------ Render --------------------------------
                // 이전 프레임을 지운다.
                Console.Clear();

                // 플레이어를 그린다.
                Console.SetCursorPosition(playerX, playerY);
                Console.Write("P");

                // 박스를 그린다.
                Console.SetCursorPosition(boxX, boxY);
                Console.Write("B");


                // ------------------------------- ProcessInput -------------------------------
                ConsoleKey key = Console.ReadKey().Key;

                // ---------------- Update ----------------

                // 플레이어 이동 처리
                if (key == ConsoleKey.LeftArrow)
                {
                    if(playerX -1 < 0)
                    {
                        playerX = 0;
                    }
                    else
                    {
                        playerX = playerX - 1;
                    }
                    playerMoveDirection = Direction.Left;
                }
                if (key == ConsoleKey.RightArrow)
                {
                    if(playerX + 1 < 0)
                    {
                        playerX = 0;
                    }
                    else
                    {
                        playerX = playerX + 1;
                    }
                    playerMoveDirection = Direction.Right;
                }
                if (key == ConsoleKey.UpArrow)
                {
                    if (playerY - 1 < 0)
                    {
                        playerY = 0;
                    }
                    else
                    {
                        playerY = playerY - 1;
                    }
                    playerMoveDirection = Direction.Up;    
                }
                if (key == ConsoleKey.DownArrow)
                {
                    if (playerY + 1 < 0)
                    {
                        playerY = 0;
                    }
                    else
                    {
                        playerY = playerY + 1;
                    }
                    playerMoveDirection = Direction.Down;
                }
                    

                // 박스 이동 처리
                // 플레이어가 박스를 밀었을 때라는게 무엇을 의미하는가? => 플레이어가 이동했는데 플레이어의 위치와 박스 위치가 겹쳤다.
                if (playerX == boxX && playerY == boxY)
                {
                    // 박스를 밀면 된다. => 박스의 좌표를 바꾼다.

                    // 위에서 밀 수도 있고
                    // 아래에서 밀 수도 있고
                    // 왼쪽에서 밀 수도 있고
                    // 오른쪽에서 밀 수고 있음.
             
                    switch (playerMoveDirection)
                    {
                        case Direction.Left:
                            boxX = Math.Max(0, boxX -1);
                            playerX = boxX + 1;
                            break;
                        case Direction.Right:
                            boxX = Math.Min(boxX + 1, 20);
                            playerX = boxX- 1;
                            break;
                        case Direction.Up:
                            boxY = Math.Max(0, boxY-1);
                            playerY = boxY + 1;
                            break;
                        case Direction.Down:
                            boxY = Math.Min(boxY+ 1, 10);
                            playerY = boxY- 1;
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine($"[Error] 플레이어 이동 방향 데이터가 오류입니다. : {playerMoveDirection}");
                            return;


                    }
                    
                }
                
                // 플레이어와 벽의 충돌 처리
                if (playerX == wallX && playerY == wallY)
                {
                    switch (playerMoveDirection)
                    {
                        case Direction.Left:
                            playerX = wallX + 1;
                            break;
                            case Direction.Right:
                            playerX = wallX - 1;
                            break;
                            case Direction.Up:
                            playerY = wallX + 1;
                            break;
                            case Direction.Down:
                            playerY = wallX - 1;
                            break;
                    }

                }

                // 박스 이동 처리
                // 플레이어가 박스를 밀었울 때라는 게 무엇을 의미하는가? => 플레이어가 이동했는데 플레이어와 위치와 박스 위치가 겹쳤다.
                if (playerX == boxX && playerY == boxY)
                {
                    // 박스를 민다. => 박스의 좌표를 바꾼다.
                    switch (playerMoveDirection)
                    {
                        case Direction.Left:
                            boxX = Math.Max(0, boxX - 1);
                            playerX = boxX + 1;
                            break;
                    }
                }
            }
        }
    }
}
