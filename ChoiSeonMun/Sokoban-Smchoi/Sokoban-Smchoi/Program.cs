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

        // 코드 최적화를 위해 플레이어가 민 박스의 인덱스를 저장한다
        int pushedBoxIndex = 0;

        // 박스 좌표
        int[] boxPositionsX = { 5, 8 };
        int[] boxPositionsY = { 5, 4 };

        // 각 박스마다 골 위에 올라와 있는지에 관한 데이터다
        bool[] isBoxOnGoal = new bool[boxPositionsX.Length]; 

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
            RenderObject(playerX, playerY, "P");

            // 골을 그린다
            int goalCount = goalPositionsX.Length;
            for (int i = 0; i < goalCount; ++i)
            {
                RenderObject(goalPositionsX[i], goalPositionsY[i], "G");
            }

            // 박스를 그린다
            int boxCount = boxPositionsX.Length;
            for (int i = 0; i < boxCount; ++i)
            {
                string boxIcon = isBoxOnGoal[i] ? "O" : "B";
                RenderObject(boxPositionsX[i], boxPositionsY[i], boxIcon);
            }

            // 벽을 그린다
            int wallCount = wallPositionsX.Length;
            for (int i = 0; i < wallCount; ++i)
            {
                RenderObject(wallPositionsX[i], wallPositionsY[i], "W");
            }
            
            // ======================= ProcessInput =======================
            // 유저로부터 입력을 받는다 
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            ConsoleKey key = keyInfo.Key;   // 실제 키는 ConsoleKeyInfo에 Key에 있다 

            // ======================= Update =======================
            MovePlayer(key, ref playerX, ref playerY, ref playerMoveDirection);

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
            for (int i = 0; i < boxCount; ++i)
            {
                if (playerX != boxPositionsX[i] || playerY != boxPositionsY[i])
                {
                    continue;
                }

                switch (playerMoveDirection)
                {
                    case Direction.Left:
                        boxPositionsX[i] = Math.Max(MIN_X, boxPositionsX[i] - 1);
                        playerX = boxPositionsX[i] + 1;

                        break;
                    case Direction.Right:
                        boxPositionsX[i] = Math.Min(boxPositionsX[i] + 1, MAX_X);
                        playerX = boxPositionsX[i] - 1;

                        break;
                    case Direction.Up:
                        boxPositionsY[i] = Math.Max(MIN_X, boxPositionsY[i] - 1);
                        playerY = boxPositionsY[i] + 1;

                        break;
                    case Direction.Down:
                        boxPositionsY[i] = Math.Min(boxPositionsY[i] + 1, MAX_Y);
                        playerY = boxPositionsY[i] - 1;

                        break;
                    default:    // Error
                        Console.Clear();
                        Console.WriteLine($"[Error] 플레이어 방향 : {playerMoveDirection}");
                        Environment.Exit(1);    // 프로그램을 종료한다.

                        break;
                }

                // 어떤 박스를 밀었는지 저장해야 한
                pushedBoxIndex = i;

                break;
            }

            // 박스끼리의 충돌 처리
            for (int i = 0; i < boxCount; ++i)
            {
                // 같은 박스라면 처리할 필요가 없다 
                if (pushedBoxIndex == i)
                {
                    continue;
                }

                if (boxPositionsX[pushedBoxIndex] != boxPositionsX[i] || boxPositionsY[pushedBoxIndex] != boxPositionsY[i])
                {
                    continue;
                }

                switch (playerMoveDirection)
                {
                    case Direction.Left:
                        boxPositionsX[pushedBoxIndex] = boxPositionsX[i] + 1;
                        playerX = boxPositionsX[pushedBoxIndex] + 1;

                        break;
                    case Direction.Right:
                        boxPositionsX[pushedBoxIndex] = boxPositionsX[i] - 1;
                        playerX = boxPositionsX[pushedBoxIndex] - 1;

                        break;
                    case Direction.Up:
                        boxPositionsY[pushedBoxIndex] = boxPositionsY[i] + 1;
                        playerY = boxPositionsY[pushedBoxIndex] + 1;

                        break;
                    case Direction.Down:
                        boxPositionsY[pushedBoxIndex] = boxPositionsY[i] - 1;
                        playerY = boxPositionsY[pushedBoxIndex] - 1;

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
                if (boxPositionsX[pushedBoxIndex] != wallPositionsX[i] || boxPositionsY[pushedBoxIndex] != wallPositionsY[i])
                {
                    continue;
                }

                switch (playerMoveDirection)
                {
                    case Direction.Left:
                        boxPositionsX[pushedBoxIndex] = wallPositionsX[i] + 1;
                        playerX = boxPositionsX[pushedBoxIndex] + 1;

                        break;
                    case Direction.Right:
                        boxPositionsX[pushedBoxIndex] = wallPositionsX[i] - 1;
                        playerX = boxPositionsX[pushedBoxIndex] - 1;

                        break;
                    case Direction.Up:
                        boxPositionsY[pushedBoxIndex] = wallPositionsY[i] + 1;
                        playerY = boxPositionsY[pushedBoxIndex] + 1;

                        break;
                    case Direction.Down:
                        boxPositionsY[pushedBoxIndex] = wallPositionsY[i] - 1;
                        playerY = boxPositionsY[pushedBoxIndex] - 1;

                        break;
                    default:    // Error
                        Console.Clear();
                        Console.WriteLine($"[Error] 플레이어 방향 : {playerMoveDirection}");
                        Environment.Exit(1);    // 프로그램을 종료한다.

                        break;
                }

                break;
            }

            // 박스가 골 위로 올라왔는지 확인
            int boxOnGoalCount = 0;
            for (int boxId = 0; boxId < boxCount; ++boxId)
            {
                // 현재 프레임의 박스 상태를 실시간으로 추적하기 위해 false로 바꿔둔다
                isBoxOnGoal[boxId] = false;

                for (int goalId = 0; goalId < goalCount; ++goalId)
                {
                    if (boxPositionsX[boxId] == goalPositionsX[goalId] && boxPositionsY[boxId] == goalPositionsY[goalId])
                    {
                        ++boxOnGoalCount;
                        isBoxOnGoal[boxId] = true;

                        break; // 이 박스에 대해서는 더 이상 체크할 게 없
                    }
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


        // 오브젝트를 그린다
        void RenderObject(int x, int y, string icon)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(icon);
        }

        // 플레이어를 움직인다
        void MovePlayer(ConsoleKey key, ref int x, ref int y, ref Direction moveDirection)
        {
            if (key == ConsoleKey.LeftArrow)
            {
                x = (int)Math.Max(MIN_X, x - 1);
                moveDirection = Direction.Left;
            }

            if (key == ConsoleKey.RightArrow)
            {
                x = (int)Math.Min(x + 1, MAX_X);
                moveDirection = Direction.Right;
            }

            if (key == ConsoleKey.UpArrow)
            {
                y = (int)Math.Max(MIN_Y, y - 1);
                moveDirection = Direction.Up;
            }

            if (key == ConsoleKey.DownArrow)
            {
                y = (int)Math.Min(y + 1, MAX_Y);
                moveDirection = Direction.Down;
            }
        }
    }
}

