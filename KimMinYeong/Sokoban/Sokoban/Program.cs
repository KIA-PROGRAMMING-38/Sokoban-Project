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
int playerDirection = 0;  // 0: None(기본값), 1: left, 2: right, 3: up, 4: down 방향으로 움직였다고 설정

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
    // 앞, 옆 뒤에 뭐가 있을 때 플레이어 이동 따로 구현
    // 키가 들어왔을 때 앞 옆 뒤에 있는게 P라면 이동하도록 구현

    if(key == ConsoleKey.RightArrow)
    {
        // 오른쪽으로 이동, 콘솔 범위를 벗어나면 예외 발생
        //playerX += 1;

        //if(playerX == boxX - 1 && playerY == boxY)
        //{
        //    boxX = boxX < 15 ? boxX + 1 : boxX;
        //}

        // 오른쪽으로 이동
        playerX = Math.Min(playerX + 1, 15);
        playerDirection = 2;

        //if(playerX == boxX - 1 && playerY == boxY)
        //{
        //    boxX = Math.Min(boxX + 1, 15);
        //}

        //if(playerX == boxX - 1 && playerX == 14)
        //{

        //}
        //else
        //{
        //    playerX = Math.Min(playerX + 1, 15);
        //}
    }

    if (key == ConsoleKey.LeftArrow)
    {
        //playerX -= 1;

        //if(playerX == boxX + 1 && playerY == boxY)
        //{
        //    boxX = boxX > 0 ? boxX - 1 : boxX;
        //}

        // playerX = Math.Max(0, playerX - 1);

        //if(playerX == boxX +1 && playerY == boxY)
        //{
        //    boxX = Math.Max(0, boxX - 1);
        //}

        playerX = Math.Max(0, playerX - 1);
        playerDirection = 1;
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

        //if(playerY == boxY + 1 && playerX == boxX)
        //{
        //    boxY = Math.Max(0, boxY - 1);
        //}

        playerY = Math.Max(0, playerY - 1);
        playerDirection = 3;
    }

    if(key == ConsoleKey.DownArrow)
    {
        //playerY += 1;

        //if(playerX == boxX - 1 && playerY == boxY)
        //{
        //    boxY = boxY < 10 ? boxY + 1 : boxY;
        //}

        // playerY = Math.Min(playerY + 1, 10);

        //if(playerY == boxY - 1 && playerX == boxX)
        //{
        //    boxY = Math.Min(boxY + 1, 10);
        //}

        playerY = Math.Min(playerY + 1, 10);
        playerDirection = 4;

        // 처음에 플레이어 좌표를 박스 if문보다 위에 작성했는데, 그렇게 하면 바뀐 거에 대해서 검사를 해서
        // 우리가 보이는 화면에서 기대하는 동작이 안나온다 (생각해보면 그럼)
        // 들어가는 수가 뭔지 보고 해야됨
    }

    // 박스 업데이트는 플레이어 업데이트랑 분리해야 유지보수가 수월해짐(확장성)
    // 여러 이동이 한번에 일어날 경우, 두 움직임에 대한 동작을 따로 작성 (하나의 움직임이 끝난 후 다른 움직임)
    // 플레이어가 이동한 후 박스에 대한 이동
    if (playerX == boxX && playerY == boxY)  // 플레이어가 이동하고나니 박스가 있네
    {
        // 플레이어가 이동했을 때, 박스가 있으면 움직여주는 것으로 박스를 미는 것을 구현
        // 플레이어가 어느 방향에서 왔는지에 대한 정보가 필요, 따라서 플레이어 이동방향을 저장할 playerDirection 데이터 생성
        // 이동 방향을 입력한 키로 인식하는 경우, 후에 플레이어가 작동 키를 바꾸는 경우 적용되지 않기 때문에 보통 키와 동작을 연결시키지 않는다
        // 따라서 playerDirection 이라는 데이터로 처리하는 것이다 (입력한 방향키로 처리하는 것이 아니라)
        // 주어진 데이터만 사용하는 것이 아니라, 추가로 필요한 데이터를 직접 정의하여 사용할 수 있어야 한다.
        
        switch (playerDirection)
        {
            case 1: // 플레이어가 왼쪽으로 이동 중
                if (boxX == 0) // 박스가 왼쪽 끝에 있다면
                {
                    playerX = 1;
                }
                else
                {
                    boxX = boxX - 1;
                }
                
                break;
            case 2:  // 플레이어가 오른쪽으로 이동 중
                if(boxX == 15)
                {
                    playerX = 14;
                }
                else
                {
                    boxX = boxX + 1;
                }
                
                break;
            case 3:  // 플레이어가 위쪽으로 이동 중
                if(boxY == 0)
                {
                    playerY = 1;
                }
                else
                {
                    boxY = boxY - 1;
                }
                
                break;
            case 4:  // 플레이어가 아래로 이동 중
                if (boxY == 10)
                {
                    playerY = 9;
                }
                else
                {
                    boxY = boxY + 1;
                }

                break;
            default:  // 이 경우는 우리가 설정한 조건이 아니기에 오류로 판정, 이처럼 보통 defualt는 오류 처리에 사용
                Console.Clear();
                Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다. {playerDirection}");

                return;
        }
    }

}