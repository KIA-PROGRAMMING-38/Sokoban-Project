namespace Sokoban
{
    // 열거형
    enum Direction // 플레이어 이동 방향
    {
        DIRECTION_LEFT = 1,
        DIRECTION_RIGHT = 2,
        DIRECTION_UP = 3,
        DIRECTION_DOWN = 4,
    }

    class Program
    {
        static void Main()
        {
            // 초기 세팅
            Console.ResetColor();                              // 컬러를 초기화한다
            Console.CursorVisible = false;                     // 커서를 켜고 끄는 것, 불리언 타입이다
            Console.Title = "홍성재의 파이어펀치";               // 타이틀 명을 바꾸어주는 이름
            Console.BackgroundColor = ConsoleColor.DarkGreen;  // 배경색을 설정한다
            Console.ForegroundColor = ConsoleColor.Yellow;     // 글꼴색을 설정한다
            Console.Clear();                                   // 출력된 모든 내용을 지운다

            // 맵의 가장자리 구현
            const int MAPFRONTIER_LEFT = 0;
            const int MAPFRONTIER_RIGHT = 30;
            const int MAPFRONTIER_UP = 0;
            const int MAPFRONTIER_DOWN = 20;

            // 가장자리의 기호
            const string wall_X = "|";
            const string wall_Y = "-";

            // 장애물 좌표, 이름
            const int MAP_WALL_X = 10;
            const int MAP_WALL_Y = 7;
            const string MAP_WALL_INITIAL = "W";

            // 플레이어의 기호(string literal)
            const string PLAYERLITERAL = "P";

            // 박스의 기호(string literal)
            const string BOXLITERAL = "O";

            // 골 지점 좌표, 이름
            const int GOAL_X = 18;
            const int GOAL_Y = 13;
            const string GOAL_INITIAL = "G";

            // 플레이어의 좌표 이동
            int player_X = 0;
            int player_Y = 0;
            int playerDirection = 0; // 0: None, 1: Left, 2: Right, 3: Up, 4: Down

            // 박스의 좌표 이동
            int box_X = 5;
            int box_Y = 6;


            // 게임 루프 == 프레임(Frame)
            while (true)
            {
                Console.Clear();

                //-------------------------------------- Render -------------------------------------------------
                // 플레이어 출력하기
                Console.SetCursorPosition(player_X, player_Y);
                Console.Write(PLAYERLITERAL);

                // 박스 출력하기
                Console.SetCursorPosition(box_X, box_Y);
                Console.Write(BOXLITERAL);

                // 장애물 출력하기
                Console.SetCursorPosition(MAP_WALL_X, MAP_WALL_Y);
                Console.Write(MAP_WALL_INITIAL);

                // 골 출력
                Console.SetCursorPosition(GOAL_X, GOAL_Y);
                Console.Write(GOAL_INITIAL);

                // X 가장자리 출력
                for (int wall_Xs = 0; wall_Xs < MAPFRONTIER_DOWN + 1; ++wall_Xs)
                {
                    Console.SetCursorPosition(MAPFRONTIER_RIGHT + 1, wall_Xs);
                    Console.Write(wall_X);
                }

                // Y 가장자리 출력
                for (int wall_Ys = 0; wall_Ys < MAPFRONTIER_RIGHT + 1; ++wall_Ys)
                {
                    Console.SetCursorPosition(wall_Ys, MAPFRONTIER_DOWN + 1);
                    Console.Write(wall_Y);
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
                    playerDirection = (int)Direction.DIRECTION_LEFT;
                }

                if (key == ConsoleKey.RightArrow && player_X < MAPFRONTIER_RIGHT)
                {
                    player_X = Math.Min(player_X + 1, MAPFRONTIER_RIGHT);
                    playerDirection = (int)Direction.DIRECTION_RIGHT;
                }

                if (key == ConsoleKey.UpArrow && player_Y > MAPFRONTIER_UP)
                {
                    player_Y = Math.Max(MAPFRONTIER_UP, player_Y - 1);
                    playerDirection = (int)Direction.DIRECTION_UP;
                }

                if (key == ConsoleKey.DownArrow && player_Y < MAPFRONTIER_DOWN)
                {
                    player_Y = Math.Min(player_Y + 1, MAPFRONTIER_DOWN);
                    playerDirection = (int)Direction.DIRECTION_DOWN;
                }


                // 박스 업데이트
                // 플레이어가 이동한 후
                if (player_X == box_X && player_Y == box_Y)
                {
                    switch (playerDirection)
                    {
                        // 박스를 움직여주면 됨
                        case (int)Direction.DIRECTION_LEFT: // 플레이어가 왼쪽으로 이동 중
                            if (box_X == MAPFRONTIER_LEFT)
                            {
                                player_X = MAPFRONTIER_LEFT + 1;
                            }
                            else if (box_X == MAP_WALL_X)
                            {
                                box_X = MAP_WALL_X - 1;
                            }
                            else
                            {
                                box_X = box_X - 1;
                            }
                            break;
                        case (int)Direction.DIRECTION_RIGHT: // 플레이어가 오른쪽으로 이동 중
                            if (box_X == MAPFRONTIER_RIGHT)
                            {
                                player_X = MAPFRONTIER_RIGHT - 1;
                            }
                            else
                            {
                                box_X = box_X + 1;
                            }
                            break;
                        case (int)Direction.DIRECTION_UP: // 플레이어가 위쪽으로 이동 중
                            if (box_Y == MAPFRONTIER_UP)
                            {
                                player_Y = MAPFRONTIER_UP + 1;
                            }
                            else
                            {
                                box_Y = box_Y - 1;
                            }
                            break;
                        case (int)Direction.DIRECTION_DOWN: // 플레이어가 아래쪽으로 이동 중
                            if (box_Y == MAPFRONTIER_DOWN)
                            {
                                player_Y = MAPFRONTIER_DOWN - 1;
                            }
                            else
                            {
                                box_Y = box_Y + 1;
                            }
                            break;
                        default: // 예외 처리
                            Console.Clear();
                            Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다 {playerDirection}");

                            break;
                    }
 
                }

                // 1. 플레이어가 벽에 부딪혀야 함
                if (player_X == MAP_WALL_X && player_Y == MAP_WALL_Y)
                {
                    switch (playerDirection)
                    {
                        case (int)Direction.DIRECTION_LEFT:
                            player_X = player_X + 1;
                            break;
                        case (int)Direction.DIRECTION_RIGHT:
                            player_X = player_X - 1;
                            break;
                        case (int)Direction.DIRECTION_UP:
                            player_Y = player_Y + 1;
                            break;
                        case (int)Direction.DIRECTION_DOWN:
                            player_Y = player_Y - 1;
                            break;
                    }
                }
                // 2. 박스가 벽에 부딪혀야 함
                if (box_X == MAP_WALL_X && box_Y == MAP_WALL_Y)
                {
                    switch (playerDirection)
                    {
                        case (int)Direction.DIRECTION_LEFT:
                            player_X = MAP_WALL_X + 2;
                            box_X = MAP_WALL_X + 1;
                            break;
                        case (int)Direction.DIRECTION_RIGHT:
                            player_X = MAP_WALL_X - 2;
                            box_X = MAP_WALL_X - 1;
                            break;
                        case (int)Direction.DIRECTION_UP:
                            player_Y = MAP_WALL_Y + 2;
                            box_Y = MAP_WALL_Y + 1;
                            break;
                        case (int)Direction.DIRECTION_DOWN:
                            player_Y = MAP_WALL_Y - 2;
                            box_Y = MAP_WALL_Y - 1;
                            break;
                    }
                }

                if (box_X == GOAL_X && box_Y == GOAL_Y)
                {
                    break;
                }

            }

            Console.Clear();
            Console.WriteLine("축하드립니다! 당신은 성공했습니다");
        }
    }
}
