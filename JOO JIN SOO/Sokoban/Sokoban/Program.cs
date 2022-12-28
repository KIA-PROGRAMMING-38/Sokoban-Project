namespace Sokoban
{
    // 열거형
    enum Map_MinMax // 맵 한계선
    {
        MAPFRONTIER_LEFT = 0,
        MAPFRONTIER_RIGHT = 30,
        MAPFRONTIER_UP = 0,
        MAPFRONTIER_DOWN = 20
    }
    
    enum Direction // 플레이어 이동 방향
    {
        DIRECTION_LEFT = 1,
        DIRECTION_RIGHT = 2,
        DIRECTION_UP = 3,
        DIRECTION_DOWN = 4,
    }

    enum Player_Initial_Coordinate
    {
        PLAYERINITIAL_X = 0,
        PLAYERINITIAL_Y = 0
    }

    enum First_Box_Initial_Coordinate
    {
        FIRST_BOX_X = 10,
        FIRST_BOX_Y = 6
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

            // 플레이어의 기호(string literal)
            const string PLAYERLITERAL = "P";
            // 박스의 기호(string literal)
            const string BOXLITERAL = "O";

            // 플레이어의 좌표 이동
            int player_X = (int)Player_Initial_Coordinate.PLAYERINITIAL_X;
            int player_Y = (int)Player_Initial_Coordinate.PLAYERINITIAL_Y;
            int playerDirection = 0; // 0: None, 1: Left, 2: Right, 3: Up, 4: Down

            // 박스의 좌표 이동
            int box_X = (int)First_Box_Initial_Coordinate.FIRST_BOX_X;
            int box_Y = (int)First_Box_Initial_Coordinate.FIRST_BOX_Y;


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

                if (key == ConsoleKey.LeftArrow && player_X > (int)Map_MinMax.MAPFRONTIER_LEFT)
                {
                    player_X = Math.Max((int)Map_MinMax.MAPFRONTIER_LEFT, player_X - 1);
                    playerDirection = (int)Direction.DIRECTION_LEFT;
                }

                if (key == ConsoleKey.RightArrow && player_X < (int)Map_MinMax.MAPFRONTIER_RIGHT)
                {
                    player_X = Math.Min(player_X + 1, (int)Map_MinMax.MAPFRONTIER_RIGHT);
                    playerDirection = (int)Direction.DIRECTION_RIGHT;
                }

                if (key == ConsoleKey.UpArrow && player_Y > (int)Map_MinMax.MAPFRONTIER_UP)
                {
                    player_Y = Math.Max((int)Map_MinMax.MAPFRONTIER_UP, player_Y - 1);
                    playerDirection = (int)Direction.DIRECTION_UP;
                }

                if (key == ConsoleKey.DownArrow && player_Y < (int)Map_MinMax.MAPFRONTIER_DOWN)
                {
                    player_Y = Math.Min(player_Y + 1, (int)Map_MinMax.MAPFRONTIER_DOWN);
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
                            if (box_X == (int)Map_MinMax.MAPFRONTIER_LEFT)
                            {
                                player_X = (int)Map_MinMax.MAPFRONTIER_LEFT + 1;
                            }
                            else
                            {
                                box_X = box_X - 1;
                            }
                            break;
                        case (int)Direction.DIRECTION_RIGHT: // 플레이어가 오른쪽으로 이동 중
                            if (box_X == (int)Map_MinMax.MAPFRONTIER_RIGHT)
                            {
                                player_X = (int)Map_MinMax.MAPFRONTIER_RIGHT - 1;
                            }
                            else
                            {
                                box_X = box_X + 1;
                            }
                            break;
                        case (int)Direction.DIRECTION_UP: // 플레이어가 위쪽으로 이동 중
                            if (box_Y == (int)Map_MinMax.MAPFRONTIER_UP)
                            {
                                player_Y = (int)Map_MinMax.MAPFRONTIER_UP + 1;
                            }
                            else
                            {
                                box_Y = box_Y - 1;
                            }
                            break;
                        case (int)Direction.DIRECTION_DOWN: // 플레이어가 아래쪽으로 이동 중
                            if (box_Y == (int)Map_MinMax.MAPFRONTIER_DOWN)
                            {
                                player_Y = (int)Map_MinMax.MAPFRONTIER_DOWN - 1;
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
