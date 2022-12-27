// 초기 세팅
Console.ResetColor(); // 컬러를 초기화한다
Console.CursorVisible = true; // 커서를 안보이게 해줌(콘솔에서는 입력하려는 표시인거임)
Console.Title = "소코반 게임!"; // 콘솔창의 제목을 입력
Console.BackgroundColor = ConsoleColor.Cyan; // 배경색을 바꿔줌
Console.ForegroundColor = ConsoleColor.Red; // 글씨의 색을 바꿔줌
Console.Clear(); // 출력된 모든 내용을 지운다


//int x = 50;
//int y = 30;
//Console.SetCursorPosition(x, y);
//Console.Write("H");

ConsoleKeyInfo keyinfo = Console.ReadKey();
Console.WriteLine(keyinfo.Key);

