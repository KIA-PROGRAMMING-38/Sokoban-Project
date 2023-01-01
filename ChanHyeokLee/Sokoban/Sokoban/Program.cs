namespace Sokoban
{
    // 열거형
    enum Direction
    {
        None,
        Left,
        Right,
        Up,
        Down
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
            const int MAP_MIN_X = 1;
            const int MAP_MIN_Y = 1;
            const int MAP_MAX_X = 20;
            const int MAP_MAX_Y = 20;

            //플레이어 및 오브젝트 생성           
            const string PLAYER_MARK = "L";
            const string OBJECT_MARK = "O";
            const string MAP_ROW_OUTLINE = "H";
            const string MAP_VERTICAL_OUTLINE = "I";
            //플레이어 초기 좌표
            const int PLAYER_X = 1;
            const int PLAYER_Y = 1;
            //오브젝트 초기 좌표
            const int OBJECT_X = 7;
            const int OBJECT_Y = 4;
            const int BOX_X = 10;
            const int BOX_Y = 6;
            const int OBJ_X = 8;
            const int OBJ_Y = 8;
            // 벽의 좌표
            const int INITIAL_WALL_X = 15;
            const int INITIAL_WALL_Y = 17;
            const int INITIAL_WALL2_X = 12;
            const int INITIAL_WALL2_Y = 10;
            const int INITIAL_WALL3_X = 4;
            const int INITIAL_WALL3_Y = 4;

            // 벽의 기호
            const string WALL_STRING = "W";
            // 골인 지점 좌표
            const int GOALIN_X = 10;
            const int GOALIN_Y = 10;
            const int GOALIN_X2 = 3;
            const int GOALIN_Y2 = 3;
            const int GOALIN_X3 = 18;
            const int GOALIN_Y3 = 19;
            // 골인 기호
            const string GOAL_STRING = "B";

            //플레이어 좌표설정
            int playerX = PLAYER_X;
            int playerY = PLAYER_Y;

            //오브젝트 좌표설정
            int[] objectX = { OBJECT_X, BOX_X, OBJ_X };
            int[] objectY = { OBJECT_Y, BOX_Y, OBJ_Y };

            // 벽의 좌표설정
            int[] wallX = { INITIAL_WALL_X, INITIAL_WALL2_X, INITIAL_WALL3_X };
            int[] wallY = { INITIAL_WALL_Y, INITIAL_WALL2_Y, INITIAL_WALL3_Y };

            // 골인 좌표설정
            int[] goalX = { GOALIN_X, GOALIN_X2, GOALIN_X3 };
            int[] goalY = { GOALIN_Y, GOALIN_Y2, GOALIN_Y3 };




            //게임 루프 == 프레임(Frame)
            while (true)
            {
                Console.Clear();

                //맵 가로 30 세로 30
                // ------------------------------ Render ------------------------------
                // 플레이어 출력하기
                Console.SetCursorPosition(playerX, playerY);
                Console.Write(PLAYER_MARK);

                // 오브젝트 출력하기
                for (int i = 0; i < objectX.Length; i++)
                {
                    Console.SetCursorPosition(objectX[i], objectY[i]);
                    Console.Write(OBJECT_MARK);
                }

                // 벽 출력
                for (int i = 0; i < wallX.Length; i++)
                {
                    Console.SetCursorPosition(wallX[i], wallY[i]);
                    Console.Write(WALL_STRING);
                }

                // 골인 출력
                for (int i = 0; i < goalX.Length; i++)
                {
                    Console.SetCursorPosition(goalX[i], goalY[i]);
                    Console.Write(GOAL_STRING);
                }

                // 맵 테두리 출력
                for (int i = 0; i < MAP_MAX_X + 1; i++)
                {
                    Console.SetCursorPosition(i, MAP_MIN_Y - 1);
                    Console.Write(MAP_ROW_OUTLINE);
                    Console.SetCursorPosition(i, MAP_MAX_Y + 1);
                    Console.Write(MAP_ROW_OUTLINE);
                }
                for (int i = 0; i < MAP_MAX_Y + 1; i++)
                {
                    Console.SetCursorPosition(MAP_MIN_X - 1, i);
                    Console.Write(MAP_VERTICAL_OUTLINE);
                    Console.SetCursorPosition(MAP_MAX_X + 1, i);
                    Console.Write(MAP_VERTICAL_OUTLINE);
                }


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




                // 오브젝트 업데이트
                // 플레이어가 이동한 후
                for (int i = 0; i < objectX.Length; i++)
                {
                    if (playerX == objectX[i] && playerY == objectY[i])
                    {
                        // 오브젝트를 움직이자
                        switch (playerDirection)
                        {
                            case Direction.Left:
                                if (objectX[i] == MAP_MIN_X)
                                {
                                    playerX = MAP_MIN_X + 1;
                                }
                                else
                                {
                                    objectX[i] -= 1;
                                }
                                break;
                            case Direction.Right:
                                if (objectX[i] == MAP_MAX_X)
                                {
                                    playerX = MAP_MAX_X - 1;
                                }
                                else
                                {
                                    objectX[i] += 1;
                                }
                                break;
                            case Direction.Up:
                                if (objectY[i] == MAP_MIN_Y)
                                {
                                    playerY = MAP_MIN_Y + 1;
                                }
                                else
                                {
                                    objectY[i] -= 1;
                                }
                                break;
                            case Direction.Down:
                                if (objectY[i] == MAP_MAX_Y)
                                {
                                    playerY = MAP_MAX_Y - 1;
                                }
                                else
                                {
                                    objectY[i] += 1;
                                }
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine("[Error] 플레이어의 이동 방향이 잘못되었습니다. {playerDirection}");
                                return; // 프로그램을 종류

                        }
                    }

                    // 플레이어가 어느 방향에서 왔는지에 따라 박스의 위치가 달라짐.
                }
                // 오브젝트 충돌
                for(int i=0; i < objectX.Length; i++)
                {
                   for(int j=0; j < objectX.Length; j++)
                    {
                        if (i != j && objectX[i] == objectX[j] && objectY[i] == objectY[j])
                        {
                            switch (playerDirection)
                            {
                                case Direction.Left:
                                    objectX[j] += 1;
                                    playerX += 1;
                                    break;
                                case Direction.Right:
                                    objectX[j] -= 1;
                                    playerX -= 1;
                                    break;
                                case Direction.Up:
                                    objectY[j] += 1;
                                    playerY += 1;
                                    break;
                                case Direction.Down:
                                    objectY[j] -= 1;
                                    playerY -= 1;
                                    break;

                            }
                        }
                   } 
                }

                // 플레이어와 벽의 충돌
                for (int i = 0; i < wallX.Length; i++)
                {
                    if (playerX == wallX[i] && playerY == wallY[i])
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
                }

                // 오브젝트와 벽의 충돌
                for (int i = 0; i < wallX.Length; i++)
                {
                    for (int j = 0; j < objectX.Length; j++)
                    {
                        if (objectX[j] == wallX[i] && objectY[j] == wallY[i])
                        {
                            switch (playerDirection)
                            {
                                case Direction.Left:
                                    objectX[j] += 1;
                                    playerX += 1;
                                    break;
                                case Direction.Right:
                                    objectX[j] -= 1;
                                    playerX -= 1;
                                    break;
                                case Direction.Up:
                                    objectY[j] += 1;
                                    playerY += 1;
                                    break;
                                case Direction.Down:
                                    objectY[j] -= 1;
                                    playerY -= 1;
                                    break;

                            }
                        }
                    }
                }

                // 골인 후 게임 종료
                int goalCount = 0;
                for (int i = 0; i < goalX.Length; i++)
                {
                    for(int j=0; j < goalX.Length; j++)
                    {
                        if (objectX[j] == goalX[i] && objectY[j] == goalY[i])
                        {
                            goalCount++;
                        }
                    }
                }
                if (goalCount == goalX.Length)
                {
                    Console.Clear();
                    Console.WriteLine("게임 끝!");
                    break;
                }
            }
        }
    }
}




