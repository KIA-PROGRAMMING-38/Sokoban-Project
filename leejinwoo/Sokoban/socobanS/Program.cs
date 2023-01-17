using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

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
                new Box{X = 4 , Y = 8 },
                new Box{X = 4 , Y = 4 },
                new Box{X = 10 , Y = 5 },
                new Box{X = 12 , Y = 10 }
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

                MovePlayer(key, ref player.X, ref player.Y, ref playerMoveDirection);


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
                        if (IsCollided(box[boxId].X, box[boxId].Y, goal[goalId].X, goal[goalId].Y))
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
                        if (false == IsCollided(player.X, player.Y, wall[wallId].X, wall[wallId].Y))
                        {
                            continue;
                        }
                        OnCollision(() =>
                        {
                            PushOut(playerMoveDirection, ref player.X, ref player.Y, wall[wallId].X, wall[wallId].Y);
                        });
                    }


                    // 박스 이동 처리
                    for (int i = 0; i < BOX_COUNT; ++i)
                    {
                        if (false == IsCollided(player.X, player.Y, box[i].X, box[i].Y))
                        {
                            continue;
                        }

                        OnCollision(() =>
                        {
                            MoveBox(playerMoveDirection, ref box[i].X, ref box[i].Y, player.X, player.Y);
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

                        if (false == IsCollided(box[player.pushedBoxIndex].X, box[player.pushedBoxIndex].Y, box[i].X, box[i].Y))
                        {
                            continue;
                        }
                        OnCollision(() =>
                        {
                            PushOut(playerMoveDirection,
                            ref box[player.pushedBoxIndex].X, ref box[player.pushedBoxIndex].Y,
                            box[i].X, box[i].Y);

                            PushOut(playerMoveDirection,
                            ref player.X, ref player.Y,
                            box[player.pushedBoxIndex].X, box[player.pushedBoxIndex].Y);
                        });

                    }

                    // 박스와 벽의 충돌 처리
                    for (int i = 0; i < WALL_COUNT; ++i)
                    {
                        if (false == IsCollided(box[player.pushedBoxIndex].X, box[player.pushedBoxIndex].Y,
                                     wall[i].X, wall[i].Y))
                        {
                            continue;
                        }

                        OnCollision(() =>
                        {
                            PushOut(playerMoveDirection,
                            ref box[player.pushedBoxIndex].X, ref box[player.pushedBoxIndex].Y,
                            wall[i].X, wall[i].Y);

                            PushOut(playerMoveDirection,
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
                        string boxIcon = isBoxOnGoal[boxId] ? "O" : "B";
                        RenderObject(box[boxId].X, box[boxId].Y, boxIcon);
                    }
                    // 벽을 그린다.
                    for (int wallId = 0; wallId < WALL_COUNT; ++wallId)
                    {
                        RenderObject(wall[wallId].X, wall[wallId].Y, "W");
                    }
                }
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
                void MoveToLeftOfTarget(out int x, in int target) => x = Math.Max(MIN_X, target - 1);
                void MoveToRightOfTarget(out int x, in int target) => x = Math.Min(MAX_X, target + 1);
                void MoveToUpOfTarget(out int y, in int target) => y = Math.Max(MIN_Y, target - 1);
                void MoveToDownOfTarget(out int y, in int target) => y = Math.Min(MAX_Y, target + 1);

                void OnCollision(Action action)
                {
                    action();
                }
                void PushOut(Direction playerMoveDirection, ref int objX, ref int objY,
                    in int collidedObjX, in int collidedObjY)
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

                void MoveBox(Direction playerMoveDirection, ref int boxX, ref int boxY,
                    in int playerX, in int playerY)
                {
                    switch (playerMoveDirection)
                    {
                        case Direction.Left:
                            MoveToLeftOfTarget(out boxX, in playerX);

                            break;
                        case Direction.Right:
                            MoveToRightOfTarget(out boxX, in playerX);

                            break;
                        case Direction.Up:
                            MoveToUpOfTarget(out boxY, in playerY);

                            break;
                        case Direction.Down:
                            MoveToDownOfTarget(out boxY, in playerY);

                            break;
                        default: // Error
                            ExitWithError($"[Error] 플레이어 방향 : {playerMoveDirection}");

                            break;
                    }
                }


                void MovePlayer(ConsoleKey key, ref int x, ref int y, ref Direction moveDirection)
                {

                    if (key == ConsoleKey.A)
                    {
                        x = Math.Max(x - 2, MIN_X);
                        moveDirection = Direction.a;
                    }
                    if (key == ConsoleKey.S)
                    {
                        x = Math.Min(x + 2, MAX_X);
                        moveDirection = Direction.s;
                    }
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

                void ExitWithError(string errorMessage)
                {
                    Console.Clear();
                    Console.WriteLine(errorMessage);
                    Environment.Exit(1);
                }

            }//게임루프 중괄호
        }
    }
}