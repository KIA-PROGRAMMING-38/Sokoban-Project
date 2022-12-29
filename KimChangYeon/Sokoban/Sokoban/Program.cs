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
   
            const int INITIAL_WALL_X = 15;
            const int INITIAL_WALL_Y = 8;
            
            int PlayerX = INITIAL_PLAYER_X;
            int PlayerY = INITIAL_PLAYER_Y;
            int[] BoxX = new int[3] { 3, 5, 8 };
            int[] BoxY = new int[3] { 3, 4, 8 };
            int Wall1X = INITIAL_WALL_X;
            int Wall1Y = INITIAL_WALL_Y;
            int[] GoalX = new int[3] { 5, 8, 9 };
            int[] GoalY = new int[3] { 9, 2, 9 };
            int Length = BoxX.Length;

            PLAYER_DIRECTION playerDIR = new PLAYER_DIRECTION();

            //15 X 10
            // 게임 루프 == 프레임(Frame)
            while (true)
            {
                Console.Clear();
                Console.WriteLine("#################");
                for (int i = 0; i <= MAP_MAX_Y - 2; i++)
                {
                    Console.WriteLine("#               #");
                    if (i != 6)
                        continue;
                    Console.WriteLine("#              ##");
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
                Console.SetCursorPosition(Wall1X, Wall1Y);
                Console.Write("#");
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
                for (int k = 0; k < Length; k++)
                {
                    if (PlayerX == BoxX[k] && PlayerY == BoxY[k]) // 외곽 벽을 만났을 떄
                    {
                        switch (playerDIR)
                        {
                            case PLAYER_DIRECTION.RIGHT: //right
                                PlayerX = Math.Min(PlayerX, MAP_MAX_X - 1);
                                BoxX[k] = Math.Min(BoxX[k] + 1, MAP_MAX_X);
                                break;
                            case PLAYER_DIRECTION.LEFT: //left
                                PlayerX = Math.Max(PlayerX, MAP_MIN_X + 2);
                                BoxX[k] = Math.Max(BoxX[k] - 1, MAP_MIN_X + 1);
                                break;
                            case PLAYER_DIRECTION.DOWN: //down
                                PlayerY = Math.Min(PlayerY, MAP_MAX_Y - 1);
                                BoxY[k] = Math.Min(BoxY[k] + 1, MAP_MAX_Y);
                                break;
                            case PLAYER_DIRECTION.UP: //up
                                PlayerY = Math.Max(PlayerY, MAP_MIN_Y + 2);
                                BoxY[k] = Math.Max(BoxY[k] - 1, MAP_MIN_Y + 1);
                                break;

                        }
                    }
                }
                if (PlayerX == Wall1X && PlayerY == Wall1Y) //벽과 플레이어
                {
                    switch (playerDIR)
                    {
                        case PLAYER_DIRECTION.RIGHT: //right
                            PlayerX = Math.Min(PlayerX, Wall1X - 1);
                            break;
                        case PLAYER_DIRECTION.LEFT: //left
                            PlayerX = Math.Max(PlayerX, Wall1X + 1);
                            break;
                        case PLAYER_DIRECTION.DOWN: //down
                            PlayerY = Math.Min(PlayerY, Wall1Y - 1);
                            break;
                        case PLAYER_DIRECTION.UP: //up
                            PlayerY = Math.Max(PlayerY, Wall1Y + 1);
                            break;
                    }
                }
                for (int h = 0; h < Length; h++)
                {
                    if (BoxX[h] == Wall1X && BoxY[h] == Wall1Y) //벽과 박스
                    {
                        switch (playerDIR)
                        {
                            case PLAYER_DIRECTION.RIGHT: //right
                                PlayerX = Math.Min(PlayerX, Wall1X - 2);
                                BoxX[h] = Math.Min(BoxX[h] + 1, Wall1X - 1);
                                break;
                            case PLAYER_DIRECTION.LEFT: //left
                                PlayerX = Math.Max(PlayerX, Wall1X + 2);
                                BoxX[h] = Math.Min(BoxX[h] + 1, Wall1X + 1);
                                break;
                            case PLAYER_DIRECTION.DOWN: //down
                                PlayerY = Math.Min(PlayerY, Wall1Y - 2);
                                BoxY[h] = Math.Min(BoxY[h] - 1, Wall1Y - 1);
                                break;
                            case PLAYER_DIRECTION.UP: //up
                                PlayerY = Math.Max(PlayerY, Wall1Y + 2);
                                BoxY[h] = Math.Max(BoxY[h] - 1, Wall1Y + 1);
                                break;
                        }
                    }
                }
                for (int g = 0; g < Length - 1; g++)
                {
                    if (BoxX[g] == BoxX[g + 1] && BoxY[g] == BoxY[g + 1]) //박스와 박스 충돌
                    {
                        switch (playerDIR)
                        {
                            case PLAYER_DIRECTION.RIGHT: //right
                                PlayerX = Math.Min(PlayerX, BoxX[g + 1] - 2);
                                BoxX[g] = Math.Min(BoxX[g], BoxX[g + 1] - 1);
                                break;
                            case PLAYER_DIRECTION.LEFT: //left
                                PlayerX = Math.Max(PlayerX, BoxX[g + 1] + 2);
                                BoxX[g] = Math.Max(BoxX[g], BoxX[g + 1] + 1);
                                break;
                            case PLAYER_DIRECTION.DOWN: //down
                                PlayerY = Math.Min(PlayerY, BoxY[g + 1] - 2);
                                BoxY[g] = Math.Min(BoxY[g], BoxY[g + 1] - 1);
                                break;
                            case PLAYER_DIRECTION.UP: //up
                                PlayerY = Math.Max(PlayerY, BoxY[g + 1] + 2);
                                BoxY[g] = Math.Max(BoxY[g], BoxY[g + 1] + 1);
                                break;
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