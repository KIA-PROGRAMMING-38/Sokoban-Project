using new_rekoban;
using System;
using System.Diagnostics.Metrics;
using System.Numerics;
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
            // 인스턴스(Instance)
            Player player = new Player();

            Renderer renderer = new Renderer();

            Wall wall = new Wall();

            Game game = new Game();

            // 플레이어의 위치 좌표
            int playerX = 14;
            int playerY = 12;

            // 플레이어의 이동 방향
            Direction playerDirection = Direction.None; // 0:None / 1:Left / 2:Right / 3:Up / 4:Down

            // 벽의 좌표
            int[] wallPositionsX = { 9, 4, 10, 2, 1, 9, 14, 7, 3, 14, 8, 6, 10, 13 };
            int[] wallPositionsY = { 1, 2, 3, 4, 5, 5, 6, 7, 8, 9, 10, 11, 11, 12 };

            // 위쪽 울타리
            //int[] fenceUpX = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            //int[] fenceUpY = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            //// 아래쪽 울타리
            //int[] fenceDownX = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            //int[] fenceDownY = { 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13 };
            //// 왼쪽 울타리
            //int[] fenceLeftX = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            //int[] fenceLeftY = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
            //// 오른쪽 울타리
            //int[] fenceRightX = { 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15 };
            //int[] fenceRightY = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };

            // 울타리의 좌표
            int[] fenceX = new int[(Game.MAX_X + 1) * 2 + (Game.MAX_Y + 1) * 2];
            int[] fenceY = new int[(Game.MAX_X + 1) * 2 + (Game.MAX_Y + 1) * 2];

            int index = 0;

            for (int i = 0; i <= Game.MAX_X; ++i)
            {
                fenceX[index] = i;
                fenceY[index] = Game.MIN_Y;
                ++index;

                fenceX[index] = i;
                fenceY[index] = Game.MAX_Y;
                ++index;
            }

            for (int i = 0; i <= Game.MAX_Y; ++i)
            {
                fenceX[index] = Game.MIN_X;
                fenceY[index] = i;
                ++index;

                fenceX[index] = Game.MAX_X;
                fenceY[index] = i;
                ++index;
            }

            // 오브젝트의 개수
            int wallCount = wallPositionsX.Length;
            int fenceCount = fenceX.Length;

            // 플레이어가 골 위에 있는지 판별할 수 있는 데이터 생성
            bool isPlayerOnGoal = false;

            // 플레이어가 골이 아닌 오브젝트 위에 있는지 판별
            bool isPlayerOnObj = false;


            while (true)
            {
                // ========================== Render ==========================
                // 이전 프레임 지우기
                Console.Clear();

                // 플레이어 그리기
                renderer.Render(player.GetX(), player.GetY(), player.GetSymbol());

                // 출구 그리기
                RenderObject(Game.GOAL_X, Game.GOAL_Y, "E");

                // 벽 그리기
                for (int i = 0; i < wallCount; ++i)
                {
                    RenderObject(wallPositionsX[i], wallPositionsY[i], "*");
                }

                // 울타리 그리기
                for (int i = 0; i < fenceCount; ++i)
                {
                    RenderObject(fenceX[i], fenceY[i], "@");
                }


                // ========================== ProcessInput ==========================
                // 사용자로부터 입력을 받음
                ConsoleKeyInfo keyinfo = Console.ReadKey();
                ConsoleKey key = keyinfo.Key;


                // ========================== Update ==========================

                // 플레이어의 이동 처리
                if (key == ConsoleKey.LeftArrow)
                {
                    isPlayerOnObj = true;

                    while (isPlayerOnObj)
                    {
                        for (int i = 0; i < wallCount; ++i)
                        {
                            if (playerX == wallPositionsX[i] && playerY == wallPositionsY[i])
                            {
                                isPlayerOnObj = false;
                                break;
                            }
                        }

                        for (int i = 0; i < fenceCount; ++i)
                        {
                            if (playerX == fenceX[i] && playerY == fenceY[i])
                            {
                                isPlayerOnObj = false;
                                break;
                            }
                        }

                        if (false == isPlayerOnObj)
                        {
                            playerX += 1;
                            break;
                        }

                        playerX -= 1;
                    }

                }

                if (key == ConsoleKey.RightArrow)
                {
                    isPlayerOnObj = true;

                    while (isPlayerOnObj)
                    {
                        for (int i = 0; i < wallCount; ++i)
                        {
                            if (playerX == wallPositionsX[i] && playerY == wallPositionsY[i])
                            {
                                isPlayerOnObj = false;
                                break;
                            }
                        }

                        for (int i = 0; i < fenceCount; ++i)
                        {
                            if (playerX == fenceX[i] && playerY == fenceY[i])
                            {
                                isPlayerOnObj = false;
                                break;
                            }
                        }

                        if (false == isPlayerOnObj)
                        {
                            playerX -= 1;
                            break;
                        }

                        playerX += 1;
                    }
                }

                if (key == ConsoleKey.UpArrow)
                {
                    isPlayerOnObj = true;

                    while (isPlayerOnObj)
                    {
                        for (int i = 0; i < wallCount; ++i)
                        {
                            if (playerX == wallPositionsX[i] && playerY == wallPositionsY[i])
                            {
                                isPlayerOnObj = false;
                                break;
                            }
                        }

                        for (int i = 0; i < fenceCount; ++i)
                        {
                            if (playerX == fenceX[i] && playerY == fenceY[i])
                            {
                                isPlayerOnObj = false;
                                break;
                            }
                        }

                        if (false == isPlayerOnObj)
                        {
                            playerY += 1;
                            break;
                        }

                        playerY -= 1;
                    }
                }

                if (key == ConsoleKey.DownArrow)
                {
                    isPlayerOnObj = true;

                    while (isPlayerOnObj)
                    {
                        for (int i = 0; i < wallCount; ++i)
                        {
                            if (playerX == wallPositionsX[i] && playerY == wallPositionsY[i])
                            {
                                isPlayerOnObj = false;
                                break;
                            }
                        }

                        for (int i = 0; i < fenceCount; ++i)
                        {
                            if (playerX == fenceX[i] && playerY == fenceY[i])
                            {
                                isPlayerOnObj = false;
                                break;
                            }
                        }

                        if (false == isPlayerOnObj)
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
                for (int i = 0; i < fenceCount; ++i)
                {
                    if (false == IsCollided(playerX, playerY, fenceX[i], fenceY[i]))
                    {
                        continue;
                    }

                    OnCollision(playerDirection, ref playerX, ref playerY, fenceX[i], fenceY[i]);
                }

                // 플레이어가 골 위에 올라왔는지 확인
                if (playerX == Game.GOAL_X && playerY == Game.GOAL_Y)
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
            void MoveToLeftOfTarget(out int x, in int target) => x = Math.Max(Game.MIN_X, target - 1);
            void MoveToRightOfTarget(out int x, in int target) => x = Math.Min(target + 1, Game.MAX_X);
            void MoveToUpOfTarget(out int y, in int target) => y = Math.Max(Game.MIN_Y, target - 1);
            void MoveToDownOfTarget(out int y, in int target) => y = Math.Min(target + 1, Game.MAX_Y);

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

            //static void RepeatCheckPlayerOnObject(Player, Wall, Wall, bool someBool)
            //{
            //    for (int i = 0; i < Wall.count; ++i)
            //    {
            //        if (Player.GetX == Wall._x[i] && Player.GetY == Wall._y[i])
            //        {
            //            someBool = false;
            //            break;
            //        }
            //    }
            //}
        }
    }
}