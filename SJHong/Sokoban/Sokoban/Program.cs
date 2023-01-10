using System;

// 게임에 필요한 타입을 정의함.
enum Direction
{
    None,
    Left,
    Right,
    Up,
    Down
}
class Sokoban
{
    static void Main()
    {
        // 초기 세팅 부터 시작


        // 1. 컬러 초기화
        Console.ResetColor();

        // 2. 커서 숨기기
        Console.CursorVisible = false;

        // 3. 콘솔 이름 설정
        Console.Title = "소코반";

        // 4. 배경색 설정
        Console.BackgroundColor = ConsoleColor.Black;

        // 5. 글꼴색 설정
        Console.ForegroundColor = ConsoleColor.White;

        // 6. 불필요한 정보 전부 지우기
        Console.Clear();

        // 7. 기호상수 설정


        // 8. 변수 설정
        const int MinX = 1;
        const int MaxX = 20;
        const int MinY = 1;
        const int MaxY = 10;

        int playerX = 3;
        int playerY = 3;
        const char playerShape = '8';

        int[] boxX = { 10, 19, 4 };
        int[] boxY = { 3, 7, 4 };
        int boxNum = boxX.Length;
        const char boxShape = 'a';

        int[] blockX = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 11, 11, 15, 15, 15, 15, 15 };
        int[] blockY = { 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 7, 8, 1, 2, 3, 4, 5 };
        int blockNum = blockX.Length;
        const char blockShape = '#';

        int[] goalX = { 14, 10, 7 };
        int[] goalY = { 7, 9, 5 };
        int goalNum = goalX.Length;
        const char goalShape = 'O';
        const char boxInGoalShape = '@';
        bool[] isBoxInGoal = new bool[boxNum];


        Direction currentPlayerDirection = Direction.None;

        // 중간중간 실행하는 습관 가지자 (빨리 실패하라) 나중에는 디버거 쓰자
        // 프레임워크: 프로그램의 동작 순서를 정의함.

        // 게임루프
        while (true)
        {
            // ====렌더==== 데이터를 가지고 그려주는 단계

            // 1. 이전 프레임을 지움.
            Console.Clear();

            // 2. 객체 위치에 커서 위치 후 그리기

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(playerX, playerY);
            Console.Write(playerShape);

            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int goalId = 0; goalId < goalNum; ++goalId)
            {
                Console.SetCursorPosition(goalX[goalId], goalY[goalId]);
                Console.Write(goalShape);
            }

            for (int boxId = 0; boxId < boxNum; ++boxId)
            {
                Console.SetCursorPosition(boxX[boxId], boxY[boxId]);

                if (isBoxInGoal[boxId] == true)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(boxInGoalShape);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(boxShape);
                }
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            for (int blockId = 0; blockId < blockNum; ++blockId)
            {
                Console.SetCursorPosition(blockX[blockId], blockY[blockId]);
                Console.Write(blockShape);
            }

            for (int i = 0; i <= MaxX; ++i)
            {
                for (int j = 0; j <= MaxY; ++j)
                {
                    if (MinX != 0 && MinY != 0)
                    {

                        Console.SetCursorPosition(MinX - 1 + i, MinY - 1);
                        Console.Write(blockShape);
                        Console.SetCursorPosition(MinX - 1, MinY + j);
                        Console.WriteLine(blockShape);
                    }
                    Console.SetCursorPosition(MaxX + 1 - i, MaxY + 1);
                    Console.Write(blockShape);
                    Console.SetCursorPosition(MaxX + 1, MaxY - j);
                    Console.Write(blockShape);
                }
            }

            int boxInGoal = 0;
            int pushedBox = 0;

            // ====프로세스 인풋====

            // 키의 정보를 받는다.
            ConsoleKey key = Console.ReadKey().Key;

            // ----업데이트----

            // 키의 정보에 따라 플레이어의 좌표 수정.
            if (key == ConsoleKey.LeftArrow)
            {
                playerX = Math.Max(MinX, --playerX);
                currentPlayerDirection = Direction.Left;
            }
            if (key == ConsoleKey.RightArrow)
            {
                playerX = Math.Min(++playerX, MaxX);
                currentPlayerDirection = Direction.Right;
            }
            if (key == ConsoleKey.UpArrow)
            {
                playerY = Math.Max(MinY, --playerY);
                currentPlayerDirection = Direction.Up;
            }
            if (key == ConsoleKey.DownArrow)
            {
                playerY = Math.Min(++playerY, MaxY);
                currentPlayerDirection = Direction.Down;
            }

