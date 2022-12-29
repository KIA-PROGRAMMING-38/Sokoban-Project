using System;
using System.Diagnostics.Tracing;

namespace Sokoban
{
    // 열거형
    enum Direction
    {
        None,
        Left,
        Right,
        Up,
        Down,
    }

    class Program
    {
        static void Main()
        {
            // 초기
            Console.ResetColor();                              // 컬러를 초기화한다.
            Console.CursorVisible = false;                     // 커서를 숨긴다.
            Console.Title = "어우석 이찬혁 바보";                // 타이틀을 설정한다.
            Console.BackgroundColor = ConsoleColor.DarkGreen;  // 배경을 설정한다.
            Console.ForegroundColor = ConsoleColor.Black;      //글꼴을 설정한다.
            Console.Clear();                                   // 출려된 모든 내용을 지운다.

            // 기호 상수
            // 맵의 가로 범위, 세로 범위
            const int MAP_MIN_X = 0;
            const int MAP_MIN_Y = 0;
            const int MAP_MAX_X = 15;
            const int MAP_MAX_Y = 10;

            // 플레이어의 초기 좌표
            const int INITIAL_PLAYER_X = 0;
            const int INITIAL_PLAYER_Y = 0;
            // 플레이어의 기호 (string literal)
            const string PLAYER_STRING = "p";

            // 박스의 초기 좌표
            const int INITIAL_BOX_X = 5;
            const int INITIAL_BOX_Y = 5;
            // 박스의 기호 (string literal)
            const string BOX_STRING = "B";

            // 벽의 좌표
            const int INITIAL_WALL_X = 7;
            const int INITIAL_WALL_Y = 8;
            // 벽의 기호(string literal)
            const string WALL_STRING = "W";

            // 플레이어 좌표 설정
            int playerX = 0;
            int playerY = 0;
            Direction playerDirection = Direction.None;
                                                         // 1 : Left, 2 : Right, 3 : Up, 4 : Down
                                                         // ★★★ 추가적인 데이터가 무엇인지 생각해내는것 매우 중요!! ★★★

            // 박스 좌표 설정
            int boxX = INITIAL_BOX_X;
            int boxY = INITIAL_BOX_Y;

            // 벽 좌표 설정
            int wallX = INITIAL_WALL_X;
            int wallY = INITIAL_WALL_Y;


            // 가로 15 새로 10
            // 게임 루프 == 프레임(Frame)
            while (true)
            {
                // 이전 프레임을 지운다.
                Console.Clear();


                // -----------------------------------------------------Render-----------------------------------------------------
                // 플레이어 출력하기    
                Console.SetCursorPosition(playerX, playerY);
                Console.Write(PLAYER_STRING);

                // 박스 출력하기
                Console.SetCursorPosition(boxX, boxY);
                Console.Write(BOX_STRING);

                // 벽 출력하기
                Console.SetCursorPosition(wallX, wallY);
                Console.Write(WALL_STRING);

                // --------------------------------------------------ProcessINput--------------------------------------------------
                ConsoleKey key = Console.ReadKey().Key;

                // -----------------------------------------------------Update-----------------------------------------------------
               
                // 플레이어 업데이트               
                if (key == ConsoleKey.LeftArrow)  // ← 왼쪽으로 이동
                {
                    playerX = Math.Max(0, playerX - 1);
                    playerDirection = Direction.Left;
                }
                if (key == ConsoleKey.RightArrow)  // → 오른쪽으로 이동
                {
                    playerX = Math.Min(playerX + 1, MAP_MAX_X);
                    playerDirection = Direction.Right;
                }
                if (key == ConsoleKey.UpArrow)  // ↑ 위로 이동
                {
                    playerY = Math.Max(0, playerY - 1);
                    playerDirection = Direction.Up;
                }
                if (key == ConsoleKey.DownArrow)  // ↓ 밑으로 이동
                {
                    playerY = Math.Min(playerY + 1, MAP_MAX_Y);
                    playerDirection = Direction.Down;
                }

                // 플레이어가 벽에 부딪혔을 때
                if (playerX == wallX && playerY == wallY)
                {
                    switch (playerDirection)
                    {
                        case Direction.Left:  // ← 왼쪽으로 이동
                            playerX = playerX- 1;
                            break;
                        case Direction.Right:  // → 오른쪽으로 이동
                            playerX = playerX + 1;
                            break;
                        case Direction.Up:  // ↑ 위로 이동
                            playerY = playerY - 1;
                            break;
                        case Direction.Down:  // ↓ 밑으로 이동
                            playerY = playerY + 1;
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다. {playerDirection}");

                            return;
                    }
                }

                // 박스 업데이트
                // 플레이가 이동한 후
                if (playerX == boxX && boxY == playerY) // 플레이어가 이동하거나니 박스가 있네?
                {
                    //  박스를 움직여주면 됨.
                    switch (playerDirection)
                    {
                        case Direction.Left: // ← 왼쪽으로 이동 중
                            if (boxX == MAP_MIN_X)
                            {
                                playerX = MAP_MIN_X + 1;
                            }
                            else
                            {
                                boxX = boxX -1;
                            }
                            break;
                        case Direction.Right:  // → 오른쪽으로 이동 중
                            if (boxX == MAP_MAX_X)
                            {
                                playerX = MAP_MAX_X - 1;
                            }
                            else
                            {
                                boxX = boxX + 1;
                            }
                            break;
                        case Direction.Up:  // ↑ 위로 이동 중
                            if (boxY == MAP_MIN_Y)
                            {
                                playerY = MAP_MIN_Y + 1;
                            }
                            else
                            {
                                boxY = boxY - 1;
                            }
                            break;
                        case Direction.Down:  // ↓ 밑으로 이동 중
                            if (boxY == MAP_MAX_Y)
                            {
                                playerY = MAP_MAX_Y - 1;
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
            
                // 박스가 벽에 부딪혔을 때
                if (boxX == wallX && boxY == wallY)  // ^^7
                {
                    switch (playerDirection)
                    {
                        case Direction.Left:  // ←
                            boxX = boxX + 1;
                            playerX = playerX + 1;
                            break;
                        case Direction.Right:  // →
                            boxX = boxX - 1;
                            playerX = playerX - 1;
                            break;
                        case Direction.Up:  // ↑
                            boxY = boxY + 1;
                            playerY = playerY + 1;
                            break;
                        case Direction.Down:  // ↓
                            boxY = boxY - 1;
                            playerY = playerY - 1;
                            break;

                    }
                }
            
            
            }
        }
    }
}