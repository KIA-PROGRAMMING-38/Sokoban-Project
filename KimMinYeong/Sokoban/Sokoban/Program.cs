using System;
using System.Xml.XPath;

public class Program
{
    enum Direction
    {
        None,
        Left,
        Right,
        Up,
        Down
    }

    static void Main()
    {
        // a와 b 중 최댓값을 구하는 함수
        int Max(int a, int b)
        {
            int result = (a > b) ? a : b;
            return result;
        }

        // a와 b 중 최솟값을 구하는 함수
        int Min(int a, int b)
        {
            int result = a < b ? a : b;
            return result;
        }

        #region 초기 설정
        Console.ResetColor();
        Console.Title = "Let's Make Sokoban";
        Console.CursorVisible = false;
        Console.BackgroundColor = ConsoleColor.DarkGreen;
        Console.ForegroundColor = ConsoleColor.Yellow;

        Console.Clear();
        #endregion

        #region 맵 정보
        const int MAX_OF_MAP_X = 20;
        const int MIN_OF_MAP_X = 0;
        const int MAX_OF_MAP_Y = 10;
        const int MIN_OF_MAP_Y = 0;
        #endregion

        #region 플레이어 정보
        const string PLAYER_STRING = "P";
        const int PLAYER_INITIAL_POSITION_X = 0;
        const int PLAYER_INITIAL_POSITION_Y = 0;
        int playerX = PLAYER_INITIAL_POSITION_X;
        int playerY = PLAYER_INITIAL_POSITION_Y;
        Direction playerDirection = Direction.None;
        #endregion

        #region 박스 정보
        const string BOX_STRING = "B";
        int[] boxPositionX = { 17, 7, 9 };
        int[] boxPositionY = { 9, 1, 3 };
        int numberOfBoxes = boxPositionX.Length;
        int pushedBoxId = 0;
        #endregion

        #region 벽 정보
        const string WALL_STRING = "W";
        int[] wallPositionX = { 9, 17, 3 };
        int[] wallPositionY = { 8, 1, 9 };
        int numberOfWalls = wallPositionX.Length;
        #endregion

        #region 골 정보
        const string GOAL_STRING = "G";
        int[] goalPositionX = { 20, 8, 1 };
        int[] goalPositionY = { 7, 1, 10 };
        int numberOfGoals = goalPositionX.Length;
        bool[] isBoxOnGoal = new bool[numberOfGoals];
        #endregion

        while (true)
        {
            Console.Clear();

            #region Render
            // ----------------------------- Render ---------------------------------
            // 플레이어 출력
            Console.SetCursorPosition(playerX, playerY);
            Console.Write(PLAYER_STRING);

            // 박스 출력
            for (int i = 0; i < numberOfBoxes; ++i)
            {
                Console.SetCursorPosition(boxPositionX[i], boxPositionY[i]);
                Console.Write(BOX_STRING);
            }

            // 벽 출력
            for (int i = 0; i < numberOfWalls; ++i)
            {
                Console.SetCursorPosition(wallPositionX[i], wallPositionY[i]);
                Console.Write(WALL_STRING);
            }

            // 골 출력
            for (int i = 0; i < numberOfGoals; ++i)
            {
                Console.SetCursorPosition(goalPositionX[i], goalPositionY[i]);
                if (isBoxOnGoal[i])
                {
                    Console.Write("H");
                }
                else
                {
                    Console.Write(GOAL_STRING);
                }
            }
            #endregion

            // ------------------------- Process Input ----------------------------
            ConsoleKey key = Console.ReadKey().Key;

            // ----------------------------- update -------------------------------
            #region 플레이어 이동
            // 플레이어 이동
            if (key == ConsoleKey.LeftArrow)
            {
                playerX = Max(MIN_OF_MAP_X, playerX - 1);
                playerDirection = Direction.Left;
            }

            if (key == ConsoleKey.RightArrow)
            {
                playerX = Min(playerX + 1, MAX_OF_MAP_X);
                playerDirection = Direction.Right;
            }

            if (key == ConsoleKey.UpArrow)
            {
                playerY = Max(playerY - 1, MIN_OF_MAP_Y);
                playerDirection = Direction.Up;
            }

            if (key == ConsoleKey.DownArrow)
            {
                playerY = Min(playerY + 1, MAX_OF_MAP_Y);
                playerDirection = Direction.Down;
            }

            // 플레이어가 벽에 가로막힐 때
            for (int wallId = 0; wallId < numberOfWalls; ++wallId)
            {
                if (playerX == wallPositionX[wallId] && playerY == wallPositionY[wallId])
                {
                    switch (playerDirection)
                    {
                        case Direction.Left:
                            playerX = wallPositionX[wallId] + 1;
                            break;

                        case Direction.Right:
                            playerX = wallPositionX[wallId] - 1;
                            break;

                        case Direction.Up:
                            playerY = wallPositionY[wallId] + 1;
                            break;

                        case Direction.Down:
                            playerY = wallPositionY[wallId] - 1;
                            break;

                        default:
                            Console.Clear();
                            Console.WriteLine($"플레이어 이동방향이 잘못됐습니다. {playerDirection}");
                            return;
                    }
                }
            }
            #endregion

            #region 박스 이동
            // 박스 이동 (플레이어가 밀기)
            for (int boxId = 0; boxId < numberOfBoxes; ++boxId)
            {
                if (playerX == boxPositionX[boxId] && playerY == boxPositionY[boxId])
                {
                    switch (playerDirection)
                    {
                        case Direction.Left:
                            boxPositionX[boxId] = Math.Max(boxPositionX[boxId] - 1, MIN_OF_MAP_X);
                            playerX = boxPositionX[boxId] + 1;
                            break;

                        case Direction.Right:
                            boxPositionX[boxId] = Math.Min(boxPositionX[boxId] + 1, MAX_OF_MAP_X);
                            playerX = boxPositionX[boxId] - 1;
                            break;

                        case Direction.Up:
                            boxPositionY[boxId] = Math.Max(boxPositionY[boxId] - 1, MIN_OF_MAP_Y);
                            playerY = boxPositionY[boxId] + 1;
                            break;

                        case Direction.Down:
                            boxPositionY[boxId] = Math.Min(boxPositionY[boxId] + 1, MAX_OF_MAP_Y);
                            playerY = boxPositionY[boxId] - 1;
                            break;

                        default:
                            Console.Clear();
                            Console.WriteLine($"잘못된 이동 방향입니다. {playerDirection}");
                            return;
                    }
                    pushedBoxId = boxId;
                    break;
                }
            }

            // 박스가 벽에 막혔을 때
            for (int wallId = 0; wallId < numberOfWalls; ++wallId)
            {
                if (boxPositionX[pushedBoxId] == wallPositionX[wallId] && boxPositionY[pushedBoxId] == wallPositionY[wallId])
                {
                    switch (playerDirection)
                    {
                        case Direction.Left:
                            boxPositionX[pushedBoxId] = wallPositionX[wallId] + 1;
                            playerX = boxPositionX[pushedBoxId] + 1;
                            break;

                        case Direction.Right:
                            boxPositionX[pushedBoxId] = wallPositionX[wallId] - 1;
                            playerX = boxPositionX[pushedBoxId] - 1;
                            break;

                        case Direction.Up:
                            boxPositionY[pushedBoxId] = wallPositionY[wallId] + 1;
                            playerY = boxPositionY[pushedBoxId] + 1;
                            break;

                        case Direction.Down:
                            boxPositionY[pushedBoxId] = wallPositionY[wallId] - 1;
                            playerY = boxPositionY[pushedBoxId] - 1;
                            break;
                    }
                    break;
                }
            }

            // 박스끼리 충돌
            for (int collidedBoxId = 0; collidedBoxId < numberOfBoxes; ++collidedBoxId)
            {
                if (pushedBoxId == collidedBoxId)
                {
                    continue;
                }

                if (boxPositionX[pushedBoxId] == boxPositionX[collidedBoxId] && boxPositionY[pushedBoxId] == boxPositionY[collidedBoxId])
                {
                    switch (playerDirection)
                    {
                        case Direction.Left:
                            boxPositionX[pushedBoxId] = boxPositionX[collidedBoxId] + 1;
                            playerX = boxPositionX[pushedBoxId] + 1;
                            break;

                        case Direction.Right:
                            boxPositionX[pushedBoxId] = boxPositionX[collidedBoxId] - 1;
                            playerX = boxPositionX[pushedBoxId] - 1;
                            break;

                        case Direction.Up:
                            boxPositionY[pushedBoxId] = boxPositionY[collidedBoxId] + 1;
                            playerY = boxPositionY[pushedBoxId] + 1;
                            break;

                        case Direction.Down:
                            boxPositionY[pushedBoxId] = boxPositionY[collidedBoxId] - 1;
                            playerY = boxPositionY[pushedBoxId] - 1;
                            break;
                    }
                    break;
                }
            }
            #endregion

            #region 골인 판정
            // 골을 카운트 해야함 -> 골인한 수와 골 수 혹은 박스 수가 같으면 게임 끝내야 함
            // 각 골인 지점이 박스가 골인한 상태인지 확인하여 다른 문자로 출력해야 함 -> 골인 지점마다 여부를 저장할 bool[] 필요
            int goalCount = 0;

            for (int goalId = 0; goalId < numberOfGoals; ++goalId)
            {
                isBoxOnGoal[goalId] = false;

                for (int boxId = 0; boxId < numberOfBoxes; ++boxId)
                {
                    if (goalPositionX[goalId] == boxPositionX[boxId] && goalPositionY[goalId] == boxPositionY[boxId])
                    {
                        isBoxOnGoal[goalId] = true;
                        ++goalCount;
                        break;
                    }
                }
            }

            if (goalCount == numberOfGoals)
            {
                Console.Clear();
                Console.WriteLine("게임 클리어! 축하합니다!");
                return;
            }
            #endregion
        }
    }
}