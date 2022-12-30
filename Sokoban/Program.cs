﻿namespace Sokoban
{
    // 열거형
    enum Direction
    {
        Left = 1,
        Right = 2,
        Up = 3,
        Down = 4
    }

    class Program
    {

        static void Main()
        {

            // 초기 세팅
            Console.ResetColor();                       // 컬러를 초기화한다.
            Console.CursorVisible = false;              // 커서를 보이지 않게 해준다. 맥은 좀 보일수도..
            Console.Title = "Sokoban";                  // 제목을 정해준다.
            Console.BackgroundColor = ConsoleColor.Blue; // 백그라운드 색깔을 설정해준다.
            Console.ForegroundColor = ConsoleColor.DarkGray; // 글꼴 색깔 지정.
            Console.Clear(); // 출력된 모든 내용을 지운다.

            // --------------------------player----------------------
            // 플레이어 좌표 설정
            const int INITIAL_PLAYER_X = 0;
            const int INITIAL_PLAYER_Y = 0;

            int playerX = INITIAL_PLAYER_X;
            int playerY = INITIAL_PLAYER_Y;

            // 플레이어의 기호
            const string PLAYER_STRING = "H";

            //-----------------------------box------------------------
            // box 좌표 설정
            // box의 초기 좌표까지 상수로 하고 싶진 않다..
            int boxX = 5;
            int boxY = 5;
            // 박스의 기호
            const string BOX_STRING = "O";


            // 벽의 좌표
            int[] wall_X = new int[3];
            wall_X[0] = 3;
            wall_X[1] = 6;
            wall_X[2] = 7;

            int[] wall_Y = new int[3];
            wall_Y[0] = 3;
            wall_Y[1] = 5;
            wall_Y[2] = 7;

            string WALL_STRING = "#";

            // goal의 좌표
            int[] goal_X = new int[2];
            goal_X[0] = 9;
            goal_X[1] = 9;
            int[] goal_Y = new int[2];
            goal_Y[0] = 2;
            goal_Y[1] = 10;
           
            string GOAL_STRING = "G";

            Direction playerDirection = Direction.Down;

            //map
            const int MAP_MIN_X = 0;
            const int MAP_MIN_Y = 0;
            const int MAP_MAX_X = 15;
            const int MAP_MAX_Y = 10;

            

            
            
            

            


            // 게임 루프 == 프레임(Frame)
            while (true)

            {   // 이전 프레임을 지운다.
                Console.Clear();
                // ---------------------Render----------------------------

                // player 출력하기
                Console.SetCursorPosition(playerX, playerY);
                Console.Write(PLAYER_STRING);

                // box 출력하기
                Console.SetCursorPosition(boxX, boxY);
                Console.Write(BOX_STRING);

                // 벽 출력하기
                for(int i = 0; i < wall_X.Count(); i++ )
                {
                    Console.SetCursorPosition(wall_X[i], wall_Y[i]);
                    Console.Write(WALL_STRING);
                }

                // goal 출력하기
                for(int i = 0; i < goal_X.Count(); i++)
                {
                    Console.SetCursorPosition(goal_X[i], goal_Y[i]);
                    Console.Write(GOAL_STRING);
                }
                

                // ---------------------ProcessInput----------------------
                ConsoleKey key = Console.ReadKey().Key;
                // ---------------------Update----------------------------

                #region 화살표키 눌렀을때 player의 움직임
                // 왼쪽 화살표키를 눌렀을때
                if (key == ConsoleKey.LeftArrow)
                {
                    playerX = Math.Max(MAP_MIN_X, playerX - 1);
                    playerDirection = Direction.Left;

                }

                // 오른쪽 화살표키를 눌렀을때
                if (key == ConsoleKey.RightArrow)
                {
                    // 오른쪽으로 이동 => 누를때마다 1씩 이동한다.
                    playerX = Math.Min(playerX + 1, MAP_MAX_X);
                    playerDirection = Direction.Right;

                }
                // 위쪽 화살표키를 눌렀을때
                if (key == ConsoleKey.UpArrow)
                {
                    playerY = Math.Max(MAP_MIN_Y, playerY - 1);
                    playerDirection = Direction.Up;

                }
                // 아래쪽 화살표키를 눌렀을때
                if (key == ConsoleKey.DownArrow)
                {
                    playerY = Math.Min(playerY + 1, MAP_MAX_Y);
                    playerDirection = Direction.Down;

                }
                #endregion

                //박스 update
                if (playerX == boxX && playerY == boxY)
                {
                    switch (playerDirection)
                    {
                        case Direction.Left: //플레이어가 왼쪽으로 이동
                            if (boxX == MAP_MIN_X)
                            {
                                playerX = MAP_MIN_X + 1;
                            }
                            else
                            {
                                boxX -= 1;
                            }
                            break;
                        case Direction.Right: //플레이어가 오른쪽으로 이동
                            if (boxX == MAP_MAX_X)
                            {
                                playerX = MAP_MAX_X - 1;
                            }
                            else
                            {
                                boxX += 1;
                            }
                            break;
                        case Direction.Up: //플레이어가 위로 이동
                            if (boxY == MAP_MIN_Y)
                            {
                                playerY = MAP_MIN_Y + 1;
                            }
                            else
                            {
                                boxY -= 1;
                            }
                            break;
                        case Direction.Down:
                            if (boxY == MAP_MAX_Y)
                            {
                                playerY = MAP_MAX_Y - 1;
                            }
                            else
                            {
                                boxY += 1;
                            }
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine($"[Error]{playerDirection}");
                            break;
                    }
                }

                // player가 벽에서 막히게
                for(int i = 0; i < wall_X.Count(); i++)
                {
                    if (playerX == wall_X[i] && playerY == wall_Y[i])
                    {
                        switch (playerDirection)
                        {
                            case Direction.Left:
                                if (playerX == wall_X[i])
                                {
                                    playerX += 1;
                                }
                                break;
                            case Direction.Right:
                                if (playerX == wall_X[i])
                                {
                                    playerX -= 1;
                                }
                                break;
                            case Direction.Up:
                                if (playerY == wall_Y[i])
                                {
                                    playerY += 1;
                                }
                                break;
                            case Direction.Down:
                                if (playerY == wall_Y[i])
                                {
                                    playerY -= 1;
                                }
                                break;
                        }
                    }
                }
             
                // box가 벽에서 막히게
                for (int i = 0; i < wall_X.Count(); i++)
                {
                    if (boxX == wall_X[i] && boxY == wall_Y[i])
                    {
                        switch (playerDirection)
                        {
                            case Direction.Left:
                                if (boxX == wall_X[i])
                                {
                                    boxX += 1;
                                    playerX += 1;
                                }
                                break;
                            case Direction.Right:
                                if (boxX == wall_X[i])
                                {
                                    boxX -= 1;
                                    playerX -= 1;
                                }
                                break;
                            case Direction.Up:
                                if (boxY == wall_Y[i])
                                {
                                    boxY += 1;
                                    playerY += 1;
                                }
                                break;
                            case Direction.Down:
                                if (boxY == wall_Y[i])
                                {
                                    boxY -= 1;
                                    playerY -= 1;
                                }
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine($"[Error] {playerDirection}");
                                return;
                        }
                    }
                }

                // goal 구현
                for (int i = 0; i < goal_X.Count(); i++)
                {
                    if (boxX == goal_X[i] && boxY == goal_Y[i])
                    {
                        Console.Clear();
                        Console.Write("축하합니다!!");
                        return;
                    }
                }
                
                    

            }


        }
    }
}






//int x = 20;
//int y = 3;
//Console.SetCursorPosition(x, y);
//Console.Write("H"); // 개행 하면 안된다.
//ConsoleKeyInfo keyInfo = Console.ReadKey();
//Console.WriteLine(keyInfo.Key);




