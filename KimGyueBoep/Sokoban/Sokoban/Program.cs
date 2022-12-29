namespace Sokoban
{
    // 열거형
    enum PLAYER_Direction
    {
        NONE,
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
            Console.ResetColor();                                 // 컬러를 초기화한다.
            Console.CursorVisible = false;                        // 커서를 숨긴다.
            Console.Title = "소코반 프로젝트";                     // 타이틀을 설정한다.
            Console.BackgroundColor = ConsoleColor.Green;         // 배경색을 설정한다.
            Console.ForegroundColor = ConsoleColor.Red;           // 글꼴색을 설정한다.
            Console.Clear();                                     // 출력된 모든 내용을 지운다.

            // 기호 상수 정의
            // 맵의 가로 범위, 세로 범위
            const int MAP_MIN_X = 0;
            const int MAP_MIN_Y = 0;
            const int MAP_MAX_X = 15;
            const int MAP_MAX_Y = 10;

            // 플레이어의 이동 방향
            // int playerDirection = 0; // 0 : NONE, 1 : Left, 2 : Right, 3 : Up, 4 : Down
            // const int DIRECTION_LEFT = 1;
            // const int DIRECTION_RIGHT = 2;
            // const int DIRECTION_UP = 3;
            // const int DIRECTION_DOWN = 4;

            // 플레이어의 초기 좌표
            const int INITIAL_PLAYER_X = 0;
            const int INITIAL_PLAYER_Y = 0;
            // 플레이어의 기호(string literal)
            const string PLAYER_STRING = "P";

            // 박스의 초기 좌표
            const int INITIAL_BOX_X = 2;
            const int INITIAL_BOX_Y = 1;
            // 박스의 기호(string literal)
            const string BOX_STRING = "B";

            // 플레이어 좌표 설정
            PLAYER_Direction playerDirection = PLAYER_Direction.NONE;
            int playerX = INITIAL_PLAYER_X;
            int playerY = INITIAL_PLAYER_Y;

            // 박스 좌표 설정
            int box_X = INITIAL_BOX_X;
            int box_Y = INITIAL_BOX_Y;

            // 벽의 좌표 
            const int INITAL_WALL_X = 7;
            const int INITAL_WALL_Y = 8;
            // 벽의 기호
            const string WALL_STRING = "W";

            // 벽좌표 설정
            int wallX = INITAL_WALL_X;
            int wallY = INITAL_WALL_Y;

            //골의 좌표
            const int GOAL_X = 15;
            const int GOAL_Y = 10;
            // 골의 기호
            const string GOAL_STRING = "G";

            int goalX = GOAL_X;
            int goalY = GOAL_Y;


            // 가로 15, 세로 10
            // 게임 루프 == 프레임(Frame)
            while (true)
            {
                // 이전 프레임을 지운다.
                Console.Clear();

                // ---------------------------------- Render ----------------------------------
                // 플레이어 출력하기
                Console.SetCursorPosition(playerX, playerY);
                Console.Write(PLAYER_STRING);

                // 박스 출력하기
                Console.SetCursorPosition(box_X, box_Y);
                Console.Write(BOX_STRING);

                // 벽 출력하기
                Console.SetCursorPosition(INITAL_WALL_X, INITAL_WALL_Y);
                Console.Write(WALL_STRING);

                // 골 출력하기
                Console.SetCursorPosition(GOAL_X, GOAL_Y);
                Console.Write(GOAL_STRING);

                // ---------------------------------- ProcessInput ----------------------------------
                ConsoleKey key = Console.ReadKey().Key;

                // ---------------------------------- Update ----------------------------------
                // 오른쪽 화살표키를 눌렀을 때
                if (key == ConsoleKey.RightArrow)
                {
                    // 오른쪽으로 이동
                    playerX = Math.Min(playerX + 1, MAP_MAX_X);
                    playerDirection = PLAYER_Direction.RIGHT;
                    //playerDirection = DIRECTION_RIGHT;
                }
                if (key == ConsoleKey.LeftArrow)
                {
                    // 왼쪽
                    playerX = Math.Max(MAP_MIN_X, playerX - 1);
                    playerDirection = PLAYER_Direction.LEFT;
                }
                if (key == ConsoleKey.DownArrow)
                {
                    // 아래쪽
                    playerY = Math.Min(playerY + 1, MAP_MAX_Y);
                    playerDirection = PLAYER_Direction.DOWN;
                }
                if (key == ConsoleKey.UpArrow)
                {
                    // 위쪽
                    playerY = Math.Max(MAP_MIN_Y, playerY - 1);
                    playerDirection = PLAYER_Direction.UP;
                }

                // 박스 업데이트
                // 플레이어가 이동한 후
                //if (playerX == box_X && playerY == box_Y && key == ConsoleKey.RightArrow)
                //{
                //    box_X++;
                //    if (box_X > 15)
                //    {
                //        box_X = 15;
                //        playerX -= 1;
                //    }
                //}
                //if (playerX == box_X && playerY == box_Y && key == ConsoleKey.LeftArrow)
                //{
                //    box_X--;
                //    if (box_X < 0)
                //    {
                //        box_X = 0;
                //        playerX += 1;
                //    }
                //}
                //if (playerY == box_Y && playerX == box_X && key == ConsoleKey.DownArrow)
                //{
                //    box_Y++;
                //    if (box_Y > 10)
                //    {
                //        box_Y = 10;
                //        playerY -= 1;
                //    }
                //}
                //if (playerY == box_Y && playerX == box_X && key == ConsoleKey.UpArrow)
                //{
                //    box_Y--;
                //    if (box_Y < 0)
                //    {
                //        box_Y = 0;
                //        playerY += 1;
                //    }
                //}

                // 1. 플레이어가 벽에 부딪혀야 함.
                if (playerX == wallX && playerY == wallY)
                {
                    switch (playerDirection)
                    {
                        case PLAYER_Direction.LEFT:
                            playerX = playerX + 1;
                            break;
                        case PLAYER_Direction.RIGHT:
                            playerX = playerX - 1;
                            break;
                        case PLAYER_Direction.UP:
                            playerY = playerY + 1;
                            break;
                        case PLAYER_Direction.DOWN:
                            playerY = playerY - 1;
                            break;
                    }
                }

                // 박스 업데이트
                // 플레이어가 이동한 후
                if (playerX == box_X && playerY == box_Y)
                {
                    // 박스를 움직여주면 됨
                    switch (playerDirection)
                    {
                        case PLAYER_Direction.LEFT: // 왼쪽
                            if (box_X == MAP_MIN_X)
                            {
                                playerX = 1;
                            }
                            else
                            {
                                box_X = box_X - 1;
                            }
                            break;
                        case PLAYER_Direction.RIGHT: // 오른쪽
                            if (box_X == MAP_MAX_X)
                            {
                                playerX = MAP_MAX_X - 1;
                            }
                            else
                            {
                                box_X = box_X + 1;
                            }
                            break;
                        case PLAYER_Direction.UP: // 위
                            if (box_Y == MAP_MIN_Y)
                            {
                                playerY = 1;
                            }
                            else
                            {
                                box_Y = box_Y - 1;
                            }
                            break;
                        case PLAYER_Direction.DOWN: // 아래
                            if (box_Y == MAP_MAX_Y)
                            {
                                playerY = MAP_MAX_Y - 1;
                            }
                            else
                            {
                                box_Y = box_Y + 1;
                            }
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다. {playerDirection}");

                            return; // 프로그램 종료


                    }
                }
                // 2. 벽에 박스
                if (box_X == wallX && box_Y == wallY)
                {
                    switch (playerDirection)
                    {
                        case PLAYER_Direction.LEFT:
                            box_X = box_X + 1;
                            playerX = playerX + 1;
                            break;
                        case PLAYER_Direction.RIGHT:
                            box_X = box_X - 1;
                            playerX = playerX - 1;
                            break;
                        case PLAYER_Direction.UP:
                            box_Y= box_Y + 1;
                            playerY = playerY + 1;
                            break;
                        case PLAYER_Direction.DOWN:
                            box_Y= box_Y - 1;
                            playerY = playerY - 1;
                            break;

                    }
                }
                if(box_X == goalX && box_Y == goalY)
                {
                    Console.Clear();
                    Console.WriteLine("Game Clear!");
                    return;
                }

            }
        }
    }
}