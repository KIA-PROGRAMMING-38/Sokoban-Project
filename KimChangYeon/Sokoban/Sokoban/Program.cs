using Sokoban;
using System.ComponentModel;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Text.Encodings.Web;
using System.Xml;

namespace sokoban
{
    enum PLAYER_DIRECTION
    {
        RIGHT,
        LEFT,
        DOWN,
        UP
    }
    class Program
    {
        static void Main()
        {

            //초기 세팅
            Console.ResetColor();                            // 컬러를 초기화 한다
            Console.CursorVisible = false;                   // 커서를 숨긴다
            Console.Title = "홍성재의 썬더펀치";               // 타이틀을 설정한다.
            Console.BackgroundColor = ConsoleColor.DarkGray; //배경색을 설정한다.
            Console.ForegroundColor = ConsoleColor.DarkBlue; //글꼴색을 설정한다.
            Console.Clear();                                 //출력된 모든 내용을 지운다.

            // 기호 상수
            const int MAP_MIN_X = 0;
            const int MAP_MIN_Y = 0;
            const int MAP_MAX_X = 20;
            const int MAP_MAX_Y = 12;

            PLAYER_DIRECTION playerDir = new PLAYER_DIRECTION();

            Player player = new Player
            {
                X = 14,
                Y = 2,
                PlayerDir = playerDir,
                PushedBoxId = 0,
                Symbol = '●'
            };

            Box[] boxes = new Box[]
            {
                new Box {X = 3, Y = 3, IsOnGoal = false , Symbol = '■'},
                new Box {X = 6, Y = 6, IsOnGoal = false , Symbol = '■'},
                new Box {X = 9, Y = 9, IsOnGoal = false , Symbol = '■'}
            };

            Wall[] walls = new Wall[]
            {
                new Wall {X = 1 , Y = 4 , Symbol = '#'},
                new Wall {X = 2 , Y = 5 , Symbol = '#'},
                new Wall {X = 3 , Y = 6 , Symbol = '#'}
            };

            Goal[] goals = new Goal[]
            {
                new Goal { X = 13 , Y = 9, Symbol = '□' , InSymbol = '▣'},
                new Goal { X = 14 , Y = 9, Symbol = '□' , InSymbol = '▣'},
                new Goal { X = 15 , Y = 9, Symbol = '□' , InSymbol = '▣'}
            };

            HorizonItem[] horizonItem = new HorizonItem[]
            {
               new HorizonItem {X = 10 , Y = 5, Symbol = '↔'},
               new HorizonItem {X = 15 , Y = 8, Symbol = '↔'}
            };

            VerticalItem[] verticalItem = new VerticalItem[]
            {
                new VerticalItem {X = 8 , Y = 6, Symbol = '↕'},
                new VerticalItem {X = 7 , Y = 10, Symbol = '↕'}
            };


            int boxLength = boxes.Length;
            int wallLength = walls.Length;
            int goalLength = goals.Length;
            int itemLength = horizonItem.Length;
            int boxCount = default;
            int function = 0;

            bool[] isBoxOnGoal = new bool[boxLength];
            bool clearJudge = true;
            bool changeDir = false;
           

            // 게임 루프 == 프레임(Frame)
            while (clearJudge)
            {
                // --------------------------------------------- Render -------------------------------------------------------

                Render();
                
                void Render()
                {
                    MapRender();
                    GoalRender();
                    WallRender();
                    PlayerRender();
                    ChangeRender();
                    ItemRender();

                    StringRender();
                }

                void MapRender()
                {
                    Console.Clear();
                    Console.WriteLine("######################");

                    for (int i = 0; i <= MAP_MAX_Y - 1; i++)
                    {
                        Console.WriteLine("#                    #");

                    }
                    for (int j = 0; j <= MAP_MAX_X + 1; j++)
                    {

                        Console.Write("#");
                    }
                }

                void GoalRender()
                {
                    for (int goalId = 0; goalId < goalLength; goalId++)
                    {
                        Console.SetCursorPosition(goals[goalId].X, goals[goalId].Y);
                        Console.Write(goals[goalId].Symbol);
                    }
                }

                void WallRender()
                {
                    for (int wallId = 0; wallId < wallLength; wallId++)
                    {
                        Console.SetCursorPosition(walls[wallId].X, walls[wallId].Y);
                        Console.Write(walls[wallId].Symbol);
                    }
                }

                void ChangeRender()
                {
                    for (int boxId = 0; boxId < boxLength; boxId++)
                    {
                        Console.SetCursorPosition(boxes[boxId].X, boxes[boxId].Y);

                        if (boxes[boxId].IsOnGoal)
                        {
                            Console.Write(goals[boxId].InSymbol);
                        }
                        else
                        {
                            Console.Write(boxes[boxId].Symbol);
                        }
                    }
                }

                void PlayerRender()
                {
                    Console.SetCursorPosition(player.X, player.Y);
                    Console.Write(player.Symbol);
                }

                void ItemRender()
                {
                    for (int itemId = 0; itemId < itemLength; itemId++)
                    {
                        Console.SetCursorPosition(horizonItem[itemId].X, horizonItem[itemId].Y);
                        Console.Write(horizonItem[itemId].Symbol);

                        Console.SetCursorPosition(verticalItem[itemId].X, verticalItem[itemId].Y);
                        Console.Write(verticalItem[itemId].Symbol);
                    }
                }

                void StringRender()
                {
                    Console.SetCursorPosition(MAP_MAX_X + 5, 5);
                    Console.Write(function);
                }

                
                // --------------------------------------------- ProcessInput -------------------------------------------------
                ConsoleKey key = Console.ReadKey().Key;
                // --------------------------------------------- Update -------------------------------------------------------

                MoveRight();
                MoveLeft();
                MoveDown();
                MoveUp();

                WithPlayerBox ();
                WithBoxWall();
                WithPlayerWall();
                WithBoxBox();
                WithPlayerHitem();

                GoalInJudge();
                JudgeClear();

                void MoveRight()
                {
                    if (key == ConsoleKey.RightArrow && changeDir == false)
                    {

                        player.X = Math.Min(player.X + 1, MAP_MAX_X);
                        playerDir = PLAYER_DIRECTION.RIGHT;
                    }

                    else if (key == ConsoleKey.RightArrow && changeDir == true)
                    {
                        player.X = Math.Max(player.X - 1, MAP_MIN_X);
                        playerDir = PLAYER_DIRECTION.LEFT;

                        function--;
                    }
                }

                void MoveLeft()
                {
                    if (key == ConsoleKey.LeftArrow && changeDir == false)
                    {
                        player.X = Math.Max(player.X - 1, MAP_MIN_X + 1);
                        playerDir = PLAYER_DIRECTION.LEFT;
                    }

                    else if (key == ConsoleKey.LeftArrow && changeDir == true)
                    {
                        player.X = Math.Min(player.X + 1, MAP_MAX_X);
                        playerDir = PLAYER_DIRECTION.RIGHT;

                        function--;
                    }
                }

                void MoveUp()
                {
                    if (key == ConsoleKey.UpArrow)
                    {
                        player.Y = Math.Max(player.Y - 1, MAP_MIN_Y + 1);
                        playerDir = PLAYER_DIRECTION.UP;
                    }// 이동 부
                }

                void MoveDown()
                {
                    if (key == ConsoleKey.DownArrow)
                    {
                        player.Y = Math.Min(player.Y + 1, MAP_MAX_Y);
                        playerDir = PLAYER_DIRECTION.DOWN;
                    }
                }

                void WithPlayerBox()
                {
                    for (int boxId = 0; boxId < boxLength; boxId++)
                    {
                        if (player.X == boxes[boxId].X && player.Y == boxes[boxId].Y) // 외곽 벽을 만났을 떄
                        {
                            player.PushedBoxId = boxId;
                            switch (playerDir)
                            {
                                case PLAYER_DIRECTION.RIGHT: //right
                                    player.X = Math.Min(player.X, MAP_MAX_X - 1);
                                    boxes[boxId].X = Math.Min(boxes[boxId].X + 1, MAP_MAX_X);
                                    break;
                                case PLAYER_DIRECTION.LEFT: //left
                                    player.X = Math.Max(player.X, MAP_MIN_X + 2);
                                    boxes[boxId].X = Math.Max(boxes[boxId].X - 1, MAP_MIN_X + 1);
                                    break;
                                case PLAYER_DIRECTION.DOWN: //down
                                    player.Y = Math.Min(player.Y, MAP_MAX_Y - 1);
                                    boxes[boxId].Y = Math.Min(boxes[boxId].Y + 1, MAP_MAX_Y);
                                    break;
                                case PLAYER_DIRECTION.UP: //up
                                    player.Y = Math.Max(player.Y, MAP_MIN_Y + 2);
                                    boxes[boxId].Y = Math.Max(boxes[boxId].Y - 1, MAP_MIN_Y + 1);
                                    break;

                            }
                        }
                    } // 외곽 벽을 만났을 떄
                }

                void WithPlayerWall()
                {
                    for (int wallId = 0; wallId < wallLength; wallId++) //벽과 플레이어
                    {
                        if (player.X == walls[wallId].X && player.Y == walls[wallId].Y)
                        {
                            switch (playerDir)
                            {
                                case PLAYER_DIRECTION.RIGHT: //right
                                    player.X = walls[wallId].X - 1;
                                    break;
                                case PLAYER_DIRECTION.LEFT: //left
                                    player.X = walls[wallId].X + 1;
                                    break;
                                case PLAYER_DIRECTION.DOWN: //down
                                    player.Y = walls[wallId].Y - 1;
                                    break;
                                case PLAYER_DIRECTION.UP: //up
                                    player.Y = walls[wallId].Y + 1;
                                    break;
                            }
                        }
                    } //벽과 플레이어
                }

                void WithBoxWall()
                {
                    for (int boxId = 0; boxId < boxLength; boxId++) //벽과 박스
                    {
                        for (int wallId = 0; wallId < wallLength; wallId++)
                        {
                            if (boxes[boxId].X == walls[wallId].X && boxes[boxId].Y == walls[wallId].Y)
                            {
                                switch (playerDir)
                                {
                                    case PLAYER_DIRECTION.RIGHT: //right
                                        player.X = walls[wallId].X - 2;
                                        boxes[boxId].X = walls[wallId].X - 1;
                                        break;
                                    case PLAYER_DIRECTION.LEFT: //left
                                        player.X = walls[wallId].X + 2;
                                        boxes[boxId].X = walls[wallId].X + 1;
                                        break;
                                    case PLAYER_DIRECTION.DOWN: //down
                                        player.Y = walls[wallId].Y - 2;
                                        boxes[boxId].Y = walls[wallId].Y - 1;
                                        break;
                                    case PLAYER_DIRECTION.UP: //up
                                        player.Y = walls[wallId].Y + 2;
                                        boxes[boxId].Y = walls[wallId].Y + 1;
                                        break;
                                }
                            }
                        }
                    } //벽과 박스
                }

                void WithBoxBox()
                {
                    for (int boxId = 0; boxId < boxLength; boxId++) //박스와 박스 충돌
                    {
                        for (int boxId2 = 0; boxId2 < boxLength; boxId2++)
                        {
                            if (boxId == boxId2)
                            {
                                continue;
                            }
                            if (boxes[boxId].X == boxes[boxId2].X && boxes[boxId].Y == boxes[boxId2].Y && player.PushedBoxId == boxId)
                            {
                                switch (playerDir)
                                {
                                    case PLAYER_DIRECTION.RIGHT: //right
                                        player.X = player.X - 1;
                                        boxes[boxId2].X = player.X + 2;
                                        boxes[boxId].X = player.X + 1;
                                        break;
                                    case PLAYER_DIRECTION.LEFT: //left
                                        player.X = player.X + 1;
                                        boxes[boxId2].X = player.X - 2;
                                        boxes[boxId].X = player.X - 1;
                                        break;
                                    case PLAYER_DIRECTION.DOWN: //down
                                        player.Y = player.Y - 1;
                                        boxes[boxId2].Y = player.Y + 2;
                                        boxes[boxId].Y = player.Y + 1;
                                        break;
                                    case PLAYER_DIRECTION.UP: //up
                                        player.Y = player.Y + 1;
                                        boxes[boxId2].Y = player.Y - 2;
                                        boxes[boxId].Y = player.Y - 1;
                                        break;
                                }
                            }
                        }
                    } //박스와 박스 충돌
                }

                void WithPlayerHitem()
                {
                    for (int itemId = 0; itemId < itemLength; itemId++)
                    {
                        if (player.X == horizonItem[itemId].X && player.Y == horizonItem[itemId].Y)
                        {
                            function += 6;
                            horizonItem[itemId].X = MAP_MAX_X + 2;
                            horizonItem[itemId].Y = MAP_MAX_Y + 2;
                            horizonItem[itemId].Symbol = ' ';
                            changeDir = true;
                        }

                        if (function == 0)
                        {
                            changeDir = false;
                        }
                    }
                   
                }

                void WithPlayerVitem()
                {

                }

                void GoalInJudge()
                {
                    boxCount = 0;

                    for (int boxId = 0; boxId < boxLength; boxId++) // 골인 판정
                    {
                        boxes[boxId].IsOnGoal = false;

                        for (int goalId = 0; goalId < goalLength; goalId++)
                        {
                            if (boxes[boxId].X == goals[goalId].X && boxes[boxId].Y == goals[goalId].Y)
                            {

                                boxCount++;
                                boxes[boxId].IsOnGoal = true;

                                break;
                            }
                        }
                    }
                }
               
                void JudgeClear()
                {
                    if (boxCount == goalLength) // 클리어 판정
                    {
                        Console.Clear();
                        Console.WriteLine("Clear!");
                        clearJudge = false;
                    }
                }

                
            }
        }   
    }
}