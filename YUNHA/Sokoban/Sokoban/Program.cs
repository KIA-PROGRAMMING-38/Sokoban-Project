namespace Sokoban
{
    enum Direction
    {
        None,
        Left,
        Right,
        Up,
        Down
    };

    class Program
    {
        static void Main()
        {
            // 초기 세팅
            const string VERSION = "2022.12.28";
            Console.ResetColor();                                   // 컬러를 초기화한다.
            Console.CursorVisible = false;                          // 커서를 숨긴다.
            Console.Title = "Sokoban " + VERSION;                   // 타이틀을 설정한다.
            Console.BackgroundColor = ConsoleColor.DarkBlue;        // 배경색을 설정한다.
            Console.ForegroundColor = ConsoleColor.Yellow;          // 글꼴색을 설정한다.
            Console.Clear();                                        // 출력된 모든 내용을 지운다.

            // 플레이어 설정
            const int INIT_PLAYER_X = 0, INIT_PLAYER_Y = 0;
            int playerX = INIT_PLAYER_X, playerY = INIT_PLAYER_Y;
            Direction playerDirection = Direction.None;
            const string PLAYER = "K";

            // 박스 설정
            const int INIT_BOX_X = 5, INIT_BOX_Y = 5;
            int boxX = INIT_BOX_X, boxY = INIT_BOX_Y;
            const string BOX = "B";


            // 맵 설정
            // 가로 15, 세로 10
            const int MAP_RIGHT_END = 20, MAP_DOWN_END = 10, MAP_LEFT_END = 0, MAP_UP_END = 0;

            // 게임 루프 == 프레임(Frame)
            while (true)
            {
                // 이전 프레임을 지운다.
                Console.Clear();

                // -------------------------------------Render------------------------------------- 
                // 플레이어 출력하기
                Console.SetCursorPosition(playerX, playerY);
                Console.Write(PLAYER);
                Console.SetCursorPosition(boxX, boxY);
                Console.Write(BOX);


                // ----------------------------------ProcessInput---------------------------------- 
                ConsoleKey key = Console.ReadKey().Key;


                // -------------------------------------Update------------------------------------- 
                // 플레이어 업데이트
                // 오른쪽 키를 눌렀을 때
                if (key == ConsoleKey.RightArrow)
                {
                    // 오른쪽으로 이동
                    playerX = Math.Min(playerX + 1, MAP_RIGHT_END);
                    playerDirection = Direction.Right ;
                }
                // 아래쪽 키를 눌렀을 때
                if (key == ConsoleKey.DownArrow)
                {
                    // 아래로 이동
                    playerY = Math.Min(playerY + 1, MAP_DOWN_END);
                    playerDirection = Direction.Down;
                }
                // 왼쪽 키를 눌렀을 때
                if (key == ConsoleKey.LeftArrow)
                {
                    // 왼쪽으로 이동
                    playerX = Math.Max(MAP_LEFT_END, playerX - 1);
                    playerDirection = Direction.Left;
                }
                // 위쪽 키를 눌렀을 때
                if (key == ConsoleKey.UpArrow)
                {
                    // 위로 이동
                    playerY = Math.Max(MAP_UP_END, playerY - 1);
                    playerDirection = Direction.Up;
                }

                // 박스 업데이트
                // 플레이어가 이동한 후
                if (playerX == boxX && playerY == boxY)
                {
                    switch (playerDirection)
                    {
                        case Direction.Left:         // 플레이어가 왼쪽으로 이동중
                            if (boxX == MAP_LEFT_END)
                            {
                                playerX = MAP_LEFT_END + 1;
                            }
                            else
                            {
                                boxX -= 1;
                            }
                            break;
                        case Direction.Right:         // 플레이어가 오른쪽으로 이동 중
                            if (boxX == MAP_RIGHT_END)
                            {
                                playerX = MAP_RIGHT_END - 1;
                            }
                            else
                            {
                                boxX += 1;
                            }
                            break;
                        case Direction.Up:         // 플레이어가 위로 이동 중
                            if (boxY == MAP_UP_END)
                            {
                                playerY = MAP_UP_END + 1;
                            }
                            else
                            {
                                boxY -= 1;
                            }
                            break;
                        case Direction.Down:         // 플레이어가 아래로 이동 중
                            if (boxY == MAP_DOWN_END)
                            {
                                playerY = MAP_DOWN_END - 1;
                            }
                            else
                            {
                                boxY += 1;
                            }
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다. {playerDirection}");
                            return;
                    }
                }
            }
        }
    }
}

