using System.ComponentModel;

namespace sokoban
{
    enum PLAYER_DIRECTION
    {
        RIGHT,
        LEFT,
        DOWN,
        UP
    }
    class Program
    {
        static void Main()
        {
            //초기 세팅
            Console.ResetColor();                            // 컬러를 초기화 한다
            Console.CursorVisible = false;                   // 커서를 숨긴다
            Console.Title = "홍성재의 썬더펀치";               // 타이틀을 설정한다.
            Console.BackgroundColor = ConsoleColor.DarkGray; //배경색을 설정한다.
            Console.ForegroundColor = ConsoleColor.DarkBlue; //글꼴색을 설정한다.
            Console.Clear();                                 //출력된 모든 내용을 지운다.

            // 기호 상수
            const int MAP_MIN_X = 0;
            const int MAP_MIN_Y = 0;
            const int MAP_MAX_X = 15;
            const int MAP_MAX_Y = 10;
            const int INITIAL_PLAYER_X = 2;
            const int INITIAL_PLAYER_Y = 2;
            const char PLAYER_SYMBOL = 'P';
            const char GOAL_SYMBOL = '○';
            const char BOX_SYMBOL = '●';
   
            int playerX = INITIAL_PLAYER_X;
            int playerY = INITIAL_PLAYER_Y;
            int[] boxX = new int[3] { 3, 5, 8 };
            int[] boxY = new int[3] { 3, 4, 8 };
            int[] wallX = new int[3] { 15, 14, 13 };
            int[] wallY = new int[3] { 8, 8, 8 };
            int[] goalX = new int[3] { 5, 8, 9 };
            int[] goalY = new int[3] { 9, 2, 9 };
            int boxLength = boxX.Length;
            int wallLength = wallX.Length;
            int goalLength = goalX.Length;
            int pushBoxId = default;

            bool[] goalCount = new bool[goalLength + 1];
            goalCount[goalLength] = true;
            int goalJudge = default;

            PLAYER_DIRECTION playerDir = new PLAYER_DIRECTION();

            //15 X 10
            // 게임 루프 == 프레임(Frame)
            while (true)
            {
                Console.Clear();
                Console.WriteLine("#################");
                for (int i = 0; i <= MAP_MAX_Y - 1; i++)
                {
                    Console.WriteLine("#               #");
                   
                }
                for (int j = 0; j <= MAP_MAX_X + 1; j++)
                {

                    Console.Write("#");
                }

                // --------------------------------------------- Render -------------------------------------------------------
                // 플레이어 출력하기
                for (int i = 0; i < goalLength; i++)
                {
                    Console.SetCursorPosition(goalX[i], goalY[i]);
                    Console.Write("○");
                }
             
                for (int i = 0; i < wallLength; i++)
                {
                    Console.SetCursorPosition(wallX[i], wallY[i]);
                    Console.Write("#");
                }
                for (int i = 0; i < boxLength; i++)
                {
                    Console.SetCursorPosition(boxX[i], boxY[i]);
                    Console.Write("●");
                }
                

                Console.SetCursorPosition(playerX, playerY);
                Console.Write(PLAYER_SYMBOL);

                // --------------------------------------------- ProcessInput -------------------------------------------------
                ConsoleKey key = Console.ReadKey().Key;
                // --------------------------------------------- Update -------------------------------------------------------
                // 오른쪽 화살표키를
                if (key == ConsoleKey.RightArrow)  //이동 부
                {
                    playerX = Math.Min(playerX + 1, MAP_MAX_X);
                    playerDir = PLAYER_DIRECTION.RIGHT;
                } 
                if (key == ConsoleKey.LeftArrow)
                {
                    playerX = Math.Max(playerX - 1, MAP_MIN_X + 1);
                    playerDir = PLAYER_DIRECTION.LEFT;
                }
                if (key == ConsoleKey.DownArrow)
                {
                    playerY = Math.Min(playerY + 1, MAP_MAX_Y);
                    playerDir = PLAYER_DIRECTION.DOWN;
                }
                if (key == ConsoleKey.UpArrow)
                {
                    playerY = Math.Max(playerY - 1, MAP_MIN_Y + 1);
                    playerDir = PLAYER_DIRECTION.UP;
                }
                for (int i = 0; i < boxLength; i++)
                {
                    if (playerX == boxX[i] && playerY == boxY[i]) // 외곽 벽을 만났을 떄
                    {
                        pushBoxId = i;
                        switch (playerDir)
                        {
                            case PLAYER_DIRECTION.RIGHT: //right
                                playerX = Math.Min(playerX, MAP_MAX_X - 1);
                                boxX[i] = Math.Min(boxX[i] + 1, MAP_MAX_X);
                                break;
                            case PLAYER_DIRECTION.LEFT: //left
                                playerX = Math.Max(playerX, MAP_MIN_X + 2);
                                boxX[i] = Math.Max(boxX[i] - 1, MAP_MIN_X + 1);
                                break;
                            case PLAYER_DIRECTION.DOWN: //down
                                playerY = Math.Min(playerY, MAP_MAX_Y - 1);
                                boxY[i] = Math.Min(boxY[i] + 1, MAP_MAX_Y);
                                break;
                            case PLAYER_DIRECTION.UP: //up
                                playerY = Math.Max(playerY, MAP_MIN_Y + 2);
                                boxY[i] = Math.Max(boxY[i] - 1, MAP_MIN_Y + 1);
                                break;

                        }
                    }
                } // 외곽 벽을 만났을 떄
                for (int i = 0; i < wallLength; i++) //벽과 플레이어
                {
                    if (playerX == wallX[i] && playerY == wallY[i]) 
                    {
                        switch (playerDir)
                        {
                            case PLAYER_DIRECTION.RIGHT: //right
                                playerX = wallX[i] - 1;
                                break;
                            case PLAYER_DIRECTION.LEFT: //left
                                playerX = wallX[i] + 1;
                                break;
                            case PLAYER_DIRECTION.DOWN: //down
                                playerY = wallY[i] - 1;
                                break;
                            case PLAYER_DIRECTION.UP: //up
                                playerY = wallY[i] + 1;
                                break;
                        }
                    }
                } //벽과 플레이어
                for (int i = 0; i < boxLength; i++) //벽과 박스
                {
                    for (int j = 0; j < wallLength; j++)
                    {
                        if (boxX[i] == wallX[j] && boxY[i] == wallY[j]) 
                        {
                            switch (playerDir)
                            {
                                case PLAYER_DIRECTION.RIGHT: //right
                                    playerX = Math.Min(playerX, wallX[j] - 2);
                                    boxX[i] = Math.Min(boxX[i] + 1, wallX[j] - 1);
                                    break;
                                case PLAYER_DIRECTION.LEFT: //left
                                    playerX = Math.Max(playerX, wallX[j] + 2);
                                    boxX[i] = Math.Min(boxX[i] + 1, wallX[j] + 1);
                                    break;
                                case PLAYER_DIRECTION.DOWN: //down
                                    playerY = Math.Min(playerY, wallY[j] - 2);
                                    boxY[i] = Math.Min(boxY[i] - 1, wallY[j] - 1);
                                    break;
                                case PLAYER_DIRECTION.UP: //up
                                    playerY = Math.Max(playerY, wallY[j] + 2);
                                    boxY[i] = Math.Max(boxY[i] - 1, wallY[j] + 1);
                                    break;
                            }
                        }
                    }
                } //벽과 박스
                for (int i = 0; i < boxLength; i++) //박스와 박스 충돌
                {
                    for (int j = 0; j < boxLength; j++)
                    {
                        if (i == j)
                        {
                            continue;
                        }
                        if (boxX[i] == boxX[j] && boxY[i] == boxY[j] && pushBoxId == i) 
                        {
                            switch (playerDir)
                            {
                                case PLAYER_DIRECTION.RIGHT: //right
                                    playerX = playerX - 1;
                                    boxX[j] = playerX + 2;
                                    boxX[i] = playerX + 1;
                                    break;
                                case PLAYER_DIRECTION.LEFT: //left
                                    playerX = playerX + 1;
                                    boxX[j] = playerX - 2;
                                    boxX[i] = playerX - 1;
                                    break;
                                case PLAYER_DIRECTION.DOWN: //down
                                    playerY = playerY - 1;
                                    boxY[j] = playerY + 2;
                                    boxY[i] = playerY + 1;
                                    break;
                                case PLAYER_DIRECTION.UP: //up
                                    playerY = playerY + 1;
                                    boxY[j] = playerY - 2;
                                    boxY[i] = playerY - 1;
                                    break;
                            }
                        }
                    }
                } //박스와 박스 충돌



                for (int i = 0; i < boxLength; i++)
                {
                    for (int j = 0; j < boxLength; j++)
                    {
                        if (boxX[i] == goalX[j] && boxY[i] == goalY[j])
                        {
                            goalCount[j] = true;
                        }
                    }
                }
               if (!(Array.Exists(goalCount,x => false)))
                {
                    Console.Clear();
                    Console.WriteLine("Clear!");

                    return;
                }
            }
        }
    }
}