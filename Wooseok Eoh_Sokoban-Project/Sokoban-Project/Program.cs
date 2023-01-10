


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

    enum REVERSE_DIRECTION
    {
        NONE = 0,
        REVERSERIGHT = 1,
        REVERSELEFT = 2,
        REVERSEUP = 3,
        REVERSEDOWN = 4
    }


    class program
    {
        static void Main()
        {
            #region 초기세팅

            Console.ResetColor(); // 컬러를 초기화
            Console.BackgroundColor = ConsoleColor.Blue; // 배경 색
            Console.ForegroundColor = ConsoleColor.Yellow; // 글자 색
            Console.CursorVisible = false; // 커서를 숨김
            Console.Title = "어코반"; // 제목을 지어줌
            Console.Clear(); // 원래 밑에 뜨는 그 뭐 많은 것들 지워주는거임

            #endregion

            #region 게임종료 시 ㅊㅋ메세지
            string congrats = " ⊂_ヽ\r\n　 ＼＼ Λ＿Λ\r\n　　 ＼( ‘ㅅ’ ) 두둠칫\r\n　　　 >　⌒ヽ\r\n　　　/ 　 へ＼\r\n　　 /　　/　＼＼\r\n　　 ﾚ　ノ　　 ヽ_つ\r\n　　/　/두둠칫\r\n　 /　/|\r\n　(　(ヽ\r\n　|　|、＼\r\n　| 丿 ＼ ⌒)\r\n　| |　　) /\r\n`ノ )　　Lﾉ";
            #endregion

            #region 변수 좌표 및 기호

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

            string box = "B";

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

            string tree = "♣";

            const int tree1X = 3;
            const int tree1Y = 4;

            const int tree2X = 3;
            const int tree2Y = 5;

            const int tree3X = 3;
            const int tree3Y = 6;

            const int tree4X = 3;
            const int tree4Y = 7;

            const int tree5X = 13;
            const int tree5Y = 1;

            const int tree6X = 13;
            const int tree6Y = 2;

            const int tree7X = 13;
            const int tree7Y = 3;

            const int tree8X = 13;
            const int tree8Y = 10;

            const int tree9X = 13;
            const int tree9Y = 9;

            const int tree10X = 13;
            const int tree10Y = 8;

            const int tree11X = 13;
            const int tree11Y = 7;

            const int tree12X = 13;
            const int tree12Y = 6;

            const int tree13X = 13;
            const int tree13Y = 5;


            int[] tree_X = new int[] { tree1X, tree2X, tree3X, tree4X, tree5X, tree6X, tree7X, tree8X, tree9X, tree10X, tree11X, tree12X, tree13X };
            int[] tree_Y = new int[] { tree1Y, tree2Y, tree3Y, tree4Y, tree5Y, tree6Y, tree7Y, tree8Y, tree9Y, tree10Y, tree11Y, tree12Y, tree13Y };

            #endregion

            #region goal 기호 및 좌표

            string goal = "*";

            const int goal1X = 8;
            const int goal1Y = 1;

            const int goal2X = 14;
            const int goal2Y = 2;

            const int goal3X = 4;
            const int goal3Y = 8;

            const int goal4X = 23;
            const int goal4Y = 5;

            const int goal5X = 27;
            const int goal5Y = 6;



            int[] goal_X = new int[] { goal1X, goal2X, goal3X, goal4X, goal5X };
            int[] goal_Y = new int[] { goal1Y, goal2Y, goal3Y, goal4Y, goal5Y };

            #endregion

            #region DIRECTION 이라는 타입을 가진 객체를 초기화해줌
            DIRECTION direction = default; // DIRECTION(열거형) 타입의 direction 이라는 객체를 초기화해줬음
            #endregion

            #region POSITION 이라는 타입을 가진 객체배열을 만들고 초기화해줌
            POSITION[] position = new POSITION[box_X.Length];
            #endregion

            REVERSE_DIRECTION reverse_movement = REVERSE_DIRECTION.NONE;

            #region 골안 박스 바꿔주는 기호
            string success = "♬";
            #endregion

            #region 골 위 박스의 유무를 저장하는 배열
            bool[] isboxongoal = new bool[box_X.Length]; // n번째 박스가 골위에 있는지를 저장하는 bool배열
            #endregion

            int movecount = 0; // 이동한 횟수를 저장하는 변수

            #region 포탈 기호 및 좌표

            string portal = "?";
            const int portal1X = 5;
            const int portal1Y = 8;

            const int portal2X = 23;
            const int portal2Y = 2;

            int[] portal1_X = new int[] { portal1X, portal2X };
            int[] portal1_Y = new int[] { portal1Y, portal2Y };

            #endregion


            #region 아이템 기호 및 좌표
            string confusion = "♥";
            int confusionX = 7;
            int confusionY = 5;

            bool ate = false;
            int last = 0;
            #endregion




            #endregion

            #region 게임루프

            while (true)
            {
                Console.Clear();

                #region 렌더링

                Console.SetCursorPosition(Map_minX + 2, Map_maxY + 3);
                Console.Write("'Space' = 당기기");

                Console.SetCursorPosition(Map_minX + 2, Map_maxY + 4);
                Console.Write("'A' = 발로 차기 ");

                Console.SetCursorPosition(Map_minX + 2, Map_maxY + 5);
                Console.Write("'?' = 포탈 ");

                Console.SetCursorPosition(Map_minX + 2, Map_maxY + 8);
                Console.Write("Hint = 13+26+4-30+3-12+7-6+1-2");

                Console.SetCursorPosition(Map_maxX + 5, Map_minY + 2);
                Console.Write($"움직인 횟수 = {movecount}");

                #region 아이템 그려주기
                if (ate == true)
                {
                    // 한번 먹었으면 안그리기
                }
                else // 안먹었으면 그려주기
                {
                    Console.SetCursorPosition(confusionX, confusionY);
                    Console.Write(confusion);
                }

                Console.SetCursorPosition(Map_maxX + 5, 5);
                Console.Write($"Confusion Left = {last}");

                #endregion

                #region 프레임 그려주기
                for (int i = 1; i < Map_maxY + 1; ++i) // Y축 그려주기 = X는 고정이고 Y만 달라지는거임
                {
                    Console.SetCursorPosition(Map_minX - 1, i);
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write(frame);
                    Console.SetCursorPosition(Map_maxX + 1, i);
                    Console.Write(frame);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }

                for (int i = 1; i < Map_maxX + 3; ++i) // x축 그려주기 = y축은 고정이고 X만 달라지는거임
                {
                    Console.SetCursorPosition(i - 1, Map_minY - 1);
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write(frame);
                    Console.SetCursorPosition(i - 1, Map_maxY + 1);
                    Console.Write(frame);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                #endregion

                #region goal 여러개 그려주기

                for (int i = 0; i < goal_X.Length; ++i)
                {
                    Console.SetCursorPosition(goal_X[i], goal_Y[i]);
                    Console.Write(goal);

                }

                #endregion

                #region 나무 여러개 그려주기
                for (int i = 0; i < tree_X.Length; ++i)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(tree_X[i], tree_Y[i]);

                    
                    Console.Write(tree);
                    
                }


                Console.SetCursorPosition(13, 4); // 페이크 나무
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(tree);
                Console.ForegroundColor = ConsoleColor.Yellow;

                #endregion

                #region 플레이어 그려주기
                Console.SetCursorPosition(player_X, player_Y);
                Console.Write(player);
                #endregion

                #region 박스 그려주기 + 골에 박스가 들어가면 다른 아이가 출력되도록 그려주기

                for (int k = 0; k < box_X.Length; ++k)
                {
                    Console.SetCursorPosition(box_X[k], box_Y[k]);

                    if (isboxongoal[k])
                    {
                        Console.Write(success);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write(box);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                }



                #endregion

                #region 포탈 그려주기

                for (int i = 0; i < portal1_X.Length; ++i)
                {
                    Console.SetCursorPosition(portal1_X[i], portal1_Y[i]);
                    Console.Write(portal);
                }

                #endregion

                #endregion

                #region input
                // 인풋

                ConsoleKey key = Console.ReadKey().Key;
                #endregion

                #region 업데이트

                #region 이동한 횟수 측정하기

                if (key == ConsoleKey.RightArrow || key == ConsoleKey.LeftArrow || key == ConsoleKey.UpArrow || key == ConsoleKey.DownArrow || key == ConsoleKey.Spacebar)
                {
                    ++movecount;
                }

                #endregion

                #region 플레이어 이동 구현 + 플레이어가 포탈 타는 거 구현 +  아이템 먹으면 5프레임 반대로 움직임 구현

                if (player_X == confusionX && player_Y == confusionY)
                {
                    ate = true;
                    last = 5;
                    confusionX = 50;
                    confusionY = 50;
                    Console.SetCursorPosition(confusionX, confusionY);


                }

                if (ate == true && 0 < last)
                {
                    if (key == ConsoleKey.RightArrow)
                    {
                        --player_X;
                    }
                    if (key == ConsoleKey.LeftArrow)
                    {
                        ++player_X;
                    }
                    if (key == ConsoleKey.UpArrow)
                    {
                        ++player_Y;
                    }
                    if (key == ConsoleKey.DownArrow)
                    {
                        --player_Y;
                    }
                    --last;
                }
                else
                {
                    if (key == ConsoleKey.RightArrow)
                    {
                        player_X = Math.Min(player_X + 1, Map_maxX);
                        direction = DIRECTION.RIGHT;
                        if (player_X == portal1_X[0] && player_Y == portal1_Y[0])
                        {
                            player_X = portal1_X[1] + 1;
                            player_Y = portal1_Y[1];
                        }
                        else if (player_X == portal1_X[1] && player_Y == portal1_Y[1])
                        {
                            player_X = portal1_X[0] + 1;
                            player_Y = portal1_Y[0];
                        }
                    }

                    if (key == ConsoleKey.LeftArrow)
                    {
                        player_X = Math.Max(player_X - 1, Map_minX);
                        direction = DIRECTION.LEFT;
                        if (player_X == portal1_X[0] && player_Y == portal1_Y[0])
                        {
                            player_X = portal1_X[1] - 1;
                            player_Y = portal1_Y[1];
                        }
                        else if (player_X == portal1_X[1] && player_Y == portal1_Y[1])
                        {
                            player_X = portal1_X[0] - 1;
                            player_Y = portal1_Y[0];
                        }
                    }

                    if (key == ConsoleKey.UpArrow)
                    {
                        player_Y = Math.Max(player_Y - 1, Map_minY);
                        direction = DIRECTION.UP;
                        if (player_X == portal1_X[0] && player_Y == portal1_Y[0])
                        {
                            player_X = portal1_X[1];
                            player_Y = portal1_Y[1] - 1;
                        }
                        else if (player_X == portal1_X[1] && player_Y == portal1_Y[1])
                        {
                            player_X = portal1_X[0];
                            player_Y = portal1_Y[0] - 1;
                        }
                    }

                    if (key == ConsoleKey.DownArrow)
                    {
                        player_Y = Math.Min(player_Y + 1, Map_maxY);
                        direction = DIRECTION.DOWN;
                        if (player_X == portal1_X[0] && player_Y == portal1_Y[0])
                        {
                            player_X = portal1_X[1];
                            player_Y = portal1_Y[1] + 1;
                        }
                        else if (player_X == portal1_X[1] && player_Y == portal1_Y[1])
                        {
                            player_X = portal1_X[0];
                            player_Y = portal1_Y[0] + 1;
                        }
                    }
                }


                #endregion

                #region 플레이어와 박스의 상대적인 위치 정의


                for (int i = 0; i < box_X.Length; ++i)
                {
                    if (player_Y == box_Y[i] && player_X == box_X[i] + 1)
                    {
                        position[i] = POSITION.RIGHT;
                        break;
                    }

                    else if (player_Y == box_Y[i] && player_X == box_X[i] - 1)
                    {
                        position[i] = POSITION.LEFT;
                        break;
                    }

                    else if (player_X == box_X[i] && player_Y == box_Y[i] + 1)
                    {
                        position[i] = POSITION.DOWN;
                        break;
                    }

                    else if (player_X == box_X[i] && player_Y == box_Y[i] - 1)
                    {
                        position[i] = POSITION.UP;
                        break;
                    }
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
                                if (box_X[i] == portal1_X[0] && box_Y[i] == portal1_Y[0])
                                {
                                    box_X[i] = portal1_X[1] + 1;
                                    box_Y[i] = portal1_Y[1];
                                }
                                else if (box_X[i] == portal1_X[1] && box_Y[i] == portal1_Y[1])
                                {
                                    box_X[i] = portal1_X[0] + 1;
                                    box_Y[i] = portal1_Y[0];
                                }
                                break;

                            case DIRECTION.LEFT:

                                box_X[i] = Math.Max(player_X - 1, Map_minX);
                                player_X = box_X[i] + 1;
                                if (box_X[i] == portal1_X[0] && box_Y[i] == portal1_Y[0])
                                {
                                    box_X[i] = portal1_X[1] - 1;
                                    box_Y[i] = portal1_Y[1];
                                }
                                else if (box_X[i] == portal1_X[1] && box_Y[i] == portal1_Y[1])
                                {
                                    box_X[i] = portal1_X[0] - 1;
                                    box_Y[i] = portal1_Y[0];
                                }
                                break;

                            case DIRECTION.UP:

                                box_Y[i] = Math.Max(player_Y - 1, Map_minY);
                                player_Y = box_Y[i] + 1;
                                if (box_X[i] == portal1_X[0] && box_Y[i] == portal1_Y[0])
                                {
                                    box_Y[i] = portal1_Y[1] - 1;
                                    box_X[i] = portal1_X[1];
                                }
                                else if (box_X[i] == portal1_X[1] && box_Y[i] == portal1_Y[1])
                                {
                                    box_Y[i] = portal1_Y[0] - 1;
                                    box_X[i] = portal1_X[0];
                                }
                                break;

                            case DIRECTION.DOWN:

                                box_Y[i] = Math.Min(player_Y + 1, Map_maxY);
                                player_Y = box_Y[i] - 1;
                                if (box_X[i] == portal1_X[0] && box_Y[i] == portal1_Y[0])
                                {
                                    box_Y[i] = portal1_Y[1] + 1;
                                    box_X[i] = portal1_X[1];
                                }
                                else if (box_X[i] == portal1_X[1] && box_Y[i] == portal1_Y[1])
                                {
                                    box_Y[i] = portal1_Y[0] + 1;
                                    box_X[i] = portal1_X[0];
                                }
                                break;
                        }
                    }
                }
                #endregion

                #region 박스 당기기 구현
                for (int i = 0; i < box_X.Length; ++i)
                {

                    if (key == ConsoleKey.Spacebar)
                    {
                        switch (position[i])
                        {
                            case POSITION.RIGHT:

                                player_X = Math.Min(player_X + 1, Map_maxX);
                                box_X[i] = player_X - 1;
                                break;

                            case POSITION.LEFT:

                                player_X = Math.Max(player_X - 1, Map_minX);
                                box_X[i] = player_X + 1;
                                break;

                            case POSITION.UP:

                                player_Y = Math.Max(player_Y - 1, Map_minY);
                                box_Y[i] = player_Y + 1;
                                break;

                            case POSITION.DOWN:

                                player_Y = Math.Min(player_Y + 1, Map_maxY);
                                box_Y[i] = player_Y - 1;
                                break;
                        }
                    }

                }
                #endregion

                #region 박스 차기 구현
                for (int i = 0; i < box_X.Length; ++i)
                {

                    if (key == ConsoleKey.A)
                    {
                        switch (position[i])
                        {
                            case POSITION.RIGHT:


                                box_X[i] = Map_minX;

                                break;

                            case POSITION.LEFT:

                                box_X[i] = Map_maxX;

                                break;

                            case POSITION.UP:

                                box_Y[i] = Map_maxY;

                                break;

                            case POSITION.DOWN:

                                box_Y[i] = Map_minY;

                                break;
                        }
                    }
                    position[i] = POSITION.NONE;
                }
                #endregion

                #region 플레이어가 나무에 막히는 거 구현

                for (int i = 0; i < tree_X.Length; ++i)
                {
                    for (int k = 0; k < box_X.Length; ++k)
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
                    isboxongoal[i] = false;

                    for (int k = 0; k < goal_X.Length; ++k)
                    {
                        if (box_X[i] == goal_X[k] && box_Y[i] == goal_Y[k])
                        {
                            ++count;
                            isboxongoal[i] = true;
                            // n번째 박스가 골 위에 있는지를 저장
                            break;
                            // break를 넣어주는 이유는 어차피 골하나에 박스는 하나밖에 못올라가니까, 박스 하나가 올려진게 확인이 되었으면 나머지 박스는 검사 안해도됌

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

            #endregion

            // 목적 달성하면 나오는 화면
            Console.Clear();
            Console.WriteLine(congrats);

            
        }

    }
}





