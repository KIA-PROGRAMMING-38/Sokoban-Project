using System;
using Sokoban;

namespace Sokoban
{
    
    class Program
    {
        static void Main(string[] arg)
        {
            // 초기 세팅
            Console.ResetColor();                           // 컬러를 초기화한다.
            Console.CursorVisible = false;                  // 커서를 숨긴다.
            Console.Title = "경이루 아카데미";               // 타이틀을 설정한다.
            Console.BackgroundColor = ConsoleColor.Magenta; // 배경색을 설정한다.
            Console.ForegroundColor = ConsoleColor.Yellow; // 글꼴색을 설정한다.
            Console.Clear();                               // 출력된 모든 내용을 지운다.


            Player player = new Player();
            Renderer renderer = new Renderer();

            const int OUTLINE_LENGTH_X = 33;
            const int OUTLINE_LENGTH_Y = 20;

            const string BOX_SYMBOL = "B";
            const string WALL_SYMBOL = "X";
            const string GOAL_SYMBOL = "O";
            const string MAP_OUTLINE_SYMBOL = "#";
            const string GOALIN_SYMBOL = "@";

            const int TOTAL_BOX_NUM = 3;
            const int BOX1_INITIAL_X = 15;
            const int BOX1_INITIAL_Y = 7;
            const int BOX2_INITIAL_X = 10;
            const int BOX2_INITIAL_Y = 12;
            const int BOX3_INITIAL_X = 17;
            const int BOX3_INITIAL_Y = 14;

            const int TOTAL_WALL_NUM = 3;
            const int WALL1_INITIAL_X = 13;
            const int WALL1_INITIAL_Y = 10;
            const int WALL2_INITIAL_X = 27;
            const int WALL2_INITIAL_Y = 19;
            const int WALL3_INITIAL_X = 19;
            const int WALL3_INITIAL_Y = 9;

            const int TOTAL_GOAL_NUM = 3;
            const int GOAL1_INITIAL_X = 25;
            const int GOAL1_INITIAL_Y = 17;
            const int GOAL2_INITIAL_X = 20;
            const int GOAL2_INITIAL_Y = 12;
            const int GOAL3_INITIAL_X = 20;
            const int GOAL3_INITIAL_Y = 17;

            int[] boxX = { BOX1_INITIAL_X, BOX2_INITIAL_X, BOX3_INITIAL_X };
            int[] boxY = { BOX1_INITIAL_Y, BOX2_INITIAL_Y, BOX3_INITIAL_Y };

            int[] wallX = { WALL1_INITIAL_X, WALL2_INITIAL_X, WALL3_INITIAL_X };
            int[] wallY = { WALL1_INITIAL_Y, WALL2_INITIAL_Y, WALL3_INITIAL_Y };

            int[] goalX = { GOAL1_INITIAL_X, GOAL2_INITIAL_X, GOAL3_INITIAL_X };
            int[] goalY = { GOAL1_INITIAL_Y, GOAL2_INITIAL_Y, GOAL3_INITIAL_Y };

            bool[] isBoxOnGoal = new bool[TOTAL_BOX_NUM];


            bool isGameDoing = true;
            // 게임 루프 == 프레임(Frame)
            while (isGameDoing)
            {
                // -------------------------------------- Render ------------------------------------------------
                Console.Clear();

                //플레이어
                renderer.Render(player.GetX(), player.GetY(), player.GetSymbol());
                //골
                for (int goalId = 0; goalId < TOTAL_GOAL_NUM; ++goalId)
                {
                    Console.SetCursorPosition(goalX[goalId], goalY[goalId]);
                    Console.Write(GOAL_SYMBOL);
                }

                //박스
                for (int boxId = 0; boxId < TOTAL_BOX_NUM; ++boxId)
                {
                    Console.SetCursorPosition(boxX[boxId], boxY[boxId]);
                    //골에 박스
                    if (isBoxOnGoal[boxId])
                    {
                        Console.Write(GOALIN_SYMBOL);
                    }
                    else
                    {
                        Console.Write(BOX_SYMBOL);
                    }
                }

                //벽
                for (int wallId = 0; wallId < TOTAL_WALL_NUM; ++wallId)
                {
                    Console.SetCursorPosition(wallX[wallId], wallY[wallId]);
                    Console.Write(WALL_SYMBOL);
                }


                //맵 테두리
                for (int i = -1; i <= OUTLINE_LENGTH_X; ++i)
                {
                    Console.SetCursorPosition(Game.MAP_MIN_X + i, Game.MAP_MIN_Y - 1);
                    Console.Write(MAP_OUTLINE_SYMBOL);
                    Console.SetCursorPosition(Game.MAP_MIN_X + i, Game.MAP_MAX_Y + 1);
                    Console.Write(MAP_OUTLINE_SYMBOL);
                }
                for (int i = 0; i <= OUTLINE_LENGTH_Y; ++i)
                {
                    Console.SetCursorPosition(Game.MAP_MIN_X - 1, Game.MAP_MIN_Y + i);
                    Console.Write(MAP_OUTLINE_SYMBOL);
                    Console.SetCursorPosition(Game.MAP_MAX_X + 1, Game.MAP_MIN_Y + i);
                    Console.Write(MAP_OUTLINE_SYMBOL);
                }
                // -------------------------------------- ProcessInput ------------------------------------------------
                ConsoleKey playerKey = Console.ReadKey().Key; // ConsoleKeyInfo keyInfo = Console.ReadKey(); ConsoleKey playerKey = keyInfo.Key;
                // -------------------------------------- Update ------------------------------------------------

                // 플레이어

                // 플레이어의 이동
                player.Move(playerKey);

                // 박스
                for (int boxId = 0; boxId < TOTAL_BOX_NUM; ++boxId)
                {
                    // 박스에 플레이어
                    if (player.GetX() == boxX[boxId] && player.GetY() == boxY[boxId])
                    {
                        switch (player.GetMoveDirection())
                        {
                            case Player.Direction.Up:
                                boxY[boxId] = Math.Max(Game.MAP_MIN_Y, --boxY[boxId]);
                                player.SetY(boxY[boxId] + 1);
                                break;
                            case Player.Direction.Down:
                                boxY[boxId] = Math.Min(++boxY[boxId], Game.MAP_MAX_Y);
                                player.SetY(boxY[boxId] - 1);
                                break;
                            case Player.Direction.Left:
                                boxX[boxId] = Math.Max(Game.MAP_MIN_X, --boxX[boxId]);
                                player.SetX(boxX[boxId] + 1);
                                break;
                            case Player.Direction.Right:
                                boxX[boxId] = Math.Min(++boxX[boxId], Game.MAP_MAX_X);
                                player.SetX(boxX[boxId] - 1);
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다.");

                                return;
                        }
                    }
                    // 박스에 박스
                    for (int collidedBoxId = 0; collidedBoxId < TOTAL_BOX_NUM; ++collidedBoxId)
                    {
                        if (boxId == collidedBoxId)
                        {
                            continue;
                        }
                        if (boxX[boxId] == boxX[collidedBoxId] && boxY[boxId] == boxY[collidedBoxId])
                        {
                            switch (player.GetMoveDirection())
                            {
                                case Player.Direction.Up:
                                    boxY[boxId] = boxY[collidedBoxId] + 1;
                                    player.SetY(boxY[boxId] + 1);
                                    break;
                                case Player.Direction.Down:
                                    boxY[boxId] = boxY[collidedBoxId] - 1;
                                    player.SetY(boxY[boxId] - 1);
                                    break;
                                case Player.Direction.Left:
                                    boxX[boxId] = boxX[collidedBoxId] + 1;
                                    player.SetX(boxX[boxId] + 1);
                                    break;
                                case Player.Direction.Right:
                                    boxX[boxId] = boxX[collidedBoxId] - 1;
                                    player.SetX(boxX[boxId] - 1);
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다.");

                                    return;
                            }
                            break;
                        }
                    }
                }

                // 벽
                for (int wallId = 0; wallId < TOTAL_WALL_NUM; ++wallId)
                {
                    // 벽에 플레이어
                    if (player.GetX() == wallX[wallId] && player.GetY() == wallY[wallId])
                    {
                        switch (player.GetMoveDirection())
                        {
                            case Player.Direction.Up:
                                player.SetY(wallY[wallId] + 1);
                                break;
                            case Player.Direction.Down:
                                player.SetY(wallY[wallId] - 1);
                                break;
                            case Player.Direction.Left:
                                player.SetX(wallX[wallId] + 1);
                                break;
                            case Player.Direction.Right:
                                player.SetX(wallX[wallId] - 1);
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다.");

                                return;
                        }
                        break;
                    }
                    // 벽에 박스
                    for (int boxId = 0; boxId < TOTAL_BOX_NUM; ++boxId)
                    {
                        if (boxX[boxId] == wallX[wallId] && boxY[boxId] == wallY[wallId])
                        {
                            switch (player.GetMoveDirection())
                            {
                                case Player.Direction.Up:
                                    boxY[boxId] = wallY[wallId] + 1;
                                    player.SetY(boxY[boxId] + 1);
                                    break;
                                case Player.Direction.Down:
                                    boxY[boxId] = wallY[wallId] - 1;
                                    player.SetY(boxY[boxId] - 1);
                                    break;
                                case Player.Direction.Left:
                                    boxX[boxId] = wallX[wallId] + 1;
                                    player.SetX(boxX[boxId] + 1);
                                    break;
                                case Player.Direction.Right:
                                    boxX[boxId] = wallX[wallId] - 1;
                                    player.SetX(boxX[boxId] - 1);
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다.");

                                    return;
                            }
                            break;
                        }
                    }
                }

                // 골
                int goalCount = 0;
                for (int boxId = 0; boxId < TOTAL_BOX_NUM; ++boxId)
                {
                    isBoxOnGoal[boxId] = false;
                    for (int goalId = 0; goalId < TOTAL_GOAL_NUM; ++goalId)
                    {
                        if (boxX[boxId] == goalX[goalId] && boxY[boxId] == goalY[goalId])
                        {
                            ++goalCount;
                            isBoxOnGoal[boxId] = true;
                            break;
                        }
                    }
                }
                if (goalCount == TOTAL_GOAL_NUM)
                {
                    isGameDoing = false;
                }
            }
            Console.ResetColor();
            Console.Clear();
            Console.WriteLine("Game Clear!");
        }
    }
}