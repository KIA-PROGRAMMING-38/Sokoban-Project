using System;
using System.Reflection.Metadata;
using System.Security.Cryptography;

namespace Rekonban
{
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
            // 기초 세팅
            Console.ResetColor();
            Console.CursorVisible = false;
            Console.Title = "능지게임";
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Clear();

            // 기호 상수 정의
            const int MAP_MIN_X = 0;
            const int MAP_MAX_X = 15;
            const int MAP_MIN_Y = 0;
            const int MAP_MAX_Y = 13;

            const int GOAL_X = 14;
            const int GOAL_Y = 7;

            // 플레이어의 위치 좌표
            int playerX = 14;
            int playerY = 12;

            // 플레이어의 이동 방향
            Direction playerDirection = Direction.None; // 0:None / 1:Left / 2:Right / 3:Up / 4:Down

            // 벽의 좌표
            int[] wallPositionsX = { 9, 4, 10, 2, 1, 9, 14, 7, 3, 14, 8, 6, 10, 13 };
            int[] wallPositionsY = { 1, 2, 3, 4, 5, 5, 6, 7, 8, 9, 10, 11, 11, 12 };

            // 가로 울타리 (MAP_MAX_X + 1) * 2
            int[] fenceX = new int[(MAP_MAX_X + 1) * 2+ (MAP_MAX_Y + 1) * 2];
            int[] fenceY = new int[(MAP_MAX_X + 1) * 2+ (MAP_MAX_Y + 1) * 2];

            int index = 0;
            for(int i = 0; i <= MAP_MAX_X; ++i)
            {
                fenceX[index] = i;
                fenceY[index] = MAP_MIN_Y;
                ++index;

                fenceX[index] = i;
                fenceY[index] = MAP_MAX_Y;
                ++index;
            }

            for(int i = 0; i <= MAP_MAX_Y; ++i)
            {
                fenceX[index] = MAP_MIN_X;
                fenceY[index] = i;
                ++index;

                fenceX[index] = MAP_MAX_X;
                fenceY[index] = i;
                ++index;
            }
            
            // 가로 방향 울타리 좌표
            // 위쪽 울타리
            //int[] fenceUpX = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            //int[] fenceUpY = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            //// 아래쪽 울타리
            //int[] fenceDownX = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            //int[] fenceDownY = { 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13 };
            //// 세로 방향 울타리 좌표
            //// 왼쪽 울타리
            //int[] fenceLeftX = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            //int[] fenceLeftY = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
            //// 오른쪽 울타리
            //int[] fenceRightX = { 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15 };
            //int[] fenceRightY = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };

            // 오브젝트의 개수
            int wallCount = wallPositionsX.Length;
            int fenceCount = fenceX.Length;
            // int fenceCount = fenceY.Length;

            // 플레이어가 골 위에 있는지 판별할 수 있는 데이터 생성
            bool isPlayerOnGoal = false;

            // 플레이어가 벽 위에 있는지 판별
            bool isPlayerOnWall = false;

            // 플레이어가 울타리 위에 있는지 판별
            bool isPlayerOnFence = false;


