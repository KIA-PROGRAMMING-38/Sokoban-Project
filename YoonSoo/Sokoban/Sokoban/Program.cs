// 초기
using System.Data;

Console.ResetColor();                              // 컬러를 초기화한다.
Console.CursorVisible = false;                     // 커서를 숨긴다.
Console.Title = "어우석 바보";                      // 타이틀을 설정한다.
Console.BackgroundColor = ConsoleColor.DarkGreen;  // 배경을 설정한다.
Console.ForegroundColor = ConsoleColor.Black;      //글꼴을 설정한다.
Console.Clear();                                   // 출려된 모든 내용을 지운다.



//int x = 10;
//int y = 5;
//Console.SetCursorPosition(x, y);
//Console.Write("W");

ConsoleKeyInfo keyInfo = Console.ReadKey();
Console.WriteLine(keyInfo.Key);

//Console.ReadLine();  // Block 멈추면 안됨 










//while (true)

    //ProcessInput();
   // Update();
    //Render();

//Console.WriteLine("나은수 바봉");



// Console.WriteLine("{0, 11", "P");  // ("           P");
