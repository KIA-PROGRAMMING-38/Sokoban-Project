using System;

namespace Sokoban
{
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
        static void Main()
        {
            // 초기 세팅
            Console.ResetColor();
            Console.CursorVisible = false;
            Console.Title = "Huiji_Sokoban";
            // 맥 backgroundColor 실화냐
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Clear();

            // 기호 상수 정의
            const int WALL_COUNT = 4;
            const int BOX_COUNT = 3;
            const int GOAL_COUNT = 3;
            const int MAP_MIN_X = 1;
            const int MAP_MAX_X = 19;
            const int MAP_MIN_Y = 1;
            const int MAP_MAX_Y = 9;
            
            // 플레이어 초기 위치설정
            int playerX = 1;
            int playerY = 1;

            // 박스 초기 위치설정
            int[] boxPositionX = { 5, 7, 15 };
            int[] boxPositionY = { 3, 6, 8 };

            // wall 위치설정
            int[] wallPositionX = { 3, 4, 7, 8 };
            int[] wallPositionY = { 3, 3, 8, 8 };

            // goal 위치설정
            int[] goalPositionX = { 18, 10, 5 };
            int[] goalPositionY = { 3, 6, 8};

            //플레이어 이동방향을 저장하기 위한 변수
            Direction playerDirection = Direction.None;

            //밀고 있는 박스를 저장하기 위한 변수
            int pushedBoxId = 0;

            //박스가 들어간 골을 저장하기 위한 변수
            bool[] goalIn = new bool[GOAL_COUNT];

            while (true)
            {
                //Render------------------------------------------------------------
                Console.Clear();
            
                // wall 출력
                for (int wallCount = 0; wallCount < WALL_COUNT; ++wallCount)
                {
                    int wallX = wallPositionX[wallCount];
                    int wallY = wallPositionY[wallCount];
                    Console.SetCursorPosition(wallX, wallY);
                    Console.Write("⎕");
                }
                for (int MapX = 0; MapX <= 20; ++MapX)
                {
                    Console.SetCursorPosition(MapX, 0);
                    Console.Write("⎕");
                    Console.SetCursorPosition(MapX, 10);
                    Console.Write("⎕");
                }
                for (int MapY = 1; MapY < 10; ++MapY)
                {
                    Console.SetCursorPosition(0, MapY);
                    Console.Write("⎕");
                    Console.SetCursorPosition(20, MapY);
                    Console.Write("⎕");
                }

                // box 출력
                for (int boxCount = 0; boxCount < BOX_COUNT; ++boxCount)
                {
                    int boxX = boxPositionX[boxCount];
                    int boxY = boxPositionY[boxCount];
                    Console.SetCursorPosition(boxX, boxY);
                    Console.Write("✦");
                }
                // goal 출력
                for (int goalId = 0; goalId < GOAL_COUNT; ++goalId)
                {
                    int goalX = goalPositionX[goalId];
                    int goalY = goalPositionY[goalId];
                    Console.SetCursorPosition(goalX, goalY);
                    
                    if (goalIn[goalId] == true)
                    {
                        Console.Write("♥︎");
                    }
                    else
                    {
                        Console.Write("✪");
                    }
                }

                // 플레이어 출력
                Console.SetCursorPosition(playerX, playerY);
                Console.Write("☻");

                //Process Input-----------------------------------------------------
                ConsoleKey key = Console.ReadKey().Key;
                //Update------------------------------------------------------------

                //player 이동
                if (key == ConsoleKey.LeftArrow)
                {
                    playerX = Math.Max(MAP_MIN_X, playerX - 1);
                    playerDirection = Direction.Left;
                }
                if (key == ConsoleKey.RightArrow)
                {
                    playerX = Math.Min(playerX + 1, MAP_MAX_X);
                    playerDirection = Direction.Right;
                }
                if (key == ConsoleKey.UpArrow)
                {
                    playerY = Math.Max(MAP_MIN_Y, playerY - 1);
                    playerDirection = Direction.Up;
                }
                if (key == ConsoleKey.DownArrow)
                {
                    playerY = Math.Min(playerY + 1, MAP_MAX_Y);
                    playerDirection = Direction.Down;
                }

                //player가 박스를 밀때
                for (int boxId = 0; boxId < BOX_COUNT; ++boxId)
                {
                    int boxX = boxPositionX[boxId];
                    int boxY = boxPositionY[boxId];
                    if (playerX == boxX && playerY == boxY)
                    {
                        switch (playerDirection)
                        {
                            case Direction.Left:
                                boxX = Math.Max(playerX - 1, MAP_MIN_X);
                                break;

                            case Direction.Right:
                                boxX = Math.Min(playerX + 1, MAP_MAX_X);
                                break;

                            case Direction.Up:
                                boxY = Math.Max(playerY - 1, MAP_MIN_X);
                                    break;

                            case Direction.Down:
                                boxY = Math.Min(playerY + 1, MAP_MAX_Y);
                                break;

                            case Direction.None:
                                Console.Clear();
                                Console.WriteLine("[Error]플레이어 이동방향 데이터가 잘못 되었습니다.");
                                break;
                        }
                        boxPositionX[boxId] = boxX;
                        boxPositionY[boxId] = boxY;
                        pushedBoxId = boxId;
                    }
                }

                //player가 벽에 부딪힐 때
                for (int wallId = 0; wallId < WALL_COUNT; ++wallId)
                {
                    int wallX = wallPositionX[wallId];
                    int wallY = wallPositionY[wallId];
                    if (playerX == wallX && playerY == wallY)
                    {
                        switch (playerDirection)
                        {
                            case Direction.Left:
                                playerX += 1;
                                break;

                            case Direction.Right:
                                playerX -= 1;
                                break;

                            case Direction.Up:
                                playerY += 1;
                                break;

                            case Direction.Down:
                                playerY -= 1;
                                break;

                            case Direction.None:
                                Console.Clear();
                                Console.WriteLine("플레이어 이동방향 데이터가 잘못 되었습니다.");
                                break;
                        }
                    }
                }
                //box가 벽에 부딪힐 때
                for (int boxId = 0; boxId < BOX_COUNT; ++boxId)
                {
                    int boxX = boxPositionX[boxId];
                    int boxY = boxPositionY[boxId];
                    for (int wallId = 0; wallId < WALL_COUNT; ++wallId)
                    {
                        int wallX = wallPositionX[wallId];
                        int wallY = wallPositionY[wallId];
                        if (boxX == wallX && boxY == wallY)
                        {
                            switch (playerDirection)
                            {
                                case Direction.Left:
                                    boxX += 1;
                                    playerX +=1;
                                    break;

                                case Direction.Right:
                                    boxX -= 1;
                                    playerX -= 1;
                                    break;

                                case Direction.Up:
                                    boxY += 1;
                                    playerY += 1;
                                    break;

                                case Direction.Down:
                                    boxY -= 1;
                                    playerY -= 1;
                                    break;

                                case Direction.None:
                                    Console.Clear();
                                    Console.WriteLine("[Error]플레이어 이동방향 데이터가 잘못 되었습니다");
                                    break;
                            }
                        }
                        boxPositionX[boxId] = boxX;
                        boxPositionY[boxId] = boxY;
                        break;
                    }
                }
                
                // 박스랑 박스가 부딪힐 때

                for (int CollidedBoxId = 0; CollidedBoxId < BOX_COUNT; ++CollidedBoxId)
                {
                    if (CollidedBoxId == pushedBoxId)
                    {
                        continue;
                    }
                    if (boxPositionX[pushedBoxId] == boxPositionX[CollidedBoxId] && boxPositionY[pushedBoxId] == boxPositionY[CollidedBoxId])
                    {
                        switch (playerDirection)
                        {
                            case Direction.Left:
                                boxPositionX[pushedBoxId] += 1;
                                playerX += 1;
                                break;

                            case Direction.Right:
                                boxPositionX[pushedBoxId] -= 1;
                                playerX -= 1;
                                break;

                            case Direction.Up:
                                boxPositionY[pushedBoxId] += 1;
                                playerY += 1;
                                break;

                            case Direction.Down:
                                boxPositionY[pushedBoxId] -= 1;
                                playerY -= 1;
                                break;
                        }
                    }                    
                }

                // goal 에 box가 들어가면? 다 들어가야함

                int count = 0;

                for (int goalId = 0; goalId < GOAL_COUNT; ++goalId)
                {
                    for (int boxId = 0; boxId < BOX_COUNT; ++boxId)
                    {
                        goalIn[goalId] = false;

                        if (goalPositionX[goalId] == boxPositionX[boxId] && goalPositionY[goalId] == boxPositionY[boxId])
                        {
                            goalIn[goalId] = true;
                            ++count;
                            break;
                        }                        
                    }
                    if (count == GOAL_COUNT)
                    {
                        Console.Clear();
                        Console.WriteLine("축하!!");
                        return;
                    }
                }                
            }            
        }
    }
}