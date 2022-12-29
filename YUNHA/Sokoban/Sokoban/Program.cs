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
            int[,] boxList = { { 5, 5 }, { 12, 5 }, { 3, 7 } };
            // const int INIT_BOX_X = 5, INIT_BOX_Y = 5;
            // int boxX = INIT_BOX_X, boxY = INIT_BOX_Y;
            const string BOX = "B";

            // 벽 설정
            int[,] wallList = { { 3, 4 }, { 1, 6 }, { 2, 8 } };
            // const int INIT_WALL_X = 3, INIT_WALL_Y = 3;
            const string WALL = "X";

            // 골인 지점
            int[,] goalList = { { 2, 2 }, { 4, 7 }, { 6, 9 } };
            // const int GOAL_X = 7, GOAL_Y = 7;
            const string GOAL = "G";

            // 맵 설정
            // 가로 15, 세로 10
            const int MAP_RIGHT_END = 20, MAP_DOWN_END = 10, MAP_LEFT_END = 0, MAP_UP_END = 0;

            // 게임 루프 == 프레임(Frame)
            while (true)
            {
                // 이전 프레임을 지운다.
                Console.Clear();
                bool[] goaledArr = new bool[3];


                // -------------------------------------Render------------------------------------- 
                // 플레이어 출력하기
                Console.SetCursorPosition(playerX, playerY);
                Console.Write(PLAYER);
                // 박스 출력하기
                for (int i = 0; i < boxList.Length / 2; i++)
                {
                    Console.SetCursorPosition(boxList[i, 0], boxList[i, 1]);
                    Console.Write(BOX);
                }

                // 벽 출력하기
                for (int i = 0; i < wallList.Length / 2; i++)
                {
                    Console.SetCursorPosition(wallList[i, 0], wallList[i, 1]);
                    Console.Write(WALL);
                }
                // 골인 지점 출력하기
                for (int i = 0; i < goalList.Length / 2; i++)
                {
                    Console.SetCursorPosition(goalList[i, 0], goalList[i, 1]);
                    Console.Write(GOAL);
                }

                // ----------------------------------ProcessInput---------------------------------- 
                ConsoleKey key = Console.ReadKey().Key;


                // -------------------------------------Update------------------------------------- 
                #region 플레이어 업데이트
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
                #endregion

                #region 박스 업데이트
                // 박스 업데이트
                // 플레이어가 이동한 후
                for(int i = 0; i < boxList.Length / 2; i++)
                {
                    if(playerX == boxList[i, 0] && playerY == boxList[i, 1])
                    {
                        switch (playerDirection)
                        {
                            case Direction.Left:         // 플레이어가 왼쪽으로 이동중
                                if (boxList[i, 0] == MAP_LEFT_END)
                                {
                                    playerX = MAP_LEFT_END + 1;
                                }
                                else
                                {
                                    boxList[i, 0] -= 1;
                                }
                                break;
                            case Direction.Right:         // 플레이어가 오른쪽으로 이동 중
                                if (boxList[i, 0] == MAP_RIGHT_END)
                                {
                                    playerX = MAP_RIGHT_END - 1;
                                }
                                else
                                {
                                    boxList[i, 0] += 1;
                                }
                                break;
                            case Direction.Up:         // 플레이어가 위로 이동 중
                                if (boxList[i, 1] == MAP_UP_END)
                                {
                                    playerY = MAP_UP_END + 1;
                                }
                                else
                                {
                                    boxList[i, 1] -= 1;
                                }
                                break;
                            case Direction.Down:         // 플레이어가 아래로 이동 중
                                if (boxList[i, 1] == MAP_DOWN_END)
                                {
                                    playerY = MAP_DOWN_END - 1;
                                }
                                else
                                {
                                    boxList[i, 1] += 1;
                                }
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다. {playerDirection}");
                                return;
                        }
                    }
                }
                #endregion

                #region 박스 <=> 박스 상호작용
                for(int i = 0; i < (boxList.Length / 2) - 1; i++)
                {
                    for(int j = i + 1;  j < boxList.Length / 2; j++)
                    {
                        if(boxList[i, 0] == boxList[j, 0] && boxList[i, 1] == boxList[j, 1])
                        {
                            switch(playerDirection)
                            {
                                case Direction.Left:
                                    boxList[i, 0] = boxList[j, 0] + 1;
                                    playerX = boxList[i, 0] + 1;
                                    break;
                                case Direction.Right:
                                    boxList[i, 0] = boxList[j, 0] - 1;
                                    playerX = boxList[i, 0] - 1;
                                    break;
                                case Direction.Up:
                                    boxList[i, 1] = boxList[j, 1] + 1;
                                    playerY = boxList[i, 1] + 1;
                                    break;
                                case Direction.Down:
                                    boxList[i, 1] = boxList[j, 1] - 1;
                                    playerY = boxList[i, 1] - 1;
                                    break;
                                default:
                                    return;

                            }
                        }
                    }
                }


                #endregion

                #region 플레이어 <=> 벽 상호작용
                // 벽 상호작용
                for (int i = 0; i < wallList.Length / 2; i++)
                {
                    if (playerX == wallList[i, 0] && playerY == wallList[i, 1])
                    {
                        switch (playerDirection)
                        {
                            case Direction.Right:
                                playerX = wallList[i, 0] - 1;
                                break;
                            case Direction.Left:
                                playerX = wallList[i, 0] + 1;
                                break;
                            case Direction.Up:
                                playerY = wallList[i , 1] + 1;
                                break;
                            case Direction.Down:
                                playerY = wallList[i, 1] - 1;
                                break;
                            default:
                                return;
                        }
                    }
                }
                
                #endregion

                #region 박스 <=> 벽 상호작용
                for(int i = 0; i < boxList.Length / 2; i++)
                {
                    for(int j = 0; j < wallList.Length / 2; j++)
                    {
                        if (boxList[i, 0] == wallList[j, 0] && boxList[i, 1] == wallList[j, 1])
                        {
                            switch(playerDirection)
                            {
                                case Direction.Right:
                                    boxList[i, 0] = wallList[j, 0] - 1;
                                    playerX = boxList[i, 0] - 1;
                                    break;
                                case Direction.Left:
                                    boxList[i, 0] = wallList[j, 0] + 1;
                                    playerX = boxList[i, 0] + 1;
                                    break;
                                case Direction.Up:
                                    boxList[i, 1] = wallList[j, 1] + 1;
                                    playerY = boxList[i, 1] + 1;
                                    break;
                                case Direction.Down:
                                    boxList[i, 1] = wallList[j, 1] - 1;
                                    playerY = boxList[j, 1] - 1;
                                    break;
                                default:
                                    return;
                            }
                        }
                    }
                }
                #endregion
                
                #region 골인 구현
                for(int i = 0; i < boxList.Length / 2; i++)
                {
                    for(int j = 0; j < goalList.Length / 2; j++)
                    {
                        if (boxList[i, 0] == goalList[j, 0] && boxList[i, 1] == goalList[j, 1])
                        {
                            goaledArr[j] = true;
                        }
                    }
                }

                if (goaledArr[0] == true && goaledArr[1] == true && goaledArr[2] == true)
                {
                    Console.Clear();
                    Console.WriteLine("GAME CLEAR");
                    break;
                }
                #endregion
            }
        }
    }
}

