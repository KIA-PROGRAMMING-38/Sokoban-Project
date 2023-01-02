namespace Sokoban
{
    enum Directions
    {
        NONE,
        LEFT,
        RIGHT,
        UP,
        DOWN,
    }
    class Program
    {
        static void Main()
        {
            //초기 세팅
            Console.ResetColor(); // 컬러를 초기화한다.
            Console.CursorVisible = false; // 커서 숨기기
            Console.Title = "미래의 babaisyou"; // 타이틀을 설정한다.
            Console.BackgroundColor = ConsoleColor.DarkGreen; // 배경색을 설정한다.
            Console.ForegroundColor = ConsoleColor.White; //글꼴색을 설정한다.
            Console.Clear(); // 출력된 모든 내용을 지운다.

            // 플레이어 설정
            const string PLAYER_CHARACTER = "B";

            //플레이어의 초기 좌표
            const int INITIAL_PLAYERX = 0;
            const int INITIAL_PLAYERY = 0;

            //플레이어의 현재 좌표
            int playerX = INITIAL_PLAYERX;
            int playerY = INITIAL_PLAYERY;

            //플레이어의 움직임 방향
            Directions playerDirection = Directions.NONE;

            // 박스 설정
            const string BOX_CHARACTER = "O";
            //const int INITIAL_BOXX = 5; const int INITIAL_BOXY = 10;
            //int boxX = INITIAL_BOXX; int boxY = INITIAL_BOXY;
            int[,] boxs = { { 5, 10 }, { 20, 12 }, { 18 , 5 } };
            int boxi = 0;

            // 플레이 공간 설정
            const int MAP_MIN_X = 0;
            const int MAP_MIN_Y = 0;
            const int MAP_MAX_X = 30;
            const int MAP_MAX_Y = 15;

            // 벽 생성
            const string WALL_CHARACTER = "W";
            //const int walls[walli, 0] = 12; const int walls[walli, 1] = 5;
            int[,] walls = { { 12, 5 }, { 8, 7 }, { 2, 6 } };
            int walli = 0;

            // 골 지점 생성
            const string GOAL_CHARACTER = "G";
            //const int GOAL_X = 20; const int GOAL_Y = 12;
            int[,] goals = { { 7, 14 }, { 8, 10 }, { 22, 14 } };
            int goali = 0;

            // 게임 종료 조건
            bool[] boxIntheGoal = new bool[3];
            
            // 게임 루프 == 프레임(frame)
            while (true)
            {
                // 이전 프레임을 지운다.
                Console.Clear();
                //---------------------------------Render--------------------------------
                // 플레이어 출력하기

                Console.SetCursorPosition(playerX, playerY);
                Console.Write(PLAYER_CHARACTER);

                // 골 출력하기
                for (goali = 0; goali < goals.Length / 2; goali++)
                {
                    Console.SetCursorPosition(goals[goali, 0], goals[goali, 1]);
                    Console.Write(GOAL_CHARACTER);
                }

                // 박스 출력하기
                for (boxi = 0; boxi < boxs.Length / 2; boxi++)
                {
                    Console.SetCursorPosition(boxs[boxi, 0], boxs[boxi, 1]);
                    Console.Write(BOX_CHARACTER);
                }   

                // 벽 출력하기
                for (walli = 0; walli < walls.Length / 2; walli++)
                {
                    Console.SetCursorPosition(walls[walli, 0], walls[walli, 1]);
                    Console.Write(WALL_CHARACTER);
                }

                //------------------------------ProcessInput-----------------------------
                ConsoleKey key = Console.ReadKey().Key;

                //---------------------------------Update--------------------------------
                //플레이어 이동
                if (key == ConsoleKey.LeftArrow) //왼쪽 이동
                {
                    playerX = Math.Max(MAP_MIN_X, playerX - 1);
                    playerDirection = Directions.LEFT;
                }
                if (key == ConsoleKey.RightArrow) //오른쪽 이동  
                {
                    playerX = Math.Min(playerX + 1, MAP_MAX_X);
                    playerDirection = Directions.RIGHT;
                }
                if (key == ConsoleKey.UpArrow) //위 이동
                {
                    playerY = Math.Max(MAP_MIN_Y, playerY - 1);
                    playerDirection = Directions.UP;
                }
                if (key == ConsoleKey.DownArrow) //아래 이동
                {
                    playerY = Math.Min(playerY + 1, MAP_MAX_Y);
                    playerDirection = Directions.DOWN;
                }

                // 플레이어- 벽 충돌
                for (walli = 0; walli < walls.Length / 2; walli++)
                {
                    if (playerX == walls[walli, 0] && playerY == walls[walli, 1])
                    {
                        switch (playerDirection)
                        {
                            case Directions.LEFT: // 벽 왼쪽
                                playerX += 1;
                                break;
                            case Directions.RIGHT: // 벽 오른쪽
                                playerX -= 1;
                                break;
                            case Directions.UP: // 박스 위쪽
                                playerY += 1;
                                break;
                            case Directions.DOWN: // 박스 아래쪽
                                playerY -= 1;
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다.");
                                return;
                        }
                    }
                }
                // 박스 업데이트
                for(boxi = 0; boxi < boxs.Length / 2; boxi++)
                {
                    if (playerX == boxs[boxi, 0] && playerY == boxs[boxi, 1]) // 플레이어가 이동하고 나니 박스가 있는 것
                    {
                        //박스 이동
                        switch (playerDirection)
                        {
                            case Directions.LEFT: // 박스 왼쪽
                                if (boxs[boxi, 0] == MAP_MIN_X)
                                {
                                    playerX = MAP_MIN_X + 1;
                                }
                                else
                                {
                                    boxs[boxi, 0] -= 1;
                                }
                                break;
                            case Directions.RIGHT: // 박스 오른쪽
                                if (boxs[boxi, 0] == MAP_MAX_X)
                                {
                                    playerX = MAP_MAX_X - 1;
                                }
                                else
                                {
                                    boxs[boxi, 0] = boxs[boxi, 0] + 1;
                                }
                                break;
                            case Directions.UP: // 박스 위쪽
                                if (boxs[boxi, 1] == MAP_MIN_Y)
                                {
                                    playerY = MAP_MIN_Y + 1;
                                }
                                else
                                {
                                    boxs[boxi, 1] = boxs[boxi, 1] - 1;
                                }
                                break;
                            case Directions.DOWN: // 박스 아래쪽
                                if (boxs[boxi, 1] == MAP_MAX_Y)
                                {
                                    playerY = MAP_MAX_Y - 1;
                                }
                                else
                                {
                                    boxs[boxi, 1] = boxs[boxi, 1] + 1;
                                }
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다.");
                                return;
                        }
                    }
                }
                // 박스 - 벽 충돌
                for (boxi = 0; boxi < boxs.Length / 2; boxi++)
                {
                    for (walli = 0; walli < walls[walli, 0] /2; walli++)
                    {
                        if (boxs[boxi, 0] == walls[walli, 0] && boxs[boxi, 1] == walls[walli, 1])
                        {
                            switch (playerDirection)
                            {
                                case Directions.LEFT: // 벽 왼쪽
                                    boxs[boxi, 0] = walls[walli, 0] + 1;
                                    playerX = boxs[boxi, 0] + 1;                                   
                                    break;
                                case Directions.RIGHT: // 벽 오른쪽
                                    boxs[boxi, 0] = walls[walli, 0] - 1;
                                    playerX = boxs[boxi, 0] - 1;
                                    break;
                                case Directions.UP: // 박스 위쪽
                                    boxs[boxi, 1] = walls[walli, 1] + 1;
                                    playerY = boxs[boxi, 1] + 1;
                                    break;
                                case Directions.DOWN: // 박스 아래쪽
                                    boxs[boxi, 1] = walls[walli, 1] - 1;
                                    playerY = boxs[boxi, 1] - 1;
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다.");
                                    return;
                            }
                        }
                    }                    
                }
                
                // 종료 조건
                for(boxi = 0; boxi < boxs.Length / 2; boxi++)
                {
                    for(goali = 0; goali < goals.Length / 2; goali++)
                    {
                        if (boxs[boxi, 0] == goals[goali, 0] && boxs[boxi, 1] == goals[goali, 1])
                            boxIntheGoal[goali] = true;
                    }
                }

                if (boxIntheGoal[0] == true && boxIntheGoal[1] == true && boxIntheGoal[2] == true)
                {
                    Console.Clear();
                    Console.WriteLine("!!!!클리어!!!!");
                    break;
                }
            }
        }
    }
}



