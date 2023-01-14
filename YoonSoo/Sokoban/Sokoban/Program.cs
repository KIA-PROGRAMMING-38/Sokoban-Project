using System;


namespace Sokoban
{
    enum Direction // 방향 저장하는 타입
    {
        None,
        Left,
        Right,
        Up,
        Down
    }

    class Sokoban
    {
        static void Main()
        {

            // 초기 세팅(설정)
            Console.ResetColor();                                  // 컬러를 초기화한다.
            Console.CursorVisible = false;                         // 커서를 숨긴다.
            Console.Title = "악령 어우석, 이찬혁 퇴치하자.";       // 타이틀 설정한다.
            Console.BackgroundColor = ConsoleColor.Black;       // 배경색을 설정한다.
            Console.ForegroundColor = ConsoleColor.DarkRed;    // 글꼴색을 설정한다.
            Console.Clear();                                  // 출력된 모든 내용을 지운다.

            // 기호 상수 정의
            const int GOAL_COUNT = 3;
            const int WALL_COUNT = 3;
            const int BOX_COUNT = GOAL_COUNT;

            // 플레이어 위치를 저장하기 위한 변수
            int playerX = 0;
            int playerY = 0;

            // 플레이어의 이동 방향을 저장하기 위한 변수
            Direction playerMoveDirection = Direction.None;

            // 플레이어가 무슨 박스를 밀고 있는지 저장하기 위한 변수
            int pushedBoxId = 0; // 1이면 박스1, 2이면 박스2

            // 박스 위치를 저장하기 위한 변수
            int[] boxPositionsX = { 3, 15, 5 };
            int[] boxPositionsY = { 6, 7, 3 };

            // 벽 위치를 저장하기 위한 변수
            int[] wallPositionX = { 4, 10, 2 };
            int[] wallPositionY = { 3, 8, 5 };

            // 골 위치를 저장하기 위한 변수
            int[] goalPositionX = { 10, 17, 2 };
            int[] goalPositionY = { 9, 3, 2 };

            // 골에 몇번 박스가 들어가 있는지 저장하기 위한 변수
            bool[] isBoxOnGoal = new bool[BOX_COUNT];

            // 가로 20 세로 10
            // 게임 루프 == 프레임(Freame)
            while (true)
            {
                // 이전 프레임을 지운다.
                Console.Clear();

                // -----------------------------------------------------Render-----------------------------------------------------
                // 플레이어 출력하기
                Console.SetCursorPosition(playerX, playerY);
                Console.Write("ⅰ");

                // 박스 출력하기 
                for (int i = 0; i < BOX_COUNT; ++i)
                {
                    int boxX = boxPositionsX[i];
                    int boxY = boxPositionsY[i];

                    Console.SetCursorPosition(boxX, boxY);
                    Console.Write('●');

                }

                // 골인 출력하기
                for (int goalId = 0; goalId < GOAL_COUNT; ++goalId)
                {
                    int goalX = goalPositionX[goalId];
                    int goalY = goalPositionY[goalId];

                    Console.SetCursorPosition(goalX, goalY);
                    if (isBoxOnGoal[goalId] == true)
                    {
                        Console.Write("†");
                    }
                    else
                    {
                        Console.Write('○');
                    }
                }

                // 벽 출력하기
                for (int i = 0; i < WALL_COUNT; i++)
                {
                    int wallX = wallPositionX[i];
                    int wallY = wallPositionY[i];

                    Console.SetCursorPosition(wallX, wallY);
                    Console.Write("▥");
                }



                // --------------------------------------------------ProcessINput--------------------------------------------------
                ConsoleKey key = Console.ReadKey().Key; // 저장을 해야 사용할 수 있다

                // -----------------------------------------------------Update-----------------------------------------------------

                // 플레이어 업데이트(이동 처리)
                if (key == ConsoleKey.LeftArrow) // ← 왼쪽으로 이동
                {
                    playerX = Math.Max(0, playerX - 1);
                    playerMoveDirection = Direction.Left;
                }

                if (key == ConsoleKey.RightArrow) // → 오른쪽으로 이동
                {
                    playerX = Math.Min(playerX + 1, 20);
                    playerMoveDirection = Direction.Right;
                }

                if (key == ConsoleKey.UpArrow) // ↑ 위로 이동
                {
                    playerY = Math.Max(0, playerY - 1);
                    playerMoveDirection = Direction.Up;
                }

                if (key == ConsoleKey.DownArrow) // ↓ 밑으로 이동
                {
                    playerY = Math.Min(playerY + 1, 10);
                    playerMoveDirection = Direction.Down;
                }


                // 플레이어가 벽에 부딪혔을 때
                for (int i = 0; i < WALL_COUNT; ++i)
                {
                    int wallX = wallPositionX[i];
                    int wallY = wallPositionY[i];

                    if (playerX == wallX && playerY == wallY)
                    {
                        switch (playerMoveDirection)
                        {
                            case Direction.Left: // ←
                                playerX = wallX + 1;
                                break;
                            case Direction.Right: // →
                                playerX = wallX - 1;
                                break;
                            case Direction.Up: // ↑
                                playerY = wallY + 1;
                                break;
                            case Direction.Down: // ↓
                                playerY = wallY - 1;
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다. : {playerMoveDirection}");

                                return;
                        }
                    }

                }


                // 박스 업데이트               
                //플레이어가 이동한 후
                for (int i = 0; i < BOX_COUNT; ++i)
                {
                    int boxX = boxPositionsX[i];
                    int boxY = boxPositionsY[i];

                    if (playerX == boxX && playerY == boxY) // 플레이어가 이동하고나니 박스가 있네?
                    {
                        // 박스를 움직여주면 됨
                        switch (playerMoveDirection)
                        {
                            case Direction.Left:  // ← 왼쪽으로 이동 중
                                boxX = Math.Max(0, boxX - 1);
                                playerX = boxX + 1;
                                break;
                            case Direction.Right:  // → 오른쪽으로 이동 중
                                boxX = Math.Min(boxX + 1, 20);
                                playerX = boxX - 1;
                                break;
                            case Direction.Up:  // ↑ 위로 이동 중
                                boxY = Math.Max(0, boxY - 1);
                                playerY = boxY + 1;
                                break;
                            case Direction.Down:  // ↓ 밑으로 이동 중
                                boxY = Math.Min(boxY + 1, 10);
                                playerY = boxY - 1;
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다. {playerMoveDirection}");

                                return;

                        }
                        pushedBoxId = i;
                    }
                    boxPositionsX[i] = boxX;
                    boxPositionsY[i] = boxY;
                }


                // 박스가 벽에 부딪혔을 때
                for (int i = 0; i < BOX_COUNT; ++i)
                {
                    int boxX = boxPositionsX[i];
                    int boxY = boxPositionsY[i];

                    for (int wallId = 0; wallId < WALL_COUNT; ++wallId)
                    {
                        int wallX = wallPositionX[wallId];
                        int wallY = wallPositionY[wallId];

                        if (boxX == wallX && boxY == wallY)
                        {
                            switch (playerMoveDirection)
                            {
                                case Direction.Left:
                                    boxX = wallX + 1;
                                    playerX = boxX + 1;
                                    break;

                                case Direction.Right:
                                    boxX = wallX - 1;
                                    playerX = boxX - 1;
                                    break;

                                case Direction.Up:
                                    boxY = wallY + 1;
                                    playerY = boxY + 1;
                                    break;

                                case Direction.Down:
                                    boxY = wallY - 1;
                                    playerY = boxY - 1;
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다. {playerMoveDirection}");

                                    return; // 프로그램 종료

                            }

                            boxPositionsX[i] = boxX;
                            boxPositionsY[i] = boxY;
                            break; // 박스가 동시에 같은 벽에 충돌할 일은 없을 것이다

                        }
                    }
                }


                // 박스끼리 부딪혔을 때
                for (int collidedBoxId = 0; collidedBoxId < BOX_COUNT; ++collidedBoxId)
                {
                    if (pushedBoxId == collidedBoxId)
                    {
                        continue;
                    }
                    if (boxPositionsX[pushedBoxId] == boxPositionsX[collidedBoxId] && boxPositionsY[pushedBoxId] == boxPositionsY[collidedBoxId])
                    {
                        switch (playerMoveDirection)
                        {
                            case Direction.Left:
                                boxPositionsX[pushedBoxId] += 1;
                                playerX += 1;
                                break;

                            case Direction.Right:
                                boxPositionsX[pushedBoxId] -= 1;
                                playerX -= 1;
                                break;

                            case Direction.Up:
                                boxPositionsY[pushedBoxId] += 1;
                                playerY += 1;
                                break;

                            case Direction.Down:
                                boxPositionsY[pushedBoxId] -= 1;
                                playerY -= 1;
                                break;

                            default:
                                Console.Clear();
                                Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다.");
                                return;
                        }
                    }
                }


                // 골인 지점 만들기
                // 1) Box1번과 Goal1번이 만났을 때
                // 2) Box1번과 Goal2번이 만났을 때
                // 3) Box2번과 Goal1번이 만났을 때
                // 4. Box2번과 Goal2번이 만났을 때

                int boxOnGoalCount = 0;
                for (int goalId = 0; goalId < GOAL_COUNT; goalId++)
                {
                    isBoxOnGoal[goalId] = false;

                    for (int boxId = 0; boxId < BOX_COUNT; boxId++)
                    {
                        // 박스가 골 지점 위에 있는지 확인한다.
                        if (goalPositionX[goalId] == boxPositionsX[boxId] && goalPositionY[goalId] == boxPositionsY[boxId])
                        {
                            ++boxOnGoalCount;
                            isBoxOnGoal[goalId] = true;
                            break; // goal하나에 박스 하나만 올라가 있기 때문에
                        }
                    }
                }

                if (boxOnGoalCount == GOAL_COUNT)
                {
                    Console.Clear();
                    System.Console.WriteLine("퇴.퇴.퇴.퇴.퇴.퇴.퇴.퇴.퇴.퇴.퇴.퇴.퇴.퇴.퇴");
                    Console.WriteLine("마.마.마.마.마.마.마.마.마.마.마.마.마.마.마");
                    Console.WriteLine("성.성.성.성.성.성.성.성.성.성.성.성.성.성.성");
                    Console.WriteLine("공.공.공.공.공.공.공.공.공.공.공.공.공.공.공");
                    return;
                }

            }
        }
    }

}


