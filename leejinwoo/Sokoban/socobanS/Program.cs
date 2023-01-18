using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace socobanS
{
    enum Direction // 방향을 저장하는 타입
    {
        None,
        Left,
        Right,
        Up,
        Down,
        Space,
        a,
        s
    }


    class Sokoban
    {
        static void Main()
        {

            // 초기 세팅
            Console.ResetColor(); // 컬러를 초기화 하는 것
            Console.CursorVisible = false; // 커서를 숨기기
            Console.Title = "진우의 소코반"; // 타이틀을 설정한다.
            Console.BackgroundColor = ConsoleColor.Cyan; // 배경색을 설정한다.
            Console.ForegroundColor = ConsoleColor.Black; // 글꼴색을 설정한다.
            Console.Clear(); // 출력된 내용을 지운다.

            // 기호 상수 정의

            const int GOAL_COUNT = 4;
            const int BOX_COUNT = GOAL_COUNT;
            const int WALL_COUNT = 17;

            const int MIN_X = 1;
            const int MIN_Y = 1;
            const int MAX_X = 20;
            const int MAX_Y = 15;

            // 맵 주변 벽 만들기
            const int MAP_MIN_X = 0;
            const int MAP_MIN_Y = 0;
            const int MAP_MAX_X = 21;
            const int MAP_MAX_Y = 16;

            Random random = new Random();

            



            //int playerX = 2;
            //int playerY = 2;

            Player player = new Player
            {
                X = 1,
                Y = 1,
                MoveDirection = Direction.None,
                pushedBoxIndex = 0
            };




            Direction playerMoveDirection = Direction.None;




            Box[] box = new Box[]
            {
                new Box{X = 4 , Y = 8 , IsOnGoal =false },
                new Box{X = 4 , Y = 4 , IsOnGoal =false },
                new Box{X = 10 , Y = 5 , IsOnGoal =false },
                new Box{X = 12 , Y = 10 , IsOnGoal =false }
            };
            // 박스가 골 위에 있는지를 저장하기 위한 변수
            bool[] isBoxOnGoal = new bool[box.Length];

            Wall[] wall = new Wall[]
            {
                new Wall { X = 7, Y = 1 },
                new Wall { X = 7, Y = 2 },
                new Wall { X = 7, Y = 3 },
                new Wall { X = 7, Y = 4 },
                new Wall { X = 7, Y = 5 },
                new Wall { X = 7, Y = 6 },
                new Wall { X = 7, Y = 7 },
                new Wall { X = 7, Y = 8 },
                new Wall { X = 7, Y = 9 },
                new Wall { X = 7, Y = 10 },
                new Wall { X = 7, Y = 11 },
                new Wall { X = 6, Y = 11 },
                new Wall { X = 5, Y = 11 },
                new Wall { X = 4, Y = 11 },
                new Wall { X = 3, Y = 11 },
                new Wall { X = 2, Y = 11 },
                new Wall { X = 1, Y = 11 }
            };


            Goal[] goal = new Goal[]
            {
                new Goal {X = 10, Y = 3},
                new Goal {X = 10, Y = 6},
                new Goal {X = 2, Y = 3},
                new Goal {X = 4, Y = 10}
            };




            

            // 게임 루프 구성
            while (true)
            {
                Action<int, int, string> renderObject = RenderObject;


                // ----------------------------랜더 -------------------------

                Render();

                int goalCount = goal.Length;
                // -------------------------사용자 입력 ---------------------

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                ConsoleKey key = keyInfo.Key;
                // ---------------------------업데이트-----------------------
                if (key == ConsoleKey.A || key == ConsoleKey.S
                    || key == ConsoleKey.LeftArrow || key == ConsoleKey.RightArrow
                    || key == ConsoleKey.UpArrow || key == ConsoleKey.DownArrow)
                {
                    int fake = random.Next(0, 100);

                    if (5 > fake)
                    {
                        player.X = 2;
                        player.Y = 1;

                        Console.SetCursorPosition(30, 8);
                        Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@메롱@@@@@@@@@@@@@@@@@@@@@@");
                        Console.SetCursorPosition(30, 9);
                        Console.WriteLine("@@@@@@@@@@@@@@@@@@@다시돌아가셈@@@@@@@@@@@@@@@@@@");
                        Thread.Sleep(700);
                    }
                }
                Move.MovePlayer(key, player);
                


                Update(key);

                // 박스와 골의 처리
                int boxOnGoalCount = 0;

                // 골 지점에 박스에 존재하냐?
                for (int boxId = 0; boxId < BOX_COUNT; ++boxId) // 모든 골 지점에 대해서
                {
                    // 현재 박스가 골 위에 올라와 있는지 체크한다.
                    isBoxOnGoal[boxId] = false; // 없을 가능성이 높기 때문에 false로 초기화 한다.

                    for (int goalId = 0; goalId < GOAL_COUNT; ++goalId) // 모든 박스에 대해서
                    {
                        // 박스가 골 지점 위에 있는지 확인한다.
                        if (Collision.IsCollided(box[boxId].X, box[boxId].Y, goal[goalId].X, goal[goalId].Y))
                        {
                            ++boxOnGoalCount;

                            isBoxOnGoal[boxId] = true; // 박스가 골 위에 있다는 사실을 저장해둔다.

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

                // 프레임을 그립니다.
                void Render()
                {
                    // 이전 프레임을 지운다.
                    Console.Clear();

                    //맵을 그린다.
                    for (int mapLineX = 0; mapLineX < MAP_MAX_X + 1; ++mapLineX)
                    {
                        Console.SetCursorPosition(mapLineX, MAP_MAX_Y);
                        Console.Write("-");
                        Console.SetCursorPosition(mapLineX, MAP_MIN_Y);
                        Console.Write("-");
                    }

                    for (int mapLineY = 1; mapLineY < MAP_MAX_Y; ++mapLineY)
                    {
                        Console.SetCursorPosition(MAP_MIN_X, mapLineY);
                        Console.Write("|");
                        Console.SetCursorPosition(MAP_MAX_X, mapLineY);
                        Console.Write("|");
                    }

                    // 플레이어를 그린다.
                    RenderObject(player.X, player.Y, "P");
                   
                    objectDraw();
                    // 골을 그린다.


                }

               
                // 업데이트
                void Update(ConsoleKey key)
                {
                    

                    // 플레이어와 벽의 충돌 처리
                    for (int wallId = 0; wallId < WALL_COUNT; ++wallId)
                    {
                        if (false == Collision.IsCollided(player.X, player.Y, wall[wallId].X, wall[wallId].Y))
                        {
                            continue;
                        }
                        Collision.OnCollision(() =>
                        {
                            Collision.PushOut(player.MoveDirection, ref player.X, ref player.Y, wall[wallId].X, wall[wallId].Y);
                        });
                    }

                    
                    // 박스 이동 처리
                    for (int i = 0; i < BOX_COUNT; ++i)
                    {
                        if (false == Collision.IsCollided(player.X, player.Y, box[i].X, box[i].Y))
                        {
                            continue;
                        }

                        Collision.OnCollision(() =>
                        {
                            Move.MoveBox(player, box[i]);
                        });

                        player.pushedBoxIndex = i;

                        break;
                    }


                    // 박스끼리 충돌 처리
                    for (int i = 0; i < BOX_COUNT; ++i)
                    {
                        // 같은 박스라면 처리할 필요가 X
                        if (player.pushedBoxIndex == i)
                        {
                            continue;
                        }

                        if (false == Collision.IsCollided(box[player.pushedBoxIndex].X, box[player.pushedBoxIndex].Y, box[i].X, box[i].Y))
                        {
                            continue;
                        }
                        Collision.OnCollision(() =>
                        {
                            Collision.PushOut(player.MoveDirection,
                            ref box[player.pushedBoxIndex].X, ref box[player.pushedBoxIndex].Y,
                            box[i].X, box[i].Y);

                            Collision.PushOut(player.MoveDirection,
                            ref player.X, ref player.Y,
                            box[player.pushedBoxIndex].X, box[player.pushedBoxIndex].Y);
                        });

                    }

                    // 박스와 벽의 충돌 처리
                    for (int i = 0; i < WALL_COUNT; ++i)
                    {
                        if (false == Collision.IsCollided(box[player.pushedBoxIndex].X, box[player.pushedBoxIndex].Y,
                                     wall[i].X, wall[i].Y))
                        {
                            continue;
                        }

                        Collision.OnCollision(() =>
                        {
                            Collision.PushOut(player.MoveDirection,
                            ref box[player.pushedBoxIndex].X, ref box[player.pushedBoxIndex].Y,
                            wall[i].X, wall[i].Y);

                            Collision.PushOut(player.MoveDirection,
                            ref player.X, ref player.Y,
                            box[player.pushedBoxIndex].X, box[player.pushedBoxIndex].Y);
                        });

                        break;
                    }
                    
                    
                }

               

                // 오브젝트를 그립니다.
                void RenderObject(int x, int y, string obj)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(obj);
                }
                void objectDraw()
                {
                    for (int i = 0; i < GOAL_COUNT; ++i)
                    {
                        RenderObject(goal[i].X, goal[i].Y, "G");
                    }
                    // 박스를 그린다.
                    for (int boxId = 0; boxId < BOX_COUNT; ++boxId)
                    {
                        string boxIcon = box[boxId].IsOnGoal ? "O" : "B";
                        RenderObject(box[boxId].X, box[boxId].Y, boxIcon);
                    }
                    // 벽을 그린다.
                    for (int wallId = 0; wallId < WALL_COUNT; ++wallId)
                    {
                        RenderObject(wall[wallId].X, wall[wallId].Y, "W");
                    }

                    
                }
                int CountBoxOnGoal(Box[]box, Goal[] goal)
                {
                    int boxCount = box.Length;
                    int goalCount = goal.Length;

                    int result = 0;

                    for (int boxId = 0; boxId < boxCount; ++boxId)
                    {
                        box[boxId].IsOnGoal = false;
                        for (int goalId = 0; goalId < goalCount; ++goalId)
                        {
                            if (Collision.IsCollided(box[boxId].X, box[boxId].Y,
                                goal[goalId].X, goal[goalId].Y))
                            {
                                ++result;
                                box[boxId].IsOnGoal = true;
                                break;
                            }
                        }

                    }
                    return result;
                }
                // 두 물체가 충돌했는지 판별합니다.
              
                

                

               

                void ExitWithError(string errorMessage)
                {
                    Console.Clear();
                    Console.WriteLine(errorMessage);
                    Environment.Exit(1);
                }
                int boxOngoalCount = CountBoxOnGoal(box, goal);
            }//게임루프 중괄호
        }
    }
}