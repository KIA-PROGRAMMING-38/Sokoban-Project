namespace Sokoban
{
    // 열거형
    

    

    enum DIRECTION
    {
        NONE = 0,
        LEFT = 1,
        RIGHT = 2,
        UP = 3,
        DOWN = 4
    }








    class program
    {
        static void Main()
        {
            // 초기 세팅
            Console.ResetColor(); // 컬러를 초기화한다
            Console.CursorVisible = false; // 커서를 안보이게 해줌(콘솔에서는 입력하려는 표시인거임)
            Console.Title = "소코반 게임!"; // 콘솔창의 제목을 입력
            Console.BackgroundColor = ConsoleColor.Cyan; // 배경색을 바꿔줌
            Console.ForegroundColor = ConsoleColor.Red; // 글씨의 색을 바꿔줌
            Console.Clear(); // 출력된 모든 내용을 지운다




            // 기호상수
            // 맵의 가로범위, 세로범위 - O
            // 플레이어 기호(string literal) - O
            // 박스의 기호(string literal) - O
            // 플레이어 이동방향 - O
            // 플레이어의 초기좌표 - O
            // 박스의 초기좌표 - O


            const int MAP_MIN_X = 0;
            const int MAP_MIN_Y = 0;
            const int MAP_MAX_X = 15; // 맵의 가로범위
            const int MAP_MAX_Y = 10; // 맵의 세로범위

            const string Player = "R"; // 플레이어 기호
            const string Box = "B"; // 박스 기호

            //  const int player_Direction = 0;
            // int playerDirection = player_Direction; // 0: None, 1: 왼쪽으로 이동중이였다(Left), 2: Right, 3: Up, 4: Down

            DIRECTION playerDirection = DIRECTION.NONE;

            const int DirectionLeft = 1; // 플레이어 왼쪽 이동방향
            const int DirectionRight = 2; // 플레이어 오른쪽 이동방향
            const int DirectionUp = 3; // 플레이어 위쪽 이동방향
            const int DirectionDown = 4; // 플레이어 아래쪽 이동방향



            const int player_X = 0; // Player_X를 굽이 playerx 로 바꾼 이유는 아래를 다 바꾸기 귀찮아서임...
            int playerX = player_X; // 플레이어 X 초기좌표

            const int player_Y = 0; // 플레이어 Y 초기좌표
            int playerY = player_Y;


            const int Box_X = 2; // 플레이어 X 초기좌표
            int boxX = Box_X;

            const int Box_Y = 3; // 플레이어 Y 초기좌표
            int boxY = Box_Y;

            // 가로 15, 세로 15


            
          

            // 게임 루프 == 프레임(Frame)
            while (true)
            {

                Console.Clear();

                // ------------------------------------------ render -----------------------------------------------
                // 플레이어 출력하기

                Console.SetCursorPosition(playerX, playerY);
                Console.Write($"{Player}");


                Console.SetCursorPosition(boxX, boxY);
                Console.Write($"{Box}");


                // ------------------------------------------ ProcessInput -----------------------------------------

                ConsoleKey key = Console.ReadKey().Key;



                // ------------------------------------------ Update -----------------------------------------------
                // 플레이어 좌표 구하기
                if (key == ConsoleKey.RightArrow)
                {
                    // 오른쪽으로 이동
                    playerX = Math.Min(playerX + 1, MAP_MAX_X);
                    playerDirection = DIRECTION.RIGHT;
                }

                if (key == ConsoleKey.LeftArrow)
                {
                    playerX = Math.Max(MAP_MIN_X , playerX - 1);
                    playerDirection = DIRECTION.LEFT;
                }

                if (key == ConsoleKey.DownArrow)
                {
                    playerY = Math.Min(playerY + 1, MAP_MAX_Y);
                    playerDirection = DIRECTION.DOWN;
                }

                if (key == ConsoleKey.UpArrow)
                {
                    playerY = Math.Max(MAP_MIN_Y, playerY - 1);
                    playerDirection = DIRECTION.UP;
                }





                // 플레이어가 이동한 좌표를 다 구한 후!

                if (playerX == boxX && playerY == boxY) // 이동하고보니 박스가 있네?
                {
                    // 박스를 움직여주기
                    // 플레이어가 어디서 왔는지 모름!
                    // 플레이어가 온 방향에 따라서 박스의 위치가 달라짐. 1)왼쪽으로 이동중일떄 2)오른쪽으로 이동중일때 3)위로 이동중 4)아래로 이동중
                    switch (playerDirection)
                    {
                        case DIRECTION.LEFT: // 왼쪽으로 이동중일때
                            if (boxX == MAP_MIN_X) // 박스가 왼쪽 끝에 있다면?
                            {
                                playerX = MAP_MIN_X + 1;
                            }
                            else
                            {
                                boxX = boxX - 1;
                            }
                            break;

                        case DIRECTION.RIGHT: // 오른쪽으로 이동중일 때
                            if (boxX == MAP_MAX_X)
                            {
                                playerX = MAP_MAX_X - 1;
                            }
                            else
                            {
                                boxX = boxX + 1;
                            }

                            break;

                        case DIRECTION.UP: // 위로 이동중일때
                            if (boxY == MAP_MIN_Y)
                            {
                                playerY = MAP_MIN_Y + 1;
                            }
                            else
                            {
                                boxY = boxY - 1;
                            }
                            break;

                        case DIRECTION.DOWN: // 아래로 이동중 일때
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
                            Console.WriteLine($"플레이어의 이동방향이 잘못되었습니다. {playerDirection}");
                            return;

                    }

                }








            }

        }


    }




}






