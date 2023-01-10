namespace Sokoban
    // 1. 박스끼리 안 겹치게하기
    // 2. 모든 박스들 모든 골 지점이 넣으면 끝나게 하기
{
    // 열거형
   enum Direction
    {
        Left = 1,
        Right = 2,
        Up = 3,
        Down = 4
    }
    enum Wall
    {
        WALL1 = 1,
        WALL2 = 2,
        WALL3  = 3
    }
    enum Goal
    {
        GOAL1 = 1,
        GOAL2 = 2,
        GOAL3 = 3
    }
    class Program
    {
        static void Main()
        {
            Console.ResetColor();                           // 컬러를 초기화한다.
            Console.CursorVisible = false;                  // 커서를 숨긴다.
            Console.Title = "Sokoban";                      // 타이틀을 설정한다.
            Console.BackgroundColor = ConsoleColor.Blue;    // 배경색을 설정한다.
            Console.ForegroundColor = ConsoleColor.Gray;    // 글꼴색을 설정한다.
            Console.Clear();                                // 출력된 모든 내용을 지운다.

            // 플레이어의 초기 좌표
            const int PLAYER_START_X = 0;
            const int PLAYER_START_Y = 0;

            // 플레이어 좌표 설정
            int playerX = PLAYER_START_X;
            int playerY = PLAYER_START_Y;
            Direction playerDirection = 0; // 0 : None, 1 : Left, 2 : Right, 3 : Up, 4 : Down 내가 정한 규칙

            // 박스의 초기 좌표
            const int BOX1_START_X = 5;
            const int BOX1_START_Y = 5;

            const int BOX2_START_X = 10;
            const int BOX2_START_Y = 9;

            const int BOX3_START_X = 13;
            const int BOX3_START_Y = 7;

            // 박스 좌표 설정
            int box1X = BOX1_START_X;
            int box1Y = BOX1_START_Y;

            int box2X = BOX2_START_X;
            int box2Y = BOX2_START_Y;

            int box3X = BOX3_START_X;
            int box3Y = BOX3_START_Y;

            // 벽 초기 좌표 설정
            const int WALL1_X = 7;
            const int WALL1_Y = 8;

            const int WALL2_X = 10;
            const int WALL2_Y = 1;

            const int WALL3_X = 6;
            const int WALL3_Y = 6;

            Direction Wall = 0; // 1: 첫번째 벽, 2: 두번째 벽, 3: 세번째 벽

            // 벽의 기호
            const string WALL1 = "W";
            const string WALL2 = "W";
            const string WALL3 = "W";

            // 골 좌표 생성
            const int GOAL1_X = 14;
            const int GOAL1_Y = 8;

            const int GOAL2_X = 2;
            const int GOAL2_Y = 2;

            const int GOAL3_X = 1;
            const int GOAL3_Y = 4;

            Direction Goal = 0; // 1: 첫번째 골, 2: 두번째 골, 3: 세번째 골

            // 골의 기호
            const string GOAL1 = "G";
            const string GOAL2 = "G";
            const string GOAL3 = "G";

            // 플레이어 기호(string literal)
            const string PLAYER_STRING = "P";

            // 박스의 기호(string literal)
            const string BOX1_STRING = "B";
            const string BOX2_STRING = "B";
            const string BOX3_STRING = "B";


            // 맵의 가로 범위, 세로 범위
            // 가로 15 세로 10
            const int MAP_MIN_X = 0;
            const int MAP_MIN_Y = 0;
            const int MAP_MAX_X = 15;
            const int MAP_MAX_Y = 10;

            // 골과 박스의 개수
            const int GOAL_COUNT = 3;
            const int BOX_COUNT = GOAL_COUNT;

            // 게임 루프 == 프레임(Frame)


            while (true)
            {
                // 이전 프레임을 지운다.
                Console.Clear();

                // -------------------------- Render -------------------------------------
                // 플레이어 출력하기
                Console.SetCursorPosition(playerX, playerY);
                Console.Write(PLAYER_STRING);

                // 박스 출력하기
                Console.SetCursorPosition(box1X, box1Y);
                Console.Write(BOX1_STRING);
                Console.SetCursorPosition(box2X, box2Y);
                Console.Write(BOX2_STRING);
                Console.SetCursorPosition(box3X, box3Y);
                Console.Write(BOX3_STRING);

                // 벽 출력하기
                Console.SetCursorPosition(WALL1_X, WALL1_Y);
                Console.Write(WALL1);
                Console.SetCursorPosition(WALL2_X, WALL2_Y);
                Console.Write(WALL2);
                Console.SetCursorPosition(WALL3_X, WALL3_Y);
                Console.Write(WALL3);

                // 골 출력하기
                Console.SetCursorPosition(GOAL1_X, GOAL1_Y);
                Console.Write(GOAL1);
                Console.SetCursorPosition(GOAL2_X, GOAL2_Y);
                Console.Write(GOAL2);
                Console.SetCursorPosition(GOAL3_X, GOAL3_Y);
                Console.Write(GOAL3);

                // -------------------------- ProcessInput -------------------------------
                ConsoleKey key = Console.ReadKey().Key;

                // -------------------------- Update -------------------------------------

                // 플레이어 움직이기
                if (key == ConsoleKey.LeftArrow)
                {
                    // 왼쪽 방향키 눌렀을 때
                    // 왼쪽으로 이동
                    playerX = Math.Max(PLAYER_START_X, playerX - 1);
                    playerDirection = Direction.Left;
                }

                if (key == ConsoleKey.RightArrow)
                {
                    // 오른쪽 화살표키를 눌렀을 때
                    // 오른쪽으로 이동
                    playerX = Math.Min(playerX + 1, MAP_MAX_X);
                    playerDirection = Direction.Right;
                }

                if (key == ConsoleKey.UpArrow)
                {
                    // 위쪽 방향키 눌렀을 때
                    // 위로 이동
                    playerY = Math.Max(MAP_MIN_Y, playerY - 1);
                    playerDirection = Direction.Up;
                }

                if (key == ConsoleKey.DownArrow)
                {
                    // 아래쪽 방향키 눌렀을 때
                    // 아래로 이동
                    playerY = Math.Min(playerY + 1, MAP_MAX_Y);
                    playerDirection = Direction.Down;
                }

                // ----------------------------------------

                // 박스 업데이트1
                if (playerX == box1X && playerY == box1Y)
                {
                    switch (playerDirection)
                    {
                        case (Direction.Left): // 플레이어가 왼쪽으로 이동중
                            if (box1X == MAP_MIN_X) //박스가 왼쪽 끝에 있다면
                            {
                                playerX = MAP_MIN_X + 1;
                            }
                            else
                            {
                                box1X = Math.Max(MAP_MIN_X, box1X - 1);
                            }

                            break;
                        case (Direction.Right): // 플레이어가 오른쪽으로 이동중
                            if (box1X == MAP_MAX_X)
                            {
                                playerX = MAP_MAX_X - 1;
                            }
                            else
                            {
                                box1X = Math.Min(box1X + 1, MAP_MAX_X);
                            }

                            break;
                        case (Direction.Up): // 플레이어가 위쪽으로 이동중
                            if (box1Y == MAP_MIN_Y)
                            {
                                playerY = MAP_MIN_Y + 1;
                            }
                            else
                            {
                                box1Y = Math.Max(MAP_MIN_Y , box1Y - 1);
                            }

                            break;
                        case (Direction.Down): // 플레이어가 아래쪽으로 이동중
                            if (box1Y == MAP_MAX_Y)
                            {
                                playerY = MAP_MAX_Y - 1;
                            }
                            else
                            {
                                box1Y = Math.Min(box1Y + 1, MAP_MAX_Y);
                            }

                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다. {playerDirection}");

                            return; // 프로그램 종료
                    }
                }
                // 박스 업데이트2
                if (playerX == box2X && playerY == box2Y)
                {
                    switch (playerDirection)
                    {
                        case (Direction.Left): // 플레이어가 왼쪽으로 이동중
                            if (box2X == MAP_MIN_X) //박스가 왼쪽 끝에 있다면
                            {
                                playerX = MAP_MIN_X + 1;
                            }
                            else
                            {
                                box2X = Math.Max(MAP_MIN_X, box2X - 1);
                            }

                            break;
                        case (Direction.Right): // 플레이어가 오른쪽으로 이동중
                            if (box2X == MAP_MAX_X)
                            {
                                playerX = MAP_MAX_X - 1;
                            }
                            else
                            {
                                box2X = Math.Min(box2X + 1, MAP_MAX_X);
                            }

                            break;
                        case (Direction.Up): // 플레이어가 위쪽으로 이동중
                            if (box2Y == MAP_MIN_Y)
                            {
                                playerY = MAP_MIN_Y + 1;
                            }
                            else
                            {
                                box2Y = Math.Max(MAP_MIN_Y, box2Y - 1);
                            }

                            break;
                        case (Direction.Down): // 플레이어가 아래쪽으로 이동중
                            if (box2Y == MAP_MAX_Y)
                            {
                                playerY = MAP_MAX_Y - 1;
                            }
                            else
                            {
                                box2Y = Math.Min(box2Y + 1, MAP_MAX_Y);
                            }

                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다. {playerDirection}");

                            return; // 프로그램 종료
                    }
                }
                // 박스 업데이트3
                if (playerX == box3X && playerY == box3Y)
                {
                    switch (playerDirection)
                    {
                        case (Direction.Left): // 플레이어가 왼쪽으로 이동중
                            if (box3X == MAP_MIN_X) //박스가 왼쪽 끝에 있다면
                            {
                                playerX = MAP_MIN_X + 1;
                            }
                            else
                            {
                                box3X = Math.Max(MAP_MIN_X, box3X - 1);
                            }

                            break;
                        case (Direction.Right): // 플레이어가 오른쪽으로 이동중
                            if (box3X == MAP_MAX_X)
                            {
                                playerX = MAP_MAX_X - 1;
                            }
                            else
                            {
                                box3X = Math.Min(box3X + 1, MAP_MAX_X);
                            }

                            break;
                        case (Direction.Up): // 플레이어가 위쪽으로 이동중
                            if (box3Y == MAP_MIN_Y)
                            {
                                playerY = MAP_MIN_Y + 1;
                            }
                            else
                            {
                                box3Y = Math.Max(MAP_MIN_Y, box3Y - 1);
                            }

                            break;
                        case (Direction.Down): // 플레이어가 아래쪽으로 이동중
                            if (box3Y == MAP_MAX_Y)
                            {
                                playerY = MAP_MAX_Y - 1;
                            }
                            else
                            {
                                box3Y = Math.Min(box3Y + 1, MAP_MAX_Y);
                            }

                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다. {playerDirection}");

                            return; // 프로그램 종료
                    }
                }
                // 사람이 벽에 부딪히면 멈추게 하기1
                if (playerX == WALL1_X && playerY == WALL1_Y)
                {
                    switch (playerDirection)
                    {
                        case Direction.Left:
                            {
                                playerX = WALL1_X + 1;
                            }
                            break;
                        case Direction.Right:
                            {
                                playerX = WALL1_X - 1;
                            }    
                            break;
                        case Direction.Up:
                            {
                                playerY = WALL1_Y + 1;
                            }
                            break;
                        case Direction.Down:
                            {
                                playerY = WALL1_Y - 1;
                            }
                            break;
                    }
                    
                }
                // 사람이 벽에 부딪히면 멈추게 하기2
                if (playerX == WALL2_X && playerY == WALL2_Y)
                {
                    switch(playerDirection)
                    {
                        case Direction.Left:
                            playerX = WALL2_X + 1;
                            break;
                        case Direction.Right:
                            playerX = WALL2_X - 1;
                            break;
                        case Direction.Up:
                            playerY = WALL2_Y + 1;
                            break;
                        case Direction.Down:
                            playerY = WALL2_Y - 1;
                            break;
                    }
                    
                }
                // 사람이 벽에 부딪히면 멈추게 하기3
                if (playerX == WALL3_X && playerY == WALL3_Y)
                {
                    switch(playerDirection)
                    {
                        case Direction.Left:
                            {
                                playerX = WALL3_X + 1;
                            }
                            break;
                        case Direction.Right:
                            {
                                playerX = WALL3_X - 1;
                            }
                            break;
                        case Direction.Up:
                            {
                                playerY = WALL3_Y + 1;
                            }
                            break;
                        case Direction.Down:
                            {
                                playerY = WALL3_Y - 1;
                            }
                            break;
                    }
                }
                // 첫번째 박스가 첫번째 벽에 부딪히면 멈추게 하기
                if (box1X == WALL1_X && box1Y == WALL1_Y)
                {
                    switch (playerDirection)
                    {
                        case Direction.Left:
                            ++box1X;
                            ++playerX;
                            break;
                        case Direction.Right:
                            --box1X;
                            --playerX;
                            break;
                        case Direction.Up:
                            ++box1Y;
                            ++playerY;
                            break;
                        case Direction.Down:
                            --box1Y;
                            --playerY;
                            break;
                    }
                }
                // 첫번째 박스가 두번째 벽에 부딪히면 멈추게 하기
                if (box1X == WALL2_X && box1Y == WALL2_Y)
                    switch(playerDirection)
                    {
                        case Direction.Left:
                        {
                                ++box1X;
                                ++playerX;
                        }
                            break;
                        case Direction.Right:
                            {
                                --box1X;
                                --playerX;
                            }
                            break;
                        case Direction.Up:
                            {
                                ++box1Y;
                                ++playerY;
                            }
                            break;
                        case Direction.Down:
                            {
                                --box1Y;
                                --playerY;
                            }
                            break;
                    }
                // 첫번째 박스가 세번째 벽에 부딪히면 멈추게 하기
                if (box1X == WALL3_X && box1Y == WALL3_Y)
                {
                    switch(playerDirection)
                    {
                        case Direction.Left:
                            {
                                ++box1X;
                                ++playerX;
                            }
                            break;
                        case Direction.Right:
                            {
                                --box1X;
                                --playerX;
                            }
                            break;
                        case Direction.Up:
                            {
                                ++box1Y;
                                ++playerY;
                            }
                            break;
                        case Direction.Down:
                            {
                                --box1Y;
                                --playerY;
                            }
                            break;
                    }
                }
                // 두번째 박스가 첫번째 벽에 부딪히면 멈추게 하기
                if (box2X == WALL1_X && box2Y == WALL1_Y)
                {
                    switch(playerDirection)
                    {
                        case Direction.Left:
                            {
                                ++box2X;
                                ++playerX;
                            }
                            break;
                        case Direction.Right:
                            {
                                --box2X;
                                --playerX;
                            }
                            break;
                        case Direction.Up:
                            {
                                ++box2Y;
                                ++playerY;
                            }
                            break;
                        case Direction.Down:
                            {
                                --box2Y;
                                --playerY;
                            }
                            break;
                    }
                }
                // 두번째 박스가 두번째 벽에 부딪히면 멈추게 하기
                if (box2X == WALL2_X && box2Y == WALL2_Y)
                {
                    switch (playerDirection)
                    {
                        case Direction.Left:
                            {
                                ++box2X;
                                ++playerX;
                            }
                            break;
                        case Direction.Right:
                            {
                                --box2X;
                                --playerX;
                            }
                            break;
                        case Direction.Up:
                            {
                                ++box2Y;
                                ++playerY;
                            }
                            break;
                        case Direction.Down:
                            {
                                --box2Y;
                                --playerY;
                            }
                            break;
                    }
                }
                // 두번째 박스가 세번째 벽에 부딪히면 멈추게 하기
                if (box2X == WALL3_X && box2Y == WALL3_Y)
                {
                    switch(playerDirection)
                    {
                        case Direction.Left:
                            {
                                ++box2X;
                                ++playerX;
                            }
                            break;
                        case Direction.Right:
                            {
                                --box2X;
                                --playerX;
                            }
                            break;
                        case Direction.Up:
                            {
                                ++box2Y;
                                ++playerY;
                            }
                            break;
                        case Direction.Down:
                            {
                                --box2Y;
                                --playerY;
                            }
                            break;
                    }
                }
                // 세번째 박스가 첫번째 벽에 부딪히면 멈추게 하기
                if (box3X == WALL1_X && box3Y == WALL1_Y)
                {
                    switch(playerDirection)
                    {
                        case Direction.Left:
                            {
                                ++box3X;
                                ++playerX;
                            }
                            break;
                        case Direction.Right:
                            {
                                --box3X;
                                --playerX;
                            }
                            break;
                        case Direction.Up:
                            {
                                ++box3Y;
                                ++playerY;
                            }
                            break;
                        case Direction.Down:
                            {
                                --box3Y;
                                --playerY;
                            }
                            break;
                    }
                }
                // 세번째 박스가 두번째 벽에 부딪히면 멈추게 하기
                if (box3X == WALL2_X && box3Y == WALL2_Y)
                {
                    switch(playerDirection)
                    {
                        case Direction.Left:
                            {
                                ++box3X;
                                ++playerX;
                            }
                            break;
                        case Direction.Right:
                            {
                                --box3X;
                                --playerX;
                            }
                            break;
                        case Direction.Up:
                            {
                                ++box3Y;
                                ++playerY;
                            }
                            break;
                        case Direction.Down:
                            {
                                --box3Y;
                                --playerY;
                            }
                            break;
                    }
                }
                // 세번째 박스가 세번째 벽에 부딪히면 멈추게 하기
                if (box3X == WALL3_X && box3Y == WALL3_Y)
                {
                    switch(playerDirection)
                    {
                        case Direction.Left:
                            {
                                ++box3X;
                                ++playerX;
                            }
                            break;
                        case Direction.Right:
                            {
                                --box3X;
                                --playerX;
                            }
                            break;
                        case Direction.Up:
                            {
                                ++box3Y;
                                ++playerY;
                            }
                            break;
                        case Direction.Down:
                            {
                                --box3Y;
                                --playerY;
                            }
                            break;
                    }
                }
                // 첫번째 박스가 두번째 박스에 부딪히면 멈추게 하기
                if (box1X == box2X && box1Y == box2Y)
                {
                    switch (playerDirection)
                    {
                        case Direction.Left:

                    }
                }
                // 첫번째 박스가 세번째 박스에 부딪히면 멈추게 하기

                // 두번째 박스가 세번째 박스에 부딪히면 멈추게 하기

                    // 박스를 골 지점에 넣으면 종료
                if (box1X == GOAL1_X && box1Y == GOAL1_Y)
                {
                    return;
                }
            }
        }
    }
}