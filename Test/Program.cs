namespace Test;

enum Direction
{
    Left = 1,
    Right = 2,
    Up = 3,
    Down = 4
}

class Program
{
    static void Main(string[] args)
    {
        // 초기 세팅
        Console.ResetColor();
        Console.CursorVisible = false;
        Console.Title = "Hui_Ji";
        Console.BackgroundColor = ConsoleColor.DarkCyan;
        Console.ForegroundColor = ConsoleColor.White;
        Console.Clear();

        /* Render 하기 위해서 필요한 객체들*/

        // Map 초기설정

        const int MAP_MIN_X = 0;
        const int MAP_MAX_X = 0;
        const int MAP_MIN_Y = 0;
        const int MAP_MAX_Y = 0;

        // player------------------------

        // player 좌표설정

        int player_X = 0;
        int player_Y = 0;
        const string PLAYER_STRING = "H";

        Direction playerDirection = Direction.Left;

        // box----------------------------

        // box 여러개 좌표설정

        int[] box_X = new int[3];
        box_X[0] = 3;
        box_X[1] = 5;
        box_X[2] = 9;
        int[] box_Y = new int[3];
        box_Y[0] = 1;
        box_Y[1] = 5;
        box_Y[2] = 9;

        const string BOX_STRING = "#";

        // goal---------------------------

        //goal 여러개 좌표설정

        int[] goal_X = new int[3];
        goal_X[0] = 9;
        goal_X[1] = 7;
        goal_X[2] = 13;
        int[] goal_Y = new int[3];
        goal_Y[0] = 1;
        goal_Y[1] = 7;
        goal_Y[2] = 10;

        const string GOAL_STRING = "0";


        // 게임루프

        while (true)
        {
            Console.Clear();
            // Render-----------------------------------------------------------

            //player 출력
            Console.SetCursorPosition(player_X, player_Y);
            Console.Write(PLAYER_STRING);

            //box 출력
            for (int i = 0; i < box_X.Count(); i++)
            {
                Console.SetCursorPosition(box_X[i], box_Y[i]);
                Console.Write(BOX_STRING);
            }

            //goal 출력
            for (int i = 0; i < goal_X.Count(); i++)
            {
                Console.SetCursorPosition(goal_X[i], goal_Y[i]);
                Console.Write(GOAL_STRING);
            }

            // prosess Input----------------------------------------------------

            ConsoleKey key = Console.ReadKey().Key;

            // update-----------------------------------------------------------

            if (key == ConsoleKey.LeftArrow)
            {
                player_X = Math.Max(MAP_MIN_X, player_X - 1);
                playerDirection = Direction.Left;
            }
            if (key == ConsoleKey.RightArrow)
            {
                player_X = Math.Min(MAP_MAX_X, player_Y + 1);
                playerDirection = Direction.Right;
            }
            if (key == ConsoleKey.UpArrow)
            {
                player_Y = Math.Max(MAP_MIN_Y, player_Y - 1);
                playerDirection = Direction.Up;
            }
            if (key == ConsoleKey.DownArrow)
            {
                player_Y = Math.Min(MAP_MAX_Y, player_Y + 1);
            }
                
        }



    }
}

