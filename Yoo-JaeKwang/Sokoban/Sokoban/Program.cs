using System;
using System.Runtime.InteropServices;
using Sokoban;

namespace Sokoban
{
    

    class Program
    {
        static void Main()
        {
            Player player = new Player
            {
                X = 7,
                Y = 4,
                PastX = 7,
                PastY = 4,
                MoveDirection = Direction.None,
                GrabOnOff = Grab.None,
                OnMain = true,
                OnMine = false
            };

            Game game = new Game
            {
                PushedBoxId = 0,
                // GrabedBoxId = 0,
                ActivatedTrapId = 0,
                PortalId = PortalNum.None,
            };

            Box[] box = new Box[Game.BOX_COUNT]
            {
                new Box { X = 14, Y = 4, PastX = 0, PastY = 0, IsOnGoal = false },
                new Box { X = 9, Y = 12, PastX = 0, PastY = 0, IsOnGoal = false },
                new Box { X = 16, Y = 14, PastX = 0, PastY = 0, IsOnGoal = false },
                new Box { X = 8, Y = 9, PastX = 0, PastY = 0, IsOnGoal = false }
            };

            Wall[] wall = new Wall[Game.WALL_COUNT]
            {
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
                new Wall { X = 27, Y = 15 },
                new Wall { X = 27, Y = 16 },
                new Wall { X = 27, Y = 17 },
                new Wall { X = 27, Y = 18 },
                new Wall { X = 27, Y = 19 },
                new Wall { X = 27, Y = 20 },
                new Wall { X = 27, Y = 21 },
                new Wall { X = 27, Y = 22 },
                new Wall { X = 27, Y = 23 }

            };

            Goal[] goal = new Goal[Game.GOAL_COUNT]
            {
                new Goal { X = 24, Y = 17 },
                new Goal { X = 19, Y = 12 },
                new Goal { X = 19, Y = 17 },
                new Goal { X = 35, Y = 21 }
            };

            WarpInOut[] warpInOut = new WarpInOut[Game.WARP_COUNT]
            {
                new WarpInOut { X = 14, Y = 7 }
            };
            WarpOutIn[] warpOutIn = new WarpOutIn[Game.WARP_COUNT]
            {
                new WarpOutIn { X = 31, Y = 19 }
            };

            Portal[] portal = new Portal[Game.PORTAL_COUNT]
            {
                new Portal { X = 0, Y = 0 },
                new Portal { X = 8, Y = 5 },
                new Portal { X = 34, Y = 5 },
                new Portal{ X = 8, Y = 22 },
                new Portal{ X = 34, Y = 22 }
            };

            Trap[] trap = new Trap[Game.TRAP_COUNT]
            {
                new Trap { X = 5, Y = 3, IsObjOnTrap = false },
                new Trap { X = 5, Y = 23, IsObjOnTrap = false },
                new Trap { X = 37, Y = 3, IsObjOnTrap = false },
                new Trap { X = 37, Y = 23, IsObjOnTrap = false }
            };

            Mine.Tunnel mineTunnel = new Mine.Tunnel
            {
                InMainX = 37,
                InMainY = 16,
                InMineX = 72,
                InMineY = 20
            };
        
            Mine.Mineral[] mineral = new Mine.Mineral[Game.MINERAL_COUNT]
            {
                new Mine.Mineral { X = 0, Y = 0, Name = "" },
                new Mine.Mineral { X = 52, Y = 16, Name = "Ruby" },
                new Mine.Mineral { X = 72, Y = 16, Name = "Gold" },
                new Mine.Mineral { X = 92, Y = 16, Name = "Emerald" },
                new Mine.Mineral { X = 52, Y = 23, Name = "Sapphire" },
                new Mine.Mineral { X = 72, Y = 23, Name = "Aquamarine" },
                new Mine.Mineral { X = 92, Y = 23, Name = "Diamond" }
            };
            
            StatusMessage statusMessage = new StatusMessage
            {
                FullScreenX = 51,
                FullScreenY = 2,
                FontX = 51,
                FontY = 3,
                NearThanX = 51,
                NearThanY = 5,
                OperationX = 51,
                OperationY = 7,
                GrabX = 51,
                GrabY = 8,
                CurrentKeyX = 51,
                CurrentKeyY = 12,
                HowMuchOperation = 0

            };

            ConsoleKey key = default;

            RenderOutGameLoop();

            // 게임 루프
            while (true)
            {
                RenderInGameLoop();

                key = Console.ReadKey().Key;

                Update(key);

                int boxOnGoalCount = 0;

                // 골인
                for (int boxId = 0; boxId < Game.BOX_COUNT; ++boxId)
                {
                    box[boxId].IsOnGoal = false; 

                    for (int goalId = 0; goalId < Game.GOAL_COUNT; ++goalId)
                    {
                        if (Game.Function.IsCollided(box[boxId].X, box[boxId].Y, goal[goalId].X, goal[goalId].Y))
                        {
                            ++boxOnGoalCount;

                            box[boxId].IsOnGoal = true; 

                            break;
                        }
                    }
                }

                // 모든 골 지점에 박스가 올라와 있다면?
                if (boxOnGoalCount == Game.GOAL_COUNT)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("축하합니다. 클리어 하셨습니다.");

                    break;
                }

                // 오브젝트가 함정을 밟았다면
                for (int trapId = 0; trapId < Game.TRAP_COUNT; ++trapId)
                {
                    if (Game.Function.IsCollided(player.X, player.Y, trap[trapId].X, trap[trapId].Y) || Game.Function.IsCollided(box[game.PushedBoxId].X, box[game.PushedBoxId].Y, trap[trapId].X, trap[trapId].Y))
                    {
                        trap[trapId].IsObjOnTrap = true;
                        game.ActivatedTrapId = trapId;
                        break;
                    }
                }
                if (trap[game.ActivatedTrapId].IsObjOnTrap)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("YOU JUST ACTIVATED TRAP\nTRY AGAIN");

                    break;
                }

            }

