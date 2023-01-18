using System.Numerics;

namespace Sokoban
{
    enum Direction // 방향을 저장하는 타입
    {
        None,
        Left,
        Right,
        Up,
        Down,
        spacebar
    }
    class Sokoban
    {
        static void Main()
        {

            // 초기 세팅
            Console.ResetColor(); // 컬러를 초기화 하는 것
            Console.CursorVisible = false; // 커서를 숨기기
            Console.Title = "소코반"; // 타이틀을 설정한다.
            Console.BackgroundColor = ConsoleColor.Black; // 배경색을 설정한다.
            Console.Clear(); // 출력된 내용을 지운다.


            // 기호 상수 정의
            const int GOAL_COUNT = 3;
            const int BOX_COUNT = GOAL_COUNT;
            const int WALL_COUNT = 12;
            const int TRAP_COUNT = 2;
            const int BREAK_COUNT = 4;

            const int MIN_X = 1;
            const int MIN_Y = 1;
            const int MAX_X = 30;
            const int MAX_Y = 25;

            // 플레이어 위치를 저장하기 위한 변수
            // int playerX = 0;
            // int playerY = 0;
            // Direction playerMoveDirection = Direction.None;
            // int pushedBoxIndex = 0;

            Player player = new Player
            {
                X = 1,
                Y = 1,
                MoveDirection = Direction.None,
                PushedBoxIndex = 0
            };

            // 박스 위치를 저장하기 위한 변수
            Box[] boxes = new Box[]
            {
                new Box{X = 10, Y = 18, IsOnGoal = false},
                new Box{X = 6, Y = 10, IsOnGoal = false},
                new Box{X = 18, Y = 15, IsOnGoal = false}
            };

            // 벽 위치를 저장하기 위한 변수
            Wall[] walles = new Wall[]
            {
                new Wall{X = 4, Y = 6},
                new Wall{X = 4, Y = 7},
                new Wall{X = 4, Y = 5},
                new Wall{X = 3, Y = 6},
                new Wall{X = 17, Y = 14},
                new Wall{X = 17, Y = 15},
                new Wall{X = 17, Y = 16},
                new Wall{X = 17, Y = 17},
                new Wall{X = 17, Y = 18},
                new Wall{X = 17, Y = 19},
                new Wall{X = 12, Y = 10},
                new Wall{X = 12, Y = 11}
            };

            // 골 위치를 저장하기 위한 변수
            Goal[] goales = new Goal[]
            {
                new Goal{X = 14, Y = 10},
                new Goal{X = 7, Y = 14},
                new Goal{X = 3, Y = 5}
            };

            // 함정 위치를 저장하기 위한 변수
            Trap[] trapes = new Trap[]
            {
                new Trap{X = 10, Y = 17 },
                new Trap{X = 10, Y = 10 }
            };



            // 게임 루프 구성
            while (true)
            {
                Render();

                ConsoleKey key = Console.ReadKey().Key;

                Update(key);

                // 박스와 골의 처리
                int boxOnGoalCount = 0;

                // 골 지점에 박스에 존재하냐?
                for (int boxId = 0; boxId < BOX_COUNT; ++boxId) // 모든 골 지점에 대해서
                {
                    // 현재 박스가 골 위에 올라와 있는지 체크한다.
                    boxes[boxId].IsOnGoal = false; // 없을 가능성이 높기 때문에 false로 초기화 한다.

                    for (int goalId = 0; goalId < GOAL_COUNT; ++goalId) // 모든 박스에 대해서
                    {
                        // 박스가 골 지점 위에 있는지 확인한다.
                        if (IsCollided(boxes[boxId].X, boxes[boxId].Y, goales[goalId].X, goales[goalId].Y))
                        {
                            ++boxOnGoalCount;

                            boxes[boxId].IsOnGoal = true; // 박스가 골 위에 있다는 사실을 저장해둔다.

                            break;
                        }
                    }
                }
                // 함정에 걸렸다면
                for (int trapId = 0; trapId < TRAP_COUNT; ++trapId)
                {
                    if (trapes[trapId].X == player.X && trapes[trapId].Y == player.Y)
                    {
                        TrapMessage();
                    }
                }

                // 모든 골 지점에 박스가 올라와 있다면?
                if (boxOnGoalCount == GOAL_COUNT)
                {
                    Console.Clear();
                    Console.WriteLine("축하합니다. 클리어 하셨습니다.");

                    break;
                }
            }


            // 프레임을 그립니다.
            void Render()
            {
                // 이전 프레임을 지운다.
                Console.Clear();

                // 플레이어를 그린다.
                Console.ForegroundColor = ConsoleColor.Red;
                RenderObject(player.X, player.Y, "♀");

                // 골을 그린다.
                for (int i = 0; i < GOAL_COUNT; ++i)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    RenderObject(goales[i].X, goales[i].Y, "◎");
                }

                // 박스를 그린다.
                for (int boxId = 0; boxId < BOX_COUNT; ++boxId)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    string boxShape = boxes[boxId].IsOnGoal ? "●" : "■";
                    RenderObject(boxes[boxId].X, boxes[boxId].Y, boxShape);
                }

