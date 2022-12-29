namespace Sokoban
{
    // 모든 기호상수를 열거형으로 바꿀 필요는 없다.
    // 초기 좌표같은 부분은 기호상수 그대로 사용하는 것이 오히려 편하고,
    // 데이터의 제한이 필요하다는 용도도 사용할 수 있도록.. 잘 사용한다

    // 열거형은 타입이기 때문에 타입으로 된 객체를 만들어서 값을 넣었을 때 가독성이 있는 것을 열거형으로 사용
    // 단순히 기호 상수로 지정한 이름이 비슷하다는 것만으로 열거형으로 만들 필요는 없다.
    enum Direction
    {
        None = 0,
        LEFT = 1,
        RIGHT = 2,
        UP = 3,
        DOWN = 4
    }

    // 처음엔 맵의 최대, 최소 범위도 열거형으로 만들었는데,
    // 객체로 만들어 사용하지 않고 단순히 이 열거형의 값을 그대로 가져와서 썼다. 이런 경우는 그냥 기호상수로만 사용해도 될듯...
    // 심지어 쓸 때마다 int로 형변환해서 사용함.. 깔끔하지 않다 -> 숫자 자체가 유의미하기 때문
    // 여기의 데이터는 0, 10, 15가 int 그 자체로서 의미를 가지고있다. (유의미한 데이터) 그럼 int 타입을 사용하는 것이 나음
    // 위의 Direction은 1, 2, 3, 4는 우리가 정의한 것이지 그 숫자 자체가 의미를 가지고있지는 않다.
    //enum MapLimit
    //{
    //    MIN_X = 0,
    //    MIN_Y = 0,
    //    MAX_X = 15,
    //    MAX_Y = 10
    //}

class Program
    {
        static void Main()
        {
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

            const string PLAYER_ICON = "P";
            const int PLAYER_INITIAL_POSITION_X = 0;
            const int PLAYER_INITIAL_POSITION_Y = 0;
            int playerX = PLAYER_INITIAL_POSITION_X;  // 난 처음에 루프 밖에 첫 위치를 그려주는 코드 작성 후 루프에 들어가게 했는데, 그냥 기존 코드 유지하고 좌표의 초기값을 상수로 해주면 된다..!!
            int playerY = PLAYER_INITIAL_POSITION_Y;

            // 0: None(기본값), 1: left, 2: right, 3: up, 4: down 방향으로 움직였다고 설정
            Direction playerDirection = Direction.None;

            const string BOX_ICON = "O";
            const int BOX_INITIAL_POSITION_X = 5;
            const int BOX_INITIAL_POSITION_Y = 3;
            int boxX = BOX_INITIAL_POSITION_X;
            int boxY = BOX_INITIAL_POSITION_Y;

            // 벽의 기호, 좌표
            const string WALL_STRING = "W";
            const int INITIAL_WALL_X = 7;
            const int INITIAL_WALL_Y = 8;

            const int MIN_OF_MAP_X = 0;
            const int MAX_OF_MAP_X = 15;
            const int MIN_OF_MAP_Y = 0;
            const int MAX_OF_MAP_Y = 10;

            // 15 * 10 맵 크기를 제한할 것임 (폰트때문에 가로가 좀 더 넓어야 예쁨)

            // 기호 상수로 바꿔야하는 값들 (매직넘버)
            // 맵의 가로 범위, 세로 범위
            // 플레이어의 기호 (string literal)
            // 박스의 기호 (string literal)
            // 플레이어의 이동 방향
            // 플레이어의 초기 좌표
            // 박스의 초기 좌표

            // 게임 루프 == 프레임(Frame)
            while (true)
            {
                Console.Clear();  // 이전 프레임을 지운다. 게임루프 한 번이 한 프레임이기 때문에 이전 프레임의 내용을 지워주면 실시간으로 이동하는 것처럼 보인다(이전 프레임 지우고 입력이 있으면 그만큼 옆으로)
                
                // -------------------------------- Render ---------------------------------

                // 벽 출력하기
                Console.SetCursorPosition(INITIAL_WALL_X, INITIAL_WALL_Y);
                Console.Write(WALL_STRING);

                // 플레이어가 벽에의해 막히는 것을 구현
                if(playerX == INITIAL_WALL_X && playerY == INITIAL_WALL_Y)
                {
                    switch(playerDirection)
                    {
                        case Direction.RIGHT:
                            playerX -= 1;
                            break;
                        case Direction.LEFT:
                            playerX += 1;
                            break;
                        case Direction.UP:
                            playerY += 1;
                            break;
                        case Direction.DOWN:
                            playerY -= 1;
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다. {playerDirection}");
                            return;
                    }
                }

                // 플레이어 출력하기
                Console.SetCursorPosition(playerX, playerY);
                Console.Write(PLAYER_ICON);

                Console.SetCursorPosition(boxX, boxY);
                Console.Write(BOX_ICON);

                // -------------------------------- ProcessInput ------------------------------
                ConsoleKey key = Console.ReadKey().Key;  // Console.ReadyKey()를 하면 나오는 결과가 ConsoleKeyInfo 타입이라는 것을 알 수 있다. 그렇기 때문에 바로 .Key를 붙여서 ConsoleKey 타입이 나오고, 사용할 수 있는 것
                                                         // ConsoleKey와 ConsoleKeyInfo는 서로 다른 타입이다. ConsoleKeyInfo에 ConsoleKey 타입의 정보가 포함된다.따라서 프로퍼티를 통해 접근하여 가져올 수 있다.





                // -------------------------------- Update ----------------------------------
                // 오른쪽 화살표 키를 눌렀을 때 오른쪽으로 이동
                // 오르쪽 화살표 키를 눌렀을 때
                // 앞, 옆 뒤에 뭐가 있을 때 플레이어 이동 따로 구현
                // 키가 들어왔을 때 앞 옆 뒤에 있는게 P라면 이동하도록 구현

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

                    playerX = Math.Max(MIN_OF_MAP_X, playerX - 1);
                    playerDirection = Direction.LEFT;
                }

                if (key == ConsoleKey.RightArrow)
                {
                    // 오른쪽으로 이동, 콘솔 범위를 벗어나면 예외 발생
                    //playerX += 1;

                    //if(playerX == boxX - 1 && playerY == boxY)
                    //{
                    //    boxX = boxX < 15 ? boxX + 1 : boxX;
                    //}

                    // 오른쪽으로 이동
                    playerX = Math.Min(playerX + 1, MAX_OF_MAP_X);
                    playerDirection = Direction.RIGHT;

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

                    playerY = Math.Max(MIN_OF_MAP_Y, playerY - 1);
                    playerDirection = Direction.UP;
                }

                if (key == ConsoleKey.DownArrow)
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

                    playerY = Math.Min(playerY + 1, MAX_OF_MAP_Y);
                    playerDirection = Direction.DOWN;

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
                        case Direction.LEFT: // 플레이어가 왼쪽으로 이동 중
                            if (boxX == MIN_OF_MAP_X) // 박스가 왼쪽 끝에 있다면
                            {
                                playerX = MIN_OF_MAP_X + 1;
                            }
                            else
                            {
                                boxX = boxX - 1;
                            }

                            break;
                        case Direction.RIGHT:  // 플레이어가 오른쪽으로 이동 중
                            if (boxX == MAX_OF_MAP_X)
                            {
                                playerX = MAX_OF_MAP_X - 1;
                            }
                            else
                            {
                                boxX = boxX + 1;
                            }

                            break;
                        case Direction.UP:  // 플레이어가 위쪽으로 이동 중
                            if (boxY == MIN_OF_MAP_Y)
                            {
                                playerY = MIN_OF_MAP_Y + 1;
                            }
                            else
                            {
                                boxY = boxY - 1;
                            }

                            break;
                        case Direction.DOWN:  // 플레이어가 아래로 이동 중
                            if (boxY == MAX_OF_MAP_Y)
                            {
                                playerY = MAX_OF_MAP_Y - 1;
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

                if (boxX == INITIAL_WALL_X && boxY == INITIAL_WALL_Y)
                {
                    switch (playerDirection)
                    {
                        case Direction.RIGHT:
                            playerX -= 1;
                            boxX -= 1;
                            break;
                        case Direction.LEFT:
                            playerX += 1;
                            boxX += 1;
                            break;
                        case Direction.UP:
                            playerY += 1;
                            boxY += 1;
                            break;
                        case Direction.DOWN:
                            playerY -= 1;
                            boxY -= 1;
                            break;
                    }
                }
            }
        }
    }
}