            // Render
            void RenderInGameLoop()
            {
                // 움직이는 오브젝트의 이전 프레임을 지운다.
                Game.Function.RenderObject(player.PastX, player.PastY, " ", ConsoleColor.Black);
                for (int boxId = 0; boxId < Game.BOX_COUNT; ++boxId)
                {
                    if (true == Game.Function.IsCollided(box[boxId].X, box[boxId].Y, box[boxId].PastX, box[boxId].PastY))
                    {
                        continue;
                    }
                    Game.Function.RenderObject(box[boxId].PastX, box[boxId].PastY, " ", ConsoleColor.Black);
                }

                // 플레이어를 그린다.
                Game.Function.RenderObject(player.X, player.Y, "☺", ConsoleColor.Black);

                // 골을 그린다.
                for (int i = 0; i < Game.GOAL_COUNT; ++i)
                {
                    if (true == Game.Function.IsCollided(player.X, player.Y, goal[i].X, goal[i].Y))
                    {
                        Game.Function.RenderObject(player.X, player.Y, "☺", ConsoleColor.Black);
                    }
                    else
                    {
                        Game.Function.RenderObject(goal[i].X, goal[i].Y, "▢", ConsoleColor.DarkBlue);
                    }
                }

                // 워프를 그림
                for (int i = 0; i < Game.WARP_COUNT; ++i)
                {
                    Game.Function.RenderObject(warpInOut[i].X, warpInOut[i].Y, "℧", ConsoleColor.Cyan);
                    Game.Function.RenderObject(warpOutIn[i].X, warpOutIn[i].Y, "℧", ConsoleColor.Cyan);
                }

                // 포탈을 그림
                for (int i = 1; i < Game.PORTAL_COUNT; ++i)
                {
                    if (true == Game.Function.IsCollided(player.X, player.Y, portal[i].X, portal[i].Y))
                    {
                        Game.Function.RenderObject(portal[i].X, portal[i].Y, "☻", ConsoleColor.DarkCyan);
                    }
                    else
                    {
                        Game.Function.RenderObject(portal[i].X, portal[i].Y, "Ω", ConsoleColor.DarkCyan);
                    }
                }
                // 박스를 그린다.

                for (int boxId = 0; boxId < Game.BOX_COUNT; ++boxId)
                {
                    if (true == box[boxId].IsOnGoal)
                    {
                        Game.Function.RenderObject(box[boxId].X, box[boxId].Y, "▣", ConsoleColor.DarkBlue);
                    }
                    else
                    {
                        Game.Function.RenderObject(box[boxId].X, box[boxId].Y, "▨", ConsoleColor.DarkYellow);
                    }
                }

                // 함정을 그린다.
                for (int trapId = 0; trapId < Game.TRAP_COUNT; ++trapId)
                {
                    Game.Function.RenderObject(trap[trapId].X, trap[trapId].Y, "⊗", ConsoleColor.Red);
                }

                // 메인 터널을 그린다.
                if (true == Game.Function.IsCollided(player.X, player.Y, mineTunnel.InMainX, mineTunnel.InMainY))
                {
                    Game.Function.RenderObject(mineTunnel.InMainX, mineTunnel.InMainY, "☻", ConsoleColor.Gray);
                }
                else
                {
                    Game.Function.RenderObject(mineTunnel.InMainX, mineTunnel.InMainY, "≎", ConsoleColor.Gray);
                }

                if (true == Game.Function.IsCollided(player.X, player.Y, mineTunnel.InMineX, mineTunnel.InMineY))
                {
                    Game.Function.RenderObject(mineTunnel.InMineX, mineTunnel.InMineY, "☻", ConsoleColor.Gray);
                }
                else
                {
                    Game.Function.RenderObject(mineTunnel.InMineX, mineTunnel.InMineY, "≎", ConsoleColor.Gray);
                }

                // 메시지
                // 조작 회수
                Game.Function.RenderObject(statusMessage.OperationX, statusMessage.OperationY, $"현재 조작 회수 : {statusMessage.HowMuchOperation}", ConsoleColor.Green);
                // 그랩 온오프 상태 메시지
                if (Grab.Grab == player.GrabOnOff)
                {
                    Game.Function.RenderObject(statusMessage.GrabX, statusMessage.GrabY, "Grab Toggle : On ", ConsoleColor.Green);
                }
                else
                {
                    Game.Function.RenderObject(statusMessage.GrabX, statusMessage.GrabY, "Grab Toggle : Off", ConsoleColor.Green);
                }

                
                Game.Function.RenderObject(statusMessage.CurrentKeyX, statusMessage.CurrentKeyY, $"현재 입력 키 :                                ", ConsoleColor.Red);
                Game.Function.RenderObject(statusMessage.CurrentKeyX, statusMessage.CurrentKeyY, $"현재 입력 키 : {key}", ConsoleColor.Red);
                Game.Function.RenderObject(statusMessage.CurrentKeyX, statusMessage.CurrentKeyY + 1, $"플레이어의 현재 좌표 (  ,        ", ConsoleColor.Red);
                Game.Function.RenderObject(statusMessage.CurrentKeyX, statusMessage.CurrentKeyY + 1, $"플레이어의 현재 좌표 ({player.X}, {player.Y})", ConsoleColor.Red);

                Console.SetCursorPosition(0, 0);

            }
            void RenderOutGameLoop()
            {
                // 초기 세팅
                Console.ResetColor();                                // 컬러를 초기화한다.
                Console.CursorVisible = false;                       // 커서를 숨긴다.
                Console.Title = "경이루 아카데미";                    // 타이틀을 설정한다.
                Console.BackgroundColor = ConsoleColor.White;       // 배경색을 설정한다.
                Console.ForegroundColor = ConsoleColor.White;       // 글꼴색을 설정한다.
                Console.OutputEncoding = System.Text.Encoding.UTF8; // UTF8 문자 허용
                Console.Clear();                                    // 출력된 모든 내용을 지운다.

                // 메시지
                // 전체화면
                Game.Function.RenderObject(statusMessage.FullScreenX, statusMessage.FullScreenY, $"전체 화면이 아닌 큰 화면 플레이를 권장합니다", ConsoleColor.DarkBlue);
                // 폰트
                Game.Function.RenderObject(statusMessage.FontX, statusMessage.FontY, $"폰트 크기를 크게 사용하기를 권장합니다", ConsoleColor.DarkBlue);
                // 왈왈
                Game.Function.RenderObject(statusMessage.NearThanX, statusMessage.NearThanY, $"사물이 보이는 것보다 가까이 있습니다", ConsoleColor.DarkBlue);
                // 조작 가능 키
                Game.Function.RenderObject(statusMessage.CurrentKeyX, statusMessage.CurrentKeyY - 2, $"조작 가능 키 : 1, 2, 3, 4,↑", ConsoleColor.Red);
                Game.Function.RenderObject(statusMessage.CurrentKeyX, statusMessage.CurrentKeyY - 1, $"                  G, M, ←↓→  ", ConsoleColor.Red);

                // 벽을 그린다.
                for (int wallId = 0; wallId < Game.WALL_COUNT; ++wallId)
                {
                    Game.Function.RenderObject(wall[wallId].X, wall[wallId].Y, "▒", ConsoleColor.DarkMagenta);
                }
                // 메인 스테이지 테두리를 그린다.
                for (int i = -1; i <= Game.OUTLINE_LENGTH_X; ++i)
                {
                    Game.Function.RenderObject(Game.MIN_X + i, Game.MIN_Y - 1, "▒", ConsoleColor.DarkMagenta);
                    Game.Function.RenderObject(Game.MIN_X + i, Game.MAX_Y + 1, "▒", ConsoleColor.DarkMagenta);
                }
                for (int i = 0; i <= Game.OUTLINE_LENGTH_Y; ++i)
                {
                    Game.Function.RenderObject(Game.MIN_X - 1, Game.MIN_Y + i, "▒", ConsoleColor.DarkMagenta);
                    Game.Function.RenderObject(Game.MAX_X + 1, Game.MIN_Y + i, "▒", ConsoleColor.DarkMagenta);
                }
                // 광산 테두리를 그린다.
                for (int i = -1; i <= Game.MINE_OUTLINE_LENGTH_X; ++i)
                {
                    Game.Function.RenderObject(Game.MINE_MIN_X + i, Game.MINE_MIN_Y - 1, "▒", ConsoleColor.DarkMagenta);
                    Game.Function.RenderObject(Game.MINE_MIN_X + i, Game.MINE_MAX_Y + 1, "▒", ConsoleColor.DarkMagenta);
                }
                for (int i = 0; i <= Game.MINE_OUTLINE_LENGTH_Y; ++i)
                {
                    Game.Function.RenderObject(Game.MINE_MIN_X - 1, Game.MINE_MIN_Y + i, "▒", ConsoleColor.DarkMagenta);
                    Game.Function.RenderObject(Game.MINE_MAX_X + 1, Game.MINE_MIN_Y + i, "▒", ConsoleColor.DarkMagenta);
                }
                // 미네랄을 그린다.
                Game.Function.RenderObject(mineral[(int)Mineral.Ruby].X, mineral[(int)Mineral.Ruby].Y, "♦", ConsoleColor.Red);
                Game.Function.RenderObject(mineral[(int)Mineral.Gold].X, mineral[(int)Mineral.Gold].Y, "♦", ConsoleColor.DarkYellow);
                Game.Function.RenderObject(mineral[(int)Mineral.Emerald].X, mineral[(int)Mineral.Emerald].Y, "♦", ConsoleColor.Green);
                Game.Function.RenderObject(mineral[(int)Mineral.Sapphire].X, mineral[(int)Mineral.Sapphire].Y, "♦", ConsoleColor.DarkBlue);
                Game.Function.RenderObject(mineral[(int)Mineral.Aquamarine].X, mineral[(int)Mineral.Aquamarine].Y, "♦", ConsoleColor.Cyan);
                Game.Function.RenderObject(mineral[(int)Mineral.Diamond].X, mineral[(int)Mineral.Diamond].Y, "♦", ConsoleColor.DarkGray);
            }

