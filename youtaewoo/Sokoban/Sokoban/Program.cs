using System;
using System.Collections.Generic;
using System.Linq;
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
            Console.ResetColor(); // 컬러를 초기화 하는 것
            Console.CursorVisible = false; // 커서를 숨기기
            Console.Title = "홍성재의 파이어펀치"; // 타이틀을 설정한다.
            Console.BackgroundColor = ConsoleColor.DarkGreen; // 배경색을 설정한다.
            Console.ForegroundColor = ConsoleColor.Yellow; // 글꼴색을 설정한다.
            Console.Clear(); // 출력된 내용을 지운다.

            // 기호 상수 정의
            const int GOAL_COUNT = 2;
            const int BOX_COUNT = GOAL_COUNT;
            const int WALL_COUNT = 2;

            // 플레이어 위치를 저장하기 위한 변수
            int playerX = 0;
            int playerY = 0;

            // 플레이어의 이동 방향을 저장하기 위한 변수
            Direction playerMoveDirection = Direction.None;

            // 플레이어가 무슨 박스를 밀고 있는지 저장하기 위한 변수
            int pushedBoxId = 0; // 1이면 박스1, 2면 박스2

            // 박스 위치를 저장하기 위한 변수
            int[] boxPositionsX = { 5, 7 };
            int[] boxPositionsY = { 5, 3 };

            // 벽 위치를 저장하기 위한 변수
            int[] wallPositionsX = { 7, 5 };
            int[] wallPositionsY = { 7, 3 };

            // 골 위치를 저장하기 위한 변수
            int[] goalPositionsX = { 9, 1 };
            int[] goalPositionsY = { 9, 2 };

            // 박스가 골 위에 있는지를 저장하기 위한 변수
            bool[] isBoxOnGoal = new bool[BOX_COUNT];

            // 게임 루프 구성
            while (true)
            {
                // --------------------------------- Render -----------------------------------------
                // 이전 프레임을 지운다.
                Console.Clear();

                // 벽을 그린다.
                for (int i = 0; i < WALL_COUNT; ++i)
                {
                    int wallX = wallPositionsX[i];
                    int wallY = wallPositionsY[i];

                    Console.SetCursorPosition(wallX, wallY);
                    Console.Write("W");
                }

                // 골을 그린다.
                for (int i = 0; i < GOAL_COUNT; ++i)
                {
                    int goalX = goalPositionsX[i];
                    int goalY = goalPositionsY[i];

                    Console.SetCursorPosition(goalX, goalY);
                    Console.Write("G");
                }

                // 플레이어를 그린다.
                Console.SetCursorPosition(playerX, playerY);
                Console.Write("P");

                // 박스를 그린다.
                for (int i = 0; i < BOX_COUNT; ++i)
                {
                    int boxX = boxPositionsX[i];
                    int boxY = boxPositionsY[i];

                    Console.SetCursorPosition(boxX, boxY);
                    Console.Write("B");

                    if (boxPositionsX[i] == goalPositionsX[i] && boxPositionsY[i] == goalPositionsY[i])
                    {
                        Console.SetCursorPosition(boxX, boxY);
                        Console.Write("O");
                    }
                }

                // --------------------------------- ProcessInput -----------------------------------------
                ConsoleKey key = Console.ReadKey().Key;

                // --------------------------------- Update -----------------------------------------

                // 플레이어 이동 처리
                if (key == ConsoleKey.LeftArrow)
                {
                    playerX = Math.Max(0, playerX - 1);
                    playerMoveDirection = Direction.Left;
                }

                if (key == ConsoleKey.RightArrow)
                {
                    playerX = Math.Min(playerX + 1, 20);
                    playerMoveDirection = Direction.Right;
                }

                if (key == ConsoleKey.UpArrow)
                {
                    playerY = Math.Max(0, playerY - 1);
                    playerMoveDirection = Direction.Up;
                }

                if (key == ConsoleKey.DownArrow)
                {
                    playerY = Math.Min(playerY + 1, 10);
                    playerMoveDirection = Direction.Down;
                }

                // 플레이어와 벽의 충돌 처리 
                for (int wallId = 0; wallId < WALL_COUNT; ++wallId)
                {
                    if (playerX == wallPositionsX[wallId] && playerY == wallPositionsY[wallId])
                    {
                        switch (playerMoveDirection)
                        {
                            case Direction.Left:
                                playerX = wallPositionsX[wallId] + 1;
                                break;
                            case Direction.Right:
                                playerX = wallPositionsX[wallId] - 1;
                                break;
                            case Direction.Up:
                                playerY = wallPositionsY[wallId] + 1;
                                break;
                            case Direction.Down:
                                playerY = wallPositionsY[wallId] - 1;
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
                for (int boxId = 0; boxId < BOX_COUNT; ++boxId)
                {
                    int boxX = boxPositionsX[boxId];
                    int boxY = boxPositionsY[boxId];

                    if (playerX == boxX && playerY == boxY)
                    {
                        // 박스를 민다. => 박스의 좌표를 바꾼다.
                        switch (playerMoveDirection)
                        {
                            case Direction.Left:
                                boxX = Math.Max(0, boxX - 1);
                                playerX = boxX + 1;
                                break;
                            case Direction.Right:
                                boxX = Math.Min(boxX + 1, 20);
                                playerX = boxX - 1;
                                break;
                            case Direction.Up:
                                boxY = Math.Max(0, boxY - 1);
                                playerY = boxY + 1;
                                break;
                            case Direction.Down:
                                boxY = Math.Min(boxY + 1, 10);
                                playerY = boxY - 1;
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine($"[Error] 플레이어 이동 방향 데이터가 오류입니다. : {playerMoveDirection}");

                                return;
                        }

                        pushedBoxId = boxId;
                    }

                    boxPositionsX[boxId] = boxX;
                    boxPositionsY[boxId] = boxY;
                }

                //for (int i = 0; i < BOX_COUNT; ++i)
                //{
                //    for (int j = 0; j < WALL_COUNT; ++j)
                //    {
                //        if (boxPositionsX[i] == wallPositionsX[j] && boxPositionsY[i] == wallPositionsY[j])
                //        {
                //            switch (playerMoveDirection)
                //            {
                //                case Direction.Left:
                //                    boxPositionsX[i] = wallPositionsX[j] + 1;
                //                    playerX = boxPositionsX[i] + 1;
                //                    break;
                //                case Direction.Right:
                //                    boxPositionsX[i] = wallPositionsX[j] - 1;
                //                    playerX = boxPositionsX[i] - 1;
                //                    break;
                //                case Direction.Up:
                //                    boxPositionsY[i] = wallPositionsY[j] + 1;
                //                    playerY = boxPositionsY[i] + 1;
                //                    break;
                //                case Direction.Down:
                //                    boxPositionsY[i] = wallPositionsY[j] - 1;
                //                    playerY = boxPositionsY[i] - 1;
                //                    break;
                //                default:
                //                    Console.Clear();
                //                    Console.WriteLine($"[ERROR]");
                //                    return;
                //            }
                //            break;
                //        }
                //    }
                //}
                // 박스와 벽의 충돌 처리 
                for (int boxId = 0; boxId < BOX_COUNT; ++boxId)
                {
                    int boxX = boxPositionsX[boxId];
                    int boxY = boxPositionsY[boxId];
                    for (int wallId = 0; wallId < WALL_COUNT; ++wallId)
                    {
                        int wallX = wallPositionsX[wallId];
                        int wallY = wallPositionsY[wallId];
                        if (boxX == wallY && boxY == wallY)
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

                            boxPositionsX[boxId] = boxX;
                            boxPositionsY[boxId] = boxY;
                            break;
                        }
                    }
                }


                // 박스끼리 충돌 처리
                for (int collidedBoxId = 0; collidedBoxId < BOX_COUNT; ++collidedBoxId)
                {
                    if (pushedBoxId == collidedBoxId)
                    {
                        continue;
                    }
                    if (boxPositionsX[pushedBoxId] == boxPositionsX[collidedBoxId] && boxPositionsY[pushedBoxId] == boxPositionsY[collidedBoxId]) // 박스 1을 밀어서 박스 2에 닿은 건지, 박스 2를 밀어서 박스 1에 닿은건지?
                    {
                        switch (playerMoveDirection)
                        {
                            case Direction.Left:
                                boxPositionsX[pushedBoxId] = boxPositionsX[collidedBoxId] + 1;
                                playerX = boxPositionsX[pushedBoxId] + 1;

                                break;
                            case Direction.Right:
                                boxPositionsX[pushedBoxId] = boxPositionsX[collidedBoxId] - 1;
                                playerX = boxPositionsX[pushedBoxId] - 1;

                                break;
                            case Direction.Up:
                                boxPositionsY[pushedBoxId] = boxPositionsY[collidedBoxId] + 1;
                                playerY = boxPositionsY[pushedBoxId] + 1;

                                break;
                            case Direction.Down:
                                boxPositionsY[pushedBoxId] = boxPositionsY[collidedBoxId] - 1;
                                playerY = boxPositionsY[pushedBoxId] - 1;

                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine($"[Error] 플레이어 이동 방향 데이터가 오류입니다. : {playerMoveDirection}");

                                return;
                        }
                    }
                }


                // 박스와 골의 처리 ( 오늘한거 미반영)

                int boxOnGoalCount = 0;

                // 골 지점에 박스에 존재하냐?
                for (int goalId = 0; goalId < GOAL_COUNT; ++goalId) // 모든 골 지점에 대해서
                {
                    // 박스가 있는지 체크한다.
                    for (int boxId = 0; boxId < BOX_COUNT; ++boxId) // 모든 박스에 대해서
                    {
                        //박스가 골 지점 위에 있는지 확인한다.
                        if (boxPositionsX[boxId] == goalPositionsX[goalId] && boxPositionsY[boxId] == goalPositionsY[goalId])
                        {
                            ++boxOnGoalCount;
                            break;
                        }
                    }
                }
                // 모든 골 지점에 박스가 올라와 있다.
                if (boxOnGoalCount == GOAL_COUNT)
                {
                    Console.Clear();
                    Console.WriteLine("축하합니다. 클리어 하셨습니다.");

                    break;
                }
            }
        }
    }
}

