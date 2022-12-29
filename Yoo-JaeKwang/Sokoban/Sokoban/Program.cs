namespace Sokoban
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
            const int PLAYER_INITIAL_X = 3;
            const int PLAYER_INITIAL_Y = 2;
            const int BOX_INITIAL_X = 10;
            const int BOX_INITIAL_Y = 5;
            const int WALL_INITIAL_X = 8;
            const int WALL_INITIAL_Y = 8;
            int playerX = PLAYER_INITIAL_X;
            int playerY = PLAYER_INITIAL_Y;
            int boxX = BOX_INITIAL_X;
            int boxY = BOX_INITIAL_Y;
            Direction playerDirection = default;

            // 게임 루프 == 프레임(Frame)
            while (true)
            {
                Console.Clear();
                // -------------------------------------- Render ------------------------------------------------
                Console.SetCursorPosition(playerX, playerY);
                Console.Write(PLAYER_SYMBOL);
                Console.SetCursorPosition(boxX, boxY);
                Console.Write(BOX_SYMBOL);
                Console.SetCursorPosition(WALL_INITIAL_X, WALL_INITIAL_Y);
                Console.Write(WALL_SYMBOL);
                // -------------------------------------- ProcessInput ------------------------------------------------
                ConsoleKey playerKey = Console.ReadKey().Key; // ConsoleKeyInfo keyInfo = Console.ReadKey(); ConsoleKey key = keyInfo.Key;
                // -------------------------------------- Update ------------------------------------------------
                // 플레이어
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
                if (playerX == boxX && playerY == boxY)
                {
                    switch (playerDirection)
                    {
                        case Direction.Up:
                            if (boxY == MAP_MIN_Y)
                            {
                                playerY = MAP_MIN_Y + 1;
                            }
                            else
                            {
                                --boxY;
                            }
                            break;
                        case Direction.Down:
                            if (boxY == MAP_MAX_Y)
                            {
                                playerY = MAP_MAX_Y - 1;
                            }
                            else
                            {
                                ++boxY;
                            }
                            break;
                        case Direction.Left:
                            if (boxX == MAP_MIN_X)
                            {
                                playerX = MAP_MIN_X + 1;
                            }
                            else
                            {
                                --boxX;
                            }
                            break;
                        case Direction.Right:
                            if (boxX == MAP_MAX_X)
                            {
                                playerX = MAP_MAX_X - 1;
                            }
                            else
                            {
                                ++boxX;
                            }
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다.");

                            return;
                    }
                }
                //벽에 사람
                if (playerX == WALL_INITIAL_X && playerY == WALL_INITIAL_Y)
                {
                    switch (playerDirection)
                    {
                        case Direction.Up:
                            playerY = WALL_INITIAL_Y + 1;
                            break;
                        case Direction.Down:
                            playerY = WALL_INITIAL_Y - 1;
                            break;
                        case Direction.Left:
                            playerX = WALL_INITIAL_X + 1;
                            break;
                        case Direction.Right:
                            playerX = WALL_INITIAL_X - 1;
                            break;
                    }
                }
                if (boxX == WALL_INITIAL_X && boxY == WALL_INITIAL_Y)
                {
                    switch (playerDirection)
                    {
                        case Direction.Up:
                            boxY = WALL_INITIAL_Y + 1;
                            playerY = boxY + 1;
                            break;
                        case Direction.Down:
                            boxY = WALL_INITIAL_Y - 1;
                            playerY = boxY - 1;
                            break;
                        case Direction.Left:
                            boxX = WALL_INITIAL_X + 1;
                            playerX = boxX + 1;
                            break;
                        case Direction.Right:
                            boxX = WALL_INITIAL_X - 1;
                            playerX = boxX - 1;
                            break;
                    }
                }
            }
        }
    }
}