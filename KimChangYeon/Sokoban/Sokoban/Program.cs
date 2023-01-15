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
            Console.BackgroundColor = ConsoleColor.DarkGray; 
 
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
                Y = 6,
                PlayerDir = playerDir,
                PushedBoxId = 0,
                Symbol = '●',
                Color = ConsoleColor.White
            };

            Changer changer = new Changer
            {
                X = MAP_MAX_X + 5,
                Y = MAP_MAX_Y ,
                Symbol = '◎',
                Color = ConsoleColor.White
            };

            Box[] boxes = new Box[]
            {
                new Box {X = 22, Y = 14, IsOnGoal = false , Symbol = ' ', Color = ConsoleColor.White},
                new Box {X = 23, Y = 14, IsOnGoal = false , Symbol = ' ', Color = ConsoleColor.DarkGreen},
                new Box {X = 24, Y = 14, IsOnGoal = false , Symbol = ' ', Color = ConsoleColor.DarkYellow},
                new Box {X = 25, Y = 14, IsOnGoal = false , Symbol = ' ', Color = ConsoleColor.Black}
            };

            ColorBox[] colorboxes = new ColorBox[]
            {
                new ColorBox {X = MAP_MAX_X + 6 , Y = MAP_MAX_Y - 1 , Symbol = '■', Color = ConsoleColor.White},
                new ColorBox {X = MAP_MAX_X + 4 , Y = MAP_MAX_Y , Symbol = '■', Color = ConsoleColor.DarkGreen},
                new ColorBox {X = MAP_MAX_X + 6 , Y = MAP_MAX_Y , Symbol = '■', Color = ConsoleColor.DarkYellow},
                new ColorBox {X = MAP_MAX_X + 4 , Y = MAP_MAX_Y - 1, Symbol = '■', Color = ConsoleColor.Black}
            };

            Exit exit = new Exit
            {
                X = 5,
                Y = 6,
                Symbol = '○'
            };

            Wall[] walls = new Wall[]
            {
                new Wall {X = 1 , Y = 2 , Symbol = '#', Color = ConsoleColor.Black},
                new Wall {X = 2 , Y = 2 , Symbol = '#', Color = ConsoleColor.Black},
                new Wall {X = 2 , Y = 1 , Symbol = '#', Color = ConsoleColor.Black},

                new Wall {X = MAP_MAX_X - 2 , Y = 1 , Symbol = '#', Color = ConsoleColor.White},
                new Wall {X = MAP_MAX_X - 1 , Y = 2 , Symbol = '#', Color = ConsoleColor.White},
                new Wall {X = MAP_MAX_X  , Y = 3 , Symbol = '#', Color = ConsoleColor.White},

                new Wall {X = 1 , Y = MAP_MAX_Y - 1 , Symbol = '#', Color = ConsoleColor.DarkGreen},
                new Wall {X = 2 , Y = MAP_MAX_Y , Symbol = '#', Color = ConsoleColor.DarkGreen},
                new Wall {X = 2  , Y = MAP_MAX_Y - 1 , Symbol = '#', Color = ConsoleColor.DarkGreen},

                new Wall {X = exit.X + 1 , Y = exit.Y , Symbol = '#', Color = ConsoleColor.DarkYellow},
                new Wall {X = exit.X - 1 , Y = exit.Y , Symbol = '#', Color = ConsoleColor.DarkYellow},
                new Wall {X = exit.X  , Y = exit.Y - 1 , Symbol = '#', Color = ConsoleColor.DarkYellow},
                new Wall {X = exit.X , Y = exit.Y + 1 , Symbol = '#', Color = ConsoleColor.DarkYellow}
            };

            Goal[] goals = new Goal[]
            {
                new Goal { X = 5 , Y = 2, Symbol = "□" , InSymbol = "▣", Color = ConsoleColor.White},
                new Goal { X = 16 , Y = 10, Symbol = "□" , InSymbol = "▣", Color = ConsoleColor.DarkGreen},
                new Goal { X = 5 , Y = 10, Symbol = "□" , InSymbol = "▣", Color = ConsoleColor.DarkYellow},
                new Goal { X = 16 , Y = 2, Symbol = "□" , InSymbol = "▣", Color = ConsoleColor.Black}
            };

            PointItem[] pointItems = new PointItem[]
            {
                new PointItem {X = 1, Y = 1, Symbol = '*'},
                new PointItem {X = MAP_MAX_X, Y = 1, Symbol = '*'},
                new PointItem {X = 1, Y = MAP_MAX_Y, Symbol = '*'},
                new PointItem {X = player.X - 2, Y = player.Y, Symbol = '*'}
            };

            HorizonItem[] horizonItem = new HorizonItem[]
            {
               new HorizonItem {X = MAP_MAX_X - 1 , Y = 1, Symbol = '↔', Color = ConsoleColor.Red},
               new HorizonItem {X = 1 , Y = MAP_MAX_Y - 2, Symbol = '↔', Color = ConsoleColor.Red},
               new HorizonItem {X = 3 , Y = MAP_MAX_Y, Symbol = '↔', Color = ConsoleColor.Red},
               new HorizonItem {X = player.X - 1 , Y = player.Y, Symbol = '↔', Color = ConsoleColor.Red}
            };

            VerticalItem[] verticalItem = new VerticalItem[]
            {
                new VerticalItem {X = MAP_MAX_X , Y = 2, Symbol = '↕', Color = ConsoleColor.Red},
                new VerticalItem {X = 3 , Y = MAP_MAX_Y, Symbol = '↕', Color = ConsoleColor.Red},
                new VerticalItem {X = 3 , Y = MAP_MAX_Y - 1, Symbol = '↕', Color = ConsoleColor.Red},
                new VerticalItem {X = 2 , Y = MAP_MAX_Y - 2, Symbol = '↕', Color = ConsoleColor.Red}
            };

           


            int boxLength = boxes.Length;
            int wallLength = walls.Length;
            int goalLength = goals.Length;
            int itemLength = horizonItem.Length;
            int colorBoxLength = colorboxes.Length;
            int pItemLength = pointItems.Length;
            int boxCount = default;
            int hFunction = 0;
            int vFunction = 0;
            int point = 0;
            int move = 0;

            bool[] isBoxOnGoal = new bool[boxLength];
            bool clearJudge = true;
            bool hChangeDir = false;
            bool vChangeDir = false;

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
                    RenderExit();
                    ChangeRender();
                    ItemRender();
                    RenderColorBox();
                    ChangerRender();
                    RenderPointItem();
                    PlayerRender();
                    StringRender();
                }

                void MapRender()
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.White;
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
                        Console.ForegroundColor = goals[goalId].Color;
                        Console.SetCursorPosition(goals[goalId].X, goals[goalId].Y);
                        Console.Write(goals[goalId].Symbol);
                    }
                }

                void WallRender()
                {
                    for (int wallId = 0; wallId < wallLength; wallId++)
                    {
                        Console.ForegroundColor = walls[wallId].Color;
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
                            Console.ForegroundColor = boxes[boxId].Color;
                            Console.Write(goals[boxId].InSymbol);
                            
                        }
                        else
                        {
                            Console.ForegroundColor = boxes[boxId].Color;
                            Console.Write(boxes[boxId].Symbol);
                            
                        }
                    }
                    
                }

                void PlayerRender()
                {
                    Console.ForegroundColor = player.Color;
                    Console.SetCursorPosition(player.X, player.Y);
                    Console.Write(player.Symbol);
                }

                void ItemRender()
                {
                    for (int itemId = 0; itemId < itemLength; itemId++)
                    {
                        Console.ForegroundColor = horizonItem[itemId].Color;

                        Console.SetCursorPosition(horizonItem[itemId].X, horizonItem[itemId].Y);
                        Console.Write(horizonItem[itemId].Symbol);

                        Console.ForegroundColor = verticalItem[itemId].Color;

                        Console.SetCursorPosition(verticalItem[itemId].X, verticalItem[itemId].Y);
                        Console.Write(verticalItem[itemId].Symbol);
                    }
                }

                void StringRender()
                {
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.SetCursorPosition(MAP_MAX_X + 4, 5);
                    Console.Write($"↔ : {hFunction}");

                    Console.SetCursorPosition(MAP_MAX_X + 4, 6);
                    Console.Write($"↕ : {vFunction}");

                    Console.SetCursorPosition(MAP_MAX_X + 4, MAP_MAX_Y - 2);
                    Console.Write("CHANGER(WASD)");

                    Console.SetCursorPosition(MAP_MAX_X + 4, 3);
                    Console.Write($"* : {point} / {pItemLength}");

                    Console.SetCursorPosition(MAP_MAX_X + 4, MAP_MAX_Y - 4);
                    Console.Write($"MOVE : {move}");
                }

                void ChangerRender()
                {
                    Console.ForegroundColor = changer.Color;
                    Console.SetCursorPosition(changer.X, changer.Y);
                    Console.Write(changer.Symbol);
                }

                void RenderColorBox()
                {
                    for (int boxId = 0; boxId < colorBoxLength; boxId++)
                    {
                        Console.ForegroundColor = colorboxes[boxId].Color;
                        Console.SetCursorPosition(colorboxes[boxId].X, colorboxes[boxId].Y);
                        Console.Write(colorboxes[boxId].Symbol);
                    }
                }

                void RenderPointItem()
                {
                    for (int itemId = 0; itemId < pItemLength; itemId++)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.SetCursorPosition(pointItems[itemId].X, pointItems[itemId].Y);
                        Console.Write(pointItems[itemId].Symbol);
                    }
                }

                void RenderExit()
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.SetCursorPosition(exit.X, exit.Y);
                    Console.Write(exit.Symbol);
                }

                
                // --------------------------------------------- ProcessInput -------------------------------------------------
                ConsoleKey key = Console.ReadKey().Key;
                // --------------------------------------------- Update -------------------------------------------------------


                MoveRight();
                MoveLeft();
                MoveDown();
                MoveUp();

                SpawnBox();
                WithPlayerBox ();
                WithBoxWall();
                WithPlayerWall();
                WithBoxBox();
                WithPlayerHitem();
                WithPlayerVitem();
                OpenWall();
                ChangePlayerColor();
                AddPoint();

                GoalInJudge();
                JudgeClear();
                

                void MoveRight()
                {
                    if (key == ConsoleKey.RightArrow && hChangeDir == false)
                    {

                        player.X = Math.Min(player.X + 1, MAP_MAX_X);
                        playerDir = PLAYER_DIRECTION.RIGHT;

                        move++;
                    }

                    else if (key == ConsoleKey.RightArrow && hChangeDir == true)
                    {
                        player.X = Math.Max(player.X - 1, MAP_MIN_X + 1);
                        playerDir = PLAYER_DIRECTION.LEFT;

                        hFunction--;
                        move++;
                    }

                    if (key == ConsoleKey.D)
                    {
                        changer.X = Math.Min(changer.X + 1, MAP_MAX_X + 6);
                    }
                }

                void MoveLeft()
                {
                    if (key == ConsoleKey.LeftArrow && hChangeDir == false)
                    {
                        player.X = Math.Max(player.X - 1, MAP_MIN_X + 1);
                        playerDir = PLAYER_DIRECTION.LEFT;

                        move++;
                    }

                    else if (key == ConsoleKey.LeftArrow && hChangeDir == true)
                    {
                        player.X = Math.Min(player.X + 1, MAP_MAX_X);
                        playerDir = PLAYER_DIRECTION.RIGHT;

                        hFunction--;
                        move++;
                    }

                    if (key == ConsoleKey.A)
                    {
                        changer.X = Math.Max(changer.X - 1, MAP_MAX_X + 4);
                    }
                }

                void MoveUp()
                {
                    if (key == ConsoleKey.UpArrow && vChangeDir == false)
                    {
                        player.Y = Math.Max(player.Y - 1, MAP_MIN_Y + 1);
                        playerDir = PLAYER_DIRECTION.UP;

                        move++;
                    }
                    
                    else if (key == ConsoleKey.UpArrow && vChangeDir == true)
                    {
                        player.Y = Math.Min(player.Y + 1, MAP_MAX_Y);
                        playerDir = PLAYER_DIRECTION.DOWN;

                        vFunction--;
                        move++;
                    }

                    if (key == ConsoleKey.W)
                    {
                        changer.Y = Math.Max(changer.Y - 1, MAP_MAX_Y - 1);
                    }
                }

                void MoveDown()
                {
                    if (key == ConsoleKey.DownArrow && vChangeDir == false)
                    {
                        player.Y = Math.Min(player.Y + 1, MAP_MAX_Y);
                        playerDir = PLAYER_DIRECTION.DOWN;

                        move++;
                    }

                    else if (key == ConsoleKey.DownArrow && vChangeDir == true)
                    {
                        player.Y = Math.Max(player.Y - 1, MAP_MIN_Y + 1);
                        playerDir = PLAYER_DIRECTION.UP;

                        vFunction--;
                        move++;
                    }

                    if (key == ConsoleKey.S)
                    {
                        changer.Y = Math.Min(changer.Y + 1, MAP_MAX_Y);
                    }
                }

                void SpawnBox()
                {
                    for (int pItemId = 0; pItemId < pItemLength; pItemId++)
                    {
                        if (player.X == pointItems[pItemId].X && player.Y == pointItems[pItemId].Y)
                        {
                            boxes[pItemId].X = 10;
                            boxes[pItemId].Y = 6;
                            boxes[pItemId].Symbol = '■';

                            break;
                        }
                    }
                }

                void WithPlayerBox()
                {
                    for (int boxId = 0; boxId < boxLength; boxId++)
                    {
                        if (player.X == boxes[boxId].X && player.Y == boxes[boxId].Y && player.Color == boxes[boxId].Color) // 외곽 벽을 만났을 떄
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
                        if (player.X == boxes[boxId].X && player.Y == boxes[boxId].Y && !(player.Color == boxes[boxId].Color))
                        {
                            switch (playerDir)
                            {
                                case PLAYER_DIRECTION.RIGHT: //right
                                    player.X = boxes[boxId].X - 1;
                                    break;
                                case PLAYER_DIRECTION.LEFT: //left
                                    player.X = boxes[boxId].X + 1;
                                    break;
                                case PLAYER_DIRECTION.DOWN: //down
                                    player.Y = boxes[boxId].Y - 1;
                                    break;
                                case PLAYER_DIRECTION.UP: //up
                                    player.Y = boxes[boxId].Y + 1;
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
                            hFunction += 10;
                            horizonItem[itemId].X = MAP_MAX_X + 2;
                            horizonItem[itemId].Y = MAP_MAX_Y + 2;
                            horizonItem[itemId].Symbol = ' ';
                            hChangeDir = true;

                            break;
                        }

                        if (hFunction == 0)
                        {
                            hChangeDir = false;
                        }
                    }
                   
                }

                void WithPlayerVitem()
                {
                    for (int itemId = 0; itemId < itemLength; itemId++)
                    {
                        if (player.X == verticalItem[itemId].X && player.Y == verticalItem[itemId].Y)
                        {
                            vFunction += 10;  //효과 횟수
                            verticalItem[itemId].X = MAP_MAX_X + 2;
                            verticalItem[itemId].Y = MAP_MAX_Y + 2;
                            verticalItem[itemId].Symbol = ' ';
                            vChangeDir = true;

                            break;
                        }

                        if (vFunction == 0)
                        {
                            vChangeDir = false;
                        }
                    }
                }

                void ChangePlayerColor()
                {
                    for (int boxId = 0; boxId < colorBoxLength; boxId++)
                    {
                        if (changer.X == colorboxes[boxId].X && changer.Y == colorboxes[boxId].Y)
                        {
                            changer.Color = ConsoleColor.Red;
                            player.Color = colorboxes[boxId].Color;
                            
                            break;
                        }
                        else
                        {
                            changer.Color = ConsoleColor.Black;
                        }
                    }
                }

                void AddPoint()
                {
                    for (int pItemId = 0; pItemId < pItemLength; pItemId++)
                    {
                        if (player.X == pointItems[pItemId].X && player.Y == pointItems[pItemId].Y)
                        {
                            point++;
                            pointItems[pItemId].X = MAP_MAX_X + 4;
                            pointItems[pItemId].Y = MAP_MAX_Y + 4;
                            pointItems[pItemId].Symbol = ' ';

                            break;

                        }
                    }
                }

                void OpenWall()
                {
                    for (int goalId = 0; goalId < goalLength; goalId++)
                    {
                        for (int wallId = 0; wallId < wallLength; wallId++)
                        {
                            if (boxes[goalId].IsOnGoal == true && walls[wallId].Color == goals[goalId].Color)
                            {
                                walls[wallId].X = MAP_MAX_X + 5;
                                walls[wallId].Y = MAP_MAX_Y + 5;
                                walls[wallId].Symbol = ' ';
                                
                            }
                            
                        }
                    }
                }
                
                void GoalInJudge()
                {
                    for (int boxId = 0; boxId < boxLength; boxId++) // 골인 판정
                    {
                        boxes[boxId].IsOnGoal = false;

                        for (int goalId = 0; goalId < goalLength; goalId++)
                        {

                            if (boxes[boxId].X == goals[goalId].X && boxes[boxId].Y == goals[goalId].Y && boxes[boxId].Color == goals[goalId].Color)
                            {

                                boxes[boxId].IsOnGoal = true;
                            }

                            else if (boxes[boxId].X == goals[goalId].X && boxes[boxId].Y == goals[goalId].Y && boxes[boxId].Color != goals[goalId].Color)
                            {

                                switch (playerDir)
                                {

                                    case PLAYER_DIRECTION.RIGHT: //right
                                        player.X = player.X - 1;
                                        boxes[boxId].X = boxes[boxId].X - 1;
                                        break;
                                    case PLAYER_DIRECTION.LEFT: //left
                                        player.X = player.X + 1;
                                        boxes[boxId].X = boxes[boxId].X + 1;
                                        break;
                                    case PLAYER_DIRECTION.DOWN: //down
                                        player.Y = player.Y - 1;
                                        boxes[boxId].Y = boxes[boxId].Y - 1;
                                        break;
                                    case PLAYER_DIRECTION.UP: //up
                                        player.Y = player.Y + 1;
                                        boxes[boxId].Y = boxes[boxId].Y + 1;
                                        break;
                                }
                            }
                            
                        }
                    }
                }
               
                void JudgeClear()
                {
                    if (player.X == exit.X && player.Y == exit.Y) // 클리어 판정
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
