using System.ComponentModel;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Text.Encodings.Web;
using System.Xml;

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
            const int MAP_MAX_X = 20;
            const int MAP_MAX_Y = 12;

            const int INITIAL_PLAYER_X = 14;
            const int INITIAL_PLAYER_Y = 2;

            const char PLAYER_SYMBOL = '●';
            const char GOAL_SYMBOL = '□';
            const char BOX_SYMBOL = '■';
            const char WALL_SYMBOL = '#';
            const char GOAL_IN_SYMBOL = '▣';

            int playerX = INITIAL_PLAYER_X;
            int playerY = INITIAL_PLAYER_Y;

            int[] boxX = new int[3] { 3, 6, 9 };
            int[] boxY = new int[3] { 3, 6, 9 };

            int[] wallX = new int[3] { 1, 2, 3 };
            int[] wallY = new int[3] { 4, 5, 6 };

            int[] goalX = new int[3] { 13, 14, 15 };
            int[] goalY = new int[3] { 9, 9, 9 };

            int boxLength = boxX.Length;
            int wallLength = wallX.Length;
            int goalLength = goalX.Length;

            int pushBoxId = default;
            int boxCount = default;

            bool[] isBoxOnGoal = new bool[boxLength];
            bool clearJudge = true;
           

            PLAYER_DIRECTION playerDir = new PLAYER_DIRECTION();

            // 게임 루프 == 프레임(Frame)
            while (clearJudge)
            {
                // --------------------------------------------- Render -------------------------------------------------------

                Render();
                
                
                void Render()
                {
                    MapRender();
                    GoalRender();
                    WallRender();
                    PlayerRender();
                    ChangeRender();
                }
                void MapRender()
                {
                    Console.Clear();
                    Console.WriteLine("######################");

                    for (int i = 0; i <= MAP_MAX_Y - 1; i++)
                    {
                        Console.WriteLine("#                    #");

                    }
                    for (int j = 0; j <= MAP_MAX_X + 1; j++)
                    {

                        Console.Write("#");
                    }
                }

                void GoalRender()
                {
                    for (int goalId = 0; goalId < goalLength; goalId++)
                    {
                        Console.SetCursorPosition(goalX[goalId], goalY[goalId]);
                        Console.Write(GOAL_SYMBOL);
                    }
                }

                void WallRender()
                {
                    for (int wallId = 0; wallId < wallLength; wallId++)
                    {
                        Console.SetCursorPosition(wallX[wallId], wallY[wallId]);
                        Console.Write(WALL_SYMBOL);
                    }
                }

                void ChangeRender()
                {
                    for (int boxId = 0; boxId < boxLength; boxId++)
                    {
                        Console.SetCursorPosition(boxX[boxId], boxY[boxId]);

                        if (isBoxOnGoal[boxId])
                        {
                            Console.Write(GOAL_IN_SYMBOL);
                        }
                        else
                        {
                            Console.Write(BOX_SYMBOL);
                        }
                    }
                }

                void PlayerRender()
                {
                    Console.SetCursorPosition(playerX, playerY);
                    Console.Write(PLAYER_SYMBOL);
                }

                // --------------------------------------------- ProcessInput -------------------------------------------------
                ConsoleKey key = Console.ReadKey().Key;
                // --------------------------------------------- Update -------------------------------------------------------
                MoveRight();
                MoveLeft();
                MoveDown();
                MoveUp();

                WithPlayerBox ();
                WithBoxWall();
                WithPlayerWall();
                WithBoxBox();

                GoalInJudge();
                JudgeClear();

                void MoveRight()
                {
                    if (key == ConsoleKey.RightArrow) 
                    {
                        playerX = Min(playerX + 1, MAP_MAX_X);
                        playerDir = PLAYER_DIRECTION.RIGHT;
                    }
                }

                void MoveLeft()
                {
                    if (key == ConsoleKey.LeftArrow)
                    {
                        playerX = Max(playerX - 1, MAP_MIN_X + 1);
                        playerDir = PLAYER_DIRECTION.LEFT;
                    }
                }

                void MoveUp()
                {
                    if (key == ConsoleKey.UpArrow)
                    {
                        playerY = Max(playerY - 1, MAP_MIN_Y + 1);
                        playerDir = PLAYER_DIRECTION.UP;
                    }// 이동 부
                }

                void MoveDown()
                {
                    if (key == ConsoleKey.DownArrow)
                    {
                        playerY = Min(playerY + 1, MAP_MAX_Y);
                        playerDir = PLAYER_DIRECTION.DOWN;
                    }
                }

                void WithPlayerBox()
                {
                    for (int boxId = 0; boxId < boxLength; boxId++)
                    {
                        if (playerX == boxX[boxId] && playerY == boxY[boxId]) // 외곽 벽을 만났을 떄
                        {
                            pushBoxId = boxId;
                            switch (playerDir)
                            {
                                case PLAYER_DIRECTION.RIGHT: //right
                                    playerX = Min(playerX, MAP_MAX_X - 1);
                                    boxX[boxId] = Min(boxX[boxId] + 1, MAP_MAX_X);
                                    break;
                                case PLAYER_DIRECTION.LEFT: //left
                                    playerX = Max(playerX, MAP_MIN_X + 2);
                                    boxX[boxId] = Max(boxX[boxId] - 1, MAP_MIN_X + 1);
                                    break;
                                case PLAYER_DIRECTION.DOWN: //down
                                    playerY = Min(playerY, MAP_MAX_Y - 1);
                                    boxY[boxId] = Min(boxY[boxId] + 1, MAP_MAX_Y);
                                    break;
                                case PLAYER_DIRECTION.UP: //up
                                    playerY = Max(playerY, MAP_MIN_Y + 2);
                                    boxY[boxId] = Max(boxY[boxId] - 1, MAP_MIN_Y + 1);
                                    break;

                            }
                        }
                    } // 외곽 벽을 만났을 떄
                }

                void WithPlayerWall()
                {
                    for (int wallId = 0; wallId < wallLength; wallId++) //벽과 플레이어
                    {
                        if (playerX == wallX[wallId] && playerY == wallY[wallId])
                        {
                            switch (playerDir)
                            {
                                case PLAYER_DIRECTION.RIGHT: //right
                                    playerX = wallX[wallId] - 1;
                                    break;
                                case PLAYER_DIRECTION.LEFT: //left
                                    playerX = wallX[wallId] + 1;
                                    break;
                                case PLAYER_DIRECTION.DOWN: //down
                                    playerY = wallY[wallId] - 1;
                                    break;
                                case PLAYER_DIRECTION.UP: //up
                                    playerY = wallY[wallId] + 1;
                                    break;
                            }
                        }
                    } //벽과 플레이어
                }

                void WithBoxWall()
                {
                    for (int boxId = 0; boxId < boxLength; boxId++) //벽과 박스
                    {
                        for (int wallId = 0; wallId < wallLength; wallId++)
                        {
                            if (boxX[boxId] == wallX[wallId] && boxY[boxId] == wallY[wallId])
                            {
                                switch (playerDir)
                                {
                                    case PLAYER_DIRECTION.RIGHT: //right
                                        playerX = wallX[wallId] - 2;
                                        boxX[boxId] = wallX[wallId] - 1;
                                        break;
                                    case PLAYER_DIRECTION.LEFT: //left
                                        playerX = wallX[wallId] + 2;
                                        boxX[boxId] = wallX[wallId] + 1;
                                        break;
                                    case PLAYER_DIRECTION.DOWN: //down
                                        playerY = wallY[wallId] - 2;
                                        boxY[boxId] = wallY[wallId] - 1;
                                        break;
                                    case PLAYER_DIRECTION.UP: //up
                                        playerY = wallY[wallId] + 2;
                                        boxY[boxId] = wallY[wallId] + 1;
                                        break;
                                }
                            }
                        }
                    } //벽과 박스
                }

                void WithBoxBox()
                {
                    for (int boxId = 0; boxId < boxLength; boxId++) //박스와 박스 충돌
                    {
                        for (int boxId2 = 0; boxId2 < boxLength; boxId2++)
                        {
                            if (boxId == boxId2)
                            {
                                continue;
                            }
                            if (boxX[boxId] == boxX[boxId2] && boxY[boxId] == boxY[boxId2] && pushBoxId == boxId)
                            {
                                switch (playerDir)
                                {
                                    case PLAYER_DIRECTION.RIGHT: //right
                                        playerX = playerX - 1;
                                        boxX[boxId2] = playerX + 2;
                                        boxX[boxId] = playerX + 1;
                                        break;
                                    case PLAYER_DIRECTION.LEFT: //left
                                        playerX = playerX + 1;
                                        boxX[boxId2] = playerX - 2;
                                        boxX[boxId] = playerX - 1;
                                        break;
                                    case PLAYER_DIRECTION.DOWN: //down
                                        playerY = playerY - 1;
                                        boxY[boxId2] = playerY + 2;
                                        boxY[boxId] = playerY + 1;
                                        break;
                                    case PLAYER_DIRECTION.UP: //up
                                        playerY = playerY + 1;
                                        boxY[boxId2] = playerY - 2;
                                        boxY[boxId] = playerY - 1;
                                        break;
                                }
                            }
                        }
                    } //박스와 박스 충돌
                }

                void GoalInJudge()
                {
                    boxCount = 0;

                    for (int boxId = 0; boxId < boxLength; boxId++) // 골인 판정
                    {
                        isBoxOnGoal[boxId] = false;

                        for (int goalId = 0; goalId < goalLength; goalId++)
                        {
                            if (boxX[boxId] == goalX[goalId] && boxY[boxId] == goalY[goalId])
                            {

                                boxCount++;
                                isBoxOnGoal[boxId] = true;

                                break;
                            }
                        }
                    }
                }
               
                void JudgeClear()
                {
                    if (boxCount == goalLength) // 클리어 판정
                    {
                        Console.Clear();
                        Console.WriteLine("Clear!");
                        clearJudge = false;
                    }
                }

                int Max(int a, int b)
                {
                    if (a < b)
                    {
                        return b;
                    }
                    else
                    {
                        return a;
                    }
                }

                int Min(int a, int b) => a < b ? a : b;
            }
        }   
    }
}