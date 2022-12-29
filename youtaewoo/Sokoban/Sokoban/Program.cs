namespace Sokoban
{
    enum Directions
    {
        NONE,
        LEFT,
        RIGHT,
        UP,
        DOWN,
    }
    class Program
    {
        static void Main()
        {
            //초기 세팅
            Console.ResetColor(); // 컬러를 초기화한다.
            Console.CursorVisible = false; // 커서 숨기기
            Console.Title = "미래의 babaisyou"; // 타이틀을 설정한다.
            Console.BackgroundColor = ConsoleColor.DarkGreen; // 배경색을 설정한다.
            Console.ForegroundColor = ConsoleColor.White; //글꼴색을 설정한다.
            Console.Clear(); // 출력된 모든 내용을 지운다.

            // 플레이어 설정
            const string PLAYER_CHARACTER = "B";

            //플레이어의 초기 좌표
            const int INITIAL_PLAYERX = 0;
            const int INITIAL_PLAYERY = 0;

            //플레이어의 현재 좌표
            int playerX = INITIAL_PLAYERX;
            int playerY = INITIAL_PLAYERY;

            //플레이어의 움직임 방향
            Directions playerDirection = Directions.NONE;
            
            // 박스 설정
            const string BOX_CHARACTER = "O";
            const int INITIAL_BOXX = 5;
            const int INITIAL_BOXY = 10;

            // 박스의 현재 좌표
            int boxX = INITIAL_BOXX;
            int boxY = INITIAL_BOXY;

            // 플레이 공간 설정
            const int MAP_MIN_X = 0;

            const int MAP_MIN_Y = 0;

            const int MAP_MAX_X = 30;

            const int MAP_MAX_Y = 15;

            // 벽 생성
            const string WALL_CHARACTER = "W";
            const int INITIAL_WALL_X = 12;
            const int INITIAL_WALL_Y = 5;
         

            // 게임 루프 == 프레임(frame)
            while (true)
            {
                // 이전 프레임을 지운다.
                Console.Clear();
                //---------------------------------Render--------------------------------
                // 플레이어 출력하기

                Console.SetCursorPosition(playerX, playerY);
                Console.Write(PLAYER_CHARACTER);

                // 박스 출력하기
                Console.SetCursorPosition(boxX, boxY);
                Console.Write(BOX_CHARACTER);

                // 벽 출력하기
                Console.SetCursorPosition(INITIAL_WALL_X, INITIAL_WALL_Y);
                Console.Write(WALL_CHARACTER);

                //------------------------------ProcessInput-----------------------------
                ConsoleKey key = Console.ReadKey().Key;

                //---------------------------------Update--------------------------------
                //오른쪽 화살표 키를 눌렀을 때를 먼저 한정해줄 것이다.
                if (key == ConsoleKey.LeftArrow && playerX > MAP_MIN_X) //왼쪽 이동
                {
                    playerX = Math.Max(MAP_MIN_X, playerX - 1);
                    playerDirection = Directions.LEFT;
                }
                if (key == ConsoleKey.RightArrow && playerX < MAP_MAX_X) //오른쪽 이동  
                {
                    playerX = Math.Min(playerX + 1, MAP_MAX_X);
                    playerDirection = Directions.RIGHT;
                }
                if (key == ConsoleKey.UpArrow && playerY > MAP_MIN_Y) //위 이동
                {
                    playerY = Math.Max(MAP_MIN_Y, playerY - 1);
                    playerDirection = Directions.UP;
                }
                if (key == ConsoleKey.DownArrow && playerY < MAP_MAX_Y) //아래 이동
                {
                    playerY = Math.Min(playerY + 1, MAP_MAX_Y);
                    playerDirection = Directions.DOWN;
                }

                // 1. 플레이어가 벽에 부딪혀야 함
                if (playerX == INITIAL_WALL_X && playerY == INITIAL_WALL_Y)
                {
                    switch (playerDirection)
                    {
                        case Directions.LEFT: // 벽 왼쪽
                            if (INITIAL_WALL_X == playerX)
                            {
                                playerX = INITIAL_WALL_X + 1;
                            }
                            break;
                        case Directions.RIGHT: // 벽 오른쪽
                            if (INITIAL_WALL_X == playerX)
                            {
                                playerX = INITIAL_WALL_X - 1;
                            }                            
                            break;
                        case Directions.UP: // 박스 위쪽
                            if (INITIAL_WALL_Y == playerY)
                            {
                                playerY = INITIAL_WALL_Y + 1;
                            }                            
                            break;
                        case Directions.DOWN: // 박스 아래쪽
                            if (INITIAL_WALL_Y == playerY)
                            {
                                playerY = INITIAL_WALL_Y - 1;
                            }
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다.");
                            return;
                    }
                }
                // 2. 박스도 막아야 함

               
                // 박스 업데이트
                if (playerX == boxX && playerY == boxY) // 플레이어가 이동하고 나니 박스가 있는 것
                {
                    //박스를 움직여주면 된다.
                    switch (playerDirection)
                    {
                        case Directions.LEFT: // 박스 왼쪽
                            if (boxX == MAP_MIN_X)
                            {
                                playerX = MAP_MIN_X + 1;
                            }
                            else
                            {
                                boxX = boxX - 1;
                            }
                            break;
                        case Directions.RIGHT: // 박스 오른쪽
                            if (boxX == MAP_MAX_X)
                            {
                                playerX = MAP_MAX_X - 1;
                            }
                            else
                            {
                                boxX = boxX + 1;
                            }
                            break;
                        case Directions.UP: // 박스 위쪽
                            if (boxY == MAP_MIN_Y)
                            {
                                playerY = MAP_MIN_Y + 1;
                            }
                            else
                            {
                                boxY = boxY - 1;
                            }
                            break;
                        case Directions.DOWN: // 박스 아래쪽
                            if (boxY == MAP_MAX_Y)
                            {
                                playerY = MAP_MAX_Y - 1;
                            }
                            else
                            {
                                boxY = boxY + 1;
                            }
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다.");
                            return;
                    }

                    
                    }

                if (boxX == INITIAL_WALL_X && boxY == INITIAL_WALL_Y) // 얘는 박스가 벽
                {
                    switch (playerDirection)
                    {
                        case Directions.LEFT: // 벽 왼쪽
                            if (INITIAL_WALL_X == boxX)
                            {
                                boxX = INITIAL_WALL_X + 1;
                                playerX = boxX + 1;
                            }
                            break;
                        case Directions.RIGHT: // 벽 오른쪽
                            if (INITIAL_WALL_X == boxX)
                            {
                                boxX = INITIAL_WALL_X - 1;
                                playerX = boxX - 1;
                            }
                            break;
                        case Directions.UP: // 박스 위쪽
                            if (INITIAL_WALL_Y == boxY)
                            {
                                boxY = INITIAL_WALL_Y + 1;
                                playerY = boxY + 1;
                            }
                            break;
                        case Directions.DOWN: // 박스 아래쪽
                            if (INITIAL_WALL_Y == boxY)
                            {
                                boxY = INITIAL_WALL_Y - 1;
                                playerY = boxY - 1;
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
    }
}



