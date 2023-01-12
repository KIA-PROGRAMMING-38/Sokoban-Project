using System;

namespace ChoiSeonMun.Sokoban;

enum Direction
{
    None,
    Left,
    Right,
    Up,
    Down
}

class Program
{
    static void Main(string[] args)
    {
        // 초기 세팅
        Console.ResetColor();                               // 컬러를 초기화한다.
        Console.CursorVisible = false;                      // 커서를 숨긴다.
        Console.Title = "My Sokoban";                       // 타이틀을 설정한다.
        Console.BackgroundColor = ConsoleColor.DarkBlue;    // 배경색을 설정한다.
        Console.ForegroundColor = ConsoleColor.Gray;        // 글꼴색을 설정한다.
        Console.Clear();                                    // 콘솔 창에 출력된 내용을 모두 지운다.

        // 기호 상수 정의
        const int MIN_X = 0;
        const int MAX_X = 15;
        const int MIN_Y = 0;
        const int MAX_Y = 20;

        // 플레이어 위치 좌표
        int playerX = 0;
        int playerY = 0;

        // 플레이어의 이동 방향
        Direction playerMoveDirection = Direction.None;

        // 박스 좌표
        int boxX = 5;
        int boxY = 5;

        // 박스가 골 위에 올라와있는지 저장한다 
        bool isBoxOnGoal = false;

        // 벽 좌표
        int[] wallPositionsX = { 7, 11 };
        int[] wallPositionsY = { 7, 5 };

        // 골 좌표
        int[] goalPositionsX = { 10, 3 };
        int[] goalPositionsY = { 10, 6 };

        
        
        // 게임 루프
        while (true)
        {
            // ======================= Render ==========================
            // 이전 프레임을 지운다
            Console.Clear();

            // 플레이어를 그린다
            Console.SetCursorPosition(playerX, playerY);
            Console.Write("P");

            // 골을 그린다
            int goalCount = goalPositionsX.Length;
            for (int i = 0; i < goalCount; ++i)
            {
                Console.SetCursorPosition(goalPositionsX[i], goalPositionsY[i]);
                Console.Write("G");
            }
            
            // 박스를 그린다
            Console.SetCursorPosition(boxX, boxY);
            Console.Write(isBoxOnGoal ? "O" : "B");

            // 벽을 그린다
            int wallCount = wallPositionsX.Length;
            for (int i = 0; i < wallCount; ++i)
            {
                Console.SetCursorPosition(wallPositionsX[i], wallPositionsY[i]);
                Console.Write("W");
            }
            
            // ======================= ProcessInput =======================
            // 유저로부터 입력을 받는다 
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            ConsoleKey key = keyInfo.Key;   // 실제 키는 ConsoleKeyInfo에 Key에 있다 

            // ======================= Update =======================
            // 플레이어 이동 처리
            if (key == ConsoleKey.LeftArrow)
            {
                playerX = (int)Math.Max(MIN_X, playerX - 1);
                playerMoveDirection = Direction.Left;
            }

            if (key == ConsoleKey.RightArrow)
            {
                playerX = (int)Math.Min(playerX + 1, MAX_X);
                playerMoveDirection = Direction.Right;
            }

            if (key == ConsoleKey.UpArrow)
            {
                playerY = (int)Math.Max(MIN_Y, playerY - 1);
                playerMoveDirection = Direction.Up;
            }

            if (key == ConsoleKey.DownArrow)
            {
                playerY = (int)Math.Min(playerY + 1, MAX_Y);
                playerMoveDirection = Direction.Down;
            }

            // 플레이어와 벽의 충돌 처리
            for (int i = 0; i < wallCount; ++i)
            {
                if (playerX != wallPositionsX[i] || playerY != wallPositionsY[i])
                {
                    continue;
                }

                switch (playerMoveDirection)
                {
                        case Direction.Left:
                            playerX = wallPositionsX[i] + 1;

                            break;
                        case Direction.Right:
                            playerX = wallPositionsX[i] - 1;

                            break;
                        case Direction.Up:
                            playerY = wallPositionsY[i] + 1;

                            break;
                        case Direction.Down:
                            playerY = wallPositionsY[i] - 1;

                            break;
                        default:    // Error
                            Console.Clear();
                            Console.WriteLine($"[Error] 플레이어 방향 : {playerMoveDirection}");
                            Environment.Exit(1);    // 프로그램을 종료한다.

                            break;
                }
            }
            
            // 박스 업데이트
            if (playerX == boxX && playerY == boxY)
            {
                switch (playerMoveDirection)
                {
                    case Direction.Left:
                        boxX = Math.Max(MIN_X, boxX - 1);
                        playerX = boxX + 1;

                        break;
                    case Direction.Right:
                        boxX = Math.Min(boxX + 1, MAX_X);
                        playerX = boxX - 1;

                        break;
                    case Direction.Up:
                        boxY = Math.Max(MIN_X, boxY - 1);
                        playerY = boxY + 1;

                        break;
                    case Direction.Down:
                        boxY = Math.Min(boxY + 1, MAX_Y);
                        playerY = boxY - 1;

                        break;
                    default:    // Error
                        Console.Clear();
                        Console.WriteLine($"[Error] 플레이어 방향 : {playerMoveDirection}");
                        Environment.Exit(1);    // 프로그램을 종료한다.

                        break;
                }
            }

            // 박스와 벽의 충돌 처리
            for (int i = 0; i < wallCount; ++i)
            {
                if (boxX != wallPositionsX[i] || boxY != wallPositionsY[i])
                {
                    continue;
                }

                switch (playerMoveDirection)
                {
                    case Direction.Left:
                        boxX = wallPositionsX[i] + 1;
                        playerX = boxX + 1;

                        break;
                    case Direction.Right:
                        boxX = wallPositionsX[i] - 1;
                        playerX = boxX - 1;

                        break;
                    case Direction.Up:
                        boxY = wallPositionsY[i] + 1;
                        playerY = boxY + 1;

                        break;
                    case Direction.Down:
                        boxY = wallPositionsY[i] - 1;
                        playerY = boxY - 1;

                        break;
                    default:    // Error
                        Console.Clear();
                        Console.WriteLine($"[Error] 플레이어 방향 : {playerMoveDirection}");
                        Environment.Exit(1);    // 프로그램을 종료한다.

                        break;
                }
            }

            // 박스가 골 위로 올라왔는지 확인
            int boxOnGoalCount = 0;
            for (int i = 0; i < goalCount; ++i)
            {
                if (boxX == goalPositionsX[i] && boxY == goalPositionsY[i])
                {
                    ++boxOnGoalCount;
                    isBoxOnGoal = true;
                }
            }

            if (boxOnGoalCount == goalCount)
            {
                break;
            }
        }

        Console.Clear();
        Console.WriteLine("축하합니다. 게임을 클리어하셨습니다.");

        // 게임이 끝났으니 콘솔 세팅을 다시 정상화한다.
        Console.ResetColor(); 
    }
}

