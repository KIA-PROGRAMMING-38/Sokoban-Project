// 초기 세팅
Console.ResetColor();  // 컬러 초기화
Console.CursorVisible = false;  // 콘솔게임에서 커서(깜빡깜빡)가 계속 보이면 거슬리기 때문에 안보이게 설정
Console.Title = "Make Console Game";  // 콘솔 위의 이름 바꿔주기
Console.BackgroundColor = ConsoleColor.DarkGreen;  // 글자 배경색
Console.ForegroundColor = ConsoleColor.Red;  // 글꼴 색

Console.Clear();  // 출력된 모든 내용을 지운다.

// test용 출력
//Console.WriteLine("{0, 11}", "P");  // 왼쪽에 0칸 공백을 만들고 P를 출력 -> 오른쪽으로 이동해서 거기 서있는 걸 이렇게 표현
// 콘솔창에도 좌표계가 있다. 콘솔의 좌상단이 원점 -> 오른쪽으로 가는게 x축으로 양의 방향, 아래가 y축 양의 방향
//int x = 10;
//int y = 5;

//Console.SetCursorPosition(x, y);  // 커서를 위치할 좌표 설정
//Console.Write("P");  // 설정한 좌표에 P 출력해서 이동하는 것을 표현 -> 여러줄 누르고 컨트롤 kc 하면 여러줄 주석 컨트롤 ku하면 주석 해제

//ConsoleKeyInfo keyInfo = Console.ReadKey();  // 현재 눌려진 키에 대한 여러 정보가 들어있음k
//Console.WriteLine(keyInfo.Key);  // 눌려진 키가 무엇인지

