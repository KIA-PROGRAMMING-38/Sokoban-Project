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
            //맵의 가로 범위, 세로 범위
            const int MAP_MIN_X = 0;
            const int MAP_MIN_Y = 0;
            const int MAP_MAX_X = 15;
            const int MAP_MAX_Y = 10;
            //플레이어의 기호(string literal)
            //박스의 기호(string literal)
            //플레이어의 이동 방향
            //플레이어의 초기 좌표
            // 박스의 초기 좌표

            const int INITIAL_PLAYER_X = 0;
            const int INITIAL_PLAYER_Y = 0;
            const int INITIAL_BOX_X = 3;
            const int INITIAL_BOX_Y = 3;

            int PlayerX = INITIAL_PLAYER_X;
            int PlayerY = INITIAL_PLAYER_Y;
            int Box1X = INITIAL_BOX_X;
            int Box1Y = INITIAL_BOX_Y;


            PLAYER_DIRECTION playerDIR = new PLAYER_DIRECTION();

            //15 X 10
            // 게임 루프 == 프레임(Frame)

            while (true)
            {
                Console.Clear();
                // --------------------------------------------- Render -------------------------------------------------------
                // 플레이어 출력하기

                Console.SetCursorPosition(Box1X, Box1Y);
                Console.Write("O");
                Console.SetCursorPosition(PlayerX, PlayerY);
                Console.Write("Q");

                // --------------------------------------------- ProcessInput -------------------------------------------------
                ConsoleKey key = Console.ReadKey().Key;
                // --------------------------------------------- Update -------------------------------------------------------
                // 오른쪽 화살표키를
                if (key == ConsoleKey.RightArrow)
                {
                    PlayerX = Math.Min(PlayerX + 1, MAP_MAX_X);
                    playerDIR = PLAYER_DIRECTION.RIGHT;
                }
                if (key == ConsoleKey.LeftArrow)
                {
                    PlayerX = Math.Max(PlayerX - 1, MAP_MIN_X);
                    playerDIR = PLAYER_DIRECTION.LEFT;   
                }
                if (key == ConsoleKey.DownArrow)
                {
                    PlayerY = Math.Min(PlayerY + 1, MAP_MAX_Y);
                    playerDIR = PLAYER_DIRECTION.DOWN;    
                }
                if (key == ConsoleKey.UpArrow)
                {
                    PlayerY = Math.Max(PlayerY - 1, MAP_MIN_Y);
                    playerDIR = PLAYER_DIRECTION.UP;
                    
                }
                //if (PlayerX == Box1X && PlayerY == Box1Y && PlayerDir == 1)
                //{
                //    PlayerX = Math.Min(PlayerX, HorizonMax - 1);
                //    Box1X = Math.Min(Box1X + 1, HorizonMax);
                //}
                //if (PlayerX == Box1X && PlayerY == Box1Y && PlayerDir == 2)
                //{
                //    PlayerX = Math.Max(PlayerX, 1);
                //    Box1X = Math.Max(Box1X - 1, 0);
                //}
                //if (PlayerX == Box1X && PlayerY == Box1Y && PlayerDir == 4)
                //{
                //    PlayerY = Math.Max(PlayerY, 1);
                //    Box1Y = Math.Max(Box1Y - 1, 0);
                //}
                //if (PlayerX == Box1X && PlayerY == Box1Y && PlayerDir == 3)
                //{
                //    PlayerY = Math.Min(PlayerY, VerticalMax - 1);
                //    Box1Y = Math.Min(Box1Y + 1, VerticalMax);
                //}
                if (PlayerX == Box1X && PlayerY == Box1Y)
                {
                    switch (playerDIR)
                    {
                        case PLAYER_DIRECTION.RIGHT: //right
                            PlayerX = Math.Min(PlayerX, MAP_MAX_X - 1);
                            Box1X = Math.Min(Box1X + 1, MAP_MAX_X);
                            break;
                        case PLAYER_DIRECTION.LEFT: //left
                            PlayerX = Math.Max(PlayerX, MAP_MIN_X + 1);
                            Box1X = Math.Max(Box1X - 1, MAP_MIN_X);
                            break;
                        case PLAYER_DIRECTION.DOWN: //down
                            PlayerY = Math.Min(PlayerY, MAP_MAX_Y - 1);
                            Box1Y = Math.Min(Box1Y + 1, MAP_MAX_Y);
                            break;
                        case PLAYER_DIRECTION.UP: //up
                            PlayerY = Math.Max(PlayerY, MAP_MIN_Y + 1);
                            Box1Y = Math.Max(Box1Y - 1, MAP_MIN_Y);
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("[error] 플레이어의 이동방향이 이상합니다.");

                            return; //프로그램 종료

                    }
                }


            }
        }
    }
}