                // 벽을 그린다.
                for (int wallId = 0; wallId < WALL_COUNT; ++wallId)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    RenderObject(walles[wallId].X, walles[wallId].Y, "▣");
                }

                //맵 테두리를 그린다.
                for (int mapWideId = 0; mapWideId < MAX_X + 2; ++mapWideId)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    RenderObject(mapWideId, 0, "#");
                    RenderObject(mapWideId, MAX_Y + 1, "#");
                }
                for (int mapLengthId = 0; mapLengthId < MAX_Y + 2; ++mapLengthId)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    RenderObject(0, mapLengthId, "#");
                    RenderObject(MAX_X + 2, mapLengthId, "#");
                }

                // 설명만들기
                RenderObject(MAX_X + 5, MIN_Y, "이동 : 방향키");

            }

            // 오브젝트를 그립니다.
            void RenderObject(int x, int y, string obj)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(obj);
            }

            // 골 위에 박스가 몇 개 있는지 센다
            int CountBoxOnGoal(in int[] boxPositionsX, in int[] boxPositionsY, ref bool[] isBoxOnGoal,
            in int[] goalPositionsX, in int[] goalPositionsY)
            {
                int boxCount = boxPositionsX.Length;
                int goalCount = goalPositionsX.Length;

                int result = 0;
                for (int boxId = 0; boxId < boxCount; ++boxId)
                {
                    isBoxOnGoal[boxId] = false;

                    for (int goalId = 0; goalId < goalCount; ++goalId)
                    {
                        if (IsCollided(boxPositionsX[boxId], boxPositionsY[boxId],
                        goalPositionsX[goalId], goalPositionsY[goalId]))
                        {
                            ++result;

                            isBoxOnGoal[boxId] = true;
                            break;
                        }
                    }
                }

                return result;
            }

            void Update(ConsoleKey key)
            {
                MovePlayer(key, ref player.X, ref player.Y, ref player.MoveDirection);

                // 플레이어와 벽의 충돌 처리
                for (int wallId = 0; wallId < WALL_COUNT; ++wallId)
                {
                    if (false == IsCollided(player.X, player.Y, walles[wallId].X, walles[wallId].Y))
                    {
                        continue;
                    }

                    OnCollision(player.MoveDirection,
                    ref player.X, ref player.Y,
                    in walles[wallId].X, in walles[wallId].Y);
                }


                // 박스 이동 처리
                // 플레이어가 박스를 밀었을 때라는 게 무엇을 의미하는가? => 플레이어가 이동했는데 플레이어의 위치와 박스 위치가 겹쳤다.
                for (int i = 0; i < BOX_COUNT; ++i)
                {
                    if (false == IsCollided(player.X, player.Y, boxes[i].X, boxes[i].Y))
                    {
                        continue;
                    }

                    switch (player.MoveDirection)
                    {
                        case Direction.Left:
                            MoveToLeftOfTarget(out boxes[i].X, in player.X);

                            break;
                        case Direction.Right:
                            MoveToRightOfTarget(out boxes[i].X, in player.X);

                            break;
                        case Direction.Up:
                            MoveToUpOfTarget(out boxes[i].Y, in player.Y);

                            break;
                        case Direction.Down:
                            MoveToDownOfTarget(out boxes[i].Y, in player.Y);
                            break;
                        default: // Error
                            ExitWithError($"[Error] 플레이어 방향 : {player.MoveDirection}");

                            break;
                    }

                    player.PushedBoxIndex = i;

                    break;
                }

                // 박스와 벽의 충돌 처리
                for (int wallId = 0; wallId < WALL_COUNT; ++wallId)
                {
                    if (false == IsCollided(boxes[player.PushedBoxIndex].X, boxes[player.PushedBoxIndex].Y, walles[wallId].X, walles[wallId].Y))
                    {
                        continue;
                    }

                    OnCollision(player.MoveDirection,
                    ref boxes[player.PushedBoxIndex].X, ref boxes[player.PushedBoxIndex].Y,
                    in walles[wallId].X, in walles[wallId].Y);
                    OnCollision(player.MoveDirection,
                    ref player.X, ref player.Y,
                    in boxes[player.PushedBoxIndex].X, in boxes[player.PushedBoxIndex].Y);
                    break;
                }

                // 박스끼리 충돌 처리
                for (int collidedBoxId = 0; collidedBoxId < BOX_COUNT; ++collidedBoxId)
                {
                    // 같은 박스라면 처리할 필요가 X
                    if (player.PushedBoxIndex == collidedBoxId)
                    {
                        continue;
                    }

                    if (false == IsCollided(boxes[player.PushedBoxIndex].X, boxes[player.PushedBoxIndex].Y, boxes[collidedBoxId].X, boxes[collidedBoxId].Y))
                    {
                        continue;
                    }

                    OnCollision(player.MoveDirection,
                    ref boxes[player.PushedBoxIndex].X, ref boxes[player.PushedBoxIndex].Y,
                    in boxes[collidedBoxId].X, in boxes[collidedBoxId].Y);
                    OnCollision(player.MoveDirection, ref player.X, ref player.Y,
                    in boxes[player.PushedBoxIndex].X, in boxes[player.PushedBoxIndex].Y);

                   
                }

            }
            // target 근처로 이동시킨다
            void MoveToLeftOfTarget(out int x, in int target) => x = Math.Max(MIN_X, target - 1);
            void MoveToRightOfTarget(out int x, in int target) => x = Math.Min(target + 1, MAX_X);
            void MoveToUpOfTarget(out int y, in int target) => y = Math.Max(MIN_Y, target - 1);
            void MoveToDownOfTarget(out int y, in int target) => y = Math.Min(target + 1, MAX_Y);

            // 플레이어를 이동시킨다.
            void MovePlayer(ConsoleKey key, ref int x, ref int y, ref Direction moveDirection)
            {
                if (key == ConsoleKey.LeftArrow)
                {
                    player.X = Math.Max(MIN_X, player.X - 1);
                    player.MoveDirection = Direction.Left;
                }

                if (key == ConsoleKey.RightArrow)
                {
                    player.X = Math.Min(player.X + 1, MAX_X);
                    player.MoveDirection = Direction.Right;
                }

                if (key == ConsoleKey.UpArrow)
                {
                    player.Y = Math.Max(MIN_Y, player.Y - 1);
                    player.MoveDirection = Direction.Up;
                }

                if (key == ConsoleKey.DownArrow)
                {
                    player.Y = Math.Min(player.Y + 1, MAX_Y);
                    player.MoveDirection = Direction.Down;
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

            // 두 물체가 충돌했는지 판별합니다.
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

            // 트랩에 걸렸을 때 이벤트
            void TrapMessage()
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("당신은 함정에 걸리셨습니다.\n\n퀴즈가 전부 정답이라면 당신은 다시 게임으로 돌아오실 수 있습니다.\n\n");
                Console.Write("첫번째 문제(단답형)\n\n최선문 교수님의 첫 근무지는 어디인가요? ");
                string choiceExitTrap = Console.ReadLine();
                int[] choiceAnswer = new int[2];
                switch (choiceExitTrap)
                {
                    case "시프트업":
                        ++choiceAnswer[0];
                        break;
                    default:
                        ++choiceAnswer[1];
                        break;
                }

                Console.Write("\n\n두번째 문제(단답형)\n\n서울에서 어딘가로 거리를 잴 때 서울 어디를 기준으로 거리를 재나요? ");
                string choiceExitTrap2 = Console.ReadLine();
                switch (choiceExitTrap2)
                {
                    case "광화문":
                        ++choiceAnswer[0];
                        break;
                    default:
                        ++choiceAnswer[1];
                        break;
                }
                Console.Write($"\n\n당신은 {choiceAnswer[0]} 문제를 맞추었고 {choiceAnswer[1]} 틀렸습니다.\n\n");

                if (choiceAnswer[0] == 2)
                {

                    Console.Write("함정 탈출에 성공하셨습니다. 3초 후에 게임으로 돌아갑니다.");
                    Thread.Sleep(3000); //1000 - 1초

                }
                else
                {
                    Console.Write("탈출에 실패하셨습니다. 게임을 재시작하시려면 y 를 종료하시려면 n을 눌러주세요 :  ");
                    string choice = Console.ReadLine();
                    if (choice == "y")
                    {
                        Main();
                    }
                    else
                    {
                        Console.Clear();
                        Console.Write("게임을 종료됩니다.");
                        Environment.Exit(0);
                    }
                }

                choiceExitTrap = string.Empty;
                choiceExitTrap2 = string.Empty;
            }

        }
        
    }
}