using System;
namespace Sokoban
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

            // 초기세팅
            Console.ResetColor();
            Console.CursorVisible = false;
            Console.Title = "진우의 소코반메이커";
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Clear();

            // 기호 상수
            //맵 가로범위, 세로범위
            const int MAP_MIN_X = 0;
            const int MAP_MIN_Y = 0;
            const int MAP_MAX_X = 15;
            const int MAP_MAX_Y = 10;

            // 플레이어의 이동 방향
            const int DIRECTION_LEFT = 1;
            const int DIRECTION_RIGHT = 2;
            const int DIRECTION_UP = 3;
            const int DIRECTION_DOWN = 4;

            // 플레이어의 초기 좌표
            const int INITIAL_PLAYER_X = 0;
            const int INITIAL_PLAYER_Y = 0;
            Direction playerDirection = 0;
            // 플레이어 기호 (string literal)
            const string PLAYER_STRING = "P";
            // 박스의 기호 (string literal)
            const string BOX_STRING = "B";

            // 박스의 초기 좌표
            const int INITIAL_BOX_X = 5;
            const int INITIAL_BOX_Y = 5;

            //벽의 좌표
            const int INITIAL_WALL_X = 7;
            const int INITIAL_WALL_Y = 8;
            //벽의 기호 
            const string WALL_STRING = "H";
            

            int playerX = 0;
            int playerY = 0;
             // 0 : None, 1 : Left, 2 : Right, 3 : Up, 4 : Down [1, 4]
            //박스 좌표설정
            int boxX = 5;
            int boxY = 5;
        

            // 가로 15 세로 10
            //게임 루프 == 프레임
            while (true)
            {
                Console.Clear();
                //------------ Render-----------
                // 플레이어 출력하기
                Console.SetCursorPosition(playerX, playerY);
                Console.Write(PLAYER_STRING);
                //박스 출력하기
                Console.SetCursorPosition(boxX, boxY);
                Console.Write(BOX_STRING);
                //벽 출력하기
                Console.SetCursorPosition(INITIAL_WALL_X, INITIAL_WALL_Y);
                Console.Write(WALL_STRING);
                //------------ Processintput---------
                ConsoleKey key = Console.ReadKey().Key;

                // ------------update--------------
                //오른쪽 화살표키를 눌렀을때
                
                if (key == ConsoleKey.LeftArrow)
                {
                    playerX = Math.Max(MAP_MIN_X, playerX - 1);
                    playerDirection = Direction.Left;
                } // 캐릭터 왼쪽이동 움직임 구현
                if (key == ConsoleKey.RightArrow)
                {
                    playerX = Math.Min(MAP_MAX_X, playerX + 1);
                    playerDirection = Direction.Right;

                } // 캐릭터 오른쪽이동 움직임 구현
                if (key == ConsoleKey.UpArrow)
                {
                    playerY = Math.Max(MAP_MIN_Y, playerY - 1);
                    playerDirection = Direction.Up;
                } // 캐릭터 위이동 움직임 구현
                if (key == ConsoleKey.DownArrow)
                {
                    playerY = Math.Min(MAP_MAX_Y, playerY + 1);
                    playerDirection = Direction.Down;
                } // 캐릭터 아래이동 움직임 구현

                // 플레이어 , 벽 충돌방지
                if (playerX == INITIAL_WALL_X && playerY == INITIAL_WALL_Y)
                {
                    switch (playerDirection)
                    {
                        case Direction.Left:
                            if (playerX == INITIAL_WALL_X)
                            {
                                playerX += 1;
                            }
                            break;
                        case Direction.Right:
                            if(playerX == INITIAL_WALL_X)
                            {
                                playerX -= 1;
                            }
                            break;
                        case Direction.Up:
                            if (playerY == INITIAL_WALL_Y)
                            {
                                playerY += 1;
                            } break;
                        case Direction.Down:
                            if (playerY == INITIAL_WALL_Y)
                            {
                                playerY -= 1;
                            }break;
                    }

                }
                
                // 플레이어 , 박스 이동 구현
                if (playerX == boxX && playerY == boxY) // 플레이어가 이동하고나니 박스가 있네?
                {
                    // 박스를 움직여주면 됨.
                    switch (playerDirection)
                    {
                        case Direction.Left: // 플레이어가 왼쪽으로 이동 중
                            if (boxX == MAP_MIN_X) // 박스가 왼쪽 끝에 있다면?
                            {
                                playerX = 1;
                            }
                            else
                            {
                                boxX = boxX - 1;
                            }
                            break;
                        case Direction.Right: // 플레이어가 오른쪽으로 이동 중
                            if (boxX == MAP_MAX_X) // 박스가 오른쪽 끝에 있다면?
                            {
                                playerX = 14;
                            }
                            else
                            {
                                boxX = boxX + 1;
                            }
                            break;
                        case Direction.Up: // 플레이어가 위로 이동 중
                            if (boxY == MAP_MIN_Y)
                            {
                                playerY = 1;
                            }
                            else
                            {
                                boxY = boxY - 1;
                            }
                            break;
                        case Direction.Down: // 플레이어가 아래로 이동 중
                            if (boxY == MAP_MAX_Y)
                            {
                                playerY = 9;
                            }
                            else
                            {
                                boxY = boxY + 1;
                            }
                            break;
                        default: //
                            Console.Clear();
                            Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다. {playerDirection}");

                            return; // 프로그램 종료
                    }

                }
                // 박스 와 벽 충돌 방지
                if (boxX == INITIAL_WALL_X && boxY == INITIAL_WALL_Y)
                {
                    switch (playerDirection)
                    {
                        case Direction.Left:
                                boxX += 1;
                                playerX = boxX + 1;
                           break;

                        case Direction.Right:
                                boxX -= 1;
                                playerX = boxX - 1;
                            break;

                        case Direction.Up:
                                boxY += 1;
                                playerY = boxY + 1;
                            break;

                        case Direction.Down:
                                boxY -= 1;
                                playerY = boxY - 1;
                            break;
                    }

                }

            }
        }
    }
}