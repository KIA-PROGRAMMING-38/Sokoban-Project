using System;
using Sokoban;

namespace Sokoban
{
    internal enum Direction
    {
        None,
        Up,
        Down,
        Left,
        Right
    }
    enum Grab
    {
        None,
        Grab
    }

    enum PortalNum
    {
        None,
        One,
        Two,
        Three,
        Four,
    }

    class Program
    {
        static void Main()
        {
            // 초기 세팅
            Console.ResetColor();                                // 컬러를 초기화한다.
            Console.CursorVisible = false;                       // 커서를 숨긴다.
            Console.Title = "경이루 아카데미";                    // 타이틀을 설정한다.
            Console.BackgroundColor = ConsoleColor.White;       // 배경색을 설정한다.
            Console.ForegroundColor = ConsoleColor.Black;       // 글꼴색을 설정한다.
            Console.OutputEncoding = System.Text.Encoding.UTF8; // UTF8 문자 허용
            Console.Clear();                                    // 출력된 모든 내용을 지운다.


            // 기호 상수 정의
            const int GOAL_COUNT = 4;
            const int BOX_COUNT = GOAL_COUNT;
            const int WALL_COUNT = 19;
            const int WARP_COUNT = 1;
            const int PORTAL_COUNT = 5;
            const int TRAP_COUNT = 4;

            const int MIN_X = 6;
            const int MIN_Y = 3;
            const int MAX_X = 38;
            const int MAX_Y = 23;
            const int OUTLINE_LENGTH_X = 33;
            const int OUTLINE_LENGTH_Y = 20;

            Player player = new Player
            {
                X = 8,
                Y = 4,
                PastX = 8,
                PastY = 4,
                MoveDirection = Direction.None,
                GrabOnOff = Grab.None,
                PortalNum = PortalNum.None,
                PushedBoxIndex = 0,
                GrabedBoxIndex = 0
            };

            Box[] box = new Box[BOX_COUNT]
            {
                new Box { X = 15, Y = 4, IsOnGoal = false },
                new Box { X = 10, Y = 12, IsOnGoal = false },
                new Box { X = 17, Y = 14, IsOnGoal = false },
                new Box { X = 9, Y = 9, IsOnGoal = false }
            };

            Wall[] wall = new Wall[WALL_COUNT]
            {
                new Wall { X = 38, Y = 15 },
                new Wall { X = 37, Y = 15 },
                new Wall { X = 36, Y = 15 },
                new Wall { X = 35, Y = 15 },
                new Wall { X = 34, Y = 15 },
                new Wall { X = 33, Y = 15 },
                new Wall { X = 32, Y = 15 },
                new Wall { X = 31, Y = 15 },
                new Wall { X = 30, Y = 15 },
                new Wall { X = 29, Y = 15 },
                new Wall { X = 28, Y = 15 },
                new Wall { X = 28, Y = 16 },
                new Wall { X = 28, Y = 17 },
                new Wall { X = 28, Y = 18 },
                new Wall { X = 28, Y = 19 },
                new Wall { X = 28, Y = 20 },
                new Wall { X = 28, Y = 21 },
                new Wall { X = 28, Y = 22 },
                new Wall { X = 28, Y = 23 }

            };

            Goal[] goal = new Goal[GOAL_COUNT]
            {
                new Goal { X = 25, Y = 17 },
                new Goal { X = 20, Y = 12 },
                new Goal { X = 20, Y = 17 },
                new Goal { X = 36, Y = 21 }
            };

            WarpInOut[] warpInOut = new WarpInOut[WARP_COUNT]
            {
                new WarpInOut { X = 15, Y = 7 }
            };
            WarpOutIn[] warpOutIn = new WarpOutIn[WARP_COUNT]
            {
                new WarpOutIn { X = 32, Y = 19 }
            };

            Portal[] portal = new Portal[PORTAL_COUNT]
            {
                new Portal { X = 0, Y = 0 },
                new Portal { X = 9, Y = 5 },
                new Portal { X = 35, Y = 5 },
                new Portal{ X = 9, Y = 22 },
                new Portal{ X = 35, Y = 22 }
            };

            Trap[] trap = new Trap[TRAP_COUNT]
            {
                new Trap { X = 6 , Y = 3, IsObjOnTrap = false },
                new Trap { X = 6 , Y = 23, IsObjOnTrap = false },
                new Trap { X = 38 , Y = 3, IsObjOnTrap = false },
                new Trap { X = 38, Y = 23, IsObjOnTrap = false }
            };

            int activatedTrapId = 0;
            int howMuchOperation = 0;

            StatusMessage statusMessage = new StatusMessage
            {
                FullScreenX = 55,
                FullScreenY = 2,
                FontX = 55,
                FontY = 3,
                NearThanX = 55,
                NearThanY = 5,
                OperationX = 55,
                OperationY = 7,
                GrabX = 55,
                GrabY = 8,
                CurrentKeyX = 55,
                CurrentKeyY = 15
            };

            ConsoleKey key = default;

            // 게임 루프 구성
            while (true)
            {
                Render();

                key = Console.ReadKey().Key;

                Update(key);

                int boxOnGoalCount = 0;

                // 골 지점에 박스에 존재하냐?
                for (int boxId = 0; boxId < BOX_COUNT; ++boxId) // 모든 골 지점에 대해서
                {
                    // 현재 박스가 골 위에 올라와 있는지 체크한다.
                    box[boxId].IsOnGoal = false; // 없을 가능성이 높기 때문에 false로 초기화 한다.

                    for (int goalId = 0; goalId < GOAL_COUNT; ++goalId) // 모든 박스에 대해서
                    {
                        // 박스가 골 지점 위에 있는지 확인한다.
                        if (IsCollided(box[boxId].X, box[boxId].Y, goal[goalId].X, goal[goalId].Y))
                        {
                            ++boxOnGoalCount;

                            box[boxId].IsOnGoal = true; // 박스가 골 위에 있다는 사실을 저장해둔다.

                            break;
                        }
                    }
                }

                // 모든 골 지점에 박스가 올라와 있다면?
                if (boxOnGoalCount == GOAL_COUNT)
                {
                    Console.Clear();
                    Console.WriteLine("축하합니다. 클리어 하셨습니다.");

                    break;
                }

                // 오브젝트가 함정을 밟았다면
                for (int trapId = 0; trapId < TRAP_COUNT; ++trapId)
                {
                    if (IsCollided(player.X, player.Y, trap[trapId].X, trap[trapId].Y) || IsCollided(box[player.PushedBoxIndex].X, box[player.PushedBoxIndex].Y, trap[trapId].X, trap[trapId].Y))
                    {
                        trap[trapId].IsObjOnTrap = true;
                        activatedTrapId = trapId;
                        break;
                    }
                }
                if (trap[activatedTrapId].IsObjOnTrap)
                {
                    Console.Clear();
                    Console.WriteLine("YOU JUST ACTIVATED TRAP\nTRY AGAIN");

                    break;
                }

            }


            // 프레임을 그립니다.
            void Render()
            {
                // 이전 프레임을 지운다.
                Console.Clear();

                // 골을 그린다.
                for (int i = 0; i < GOAL_COUNT; ++i)
                {
                    RenderObject(goal[i].X, goal[i].Y, "▢", ConsoleColor.DarkBlue);
                }

                // 플레이어를 그린다.
                RenderObject(player.X, player.Y, "☺", ConsoleColor.Black);


                // 벽을 그린다.
                for (int wallId = 0; wallId < WALL_COUNT; ++wallId)
                {
                    RenderObject(wall[wallId].X, wall[wallId].Y, "▒", ConsoleColor.DarkMagenta);
                }

                // 테두리를 그린다.
                for (int i = -1; i <= OUTLINE_LENGTH_X; ++i)
                {
                    RenderObject(MIN_X + i, MIN_Y - 1, "▒", ConsoleColor.DarkMagenta);
                    RenderObject(MIN_X + i, MAX_Y + 1, "▒", ConsoleColor.DarkMagenta);
                }
                for (int i = 0; i <= OUTLINE_LENGTH_Y; ++i)
                {
                    RenderObject(MIN_X - 1, MIN_Y + i, "▒", ConsoleColor.DarkMagenta);
                    RenderObject(MAX_X + 1, MIN_Y + i, "▒", ConsoleColor.DarkMagenta);
                }

                // 워프를 그린다.
                for (int i = 0; i < WARP_COUNT; ++i)
                {
                    RenderObject(warpInOut[i].X, warpInOut[i].Y, "℧", ConsoleColor.Cyan);
                    RenderObject(warpOutIn[i].X, warpOutIn[i].Y, "℧", ConsoleColor.Cyan);
                }

                // 포탈을 그린다.
                for (int i = 1; i < PORTAL_COUNT; ++i)
                {
                    if (player.X == portal[i].X && player.Y == portal[i].Y)
                    {
                        RenderObject(portal[i].X, portal[i].Y, "☻", ConsoleColor.DarkCyan);
                    }
                    else
                    {
                        RenderObject(portal[i].X, portal[i].Y, "Ω", ConsoleColor.DarkCyan);
                    }
                }
                // 박스를 그린다.
                for (int boxId = 0; boxId < BOX_COUNT; ++boxId)
                {
                    if (true == box[boxId].IsOnGoal)
                    {
                        RenderObject(box[boxId].X, box[boxId].Y, "▣", ConsoleColor.DarkBlue);
                    }
                    else
                    {
                        RenderObject(box[boxId].X, box[boxId].Y, "▨", ConsoleColor.DarkYellow);
                    }
                }

                // 함정을 그린다.
                for (int trapId = 0; trapId < TRAP_COUNT; ++trapId)
                {
                    RenderObject(trap[trapId].X, trap[trapId].Y, "⊗", ConsoleColor.Red);
                }


                // 메시지 모음

                // 전체화면
                RenderObject(statusMessage.FullScreenX, statusMessage.FullScreenY, $"전체 화면이 아닌 큰 화면 플레이를 권장합니다", ConsoleColor.DarkBlue);
                // 폰트
                RenderObject(statusMessage.FontX, statusMessage.FontY, $"폰트 크기를 크게 사용하기를 권장합니다", ConsoleColor.DarkBlue);
                // 왈왈
                RenderObject(statusMessage.NearThanX, statusMessage.NearThanY, $"사물이 보이는 것보다 가까이 있습니다", ConsoleColor.DarkBlue);

                // 조작 회수
                RenderObject(statusMessage.OperationX, statusMessage.OperationY, $"현재 조작 회수 : {howMuchOperation}", ConsoleColor.Green);

                // 그랩 온오프 상태 메시지
                if (Grab.Grab == player.GrabOnOff)
                {
                    RenderObject(statusMessage.GrabX, statusMessage.GrabY, "Grab Toggle : On", ConsoleColor.Green);
                }
                else
                {
                    RenderObject(statusMessage.GrabX, statusMessage.GrabY, "Grab Toggle : Off", ConsoleColor.Green);
                }

                RenderObject(statusMessage.CurrentKeyX, statusMessage.CurrentKeyY, $"현재 입력 키 : {key}", ConsoleColor.Red);
                RenderObject(statusMessage.CurrentKeyX, statusMessage.CurrentKeyY + 1, $"플레이어의 현재 좌표 ({player.X}, {player.Y})", ConsoleColor.Red);

                Console.SetCursorPosition(0, 0);

            }

            // 오브젝트를 그립니다.
            void RenderObject(int x, int y, string obj, ConsoleColor color)
            {
                ConsoleColor temp = Console.ForegroundColor;
                Console.SetCursorPosition(x, y);
                Console.ForegroundColor = color;
                Console.Write(obj);
                Console.ForegroundColor = temp;
            }

            void Update(ConsoleKey key)
            {
                // 플레이어 이동 처리
                MovePlayer(key, player);
                ActPlayer(key, ref player.GrabOnOff);
                // 플레이어가 포탈에 들어갔을 때
                for (int i = 1; i < PORTAL_COUNT; ++i)
                {
                    if (false == IsCollided(player.X, player.Y, portal[i].X, portal[i].Y))
                    {
                        continue;
                    }

                    PortalPlayer(key, player);

                    break;
                }
                // 플레이어가 인아웃 워프에 들어갔을 때
                for (int warpId = 0; warpId < WARP_COUNT; ++warpId)
                {
                    if (false == IsCollided(player.X, player.Y, warpInOut[warpId].X, warpInOut[warpId].Y))
                    {
                        continue;
                    }
                    OnCollision(() =>
                    {
                        MoveObj(player.MoveDirection,
                            ref player.X, ref player.Y,
                            in warpOutIn[warpId].X, in warpOutIn[warpId].Y);
                    });
                    break;
                }
                // 플레이어가 아웃인 워프에 들어갔을 때
                for (int warpId = 0; warpId < WARP_COUNT; ++warpId)
                {
                    if (false == IsCollided(player.X, player.Y, warpOutIn[warpId].X, warpOutIn[warpId].Y))
                    {
                        continue;
                    }
                    OnCollision(() =>
                    {
                        MoveObj(player.MoveDirection,
                            ref player.X, ref player.Y,
                            in warpInOut[warpId].X, in warpInOut[warpId].Y);
                    });
                    break;
                }
                // 플레이어와 벽의 충돌 처리
                for (int wallId = 0; wallId < WALL_COUNT; ++wallId)
                {
                    if (false == IsCollided(player.X, player.Y, wall[wallId].X, wall[wallId].Y))
                    {
                        continue;
                    }

                    OnCollision(() =>
                    {
                        PushOut(player.MoveDirection,
                            ref player.X, ref player.Y,
                            wall[wallId].X, wall[wallId].Y);
                    });

                    break;
                }


                // 박스 이동 처리
                // 플레이어가 박스를 밀었을 때라는 게 무엇을 의미하는가? => 플레이어가 이동했는데 플레이어의 위치와 박스 위치가 겹쳤다.
                for (int i = 0; i < BOX_COUNT; ++i)
                {
                    if (false == IsCollided(player.X, player.Y, box[i].X, box[i].Y))
                    {
                        continue;
                    }
                    OnCollision(() =>
                    {
                        MoveObj(player.MoveDirection,
                            ref box[i].X, ref box[i].Y,
                            in player.X, in player.Y);
                    });
                    player.PushedBoxIndex = i;

                    break;
                }
                // 박스가 인아웃 포탈에 들어갔을 때
                for (int warpId = 0; warpId < WARP_COUNT; ++warpId)
                {
                    if (false == IsCollided(box[player.PushedBoxIndex].X, box[player.PushedBoxIndex].Y, warpInOut[warpId].X, warpInOut[warpId].Y))
                    {
                        continue;
                    }

                    OnCollision(() =>
                    {
                        MoveObj(player.MoveDirection,
                            ref box[player.PushedBoxIndex].X, ref box[player.PushedBoxIndex].Y,
                            in warpOutIn[warpId].X, in warpOutIn[warpId].Y);
                    });

                    // 포탈앞에 이미 박스가 있을 때
                    for (int collidedBoxId = 0; collidedBoxId < BOX_COUNT; ++collidedBoxId)
                    {
                        if (player.PushedBoxIndex == collidedBoxId)
                        {
                            continue;
                        }

                        if (false == IsCollided(box[player.PushedBoxIndex].X, box[player.PushedBoxIndex].Y, box[collidedBoxId].X, box[collidedBoxId].Y))
                        {
                            continue;
                        }

                        OnCollision(() =>
                        {
                            PushOut(player.MoveDirection,
                                ref box[player.PushedBoxIndex].X, ref box[player.PushedBoxIndex].Y,
                                warpInOut[warpId].X, warpInOut[warpId].Y);

                            PushOut(player.MoveDirection,
                                ref player.X, ref player.Y,
                                box[player.PushedBoxIndex].X, box[player.PushedBoxIndex].Y);
                        });

                        break;
                    }

                }
                // 박스가 아웃인 포탈에 들어갔을 때
                for (int warpId = 0; warpId < WARP_COUNT; ++warpId)
                {
                    if (false == IsCollided(box[player.PushedBoxIndex].X, box[player.PushedBoxIndex].Y, warpOutIn[warpId].X, warpOutIn[warpId].Y))
                    {
                        continue;
                    }
                    OnCollision(() =>
                    {
                        MoveObj(player.MoveDirection,
                            ref box[player.PushedBoxIndex].X, ref box[player.PushedBoxIndex].Y,
                            in warpInOut[warpId].X, in warpInOut[warpId].Y);
                    });
                    // 포탈앞에 이미 박스가 있을 때
                    for (int collidedBoxId = 0; collidedBoxId < BOX_COUNT; ++collidedBoxId)
                    {
                        if (player.PushedBoxIndex == collidedBoxId)
                        {
                            continue;
                        }

                        if (false == IsCollided(box[player.PushedBoxIndex].X, box[player.PushedBoxIndex].Y, box[collidedBoxId].X, box[collidedBoxId].Y))
                        {
                            continue;
                        }

                        OnCollision(() =>
                        {
                            PushOut(player.MoveDirection,
                                ref box[player.PushedBoxIndex].X, ref box[player.PushedBoxIndex].Y,
                                warpOutIn[warpId].X, warpOutIn[warpId].Y);

                            PushOut(player.MoveDirection,
                                ref player.X, ref player.Y,
                                box[player.PushedBoxIndex].X, box[player.PushedBoxIndex].Y);
                        });

                        break;
                    }
                }
                // 박스와 벽의 충돌 처리
                for (int wallId = 0; wallId < WALL_COUNT; ++wallId)
                {
                    if (false == IsCollided(box[player.PushedBoxIndex].X, box[player.PushedBoxIndex].Y, wall[wallId].X, wall[wallId].Y))
                    {
                        continue;
                    }

                    OnCollision(() =>
                    {
                        PushOut(player.MoveDirection,
                            ref box[player.PushedBoxIndex].X, ref box[player.PushedBoxIndex].Y,
                            wall[wallId].X, wall[wallId].Y);

                        PushOut(player.MoveDirection,
                            ref player.X, ref player.Y,
                            box[player.PushedBoxIndex].X, box[player.PushedBoxIndex].Y);
                    });

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

                    if (false == IsCollided(box[player.PushedBoxIndex].X, box[player.PushedBoxIndex].Y, box[collidedBoxId].X, box[collidedBoxId].Y))
                    {
                        continue;
                    }

                    OnCollision(() =>
                    {
                        PushOut(player.MoveDirection,
                            ref box[player.PushedBoxIndex].X, ref box[player.PushedBoxIndex].Y,
                            box[collidedBoxId].X, box[collidedBoxId].Y);

                        PushOut(player.MoveDirection,
                            ref player.X, ref player.Y,
                            box[player.PushedBoxIndex].X, box[player.PushedBoxIndex].Y);
                    });


                    break;
                }
                // 박스 그랩처리
                for (int i = 0; i < BOX_COUNT; ++i)
                {
                    if (Grab.Grab == player.GrabOnOff)
                    {
                        if (Direction.Left == player.MoveDirection && IsCollided(player.X + 2, player.Y, box[i].X, box[i].Y) && IsCollided(player.PastX + 1, player.PastY, box[i].X, box[i].Y))
                        {
                            box[i].X = player.X + 1;
                        }
                        if (Direction.Right == player.MoveDirection && IsCollided(player.X - 2, player.Y, box[i].X, box[i].Y) && IsCollided(player.PastX - 1, player.PastY, box[i].X, box[i].Y))
                        {
                            box[i].X = player.X - 1;
                        }
                        if (Direction.Up == player.MoveDirection && IsCollided(player.X, player.Y + 2, box[i].X, box[i].Y) && IsCollided(player.PastX, player.PastY + 1, box[i].X, box[i].Y))
                        {
                            box[i].Y = player.Y + 1;
                        }
                        if (Direction.Down == player.MoveDirection && IsCollided(player.X, player.Y - 2, box[i].X, box[i].Y) && IsCollided(player.PastX, player.PastY - 1, box[i].X, box[i].Y))
                        {
                            box[i].Y = player.Y - 1;
                        }
                        player.GrabedBoxIndex = i;
                    }
                }
            }

            // 플레이어를 이동시킨다.
            void MovePlayer(ConsoleKey key, Player player)
            {
                player.MoveDirection = Direction.None;

                if (key == ConsoleKey.LeftArrow)
                {
                    player.PastX = player.X;
                    player.PastY = player.Y;
                    MoveToLeftOfTarget(out player.X, out player.Y, in player.X, in player.Y);
                    player.MoveDirection = Direction.Left;
                }

                if (key == ConsoleKey.RightArrow)
                {
                    player.PastX = player.X;
                    player.PastY = player.Y;
                    MoveToRightOfTarget(out player.X, out player.Y, in player.X, in player.Y);
                    player.MoveDirection = Direction.Right;
                }

                if (key == ConsoleKey.UpArrow)
                {
                    player.PastX = player.X;
                    player.PastY = player.Y;
                    MoveToUpOfTarget(out player.X, out player.Y, in player.X, in player.Y);
                    player.MoveDirection = Direction.Up;
                }

                if (key == ConsoleKey.DownArrow)
                {
                    player.PastX = player.X;
                    player.PastY = player.Y;
                    MoveToDownOfTarget(out player.X, out player.Y, in player.X, in player.Y);
                    player.MoveDirection = Direction.Down;
                }

                howMuchOperation += 1;
            }
            void ActPlayer(ConsoleKey key, ref Grab action)
            {

                // 그랩 토글처리
                if (key == ConsoleKey.G)
                {
                    if (action == Grab.Grab)
                    {
                        action = Grab.None;
                    }
                    else
                    {
                        action = Grab.Grab;
                    }

                }
            }
            void PortalPlayer(ConsoleKey key, Player player)
            {
                if (key == ConsoleKey.D1)
                {
                    player.X = portal[(int)PortalNum.One].X;
                    player.Y = portal[(int)PortalNum.One].Y;
                }
                if (key == ConsoleKey.D2)
                {
                    player.X = portal[(int)PortalNum.Two].X;
                    player.Y = portal[(int)PortalNum.Two].Y;
                }
                if (key == ConsoleKey.D3)
                {
                    player.X = portal[(int)PortalNum.Three].X;
                    player.Y = portal[(int)PortalNum.Three].Y;
                }
                if (key == ConsoleKey.D4)
                {
                    player.X = portal[(int)PortalNum.Four].X;
                    player.Y = portal[(int)PortalNum.Four].Y;
                }
            }

            // 두 물체가 충돌했는지 판별합니다.
            bool IsCollided(in int objX, in int objY, in int targetX, in int targetY)
            {
                if (objX == targetX && objY == targetY)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            // 충돌처리
            void OnCollision(Action action)
            {
                action.Invoke();
            }

            void PushOut(Direction playerMoveDirection,
                ref int objX, ref int objY,
                in int collidedObjX, in int collidedObjY)
            {
                switch (playerMoveDirection)
                {
                    case Direction.Left:
                        MoveToRightOfTarget(out objX, out objY, in collidedObjX, in collidedObjY);
                        break;
                    case Direction.Right:
                        MoveToLeftOfTarget(out objX, out objY, in collidedObjX, in collidedObjY);
                        break;
                    case Direction.Up:
                        MoveToDownOfTarget(out objX, out objY, in collidedObjX, in collidedObjY);
                        break;
                    case Direction.Down:
                        MoveToUpOfTarget(out objX, out objY, in collidedObjX, in collidedObjY);
                        break;
                    default:
                        ExitWithError($"[Error] 플레이어 이동 방향 데이터가 오류입니다. : {playerMoveDirection}");
                        return;
                }
            }
            void MoveObj(Direction playerMoveDirection,
                ref int objX, ref int objY,
                in int targetX, in int targetY)
            {
                switch (playerMoveDirection)
                {
                    case Direction.Left:
                        MoveToLeftOfTarget(out objX, out objY, in targetX, in targetY);
                        break;
                    case Direction.Right:
                        MoveToRightOfTarget(out objX, out objY, in targetX, in targetY);
                        break;
                    case Direction.Up:
                        MoveToUpOfTarget(out objX, out objY, in targetX, in targetY);
                        break;
                    case Direction.Down:
                        MoveToDownOfTarget(out objX, out objY, in targetX, in targetY);
                        break;
                    default:
                        ExitWithError($"[Error] 플레이어 이동 방향 데이터가 오류입니다. : {playerMoveDirection}");
                        return;
                }
            }



            // 에러 메시지 출력 후 종료 처리
            void ExitWithError(string errorMessage)
            {
                Console.Clear();
                Console.WriteLine(errorMessage);
                Environment.Exit(1);
            }

            // 타겟 근처로 이동시킨다.
            void MoveToLeftOfTarget(out int x, out int y, in int targetX, in int targetY)
            {
                x = Math.Max(MIN_X, targetX - 1);
                y = targetY;
            }
            void MoveToRightOfTarget(out int x, out int y, in int targetX, in int targetY)
            {
                x = Math.Min(targetX + 1, MAX_X);
                y = targetY;
            }
            void MoveToUpOfTarget(out int x, out int y, in int targetX, in int targetY)
            {
                x = targetX;
                y = Math.Max(MIN_Y, targetY - 1);
            }
            void MoveToDownOfTarget(out int x, out int y, in int targetX, in int targetY)
            {
                x = targetX;
                y = Math.Min(targetY + 1, MAX_Y);
            }

        }
    }
}