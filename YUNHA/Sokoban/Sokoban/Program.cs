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
            const string VERSION = "2023.01.09";
            Console.ResetColor();                                   // 컬러를 초기화한다.
            Console.CursorVisible = false;                          // 커서를 숨긴다.
            Console.Title = "Sokoban " + VERSION;                   // 타이틀을 설정한다.
            Console.BackgroundColor = ConsoleColor.DarkBlue;        // 배경색을 설정한다.
            Console.ForegroundColor = ConsoleColor.Yellow;          // 글꼴색을 설정한다.
            Console.Clear();                                        // 출력된 모든 내용을 지운다.

            // 맵 설정
            // 가로 40, 세로 20
            const int MAP_RIGHT_END = 39, MAP_DOWN_END = 19, MAP_LEFT_END = 0, MAP_UP_END = 0;
            const int GOAL_COUNT = 3;
            const int BOX_COUNT = GOAL_COUNT;

            // 플레이어 설정
            const int INIT_PLAYER_X = 1, INIT_PLAYER_Y = 1;
            int playerX = INIT_PLAYER_X, playerY = INIT_PLAYER_Y;
            Direction playerDirection = Direction.None;
            const string PLAYER = "K";

            // 박스 설정
            int[] boxPositionsX = { 5, 22, 33 };
            int[] boxPositionsY = { 7, 10, 13 };
            int pushedBoxId = 0;
            const string BOX = "B";

            // 벽 설정
            int[] wallPositionsX = { 11, 16, 25 };
            int[] wallPositionsY = { 6, 11, 11 };
            const int WALL_COUNT = 3;
            const string WALL = "X";

            // 골인 지점
            int[] goalPositionsX = { 22, 8, 30 };
            int[] goalPositionsY = { 7, 14, 12 };
            const string GOAL = "G";
            const string GOALIN = "@";

            // 게임 루프 == 프레임(Frame)
            while (true)
            {
                // 이전 프레임을 지운다.
                Console.Clear();
                
                // -------------------------------------Render------------------------------------- 
                // 플레이어 출력하기
                Console.SetCursorPosition(playerX, playerY);
                Console.Write(PLAYER);

                // 벽 출력하기
                for (int wallId = 0; wallId < WALL_COUNT; ++wallId)
                {
                    Console.SetCursorPosition(wallPositionsX[wallId], wallPositionsY[wallId]);
                    Console.Write(WALL);
                }

                // 박스 출력하기
                for (int boxId = 0; boxId < BOX_COUNT; ++boxId)
                {
                    Console.SetCursorPosition(boxPositionsX[boxId], boxPositionsY[boxId]);
                    Console.Write(BOX);
                }

                // 골인 지점 출력하기
                for (int goalId = 0; goalId < GOAL_COUNT; ++goalId)
                {
                    for (int boxId = 0; boxId < BOX_COUNT; ++boxId)
                    {
                        if (goalPositionsX[goalId] == boxPositionsX[boxId] && goalPositionsY[goalId] == boxPositionsY[boxId])
                        {
                            Console.SetCursorPosition(goalPositionsX[goalId], goalPositionsY[goalId]);
                            Console.Write(GOALIN);
                            break;
                        }
                        else
                        {
                            Console.SetCursorPosition(goalPositionsX[goalId], goalPositionsY[goalId]);
                            Console.Write(GOAL);
                        }
                    }
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

                #region 박스 <=> 플레이어 상호작용
                // 박스 업데이트

                // 플레이어가 이동한 후
                for(int boxId = 0; boxId < BOX_COUNT; ++boxId)
                {
                    int boxX = boxPositionsX[boxId];
                    int boxY = boxPositionsY[boxId];
                    if(playerX == boxX && playerY == boxY)
                    {
                        pushedBoxId = boxId;
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
                        boxPositionsX[boxId] = boxX;
                        boxPositionsY[boxId] = boxY;
                        break;
                    }
                    
                }
                #endregion

                #region 박스 <=> 박스 상호작용
                for (int collisionBoxId = 0; collisionBoxId < BOX_COUNT; collisionBoxId++)
                {
                    int pushedBoxX = boxPositionsX[pushedBoxId];
                    int pushedBoxY = boxPositionsY[pushedBoxId];
                    if (pushedBoxId != collisionBoxId)
                    {
                        if (boxPositionsX[pushedBoxId] == boxPositionsX[collisionBoxId] && boxPositionsY[pushedBoxId] == boxPositionsY[collisionBoxId])
                        {
                            int collisionBoxX = boxPositionsX[collisionBoxId];
                            int collisionBoxY = boxPositionsY[collisionBoxId];
                            switch (playerDirection)
                            {
                                case Direction.Left:
                                    pushedBoxX = collisionBoxX + 1;
                                    playerX = pushedBoxX + 1;
                                    break;
                                case Direction.Right:
                                    pushedBoxX = collisionBoxX - 1;
                                    playerX = pushedBoxX - 1;
                                    break;
                                case Direction.Up:
                                    pushedBoxY = collisionBoxY + 1;
                                    playerY = pushedBoxY + 1;
                                    break;
                                case Direction.Down:
                                    pushedBoxY = collisionBoxY - 1;
                                    playerY = pushedBoxY - 1;
                                    break;
                                default:
                                    return;
                            }
                        }
                        boxPositionsX[pushedBoxId] = pushedBoxX;
                        boxPositionsY[pushedBoxId] = pushedBoxY;
                    }
                    
                }


                #endregion

                #region 플레이어 <=> 벽 상호작용
                // 벽 상호작용
                for (int wallId = 0; wallId < WALL_COUNT; ++wallId)
                {
                    if (playerX == wallPositionsX[wallId] && playerY == wallPositionsY[wallId])
                    {
                        switch (playerDirection)
                        {
                            case Direction.Right:
                                playerX = wallPositionsX[wallId] - 1;
                                break;
                            case Direction.Left:
                                playerX = wallPositionsX[wallId] + 1;
                                break;
                            case Direction.Up:
                                playerY = wallPositionsY[wallId] + 1;
                                break;
                            case Direction.Down:
                                playerY = wallPositionsY[wallId] - 1;
                                break;
                            default:
                                return;
                        }
                    }
                }
                
                #endregion

                #region 박스 <=> 벽 상호작용
                for(int boxId = 0; boxId < BOX_COUNT; ++boxId)
                {
                    for(int wallId = 0; wallId < WALL_COUNT; ++wallId)
                    {
                        if (boxPositionsX[boxId] == wallPositionsX[wallId] && boxPositionsY[boxId] == wallPositionsY[wallId])
                        {
                            switch(playerDirection)
                            {
                                case Direction.Right:
                                    boxPositionsX[boxId] = wallPositionsX[wallId] - 1;
                                    playerX = boxPositionsX[boxId] - 1;
                                    break;
                                case Direction.Left:
                                    boxPositionsX[boxId] = wallPositionsX[wallId] + 1;
                                    playerX = boxPositionsX[boxId] + 1;
                                    break;
                                case Direction.Up:
                                    boxPositionsY[boxId] = wallPositionsY[wallId] + 1;
                                    playerY = boxPositionsY[boxId] + 1;
                                    break;
                                case Direction.Down:
                                    boxPositionsY[boxId] = wallPositionsY[wallId] - 1;
                                    playerY = boxPositionsY[boxId] - 1;
                                    break;
                                default:
                                    return;
                            }
                        }
                    }
                }
                #endregion

                #region 골인 구현
                
                bool[] goaledArr = new bool[GOAL_COUNT];
                bool isGameClear = false;

                for (int goalId = 0; goalId < GOAL_COUNT; ++goalId)
                {
                    for(int boxId = 0; boxId < BOX_COUNT; ++boxId)
                    {
                        if (goalPositionsX[goalId] == boxPositionsX[boxId] && goalPositionsY[goalId] == boxPositionsY[boxId])
                        {
                            goaledArr[goalId] = true;
                            break;
                        }
                    }
                }

                for(int goalId = 0; goalId < GOAL_COUNT; ++goalId)
                {
                    if (goaledArr[goalId] == false)
                    {
                        isGameClear = false;
                        break;
                    }
                    else
                    {
                        isGameClear = true;
                    }
                }

                if(isGameClear == true)
                {
                    Console.Clear();
                    Console.WriteLine("GAME CLEAR!");
                    break;
                }

                #endregion
            }
        }
    }
}
