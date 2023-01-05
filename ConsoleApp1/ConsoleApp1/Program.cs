


using System;

namespace prototype
{
    enum DIRECTION // DIRECTION 이라는 타입을 만들어줌
    {
        NONE = 0,
        RIGHT = 1,
        LEFT = 2,
        UP = 3,
        DOWN = 4
    }

    enum POSITION // POSITION 이라는 타입을 만들어줌
    {
        NONE = 0,
        RIGHT = 1,
        LEFT = 2,
        UP = 3,
        DOWN = 4
    }


    class program
    {
        static void Main()
        {
            #region 초기세팅

            Console.ResetColor(); // 컬러를 초기화
            Console.BackgroundColor = ConsoleColor.Red; // 배경 색
            Console.ForegroundColor = ConsoleColor.Yellow; // 글자 색
            Console.CursorVisible = false; // 커서를 숨김
            Console.Title = "복기용 프로토타입"; // 제목을 지어줌
            Console.Clear(); // 원래 밑에 뜨는 그 뭐 많은 것들 지워주는거임

            #endregion

            #region 플레이어 기호 및 좌표

            const int playX = 1;
            const int playY = 1;

            int player_X = playX;
            int player_Y = playY;
            string player = "P";

            #endregion

            #region 맵 , 프레임 기호 및 좌표

            const int Map_maxX = 30;
            const int Map_minX = 1;
            const int Map_maxY = 10;
            const int Map_minY = 1;

            string frame = "#";

            #endregion

            #region 박스 여러개 기호 및 좌표

            string box = "O";

            int box1_X = 2;
            int box1_Y = 2;



            int box2_X = 4;
            int box2_Y = 4;


            int box3_X = 6;
            int box3_Y = 6;

            int box4_X = 17;
            int box4_Y = 7;

            int box5_X = 15;
            int box5_Y = 8;


            int[] box_X = new int[] { box1_X, box2_X, box3_X, box4_X, box5_X };
            int[] box_Y = new int[] { box1_Y, box2_Y, box3_Y, box4_Y, box5_Y };



            #endregion

            #region 나무들 기호 및 좌표

            string tree = "|";

            const int tree1X = 3;
            const int tree1Y = 4;

            const int tree2X = 3;
            const int tree2Y = 5;

            const int tree3X = 3;
            const int tree3Y = 6;

            const int tree4X = 3;
            const int tree4Y = 7;

            const int tree5X = 7;
            const int tree5Y = 1;

            const int tree6X = 13;
            const int tree6Y = 1;

            const int tree7X = 13;
            const int tree7Y = 2;

            const int tree8X = 13;
            const int tree8Y = 3;



            int[] tree_X = new int[] { tree1X, tree2X, tree3X, tree4X, tree5X, tree6X, tree7X, tree8X };
            int[] tree_Y = new int[] { tree1Y, tree2Y, tree3Y, tree4Y, tree5Y, tree6Y, tree7Y, tree8Y };

            #endregion

            #region goal 기호 및 좌표

            string goal = "*";

            const int goal1X = 8;
            const int goal1Y = 1;

            const int goal2X = 14;
            const int goal2Y = 2;

            const int goal3X = 3;
            const int goal3Y = 8;

            int[] goal_X = new int[] { goal1X, goal2X, goal3X };
            int[] goal_Y = new int[] { goal1Y, goal2Y, goal3Y };

            #endregion

            #region DIRECTION 이라는 타입을 가진 객체를 초기화해줌
            DIRECTION direction = default; // DIRECTION(열거형) 타입의 direction 이라는 객체를 초기화해줬음
            #endregion

            #region POSITION 이라는 타입을 가진 객체배열을 만들고 초기화해줌
            POSITION[] position = new POSITION[box_X.Length];
            #endregion

            while (true)
            {
                Console.Clear();

                #region 렌더링
                // 렌더링

                #region goal 여러개 그려주기

                for (int i = 0; i < goal_X.Length; ++i)
                {
                    Console.SetCursorPosition(goal_X[i], goal_Y[i]);
                    Console.Write(goal);
                }

                #endregion

                #region 플레이어 그려주기
                Console.SetCursorPosition(player_X, player_Y);
                Console.Write(player);
                #endregion

                #region 프레임 그려주기
                for (int i = 1; i < Map_maxY + 1; ++i) // Y축 그려주기 = X는 고정이고 Y만 달라지는거임
                {
                    Console.SetCursorPosition(Map_minX - 1, i);
                    Console.Write(frame);
                    Console.SetCursorPosition(Map_maxX + 1, i);
                    Console.Write(frame);
                }

                for (int i = 1; i < Map_maxX + 3; ++i) // x축 그려주기 = y축은 고정이고 X만 달라지는거임
                {
                    Console.SetCursorPosition(i - 1, Map_minY - 1);
                    Console.Write(frame);
                    Console.SetCursorPosition(i - 1, Map_maxY + 1);
                    Console.Write(frame);
                }
                #endregion

                #region 박스 여러개 그려주기

                for (int i = 0; i < box_X.Length; ++i)
                {
                    Console.SetCursorPosition(box_X[i], box_Y[i]);
                    Console.Write(box);
                }


                #endregion

                #region 나무 여러개 그려주기
                for (int i = 0; i < tree_X.Length; ++i)
                {
                    Console.SetCursorPosition(tree_X[i], tree_Y[i]);
                    Console.Write(tree);
                }

                #endregion




                #endregion

                #region input
                // 인풋

                ConsoleKey key = Console.ReadKey().Key;


                #endregion


                #region 업데이트


                #region 플레이어 이동 구현

                if (key == ConsoleKey.RightArrow)
                {
                    player_X = Math.Min(player_X + 1, Map_maxX);
                    direction = DIRECTION.RIGHT;
                }

                if (key == ConsoleKey.LeftArrow)
                {
                    player_X = Math.Max(player_X - 1, Map_minX);
                    direction = DIRECTION.LEFT;
                }

                if (key == ConsoleKey.UpArrow)
                {
                    player_Y = Math.Max(player_Y - 1, Map_minY);
                    direction = DIRECTION.UP;
                }

                if (key == ConsoleKey.DownArrow)
                {
                    player_Y = Math.Min(player_Y + 1, Map_maxY);
                    direction = DIRECTION.DOWN;
                }

                #endregion

                #region 박스 당기는 거 구현하기


                for (int i = 0; i < box_X.Length; ++i)
                {
                    if (player_Y == box_Y[i] && player_X == box_X[i] + 1)
                    {
                        position[i] = POSITION.RIGHT;
                    }

                    else if (player_Y == box_Y[i] && player_X == box_X[i] - 1)
                    {
                        position[i] = POSITION.LEFT;
                    }

                    else if (player_X == box_X[i] && player_Y == box_Y[i] + 1)
                    {
                        position[i] = POSITION.DOWN;
                    }

                    else if (player_X == box_X[i] && player_Y == box_Y[i] - 1)
                    {
                        position[i] = POSITION.UP;
                    }
                }


                

                for (int i = 0; i < box_X.Length ;++i)
                {
                    if (key == ConsoleKey.Spacebar)
                    {
                        switch(position[i])
                        {
                            case POSITION.RIGHT:
                                player_X++;
                                box_X[i]++;
                                
                                break;

                            case POSITION.LEFT:
                                player_X--;
                                box_X[i]--;
                                
                                break;

                            case POSITION.UP:
                                player_Y--;
                                box_Y[i]--;
                                
                                break;

                            case POSITION.DOWN:
                                player_Y++;
                                box_Y[i]++;
                                
                                break;
                        }
                    }
                }

                for (int i = 0; i < position.Length ;++i)
                {
                    position[i] = POSITION.NONE;
                }

                #endregion

                #region 플레이어가 박스 여러개 미는 거 구현 + 박스도 벽에 막힘

                for (int i = 0; i < box_X.Length; ++i)
                {
                    if (player_X == box_X[i] && player_Y == box_Y[i])
                    {
                        switch (direction)
                        {
                            case DIRECTION.RIGHT:

                                box_X[i] = Math.Min(player_X + 1, Map_maxX);
                                player_X = box_X[i] - 1;
                                break;

                            case DIRECTION.LEFT:

                                box_X[i] = Math.Max(player_X - 1, Map_minX);
                                player_X = box_X[i] + 1;
                                break;

                            case DIRECTION.UP:

                                box_Y[i] = Math.Max(player_Y - 1, Map_minY);
                                player_Y = box_Y[i] + 1;
                                break;

                            case DIRECTION.DOWN:

                                box_Y[i] = Math.Min(player_Y + 1, Map_maxY);
                                player_Y = box_Y[i] - 1;
                                break;
                        }
                    }
                }







                #endregion

                #region 플레이어가 나무에 막히는 거 구현

                for (int i = 0; i < tree_X.Length; ++i)
                {
                    if (player_X == tree_X[i] && player_Y == tree_Y[i])
                    {
                        switch (direction)
                        {
                            case DIRECTION.LEFT:

                                player_X = tree_X[i] + 1;
                                break;

                            case DIRECTION.RIGHT:

                                player_X = tree_X[i] - 1;
                                break;

                            case DIRECTION.UP:

                                player_Y = tree_Y[i] + 1;
                                break;

                            case DIRECTION.DOWN:

                                player_Y = tree_Y[i] - 1;
                                break;

                        }

                    }
                }

                #endregion

                #region 박스가 나무에 막히는 거 구현
                for (int i = 0; i < tree_X.Length; ++i)
                {
                    for (int k = 0; k < box_X.Length; ++k)
                    {
                        if (box_X[k] == tree_X[i] && box_Y[k] == tree_Y[i])
                        {
                            switch (direction)
                            {

                                case DIRECTION.RIGHT:

                                    box_X[k] = tree_X[i] - 1;
                                    player_X = box_X[k] - 1;
                                    break;

                                case DIRECTION.LEFT:

                                    box_X[k] = tree_X[i] + 1;
                                    player_X = box_X[k] + 1;
                                    break;

                                case DIRECTION.UP:

                                    box_Y[k] = tree_Y[i] + 1;
                                    player_Y = box_Y[k] + 1;
                                    break;

                                case DIRECTION.DOWN:

                                    box_Y[k] = tree_Y[i] - 1;
                                    player_Y = box_Y[k] - 1;
                                    break;
                            }
                        }
                    }
                }
                #endregion

                #region 박스끼리 밀면 막히는 거 구현

                for (int i = 0; i < box_X.Length; ++i)
                {
                    for (int k = 0; k < box_X.Length; ++k)
                    {
                        if (k != i && box_X[k] == box_X[i] && box_Y[k] == box_Y[i])
                        {
                            switch (direction)
                            {
                                case DIRECTION.RIGHT:

                                    box_X[i] = box_X[k] - 1;
                                    --player_X;
                                    break;

                                case DIRECTION.LEFT:

                                    box_X[i] = box_X[k] + 1;
                                    ++player_X;
                                    break;

                                case DIRECTION.UP:

                                    box_Y[i] = box_Y[k] + 1;
                                    ++player_Y;
                                    break;

                                case DIRECTION.DOWN:

                                    box_Y[i] = box_Y[k] - 1;
                                    --player_Y;
                                    break;
                            }
                        }
                    }
                }
                #endregion

                #region 골에 박스 넣으면 끝나는 거 구현
                int count = 0;

                for (int i = 0; i < box_X.Length; ++i)
                {
                    for (int k = 0; k < goal_X.Length; ++k)
                    {
                        if (box_X[i] == goal_X[k] && box_Y[i] == goal_Y[k])
                        {
                            ++count;
                        }
                    }
                }

                if (count == goal_X.Length)
                {
                    break;
                }

                #endregion

                



                
                
                

                



                #endregion
            }

            // 목적 달성하면 나오는 화면
            Console.Clear();
            Console.WriteLine("ㅊㅋㅊㅋㅊㅋㅊㅋㅊㅋ");


        }

    }
}




