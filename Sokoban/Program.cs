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

            // 초기 세팅
            Console.ResetColor();                       // 컬러를 초기화한다.
            Console.CursorVisible = false;              // 커서를 보이지 않게 해준다. 맥은 좀 보일수도..
            Console.Title = "Sokoban";                  // 제목을 정해준다.
            Console.BackgroundColor = ConsoleColor.Blue; // 백그라운드 색깔을 설정해준다.
            Console.ForegroundColor = ConsoleColor.DarkGray; // 글꼴 색깔 지정.
            Console.Clear(); // 출력된 모든 내용을 지운다.

            const int INITIAL_PLAYER_X = 0;
            const int INITIAL_PLAYER_Y = 0;

            // 플레이어 좌표 설정
            int playerX = INITIAL_PLAYER_X;
            int playerY = INITIAL_PLAYER_Y;

            
            // box 좌표 설정
            int boxX = 5;
            int boxY = 5;

            

            Direction playerDirection = Direction.Down;

            //map
            const int MAP_MIN_X = 0;
            const int MAP_MIN_Y = 0;
            const int MAP_MAX_X = 15;
            const int MAP_MAX_Y = 10;

            // 플레이어의 기호
            const string PLAYER_STRING = "H";

            // 박스의 기호
            const string BOX_STRING = "O";
            //플레이어의 초기 좌포
            




            // 게임 루프 == 프레임(Frame)
            while (true)

            {   // 이전 프레임을 지운다.
                Console.Clear();
                // ---------------------Render----------------

                Console.SetCursorPosition(playerX, playerY);
                Console.Write(PLAYER_STRING);

                // self
                Console.SetCursorPosition(boxX, boxY);
                Console.Write(BOX_STRING);

                // ---------------------ProcessInput-----------
                ConsoleKey key = Console.ReadKey().Key;
                // ---------------------Update-----------------

                if (key == ConsoleKey.LeftArrow)
                {
                    playerX = Math.Max(MAP_MIN_X, playerX - 1);
                    playerDirection = Direction.Left;

                }

                // 오른쪽 화살표키를 눌렀을 때
                if (key == ConsoleKey.RightArrow)
                {
                    // 오른쪽으로 이동 => 누를때마다 1씩 이동한다.
                    playerX = Math.Min(playerX + 1, MAP_MAX_X);
                    playerDirection = Direction.Right;

                }

                if (key == ConsoleKey.UpArrow)
                {
                    playerY = Math.Max(MAP_MIN_Y, playerY - 1);
                    playerDirection = Direction.Up;

                }

                if (key == ConsoleKey.DownArrow)
                {
                    playerY = Math.Min(playerY + 1, MAP_MAX_Y);
                    playerDirection = Direction.Up;

                }

                //박스 update
                if (playerX == boxX && playerY == boxY)
                {
                    switch (playerDirection)
                    {
                        case Direction.Left: //플레이어가 왼쪽으로 이동
                            if (boxX == MAP_MIN_X)
                            {
                                playerX = MAP_MIN_X + 1;
                            }
                            else
                            {
                                boxX -= 1;
                            }
                            break;
                        case Direction.Right: //플레이어가 오른쪽으로 이동
                            if (boxX == MAP_MAX_X)
                            {
                                playerX = MAP_MAX_X - 1;
                            }
                            else
                            {
                                boxX += 1;
                            }
                            break;
                        case Direction.Up: //플레이어가 위로 이동
                            if (boxY == MAP_MIN_Y)
                            {
                                playerY = MAP_MIN_Y + 1;
                            }
                            else
                            {
                                boxY -= 1;
                            }
                            break;
                        case Direction.Down:
                            if (boxY == MAP_MAX_Y)
                            {
                                playerY = MAP_MAX_Y - 1;
                            }
                            else
                            {
                                boxY += 1;
                            }
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine($"[Error]{playerDirection}");
                            break;
                    }
                }


            }
            

    }
}
}






//int x = 20;
//int y = 3;
//Console.SetCursorPosition(x, y);
//Console.Write("H"); // 개행 하면 안된다.
//ConsoleKeyInfo keyInfo = Console.ReadKey();
//Console.WriteLine(keyInfo.Key);




