﻿using System;
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
            // a와 b 중 최댓값을 구한다.
            int Max(int a, int b)
            {
                int result = 0;
                if (a < b)
                {
                    result = b;
                }
                else
                {
                    result = a;
                }

                return result;
            }

            int Min(int a, int b) => (a < b) ? a : b; // 괄호 없어도 ㄷ

            // int 타입의 데이터를 여러 개 받을 수 있다.
            int VariadicMax(params int[] numbers)
            {
                
                int result = numbers[0];

                for (int i = 1; i < numbers.Length; ++i)
                {
                    if (result < numbers[i])
                    {
                        result = numbers[i];
                    }
                }
                return result;
            }
            

            // 초기 세팅
            Console.ResetColor(); // 컬러를 초기화 하는 것
            Console.CursorVisible = false; // 커서를 숨기기
            Console.Title = "홍성재의 파이어펀치"; // 타이틀을 설정한다.
            Console.BackgroundColor = ConsoleColor.DarkGreen; // 배경색을 설정한다.
            Console.ForegroundColor = ConsoleColor.Yellow; // 글꼴색을 설정한다.
            Console.Clear(); // 출력된 내용을 지운다.

            // 기호 상수 정의
            const int GOAL_COUNT = 2;
            const int WALL_COUNT = 2;
            const int BOX_COUNT = GOAL_COUNT;

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
            int[] wallPositionX = { 4, 7 };
            int[] wallPositionY = { 3, 7 };

            // 골 위치를 저장하기 위한 변수
            int[] goalPositionsX = { 9, 1 };
            int[] goalPositionsY = { 9, 2 };

            // 골에 몇번 박스가 들어가 있는지 저장하기 위한 변수
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
                for (int goalId = 0; goalId < GOAL_COUNT; ++goalId)
                {
                    int goalX = goalPositionsX[goalId];
                    int goalY = goalPositionsY[goalId];

                    Console.SetCursorPosition(goalX, goalY);
                    Console.Write("G");
                }

                // 박스를 그린다.
                for (int i = 0; i < BOX_COUNT; ++i)
                {
                    int boxX = boxPositionsX[i];
                    int boxY = boxPositionsY[i];

                    Console.SetCursorPosition(boxX, boxY);

                    if (isBoxOnGoal[i] == true)
                    {
                        Console.Write("*");
                    }
                    else
                    {
                        Console.Write("B");
                    }

                }


                // 벽을 그린다.
                for (int i = 0; i < WALL_COUNT; ++i)
                {
                    int wallX = wallPositionX[i];
                    int wallY = wallPositionY[i];

                    Console.SetCursorPosition(wallX, wallY);
                    Console.Write("W");
                }




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
                for (int i = 0; i < WALL_COUNT; ++i)
                {
                    int wallX = wallPositionX[i];
                    int wallY = wallPositionY[i];

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

                        boxPositionsX[i] = boxX;
                        boxPositionsY[i] = boxY;
                        pushedBoxId = i;
                    }


                }


                // 박스와 벽의 충돌 처리
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
                                    Console.WriteLine($"[Error] 플레이어 이동 방향 데이터가 오류입니다. : {playerMoveDirection}");

                                    return;
                            }

                            boxPositionsX[i] = boxX;
                            boxPositionsY[i] = boxY;
                            break; // 박스가 동시에 같은 벽에 부딪힐 일은 없기 때문에
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
                                Console.WriteLine($"[Error] 플레이어 이동 방향 데이터가 잘못 되었습니다.");
                                return;
                        }
                    }
                }

                // 박스와 골의 처리
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
                        if (goalPositionsX[goalId] == boxPositionsX[boxId] && goalPositionsY[goalId] == boxPositionsY[boxId])
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
                    Console.WriteLine("축하합니다!!");
                    break;
                }

            }
        }
    }
}