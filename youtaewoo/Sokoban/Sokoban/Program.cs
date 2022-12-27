//초기 세팅
Console.ResetColor(); // 컬러를 초기화한다.
Console.CursorVisible = false; // 커서 숨기기
Console.Title = "미래의babaisyou"; // 타이틀을 설정한다.
Console.BackgroundColor = ConsoleColor.DarkGreen; // 배경색을 설정한다.
Console.ForegroundColor = ConsoleColor.White; //글꼴색을 설정한다.
Console.Clear(); // 출력된 모든 내용을 지운다.

//int x = 12;
//int y = 6;
//Console.SetCursorPosition(x, y);
//Console.Write("B"); //여기서 개행하면 안된다.

ConsoleKeyInfo KeyInfo = Console.ReadKey();
Console.WriteLine(KeyInfo.Key);
