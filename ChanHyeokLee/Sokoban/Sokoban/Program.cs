namespace Sokoban
{
    // 열거형
    enum Direction
    {
        None = 0,
        Left = 1,
        Right = 2,
        Up = 3,
        Down = 4
    }
        
    class Program
    {
        static void Main()
        {
            //초기 세팅
            Console.ResetColor(); //컬러를 초기화한다.
            Console.CursorVisible = false; // 커서를 없애준다.
            Console.Title = "소코반"; //타이틀 이름
            Console.BackgroundColor = ConsoleColor.Red; // 배경색을 설정한다.
            Console.ForegroundColor = ConsoleColor.White; // 글꼴 색을 설정한다.
            Console.Clear(); // 출력된 모든 내용을 지운다.

            
            //맵 범위
            const int MAP_MIN_X = 0;
            const int MAP_MIN_Y = 0;
            const int MAP_MAX_X = 15;
            const int MAP_MAX_Y = 10;
            
            //플레이어 및 오브젝트 생성           
            const string PLAYER_MARK = "L";
            const string OBJECT_MARK = "O";
            //초기 좌표
            const int PLAYER_X = 0;
            const int PLAYER_Y = 0;
            const int OBJECT_X = 5;
            const int OBJECT_Y = 5;

            //플레이어 좌표설정
            int playerX = PLAYER_X;
            int playerY = PLAYER_Y;
            int objectX = OBJECT_X;
            int objectY = OBJECT_Y;

            // 벽의 좌표
            const int INITIAL_WALL_X = 7;
            const int INITIAL_WALL_Y = 8;
            // 벽의 기호
            const string WALL_STRING = "W";
            // 벽의 좌표설정
            int wallX = INITIAL_WALL_X;
            int wallY = INITIAL_WALL_Y;
            


            


            //게임 루프 == 프레임(Frame)
            while (true)
            {
                Console.Clear();
                
                //맵 가로 15 세로 10
                // ------------------------------ Render ------------------------------
                // 플레이어 출력하기
                Console.SetCursorPosition(playerX, playerY);
                Console.Write(PLAYER_MARK);
                // 박스 출력하기
                Console.SetCursorPosition(objectX, objectY);
                Console.Write(OBJECT_MARK);
                // 벽 출력
                Console.SetCursorPosition(INITIAL_WALL_X, INITIAL_WALL_Y);
                Console.Write(WALL_STRING);
                
                
                // ------------------------------ ProcessInput ------------------------
                ConsoleKey key = Console.ReadKey().Key;

                // ------------------------------ Update ------------------------------

                
                Direction playerDirection = Direction.None;
                // 오른쪽 화살표 키를 눌렀을 때
                if (key == ConsoleKey.RightArrow)
                {

                    playerX = Math.Min(playerX + 1, MAP_MAX_X);
                    playerDirection = Direction.Right;
                    
                }
                // 왼쪽 화살표 키를 눌렀을 때
                if (key == ConsoleKey.LeftArrow)
                {
                    playerX = Math.Max(MAP_MIN_X, playerX - 1);
                    playerDirection = Direction.Left;
                }
                // 위쪽 화살표 키를 눌렀을 때
                if (key == ConsoleKey.UpArrow)
                {
                    playerY = Math.Max(MAP_MIN_Y, playerY - 1);
                    playerDirection = Direction.Up;
                }
                // 아래로 움질일 때
                if (key == ConsoleKey.DownArrow)
                {
                    playerY = Math.Min(playerY + 1, MAP_MAX_Y);
                    playerDirection = Direction.Down;
                }
                
                
                
                
                // 박스 업데이트
                // 플레이어가 이동한 후
                if (playerX == objectX && playerY == objectY)
                {
                    // 박스를 움직이자
                    switch (playerDirection)
                    {
                        case Direction.Left:
                            if (objectX == MAP_MIN_X)
                            {
                                playerX = 1;
                            }
                            else
                            {
                                objectX -= 1;
                            }
                            break;
                        case Direction.Right:
                            if (objectX == MAP_MAX_X)
                            {
                                playerX = 14;
                            }
                            else
                            {
                                objectX += 1;
                            }
                            break;
                        case Direction.Up:
                            if (objectY == MAP_MIN_Y)
                            {
                                playerY += 1;
                            }
                            else
                            {
                                objectY -= 1;
                            }
                            break;
                        case Direction.Down:
                            if (objectY == MAP_MAX_Y)
                            {
                                playerY = 9;
                            }
                            else
                            {
                                objectY += 1;
                            }
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("[Error] 플레이어의 이동 방향이 잘못되었습니다. {playerDirection}");
                            return; // 프로그램을 종류
                    }
                    // 플레이어가 어느 방향에서 왔는지에 따라 박스의 위치가 달라짐.

                }
                // 플레이어와 벽의 충돌
                if (playerX == wallX && playerY == wallY)
                {
                    switch (playerDirection)
                    {
                        case Direction.Left:
                            playerX += 1;
                            break;
                        case Direction.Right:
                            playerX -= 1;
                            break;
                        case Direction.Up:
                            playerY += 1;
                            break;
                        case Direction.Down:
                            playerY -= 1;
                            break;
                    }
                }
                // 박스와 벽의 충돌
                if (objectX == wallX && objectY == wallY)
                {
                    switch (playerDirection)
                    {
                        case Direction.Left:
                            objectX += 1;
                            playerX += 1;
                            break;
                        case Direction.Right:
                            objectX -= 1;
                            playerX -= 1;
                            break;
                        case Direction.Up:
                            objectY += 1;
                            playerY += 1;
                            break;
                        case Direction.Down:
                            objectY -= 1;
                            playerY -= 1;
                            break;

                    }
                }



            }
        }
    }
}




