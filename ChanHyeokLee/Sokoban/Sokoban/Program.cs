//초기 세팅
Console.ResetColor(); //컬러를 초기화한다.
Console.CursorVisible = false; // 커서를 없애준다.
Console.Title = "소코반"; //타이틀 이름
Console.BackgroundColor = ConsoleColor.Red; // 배경색을 설정한다.
Console.ForegroundColor = ConsoleColor.White; // 글꼴 색을 설정한다.
Console.Clear(); // 출력된 모든 내용을 지운다.

//int x = 10;
//int y = 5;
//Console.SetCursorPosition(x, y);
//Console.Write("L");
ConsoleKeyInfo keyInfo = Console.ReadKey();
Console.WriteLine(keyInfo.Key);
/*while(true)
{
    Processinput0; // 입력을 처리한다.
    Update(); // 게임 데이터를 업데이트한다.
    Render(); // 업데이트 된 데이터를 바탕으로 화면에 출력한다.
}*/


