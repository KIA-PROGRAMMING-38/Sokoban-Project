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

int playerX = 0;
int playerY = 0;

int boxX = 5;
int boxY = 3;

// 15 * 10 맵 크기를 제한할 것임 (폰트때문에 가로가 좀 더 넓어야 예쁨)


// 게임 루프 == 프레임(Frame)
while (true)
{
    Console.Clear();  // 이전 프레임을 지운다. 게임루프 한 번이 한 프레임이기 때문에 이전 프레임의 내용을 지워주면 실시간으로 이동하는 것처럼 보인다(이전 프레임 지우고 입력이 있으면 그만큼 옆으로)
    // -------------------------------- Render ---------------------------------
    // 플레이어 출력하기
    Console.SetCursorPosition(playerX, playerY);
    Console.Write("P");

    Console.SetCursorPosition(boxX, boxY);
    Console.Write("O");

    // -------------------------------- ProcessInput ------------------------------
    ConsoleKey key = Console.ReadKey().Key;  // Console.ReadyKey()를 하면 나오는 결과가 ConsoleKeyInfo 타입이라는 것을 알 수 있다. 그렇기 때문에 바로 .Key를 붙여서 ConsoleKey 타입이 나오고, 사용할 수 있는 것
    // ConsoleKey와 ConsoleKeyInfo는 서로 다른 타입이다. ConsoleKeyInfo에 ConsoleKey 타입의 정보가 포함된다.따라서 프로퍼티를 통해 접근하여 가져올 수 있다.

    



    // -------------------------------- Update ----------------------------------
    // 오른쪽 화살표 키를 눌렀을 때 오른쪽으로 이동
    // 오르쪽 화살표 키를 눌렀을 때
    if(key == ConsoleKey.RightArrow)
    {
        // 오른쪽으로 이동, 콘솔 범위를 벗어나면 예외 발생
        //playerX += 1;

        //if(playerX == boxX - 1 && playerY == boxY)
        //{
        //    boxX = boxX < 15 ? boxX + 1 : boxX;
        //}

        // 오른쪽으로 이동
        // playerX = Math.Min(playerX + 1, 15);

        if(playerX == boxX - 1 && playerY == boxY)
        {
            boxX = Math.Min(boxX + 1, 15);
        }
        playerX = Math.Min(playerX + 1, 15);
    }

    if (key == ConsoleKey.LeftArrow)
    {
        //playerX -= 1;

        //if(playerX == boxX + 1 && playerY == boxY)
        //{
        //    boxX = boxX > 0 ? boxX - 1 : boxX;
        //}

        // playerX = Math.Max(0, playerX - 1);

        if(playerX == boxX +1 && playerY == boxY)
        {
            boxX = Math.Max(0, boxX - 1);
        }

        playerX = Math.Max(0, playerX - 1);
    }

    // 키 입력 구현할 때는 보통 if else 말고 if만 사용해서 각 키에 대한 동작을 구현한다.
    if (key == ConsoleKey.UpArrow)
    {
        //playerY -= 1;

        //if(playerY == boxY + 1 && playerX == boxX)
        //{
        //    boxY = boxY > 1 ? boxY - 1 : boxY;
        //}

        // playerY = Math.Max(0, playerY - 1);

        if(playerY == boxY + 1 && playerX == boxX)
        {
            boxY = Math.Max(0, boxY - 1);
        }

        playerY = Math.Max(0, playerY - 1);
    }

    if(key == ConsoleKey.DownArrow)
    {
        //playerY += 1;

        //if(playerX == boxX - 1 && playerY == boxY)
        //{
        //    boxY = boxY < 10 ? boxY + 1 : boxY;
        //}

        // playerY = Math.Min(playerY + 1, 10);

        if(playerY == boxY - 1 && playerX == boxX)
        {
            boxY = Math.Min(boxY + 1, 10);
        }

        playerY = Math.Min(playerY + 1, 10);

        // 처음에 플레이어 좌표를 박스 if문보다 위에 작성했는데, 그렇게 하면 바뀐 거에 대해서 검사를 해서
        // 우리가 보이는 화면에서 기대하는 동작이 안나온다 (생각해보면 그럼)
        // 들어가는 수가 뭔지 보고 해야됨
    }


}