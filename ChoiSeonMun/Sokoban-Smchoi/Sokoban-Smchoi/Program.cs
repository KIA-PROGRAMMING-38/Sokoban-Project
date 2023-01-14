using System;

namespace ChoiSeonMun.Sokoban;

enum Direction
{
    None,
    Left,
    Right,
    Up,
    Down
}

class Program
{
    static void Main(string[] args)
    {
        // 초기 세팅
        Console.ResetColor();                               // 컬러를 초기화한다.
        Console.CursorVisible = false;                      // 커서를 숨긴다.
        Console.Title = "My Sokoban";                       // 타이틀을 설정한다.
        Console.BackgroundColor = ConsoleColor.DarkBlue;    // 배경색을 설정한다.
        Console.ForegroundColor = ConsoleColor.Gray;        // 글꼴색을 설정한다.
        Console.Clear();                                    // 콘솔 창에 출력된 내용을 모두 지운다.

        // 기호 상수 정의
        const int MIN_X = 0;
        const int MAX_X = 15;
        const int MIN_Y = 0;
        const int MAX_Y = 20;

        // 플레이어 위치 좌표
        int playerX = 0;
        int playerY = 0;

        // 플레이어의 이동 방향
        Direction playerMoveDirection = Direction.None;

        // 코드 최적화를 위해 플레이어가 민 박스의 인덱스를 저장한다
        int pushedBoxIndex = 0;

        // 박스 좌표
        int[] boxPositionsX = { 5, 8 };
        int[] boxPositionsY = { 5, 4 };

        // 각 박스마다 골 위에 올라와 있는지에 관한 데이터다
        bool[] isBoxOnGoal = new bool[boxPositionsX.Length]; 

        // 벽 좌표
        int[] wallPositionsX = { 7, 11 };
        int[] wallPositionsY = { 7, 5 };

        // 골 좌표
        int[] goalPositionsX = { 10, 3 };
        int[] goalPositionsY = { 10, 6 };
        
        // 게임 루프
        while (true)
        {
            // ======================= Render ==========================
            // 이전 프레임을 지운다
            Console.Clear();

            // 플레이어를 그린다
            RenderObject(playerX, playerY, "P");

            // 골을 그린다
            int goalCount = goalPositionsX.Length;
            for (int i = 0; i < goalCount; ++i)
            {
                RenderObject(goalPositionsX[i], goalPositionsY[i], "G");
            }

            // 박스를 그린다
            int boxCount = boxPositionsX.Length;
            for (int i = 0; i < boxCount; ++i)
            {
                string boxIcon = isBoxOnGoal[i] ? "O" : "B";
                RenderObject(boxPositionsX[i], boxPositionsY[i], boxIcon);
            }

            // 벽을 그린다
            int wallCount = wallPositionsX.Length;
            for (int i = 0; i < wallCount; ++i)
            {
                RenderObject(wallPositionsX[i], wallPositionsY[i], "W");
            }
            
            // ======================= ProcessInput =======================
            // 유저로부터 입력을 받는다 
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            ConsoleKey key = keyInfo.Key;   // 실제 키는 ConsoleKeyInfo에 Key에 있다 

            // ======================= Update =======================
            MovePlayer(key, ref playerX, ref playerY, ref playerMoveDirection);

            // 플레이어와 벽의 충돌 처리
            for (int i = 0; i < wallCount; ++i)
            {
                if (false == IsCollided(playerX, playerY, wallPositionsX[i], wallPositionsY[i]))
                {
                    continue;
                }

                OnCollision(playerMoveDirection,
                    ref playerX, ref playerY,
                    in wallPositionsX[i], in wallPositionsY[i]);
            }
            
            // 박스 업데이트
            for (int i = 0; i < boxCount; ++i)
            {
                if (false == IsCollided(playerX, playerY, boxPositionsX[i], boxPositionsY[i]))
                {
                    continue;
                }

                switch (playerMoveDirection)
                {
                    case Direction.Left:
                        MoveToLeftOfTarget(out boxPositionsX[i], in playerX);

                        break;
                    case Direction.Right:
                        MoveToRightOfTarget(out boxPositionsX[i], in playerX);

                        break;
                    case Direction.Up:
                        MoveToUpOfTarget(out boxPositionsY[i], in playerY);
                        
                        break;
                    case Direction.Down:
                        MoveToDownOfTarget(out boxPositionsY[i], in playerY);
                        
                        break;
                    default:    // Error
                        ExitWithError($"[Error] 플레이어 방향 : {playerMoveDirection}");

                        break;
                }

                // 어떤 박스를 밀었는지 저장해야 한다 
                pushedBoxIndex = i;

                break;
            }

            // 박스끼리의 충돌 처리
            for (int i = 0; i < boxCount; ++i)
            {
                // 같은 박스라면 처리할 필요가 없다 
                if (pushedBoxIndex == i)
                {
                    continue;
                }

                if (false == IsCollided(boxPositionsX[pushedBoxIndex], boxPositionsY[pushedBoxIndex],
                                    boxPositionsX[i], boxPositionsY[i]))
                {
                    continue;
                }

                OnCollision(playerMoveDirection,
                    ref boxPositionsX[pushedBoxIndex], ref boxPositionsY[pushedBoxIndex],
                    in boxPositionsX[i], in boxPositionsY[i]);
                OnCollision(playerMoveDirection,
                    ref playerX, ref playerY,
                    in boxPositionsX[pushedBoxIndex], in boxPositionsY[pushedBoxIndex]);
            }

            // 박스와 벽의 충돌 처리
            for (int i = 0; i < wallCount; ++i)
            {
                if (false == IsCollided(boxPositionsX[pushedBoxIndex], boxPositionsY[pushedBoxIndex],
                                    wallPositionsX[i], wallPositionsY[i]))
                {
                    continue;
                }

                OnCollision(playerMoveDirection,
                    ref boxPositionsX[pushedBoxIndex], ref boxPositionsY[pushedBoxIndex],
                    in wallPositionsX[i], in wallPositionsY[i]);
                OnCollision(playerMoveDirection,
                    ref playerX, ref playerY,
                    in boxPositionsX[pushedBoxIndex], in boxPositionsY[pushedBoxIndex]);
                break;
            }

            // 박스가 골 위로 올라왔는지 확인
            int boxOnGoalCount = 0;
            for (int boxId = 0; boxId < boxCount; ++boxId)
            {
                // 현재 프레임의 박스 상태를 실시간으로 추적하기 위해 false로 바꿔둔다
                isBoxOnGoal[boxId] = false;

                for (int goalId = 0; goalId < goalCount; ++goalId)
                {
                    if (IsCollided(boxPositionsX[boxId], boxPositionsY[boxId],
                                    goalPositionsX[goalId], goalPositionsY[goalId]))
                    {
                        ++boxOnGoalCount;
                        isBoxOnGoal[boxId] = true;

                        break; // 이 박스에 대해서는 더 이상 체크할 게 없
                    }
                }
            }

            if (boxOnGoalCount == goalCount)
            {
                break;
            }
        }

        Console.Clear();
        Console.WriteLine("축하합니다. 게임을 클리어하셨습니다.");

        // 게임이 끝났으니 콘솔 세팅을 다시 정상화한다.
        Console.ResetColor();


        // 오브젝트를 그린다
        void RenderObject(int x, int y, string icon)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(icon);
        }