            while (true)
            {
                // ========================== Render ==========================
                // 이전 프레임 지우기
                Console.Clear();

                // 플레이어 그리기
                RenderObject(playerX, playerY, "P");

                // 골 그리기
                RenderObject(GOAL_X, GOAL_Y, "G");

                // 벽 그리기
                for (int i = 0; i < wallCount; ++i)
                {
                    RenderObject(wallPositionsX[i], wallPositionsY[i], "W");
                }

                // 왼쪽 울타리 그리기
                for (int i = 0; i < fenceCount; ++i)
                {
                    RenderObject(fenceX[i], fenceY[i], "F");
                }
                //// 오른쪽 울타리 그리기
                //for (int i = 0; i < fenceLengthCount; ++i)
                //{
                //    RenderObject(fenceX[i], fenceY[i], "F");
                //}
                //// 위쪽 울타리 그리기
                //for (int i = 0; i < fenceWidthCount; ++i)
                //{
                //    RenderObject(fenceUpX[i], fenceUpY[i], "F");
                //}
                //// 아래쪽 울타리 그리기
                //for (int i = 0; i < fenceWidthCount; ++i)
                //{
                //    RenderObject(fenceDownX[i], fenceDownY[i], "F");
                //}

                // ========================== ProcessInput ==========================
                // 사용자로부터 입력을 받음
                ConsoleKeyInfo keyinfo = Console.ReadKey();
                ConsoleKey key = keyinfo.Key;


                // ========================== Update ==========================

                // 플레이어의 이동 처리
                //MovePlayer(key, ref playerX, ref playerY, ref playerDirection);
                if (key == ConsoleKey.LeftArrow)
                {
                    isPlayerOnWall = true;

                    while (isPlayerOnWall)
                    {
                        for (int i = 0; i < wallCount; ++i)
                        {
                            if (playerX == wallPositionsX[i] && playerY == wallPositionsY[i])
                            {
                                isPlayerOnWall = false;
                                break;
                            }
                        }

                        for (int i = 0; i < fenceCount; ++i)
                        {
                            if (playerX == fenceX[i] && playerY == fenceY[i])
                            {
                                isPlayerOnWall = false;
                                break;
                            }
                        }

                        if (false == isPlayerOnWall)
                        {
                            playerX += 1;
                            break;
                        }

                        playerX -= 1;
                    }

                }

                if (key == ConsoleKey.RightArrow)
                {
                    isPlayerOnWall = true;

                    while (isPlayerOnWall)
                    {
                        for (int i = 0; i < wallCount; ++i)
                        {
                            if (playerX == wallPositionsX[i] && playerY == wallPositionsY[i])
                            {
                                isPlayerOnWall = false;
                                break;
                            }
                        }

                        for (int i = 0; i < fenceCount; ++i)
                        {
                            if (playerX == fenceX[i] && playerY == fenceY[i])
                            {
                                isPlayerOnWall = false;
                                break;
                            }
                        }

                        if (false == isPlayerOnWall)
                        {
                            playerX -= 1;
                            break;
                        }

                        playerX += 1;
                    }
                }

                if (key == ConsoleKey.UpArrow)
                {
                    isPlayerOnWall = true;

                    while (isPlayerOnWall)
                    {
                        for (int i = 0; i < wallCount; ++i)
                        {
                            if (playerX == wallPositionsX[i] && playerY == wallPositionsY[i])
                            {
                                isPlayerOnWall = false;
                                break;
                            }
                        }

                        for (int i = 0; i < fenceCount; ++i)
                        {
                            if (playerX == fenceX[i] && playerY == fenceY[i])
                            {
                                isPlayerOnWall = false;
                                break;
                            }
                        }

                        if (false == isPlayerOnWall)
                        {
                            playerY += 1;
                            break;
                        }

                        playerY -= 1;
                    }
                }

                if (key == ConsoleKey.DownArrow)
                {
                    isPlayerOnWall = true;

                    while (isPlayerOnWall)
                    {
                        for (int i = 0; i < wallCount; ++i)
                        {
                            if (playerX == wallPositionsX[i] && playerY == wallPositionsY[i])
                            {
                                isPlayerOnWall = false;
                                break;
                            }
                        }

                        for (int i = 0; i < fenceCount; ++i)
                        {
                            if (playerX == fenceX[i] && playerY == fenceY[i])
                            {
                                isPlayerOnWall = false;
                                break;
                            }
                        }

                        if (false == isPlayerOnWall)
                        {
                            playerY -= 1;
                            break;
                        }

                        playerY += 1;
                    }
                }

                // 플레이어와 벽의 충돌 처리
                for (int i = 0; i < wallCount; ++i)
                {
                    if (false == IsCollided(playerX, playerY, wallPositionsX[i], wallPositionsY[i]))
                    {
                        continue;
                    }

                    OnCollision(playerDirection, ref playerX, ref playerY, in wallPositionsX[i], in wallPositionsY[i]);
                }

                // 플레이어와 울타리의 충돌 처리
                // 왼쪽 울타리
                for (int i = 0; i < fenceCount; ++i)
                {
                    if (false == IsCollided(playerX, playerY, fenceX[i], fenceY[i]))
                    {
                        continue;
                    }

                    OnCollision(playerDirection, ref playerX, ref playerY, fenceX[i], fenceY[i]);
                }
                //// 오른쪽 울타리
                //for (int i = 0; i < fenceLengthCount; ++i)
                //{
                //    if (false == IsCollided(playerX, playerY, fenceRightX[i], fenceRightY[i]))
                //    {
                //        continue;
                //    }

                //    OnCollision(playerDirection, ref playerX, ref playerY, in fenceRightX[i], in fenceRightY[i]);
                //}
                //// 위쪽 울타리
                //for (int i = 0; i < fenceWidthCount; ++i)
                //{
                //    if (false == IsCollided(playerX, playerY, fenceUpX[i], fenceUpY[i]))
                //    {
                //        continue;
                //    }

                //    OnCollision(playerDirection, ref playerX, ref playerY, in fenceUpX[i], in fenceUpY[i]);
                //}
                //// 아래쪽 울타리
                //for (int i = 0; i < fenceWidthCount; ++i)
                //{
                //    if (false == IsCollided(playerX, playerY, fenceDownX[i], fenceDownY[i]))
                //    {
                //        continue;
                //    }

                //    OnCollision(playerDirection, ref playerX, ref playerY, in fenceDownX[i], in fenceDownY[i]);
                //}

                // 플레이어가 골 위에 올라왔는지 확인
                if (playerX == GOAL_X && playerY == GOAL_Y)
                {
                    isPlayerOnGoal = true;
                }

                if (isPlayerOnGoal)
                {
                    break;
                }
            }

