using System;
namespace Sokoban
{
    

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
            // 맵 가로범위, 세로범위
            //const int MAP_MIN_X = 0;
            //const int MAP_MIN_Y = 0;
            //const int MAP_MAX_X = 15;
            //const int MAP_MAX_Y = 10;

            //// 플레이어의 이동 방향
            //const int DIRECTION_LEFT = 1;
            //const int DIRECTION_RIGHT = 2;
            //const int DIRECTION_UP = 3;
            //const int DIRECTION_DOWN = 4;

            //// 플레이어의 초기 좌표
            //const int INITIAL_PLAYER_X = 0;
            //const int INITIAL_PLAYER_Y = 0;
            //Direction playerDirection = Direction
            //// 플레이어 기호 (string literal)
            //const string PLAYER_STRING = "P";
            //// 박스의 기호 (string literal)
            //const string BOX_STRING = "B";

            //// 박스의 초기 좌표
            //const int INITIAL_BOX_X = 0;
            //const int INITIAL_BOX_Y = 0;


            int playerX = 0;
            int playerY = 0;
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
                Console.Write("p");
                //박스 출력하기
                Console.SetCursorPosition(boxX, boxY);
                Console.Write("B");

                //------------ Processintput---------
                ConsoleKey key = Console.ReadKey().Key;

                // ------------update--------------
                //오른쪽 화살표키를 눌렀을때
                if (key == ConsoleKey.RightArrow)
                {
                    playerX = Math.Min(15, playerX + 1);
                }

                if (key == ConsoleKey.LeftArrow)
                {
                    playerX = Math.Max(0, playerX - 1);
                }
                if (key == ConsoleKey.UpArrow)
                {
                    playerY = Math.Max(0, playerY - 1);
                }
                if (key == ConsoleKey.DownArrow)
                {
                    playerY = Math.Min(10, playerY + 1);
                }

                
            }






        }
    }
}