        // target 근처로 이동시킨다 
        void MoveToLeftOfTarget(out int x, in int target) => x = Math.Max(MIN_X, target - 1);
        void MoveToRightOfTarget(out int x, in int target) => x = Math.Min(target + 1, MAX_X);
        void MoveToUpOfTarget(out int y, in int target) => y = Math.Max(MIN_Y, target - 1);
        void MoveToDownOfTarget(out int y, in int target) => y = Math.Min(target + 1, MAX_Y);

        // 플레이어를 움직인다
        void MovePlayer(ConsoleKey key, ref int x, ref int y, ref Direction moveDirection)
        {
            if (key == ConsoleKey.LeftArrow)
            {
                MoveToLeftOfTarget(out x, in x);
                moveDirection = Direction.Left;
            }

            if (key == ConsoleKey.RightArrow)
            {
                MoveToRightOfTarget(out x, in x);
                moveDirection = Direction.Right;
            }

            if (key == ConsoleKey.UpArrow)
            {
                MoveToUpOfTarget(out y, in y);
                moveDirection = Direction.Up;
            }

            if (key == ConsoleKey.DownArrow)
            {
                MoveToDownOfTarget(out y, in y);
                moveDirection = Direction.Down;
            }
        }

        // 충돌을 처리한다
        void OnCollision(Direction playerMoveDirection,
            ref int objX, ref int objY, in int collidedObjX, in int collidedObjY)
        {
            switch (playerMoveDirection)
            {
                case Direction.Left:
                    MoveToRightOfTarget(out objX, in collidedObjX);

                    break;
                case Direction.Right:
                    MoveToLeftOfTarget(out objX, in collidedObjX);

                    break;
                case Direction.Up:
                    MoveToDownOfTarget(out objY, in collidedObjY);

                    break;
                case Direction.Down:
                    MoveToUpOfTarget(out objY, in collidedObjY);

                    break;
            }
        }

        // 에러 메시지와 함께 애플리케이션을 종료한다 
        void ExitWithError(string errorMessage)
        {
            Console.Clear();
            Console.WriteLine(errorMessage);
            Environment.Exit(1);
        }

        // 충돌했는지 검사한다 
        bool IsCollided(int x1, int y1, int x2, int y2)
        {
            if (x1 == x2 && y1 == y2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

