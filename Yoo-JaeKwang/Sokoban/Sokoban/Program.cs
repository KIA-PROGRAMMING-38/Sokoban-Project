// 초기 세팅
Console.ResetColor(); // 컬러를 초기화한다.
Console.CursorVisible = false;
Console.Title = "경이루 아카데미";
Console.BackgroundColor = ConsoleColor.Magenta;
Console.ForegroundColor = ConsoleColor.Yellow;
Console.Clear();

//int x = 10;
//int y = 5;
//Console.SetCursorPosition(x, y);
//Console.Write("P");

ConsoleKeyInfo keyInfo = Console.ReadKey();
Console.WriteLine(keyInfo.Key);