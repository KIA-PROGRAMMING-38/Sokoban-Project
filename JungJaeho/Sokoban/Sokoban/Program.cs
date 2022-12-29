using System.Text;
namespace JJH_SOKOBAN
{
    enum Direction
    {
        None,
        Left,
        Right,
        Up,
        Down,
    }
    class Sokoban
    {
        public static void Main(string[] args)
        {

            #region 초기세팅
            Console.ResetColor();                               // 컬러 초기화
            Console.CursorVisible = false;                      // 커서 비활성화
            Console.Title = "소코반";                     // 타이틀 설정
            Console.BackgroundColor = ConsoleColor.Black;    // 배경색 설정
            Console.ForegroundColor = ConsoleColor.DarkCyan;    // 전경색 설정
            Console.Clear();                                    // 콘솔창 클리어
            /*-------------------------------------------------*/
            #endregion

            #region 맵세팅
            ConsoleColor defaultForegroundColor = Console.ForegroundColor;
            const ConsoleColor WALL_COLOR = ConsoleColor.Red;
            const char WALL_ICON = 'Π';
            const int WallOffset = 1;
            const int MAP_MIN_X = 0;
            const int MAP_MIN_Y = 0;
            const int MAP_MAX_X = 35;
            const int MAP_MAX_Y = 20;
            #endregion

            #region Player
            Direction playerDir = Direction.None;
            const ConsoleColor PLAYER_COLOR = ConsoleColor.DarkCyan;
            const int INITIAL_PLAYER_X = 2;
            const int INITIAL_PLAYER_Y = 2;
            const char PLAYER_ICON = 'Д';
            int playerX = INITIAL_PLAYER_X;
            int playerY = INITIAL_PLAYER_Y;
            int prevPlayerX = 0;
            int prevPlayerY = 0;
            #endregion

            #region Box
            const ConsoleColor BOX_COLOR = ConsoleColor.White;
            const int INITIAL_BOX_X = 5;
            const int INITIAL_BOX_Y = 3;
            const char BOX_ICON = '■'; //▥ Д
            int prevBoxPosX = 0;
            int prevBoxPosY = 0;
            int[] boxPosX = { INITIAL_BOX_X, INITIAL_BOX_X + 3, INITIAL_BOX_X + 5 };
            int[] boxPosY = { INITIAL_BOX_Y, INITIAL_BOX_Y + 3, INITIAL_BOX_Y + 4 };
            #endregion

            #region Wall
            int[] WallPosX = { 7, 9, 10 };
            int[] WallPosY = { 10, 17, 4 };
            #endregion

            #region Goal
            const ConsoleColor GOAL_COLOR = ConsoleColor.White;
            int[] GoalPosX = { 15, 20, 8 };
            int[] GoalPosY = { 3, 10, 7 };
            const char GOAL_ICON = 'S'; //▥ Д
            #endregion

            bool isGameOver = false;
            int GoalInCount = 0;

            #region 게임 루프
            while (true)
            {
                Console.Clear();
                #region Render
                /*---------------------- Render ---------------------- */
                Console.ForegroundColor = WALL_COLOR;
                // 벽
                for (int i = 0; i <= MAP_MAX_X; i++)
                {
                    Console.SetCursorPosition(i, MAP_MIN_Y);
                    Console.Write(WALL_ICON);
                    Console.SetCursorPosition(i, MAP_MAX_Y);
                    Console.Write(WALL_ICON);
                }
                for (int i = 0; i <= MAP_MAX_Y; i++)
                {
                    Console.SetCursorPosition(MAP_MIN_X, i);
                    Console.Write(WALL_ICON);
                    Console.SetCursorPosition(MAP_MAX_X, i);
                    Console.Write(WALL_ICON);
                }

                for (int i = 0; i < WallPosX.Length; ++i)
                {
                    Console.SetCursorPosition(WallPosX[i], WallPosY[i]);
                    Console.ForegroundColor = WALL_COLOR;
                    Console.Write(WALL_ICON);
                }

                for (int i = 0; i < GoalPosX.Length; ++i)
                {
                    Console.SetCursorPosition(GoalPosX[i], GoalPosY[i]);
                    Console.ForegroundColor = GOAL_COLOR;
                    Console.Write(GOAL_ICON);
                }

                Console.SetCursorPosition(playerX, playerY);
                Console.ForegroundColor = PLAYER_COLOR;
                Console.Write(PLAYER_ICON);

                for (int i = 0; i < boxPosX.Length; ++i)
                {
                    Console.SetCursorPosition(boxPosX[i], boxPosY[i]);
                    Console.ForegroundColor = BOX_COLOR;
                    Console.Write(BOX_ICON);
                }


                Console.ForegroundColor = defaultForegroundColor;

                // 게임 오버 처리
                if (isGameOver)
                {
                    Thread.Sleep(1000);
                    Console.Clear();
                    Console.WriteLine("Game Over!");
                    break;
                }
                #endregion

                #region ProcessInput
                /*------------------- ProcessInput ------------------- */
                ConsoleKey key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.RightArrow:
                        playerDir = Direction.Right;
                        break;
                    case ConsoleKey.LeftArrow:
                        playerDir = Direction.Left;
                        break;
                    case ConsoleKey.UpArrow:
                        playerDir = Direction.Up;
                        break;
                    case ConsoleKey.DownArrow:
                        playerDir = Direction.Down;
                        break;
                }
                #endregion

                #region Update
                /*---------------------- Update ---------------------- */


                // Player
                prevPlayerX = playerX;
                prevPlayerY = playerY;

                switch (playerDir)
                {
                    case Direction.Left:
                        playerX = Math.Max(playerX - 1, MAP_MIN_X + WallOffset);
                        break;
                    case Direction.Right:
                        playerX = Math.Min(playerX + 1, MAP_MAX_X - WallOffset);
                        break;
                    case Direction.Up:
                        playerY = Math.Max(playerY - 1, MAP_MIN_Y + WallOffset);
                        break;
                    case Direction.Down:
                        playerY = Math.Min(playerY + 1, MAP_MAX_Y - WallOffset);
                        break;
                }

                for (int i = 0; i < WallPosX.Length; ++i)
                {
                    if (playerX == WallPosX[i] && playerY == WallPosY[i])
                    {
                        playerX = prevPlayerX;
                        playerY = prevPlayerY;
                    }
                }
                // BOX
                for (int i = 0; i < boxPosX.Length; i++)
                {
                    prevBoxPosX = boxPosX[i];
                    prevBoxPosY = boxPosY[i];

                    if (playerX == boxPosX[i] && playerY == boxPosY[i])
                    {
                        // 방향
                        boxPosX[i] += playerX - prevPlayerX;
                        boxPosY[i] += playerY - prevPlayerY;


                        // 박스 범위 나갈 때
                        if (boxPosX[i] > MAP_MAX_X - WallOffset || boxPosY[i] > MAP_MAX_Y - WallOffset || boxPosY[i] < MAP_MIN_Y + WallOffset || boxPosX[i] < MAP_MIN_X + WallOffset)
                        {
                            boxPosX[i] = prevBoxPosX;
                            boxPosY[i] = prevBoxPosY;
                            playerX = prevPlayerX;
                            playerY = prevPlayerY;
                        }
                        // 박스앞에 박스 있으면
                        for (int j = 0; j < boxPosX.Length; j++)
                        {
                            if (i == j) continue;
                            if (boxPosX[j] == boxPosX[i] && boxPosY[j] == boxPosY[i])
                            {
                                boxPosX[i] = prevBoxPosX; boxPosY[i] = prevBoxPosY;
                                playerX = prevPlayerX;
                                playerY = prevPlayerY;
                                break;
                            }
                        }
                    }
                    for (int j = 0; j < WallPosX.Length; ++j)
                    {
                        if (WallPosX[j] == boxPosX[i] && WallPosY[j] == boxPosY[i])
                        {
                            boxPosX[i] = prevBoxPosX;
                            boxPosY[i] = prevBoxPosY;
                            playerX = prevPlayerX;
                            playerY = prevPlayerY;
                        }
                    }
                    #endregion
                }
                // 게임 끝난지 체크
                GoalInCount = 0;
                for (int i = 0; i < boxPosX.Length; ++i)
                {
                    for (int j = 0; j < GoalPosX.Length; ++j)
                    {
                        if (boxPosX[i] == GoalPosX[j] && boxPosY[i] == GoalPosY[j])
                        {
                            GoalInCount++;
                        }
                    }
                }
                if (GoalInCount == GoalPosX.Length)
                {
                    isGameOver = true;
                }
                #endregion

            }

        }
    }
}
