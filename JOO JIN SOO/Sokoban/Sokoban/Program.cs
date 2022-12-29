namespace Sokoban
{
    enum Direction // 플레이어 이동 방향
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }

    class Program
    {
        static void Main()
        {
            // 초기 세팅
            Console.ResetColor();                              // 컬러를 초기화한다
            Console.CursorVisible = false;                     // 커서를 켜고 끄는 것, 불리언 타입이다
            Console.Title = "마이노의 라비린스 월";               // 타이틀 명을 바꾸어주는 이름
            Console.BackgroundColor = ConsoleColor.DarkBlue;  // 배경색을 설정한다
            Console.ForegroundColor = ConsoleColor.Yellow;     // 글꼴색을 설정한다
            Console.Clear();                                   // 출력된 모든 내용을 지운다

            // 맵의 가장자리 구현
            const int MAPFRONTIER_LEFT = 0;
            const int MAPFRONTIER_RIGHT = 30;
            const int MAPFRONTIER_UP = 0;
            const int MAPFRONTIER_DOWN = 20;

            // 인게임 심볼 모음
            const string WALL_X = "|";
            const string WALL_Y = "-";
            const string BOX_LITERAL = "O";
            const string PLAYER_LITERAL = "P";
            const string OBSTACLE_LITERAL = "W";
            const string GOAL_LITERAL = "G";

            // 플레이어 초기 좌표 상수화
            const int PLAYER1_X = 15;
            const int PLAYER2_Y = 10;

            // 골 지점 초기 좌표 상수화
            const int GOAL1_INITIAL_X = 15;
            const int GOAL1_INITIAL_Y = 13;
            const int GOAL2_INITIAL_X = 4;
            const int GOAL2_INITIAL_Y = 3;
            const int GOAL3_INITIAL_X = 28;
            const int GOAL3_INITIAL_Y = 11;

            // 장애물 초기 좌표 상수화
            const int OBSTACLE1_X = 10;
            const int OBSTACLE1_Y = 8;
            const int OBSTACLE2_X = 9;
            const int OBSTACLE2_Y = 9;
            const int OBSTACLE3_X = 8;
            const int OBSTACLE3_Y = 10;

            // 박스 초기 좌표 상수화
            const int BOX1_X = 15;
            const int BOX1_Y = 3;
            const int BOX2_X = 25;
            const int BOX2_Y = 17;
            const int BOX3_X = 11;
            const int BOX3_Y = 3;

            // 장애물 좌표
            int[] obstacle_X = { OBSTACLE1_X, OBSTACLE2_X, OBSTACLE3_X };
            int[] obstacle_Y = { OBSTACLE1_Y, OBSTACLE2_Y, OBSTACLE3_Y };

            // 골 지점 좌표, 이름
            int[] goalCoordinate_X = { GOAL1_INITIAL_X, GOAL2_INITIAL_X, GOAL3_INITIAL_X };
            int[] goalCoordinate_Y = { GOAL1_INITIAL_Y, GOAL2_INITIAL_Y, GOAL3_INITIAL_Y };

            // 플레이어의 좌표 이동
            int player_X = PLAYER1_X;
            int player_Y = PLAYER2_Y;
            Direction playerDirection = default; // 0: None, 1: Left, 2: Right, 3: Up, 4: Down

            // 박스의 좌표 이동
            int[] box_X = { BOX1_X, BOX2_X, BOX3_X };
            int[] box_Y = { BOX1_Y, BOX2_Y, BOX3_Y };

            // 게임 루프 == 프레임(Frame)
            while (true)
            {
                Console.Clear();

                //-------------------------------------- Render -------------------------------------------------
                // 플레이어 출력하기
                Console.SetCursorPosition(player_X, player_Y);
                Console.Write(PLAYER_LITERAL);

                // 박스 출력하기
                for (int boxNumbers = 0; boxNumbers < box_X.Length; ++boxNumbers)
                {
                    Console.SetCursorPosition(box_X[boxNumbers], box_Y[boxNumbers]);
                    Console.Write(BOX_LITERAL);
                }

                // 장애물 출력하기
                for (int obstacleNumbers = 0; obstacleNumbers < obstacle_X.Length; ++obstacleNumbers)
                {
                    Console.SetCursorPosition(obstacle_X[obstacleNumbers], obstacle_Y[obstacleNumbers]);
                    Console.Write(OBSTACLE_LITERAL);
                }

                // 골 출력
                for (int goalNumbers = 0; goalNumbers < goalCoordinate_X.Length; ++goalNumbers)
                {
                    Console.SetCursorPosition(goalCoordinate_X[goalNumbers], goalCoordinate_Y[goalNumbers]);
                    Console.Write(GOAL_LITERAL);
                }

                // X 가장자리 출력
                for (int wall_Xs = 0; wall_Xs < MAPFRONTIER_DOWN + 1; ++wall_Xs)
                {
                    Console.SetCursorPosition(MAPFRONTIER_RIGHT + 1, wall_Xs);
                    Console.Write(WALL_X);
                }

                // Y 가장자리 출력
                for (int wall_Ys = 0; wall_Ys < MAPFRONTIER_RIGHT + 1; ++wall_Ys)
                {
                    Console.SetCursorPosition(wall_Ys, MAPFRONTIER_DOWN + 1);
                    Console.Write(WALL_Y);
                }

                //-------------------------------------- ProcessInput -------------------------------------------
                ConsoleKeyInfo inputKey = Console.ReadKey();
                ConsoleKey key = inputKey.Key;
                // 이것은 ConsoleKey inputKey = Console.ReadKey().Key; 와 같다

                //-------------------------------------- Update -------------------------------------------------
                // 플레이어 업데이트
                if (key == ConsoleKey.LeftArrow && player_X > MAPFRONTIER_LEFT)
                {
                    player_X = Math.Max(MAPFRONTIER_LEFT, player_X - 1);
                    playerDirection = Direction.LEFT;
                }

                if (key == ConsoleKey.RightArrow && player_X < MAPFRONTIER_RIGHT)
                {
                    player_X = Math.Min(player_X + 1, MAPFRONTIER_RIGHT);
                    playerDirection = Direction.RIGHT;
                }

                if (key == ConsoleKey.UpArrow && player_Y > MAPFRONTIER_UP)
                {
                    player_Y = Math.Max(MAPFRONTIER_UP, player_Y - 1);
                    playerDirection = Direction.UP;
                }

                if (key == ConsoleKey.DownArrow && player_Y < MAPFRONTIER_DOWN)
                {
                    player_Y = Math.Min(player_Y + 1, MAPFRONTIER_DOWN);
                    playerDirection = Direction.DOWN;
                }

                // 박스 업데이트
                for (int boxCount = 0; boxCount < box_X.Length; ++boxCount)
                {

                    if (player_X == box_X[boxCount] && player_Y == box_Y[boxCount])
                    {
                        switch (playerDirection)
                        {
                            // 박스를 움직여주면 됨
                            case Direction.LEFT: // 플레이어가 왼쪽으로 이동 중
                                if (box_X[boxCount] == MAPFRONTIER_LEFT)
                                {
                                    player_X = MAPFRONTIER_LEFT + 1;
                                }
                                else
                                {
                                    box_X[boxCount] = box_X[boxCount] - 1;
                                }
                                break;
                            case Direction.RIGHT: // 플레이어가 오른쪽으로 이동 중
                                if (box_X[boxCount] == MAPFRONTIER_RIGHT)
                                {
                                    player_X = MAPFRONTIER_RIGHT - 1;
                                }
                                else
                                {
                                    box_X[boxCount] = box_X[boxCount] + 1;
                                }
                                break;
                            case Direction.UP: // 플레이어가 위쪽으로 이동 중
                                if (box_Y[boxCount] == MAPFRONTIER_UP)
                                {
                                    player_Y = MAPFRONTIER_UP + 1;
                                }
                                else
                                {
                                    box_Y[boxCount] = box_Y[boxCount] - 1;
                                }
                                break;
                            case Direction.DOWN: // 플레이어가 아래쪽으로 이동 중
                                if (box_Y[boxCount] == MAPFRONTIER_DOWN)
                                {
                                    player_Y = MAPFRONTIER_DOWN - 1;
                                }
                                else
                                {
                                    box_Y[boxCount] = box_Y[boxCount] + 1;
                                }
                                break;
                            default: // 예외 처리
                                Console.Clear();
                                Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다 {playerDirection}");
                                break;
                        }
                    }

                }

                // 박스끼리의 충돌시
                for (int count1 = 0; count1 < box_X.Length; ++count1)
                {
                    for (int count2 = 0; count2 < box_X.Length; ++count2)
                    {
                        if (count1 != count2 && box_X[count1] == box_X[count2] && box_Y[count1] == box_Y[count2])
                        {
                            switch (playerDirection)
                            {
                                case Direction.LEFT:
                                    box_X[count1] += 1;
                                    player_X += 1;
                                    break;
                                case Direction.RIGHT:
                                    box_X[count1] -= 1;
                                    player_X -= 1;
                                    break;
                                case Direction.UP:
                                    box_Y[count1] += 1;
                                    player_Y += 1;
                                    break;
                                case Direction.DOWN:
                                    box_Y[count1] -= 1;
                                    player_Y -= 1;
                                    break;
                            }
                        }
                    }
                }

                // 플레이어가 장애물에 부딪힘
                for (int player_against_obstacle = 0; player_against_obstacle < 3; ++player_against_obstacle)
                {
                    if (player_X == obstacle_X[player_against_obstacle] && player_Y == obstacle_Y[player_against_obstacle])
                    {
                        switch (playerDirection)
                        {
                            case Direction.LEFT:
                                player_X = player_X + 1;
                                break;
                            case Direction.RIGHT:
                                player_X = player_X - 1;
                                break;
                            case Direction.UP:
                                player_Y = player_Y + 1;
                                break;
                            case Direction.DOWN:
                                player_Y = player_Y - 1;
                                break;
                        }
                    }
                }

                // 박스가 장애물에 부딪함
                for (int count1 = 0; count1 < box_X.Length; ++count1)
                {
                    for (int count2 = 0; count2 < box_X.Length; ++count2)
                    {
                        if (box_X[count1] == obstacle_X[count2] && box_Y[count1] == obstacle_Y[count2])
                        {
                            switch (playerDirection)
                            {
                                case Direction.LEFT:
                                    player_X += 1;
                                    box_X[count1] += 1;
                                    break;
                                case Direction.RIGHT:
                                    player_X -= 1;
                                    box_X[count1] -= 1;
                                    break;
                                case Direction.UP:
                                    player_Y += 1;
                                    box_Y[count1] += 1;
                                    break;
                                case Direction.DOWN:
                                    player_Y -= 1;
                                    box_Y[count1] -= 1;
                                    break;
                            }
                        }
                    }
                }

                // 박스가 골에 들어갔을 때
                int goalRepo = 0;
                for (int count1 = 0; count1 < goalCoordinate_X.Length; ++count1)
                {
                    for (int count2 = 0; count2 < goalCoordinate_X.Length; ++count2)
                    {
                        if (box_X[count1] == goalCoordinate_X[count2] && box_Y[count1] == goalCoordinate_Y[count2])
                        {
                            ++goalRepo;

                            if (goalRepo == goalCoordinate_X.Length)
                            {
                                goto clearGame;
                            }
                        }
                    }
                }
                

            }

            clearGame:

            Console.Clear();
            Console.WriteLine("축하드립니다! 당신은 성공했습니다");
        }
    }
}