            // Update
            void Update(ConsoleKey key)
            {
                // 플레이어 이동 처리
                Game.Function.MovePlayer(key, player, statusMessage);
                Game.Function.ActPlayer(key, ref player.GrabOnOff);
                // 플레이어가 터널을 사용할 때
                Game.Function.TunnelPlayer(key, player, mineTunnel);
                // 플레이어가 포탈에 들어갔을 때
                for (int i = 1; i < Game.PORTAL_COUNT; ++i)
                {
                    if (false == Game.Function.IsCollided(player.X, player.Y, portal[i].X, portal[i].Y))
                    {
                        continue;
                    }

                    Game.Function.PortalPlayer(key, player, portal);

                    break;
                }
                // 플레이어가 인아웃 워프에 들어갔을 때
                for (int warpId = 0; warpId < Game.WARP_COUNT; ++warpId)
                {
                    if (false == Game.Function.IsCollided(player.X, player.Y, warpInOut[warpId].X, warpInOut[warpId].Y))
                    {
                        continue;
                    }
                    Game.Function.OnCollision(() =>
                    {
                        Game.Function.MoveObj(player.MoveDirection,
                            ref player.X, ref player.Y,
                            in warpOutIn[warpId].X, in warpOutIn[warpId].Y);
                    });
                    break;
                }
                // 플레이어가 아웃인 워프에 들어갔을 때
                for (int warpId = 0; warpId < Game.WARP_COUNT; ++warpId)
                {
                    if (false == Game.Function.IsCollided(player.X, player.Y, warpOutIn[warpId].X, warpOutIn[warpId].Y))
                    {
                        continue;
                    }
                    Game.Function.OnCollision(() =>
                    {
                        Game.Function.MoveObj(player.MoveDirection,
                            ref player.X, ref player.Y,
                            in warpInOut[warpId].X, in warpInOut[warpId].Y);
                    });
                    break;
                }
                // 플레이어와 벽의 충돌 처리
                for (int wallId = 0; wallId < Game.WALL_COUNT; ++wallId)
                {
                    if (false == Game.Function.IsCollided(player.X, player.Y, wall[wallId].X, wall[wallId].Y))
                    {
                        continue;
                    }

                    Game.Function.OnCollision(() =>
                    {
                        Game.Function.PushOutInMain(player.MoveDirection,
                            ref player.X, ref player.Y,
                            wall[wallId].X, wall[wallId].Y);
                    });

                    break;
                }
                // 플레이어와 미네랄의 충돌 처리
                for (int mineralId = 1; mineralId < Game.MINERAL_COUNT; ++mineralId)
                {
                    if (false == Game.Function.IsCollided(player.X, player.Y, mineral[mineralId].X, mineral[mineralId].Y))
                    {
                        continue;
                    }

                    Game.Function.OnCollision(() =>
                    {
                        Game.Function.PushOutInMine(player.MoveDirection,
                            ref player.X, ref player.Y,
                            mineral[mineralId].X, mineral[mineralId].Y);
                    });

                }


                // 박스 이동 처리
                for (int i = 0; i < Game.BOX_COUNT; ++i)
                {
                    if (false == Game.Function.IsCollided(player.X, player.Y, box[i].X, box[i].Y))
                    {
                        continue;
                    }
                    Game.Function.OnCollision(() =>
                    {
                        Game.Function.MoveObj(player.MoveDirection,
                            ref box[i].X, ref box[i].Y,
                            in player.X, in player.Y);

                        Game.Function.PushOutInMain(player.MoveDirection,
                            ref player.X, ref player.Y,
                            in box[i].X, in box[i].Y);
                    });
                    game.PushedBoxId = i;

                    break;
                }
                // 박스가 인아웃 포탈에 들어갔을 때
                for (int warpId = 0; warpId < Game.WARP_COUNT; ++warpId)
                {
                    if (false == Game.Function.IsCollided(box[game.PushedBoxId].X, box[game.PushedBoxId].Y, warpInOut[warpId].X, warpInOut[warpId].Y))
                    {
                        continue;
                    }

                    Game.Function.OnCollision(() =>
                    {
                        Game.Function.MoveObj(player.MoveDirection,
                            ref box[game.PushedBoxId].X, ref box[game.PushedBoxId].Y,
                            in warpOutIn[warpId].X, in warpOutIn[warpId].Y);
                    });

                    // 포탈앞에 이미 박스가 있을 때
                    for (int collidedBoxId = 0; collidedBoxId < Game.BOX_COUNT; ++collidedBoxId)
                    {
                        if (game.PushedBoxId == collidedBoxId)
                        {
                            continue;
                        }

                        if (false == Game.Function.IsCollided(box[game.PushedBoxId].X, box[game.PushedBoxId].Y, box[collidedBoxId].X, box[collidedBoxId].Y))
                        {
                            continue;
                        }

                        Game.Function.OnCollision(() =>
                        {
                            Game.Function.PushOutInMain(player.MoveDirection,
                                ref box[game.PushedBoxId].X, ref box[game.PushedBoxId].Y,
                                warpInOut[warpId].X, warpInOut[warpId].Y);

                            Game.Function.PushOutInMain(player.MoveDirection,
                                ref player.X, ref player.Y,
                                box[game.PushedBoxId].X, box[game.PushedBoxId].Y);
                        });

                        break;
                    }

                }
                // 박스가 아웃인 포탈에 들어갔을 때
                for (int warpId = 0; warpId < Game.WARP_COUNT; ++warpId)
                {
                    if (false == Game.Function.IsCollided(box[game.PushedBoxId].X, box[game.PushedBoxId].Y, warpOutIn[warpId].X, warpOutIn[warpId].Y))
                    {
                        continue;
                    }
                    Game.Function.OnCollision(() =>
                    {
                        Game.Function.MoveObj(player.MoveDirection,
                            ref box[game.PushedBoxId].X, ref box[game.PushedBoxId].Y,
                            in warpInOut[warpId].X, in warpInOut[warpId].Y);
                    });
                    // 포탈앞에 이미 박스가 있을 때
                    for (int collidedBoxId = 0; collidedBoxId < Game.BOX_COUNT; ++collidedBoxId)
                    {
                        if (game.PushedBoxId == collidedBoxId)
                        {
                            continue;
                        }

                        if (false == Game.Function.IsCollided(box[game.PushedBoxId].X, box[game.PushedBoxId].Y, box[collidedBoxId].X, box[collidedBoxId].Y))
                        {
                            continue;
                        }

                        Game.Function.OnCollision(() =>
                        {
                            Game.Function.PushOutInMain(player.MoveDirection,
                                ref box[game.PushedBoxId].X, ref box[game.PushedBoxId].Y,
                                warpOutIn[warpId].X, warpOutIn[warpId].Y);

                            Game.Function.PushOutInMain(player.MoveDirection,
                                ref player.X, ref player.Y,
                                box[game.PushedBoxId].X, box[game.PushedBoxId].Y);
                        });

                        break;
                    }
                }
                // 박스와 벽의 충돌
                for (int wallId = 0; wallId < Game.WALL_COUNT; ++wallId)
                {
                    if (false == Game.Function.IsCollided(box[game.PushedBoxId].X, box[game.PushedBoxId].Y, wall[wallId].X, wall[wallId].Y))
                    {
                        continue;
                    }

                    Game.Function.OnCollision(() =>
                    {
                        Game.Function.PushOutInMain(player.MoveDirection,
                            ref box[game.PushedBoxId].X, ref box[game.PushedBoxId].Y,
                            wall[wallId].X, wall[wallId].Y);

                        Game.Function.PushOutInMain(player.MoveDirection,
                            ref player.X, ref player.Y,
                            box[game.PushedBoxId].X, box[game.PushedBoxId].Y);
                    });

                    break;
                }
                // 박스끼리 충돌
                for (int collidedBoxId = 0; collidedBoxId < Game.BOX_COUNT; ++collidedBoxId)
                {
                    if (game.PushedBoxId == collidedBoxId)
                    {
                        continue;
                    }

                    if (false == Game.Function.IsCollided(box[game.PushedBoxId].X, box[game.PushedBoxId].Y, box[collidedBoxId].X, box[collidedBoxId].Y))
                    {
                        continue;
                    }

                    Game.Function.OnCollision(() =>
                    {
                        Game.Function.PushOutInMain(player.MoveDirection,
                            ref box[game.PushedBoxId].X, ref box[game.PushedBoxId].Y,
                            box[collidedBoxId].X, box[collidedBoxId].Y);

                        Game.Function.PushOutInMain(player.MoveDirection,
                            ref player.X, ref player.Y,
                            box[game.PushedBoxId].X, box[game.PushedBoxId].Y);
                    });


                    break;
                }
                // 박스 그랩
                for (int i = 0; i < Game.BOX_COUNT; ++i)
                {
                    if (Grab.Grab == player.GrabOnOff)
                    {
                        if (Direction.Left == player.MoveDirection && Game.Function.IsCollided(player.X + 2, player.Y, box[i].X, box[i].Y) && Game.Function.IsCollided(player.PastX + 1, player.PastY, box[i].X, box[i].Y))
                        {
                            box[i].PastX = box[i].X;
                            box[i].PastY = box[i].Y;
                            box[i].X = player.X + 1;
                            // game.GrabedBoxId = i;
                        }
                        if (Direction.Right == player.MoveDirection && Game.Function.IsCollided(player.X - 2, player.Y, box[i].X, box[i].Y) && Game.Function.IsCollided(player.PastX - 1, player.PastY, box[i].X, box[i].Y))
                        {
                            box[i].PastX = box[i].X;
                            box[i].PastY = box[i].Y;
                            box[i].X = player.X - 1;
                            // game.GrabedBoxId = i;
                        }
                        if (Direction.Up == player.MoveDirection && Game.Function.IsCollided(player.X, player.Y + 2, box[i].X, box[i].Y) && Game.Function.IsCollided(player.PastX, player.PastY + 1, box[i].X, box[i].Y))
                        {
                            box[i].PastX = box[i].X;
                            box[i].PastY = box[i].Y;
                            box[i].Y = player.Y + 1;
                            // game.GrabedBoxId = i;
                        }
                        if (Direction.Down == player.MoveDirection && Game.Function.IsCollided(player.X, player.Y - 2, box[i].X, box[i].Y) && Game.Function.IsCollided(player.PastX, player.PastY - 1, box[i].X, box[i].Y))
                        {
                            box[i].PastX = box[i].X;
                            box[i].PastY = box[i].Y;
                            box[i].Y = player.Y - 1;
                            // game.GrabedBoxId = i;
                        }
                    }
                }
            }




        }
    }
}