            // 플레이어의 좌표에 따라 박스 좌표 수정
            // 플레이어와 박스가 겹쳤을 때 이동해야 함.

            for (int boxId = 0; boxId < boxNum; ++boxId)
            {
                if (playerX == boxX[boxId] && playerY == boxY[boxId])
                {
                    // 그러나 박스의 좌표를 바꾸려면 그 방향을 알아야 한다. 방향에 따라 값을 다르게 출력함.
                    switch (currentPlayerDirection)
                    {
                        case Direction.Left:
                            boxX[boxId] = Math.Max(MinX, --boxX[boxId]);
                            playerX = boxX[boxId] + 1;
                            break;

                        case Direction.Right:
                            boxX[boxId] = Math.Min(++boxX[boxId], MaxX);
                            playerX = boxX[boxId] - 1;
                            break;

                        case Direction.Up:
                            boxY[boxId] = Math.Max(MinY, --boxY[boxId]);
                            playerY = boxY[boxId] + 1;
                            break;

                        case Direction.Down:
                            boxY[boxId] = Math.Min(++boxY[boxId], MaxY);
                            playerY = boxY[boxId] - 1;
                            break;

                        default:
                            Console.Clear();
                            Console.WriteLine("잘못된 이동방향입니다.");
                            return;
                    }
                    pushedBox = boxId;
                    break;
                }
            }

            // 플레이어와 장애물과 부딪혔을 때
            for (int blockId = 0; blockId < blockNum; ++blockId)
            {
                if (playerX == blockX[blockId] && playerY == blockY[blockId])
                {
                    switch (currentPlayerDirection)
                    {
                        case Direction.Left:
                            ++playerX;
                            break;
                        case Direction.Right:
                            --playerX;
                            break;
                        case Direction.Up:
                            ++playerY;
                            break;
                        case Direction.Down:
                            --playerY;
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("잘못된 이동방향입니다.");
                            return;
                    }
                }
            }

            // 장애물과 박스가 부딪혔을 때
            for (int boxId = 0; boxId < boxNum; ++boxId)
            {
                for (int blockId = 0; blockId < blockNum; ++blockId)
                {
                    if (boxX[boxId] == blockX[blockId] && boxY[boxId] == blockY[blockId])
                    {
                        switch (currentPlayerDirection)
                        {
                            case Direction.Left:
                                ++boxX[boxId];
                                ++playerX;
                                break;
                            case Direction.Right:
                                --boxX[boxId];
                                --playerX;
                                break;
                            case Direction.Up:
                                ++boxY[boxId];
                                ++playerY;
                                break;
                            case Direction.Down:
                                --boxY[boxId];
                                --playerY;
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine("잘못된 이동방향입니다.");
                                return;
                        }
                    }
                }
            }

            //박스와 박스가 부딪힐 때

            for (int collidedBox = 0; collidedBox < boxNum; ++collidedBox)
            {

                if (pushedBox == collidedBox)
                {
                    continue;
                }

                if (boxX[pushedBox] == boxX[collidedBox] && boxY[pushedBox] == boxY[collidedBox])
                {
                    switch (currentPlayerDirection)
                    {
                        case Direction.Left:
                            ++boxX[pushedBox];
                            ++playerX;
                            break;
                        case Direction.Right:
                            --boxX[pushedBox];
                            --playerX;
                            break;
                        case Direction.Up:
                            ++boxY[pushedBox];
                            ++playerY;
                            break;
                        case Direction.Down:
                            --boxY[pushedBox];
                            --playerY;
                            break;
                    }
                }

            }


            // 골에 들어갔을 떄 클리어
            for (int boxId = 0; boxId < boxNum; ++boxId)
            {
                for (int goalId = 0; goalId < goalNum; ++goalId)
                {
                    isBoxInGoal[boxId] = false;
                    if (boxX[boxId] == goalX[goalId] && boxY[boxId] == goalY[goalId])
                    {
                        ++boxInGoal;
                        isBoxInGoal[boxId] = true;
                        break;
                    }
                }
            }

            if (boxInGoal == goalNum)
            {
                Console.Clear();
                Console.WriteLine("Game Clear! Press E to exit!");
                while (true)
                {
                    if (Console.ReadKey().Key == ConsoleKey.E)
                    {
                        return;
                    }
                }
            }

        }
    }
}
