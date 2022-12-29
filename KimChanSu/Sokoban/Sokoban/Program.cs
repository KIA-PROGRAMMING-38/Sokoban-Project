namespace Sokoban
{
    // 열거형
   enum Direction
    {
        Left = 1,
        Right = 2,
        Up = 3,
        Down = 4
    }

    class Program
    {
        static void Main()
        {
            Console.ResetColor();                           // 컬러를 초기화한다.
            Console.CursorVisible = false;                  // 커서를 숨긴다.
            Console.Title = "Sokoban";                      // 타이틀을 설정한다.
            Console.BackgroundColor = ConsoleColor.Blue;    // 배경색을 설정한다.
            Console.ForegroundColor = ConsoleColor.Gray;    // 글꼴색을 설정한다.
            Console.Clear();                                // 출력된 모든 내용을 지운다.

            // 플레이어의 초기 좌표
            const int PLAYER_START_X = 0;
            const int PLAYER_START_Y = 0;

            // 플레이어 좌표 설정
            int playerX = PLAYER_START_X;
            int playerY = PLAYER_START_Y;
            Direction playerDirection = 0; // 0 : None, 1 : Left, 2 : Right, 3 : Up, 4 : Down 내가 정한 규칙

            // 박스의 초기 좌표
            const int BOX_START_X = 5;
            const int BOX_START_Y = 5;

            // 박스 좌표 설정
            int boxX = BOX_START_X;
            int boxY = BOX_START_Y;

            // 벽 초기 좌표 설정
            const int WALL_OF_X = 7;
            const int WALL_OF_Y = 8;
            const string WALL = "W";

            // 플레이어의 이동 방향
            Direction direction = Direction.Left;

            // 플레이어 기호(string literal)
            const string PLAYER_STRING = "P";

            // 박스의 기호(string literal)
            const string BOX_STRING = "B";

            // 벽의 기호
            const string WALL_STRING = "W";

            // 맵의 가로 범위, 세로 범위
            const int MAP_MIN_X = 0;
            const int MAP_MIN_Y = 0;
            const int MAP_MAX_X = 15;
            const int MAP_MAX_Y = 10;
            // 가로 15 세로 10

            // 게임 루프 == 프레임(Frame)


            while (true)
            {
                // 이전 프레임을 지운다.
                Console.Clear();

                // -------------------------- Render -------------------------------------
                // 플레이어 출력하기
                Console.SetCursorPosition(playerX, playerY);
                Console.Write(PLAYER_STRING);

                // 박스 출력하기
                Console.SetCursorPosition(boxX, boxY);
                Console.Write(BOX_STRING);

                // 벽 출력하기
                Console.SetCursorPosition(WALL_OF_X,WALL_OF_Y);
                Console.Write(WALL);

                // -------------------------- ProcessInput -------------------------------
                ConsoleKey key = Console.ReadKey().Key;

                // -------------------------- Update -------------------------------------


                if (key == ConsoleKey.LeftArrow)
                {
                    // 왼쪽 방향키 눌렀을 때
                    // 왼쪽으로 이동
                    playerX = Math.Max(PLAYER_START_X, playerX - 1);
                    playerDirection = Direction.Left;
                }

                if (key == ConsoleKey.RightArrow)
                {
                    // 오른쪽 화살표키를 눌렀을 때
                    // 오른쪽으로 이동
                    playerX = Math.Min(playerX + 1, MAP_MAX_X);
                    playerDirection = Direction.Right;
                }

                if (key == ConsoleKey.UpArrow)
                {
                    // 위쪽 방향키 눌렀을 때
                    // 위로 이동
                    playerY = Math.Max(MAP_MIN_Y, playerY - 1);
                    playerDirection = Direction.Up;
                }

                if (key == ConsoleKey.DownArrow)
                {
                    // 아래쪽 방향키 눌렀을 때
                    // 아래로 이동
                    playerY = Math.Min(playerY + 1, MAP_MAX_Y);
                    playerDirection = Direction.Down;
                }

                // ----------------------------------------

                //박스 업데이트
                // 플레이어가 이동한 후
                if (playerX == boxX && playerY == boxY)
                {
                    switch (playerDirection)
                    {
                        case (Direction.Left): // 플레이어가 왼쪽으로 이동중
                            if (boxX == MAP_MIN_X) //박스가 왼쪽 끝에 있다면
                            {
                                playerX = MAP_MIN_X + 1;
                            }
                            else
                            {
                                boxX = Math.Max(MAP_MIN_X, boxX - 1);
                            }

                            break;
                        case (Direction.Right): // 플레이어가 오른쪽으로 이동중
                            if (boxX == MAP_MAX_X)
                            {
                                playerX = MAP_MAX_X - 1;
                            }
                            else
                            {
                                boxX = Math.Min(boxX + 1, MAP_MAX_X);
                            }

                            break;
                        case (Direction.Up): // 플레이어가 위쪽으로 이동중
                            if (boxY == MAP_MIN_Y)
                            {
                                playerY = MAP_MIN_Y + 1;
                            }
                            else
                            {
                                boxY = Math.Max(MAP_MIN_Y , boxY - 1);
                            }

                            break;
                        case (Direction.Down): // 플레이어가 아래쪽으로 이동중
                            if (boxY == MAP_MAX_Y)
                            {
                                playerY = MAP_MAX_Y - 1;
                            }
                            else
                            {
                                boxY = Math.Min(boxY + 1, MAP_MAX_Y);
                            }

                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다. {playerDirection}");

                            return; // 프로그램 종료
                    }
                }
                // 사람이 벽에 부딪히면 멈추게 하기
                if (playerX == WALL_OF_X && playerY == WALL_OF_Y)
                {
                    switch (playerDirection)
                    {
                        case Direction.Left:
                            {
                                playerX = WALL_OF_X + 1;
                            }
                            break;
                        case Direction.Right:
                            {
                                playerX = WALL_OF_X - 1;
                            }    
                            break;
                        case Direction.Up:
                            {
                                playerY = WALL_OF_Y + 1;
                            }
                            break;
                        case Direction.Down:
                            {
                                playerY = WALL_OF_Y - 1;
                            }
                            break;
                    }
                    
                }
                // 박스가 벽에 부딪히면 멈추게 하기
                if (boxX == WALL_OF_X && boxY == WALL_OF_Y)
                {
                    switch (playerDirection)
                    {
                        case Direction.Left:
                            ++boxX;
                            ++playerX;
                            break;
                        case Direction.Right:
                            --boxX;
                            --playerX;
                            break;
                        case Direction.Up:
                            ++boxY;
                            ++playerY;
                            break;
                        case Direction.Down:
                            --boxY;
                            --playerY;
                            break;
                    }
                }    
            }
        }
    }
}