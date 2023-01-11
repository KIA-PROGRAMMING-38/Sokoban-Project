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

            const int MAP_MAX_Y = 23;
            const int MAP_MIN_Y = 3;
            const int MAP_MAX_X = 38;
            const int MAP_MIN_X = 6;

            const int OUTLINE_LENGTH_X = 33;
            const int OUTLINE_LENGTH_Y = 20;


            const string PLAYER_SYMBOL = "P";
            const string BOX_SYMBOL = "B";
            const string WALL_SYMBOL = "X";
            const string GOAL_SYMBOL = "O";
            const string MAP_OUTLINE_SYMBOL = "#";
            const string GOALIN_SYMBOL = "@";


            const int PLAYER_INITIAL_X = 8;
            const int PLAYER_INITIAL_Y = 4;

            const int TOTAL_BOX_NUM = 3;
            const int BOX1_INITIAL_X = 15;
            const int BOX1_INITIAL_Y = 7;
            const int BOX2_INITIAL_X = 10;
            const int BOX2_INITIAL_Y = 12;
            const int BOX3_INITIAL_X = 17;
            const int BOX3_INITIAL_Y = 14;

            const int TOTAL_WALL_NUM = 3;
            const int WALL1_INITIAL_X = 13;
            const int WALL1_INITIAL_Y = 10;
            const int WALL2_INITIAL_X = 27;
            const int WALL2_INITIAL_Y = 19;
            const int WALL3_INITIAL_X = 19;
            const int WALL3_INITIAL_Y = 9;

            const int TOTAL_GOAL_NUM = 3;
            const int GOAL1_INITIAL_X = 25;
            const int GOAL1_INITIAL_Y = 17;
            const int GOAL2_INITIAL_X = 20;
            const int GOAL2_INITIAL_Y = 12;
            const int GOAL3_INITIAL_X = 20;
            const int GOAL3_INITIAL_Y = 17;


            int playerX = PLAYER_INITIAL_X;
            int playerY = PLAYER_INITIAL_Y;

            int[] boxX = { BOX1_INITIAL_X, BOX2_INITIAL_X, BOX3_INITIAL_X };
            int[] boxY = { BOX1_INITIAL_Y, BOX2_INITIAL_Y, BOX3_INITIAL_Y };

            int[] wallX = { WALL1_INITIAL_X, WALL2_INITIAL_X, WALL3_INITIAL_X };
            int[] wallY = { WALL1_INITIAL_Y, WALL2_INITIAL_Y, WALL3_INITIAL_Y };

            int[] goalX = { GOAL1_INITIAL_X, GOAL2_INITIAL_X, GOAL3_INITIAL_X };
            int[] goalY = { GOAL1_INITIAL_Y, GOAL2_INITIAL_Y, GOAL3_INITIAL_Y };

            bool[] isBoxOnGoal = new bool[TOTAL_BOX_NUM];

            Direction playerDirection = Direction.None;
            ConsoleKey playerKey;

            bool isGameDoing = true;
            // 게임 루프 == 프레임(Frame)
            while (isGameDoing)
            {
                Console.Clear();
                Render();
                ProcessInput();
                Update();
            }
            Console.ResetColor();
            Console.Clear();
            Console.WriteLine("Game Clear!");

            // Render
            void Render()
            {
                PlayerRender();
                GoalRender();
                BoxRender();
                WallRender();
                MapOutlineRender();
            }


            void SymbolDraw(int X, int Y, string objectSymbol)
            {
                Console.SetCursorPosition(X, Y);
                Console.Write(objectSymbol);
            }

            void PlayerRender()
            {
                SymbolDraw(playerX, playerY, PLAYER_SYMBOL);
            }

            void GoalRender()
            {
                for (int goalId = 0; goalId < TOTAL_GOAL_NUM; ++goalId)
                {
                    SymbolDraw(goalX[goalId], goalY[goalId], GOAL_SYMBOL);
                }
            }

            void BoxRender()
            {
                //박스
                for (int boxId = 0; boxId < TOTAL_BOX_NUM; ++boxId)
                {
                    string symbol = null;
                    if (isBoxOnGoal[boxId])
                    {
                        symbol = GOALIN_SYMBOL;
                    }
                    else
                    {
                        symbol = BOX_SYMBOL;
                    }

                    SymbolDraw(boxX[boxId], boxY[boxId], symbol);
                }

            }

            void WallRender()
            {
                //벽
                for (int wallId = 0; wallId < TOTAL_WALL_NUM; ++wallId)
                {
                    SymbolDraw(wallX[wallId], wallY[wallId], WALL_SYMBOL);
                }
            }

            void MapOutlineRender()
            {
                //맵 테두리
                for (int i = -1; i <= OUTLINE_LENGTH_X; ++i)
                {
                    SymbolDraw(MAP_MIN_X + i, MAP_MIN_Y - 1, MAP_OUTLINE_SYMBOL);
                    SymbolDraw(MAP_MIN_X + i, MAP_MAX_Y + 1, MAP_OUTLINE_SYMBOL);
                }
                for (int i = 0; i <= OUTLINE_LENGTH_Y; ++i)
                {
                    SymbolDraw(MAP_MIN_X - 1, MAP_MIN_Y + i, MAP_OUTLINE_SYMBOL);
                    SymbolDraw(MAP_MAX_X + 1, MAP_MIN_Y + i, MAP_OUTLINE_SYMBOL);
                }
            }

            // ProcessInput
            void ProcessInput()
            {
                PlayerInput();
            }


            void PlayerInput()
            {
                playerKey = Console.ReadKey().Key; // ConsoleKeyInfo keyInfo = Console.ReadKey(); ConsoleKey playerKey = keyInfo.Key;
            }

            // Update
            void Update()
            {
                PlayerUpdate();
                BoxUpdate();
                WallUpdate();
                GoalUpdate();

            }


            void PlayerUpdate()
            {
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
            }

            void BoxUpdate()
            {
                for (int boxId = 0; boxId < TOTAL_BOX_NUM; ++boxId)
                {
                    PlayerPushBox(boxId);
                    BoxPushBox(boxId);
                }
            }
            void PlayerPushBox(int boxIndex)
            {
                // 박스에 플레이어
                if (playerX == boxX[boxIndex] && playerY == boxY[boxIndex])
                {
                    switch (playerDirection)
                    {
                        case Direction.Up:
                            boxY[boxIndex] = Math.Max(MAP_MIN_Y, --boxY[boxIndex]);
                            playerY = boxY[boxIndex] + 1;
                            break;
                        case Direction.Down:
                            boxY[boxIndex] = Math.Min(++boxY[boxIndex], MAP_MAX_Y);
                            playerY = boxY[boxIndex] - 1;
                            break;
                        case Direction.Left:
                            boxX[boxIndex] = Math.Max(MAP_MIN_X, --boxX[boxIndex]);
                            playerX = boxX[boxIndex] + 1;
                            break;
                        case Direction.Right:
                            boxX[boxIndex] = Math.Min(++boxX[boxIndex], MAP_MAX_X);
                            playerX = boxX[boxIndex] - 1;
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다.");

                            return;
                    }
                }
            }
            void BoxPushBox(int boxIndex)
            {
                // 박스에 박스
                for (int collidedBoxId = 0; collidedBoxId < TOTAL_BOX_NUM; ++collidedBoxId)
                {
                    if (boxIndex == collidedBoxId)
                    {
                        continue;
                    }
                    if (boxX[boxIndex] == boxX[collidedBoxId] && boxY[boxIndex] == boxY[collidedBoxId])
                    {
                        switch (playerDirection)
                        {
                            case Direction.Up:
                                boxY[boxIndex] = boxY[collidedBoxId] + 1;
                                playerY = boxY[boxIndex] + 1;
                                break;
                            case Direction.Down:
                                boxY[boxIndex] = boxY[collidedBoxId] - 1;
                                playerY = boxY[boxIndex] - 1;
                                break;
                            case Direction.Left:
                                boxX[boxIndex] = boxX[collidedBoxId] + 1;
                                playerX = boxX[boxIndex] + 1;
                                break;
                            case Direction.Right:
                                boxX[boxIndex] = boxX[collidedBoxId] - 1;
                                playerX = boxX[boxIndex] - 1;
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다.");

                                return;
                        }
                        break;
                    }
                }
            }

            void WallUpdate()
            {
                // 벽
                for (int wallId = 0; wallId < TOTAL_WALL_NUM; ++wallId)
                {
                    PlayerBeatWall(wallId);
                    BoxBeatWall(wallId);
                }
            }
            void PlayerBeatWall(int wallIndex)
            {
                // 벽에 플레이어
                if (playerX == wallX[wallIndex] && playerY == wallY[wallIndex])
                {
                    switch (playerDirection)
                    {
                        case Direction.Up:
                            playerY = wallY[wallIndex] + 1;
                            break;
                        case Direction.Down:
                            playerY = wallY[wallIndex] - 1;
                            break;
                        case Direction.Left:
                            playerX = wallX[wallIndex] + 1;
                            break;
                        case Direction.Right:
                            playerX = wallX[wallIndex] - 1;
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다.");

                            return;
                    }
                }
            }
            void BoxBeatWall(int wallIndex)
            {
                // 벽에 박스
                for (int boxId = 0; boxId < TOTAL_BOX_NUM; ++boxId)
                {
                    if (boxX[boxId] == wallX[wallIndex] && boxY[boxId] == wallY[wallIndex])
                    {
                        switch (playerDirection)
                        {
                            case Direction.Up:
                                boxY[boxId] = wallY[wallIndex] + 1;
                                playerY = boxY[boxId] + 1;
                                break;
                            case Direction.Down:
                                boxY[boxId] = wallY[wallIndex] - 1;
                                playerY = boxY[boxId] - 1;
                                break;
                            case Direction.Left:
                                boxX[boxId] = wallX[wallIndex] + 1;
                                playerX = boxX[boxId] + 1;
                                break;
                            case Direction.Right:
                                boxX[boxId] = wallX[wallIndex] - 1;
                                playerX = boxX[boxId] - 1;
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다.");

                                return;
                        }
                        break;
                    }
                }
            }

            void GoalUpdate()
            {
                // 골
                int goalCount = 0;
                for (int boxId = 0; boxId < TOTAL_BOX_NUM; ++boxId)
                {
                    isBoxOnGoal[boxId] = false;
                    for (int goalId = 0; goalId < TOTAL_GOAL_NUM; ++goalId)
                    {
                        if (boxX[boxId] == goalX[goalId] && boxY[boxId] == goalY[goalId])
                        {
                            ++goalCount;
                            isBoxOnGoal[boxId] = true;
                            break;
                        }
                    }
                }
                if (goalCount == TOTAL_GOAL_NUM)
                {
                    isGameDoing = false;
                }
            }
        }
    }
}