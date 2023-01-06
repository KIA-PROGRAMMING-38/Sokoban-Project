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
   
            int PlayerX = INITIAL_PLAYER_X;
            int PlayerY = INITIAL_PLAYER_Y;
            int[] BoxX = new int[3] { 3, 5, 8 };
            int[] BoxY = new int[3] { 3, 4, 8 };
            int[] WallX = new int[3] { 15, 14, 13 };
            int[] WallY = new int[3] { 8, 8, 8 };
            int[] GoalX = new int[3] { 5, 8, 9 };
            int[] GoalY = new int[3] { 9, 2, 9 };
            int BoxLength = BoxX.Length;
            int WallLength = WallX.Length;
            int WhatBox = default;

            PLAYER_DIRECTION playerDIR = new PLAYER_DIRECTION();

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

                Console.SetCursorPosition(BoxX[0], BoxY[0]);
                Console.Write("1");
                Console.SetCursorPosition(BoxX[1], BoxY[1]);
                Console.Write("2");
                Console.SetCursorPosition(BoxX[2], BoxY[2]);
                Console.Write("3");
                Console.SetCursorPosition(PlayerX, PlayerY);
                Console.Write("Q");
                for (int i = 0; i < WallX.Length; i++)
                {
                    Console.SetCursorPosition(WallX[i], WallY[i]);
                    Console.Write("#");
                }
                Console.SetCursorPosition(GoalX[0], GoalY[0]);
                Console.Write("①");
                Console.SetCursorPosition(GoalX[1], GoalY[1]);
                Console.Write("②");
                Console.SetCursorPosition(GoalX[2], GoalY[2]);
                Console.Write("③");

                // --------------------------------------------- ProcessInput -------------------------------------------------
                ConsoleKey key = Console.ReadKey().Key;
                // --------------------------------------------- Update -------------------------------------------------------
                // 오른쪽 화살표키를
                if (key == ConsoleKey.RightArrow)  //이동 부
                {
                    PlayerX = Math.Min(PlayerX + 1, MAP_MAX_X);
                    playerDIR = PLAYER_DIRECTION.RIGHT;
                }
                if (key == ConsoleKey.LeftArrow)
                {
                    PlayerX = Math.Max(PlayerX - 1, MAP_MIN_X + 1);
                    playerDIR = PLAYER_DIRECTION.LEFT;
                }
                if (key == ConsoleKey.DownArrow)
                {
                    PlayerY = Math.Min(PlayerY + 1, MAP_MAX_Y);
                    playerDIR = PLAYER_DIRECTION.DOWN;
                }
                if (key == ConsoleKey.UpArrow)
                {
                    PlayerY = Math.Max(PlayerY - 1, MAP_MIN_Y + 1);
                    playerDIR = PLAYER_DIRECTION.UP;

                }
                for (int i = 0; i < BoxLength; i++)
                {
                    if (PlayerX == BoxX[i] && PlayerY == BoxY[i]) // 외곽 벽을 만났을 떄
                    {
                        WhatBox = i;
                        switch (playerDIR)
                        {
                            case PLAYER_DIRECTION.RIGHT: //right
                                PlayerX = Math.Min(PlayerX, MAP_MAX_X - 1);
                                BoxX[i] = Math.Min(BoxX[i] + 1, MAP_MAX_X);
                                break;
                            case PLAYER_DIRECTION.LEFT: //left
                                PlayerX = Math.Max(PlayerX, MAP_MIN_X + 2);
                                BoxX[i] = Math.Max(BoxX[i] - 1, MAP_MIN_X + 1);
                                break;
                            case PLAYER_DIRECTION.DOWN: //down
                                PlayerY = Math.Min(PlayerY, MAP_MAX_Y - 1);
                                BoxY[i] = Math.Min(BoxY[i] + 1, MAP_MAX_Y);
                                break;
                            case PLAYER_DIRECTION.UP: //up
                                PlayerY = Math.Max(PlayerY, MAP_MIN_Y + 2);
                                BoxY[i] = Math.Max(BoxY[i] - 1, MAP_MIN_Y + 1);
                                break;

                        }
                    }
                }
                for (int i = 0; i < WallLength; i++)
                {
                    if (PlayerX == WallX[i] && PlayerY == WallY[i]) //벽과 플레이어
                    {
                        switch (playerDIR)
                        {
                            case PLAYER_DIRECTION.RIGHT: //right
                                PlayerX = WallX[i] - 1;
                                break;
                            case PLAYER_DIRECTION.LEFT: //left
                                PlayerX = WallX[i] + 1;
                                break;
                            case PLAYER_DIRECTION.DOWN: //down
                                PlayerY = WallY[i] - 1;
                                break;
                            case PLAYER_DIRECTION.UP: //up
                                PlayerY = WallY[i] + 1;
                                break;
                        }
                    }
                }
                for (int i = 0; i < BoxLength; i++)
                {
                    for (int j = 0; j < WallLength; j++)
                    {
                        if (BoxX[i] == WallX[j] && BoxY[i] == WallY[j]) //벽과 박스
                        {
                            switch (playerDIR)
                            {
                                case PLAYER_DIRECTION.RIGHT: //right
                                    PlayerX = Math.Min(PlayerX, WallX[j] - 2);
                                    BoxX[i] = Math.Min(BoxX[i] + 1, WallX[j] - 1);
                                    break;
                                case PLAYER_DIRECTION.LEFT: //left
                                    PlayerX = Math.Max(PlayerX, WallX[j] + 2);
                                    BoxX[i] = Math.Min(BoxX[i] + 1, WallX[j] + 1);
                                    break;
                                case PLAYER_DIRECTION.DOWN: //down
                                    PlayerY = Math.Min(PlayerY, WallY[j] - 2);
                                    BoxY[i] = Math.Min(BoxY[i] - 1, WallY[j] - 1);
                                    break;
                                case PLAYER_DIRECTION.UP: //up
                                    PlayerY = Math.Max(PlayerY, WallY[j] + 2);
                                    BoxY[i] = Math.Max(BoxY[i] - 1, WallY[j] + 1);
                                    break;
                            }
                        }
                    }
                }
                for (int i = 0; i < BoxLength; i++)
                {
                    for (int j = 0; j < BoxLength; j++)
                    {
                        if (i ==j )
                        {
                            continue;
                        }
                        if (BoxX[i] == BoxX[j] && BoxY[i] == BoxY[j] && WhatBox == i) //박스와 박스 충돌
                        {
                            switch (playerDIR)
                            {
                                case PLAYER_DIRECTION.RIGHT: //right
                                    PlayerX = PlayerX - 1;
                                    BoxX[j] = PlayerX + 2;
                                    BoxX[i] = PlayerX + 1;
                                    break;
                                case PLAYER_DIRECTION.LEFT: //left
                                    PlayerX = PlayerX + 1;
                                    BoxX[j] = PlayerX - 2;
                                    BoxX[i] = PlayerX - 1;
                                    break;
                                case PLAYER_DIRECTION.DOWN: //down
                                    PlayerY = PlayerY - 1;
                                    BoxY[j] = PlayerY + 2;
                                    BoxY[i] = PlayerY + 1;
                                    break;
                                case PLAYER_DIRECTION.UP: //up
                                    PlayerY = PlayerY + 1;
                                    BoxY[j] = PlayerY - 2;
                                    BoxY[i] = PlayerY - 1;
                                    break;
                            }
                        }
                    }
                }
                
                    if (BoxX[0] == GoalX[0] && BoxY[0] == GoalY[0] &&  //클리어 판정
                    BoxX[1] == GoalX[1] && BoxY[1] == GoalY[1] &&
                    BoxX[2] == GoalX[2] && BoxY[2] == GoalY[2])
                {
                    Console.Clear();
                    Console.WriteLine("Clear!");
                    return;
                }
                    
            }
        }
    }
}