namespace Sokoban
{
    class Program
    {
        static void Main()
        {

            // 초기 세팅
            Console.ResetColor();
            Console.CursorVisible = false; // 커서를 숨긴다. 블리언 타입입니다.
            Console.Title = "김도익 훈남"; // 타이틀을 설정한다.
            Console.BackgroundColor = ConsoleColor.DarkBlue; // 배경색을 설정한다.
            Console.ForegroundColor = ConsoleColor.White; // 글꼴색을 설정한다.
            Console.Clear(); // 출력된 모든 내용을 지운다.


            // key와 코드를 같이 묶어서 사용하면 좋지 않습니다.
            // 하나의 object를 끝내고 다음단계로 넘어가야 코드가 깔끔해집니다.
            // 플레이어 좌표 설정

            const int PLAYERX_START = 0;
            const int PLAYERY_START = 0;

            int playerX = PLAYERX_START;
            int playerY = PLAYERY_START;

            const int DIRECTION_LEFT = 1;
            const int DIRECTION_RIGHT = 2;
            const int DIRECTION_UP = 3;
            const int DIRECTION_DOWN = 4;
            int playerDirection = 0;

            //플레이어의 기호
            const string PLAYER = "P";

            // 플레이어의 이동방향 -  1 : Left, 2 : Right, 3 : Up, 4 : Down (교수님이 직접 지정한 규칙)
            // 프로그래머는 규칙을 정할 줄 알아야한다.
            // 플레이어의 이동방향을 정해준다면 유지보수가 더 편해진다.

            //박스 좌표 설정
            const int BOXX_START = 1;
            const int BOXY_START = 5;

            int BoxX = BOXX_START;
            int BoxY = BOXY_START;

            // MAP 제한선 설정
            const int LIMIT_X = 15;
            const int LIMIT_Y = 10;
            const int LIMIT_YY = 0;
            const int LIMIT_XX = 0;




            // 게임루프 == 프레임
            // 가로 15, 세로 10으로 제한
            while (true)
            {
                //이전 프레임 지운다.
                Console.Clear();

                //-------------------------------Render--------------------------- //플레이어가 무슨 일이 일어났는지 볼 수 있도록 게임을 그립니다
                // 플레이어 출력
                Console.SetCursorPosition(playerX, playerY); // 커서의 위치를 세팅해준다. (글을 쓸때 깜빡깜빡하는 것을 커서라고 합니다.)
                Console.Write("K");

                // 박스 출력하기
                Console.SetCursorPosition(BoxX, BoxY);
                Console.Write("B");

                //------------------------------ProcessInput--------------------- // 마지막 호출 이후 발생한 모든 사용자 입력을 처리합니다.
                ConsoleKey Key = Console.ReadKey().Key; //키를 입력받기 위해서 만든 명령어



                //----------------------------------Update--------------------- // 게임 시뮬레이션을 한 단계 진행합니다.
                //오른쪽 화살표 키를 눌렀을 때
                if (Key == ConsoleKey.RightArrow)
                {
                    playerX = Math.Min(playerX + 1, LIMIT_X); // Math.Min(A,B) A와 B중 더 작은 놈을 고르겠다.(동일 값 포함)
                    playerDirection = DIRECTION_RIGHT;

                }

                if (Key == ConsoleKey.LeftArrow)
                {
                    playerX = Math.Max(0, playerX - 1); // Math.Max(A, B) A와 B중 더 큰놈을 고르겠다.
                    playerDirection = DIRECTION_LEFT;
                }

                if (Key == ConsoleKey.UpArrow)
                {
                    playerY = Math.Max(0, playerY - 1);
                    playerDirection = DIRECTION_UP;
                }

                if (Key == ConsoleKey.DownArrow)
                {
                    playerY = Math.Min(playerY + 1, LIMIT_Y);
                    playerDirection = DIRECTION_DOWN;


                }

                // 박스 업데이트
                // 플레이어가 이동한 후
                if (playerX == BoxX && playerY == BoxY) // 플레이어가 이동한 위치에 박스가 있네?
                {
                    // 박스를 움직여주면 된다.
                    // 플레이어가 어느 방향에서 왔는지에 따라 박스의 위치가 달라진다.
                    switch (playerDirection)
                    {
                        case DIRECTION_LEFT: // 플레이어가 왼족으로 이동 중
                            if (BoxX == LIMIT_XX) // 박스가 왼쪽 끝에 있다면?
                            {
                                playerX = LIMIT_XX + 1;
                            }
                            else
                            {
                                BoxX = playerX - 1;
                            }

                            break;

                        case DIRECTION_RIGHT: // 플레이어가 오른쪽으로 이동중
                            if (BoxX == LIMIT_Y)
                            {
                                playerX = LIMIT_Y - 1;
                            }
                            else
                            {
                                BoxX = playerX + 1;
                            }
                            break;

                        case DIRECTION_UP:// 플레이어가 위쪽으로 이동중
                            if (BoxY == LIMIT_YY)
                            {
                                playerY = LIMIT_YY + 1;
                            }
                            else
                            {
                                BoxY = playerY - 1;
                            }
                            break;

                        case DIRECTION_DOWN:// 플레이어가 아래쪽으로 이동중
                            if (BoxY == LIMIT_Y)
                            {
                                playerY = LIMIT_Y - 1;
                            }
                            else
                            {
                                BoxY = playerY + 1;
                            }
                            break;

                        default:
                            Console.Clear();
                            Console.Write($"[Error] 플레이어의 이동 방향이 잘못 되었습니다. {playerDirection}");

                            return;
                    }

                }
            }
        }
    }
}




