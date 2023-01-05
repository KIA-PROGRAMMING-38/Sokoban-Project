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
        const int MAP_MAX_X = 15;
        const int MAP_MIN_Y = 0;
        const int MAP_MAX_Y = 10;

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

        // wall---------------------------
        int[] wall_X = new int[4];
        wall_X[0] = 5;
        wall_X[1] = 6;
        wall_X[2] = 9;
        wall_X[3] = 10;
        int[] wall_Y = new int[4];
        wall_Y[0] = 3;
        wall_Y[1] = 3;
        wall_Y[2] = 7;
        wall_Y[3] = 7;
        const string WALL_STRING = "-";
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

            //wall 출력
            for (int i = 0; i < wall_X.Count(); i++)
            {
                Console.SetCursorPosition(wall_X[i], wall_Y[i]);
                Console.WriteLine(WALL_STRING);
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

            // player 이동
            if (key == ConsoleKey.LeftArrow)
            {
                player_X = Math.Max(MAP_MIN_X, player_X - 1);
                playerDirection = Direction.Left;
            }
            if (key == ConsoleKey.RightArrow)
            {
                player_X = Math.Min(MAP_MAX_X, player_X + 1);
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
                playerDirection = Direction.Down;
            }

            //box 이동
            for (int i = 0; i < box_X.Count(); i++)
            {
                if (player_X == box_X[i] && player_Y == box_Y[i])
                {
                    switch (playerDirection)
                    {
                        case Direction.Left:
                            if (box_X[i] == MAP_MIN_X)
                            {
                                player_X += 1;
                            }
                            else
                            {
                                box_X[i] -= 1;
                            }
                            break;

                        case Direction.Right:
                            if (box_X[i] == MAP_MAX_X)
                            {
                                player_X -= 1;
                            }
                            else
                            {
                                box_X[i] += 1;
                            }
                            break;

                        case Direction.Up:
                            if (box_Y[i] == MAP_MIN_Y)
                            {
                                player_Y += 1;
                            }
                            else
                            {
                                box_Y[i] -= 1;
                            }
                            break;

                        case Direction.Down:
                            if (box_Y[i] == MAP_MAX_Y)
                            {
                                player_Y -= 1;
                            }
                            else
                            {
                                box_Y[i] += 1;
                            }
                            break;


                    }
                }
            }
            // player가 벽에 막힐때
            for (int i = 0; i < wall_X.Count(); i++)
            {
                if (player_X == wall_X[i] && player_Y == wall_Y[i])
                {
                    switch (playerDirection)
                    {
                        case Direction.Left:
                            player_X += 1;
                            break;

                        case Direction.Right:
                                player_X -= 1;
                            break;

                        case Direction.Up:
                                player_Y += 1;
                            break;

                        case Direction.Down:
                                player_Y -= 1;
                            break;

                    }
                }
            }
            // 상자가 벽에서 막힐때
            for (int i = 0; i < wall_X.Count(); i++)
            {
                for (int j =0; j < box_X.Count(); j++)
                {
                    if (wall_X[i] == box_X[j] && wall_Y[i] == box_Y[j])
                    {
                        switch (playerDirection)
                        {
                            case Direction.Left:
                                {
                                    box_X[j] += 1;
                                    player_X += 1;
                                    break;
                                }
                            case Direction.Right:
                                {
                                    box_X[j] -= 1;
                                    player_X -= 1;
                                    break;
                                }
                            case Direction.Up:
                                {
                                    box_Y[j] += 1;
                                    player_Y += 1;
                                    break;
                                }
                            case Direction.Down:
                                {
                                    box_Y[j] -= 1;
                                    player_Y -= 1;
                                    break;
                                }
                        }
                       
                    }
                    
                }
            }
            // box가 골인 했을때
            // box가 골이랑 모두다 같아졌을때
            bool[] goalIn = new bool[3];

            for (int i = 0; i < goal_X.Count(); i++)
            {
                for (int j =0; j < box_X.Count(); j++)
                {
                    if (goal_X[i] == box_X[j] && goal_Y[i] == box_Y[j])
                    {
                        goalIn[i] = true;
                    }
                    
                }
                              
            }
            if (goalIn[0] == true && goalIn[1] == true && goalIn[2] == true)
            {
                Console.Clear();
                Console.WriteLine("축하!!");
                return;
            }
                
        }



    }
}

