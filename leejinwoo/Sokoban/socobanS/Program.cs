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
        Down
    }


     class Sokoban
    {
        static void Main()
        {

            // 초기 세팅
            Console.ResetColor(); // 컬러를 초기화 하는 것
            Console.CursorVisible = false; // 커서를 숨기기
            Console.Title = "홍성재의 파이어펀치"; // 타이틀을 설정한다.
            Console.BackgroundColor = ConsoleColor.DarkGreen; // 배경색을 설정한다.
            Console.ForegroundColor = ConsoleColor.Yellow; // 글꼴색을 설정한다.
            Console.Clear(); // 출력된 내용을 지운다.

            // 기호 상수 정의
            const int GOAL_COUNT = 3;
            const int BOX_COUNT = GOAL_COUNT;
            const int WALL_COUNT = 2;

            const int MIN_X = 0;
            const int MIN_Y = 0;
            const int MAX_X = 20;
            const int MAX_Y = 15;

            // 플레이어 위치를 저장하기 위한 변수
            // int playerX = 0;
            // int playerY = 0;
            // Direction playerMoveDirection = Direction.None;
            // int pushedBoxIndex = 0;

            Player player = new Player
            {
                X = 0,
                Y = 0,
                MoveDirection = Direction.None,
                PushedBoxIndex = 0
            };

            Box[] boxes = new Box[]
            {
                new Box { X = 5, Y = 5, IsOnGoal = false },
                new Box { X = 7, Y = 3, IsOnGoal = false },
                new Box { X = 4, Y = 4, IsOnGoal = false }
            };

            Wall[] walls = new Wall[]
            {
                 new Wall { X = 7, Y = 8},
                 new Wall { X = 7, Y = 5}
            };


            Goal[] goals = new Goal[]
            {
                new Goal { X = 9, Y = 9},
                new Goal { X = 1, Y = 2},
                new Goal { X = 3, Y = 3}
            };

            // 박스 위치를 저장하기 위한 변수
            //int[] boxPositionsX = { 5, 7, 4 };
            //int[] boxPositionsY = { 5, 3, 4 };
            // 박스가 골 위에 있는지를 저장하기 위한 변수
            bool[] isBoxOnGoal = new bool[BOX_COUNT];

            // 벽 위치를 저장하기 위한 변수
            //int[] wallPositionsX = { 7, 8 };
           // int[] wallPositionsY = { 7, 5 };

            // 골 위치를 저장하기 위한 변수
           // int[] goalPositionsX = { 9, 1, 3 };
           // int[] goalPositionsY = { 9, 2, 3 };



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
                    isBoxOnGoal[boxId] = false; // 없을 가능성이 높기 때문에 false로 초기화 한다.

                    for (int goalId = 0; goalId < GOAL_COUNT; ++goalId) // 모든 박스에 대해서
                    {
                        // 박스가 골 지점 위에 있는지 확인한다.
                        if (IsCollided(boxes[boxId].X, boxes[boxId].Y, goals[goalId].X, goals[goalId].Y))
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
            }


            // 프레임을 그립니다.
            void Render()
            {
                // 이전 프레임을 지운다.
                Console.Clear();

                // 플레이어를 그린다.
                RenderObject(player.X, player.Y, "P");

                // 골을 그린다.
                for (int i = 0; i < GOAL_COUNT; ++i)
                {
                    RenderObject(goals[i].X, goals[i].Y, "G");
                }

                // 박스를 그린다.
                for (int boxId = 0; boxId < BOX_COUNT; ++boxId)
                {
                    string boxShape = isBoxOnGoal[boxId] ? "O" : "B";
                    RenderObject(boxes[boxId].X, boxes[boxId].Y, boxShape);
                }

                // 벽을 그린다.
                for (int wallId = 0; wallId < WALL_COUNT; ++wallId)
                {
                    RenderObject(walls[wallId].X, walls[wallId].Y, "W");
                }
            }

            // 오브젝트를 그립니다.
            void RenderObject(int x, int y, string obj)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(obj);
            }

            void Update(ConsoleKey key)
            {
                MovePlayer(key, ref player);

                // 플레이어와 벽의 충돌 처리
                for (int wallId = 0; wallId < WALL_COUNT; ++wallId)
                {
                    if (false == IsCollided(player.X, player.Y, walls[wallId].X, walls[wallId].Y))
                    {
                        continue;
                    }

                    switch (player.MoveDirection)
                    {
                        case Direction.Left:
                            player.X = walls[wallId].X + 1;
                            break;
                        case Direction.Right:
                            player.X = walls[wallId].X - 1;
                            break;
                        case Direction.Up:
                            player.Y = walls[wallId].Y + 1;
                            break;
                        case Direction.Down:
                            player.Y = walls[wallId].Y - 1;
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine($"[Error] 플레이어 이동 방향 데이터가 오류입니다. : {player.MoveDirection}");

                            return;
                    }

                    break;
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
                            boxes[i].X = Math.Clamp(boxes[i].X - 1, MIN_X, MAX_X);
                            player.X = boxes[i].X + 1;
                            break;
                        case Direction.Right:
                            boxes[i].X = Math.Clamp(boxes[i].X + 1, MIN_X, MAX_X);
                            player.X = boxes[i].X - 1;
                            break;
                        case Direction.Up:
                            boxes[i].Y = Math.Clamp(boxes[i].Y - 1, MIN_Y, MAX_Y);
                            player.Y = boxes[i].Y + 1;
                            break;
                        case Direction.Down:
                            boxes[i].Y = Math.Clamp(boxes[i].Y + 1, MIN_Y, MAX_Y);
                            player.Y = boxes[i].Y - 1;
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine($"[Error] 플레이어 이동 방향 데이터가 오류입니다. : {player.MoveDirection}");

                            return;
                    }

                    player.PushedBoxIndex = i;

                    break;
                }
                
                // 박스와 벽의 충돌 처리
                for (int wallId = 0; wallId < WALL_COUNT; ++wallId)
                {
                    if (false == IsCollided(boxes[player.PushedBoxIndex].X, boxes[player.PushedBoxIndex].Y, walls[wallId].X, walls[wallId].Y))
                    {
                        continue;
                    }

                    switch (player.MoveDirection)
                    {
                        case Direction.Left:
                            boxes[player.PushedBoxIndex].X = walls[wallId].X + 1;
                            player.X = boxes[player.PushedBoxIndex].X + 1;
                            break;
                        case Direction.Right:
                            boxes[player.PushedBoxIndex].X = walls[wallId].X - 1;
                            player.X = boxes[player.PushedBoxIndex].X - 1;
                            break;
                        case Direction.Up:
                            boxes[player.PushedBoxIndex].Y = walls[wallId].Y + 1;
                            player.Y = boxes[player.PushedBoxIndex].Y + 1;
                            break;
                        case Direction.Down:
                            boxes[player.PushedBoxIndex].Y = walls[wallId].Y - 1;
                            player.Y = boxes[player.PushedBoxIndex].Y - 1;
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine($"[Error] 플레이어 이동 방향 데이터가 오류입니다. : {player.MoveDirection}");

                            return;
                    }

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

                    switch (player.MoveDirection)
                    {
                        case Direction.Left:
                            boxes[player.PushedBoxIndex].X = boxes[collidedBoxId].X + 1;
                            player.X = boxes[player.PushedBoxIndex].X + 1;

                            break;
                        case Direction.Right:
                            boxes[player.PushedBoxIndex].X = boxes[collidedBoxId].X - 1;
                            player.X = boxes[player.PushedBoxIndex].X - 1;

                            break;
                        case Direction.Up:
                            boxes[player.PushedBoxIndex].Y = boxes[collidedBoxId].Y + 1;
                            player.Y = boxes[player.PushedBoxIndex].Y + 1;

                            break;
                        case Direction.Down:
                            boxes[player.PushedBoxIndex].Y = boxes[collidedBoxId].Y - 1;
                            player.Y = boxes[player.PushedBoxIndex].Y - 1;

                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine($"[Error] 플레이어 이동 방향 데이터가 오류입니다. : {player.MoveDirection}");

                            return;
                    }

                    break;
                }
            }

            // 플레이어를 이동시킨다.
            void MovePlayer(ConsoleKey key, ref Player player)
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
        }


    }
}