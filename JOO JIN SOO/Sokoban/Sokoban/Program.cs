namespace Sokoban
{
    #region 열거형 모음
    enum PlayerDirection // 방향을 저장하는 타입
    {
        NONE,
        LEFT,
        RIGHT,
        UP,
        DOWN
    }
    #endregion

    class Program
    {
        static void Main()
        {
            #region 초기 세팅
            Console.ResetColor();   // 컬러 초기화
            Console.CursorVisible = false;  // 커서 숨기기
            Console.Title = "마이노이 라비린스 월.mk2";  // 타이틀 설정
            Console.BackgroundColor = ConsoleColor.Green;   // 배경색 설정
            Console.ForegroundColor = ConsoleColor.Black;   // 글꼴색 설정
            Console.Clear();    // 출력된 내용 지우기
            #endregion

            #region 상수 모음
            // 플레이어 관련 상수
            const int playerInitialX = 10;
            const int playerInitialY = 5;
            const string playerSymbol = "P";

            // 플레이어 이동 방향 저장 변수
            PlayerDirection playerDirectionMoveNumber = PlayerDirection.NONE;

            // 박스 관련 상수
            const int FirstBox_X = 5;
            const int FirstBox_Y = 5;
            const int SecondBox_X = 9;
            const int SecondBox_Y = 9;
            const int ThirdBox_X = 3;
            const int ThirdBox_Y = 3;
            const string boxSymbol = "O";
            const int BOX_COUNT = 3;

            // 벽 관련 상수
            const int wallX_LEFT = 0;
            const int wallX_RIGHT = 21;
            const int wallY_UP = 0;
            const int wallY_DOWN = 16;
            const string wallSymbol = "$";

            // 장애물 관련 상수
            const int FIRST_OBTICLE_X = 12;
            const int FIRST_OBTICLE_Y = 8;
            const int SECOND_OBTICLE_X = 13;
            const int SECOND_OBTICLE_Y = 8;
            const int THIRD_OBTICLE_X = 12;
            const int THIRD_OBTICLE_Y = 7;
            const int FOURTH_OBTICLE_X = 13;
            const int FOURTH_OBTICLE_Y = 7;

            const int FIFTH_OBTICLE_X = 2;
            const int FIFTH_OBTICLE_Y = 10;
            const int SIXTH_OBTICLE_X = 3;
            const int SIXTH_OBTICLE_Y = 10;
            const int SEVEN_OBTICLE_X = 4;
            const int SEVEN_OBTICLE_Y = 10;
            const int EIGHTH_OBTICLE_X = 5;
            const int EIGHTH_OBTICLE_Y = 10;
            const int NINTH_OBTICLE_X = 6;
            const int NINTH_OBTICLE_Y = 10;
            const int TENTH_OBTICLE_X = 7;
            const int TENTH_OBTICLE_Y = 10;
            const string obticleSymbol = "W";
            const int OBTICLE_COUNT = 10;

            // 골 관련 상수
            const int FIRST_GOAL_X = 14;
            const int FIRST_GOAL_Y = 7;
            const int SECOND_GOAL_X = 2;
            const int SECOND_GOAL_Y = 4;
            const int THIRD_GOAL_X = 4;
            const int THIRD_GOAL_Y = 5;
            const string goalSymbol = "G";
            const int GOAL_COUNT = 3;
            int count = 0;

            // 인 게임 변수모음
            int playerX = playerInitialX;
            int playerY = playerInitialY;

            int[] boxX = { FirstBox_X, SecondBox_X, ThirdBox_X };
            int[] boxY = { FirstBox_Y, SecondBox_Y, ThirdBox_Y };

            int[] obticleX = { FIRST_OBTICLE_X, SECOND_OBTICLE_X, THIRD_OBTICLE_X, FOURTH_OBTICLE_X, FIFTH_OBTICLE_X, SIXTH_OBTICLE_X, SEVEN_OBTICLE_X, EIGHTH_OBTICLE_X, NINTH_OBTICLE_X, TENTH_OBTICLE_X };
            int[] obticleY = { FIRST_OBTICLE_Y, SECOND_OBTICLE_Y, THIRD_OBTICLE_Y, FOURTH_OBTICLE_Y, FIFTH_OBTICLE_Y, SIXTH_OBTICLE_Y, SEVEN_OBTICLE_Y, EIGHTH_OBTICLE_Y, NINTH_OBTICLE_Y, TENTH_OBTICLE_Y };

            int[] goalX = { FIRST_GOAL_X, SECOND_GOAL_X, THIRD_GOAL_X };
            int[] goalY = { FIRST_GOAL_Y, SECOND_GOAL_Y, THIRD_GOAL_Y };

            bool[] isBoxOnGoal = new bool[GOAL_COUNT];
            #endregion

            // 프레임 워크 구성
            while (true)
            {
                #region Render
                // Render------------------------------------------------------------------------------------------
                Console.Clear();    // 이전 프레임 지우기

                Console.SetCursorPosition(playerX, playerY);
                Console.Write(playerSymbol);

                // 골 구현
                for (int i = 0; i < GOAL_COUNT; ++i)
                {
                    Console.SetCursorPosition(goalX[i], goalY[i]);
                    Console.Write(goalSymbol);
                }

                // 박스 출력
                for (int i = 0; i < BOX_COUNT; ++i)
                {
                    Console.SetCursorPosition(boxX[i], boxY[i]);

                    if (isBoxOnGoal[i])
                    {
                        Console.Write("@");
                    }
                    else
                    {
                        Console.Write(boxSymbol);
                    }
                }

                // 좌우 벽 구현
                for (int i = 1; i < wallY_DOWN; ++i)
                {
                    Console.SetCursorPosition(wallX_LEFT, i);
                    Console.Write(wallSymbol);

                    Console.SetCursorPosition(wallX_RIGHT, i);
                    Console.Write(wallSymbol);
                }
                for (int i = 0; i - 1 < wallX_RIGHT; ++i)
                {
                    Console.SetCursorPosition(i, wallY_UP);
                    Console.Write(wallSymbol);

                    Console.SetCursorPosition(i, wallY_DOWN);
                    Console.Write(wallSymbol);
                }

                // 장애물 구현
                for (int i = 0; i < OBTICLE_COUNT; ++i)
                {
                    Console.SetCursorPosition(obticleX[i], obticleY[i]);
                    Console.Write(obticleSymbol);
                }
                #endregion

                #region ProcessInput
                // ProcessInput------------------------------------------------------------------------------------
                ConsoleKey key = Console.ReadKey().Key;
                #endregion

                #region Update
                // Update------------------------------------------------------------------------------------------
                #region 방향키 이동시
                if (key == ConsoleKey.LeftArrow)
                {
                    playerX = Math.Max(1, playerX - 1);
                    playerDirectionMoveNumber = PlayerDirection.LEFT;
                }

                if (key == ConsoleKey.RightArrow)
                {
                    playerX = Math.Min(playerX + 1, 20);
                    playerDirectionMoveNumber = PlayerDirection.RIGHT;
                }

                if (key == ConsoleKey.UpArrow)
                {
                    playerY = Math.Max(1, playerY - 1);
                    playerDirectionMoveNumber = PlayerDirection.UP;
                }

                if (key == ConsoleKey.DownArrow)
                {
                    playerY = Math.Min(playerY + 1, 15);
                    playerDirectionMoveNumber = PlayerDirection.DOWN;
                }
                #endregion

                #region 플레이어 업데이트
                // 플레이어가 장애물을 만난 상황
                for (int i = 0; i < obticleX.Length; ++i)
                {
                    if (playerX == obticleX[i] && playerY == obticleY[i])
                    {
                        switch (playerDirectionMoveNumber)
                        {
                            case PlayerDirection.LEFT:
                                playerX = obticleX[i] + 1;
                                break;
                            case PlayerDirection.RIGHT:
                                playerX = obticleX[i] - 1;
                                break;
                            case PlayerDirection.UP:
                                playerY = obticleY[i] + 1;
                                break;
                            case PlayerDirection.DOWN:
                                playerY = obticleY[i] - 1;
                                break;
                        }
                    }
                }
                #endregion

                #region 박스 업데이트
                // 박스가 플레이어와 만난 상황
                for (int i = 0; i < BOX_COUNT; ++i)
                {
                    if (boxX[i] == playerX && boxY[i] == playerY)
                    {
                        switch (playerDirectionMoveNumber)
                        {
                            case PlayerDirection.LEFT:
                                boxX[i] = Math.Max(1, boxX[i] - 1);
                                playerX = boxX[i] + 1;
                                break;
                            case PlayerDirection.RIGHT:
                                boxX[i] = Math.Min(boxX[i] + 1, 20);
                                playerX = boxX[i] - 1;
                                break;
                            case PlayerDirection.UP:
                                boxY[i] = Math.Max(1, boxY[i] - 1);
                                playerY = boxY[i] + 1;
                                break;
                            case PlayerDirection.DOWN:
                                boxY[i] = Math.Min(boxY[i] + 1, 15);
                                playerY = boxY[i] - 1;
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine($"[Error] 플레이어 이동 방향 데이터가 오류입니다. : {playerDirectionMoveNumber}");
                                return;
                        }

                        break;
                    }
                }

                // 박스가 장애물을 만난 상황
                for (int i = 0; i < BOX_COUNT; ++i)
                {
                    for (int j = 0; j < OBTICLE_COUNT; ++j)
                    {
                        if (boxX[i] == obticleX[j] && boxY[i] == obticleY[j])
                        {
                            switch (playerDirectionMoveNumber)
                            {
                                case PlayerDirection.LEFT:
                                    boxX[i] = boxX[i] + 1;
                                    playerX = boxX[i] + 1;

                                    break;
                                case PlayerDirection.RIGHT:
                                    boxX[i] = boxX[i] - 1;
                                    playerX = boxX[i] - 1;

                                    break;
                                case PlayerDirection.UP:
                                    boxY[i] = boxY[i] + 1;
                                    playerY = boxY[i] + 1;

                                    break;
                                case PlayerDirection.DOWN:
                                    boxY[i] = boxY[i] - 1;
                                    playerY = boxY[i] - 1;

                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine($"[Error] 플레이어 이동 방향 데이터가 오류입니다. : {playerDirectionMoveNumber}");

                                    return;

                            }
                        }
                    }
                }

                // 박스와 박스가 만난 상황
                for (int i = 0; i < BOX_COUNT; ++i)
                {
                    for (int j = 0; j < BOX_COUNT; ++j)
                    {
                        if (i != j && boxX[i] == boxX[j] && boxY[i] == boxY[j])
                        {
                            switch (playerDirectionMoveNumber)
                            {
                                case PlayerDirection.LEFT:
                                    boxX[i] += 1;
                                    playerX += 1;

                                    break;
                                case PlayerDirection.RIGHT:
                                    boxX[i] -= 1;
                                    playerX -= 1;

                                    break;
                                case PlayerDirection.UP:
                                    boxY[i] += 1;
                                    playerY += 1;

                                    break;
                                case PlayerDirection.DOWN:
                                    boxY[i] -= 1;
                                    playerY -= 1;

                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine($"[Error] 플레이어 이동 방향 데이터가 오류입니다. : {playerDirectionMoveNumber}");

                                    return;
                            }

                            break;
                        }
                    }
                }

                // 박스가 골에 들어간 상황
                count = 0;
                for (int i = 0; i < BOX_COUNT; ++i)
                {
                    isBoxOnGoal[i] = false;

                    for (int j = 0; j < GOAL_COUNT; ++j)
                    {
                        if (goalX[j] == boxX[i] && goalY[j] == boxY[i])
                        {
                            ++count;
                            isBoxOnGoal[i] = true;
                            break;
                        }
                    }
                }

                if (count == GOAL_COUNT)
                {
                    Console.Clear();
                    Console.WriteLine("축하드립니다!");
                    return;
                }
                #endregion

                #endregion
            }
        }
    }
}