            Console.Clear();
            Console.WriteLine("축하합니다. 게임을 클리어 하셨습니다.");

            // 게임이 끝났으니 콘솔 세팅을 원상복구
            Console.ResetColor();

            // ================================ 함수 ===========================================

            // 오브젝트를 그린다
            void RenderObject(int x, int y, string icon)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(icon);
            }

            // 플레이어를 움직이는 부분
            void MovePlayer(ConsoleKey key, ref int x, ref int y, ref Direction direction)
            {
                if (key == ConsoleKey.LeftArrow)
                {
                    x = Math.Max(MAP_MIN_X, x - 1);
                    direction = Direction.Left;
                }

                if (key == ConsoleKey.RightArrow)
                {
                    x = Math.Min(x + 1, MAP_MAX_X);
                    direction = Direction.Right;
                }

                if (key == ConsoleKey.UpArrow)
                {
                    y = Math.Max(MAP_MIN_Y, y - 1);
                    direction = Direction.Up;
                }

                if (key == ConsoleKey.DownArrow)
                {
                    y = Math.Min(y + 1, MAP_MAX_Y);
                    direction = Direction.Down;
                }
            }

            // 에러 메시지 출력 후 종료하는 부분
            void ExitWithError(string errorMessage)
            {
                Console.Clear();
                Console.WriteLine(errorMessage);
                Environment.Exit(1);
            }

            // 충돌했는지 판별하는 부분
            bool IsCollided(int x1, int y1, int x2, int y2)
            {
                if (x1 == x2 && y1 == y2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            // target 근처로 이동시킨다.
            void MoveToLeftOfTarget(out int x, in int target) => x = Math.Max(MAP_MIN_X, target - 1);
            void MoveToRightOfTarget(out int x, in int target) => x = Math.Min(target + 1, MAP_MAX_X);
            void MoveToUpOfTarget(out int y, in int target) => y = Math.Max(MAP_MIN_Y, target - 1);
            void MoveToDownOfTarget(out int y, in int target) => y = Math.Min(target + 1, MAP_MAX_Y);

            // 충돌을 처리하는 부분
            void OnCollision(Direction playerDirection, ref int objX, ref int objY, in int collidedObjX, in int collidedObjY)
            {
                switch (playerDirection)
                {
                    case Direction.Left:
                        MoveToRightOfTarget(out objX, in collidedObjX);
                        break;

                    case Direction.Right:
                        MoveToLeftOfTarget(out objX, in collidedObjX);
                        break;

                    case Direction.Up:
                        MoveToDownOfTarget(out objY, in collidedObjY);
                        break;

                    case Direction.Down:
                        MoveToUpOfTarget(out objY, in collidedObjY);
                        break;
                }
            }

            void AllRender()
            {
                // 이전 프레임 지우기
                Console.Clear();

                // 플레이어 그리기
                RenderObject(playerX, playerY, "P");

                // 벽 그리기
                for (int i = 0; i < wallCount; ++i)
                {
                    RenderObject(wallPositionsX[i], wallPositionsY[i], "W");
                }

                // 골 그리기
                RenderObject(GOAL_X, GOAL_Y, "G");

                //// 왼쪽 울타리 그리기
                //for (int i = 0; i < fenceLengthCount; ++i)
                //{
                //    RenderObject(fenceLeftX[i], fenceLeftY[i], "#");
                //}
                //// 오른쪽 울타리 그리기
                //for (int i = 0; i < fenceLengthCount; ++i)
                //{
                //    RenderObject(fenceRightX[i], fenceRightY[i], "#");
                //}
                //// 위쪽 울타리 그리기
                //for (int i = 0; i < fenceWidthCount; ++i)
                //{
                //    RenderObject(fenceUpX[i], fenceUpY[i], "#");
                //}
                //// 아래쪽 울타리 그리기
                //for (int i = 0; i < fenceWidthCount; ++i)
                //{
                //    RenderObject(fenceDownX[i], fenceDownY[i], "#");
                //}
            }

        }
    }
}