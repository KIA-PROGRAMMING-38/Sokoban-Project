namespace Sokoban
{
    enum Direction
    {
        None,
        Up, 
        Down, 
        Left, 
        Right,
        Max
    }
    class Program
    {
        static void Main()
        {
            // 초기 세팅
            Console.ResetColor();                           // 컬러를 초기화한다.
            Console.CursorVisible = false;                  // 커서를 숨긴다.
            Console.Title = "경이루 아카데미";               // 타이틀을 설정한다.
            Console.BackgroundColor = ConsoleColor.Magenta; // 배경색을 설정한다.
            Console.ForegroundColor = ConsoleColor.Yellow; // 글꼴색을 설정한다.
            Console.Clear();                               // 출력된 모든 내용을 지운다.

            const int MAP_MAX_Y = 26;
            const int MAP_MIN_Y = 6;
            const int MAP_MAX_X = 38;
            const int MAP_MIN_X = 6;

            const int OUTLINE_LENGTH_X = 33;
            const int OUTLINE_LENGTH_Y = 20;


            const string PLAYER_SYMBOL = "P";
            const string BOX_SYMBOL = "B";
            const string WALL_SYMBOL = "W";
            const string GOAL_SYMBOL = "G";
            const string MAP_OUTLINE_SYMBOL = "X";


            const int PLAYER_INITIAL_X = 8;
            const int PLAYER_INITIAL_Y = 7;

            const int TOTAL_BOX_NUM = 3;
            const int BOX1_INITIAL_X = 15;
            const int BOX1_INITIAL_Y = 10;
            const int BOX2_INITIAL_X = 10;
            const int BOX2_INITIAL_Y = 15;
            const int BOX3_INITIAL_X = 17;
            const int BOX3_INITIAL_Y = 17;

            const int TOTAL_WALL_NUM = 3;
            const int WALL1_INITIAL_X = 13;
            const int WALL1_INITIAL_Y = 13;
            const int WALL2_INITIAL_X = 27;
            const int WALL2_INITIAL_Y = 22;
            const int WALL3_INITIAL_X = 19;
            const int WALL3_INITIAL_Y = 12;

            const int TOTAL_GOAL_NUM = 3;
            const int GOAL1_INITIAL_X = 25;
            const int GOAL1_INITIAL_Y = 20;
            const int GOAL2_INITIAL_X = 20;
            const int GOAL2_INITIAL_Y = 15;
            const int GOAL3_INITIAL_X = 20;
            const int GOAL3_INITIAL_Y = 20;


            int playerX = PLAYER_INITIAL_X;
            int playerY = PLAYER_INITIAL_Y;

            int[] boxX = { BOX1_INITIAL_X, BOX2_INITIAL_X, BOX3_INITIAL_X };
            int[] boxY = { BOX1_INITIAL_Y, BOX2_INITIAL_Y, BOX3_INITIAL_Y };

            int[] wallX = { WALL1_INITIAL_X, WALL2_INITIAL_X, WALL3_INITIAL_X };
            int[] wallY = { WALL1_INITIAL_Y, WALL2_INITIAL_Y, WALL3_INITIAL_Y };

            int[] goalX = { GOAL1_INITIAL_X, GOAL2_INITIAL_X, GOAL3_INITIAL_X };
            int[] goalY = { GOAL1_INITIAL_Y, GOAL2_INITIAL_Y, GOAL3_INITIAL_Y };

            Direction playerDirection = default;
            int pushedBoxID = default;

            // 게임 루프 == 프레임(Frame)
            while (true)
            {
                // -------------------------------------- Render ------------------------------------------------
                Console.Clear();
                
                //플레이어
                Console.SetCursorPosition(playerX, playerY);
                Console.Write(PLAYER_SYMBOL);

                //박스
                for (int i = 0; i < TOTAL_BOX_NUM; ++i)
                {
                    Console.SetCursorPosition(boxX[i], boxY[i]);
                    Console.Write(BOX_SYMBOL);
                }

                //벽
                for (int i = 0; i < TOTAL_WALL_NUM; ++i)
                {
                    Console.SetCursorPosition(wallX[i], wallY[i]);
                    Console.Write(WALL_SYMBOL);
                }

                //골
                for (int i = 0; i < TOTAL_GOAL_NUM; ++i)
                {
                    Console.SetCursorPosition(goalX[i], goalY[i]);
                    Console.Write(GOAL_SYMBOL);
                }

                //맵 테두리
                for (int i = -1; i <= OUTLINE_LENGTH_X; ++i)
                {
                    Console.SetCursorPosition(MAP_MIN_X + i, MAP_MIN_Y - 1);
                    Console.Write(MAP_OUTLINE_SYMBOL);
                    Console.SetCursorPosition(MAP_MIN_X + i, MAP_MAX_Y + 1);
                    Console.Write(MAP_OUTLINE_SYMBOL);
                }
                for (int i = 0; i <= OUTLINE_LENGTH_Y; ++i)
                {
                    Console.SetCursorPosition(MAP_MIN_X - 1, MAP_MIN_Y + i);
                    Console.Write(MAP_OUTLINE_SYMBOL);
                    Console.SetCursorPosition(MAP_MAX_X + 1, MAP_MIN_Y + i);
                    Console.Write(MAP_OUTLINE_SYMBOL);
                }
                // -------------------------------------- ProcessInput ------------------------------------------------
                ConsoleKey playerKey = Console.ReadKey().Key; // ConsoleKeyInfo keyInfo = Console.ReadKey(); ConsoleKey playerKey = keyInfo.Key;
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
                for (int i = 0; i < TOTAL_BOX_NUM; ++i)
                {
                    // 박스에 플레이어
                    if (playerX == boxX[i] && playerY == boxY[i])
                    {
                        switch (playerDirection)
                        {
                            case Direction.Up:
                                boxY[i] = Math.Max(MAP_MIN_Y, --boxY[i]);
                                playerY = boxY[i] + 1;
                                break;
                            case Direction.Down:
                                boxY[i] = Math.Min(++boxY[i], MAP_MAX_Y);
                                playerY = boxY[i] - 1;
                                break;
                            case Direction.Left:
                                boxX[i] = Math.Max(MAP_MIN_X, --boxX[i]);
                                playerX = boxX[i] + 1;
                                break;
                            case Direction.Right:
                                boxX[i] = Math.Min(++boxX[i], MAP_MAX_X);
                                playerX = boxX[i] - 1;
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다.");

                                return;
                        }
                        pushedBoxID = i;
                    }
                    // 박스에 박스
                    for (int j = 0; j < TOTAL_BOX_NUM; ++j)
                    {
                        if (i != j && boxX[j] == boxX[i] && boxY[j] == boxY[i])
                        {
                            switch (playerDirection)
                            {
                                case Direction.Up:
                                    if (pushedBoxID == j)
                                    {
                                        ++boxY[j];
                                        ++playerY;
                                    }
                                    else
                                    {
                                        ++boxY[i];
                                        ++playerY;
                                    }
                                    break;
                                case Direction.Down:
                                    if (pushedBoxID == j)
                                    {
                                        --boxY[j];
                                        --playerY;
                                    }
                                    else
                                    {
                                        --boxY[i];
                                        --playerY;
                                    }
                                    break;
                                case Direction.Left:
                                    if (pushedBoxID == j)
                                    {
                                        ++boxX[j];
                                        ++playerX;
                                    }
                                    else
                                    {
                                        ++boxX[i];
                                        ++playerX;
                                    }
                                    break;
                                case Direction.Right:
                                    if (pushedBoxID == j)
                                    {
                                        --boxX[j];
                                        --playerX;
                                    }
                                    else
                                    {
                                        --boxX[i];
                                        --playerX;
                                    }
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다.");

                                    return;
                            }
                        }
                    }
                }
               
                // 벽
                for (int i = 0; i < TOTAL_WALL_NUM; ++i)
                {
                    // 벽에 플레이어
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
                            default:
                                Console.Clear();
                                Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다.");

                                return;
                        }
                    }
                    // 벽에 박스
                    for (int j = 0; j < TOTAL_BOX_NUM; ++j)
                    {
                        if (boxX[j] == wallX[i] && boxY[j] == wallY[i])
                        {
                            switch (playerDirection)
                            {
                                case Direction.Up:
                                    ++boxY[j];
                                    ++playerY;
                                    break;
                                case Direction.Down:
                                    --boxY[j];
                                    --playerY;
                                    break;
                                case Direction.Left:
                                    ++boxX[j];
                                    ++playerX;
                                    break;
                                case Direction.Right:
                                    --boxX[j];
                                    --playerX;
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다.");

                                    return;
                            }
                        }
                    }

                }

                // 골
                int goalCount = 0;
                for (int i = 0; i < TOTAL_BOX_NUM; ++i)
                {
                    for (int j = 0; j < TOTAL_GOAL_NUM; ++j)
                    {
                        if (boxX[i] == goalX[j] && boxY[i] == goalY[j])
                        {
                            ++goalCount;
                        }
                    }
                }
                if (goalCount == TOTAL_GOAL_NUM)
                {
                    break;
                }
            }
            Console.ResetColor();
            Console.Clear();
            Console.WriteLine("Game Clear!");
        }
    }
}