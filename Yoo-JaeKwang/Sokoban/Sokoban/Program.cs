﻿namespace Sokoban
{
    enum Direction
    {
        Up = 1, 
        Down, 
        Left, 
        Right
    }
    class Program
    {
        static void Main()
        {
            // 초기 세팅
            Console.ResetColor();                                   // 컬러를 초기화한다.
            Console.CursorVisible = false;                          // 커서를 숨긴다.
            Console.Title = "경이루 아카데미";                       // 타이틀을 설정한다.
            Console.BackgroundColor = ConsoleColor.Magenta;         // 배경색을 설정한다.
            Console.ForegroundColor = ConsoleColor.Yellow;         // 글꼴색을 설정한다.
            Console.Clear();                                       // 출력된 모든 내용을 지운다.

            const int MAP_MAX_Y = 21;
            const int MAP_MIN_Y = 1;
            const int MAP_MAX_X = 32;
            const int MAP_MIN_X = 1;


            const string PLAYER_SYMBOL = "P";
            const string BOX_SYMBOL = "B";
            const string WALL_SYMBOL = "X";
            const string GOAL_SYMBOL = "G";


            const int PLAYER_INITIAL_X = 3;
            const int PLAYER_INITIAL_Y = 2;

            const int BOX1_INITIAL_X = 10;
            const int BOX1_INITIAL_Y = 5;
            const int BOX2_INITIAL_X = 5;
            const int BOX2_INITIAL_Y = 10;
            const int BOX3_INITIAL_X = 12;
            const int BOX3_INITIAL_Y = 12;

            const int WALL1_INITIAL_X = 8;
            const int WALL1_INITIAL_Y = 8;
            const int WALL2_INITIAL_X = 22;
            const int WALL2_INITIAL_Y = 17;
            const int WALL3_INITIAL_X = 14;
            const int WALL3_INITIAL_Y = 7;

            const int GOAL1_INITIAL_X = 20;
            const int GOAL1_INITIAL_Y = 15;
            const int GOAL2_INITIAL_X = 15;
            const int GOAL2_INITIAL_Y = 10;
            const int GOAL3_INITIAL_X = 15;
            const int GOAL3_INITIAL_Y = 15;


            int playerX = PLAYER_INITIAL_X;
            int playerY = PLAYER_INITIAL_Y;

            int[] boxX = { BOX1_INITIAL_X, BOX2_INITIAL_X, BOX3_INITIAL_X };
            int[] boxY = { BOX1_INITIAL_Y, BOX2_INITIAL_Y, BOX3_INITIAL_Y };

            int[] wallX = { WALL1_INITIAL_X, WALL2_INITIAL_X, WALL3_INITIAL_X };
            int[] wallY = { WALL1_INITIAL_Y, WALL2_INITIAL_Y, WALL3_INITIAL_Y };

            int[] goalX = { GOAL1_INITIAL_X, GOAL2_INITIAL_X, GOAL3_INITIAL_X };
            int[] goalY = { GOAL1_INITIAL_Y, GOAL2_INITIAL_Y, GOAL3_INITIAL_Y };

            Direction playerDirection = default;

            // 게임 루프 == 프레임(Frame)
            while (true)
            {
                Console.Clear();
                // -------------------------------------- Render ------------------------------------------------
                //플레이어
                Console.SetCursorPosition(playerX, playerY);
                Console.Write(PLAYER_SYMBOL);

                //박스
                for (int i = 0; i < boxX.Length; ++i)
                {
                    Console.SetCursorPosition(boxX[i], boxY[i]);
                    Console.Write(BOX_SYMBOL);
                }

                //벽
                for (int i = 0; i < wallX.Length; ++i)
                {
                    Console.SetCursorPosition(wallX[i], wallY[i]);
                    Console.Write(WALL_SYMBOL);
                }

                //골
                for (int i = 0; i < goalX.Length; ++i)
                {
                    Console.SetCursorPosition(goalX[i], goalY[i]);
                    Console.Write(GOAL_SYMBOL);
                }
                // -------------------------------------- ProcessInput ------------------------------------------------
                ConsoleKey playerKey = Console.ReadKey().Key; // ConsoleKeyInfo keyInfo = Console.ReadKey(); ConsoleKey key = keyInfo.Key;
                // -------------------------------------- Update ------------------------------------------------
                
                // 플레이어

                // 플레이어의 이동
                if (playerKey == ConsoleKey.UpArrow)
                {
                    playerY = Math.Max(MAP_MIN_Y, --playerY);
                    playerDirection = Direction.Up;
                }
                if (playerKey == ConsoleKey.DownArrow)
                {
                    playerY = Math.Min(++playerY, MAP_MAX_Y);
                    playerDirection = Direction.Down;
                }
                if (playerKey == ConsoleKey.LeftArrow)
                {
                    playerX = Math.Max(MAP_MIN_X, --playerX);
                    playerDirection = Direction.Left;
                }
                if (playerKey == ConsoleKey.RightArrow)
                {
                    playerX = Math.Min(++playerX, MAP_MAX_X);
                    playerDirection = Direction.Right;
                }

                // 박스
                for (int i = 0; i < boxX.Length; ++i)
                {
                    if (playerX == boxX[i] && playerY == boxY[i])
                    {
                        switch (playerDirection)
                        {
                            case Direction.Up:
                                if (boxY[i] == MAP_MIN_Y)
                                {
                                    playerY = MAP_MIN_Y + 1;
                                }
                                else
                                {
                                    --boxY[i];
                                }
                                break;
                            case Direction.Down:
                                if (boxY[i] == MAP_MAX_Y)
                                {
                                    playerY = MAP_MAX_Y - 1;
                                }
                                else
                                {
                                    ++boxY[i];
                                }
                                break;
                            case Direction.Left:
                                if (boxX[i] == MAP_MIN_X)
                                {
                                    playerX = MAP_MIN_X + 1;
                                }
                                else
                                {
                                    --boxX[i];
                                }
                                break;
                            case Direction.Right:
                                if (boxX[i] == MAP_MAX_X)
                                {
                                    playerX = MAP_MAX_X - 1;
                                }
                                else
                                {
                                    ++boxX[i];
                                }
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다.");

                                return;
                        }
                    }

                }

                // 박스에 박스
                for (int i = 0; i < boxX.Length; ++i)
                {
                    for (int j = 0; j < boxX.Length; ++j)
                    {
                        if ( i != j && boxX[i] == boxX[j] && boxY[i] == boxY[j])
                        {
                            switch (playerDirection)
                            {
                                case Direction.Up:
                                    ++boxY[i];
                                    ++playerY;
                                    break;
                                case Direction.Down:
                                    --boxY[i];
                                    --playerY;
                                    break;
                                case Direction.Left:
                                    ++boxX[i];
                                    ++playerX;
                                    break;
                                case Direction.Right:
                                    --boxX[i];
                                    --playerX;
                                    break;
                            }
                        }
                    }
                }

                // 벽

                // 벽에 사람
                for (int i = 0; i < wallX.Length; ++i)
                {
                    if (playerX == wallX[i] && playerY == wallY[i])
                    {
                        switch (playerDirection)
                        {
                            case Direction.Up:
                                ++playerY;
                                break;
                            case Direction.Down:
                                --playerY;
                                break;
                            case Direction.Left:
                                ++playerX;
                                break;
                            case Direction.Right:
                                --playerX;
                                break;
                        }
                    }

                }

                // 벽에 박스
                for (int i = 0; i < boxX.Length; ++i)
                {
                    for (int j = 0; j < wallX.Length; ++j)
                    {
                        if (boxX[i] == wallX[j] && boxY[i] == wallY[j])
                        {
                            switch (playerDirection)
                            {
                                case Direction.Up:
                                    ++boxY[i];
                                    ++playerY;
                                    break;
                                case Direction.Down:
                                    --boxY[i];
                                    --playerY;
                                    break;
                                case Direction.Left:
                                    ++boxX[i];
                                    ++playerX;
                                    break;
                                case Direction.Right:
                                    --boxX[i];
                                    --playerX;
                                    break;
                            }
                        }

                    }
                }

                // 골인
                int goalCount = 0;
                for (int i = 0; i < boxX.Length; ++i)
                {
                    for (int j = 0; j < goalX.Length; ++j)
                    {
                        if (boxX[i] == goalX[j] && boxY[i] == goalY[j])
                        {
                            ++goalCount;
                            if (goalCount == goalX.Length)
                            {
                                goto endGame;
                            }
                        }
                    }
                }

            }
            endGame:
            Console.Clear();
            Console.WriteLine("Game Clear!");
        }
    }
}