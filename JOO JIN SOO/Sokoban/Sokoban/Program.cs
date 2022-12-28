namespace Sokoban
{
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

            // 기호 상수
            // 맵의 가로 범위, 세로 범위
            const int MAPFRONTIER_LEFT = 0;
            const int MAPFRONTIER_RIGHT = 30;
            const int MAPFRONTIER_UP = 0;
            const int MAPFRONTIER_DOWN = 20;

            // 플레이어의 기호(string literal)
            const string PLAYERLITERAL = "P";

            // 플레이어의 이동 방향
            const int DIRECTION_LEFT = 1;
            const int DIRECTION_RIGHT = 2;
            const int DIRECTION_UP = 3;
            const int DIRECTION_DOWN = 4;

            // 플레이어의 초기 좌표
            const int playerAX = 0;
            const int playerAY = 0;

            // 박스의 기호(string literal)
            const string BOXLITERAL = "O";

            // 박스의 초기 좌표
            const int boxX = 10;
            const int boxY = 6;

            // 플레이어의 좌표 이동
            int player_X = playerAX;
            int player_Y = playerAY;
            int playerDirection = 0; // 0: None, 1: Left, 2: Right, 3: Up, 4: Down

            // 박스의 좌표 이동
            int box_X = boxX;
            int box_Y = boxY;


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

                //-------------------------------------- ProcessInput -------------------------------------------
                ConsoleKeyInfo inputKey = Console.ReadKey();
                ConsoleKey key = inputKey.Key;
                // 이것은 ConsoleKey inputKey = Console.ReadKey().Key; 와 같다

                //-------------------------------------- Update -------------------------------------------------
                // 플레이어 업데이트

                if (key == ConsoleKey.LeftArrow && playerAX + player_X > MAPFRONTIER_LEFT)
                {
                    player_X = Math.Max(MAPFRONTIER_LEFT, player_X - 1);
                    playerDirection = DIRECTION_LEFT;
                }

                if (key == ConsoleKey.RightArrow && playerAX + player_X < MAPFRONTIER_RIGHT)
                {
                    player_X = Math.Min(player_X + 1, MAPFRONTIER_RIGHT);
                    playerDirection = DIRECTION_RIGHT;
                }

                if (key == ConsoleKey.UpArrow && playerAY + player_Y > MAPFRONTIER_UP)
                {
                    player_Y = Math.Max(MAPFRONTIER_UP, player_Y - 1);
                    playerDirection = DIRECTION_UP;
                }

                if (key == ConsoleKey.DownArrow && playerAY + player_Y < MAPFRONTIER_DOWN)
                {
                    player_Y = Math.Min(player_Y + 1, MAPFRONTIER_DOWN);
                    playerDirection = DIRECTION_DOWN;
                }


                // 박스 업데이트
                // 플레이어가 이동한 후


                if (player_X == box_X && player_Y == box_Y)
                {
                    switch (playerDirection)
                    {
                        // 박스를 움직여주면 됨
                        case DIRECTION_LEFT: // 플레이어가 왼쪽으로 이동 중
                            if (box_X == MAPFRONTIER_LEFT)
                            {
                                player_X = MAPFRONTIER_LEFT + 1;
                            }
                            else
                            {
                                box_X = box_X - 1;
                            }
                            break;
                        case DIRECTION_RIGHT: // 플레이어가 오른쪽으로 이동 중
                            if (box_X == MAPFRONTIER_RIGHT)
                            {
                                player_X = MAPFRONTIER_RIGHT - 1;
                            }
                            else
                            {
                                box_X = box_X + 1;
                            }
                            break;
                        case DIRECTION_UP: // 플레이어가 위쪽으로 이동 중
                            if (box_Y == MAPFRONTIER_UP)
                            {
                                player_Y = MAPFRONTIER_UP + 1;
                            }
                            else
                            {
                                box_Y = box_Y - 1;
                            }
                            break;
                        case DIRECTION_DOWN: // 플레이어가 아래쪽으로 이동 중
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

                    // 플레이어가 어느 방향에서 왔는지에 따라 박스의 위치가 달라짐
                    // 4가지 경우의 수
                    // 1. 플레이어가 박스를 오른쪽으로 밀고 있을 때
                    // 2. 플레이어가 박스를 왼쪽으로 밀고 있을 때
                    // 3. 플레이어가 박스를 위쪽으로 밀고 있을 때
                    // 4. 플레이어가 박스를 아래쪽으로 밀고 있을 때
                }
            }
        }
    }
}
