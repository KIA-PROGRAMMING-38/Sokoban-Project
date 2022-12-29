namespace Sokoban
{
    // 열거형
    

    

    enum DIRECTION
    {
        NONE = 0,
        LEFT = 1,
        RIGHT = 2,
        UP = 3,
        DOWN = 4
    }


    class program
    {
        static void Main()
        {

           

            #region 초기세팅
            // 초기 세팅
            Console.ResetColor(); // 컬러를 초기화한다
            Console.CursorVisible = false; // 커서를 안보이게 해줌(콘솔에서는 입력하려는 표시인거임)
            Console.Title = "소코반 게임!"; // 콘솔창의 제목을 입력
            Console.BackgroundColor = ConsoleColor.Cyan; // 배경색을 바꿔줌
            Console.ForegroundColor = ConsoleColor.Red; // 글씨의 색을 바꿔줌
            Console.Clear(); // 출력된 모든 내용을 지운다
            #endregion

            #region 맵 최대범위
            
            const int MAP_MIN_X = 0; // 맵의 가로 최소
            const int MAP_MIN_Y = 0; // 맵의 세로 최소
            const int MAP_MAX_X = 20; // 맵의 가로 최대
            const int MAP_MAX_Y = 15; // 맵의 세로 최대

            #endregion

            #region 플레이어기호 및 좌표
            const string Player = "P"; // 플레이어 기호
            const int player_X = 0; // 플레이어 X 초기좌표
            const int player_Y = 0; // 플레이어 Y 초기좌표
            int playerX = player_X; // 플레이어 X 초기좌표
            int playerY = player_Y; // 플레이어 Y 초기좌표
            #endregion

            #region 추가요소기호 및 좌표배열

            #region 박스 기호 및 좌표배열
            const string Ball = "O"; // 박스 기호

            const int Box1_INITIAL_X = 2; // 박스1 X좌표
            const int Box1_INITIAL_Y = 3; // 박스1 Y좌표
            

            
            const int Box2_INITIAL_X = 4; // 박스2 X 좌표
            const int Box2_INITIAL_Y = 5; // 박스2 Y 좌표
            

            
            const int Box3_INITIAL_X = 8; // 박스3 X 좌표
            const int Box3_INITIAL_Y = 10; // 박스3 X 좌표


            int[] boxX = new int[] { Box1_INITIAL_X, Box2_INITIAL_X, Box3_INITIAL_X }; // 박스들의 X좌표 배열
            int[] boxY = new int[] { Box1_INITIAL_Y, Box2_INITIAL_Y, Box3_INITIAL_Y }; // 박스들의 Y좌표 배열
            int box_length = boxX.Length; // 박스 갯수
            #endregion

            #region 벽 기호 및 좌표배열
            const string Wall = "W"; // 벽 기호

            const int Wall1_INITIAL_X= 7; // 벽1의 X 좌표
            const int Wall1_INITIAL_Y = 8; // 벽1의 Y 좌표

            
            const int Wall2_INITIAL_X = 3;
            const int Wall2_INITIAL_Y = 3;

            
            const int Wall3_INITIAL_X = 10;
            const int Wall3_INITIAL_Y = 10;

            int[] wallX = new int[] {Wall1_INITIAL_X, Wall2_INITIAL_X, Wall3_INITIAL_X }; // 벽들의 X 좌표 배열
            int[] wallY = new int[] {Wall1_INITIAL_Y, Wall2_INITIAL_Y, Wall3_INITIAL_Y }; // 벽들의 Y 좌표 배열
            int wall_length = wallX.Length; // 벽 갯수
            #endregion

            #region goal 기호 및 좌표배열
            const string Goal = "*"; // goal 기호

            const int Goal1_INITIAL_X = 9; // GOAL1 X 좌표
            const int Goal1_INITIAL_Y = 9; // GOAL1 Y 좌표

            
            const int Goal2_INITIAL_X = 3; // GOAL2 X 좌표
            const int Goal2_INITIAL_Y = 7; // GOAL2 Y 좌표

            
            const int Goal3_INITIAL_X = 5; // GOAL3 X 좌표
            const int Goal3_INITIAL_Y = 5; // GOAL3 Y 좌표

            int[] goalX = new int[] {Goal1_INITIAL_X, Goal2_INITIAL_X, Goal3_INITIAL_X }; // goal들의 X좌표 배열
            int[] goalY = new int[] {Goal1_INITIAL_Y, Goal2_INITIAL_Y, Goal3_INITIAL_Y }; // goal들의 Y좌표 배열
            int goal_length = goalX.Length; // goal 갯수
            #endregion
            
            #endregion



            DIRECTION playerDirection = DIRECTION.NONE; // playerDirection 이라는 객체를 초기화해줌

           
            while (true)
            {

                Console.Clear();

                #region 렌더링


                Console.SetCursorPosition(playerX, playerY); // 플레이어를 콘솔에 구현
                Console.Write(Player);


                for(int i = 0; i < box_length ;++i) // 박스들 콘솔에 구현
                {
                    Console.SetCursorPosition(boxX[i], boxY[i]);
                    Console.Write(Ball);
                }

                for (int i = 0; i < wall_length; ++i) // 벽들 콘솔에 구현
                {
                    Console.SetCursorPosition(wallX[i], wallY[i]);
                    Console.Write(Wall);
                }


                for (int i = 0; i < goal_length; ++i) // goal들 콘솔에 구현
                {
                    Console.SetCursorPosition(goalX[i], goalY[i]);
                    Console.Write(Goal);
                }
                #endregion

                #region 인풋

                ConsoleKey key = Console.ReadKey().Key;
                #endregion


                // ------------------------------------------ Update -----------------------------------------------




                #region 플레이어 좌표 구하기
                if (key == ConsoleKey.RightArrow) // 오른쪽으로 이동할 때
                {
                    
                    playerX = Math.Min(playerX + 1, MAP_MAX_X);
                    playerDirection = DIRECTION.RIGHT;
                    
                }

                if (key == ConsoleKey.LeftArrow) // 왼쪽으로 이동할 때
                {
                    playerX = Math.Max(MAP_MIN_X , playerX - 1);
                    playerDirection = DIRECTION.LEFT;
                   
                }

                if (key == ConsoleKey.DownArrow) // 아래로 이동할 때
                {
                    playerY = Math.Min(playerY + 1, MAP_MAX_Y);
                    playerDirection = DIRECTION.DOWN;
                    
                }

                if (key == ConsoleKey.UpArrow) // 위로 이동할 때
                {
                    playerY = Math.Max(MAP_MIN_Y, playerY - 1);
                    playerDirection = DIRECTION.UP;
                    
                }
                #endregion

                #region 박스 움직이기
                for (int i = 0; i < box_length ;++i)
                {
                    if(playerX == boxX[i] && playerY == boxY[i])
                    {
                        switch (playerDirection)
                        {
                            case DIRECTION.LEFT:
                                if (boxX[i] == MAP_MIN_X)
                                {
                                    playerX = MAP_MIN_X + 1;
                                }
                                else
                                {
                                    boxX[i] = boxX[i] - 1;
                                }
                                
                                break;

                            case DIRECTION.RIGHT:
                                if (boxX[i] == MAP_MAX_X)
                                {
                                    playerX = MAP_MAX_X - 1;
                                }
                                else
                                {
                                    boxX[i] = boxX[i] + 1;
                                }

                                break;

                            case DIRECTION.UP:
                                if (boxY[i] == MAP_MIN_Y)
                                {
                                    playerY = MAP_MIN_Y + 1;
                                }
                                else
                                {
                                    boxY[i] = boxY[i] - 1;
                                }

                                break;

                            case DIRECTION.DOWN:
                                if (boxY[i] == MAP_MAX_Y)
                                {
                                    playerY = MAP_MAX_Y - 1;
                                }
                                else
                                {
                                    boxY[i] = boxY[i] + 1;
                                }

                                break;

                            default:
                                Console.Clear();
                                Console.WriteLine("you have entered something wrong");
                                return;
                        }
                    }
                }

#endregion

                #region 플레이어가 벽에 부딪힐 때
                for (int i = 0; i < wall_length ;++i)
                {

                    if(playerX == wallX[i] && playerY == wallY[i])
                    {
                        switch (playerDirection)
                        {
                            case DIRECTION.LEFT:
                                playerX = wallX[i] + 1;
                                break;

                            case DIRECTION.RIGHT:
                                playerX = wallX[i] - 1;
                                break;

                            case DIRECTION.UP:
                                playerY = wallY[i] + 1;
                                break;

                            case DIRECTION.DOWN:
                                playerY = wallY[i] - 1;
                                break;

                            default:
                                Console.Clear();
                                Console.WriteLine("You have entered something wrong");
                                return;

                        }
                    }
                }
#endregion

                #region box가 벽에 부딪힐 때 + 이후 player의 위치

                for (int i = 0; i < wall_length ;++i)
                {
                    for(int k = 0; k < box_length; ++k)
                    {

                        if (boxX[k] == wallX[i] && boxY[k] == wallY[i])
                        {
                            switch (playerDirection)
                            {
                                case DIRECTION.LEFT:
                                    boxX[k] = wallX[i] + 1;
                                    playerX = boxX[k] + 1;
                                    break;

                                case DIRECTION.RIGHT:
                                    boxX[k] = wallX[i] - 1;
                                    playerX = boxX[k] - 1;
                                    break;

                                case DIRECTION.UP:
                                    boxY[k] = wallY[i] + 1;
                                    playerY = boxY[k] + 1;
                                    break;

                                case DIRECTION.DOWN:
                                    boxY[k] = wallY[i] - 1;
                                    playerY = boxY[k] - 1;
                                    break;

                                default:
                                    Console.Clear();
                                    Console.WriteLine("You have entered something wrong");
                                    return;
                            }
                        }
                    }
                }
                #endregion



                #region 박스끼리 부딪힐 때

                if (boxX[0] == boxX[1] && boxY[0] == boxY[1]) // 1번째 박스가 2번쨰 박스를 밀 때
                {
                    switch(playerDirection)
                    {
                        case DIRECTION.LEFT:
                            boxX[1] = boxX[0] - 1;
                            break;

                        case DIRECTION.RIGHT:
                            boxX[1] = boxX[0] + 1;
                            break;

                        case DIRECTION.UP:
                            boxY[1] = boxY[0] - 1;
                            break;

                        case DIRECTION.DOWN:
                            boxY[1] = boxY[0] + 1;
                            break;

                        default:
                            Console.Clear();
                            Console.WriteLine("You have entered something wrong");
                            return;
                    }
                }



                if (boxX[0] == boxX[1] && boxY[0] == boxY[1]) // 2번째 박스가 1번쨰 박스를 밀 때
                {
                    switch (playerDirection)
                    {
                        case DIRECTION.LEFT:
                            boxX[0] = boxX[1] - 1;
                            break;

                        case DIRECTION.RIGHT:
                            boxX[0] = boxX[1] + 1;
                            break;

                        case DIRECTION.UP:
                            boxY[0] = boxY[1] - 1;
                            break;

                        case DIRECTION.DOWN:
                            boxY[0] = boxY[1] + 1;
                            break;

                        default:
                            Console.Clear();
                            Console.WriteLine("You have entered something wrong");
                            return;
                    }
                }


                if (boxX[0] == boxX[2] && boxY[0] == boxY[2]) // 1번째 박스가 3번쨰 박스를 밀 때
                {
                    switch (playerDirection)
                    {
                        case DIRECTION.LEFT:
                            boxX[2] = boxX[0] - 1;
                            break;

                        case DIRECTION.RIGHT:
                            boxX[2] = boxX[0] + 1;
                            break;

                        case DIRECTION.UP:
                            boxY[2] = boxY[0] - 1;
                            break;

                        case DIRECTION.DOWN:
                            boxY[2] = boxY[0] + 1;
                            break;

                        default:
                            Console.Clear();
                            Console.WriteLine("You have entered something wrong");
                            return;
                    }
                }

                if (boxX[0] == boxX[2] && boxY[0] == boxY[2]) // 3번째 박스가 1번쨰 박스를 밀 때
                {
                    switch (playerDirection)
                    {
                        case DIRECTION.LEFT:
                            boxX[0] = boxX[2] - 1;
                            break;

                        case DIRECTION.RIGHT:
                            boxX[0] = boxX[2] + 1;
                            break;

                        case DIRECTION.UP:
                            boxY[0] = boxY[2] - 1;
                            break;

                        case DIRECTION.DOWN:
                            boxY[0] = boxY[2] + 1;
                            break;

                        default:
                            Console.Clear();
                            Console.WriteLine("You have entered something wrong");
                            return;
                    }
                }

                if (boxX[1] == boxX[2] && boxY[1] == boxY[2]) // 2번째 박스가 3번쨰 박스를 밀 때
                {
                    switch (playerDirection)
                    {
                        case DIRECTION.LEFT:
                            boxX[2] = boxX[1] - 1;
                            break;

                        case DIRECTION.RIGHT:
                            boxX[2] = boxX[1] + 1;
                            break;

                        case DIRECTION.UP:
                            boxY[2] = boxY[1] - 1;
                            break;

                        case DIRECTION.DOWN:
                            boxY[2] = boxY[1] + 1;
                            break;

                        default:
                            Console.Clear();
                            Console.WriteLine("You have entered something wrong");
                            return;
                    }
                }


                if (boxX[1] == boxX[2] && boxY[1] == boxY[2]) // 3번째 박스가 2번쨰 박스를 밀 때
                {
                    switch (playerDirection)
                    {
                        case DIRECTION.LEFT:
                            boxX[1] = boxX[2] - 1;
                            break;

                        case DIRECTION.RIGHT:
                            boxX[1] = boxX[2] + 1;
                            break;

                        case DIRECTION.UP:
                            boxY[1] = boxY[2] - 1;
                            break;

                        case DIRECTION.DOWN:
                            boxY[1] = boxY[2] + 1;
                            break;

                        default:
                            Console.Clear();
                            Console.WriteLine("You have entered something wrong");
                            return;
                    }
                }










                #endregion








                #region box를 goal에 넣었을때 끝남
                int A = 0; // goal에 박스 3개 다 들어있을 경우의 값을 저장하기 위해 생성함
                
                for (int i = 0; i < goal_length ;++i)
                {
                    for(int k = 0; k < box_length ;++k)
                    {
                        if (boxX[k] == goalX[i] && boxY[k] == goalY[i])
                        {
                            A++;
                        }
                    }
                    
                }

                if (A == goal_length)
                {
                    break;
                }
            }
            #endregion

            Console.Clear();
            Console.WriteLine("Finished");
        }


    }




}






