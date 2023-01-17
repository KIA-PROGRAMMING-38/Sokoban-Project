using System;

namespace ChoiSeonMun.Sokoban;

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
        Player player = new Player();
        
        // 코드 최적화를 위해 플레이어가 민 박스의 인덱스를 저장한다
        int pushedBoxIndex = 0;

        // 박스 좌표
        Box[] boxes = new Box[2]
        {
            new Box { X = 5, Y = 5, IsOnGoal = false },
            new Box { X = 8, Y = 4, IsOnGoal = false }
        };

        // 벽 좌표
        Wall[] walls = new Wall[2]
        {
            new Wall { X = 7, Y = 7 },
            new Wall { X = 11, Y = 5 }
        };

        // 골 좌표
        Goal[] goals = new Goal[2]
        {
            new Goal { X = 10, Y = 10 },
            new Goal { X = 3, Y = 6 }
        };
        
        // 게임 루프
        while (true)
        {
            // ======================= Render ==========================
            // 이전 프레임을 지운다
            Console.Clear();

            // 플레이어를 그린다
            RenderObject(player.X, player.Y, "P");

            // 골을 그린다
            int goalCount = goals.Length;
            for (int i = 0; i < goalCount; ++i)
            {
                RenderObject(goals[i].X, goals[i].Y, "G");
            }

            // 박스를 그린다
            int boxCount = boxes.Length;
            for (int i = 0; i < boxCount; ++i)
            {
                string boxIcon = boxes[i].IsOnGoal ? "O" : "B";
                RenderObject(boxes[i].X, boxes[i].Y, boxIcon);
            }

            // 벽을 그린다
            int wallCount = walls.Length;
            for (int i = 0; i < wallCount; ++i)
            {
                RenderObject(walls[i].X, walls[i].Y, "W");
            }
            
            // ======================= ProcessInput =======================
            // 유저로부터 입력을 받는다 
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            ConsoleKey key = keyInfo.Key;   // 실제 키는 ConsoleKeyInfo에 Key에 있다 

            // ======================= Update =======================
            MovePlayer(key, player);

            // 플레이어와 벽의 충돌 처리
            for (int i = 0; i < wallCount; ++i)
            {
                if (false == IsCollided(player.X, player.Y, walls[i].X, walls[i].Y))
                {
                    continue;
                }

                OnCollision(() =>
                {
                    PushOut(player.MoveDirection, ref player.X, ref player.Y, walls[i].X, walls[i].Y);
                });
            }
            
            // 박스 업데이트
            for (int i = 0; i < boxCount; ++i)
            {
                if (false == IsCollided(player.X, player.Y, boxes[i].X, boxes[i].Y))
                {
                    continue;
                }

                OnCollision(() =>
                {
                    MoveBox(player, boxes[i]);
                });

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

                if (false == IsCollided(boxes[pushedBoxIndex].X, boxes[pushedBoxIndex].Y, boxes[i].X, boxes[i].Y))
                {
                    continue;
                }

                OnCollision(() =>
                {
                    PushOut(player.MoveDirection,
                        ref boxes[pushedBoxIndex].X, ref boxes[pushedBoxIndex].Y,
                        boxes[i].X, boxes[i].Y);

                    PushOut(player.MoveDirection,
                        ref player.X, ref player.Y,
                        boxes[pushedBoxIndex].X, boxes[pushedBoxIndex].Y);
                });
            }

            // 박스와 벽의 충돌 처리
            for (int i = 0; i < wallCount; ++i)
            {
                if (false == IsCollided(boxes[pushedBoxIndex].X, boxes[pushedBoxIndex].Y, walls[i].X, walls[i].Y))
                {
                    continue;
                }

                OnCollision(() =>
                {
                    PushOut(player.MoveDirection,
                        ref boxes[pushedBoxIndex].X, ref boxes[pushedBoxIndex].Y,
                        walls[i].X, walls[i].Y);

                    PushOut(player.MoveDirection,
                        ref player.X, ref player.Y,
                        boxes[pushedBoxIndex].X, boxes[pushedBoxIndex].Y);
                });
                
                break;
            }

            int boxOnGoalCount = CountBoxOnGoal(boxes, goals);

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

        // 골 위에 박스가 몇 개 있는지 센다
        int CountBoxOnGoal(Box[] boxes, Goal[] goals)
        {
            int boxCount = boxes.Length;
            int goalCount = goals.Length;

            int result = 0;
            for (int boxId = 0; boxId < boxCount; ++boxId)
            {
                boxes[boxId].IsOnGoal = false;

                for (int goalId = 0; goalId < goalCount; ++goalId)
                {
                    if (IsCollided(boxes[boxId].X, boxes[boxId].Y, goals[goalId].X, goals[goalId].Y))
                    {
                        ++result;

                        boxes[boxId].IsOnGoal = true;

                        break;
                    }
                }
            }

            return result;
        }

        // target 근처로 이동시킨다 
        void MoveToLeftOfTarget(out int x, in int target) => x = Math.Max(MIN_X, target - 1);
        void MoveToRightOfTarget(out int x, in int target) => x = Math.Min(target + 1, MAX_X);
        void MoveToUpOfTarget(out int y, in int target) => y = Math.Max(MIN_Y, target - 1);
        void MoveToDownOfTarget(out int y, in int target) => y = Math.Min(target + 1, MAX_Y);

        // 플레이어를 움직인다
        void MovePlayer(ConsoleKey key, Player player)
        {
            if (key == ConsoleKey.LeftArrow)
            {
                MoveToLeftOfTarget(out player.X, in player.X);
                player.MoveDirection = Direction.Left;
            }

            if (key == ConsoleKey.RightArrow)
            {
                MoveToRightOfTarget(out player.X, in player.X);
                player.MoveDirection = Direction.Right;
            }

            if (key == ConsoleKey.UpArrow)
            {
                MoveToUpOfTarget(out player.Y, in player.Y);
                player.MoveDirection = Direction.Up;
            }

            if (key == ConsoleKey.DownArrow)
            {
                MoveToDownOfTarget(out player.Y, in player.Y);
                player.MoveDirection = Direction.Down;
            }
        }

        // 충돌을 처리한다 
        void OnCollision(Action action)
        {
            action();
        }

        // 충돌을 처리한다
        void PushOut(Direction playerMoveDirection,
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

        // 박스를 움직인다 
        void MoveBox(Player player, Box box)
        {
            switch (player.MoveDirection)
            {
                case Direction.Left:
                    MoveToLeftOfTarget(out box.X, in player.X);

                    break;
                case Direction.Right:
                    MoveToRightOfTarget(out box.X, in player.X);

                    break;
                case Direction.Up:
                    MoveToUpOfTarget(out box.Y, in player.Y);

                    break;
                case Direction.Down:
                    MoveToDownOfTarget(out box.Y, in player.Y);

                    break;
                default:    // Error
                    ExitWithError($"[Error] 플레이어 방향 : {player.MoveDirection}");

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

