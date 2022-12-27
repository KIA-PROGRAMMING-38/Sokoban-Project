//초기 세팅
Console.ResetColor();                            // 컬러를 초기화 한다
Console.CursorVisible = false;                   // 커서를 숨긴다
Console.Title = "홍성재의 썬더펀치";               // 타이틀을 설정한다.
Console.BackgroundColor = ConsoleColor.DarkGray; //배경색을 설정한다.
Console.ForegroundColor = ConsoleColor.DarkBlue; //글꼴색을 설정한다.
Console.Clear();                                 //출력된 모든 내용을 지운다.



int PlayerX = 0;
int PlayerY = 0;
int BoxX = 3;
int BoxY = 3;
//15 X 10
// 게임 루프 == 프레임(Frame)

while (true)
{
    Console.Clear();
    // --------------------------------------------- Render -------------------------------------------------------
    // 플레이어 출력하기

    Console.SetCursorPosition(BoxX, BoxY);
    Console.Write("■");
    Console.SetCursorPosition(PlayerX, PlayerY);
    Console.Write("♬");

    // --------------------------------------------- ProcessInput -------------------------------------------------
    ConsoleKey key = Console.ReadKey().Key;
    // --------------------------------------------- Update -------------------------------------------------------
    // 오른쪽 화살표키를
    if (key == ConsoleKey.RightArrow)
    {
        PlayerX = Math.Min(PlayerX + 1, 15);
    }
    if (key == ConsoleKey.LeftArrow)
    {
        PlayerX = Math.Max(PlayerX - 1, 0);
    }
    if (key == ConsoleKey.DownArrow)
    {
        PlayerY = Math.Min(PlayerY + 1, 10);
    }
    if (key == ConsoleKey.UpArrow)
    {
        PlayerY = Math.Max(PlayerY - 1, 0);
    }

    if (PlayerX == BoxX && PlayerY == BoxY && key == ConsoleKey.RightArrow)
    {
        PlayerX = Math.Min(BoxX, 14);
        BoxX = Math.Min(BoxX + 1, 15);
    }   
    if (PlayerX == BoxX && PlayerY == BoxY && key == ConsoleKey.LeftArrow)
    {
        PlayerX = Math.Min(BoxX, 14);
        BoxX = Math.Min(BoxX - 1, 15);
    }
    if (PlayerX == BoxX && PlayerY == BoxY && key == ConsoleKey.UpArrow)
    {
        PlayerY = Math.Min(BoxY, 9);
        BoxY = Math.Min(BoxY - 1, 10);
    }
    if (PlayerX == BoxX && PlayerY == BoxY && key == ConsoleKey.DownArrow)
    {
        PlayerY = Math.Min(BoxY, 9);
        BoxY = Math.Min(BoxY + 1, 10);
    }
    // 눌렀을 때 오른쪽으로 이동




}