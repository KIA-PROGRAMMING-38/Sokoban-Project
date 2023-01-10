using System;

namespace socobanS
{
    using System;

    namespace sososo
    {
        enum Direction // 방향을 저장하는 열거형 타입
        {
            None,
            Left,
            Right,
            Up,
            Down
        }

        class Program
        {
            static void Main()
            {

                int Max(int a, int b)
                {
                    int result = 0;
                    if (a < b)
                    {
                        result = b;
                    }
                    else
                    {
                        result = a;
                    }
                    return result;

                }
                int Min(int a, int b) => (a < b) ? a : b;



                //초기 세팅
                Console.ResetColor();                          // 컬러를 초기화
                Console.CursorVisible = false;                 // 커서를 숨기기
                Console.Title = "진우의 소코반놀이";            // 타이틀 설정
                Console.BackgroundColor = ConsoleColor.Cyan; // 배경색 설정
                Console.ForegroundColor = ConsoleColor.Black; // 글꼴색 설정
                Console.Clear();                              // 잡다한 출력 내용 지우기.
                                                              // 프레임워크 > 프로그램의 동작 순서 .

                // 기호 상수 정의
                const int GOAL_COUNT = 3;
                const int BOX_COUNT = GOAL_COUNT;

                // 플레이어 위치를 저장하기 위한 변수 생성
                int PlayerX = 0;
                int PlayerY = 0;

                // 플레이어 이동 방향을 저장하기 위한 변수
                Direction PlayerMoveDirection = Direction.None;

                // 플레이어가 무슨 박스를 밀고 있는지 저장하기 위한 변수
                int pushedBoxId = 0; // 1이면 박스1, 2면 박스2



                // 박스의 좌표
                const int Box1X = 5;
                const int Box1Y = 3;

                const int Box2X = 4;
                const int Box2Y = 8;

                const int Box3X = 18;
                const int Box3Y = 4;

                // 벽의 좌표 
                const int Wall1X = 19;
                const int Wall1Y = 6;

                const int Wall2X = 14;
                const int Wall2Y = 7;

                const int Wall3X = 12;
                const int Wall3Y = 4;

                // 골의 좌표
                const int Goal1X = 19;
                const int Goal1Y = 9;

                const int Goal2X = 12;
                const int Goal2Y = 5;

                const int Goal3X = 13;
                const int Goal3Y = 7;
                // 배열화 하는이유 : 추가적으로 예외처리를 안해도됨.
                // 박스 좌표 배열화 
                int[] BoxX = { Box1X, Box2X, Box3X };
                int[] BoxY = { Box1Y, Box2Y, Box3Y };

                int[] WallX = { Wall1X, Wall2X, Wall3X };
                int[] WallY = { Wall1Y, Wall2Y, Wall3Y };

                int[] GoalX = { Goal1X, Goal2X, Goal3X };
                int[] GoalY = { Goal1Y, Goal2Y, Goal3Y };

                bool[] BoxAndGoal = new bool[BOX_COUNT];

                // 게임 루프 구성
                while (true)
                {
                    // ------------------------------Render-------------------------------
                    // 이전 프레임을 지운다. 
                    Console.Clear(); // 반복의 이전화면 지우기
                                     // 플레이어를 게임에 그려야함.
                    Console.SetCursorPosition(PlayerX, PlayerY);
                    Console.Write("P");
                    // 골 위치 설정 및 출력
                    for (int i = 0; i < GOAL_COUNT; ++i)
                    {
                        Console.SetCursorPosition(GoalX[i], GoalY[i]);
                        Console.Write("G");

                    }
                    // 박스 위치 설정 및 출력
                    for (int i = 0; i < BOX_COUNT; ++i)
                    {

                        Console.SetCursorPosition(BoxX[i], BoxY[i]);
                        Console.Write("B");

                    }

                    // 벽 위치 설정 및 출력
                    for (int i = 0; i < WallX.Length; ++i)
                    {
                        Console.SetCursorPosition(WallX[i], WallY[i]);
                        Console.Write("W");


                    }

                    //박스와 골이 만났을때 출력 다르게.

                    for (int i = 0; i < BOX_COUNT; ++i)
                    {
                        for (int j = 0; j < GOAL_COUNT; ++j)
                        {
                            if (BoxX[i] == GoalX[j] && BoxY[i] == GoalY[j])
                            {
                                Console.SetCursorPosition(BoxX[i], BoxY[i]);
                                Console.Write("C");
                                break;

                            }
                        }
                    }


                    // ------------------------------processInput-------------------------
                    ConsoleKey Key = Console.ReadKey().Key;



                    // ------------------------------update-------------------------------

                    // 플레이어 이동
                    if (Key == ConsoleKey.LeftArrow)
                    {
                        PlayerX = Max(0, PlayerX - 1);
                        PlayerMoveDirection = Direction.Left;

                    }
                    if (Key == ConsoleKey.RightArrow)
                    {
                        PlayerX = Min(20, PlayerX + 1);
                        PlayerMoveDirection = Direction.Right;

                    }
                    if (Key == ConsoleKey.UpArrow)
                    {
                        PlayerY = Max(0, PlayerY - 1);
                        PlayerMoveDirection = Direction.Up;

                    }
                    if (Key == ConsoleKey.DownArrow)
                    {
                        PlayerY = Min(10, PlayerY + 1);
                        PlayerMoveDirection = Direction.Down;

                    }


                    // 박수 움직임 구현 , 플레이어가 박스를 밀었을 때 무엇을 의미하는지 파악하기
                    // 그 의미는 플레이어가 이동했는데 박스의 위치가 겹쳤을때 이다.
                    // 박스를 미는 경우는 왼,오,위,아래 경우 가 있으니 경우에 따라 잘 판별하기

                    // 조건이 참일때 박스를 민다 > 박스의 좌표 이동
                    for (int i = 0; i < BOX_COUNT; ++i)
                    {
                        if (PlayerX == BoxX[i] && PlayerY == BoxY[i])
                        {
                            switch (PlayerMoveDirection)
                            {
                                case Direction.Left:
                                    BoxX[i] = Math.Max(0, BoxX[i] - 1);
                                    PlayerX = BoxX[i] + 1;
                                    break;
                                case Direction.Right:
                                    BoxX[i] = Math.Min(20, BoxX[i] + 1);
                                    PlayerX = BoxX[i] - 1;
                                    break;
                                case Direction.Up:
                                    BoxY[i] = Math.Max(0, BoxY[i] - 1);
                                    PlayerY = BoxY[i] + 1;
                                    break;
                                case Direction.Down:
                                    BoxY[i] = Math.Min(10, BoxY[i] + 1);
                                    PlayerY = BoxY[i] - 1;
                                    break;

                                default:
                                    Console.Clear();
                                    Console.WriteLine($"[Error] 플레이어 이동 방향 데이터가 오류 입니다. : {PlayerMoveDirection}");
                                    return;
                            }
                        }
                    }

                    // 박스끼리의 충돌
                    for (int i = 0; i < BOX_COUNT; ++i)
                    {
                        for (int j = 0; j < BOX_COUNT; ++j)
                        {


                            if (i != j && BoxX[i] == BoxX[j] && BoxY[i] == BoxY[j]) // 박스 1을 밀어서 박스 2에 닿은 건지, 박스 2를 밀어서 박스 1에 닿은건지?
                            {
                                switch (PlayerMoveDirection)
                                {
                                    case Direction.Left:
                                        if (pushedBoxId == 1)
                                        {
                                            BoxX[i] = BoxX[j] + 1;
                                            PlayerX = BoxX[i] + 1;
                                        }
                                        else
                                        {
                                            BoxX[i] = BoxX[j] + 1;
                                            PlayerX = BoxX[i] + 1;
                                        }
                                        break;
                                    case Direction.Right:
                                        if (pushedBoxId == 1)
                                        {
                                            BoxX[i] = BoxX[j] - 1;
                                            PlayerX = BoxX[i] - 1;
                                        }
                                        else
                                        {
                                            BoxX[i] = BoxX[j] - 1;
                                            PlayerX = BoxX[i] - 1;
                                        }
                                        break;
                                    case Direction.Up:
                                        if (pushedBoxId == 1)
                                        {
                                            BoxY[i] = BoxY[j] + 1;
                                            PlayerY = BoxY[i] + 1;
                                        }
                                        else
                                        {
                                            BoxY[i] = BoxY[j] + 1;
                                            PlayerY = BoxY[i] + 1;
                                        }
                                        break;
                                    case Direction.Down:
                                        if (pushedBoxId == 1)
                                        {
                                            BoxY[i] = BoxY[j] - 1;
                                            PlayerY = BoxY[i] - 1;
                                        }
                                        else
                                        {
                                            BoxY[i] = BoxY[j] - 1;
                                            PlayerY = BoxY[i] - 1;
                                        }
                                        break;
                                    default:
                                        Console.Clear();
                                        Console.WriteLine($"[Error] 플레이어 이동 방향 데이터가 오류입니다. : {PlayerMoveDirection}");

                                        return;
                                }
                            }
                        }
                    }



                    // 벽 구현, 벽과 박스, 플레이어 만났을때 주의 하기
                    // 벽이랑 플레이어 만났을때
                    for (int q = 0; q < WallX.Length; ++q)
                    {
                        if (PlayerX == WallX[q] && PlayerY == WallY[q])
                        {
                            switch (PlayerMoveDirection)
                            {
                                case Direction.Left:
                                    PlayerX = WallX[q] + 1;
                                    break;

                                case Direction.Right:
                                    PlayerX = WallX[q] - 1;
                                    break;

                                case Direction.Up:
                                    PlayerY = WallY[q] + 1;
                                    break;

                                case Direction.Down:
                                    PlayerY = WallY[q] - 1;
                                    break;

                                default:
                                    Console.Clear();
                                    Console.WriteLine($"[Error] 플레이어 이동 방향 데이터가 오류 입니다. : {PlayerMoveDirection}");
                                    return;
                            }
                        }
                    }

                    // 박스랑 벽이 만났을때 이때 박스는 플레이어가 밀어주니
                    // 플레이어의 처리를 같이 하자.
                    for (int e = 0; e < BOX_COUNT; ++e)
                    {
                        for (int i = 0; i < BOX_COUNT; ++i)
                            if (i != e && BoxX[e] == WallX[i] && BoxY[e] == WallY[i])
                            {
                                switch (PlayerMoveDirection)
                                {
                                    case Direction.Left:
                                        BoxX[e] = WallX[i] + 1;
                                        PlayerX = BoxX[e] + 1;
                                        break;

                                    case Direction.Right:
                                        BoxX[e] = WallX[i] - 1;
                                        PlayerX = BoxX[e] - 1;
                                        break;

                                    case Direction.Up:
                                        BoxY[e] = WallY[i] + 1;
                                        PlayerY = BoxY[e] + 1;
                                        break;

                                    case Direction.Down:
                                        BoxY[e] = WallY[i] - 1;
                                        PlayerY = BoxY[e] - 1;
                                        break;
                                    default:
                                        Console.Clear();
                                        Console.WriteLine($"[Error] 플레이어 이동 방향 데이터가 오류 입니다. : {PlayerMoveDirection}");
                                        return;
                                }
                            }
                    }

                    //박스를 골인 했을때 처리하자
                    // 박스와 골의 처리
                    // 1) Box1번과 Goal1번이 만났을 때
                    // 2) Box1번과 Goal2번이 만났을 때
                    // 3) Box2번과 Goal1번이 만났을 때
                    // 4. Box2번과 Goal2번이 만났을 때
                    // 
                    int count = 0;

                    for (int i = 0; i < GOAL_COUNT; ++i)
                    {
                        for (int j = 0; j < GOAL_COUNT; j++)
                        {
                            if (GoalX[i] == BoxX[j] && GoalY[i] == BoxY[j])
                            {
                                count++;
                                break;
                            }
                        }
                    }


                    if (count == GOAL_COUNT)
                    {
                        Console.Clear();
                        Console.WriteLine("축하합니다. 클리어 하셨습니다.");

                        break;

                    }// 게임 루프 중괄호




                }
            }
        }
    }
}





