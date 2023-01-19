using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    internal enum Direction
    {
        None,
        Left,
        Right,
        Up,
        Down
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

    enum Mineral
    {
        None,
        Ruby,
        Gold,
        Emerald,
        Sapphire,
        Aquamarine,
        Amethyst
    }
    internal class Game
    {
        public int PushedBoxId;
        // public int GrabedBoxId;
        public int GainMineralId;
        public int ActivatedTrapId;
        public PortalNum PortalId;
        public int Money;
        public int BoxOnGoalCount;
        public int HowMuchOperation;

        // 기호 상수 정의
        public const int GOAL_COUNT = 4;
        public const int BOX_COUNT = GOAL_COUNT;
        public const int WALL_COUNT = 19;
        public const int WARP_COUNT = 1;
        public const int PORTAL_COUNT = 5;
        public const int TRAP_COUNT = 4;
        public const int MINERAL_COUNT = 7;

        public const int MIN_X = 5;
        public const int MIN_Y = 3;
        public const int MAX_X = 37;
        public const int MAX_Y = 23;
        public const int OUTLINE_LENGTH_X = 33;
        public const int OUTLINE_LENGTH_Y = 20;

        public const int MINE_MIN_X = 47;
        public const int MINE_MIN_Y = 16;
        public const int MINE_MAX_X = 87;
        public const int MINE_MAX_Y = 23;
        public const int MINE_OUTLINE_LENGTH_X = 41;
        public const int MINE_OUTLINE_LENGTH_Y = 7;


        public static class Function
        {
            // 오브젝트 그림
            public static void RenderObject(int x, int y, string obj, ConsoleColor color)
            {
                ConsoleColor temp = Console.ForegroundColor;
                Console.SetCursorPosition(x, y);
                Console.ForegroundColor = color;
                Console.Write(obj);
                Console.ForegroundColor = temp;
            }

            // 플레이어 이동
            public static void MovePlayer(ConsoleKey key, Player player, Game game)
            {
                player.MoveDirection = Direction.None;

                if (key == ConsoleKey.LeftArrow)
                {
                    player.PastX = player.X;
                    player.PastY = player.Y;
                    if (true == player.OnMain)
                    {
                        MoveToLeftOfTargetInMainStage(out player.X, out player.Y, in player.X, in player.Y);
                    }
                    else if (true == player.OnMine)
                    {
                        MoveToLeftOfTargetInMineStage(out player.X, out player.Y, in player.X, in player.Y);
                    }
                    player.MoveDirection = Direction.Left;
                }

                if (key == ConsoleKey.RightArrow)
                {
                    player.PastX = player.X;
                    player.PastY = player.Y;
                    if (true == player.OnMain)
                    {
                        MoveToRightOfTargetInMainStage(out player.X, out player.Y, in player.X, in player.Y);
                    }
                    else if (true == player.OnMine)
                    {
                        MoveToRightOfTargetInMineStage(out player.X, out player.Y, in player.X, in player.Y);
                    }
                    player.MoveDirection = Direction.Right;
                }

                if (key == ConsoleKey.UpArrow)
                {
                    player.PastX = player.X;
                    player.PastY = player.Y;
                    if (true == player.OnMain)
                    {
                        MoveToUpOfTargetInMainStage(out player.X, out player.Y, in player.X, in player.Y);
                    }
                    else if (true == player.OnMine)
                    {
                        MoveToUpOfTargetInMineStage(out player.X, out player.Y, in player.X, in player.Y);
                    }
                    player.MoveDirection = Direction.Up;
                }

                if (key == ConsoleKey.DownArrow)
                {
                    player.PastX = player.X;
                    player.PastY = player.Y;
                    if (true == player.OnMain)
                    {
                        MoveToDownOfTargetInMainStage(out player.X, out player.Y, in player.X, in player.Y);
                    }
                    else if (true == player.OnMine)
                    {
                        MoveToDownOfTargetInMineStage(out player.X, out player.Y, in player.X, in player.Y);
                    }
                    player.MoveDirection = Direction.Down;
                }

                game.HowMuchOperation += 1;
            }
            // 그랩
            public static void ActPlayer(ConsoleKey key, ref Grab action)
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
            // 포탈 이동
            public static void PortalPlayer(ConsoleKey key, Player player, Portal[] portal, Game game)
            {
                if (key == ConsoleKey.D1)
                {
                    player.X = portal[(int)PortalNum.One].X;
                    player.Y = portal[(int)PortalNum.One].Y;
                    game.Money -= 100;
                }
                if (key == ConsoleKey.D2)
                {
                    player.X = portal[(int)PortalNum.Two].X;
                    player.Y = portal[(int)PortalNum.Two].Y;
                    game.Money -= 100;
                }
                if (key == ConsoleKey.D3)
                {
                    player.X = portal[(int)PortalNum.Three].X;
                    player.Y = portal[(int)PortalNum.Three].Y;
                    game.Money -= 100;
                }
                if (key == ConsoleKey.D4)
                {
                    player.X = portal[(int)PortalNum.Four].X;
                    player.Y = portal[(int)PortalNum.Four].Y;
                    game.Money -= 100;
                }
            }
            // 터널 이동
            public static void TunnelPlayer(ConsoleKey key, Player player, Mine.Tunnel mineTunnel, Game game)
            {
                if (key == ConsoleKey.M)
                {
                    if (true == IsCollided(player.X, player.Y, mineTunnel.InMainX, mineTunnel.InMainY))
                    {
                        player.X = mineTunnel.InMineX;
                        player.Y = mineTunnel.InMineY;
                        player.OnMain = false;
                        player.OnMine = true;
                    }
                    else if (true == IsCollided(player.X, player.Y, mineTunnel.InMineX, mineTunnel.InMineY))
                    {
                        player.X = mineTunnel.InMainX;
                        player.Y = mineTunnel.InMainY;
                        player.OnMain = true;
                        player.OnMine = false;
                        game.Money -= 300;
                    }
                }

            }

            // 광질
            public static void Mining(int mineralWeight, int mineralValue, int index, ref int money, ref int gainMineralId)
            {
                Random random = new Random();

                int selectedNumber = random.Next(1, 1091);
                gainMineralId = 0;
                if (selectedNumber <= mineralWeight)
                {
                    money += mineralValue;
                    gainMineralId = index;
                }
            }

            // 충돌 판별
            public static bool IsCollided(in int objX, in int objY, in int targetX, in int targetY)
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

            // 충돌 처리
            public static void OnCollision(Action action)
            {
                action.Invoke();
            }
            // 충돌시 이동
            public static void MoveObj(Direction playerMoveDirection,
                ref int objX, ref int objY,
                in int targetX, in int targetY)
            {
                switch (playerMoveDirection)
                {
                    case Direction.Left:
                        MoveToLeftOfTargetInMainStage(out objX, out objY, in targetX, in targetY);
                        break;
                    case Direction.Right:
                        MoveToRightOfTargetInMainStage(out objX, out objY, in targetX, in targetY);
                        break;
                    case Direction.Up:
                        MoveToUpOfTargetInMainStage(out objX, out objY, in targetX, in targetY);
                        break;
                    case Direction.Down:
                        MoveToDownOfTargetInMainStage(out objX, out objY, in targetX, in targetY);
                        break;
                    default:
                        ExitWithError($"[Error] 플레이어 이동 방향 데이터가 오류입니다. : {playerMoveDirection}");
                        return;
                }
            }
            // 충돌시 정지
            public static void PushOutInMain(Direction playerMoveDirection,
                ref int objX, ref int objY,
                in int collidedObjX, in int collidedObjY)
            {
                switch (playerMoveDirection)
                {
                    case Direction.Left:
                        MoveToRightOfTargetInMainStage(out objX, out objY, in collidedObjX, in collidedObjY);
                        break;
                    case Direction.Right:
                        MoveToLeftOfTargetInMainStage(out objX, out objY, in collidedObjX, in collidedObjY);
                        break;
                    case Direction.Up:
                        MoveToDownOfTargetInMainStage(out objX, out objY, in collidedObjX, in collidedObjY);
                        break;
                    case Direction.Down:
                        MoveToUpOfTargetInMainStage(out objX, out objY, in collidedObjX, in collidedObjY);
                        break;
                    default:
                        ExitWithError($"[Error] 플레이어 이동 방향 데이터가 오류입니다. : {playerMoveDirection}");
                        return;
                }
            }

            public static void PushOutInMine(Direction playerMoveDirection,
                ref int objX, ref int objY,
                in int collidedObjX, in int collidedObjY)
            {
                switch (playerMoveDirection)
                {
                    case Direction.Left:
                        MoveToRightOfTargetInMineStage(out objX, out objY, in collidedObjX, in collidedObjY);
                        break;
                    case Direction.Right:
                        MoveToLeftOfTargetInMineStage(out objX, out objY, in collidedObjX, in collidedObjY);
                        break;
                    case Direction.Up:
                        MoveToDownOfTargetInMineStage(out objX, out objY, in collidedObjX, in collidedObjY);
                        break;
                    case Direction.Down:
                        MoveToUpOfTargetInMineStage(out objX, out objY, in collidedObjX, in collidedObjY);
                        break;
                    default:
                        ExitWithError($"[Error] 플레이어 이동 방향 데이터가 오류입니다. : {playerMoveDirection}");
                        return;
                }
            }
            // 트랩 구현
            public static void ObjOnTrap(Trap[] trap, Player player, Box[] box, Game game)
            {
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
                    Environment.Exit(2);
                }
            }

            // 골인 구현
            public static void BoxInGoal(Box[] box, Goal[] goal, Game game)
            {

                game.BoxOnGoalCount = 0;

                // 골인
                for (int boxId = 0; boxId < Game.BOX_COUNT; ++boxId)
                {

                    box[boxId].IsOnGoal = false;

                    for (int goalId = 0; goalId < Game.GOAL_COUNT; ++goalId)
                    {
                        if (Game.Function.IsCollided(box[boxId].X, box[boxId].Y, goal[goalId].X, goal[goalId].Y))
                        {
                            ++game.BoxOnGoalCount;

                            box[boxId].IsOnGoal = true;

                            break;
                        }
                    }
                }
            }
            // 게임 클리어
            public static void GameClear()
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("축하합니다. 클리어 하셨습니다.");
                Environment.Exit(0);
            }

            // 에러 메시지 출력 후 종료
            public static void ExitWithError(string errorMessage)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(errorMessage);
                Environment.Exit(1);
            }

            // 메인 스테이지 에서 타겟 근처로 이동
            public static void MoveToLeftOfTargetInMainStage(out int objX, out int objY, in int targetX, in int targetY)
            {
                objX = Math.Max(MIN_X, targetX - 1);
                objY = targetY;
            }
            public static void MoveToRightOfTargetInMainStage(out int objX, out int objY, in int targetX, in int targetY)
            {
                objX = Math.Min(targetX + 1, MAX_X);
                objY = targetY;
            }
            public static void MoveToUpOfTargetInMainStage(out int objX, out int objY, in int targetX, in int targetY)
            {
                objX = targetX;
                objY = Math.Max(MIN_Y, targetY - 1);
            }
            public static void MoveToDownOfTargetInMainStage(out int objX, out int objY, in int targetX, in int targetY)
            {
                objX = targetX;
                objY = Math.Min(targetY + 1, MAX_Y);
            }

            // 마인 스테이지 에서
            public static void MoveToLeftOfTargetInMineStage(out int objX, out int objY, in int targetX, in int targetY)
            {
                objX = Math.Max(MINE_MIN_X, targetX - 1);
                objY = targetY;
            }
            public static void MoveToRightOfTargetInMineStage(out int objX, out int objY, in int targetX, in int targetY)
            {
                objX = Math.Min(targetX + 1, MINE_MAX_X);
                objY = targetY;
            }
            public static void MoveToUpOfTargetInMineStage(out int objX, out int objY, in int targetX, in int targetY)
            {
                objX = targetX;
                objY = Math.Max(MINE_MIN_Y, targetY - 1);
            }
            public static void MoveToDownOfTargetInMineStage(out int objX, out int objY, in int targetX, in int targetY)
            {
                objX = targetX;
                objY = Math.Min(targetY + 1, MINE_MAX_Y);
            }
        }
    }
}
