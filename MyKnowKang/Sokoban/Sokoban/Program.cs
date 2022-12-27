using System;

class Program
{
    static void Main()
    {
        // 초기 세팅..
        Console.ResetColor();                               // 컬러를 초기화한다..
        Console.CursorVisible = false;                      // 커서를 숨긴다..
        Console.Title = "Welcome To Liverpool";             // 타이틀을 설정한다..
        Console.BackgroundColor = ConsoleColor.DarkRed;     // Background 색을 설정한다..
        Console.ForegroundColor = ConsoleColor.White;       // 글꼴색을 설정한다..
        Console.Clear();                                    // 출력된 모든 내용을 지운다..

        // 콘솔 최대 사이즈 119, 29..
    }
}