﻿// 초기 세팅
Console.ResetColor();                           // 컬러를 초기화한다.
Console.CursorVisible = false;                  // 커서를 숨긴다.
Console.Title = "Sokoban";                      // 타이틀을 설정한다.
Console.BackgroundColor = ConsoleColor.Blue;    // 배경색을 설정한다.
Console.ForegroundColor = ConsoleColor.Yellow;  // 글꼴색을 설정한다.
Console.Clear();                                // 출력된 모든 내용을 지운다.


//int x = 10;
//int y = 5;
//Console.SetCursorPosition(x, y);
//Console.Write("sokoban");
//Console.ReadKey();

ConsoleKeyInfo keyInfo = Console.ReadKey();
Console.WriteLine(keyInfo.Key);


