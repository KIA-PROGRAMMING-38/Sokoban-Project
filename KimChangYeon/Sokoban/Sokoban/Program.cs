//초기 세팅
Console.ResetColor();                            // 컬러를 초기화 한다
Console.CursorVisible = false;                   // 커서를 숨긴다
Console.Title = "홍성재의 썬더펀치";               // 타이틀을 설정한다.
Console.BackgroundColor = ConsoleColor.DarkGray; //배경색을 설정한다.
Console.ForegroundColor = ConsoleColor.DarkBlue; //글꼴색을 설정한다.
Console.Clear();                                 //출력된 모든 내용을 지운다.



int PlayerX = 0;
int PlayerY = 0;
int PlayerDir = 0; // 1 : right, 2 : left, 3 : down, 4 : up
int Box1X = 3;
int Box1Y = 3;
int VerticalMax = 15;
int HorizonMax = 20;
//15 X 10
// 게임 루프 == 프레임(Frame)

while (true)
{
    Console.Clear();
    // --------------------------------------------- Render -------------------------------------------------------
    // 플레이어 출력하기

    Console.SetCursorPosition(Box1X, Box1Y);
    Console.Write("■");
    Console.SetCursorPosition(PlayerX, PlayerY);
    Console.Write("♬");

    // --------------------------------------------- ProcessInput -------------------------------------------------
    ConsoleKey key = Console.ReadKey().Key;
    // --------------------------------------------- Update -------------------------------------------------------
    // 오른쪽 화살표키를
    if (key == ConsoleKey.RightArrow)
    {
        PlayerX = Math.Min(PlayerX + 1, HorizonMax);
        PlayerDir = 1;
    }
    if (key == ConsoleKey.LeftArrow)
    {
        PlayerX = Math.Max(PlayerX - 1, 0);
        PlayerDir = 2;
    }
    if (key == ConsoleKey.DownArrow)
    {
        PlayerY = Math.Min(PlayerY + 1, VerticalMax);
        PlayerDir = 3;
    }
    if (key == ConsoleKey.UpArrow)
    {
        PlayerY = Math.Max(PlayerY - 1, 0);
        PlayerDir = 4;
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
        switch(PlayerDir)
        {
            case 1:
                PlayerX = Math.Min(PlayerX, HorizonMax - 1);
                Box1X = Math.Min(Box1X + 1, HorizonMax);
                break;
            case 2:
                PlayerX = Math.Max(PlayerX, 1);
                Box1X = Math.Max(Box1X - 1, 0);
                break;
            case 3:
                PlayerY = Math.Min(PlayerY, VerticalMax - 1);
                Box1Y = Math.Min(Box1Y + 1, VerticalMax);
                break;
            case 4:
                PlayerY = Math.Max(PlayerY, 1);
                Box1Y = Math.Max(Box1Y - 1, 0);
                break;
            default:
                Console.Clear();
                Console.WriteLine("[error] 플레이어의 이동방향이 이상합니다.");

                return; //프로그램 종료

        }
    }


}