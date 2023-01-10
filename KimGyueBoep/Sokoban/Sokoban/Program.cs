namespace Sokoban_return
{
    enum Direction // 방향을 저장하는 타입
    {
        NONE,
        LEFT,
        RIGHT,
        UP,
        DOWN
    }
    internal class Program
    {
        static void Main()
        {
            // a와 b중 최댓값을 구한다.
            int Max(int a, int b) // 입력
            {
                int result = 0;
                if (a > b)
                {
                    result = a;
                }                     // 처리
                else
                {
                    result = b;
                }

                return result;          // 출력
            }
            int Min(int a, int b) => (a < b) ? a : b;

            // 초기 세팅
            Console.ResetColor();                     // 1. 컬러를 초기화 하는 것
            Console.CursorVisible = false;            // 2. 커서를 숨기기
            Console.Title = "홍성재의 모에모에큥";      // 3. 타이틀을 설정한다.
            Console.BackgroundColor = ConsoleColor.DarkGreen; // 4. 배경색을 설정한다.
            Console.ForegroundColor = ConsoleColor.Magenta;    // 5. 글꼴색을 설정한다.
            Console.Clear();                                  // 6. 출력된 내용을 지운다.

            // 기호 상수 정의
            const int GOAL_COUNT = 3;
            const int BOX_COUNT = GOAL_COUNT;
            const int WALL_COUNT = GOAL_COUNT;

            // 플레이어 위치를 저장하기 위한 변수
            int playerX = 0;
            int playerY = 0;

            // 플레이어의 이동 방향을 저장하기 위한 변수
            Direction playerMoveDirection = Direction.NONE;

            // 플레이어가 무슨 박스를 밀고 있는지 저장하기 위한 변수
            int pushedBoxId = 0; // 1이면 박스1, 2면 박스2

            // 박스의 위치를 저장하기 위한 변수
            int[] boxPositionsX = { 7, 13, 2 };
            int[] boxPositionsY = { 5, 6, 3 };

            // 벽의 위치를 저장하기 위한 변수
            int[] wallPositionsX = { 10, 9, 2 };
            int[] wallPositionsY = { 6, 3, 5 };

            // 골의 위치를 저장하기 위한 변수
            int[] goalPositionsX = { 17, 3, 9 };
            int[] goalPositionsY = { 7, 7, 7 };

            // 박스가 골 위에 있는지를 저장히가 위한 변수
            bool[] isBoxOnGoal = new bool[BOX_COUNT];
            // 게임루프 구성
            while (true)
            {
                // ------Render-------
                // 이전프레임을 지운다
                Console.Clear();

                // 플레이어를 그린다
                Console.SetCursorPosition(playerX, playerY);
                Console.Write('P');

                // 골을 그린다
                for (int i = 0; i < GOAL_COUNT; i++)
                {
                    int goalX = goalPositionsX[i];
                    int goalY = goalPositionsY[i];
                    Console.SetCursorPosition(goalX, goalY);
                    Console.Write('G');
                }

                // 박스를 그린다.
                for (int boxId = 0; boxId < BOX_COUNT; boxId++)
                {
                    int boxX = boxPositionsX[boxId];
                    int boxY = boxPositionsY[boxId];

                    Console.SetCursorPosition(boxX, boxY);

                    if (isBoxOnGoal[boxId])
                    {
                        Console.Write("●");
                    }
                    else
                    {
                        Console.Write("B");
                    }
                }

                // 벽을 그린다
                for (int i = 0; i < WALL_COUNT; i++)
                {
                    int wallX = wallPositionsX[i];
                    int wallY = wallPositionsY[i];
                    Console.SetCursorPosition(wallX, wallY);
                    Console.Write('W');
                }
                // ------ProcessInput-------
                ConsoleKey key = Console.ReadKey().Key;
                // --------Update--------
                // 플레이어 이동 처리
                if (key == ConsoleKey.LeftArrow)
                {
                    playerX = Max(0, playerX - 1);
                    playerMoveDirection = Direction.LEFT;
                }
                if (key == ConsoleKey.RightArrow)
                {
                    playerX = Min(playerX + 1, 20);
                    playerMoveDirection = Direction.RIGHT;
                }
                if (key == ConsoleKey.UpArrow)
                {
                    playerY = Math.Max(0, playerY - 1);
                    playerMoveDirection = Direction.UP;
                }
                if (key == ConsoleKey.DownArrow)
                {
                    playerY = Math.Min(playerY + 1, 10);
                    playerMoveDirection = Direction.DOWN;
                }

                // 플레이어와 벽의 충돌
                for (int i = 0; i < WALL_COUNT; i++)
                {
                    int wallX = wallPositionsX[i];
                    int wallY = wallPositionsY[i];

                    if (playerX == wallX && playerY == wallY)
                    {
                        switch (playerMoveDirection)
                        {
                            case Direction.LEFT:
                                playerX = wallX + 1;
                                break;
                            case Direction.RIGHT:
                                playerX = wallX - 1;
                                break;
                            case Direction.UP:
                                playerY = wallY + 1;
                                break;
                            case Direction.DOWN:
                                playerY = wallY - 1;
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine($"[Error] 플레이어 이동 방향 데이터가 오류입니다. : {playerMoveDirection}");

                                return;
                        }
                    }
                }

                // 박스 이동 처리
                // 플레이어가 박스를 밀었을 때라는 게 무엇을 의미하는가? => 플레이어가 이동했는데 플레이어의 위치와 박스 위치가 겹쳤다.
                for (int i = 0; i < BOX_COUNT; i++)
                {
                    int boxX = boxPositionsX[i];
                    int boxY = boxPositionsY[i];

                    if (playerX == boxX && playerY == boxY)
                    {
                        // 박스를 민다. => 박스의 좌표를 바꾼다.
                        switch (playerMoveDirection)
                        {
                            case Direction.LEFT:
                                boxX = Math.Max(0, boxX - 1);
                                playerX = boxX + 1;
                                break;
                            case Direction.RIGHT:
                                boxX = Math.Min(boxX + 1, 20);
                                playerX = boxX - 1;
                                break;
                            case Direction.UP:
                                boxY = Math.Max(0, boxY - 1);
                                playerY = boxY + 1;
                                break;
                            case Direction.DOWN:
                                boxY = Math.Min(boxY + 1, 10);
                                playerY = boxY - 1;
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine($"[Error] 플레이어 이동 방향 데이터가 오류입니다. : {playerMoveDirection}");

                                return;
                        }
                        pushedBoxId = i;
                    }
                    boxPositionsX[i] = boxX;
                    boxPositionsY[i] = boxY;
                }

                // 박스와 벽의 충돌 처리
                for (int i = 0; i < BOX_COUNT; i++)
                {
                    int boxX = boxPositionsX[i];
                    int boxY = boxPositionsY[i];
                    for (int j = 0; j < WALL_COUNT; j++)
                    {
                        int wallX = wallPositionsX[j];
                        int wallY = wallPositionsY[j];

                        if (boxX == wallX && boxY == wallY)
                        {
                            switch (playerMoveDirection)
                            {
                                case Direction.LEFT:
                                    boxX = wallX + 1;
                                    playerX = boxX + 1;
                                    break;
                                case Direction.RIGHT:
                                    boxX = wallX - 1;
                                    playerX = boxX - 1;
                                    break;
                                case Direction.UP:
                                    boxY = wallY + 1;
                                    playerY = boxY + 1;
                                    break;
                                case Direction.DOWN:
                                    boxY = wallY - 1;
                                    playerY = boxY - 1;
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine($"[Error] 플레이어 이동 방향 데이터가 오류입니다. : {playerMoveDirection}");

                                    return;

                            }
                            boxPositionsX[i] = boxX;
                            boxPositionsY[i] = boxY;


                            break;
                        }
                    }
                }

                // 박스 끼리 충돌 처리		
                for (int collidedBoxId = 0; collidedBoxId < BOX_COUNT; collidedBoxId++)
                {
                    // 같은 박스라면 처리할 필요가 X
                    if (pushedBoxId == collidedBoxId)
                    {
                        continue;
                    }

                    // 두개의 박스가 부딪혔을 떄
                    if (boxPositionsX[pushedBoxId] == boxPositionsX[collidedBoxId] && boxPositionsY[pushedBoxId] == boxPositionsY[collidedBoxId])
                    {
                        switch (playerMoveDirection)
                        {
                            case Direction.LEFT:
                                boxPositionsX[pushedBoxId] = boxPositionsX[collidedBoxId] + 1;
                                playerX = boxPositionsX[pushedBoxId] + 1;

                                break;
                            case Direction.RIGHT:
                                boxPositionsX[pushedBoxId] = boxPositionsX[collidedBoxId] - 1;
                                playerX = boxPositionsX[pushedBoxId] - 1;

                                break;
                            case Direction.UP:
                                boxPositionsY[pushedBoxId] = boxPositionsY[collidedBoxId] + 1;
                                playerY = boxPositionsY[pushedBoxId] + 1;

                                break;
                            case Direction.DOWN:
                                boxPositionsY[pushedBoxId] = boxPositionsY[collidedBoxId] - 1;
                                playerY = boxPositionsY[pushedBoxId] - 1;

                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine($"[Error] 플레이어 이동 방향 데이터가 오류입니다. : {playerMoveDirection}");

                                return;
                        }

                        break;
                    }
                }


                // 박스와 골의 처리
                int boxOnGolaCount = 0;

                // 골 지점에 박스에 존재하냐?
                for (int boxId = 0; boxId < BOX_COUNT; boxId++) // 모든 골 지점에 대해서
                {
                    // 현재 박스가 골 위에 올라와 있는지 체크한다.
                    isBoxOnGoal[boxId] = false; // 없을 가능성이 높기 때문에 false로 초기화한다.

                    for (int goalId = 0; goalId < GOAL_COUNT; goalId++) // 모든 박스에 대해서
                    {
                        // 박스가 골 지점 위에 있는지 확인한다.
                        if (boxPositionsX[boxId] == goalPositionsX[goalId] && boxPositionsY[boxId] == goalPositionsY[goalId])
                        {
                            boxOnGolaCount++;

                            isBoxOnGoal[boxId] = true; // 박스가 골 위에 있다는 사실을 저장해둔다.

                            // 더이상 조사할 필요가 없으므로 탈출한다.
                            break;
                        }
                    }
                }

                // 모든 골 지점에 박스가 올라와 있다면?
                if (boxOnGolaCount == GOAL_COUNT)
                {
                    Console.Clear();
                    Console.WriteLine("축하합니다. 클리어 하셨습니다!");

                    break;
                }
            }
        }
    }
}