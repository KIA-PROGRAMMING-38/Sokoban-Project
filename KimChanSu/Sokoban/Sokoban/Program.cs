using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    enum Direction // 방향을 저장하는 타입
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
            // 초기 세팅
            Console.ResetColor();                               // 컬러를 초기화 하는 것
            Console.CursorVisible = false;                      // 커서를 숨기기
            Console.Title = "SOKOBAN";                          // 타이틀을 설정한다.
            Console.BackgroundColor = ConsoleColor.DarkGreen;   // 배경색을 설정한다.
            Console.ForegroundColor = ConsoleColor.Yellow;      // 글꼴색을 설정한다.
            Console.Clear();                                    // 출력된 내용을 지운다.

            // 기호 상수 정의
            const int GOAL_COUNT = 3;
            const int BOX_COUNT = GOAL_COUNT;

            // 플레이어 위치를 저장하기 위한 변수
            int playerX = 0;
            int playerY = 0;

            // 플레이어의 이동 방향을 저장하기 위한 변수
            Direction playerMoveDirection = Direction.None;

            // 플레이어가 무슨 박스를 밀고 있는지 저장하기 위한 변수
            int pushedBoxId = 0; // 1이면 박스1, 2면 박스2, 3이면 박스3

            // 박스 위치를 저장하기 위한 변수
            int[] boxPositionsX = { 5, 7, 4 };
            int[] boxPositionsY = { 5, 3, 4 };

            // 벽 위치를 저장하기 위한 변수
            int wallX = 7;
            int wallY = 7;

            // 골 위치를 저장하기 위한 변수
            int[] goalPositionsX = { 9, 1, 3 };
            int[] goalPositionsY = { 9, 2, 3 };

            bool[] isBoxOnGoal = new bool[BOX_COUNT];

            // 게임 루프 구성
            while (true)
            {
                // --------------------------------- Render -----------------------------------------
                // 이전 프레임을 지운다.
                Console.Clear();

                // 플레이어를 그린다.
                Console.SetCursorPosition(playerX, playerY);
                Console.Write("P");

                // 골을 그린다.
                for (int i = 0; i < GOAL_COUNT; ++i)
                {
                    int goalX = goalPositionsX[i];
                    int goalY = goalPositionsY[i];

                    Console.SetCursorPosition(goalX, goalY);
                    Console.Write("G");
                }

                // 박스를 그린다.
                for (int boxId = 0; boxId < BOX_COUNT; ++boxId)
                {
                    int boxX = boxPositionsX[boxId];
                    int boxY = boxPositionsY[boxId];

                    Console.SetCursorPosition(boxX, boxY);

                    // 박스가 골 위에 있는지 판단
                    isBoxOnGoal[boxId] = false;

                    for (int goalId = 0; goalId < GOAL_COUNT; ++goalId) // 모든 골에 대해서 조사
                    {
                        // 박스가 골 위에 있는지
                        if (boxX == goalPositionsX[goalId] && boxY == goalPositionsY[goalId])
                        {
                            // 찾았다면 true로 바꿈
                            isBoxOnGoal[boxId] = true;

                            // 더이상 조사할 필요가 없으니 탈출
                            break;
                        }
                    }

                    if (isBoxOnGoal[boxId])
                    {
                        Console.Write("C");
                    }
                    else
                    {
                        Console.Write("B");
                    }
                }


                // 벽을 그린다.
                Console.SetCursorPosition(wallX, wallY);
                Console.Write("W");

                // --------------------------------- ProcessInput -----------------------------------------
                ConsoleKey key = Console.ReadKey().Key;

                // --------------------------------- Update -----------------------------------------

                // 플레이어 이동 처리
                if (key == ConsoleKey.LeftArrow)
                {
                    playerX = Max(0, playerX - 1);
                    playerMoveDirection = Direction.Left;
                }

                if (key == ConsoleKey.RightArrow)
                {
                    playerX = Min(playerX + 1, 20);
                    playerMoveDirection = Direction.Right;
                }

                if (key == ConsoleKey.UpArrow)
                {
                    playerY = Max(0, playerY - 1);
                    playerMoveDirection = Direction.Up;
                }

                if (key == ConsoleKey.DownArrow)
                {
                    playerY = Min(playerY + 1, 10);
                    playerMoveDirection = Direction.Down;
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
                            playerY = wallY + 1;
                            break;
                        case Direction.Down:
                            playerY = wallY - 1;
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine($"[Error] 플레이어 이동 방향 데이터가 오류입니다. : {playerMoveDirection}");

                            return;
                    }
                }

                // 박스 이동 처리
                // 플레이어가 박스를 밀었을 때라는 게 무엇을 의미하는가? => 플레이어가 이동했는데 플레이어의 위치와 박스 위치가 겹쳤다.
                for (int i = 0; i < BOX_COUNT; ++i)
                {
                    int boxX = boxPositionsX[i];
                    int boxY = boxPositionsY[i];

                    if (playerX == boxX && playerY == boxY)
                    {
                        // 박스를 민다. => 박스의 좌표를 바꾼다.
                        switch (playerMoveDirection)
                        {
                            case Direction.Left:
                                boxX = Max(0, boxX - 1);
                                playerX = boxX + 1;
                                break;
                            case Direction.Right:
                                boxX = Min(boxX + 1, 20);
                                playerX = boxX - 1;
                                break;
                            case Direction.Up:
                                boxY = Max(0, boxY - 1);
                                playerY = boxY + 1;
                                break;
                            case Direction.Down:
                                boxY = Min(boxY + 1, 10);
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
                for (int i = 0; i < BOX_COUNT; ++i)
                {
                    int boxX = boxPositionsX[i];
                    int boxY = boxPositionsY[i];

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
                                Console.WriteLine($"[Error] 플레이어 이동 방향 데이터가 오류입니다. : {playerMoveDirection}");

                                return;
                        }
                    }
                    boxPositionsX[i] = boxX;
                    boxPositionsY[i] = boxY;
                }

                // 박스끼리 충돌 처리
                if (boxPositionsX[0] == boxPositionsX[1] && boxPositionsX[0] == boxPositionsX[2] && boxPositionsX[1] == boxPositionsX[2] && boxPositionsY[0] == boxPositionsY[1] && boxPositionsY[0] == boxPositionsY[2] && boxPositionsY[1] == boxPositionsY[2])
                {
                    switch (playerMoveDirection)
                    {
                        case Direction.Left:
                            if (pushedBoxId == 1) // pushedBoxId 가 1이면 박스1
                            {
                                boxPositionsX[0] = boxPositionsX[0] + 1;
                                playerX = boxPositionsX[0] + 1;
                            }
                            else if (pushedBoxId == 2)
                            {
                                boxPositionsX[1] = boxPositionsX[1] + 1;
                                playerX = boxPositionsX[1] + 1;
                            }
                            else
                            {
                                boxPositionsX[2] = boxPositionsX[2] + 1;
                                playerX = boxPositionsX[2] + 1;
                            }
                            break;
                        case Direction.Right:
                            if (pushedBoxId == 1) // pushedBoxId 가 1이면 박스1
                            {
                                boxPositionsX[0] = boxPositionsX[0] - 1;
                                playerX = boxPositionsX[0] - 1;
                            }
                            else if (pushedBoxId == 2)
                            {
                                boxPositionsX[1] = boxPositionsX[1] - 1;
                                playerX = boxPositionsX[1] - 1;
                            }
                            else
                            {
                                boxPositionsX[2] = boxPositionsX[2] - 1;
                                playerX = boxPositionsX[2] - 1;
                            }
                            break;
                        case Direction.Up:
                            if (pushedBoxId == 1) // pushedBoxId 가 1이면 박스1
                            {
                                boxPositionsY[0] = boxPositionsY[0] + 1;
                                playerY = boxPositionsY[0] + 1;
                            }
                            else if (pushedBoxId == 2)
                            {
                                boxPositionsY[1] = boxPositionsY[1] + 1;
                                playerY = boxPositionsY[1] + 1;
                            }
                            else
                            {
                                boxPositionsY[2] = boxPositionsY[2] + 1;
                                playerY = boxPositionsY[2] + 1;
                            }
                            break;
                        case Direction.Down:
                            if (pushedBoxId == 1) // pushedBoxId 가 1이면 박스1
                            {
                                boxPositionsY[0] = boxPositionsY[0] - 1;
                                playerY = boxPositionsY[0] - 1;
                            }
                            else if (pushedBoxId == 2)
                            {
                                boxPositionsY[1] = boxPositionsY[1] - 1;
                                playerY = boxPositionsY[1] - 1;
                            }
                            else
                            {
                                boxPositionsY[2] = boxPositionsY[2] - 1;
                                playerY = boxPositionsY[2] - 1;
                            }
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine($"[Error] 플레이어 이동 방향 데이터가 오류입니다. : {playerMoveDirection}");

                            return;
                    }
                }


                // 박스와 골의 처리
                int boxOnGoalCount = 0;

                for (int goalId = 0; goalId < GOAL_COUNT; ++goalId) // 모든 골 지점에 대해서
                {
                    for (int boxId = 0; boxId < BOX_COUNT; ++boxId) // 모든 박스에 대해서
                    {
                        // 박스가 골 지점 위에 있는지 확인한다.
                        if (goalPositionsX[goalId] == boxPositionsX[boxId] && goalPositionsY[goalId] == boxPositionsY[boxId])
                        {
                            ++boxOnGoalCount;

                            break;
                        }
                    }
                }
                // 모든 골 지점에 박스가 올라와 있다면?
                if (boxOnGoalCount == GOAL_COUNT)
                {
                    Console.Clear();
                    Console.WriteLine("축하합니다. 클리어 하셨습니다.");

                    break;
                }
                int Max(int a , int b)
                {
                    int result = 0;
                    if (a > b)
                    {
                        result = a;
                    }
                    else
                    {
                        result = b;
                    }
                    return result;
                }
                int Min(int a , int b)
                {
                    return a < b ? a : b;
                }
            }
        }
    }
}