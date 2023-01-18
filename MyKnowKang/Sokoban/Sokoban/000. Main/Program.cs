using Sokoban;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Serialization;
using static Sokoban.Map;

namespace KMH_Sokoban
{
    class Program
    {
        static void Main()
        {
            // ------------------------------------------- 초기화(객체 생성 및 초기화).. -------------------------------------------
            #region Initialize

            // Game 객체 생성 및 기본 초기화 작업..
            Game game = new Game();

            game.Initialize();  // 이 때 윈도우 환경 결정 및 콘솔 초기화 작업을 한다..

            // ============================================================================================================================================
            // 상수 초기화..
            // ============================================================================================================================================
            #region 상수 초기화

            // Player 관련 상수 설정..
            const string PLAYER_IMAGE_WIN11 = "♀";
            const string PLAYER_IMAGE_WIN10 = "P";
            const ConsoleColor PLAYER_COLOR = ConsoleColor.Blue;
            // Player 초기 세팅..
            const int INITIAL_PLAYER_X = 1;
            const int INITIAL_PLAYER_Y = 1;
            const int INITIAL_PLAYER_HP = 10;
            const int INITIAL_PLAYER_MP = 500;
            string initPlayerImage = (game.IsWin11) ? PLAYER_IMAGE_WIN11 : PLAYER_IMAGE_WIN10;

            // Box 관련 상수 설정.. 
            const int BOX_COUNT = 5;
            const string BOX_IMAGE_WIN11 = "◎";
            const string BOX_IMAGE_WIN10 = "B";
            const ConsoleColor BOX_COLOR = ConsoleColor.Yellow;
            // Box 초기 세팅..
            int[] INIT_BOXES_X = new int[BOX_COUNT] { 5, 5, 5, 5, 10 };
            int[] INIT_BOXES_Y = new int[BOX_COUNT] { 2, 3, 4, 5, 7 };
            string initBoxImage = (game.IsWin11) ? BOX_IMAGE_WIN11 : BOX_IMAGE_WIN10;

            // Wall 관련 상수 설정..
            const int WALL_COUNT = 15;
            const string WALL_IMAGE_WIN11 = "▣";
            const string WALL_IMAGE_WIN10 = "W";
            const ConsoleColor WALL_COLOR = ConsoleColor.DarkRed;
            // Wall 초기 세팅..
            int[] INIT_WALLS_X = new int[WALL_COUNT] { 3, 3, 3, 42, 42, 42, 42, 42, 43, 44, 45, 46, 47, 48, 49 };
            int[] INIT_WALLS_Y = new int[WALL_COUNT] { 8, 7, 9, 18, 19, 20, 21, 17, 17, 17, 17, 17, 17, 17, 17 };
            string initWallImage = (game.IsWin11) ? WALL_IMAGE_WIN11 : WALL_IMAGE_WIN10;

            // Goal 지점 관련 상수..
            const int GOAL_COUNT = BOX_COUNT;
            const string GOAL_IMAGE_WIN11 = "∏";
            const string GOAL_IMAGE_WIN10 = "G";
            const ConsoleColor GOAL_COLOR = ConsoleColor.Gray;
            const ConsoleColor GOALIN_COLOR = ConsoleColor.DarkGray;
            // Goal 초기 세팅..
            int[] INIT_GOALS_X = new int[GOAL_COUNT] { 49, 49, 49, 49, 49 };
            int[] INIT_GOALS_Y = new int[GOAL_COUNT] { 18, 19, 20, 21, 7 };
            string initGoalImage = (game.IsWin11) ? GOAL_IMAGE_WIN11 : GOAL_IMAGE_WIN10;

            // Portal 관련 상수 설정..
            const int PORTAL_GATE_COUNT = 2;    // 한 개의 포탈에 이동할 수 있는 게이트의 개수??( 입구 <-> 출구 로 이동하니까 2개 )
            const int PORTAL_COUNT = 8;
            const string PORTAL_IMAGE_WIN11 = "Ⅱ";
            const string PORTAL_IMAGE_WIN10 = "X";
            // Portal 초기 세팅..
            int[,] INIT_PORTALGATE_X = new int[PORTAL_COUNT, 2]
            { { 35, 5 }, { 35, 5 }, { 35, 5 }, { 35, 5 }, { 4, 40 }, { 10, 20 }, { 20, 30 }, { 30, 10 } };
            int[,] INIT_PORTALGATE_Y = new int[PORTAL_COUNT, 2]
            { { 2, 18 }, { 3, 19 }, { 4, 20 }, { 5, 21 }, { 6, 16 }, { 1, 12 }, { 1, 12 }, { 1, 12 } };
            string initPortalImage = (game.IsWin11) ? PORTAL_IMAGE_WIN11 : PORTAL_IMAGE_WIN10;
            ConsoleColor[] INIT_PORTAL_COLOR = new ConsoleColor[PORTAL_COUNT]
            {
                ConsoleColor.Green, ConsoleColor.DarkMagenta, ConsoleColor.Gray, ConsoleColor.Blue, ConsoleColor.Yellow
                , ConsoleColor.Green, ConsoleColor.Gray, ConsoleColor.Blue
            };

            // Switch 관련 상수 설정..
            const int SWITCH_COUNT = 1;
            const ConsoleColor SWITCH_COLOR = ConsoleColor.DarkMagenta;
            const string SWITCH_IMAGE = "ㅣ";
            const string SWITCH_PUSH_IMAGE = "*";
            // Switch 초기 세팅..
            int[] INIT_SWITCHES_X = new int[SWITCH_COUNT] { 41 };
            int[] INIT_SWITCHES_Y = new int[SWITCH_COUNT] { 17 };
            int[] INIT_SWITCHESBUTTON_OFFSETX = new int[SWITCH_COUNT] { -1 };
            int[] INIT_SWITCHESBUTTON_OFFSETY = new int[SWITCH_COUNT] { 0 };
            // 스위치 누르거나 땔 때 열거나 닫는 벽 인덱스..
            int[][] INIT_OPENCLOSE_WALL_INDEX = new int[SWITCH_COUNT][];
            INIT_OPENCLOSE_WALL_INDEX[0] = new int[4] { 3, 4, 5, 6 };

            // Trap 관련 상수 설정..
            const int TRAP_COUNT = 4;
            const ConsoleColor TRAP_COLOR = ConsoleColor.DarkMagenta;
            const string TRAP_IMAGE_WIN11 = "▒";
            const string TRAP_IMAGE_WIN10 = "Y";
            // Trap 초기 세팅..
            string initTrapImage = (game.IsWin11) ? TRAP_IMAGE_WIN11 : TRAP_IMAGE_WIN10;

            // Arrow 관련 상수 설정..
            const ConsoleColor ARROW_COLOR = ConsoleColor.White;
            const string ARROW_IMAGE_WIN11 = "→←↑↓";
            const string ARROW_IMAGE_WIN10 = "→←↑↓";
            // Arrow 초기 세팅..
            string initArrowImage = (game.IsWin11) ? ARROW_IMAGE_WIN11 : ARROW_IMAGE_WIN10;

            // Item 관련 상수 설정..
            const int ITEM_COUNT = 4;
            ConsoleColor[] ITEM_COLOR = new ConsoleColor[Item.ITEM_TYPE_COUNT]
            {
                ConsoleColor.DarkMagenta, ConsoleColor.Green, ConsoleColor.Red, ConsoleColor.Blue
            };
            string[] ITEM_IMAGE_WIN11 = new string[Item.ITEM_TYPE_COUNT]
            {
                "®", "┼", "☏", "☏"
            };
            string[] ITEM_IMAGE_WIN10 = new string[Item.ITEM_TYPE_COUNT]
            {
                "R", "E", "B", "B"
            };
            // Item 초기 세팅..
            string[] initItemImage = new string[Item.ITEM_TYPE_COUNT];
            for ( int index = 0; index < Item.ITEM_TYPE_COUNT; ++index )
            {
                string win11Image = ITEM_IMAGE_WIN11[index];
                string win10Image = ITEM_IMAGE_WIN10[index];

                initItemImage[index] = (game.IsWin11) ? win11Image : win10Image;
            }
            #endregion

            // ============================================================================================================================================
            // 변수 초기화..
            // ============================================================================================================================================
            #region 변수 초기화
            // 플레이어 관련 변수 설정..
            Player player = new Player
            {
                X = INITIAL_PLAYER_X, Y = INITIAL_PLAYER_Y, PrevX = INITIAL_PLAYER_X, PrevY = INITIAL_PLAYER_Y,
                Image = initPlayerImage, Color = PLAYER_COLOR, MaxHp = INITIAL_PLAYER_HP, MaxMp = INITIAL_PLAYER_MP,
                CurHp = INITIAL_PLAYER_HP, CurMp = INITIAL_PLAYER_MP
            };


            // 박스 관련 변수 설정..
            Box[] boxes = new Box[BOX_COUNT];
            for ( int boxIndex = 0; boxIndex < BOX_COUNT; ++boxIndex )
            {
                boxes[boxIndex] = new Box
                {
                    X = INIT_BOXES_X[boxIndex], Y = INIT_BOXES_Y[boxIndex], PrevX = INIT_BOXES_X[boxIndex], PrevY = INIT_BOXES_Y[boxIndex],
                    Image = initBoxImage, Color = BOX_COLOR, CurState = Box.State.Idle, DirX = 0, DirY = 0
                };
            }


            // 맵 관련 변수 설정..
            // 맵의 각 위치들의 데이터를 저장하는 룩업 테이블..
            Map map = new Map( Game.MAP_RANGE_MIN_X, Game.MAP_RANGE_MIN_Y, Game.MAP_RANGE_MAX_X, Game.MAP_RANGE_MAX_Y,
                game.InitBorderLineImage, Game.BORDERLINE_COLOR );


            // 벽 관련 변수 설정..
            Wall[] walls = new Wall[WALL_COUNT];
            for ( int wallIndex = 0; wallIndex < WALL_COUNT; ++wallIndex )
            {
                walls[wallIndex] = new Wall
                {
                    X = INIT_WALLS_X[wallIndex], Y = INIT_WALLS_Y[wallIndex],
                    Image = initWallImage, Color = WALL_COLOR, IsActive = true, IsRender = true
                };
            }


            // 골인 지점 관련 변수 설정..
            Goal[] goals = new Goal[GOAL_COUNT];
            for ( int goalIndex = 0; goalIndex < GOAL_COUNT; ++goalIndex )
            {
                goals[goalIndex] = new Goal
                {
                    X = INIT_GOALS_X[goalIndex], Y = INIT_GOALS_Y[goalIndex], Image = initGoalImage, Color = GOAL_COLOR,
                    GoalInColor = GOALIN_COLOR, IsGoalIn = false
                };
            }


            // Portal 관련 변수 설정..
            Portal[] portals = new Portal[PORTAL_COUNT];
            for ( int portalIndex = 0; portalIndex < PORTAL_COUNT; ++portalIndex )
            {
                portals[portalIndex] = new Portal
                {
                    GatesX = new int[PORTAL_GATE_COUNT], GatesY = new int[PORTAL_GATE_COUNT], Image = initPortalImage, Color = INIT_PORTAL_COLOR[portalIndex]
                };

                for ( int gateIndex = 0; gateIndex < PORTAL_GATE_COUNT; ++gateIndex )
                {
                    portals[portalIndex].GatesX[gateIndex] = INIT_PORTALGATE_X[portalIndex, gateIndex];
                    portals[portalIndex].GatesY[gateIndex] = INIT_PORTALGATE_Y[portalIndex, gateIndex];
                }
            }


            // Switch 관련 변수 설정..
            Sokoban.Switch[] switches = new Sokoban.Switch[SWITCH_COUNT];
            for ( int switchIndex = 0; switchIndex < SWITCH_COUNT; ++switchIndex )
            {
                int loopCount = INIT_OPENCLOSE_WALL_INDEX[switchIndex].Length;

                switches[switchIndex] = new Sokoban.Switch
                {
                    X = INIT_SWITCHES_X[switchIndex], Y = INIT_SWITCHES_Y[switchIndex], ButtonOffsetX = INIT_SWITCHESBUTTON_OFFSETX[switchIndex],
                    ButtonOffsetY = INIT_SWITCHESBUTTON_OFFSETY[switchIndex], IsHolding = false, OpenCloseWallIndex = new int[loopCount],
                    SwitchImage = SWITCH_IMAGE, SwitchColor = SWITCH_COLOR, PusherImage = SWITCH_PUSH_IMAGE, PusherColor = SWITCH_COLOR
                };

                for ( int openCloseWallIndex = 0; openCloseWallIndex < loopCount; ++openCloseWallIndex )
                {
                    switches[switchIndex].OpenCloseWallIndex[openCloseWallIndex] = INIT_OPENCLOSE_WALL_INDEX[switchIndex][openCloseWallIndex];
                }
            }


            // Trap 관련 변수 설정..
            Trap[] traps = new Trap[TRAP_COUNT];
            int trapIndexTemp = 0;
            traps[trapIndexTemp++] = new BombTrap
            {
                X = 15, Y = 10, Damage = 5, Image = initTrapImage, Color = TRAP_COLOR, BurstRange = 5
                , MyType = Trap.TrapType.Bomb
            };
            traps[trapIndexTemp++] = new BombTrap
            {
                X = 4, Y = 2, Damage = 5, Image = initTrapImage, Color = TRAP_COLOR, BurstRange = 5
                , MyType = Trap.TrapType.Bomb
            };
            traps[trapIndexTemp++] = new TriggerTrap
            {
                X = 15, Y = 7, Image = initTrapImage, Color = TRAP_COLOR, MyType = Trap.TrapType.Trigger
            };
            traps[trapIndexTemp++] = new TriggerTrap
            {
                X = 32, Y = 2, Image = initTrapImage, Color = TRAP_COLOR, MyType = Trap.TrapType.Trigger
            };

            for ( int trapIndex = 2; trapIndex < TRAP_COUNT; ++trapIndex )
            {
                ((TriggerTrap)traps[trapIndex]).CreateSpawnObjectArray( Game.MAP_RANGE_MAX_Y - 1 );
                for ( int i = 0; i < Game.MAP_RANGE_MAX_Y - 1; ++i )
                {
                    TriggerTrap curTrap = (TriggerTrap)traps[trapIndex];
                    curTrap.SpawnObjectsX[i] = Game.MAP_RANGE_MAX_X - 2;
                    curTrap.SpawnObjectsY[i] = i + 1;
                    curTrap.SpawnObjectsDirX[i] = -1;
                    curTrap.SpawnObjectsDirY[i] = 0;
                }
            }


            // Arrow 관련 변수 설정..
            List<Arrow> arrows = new List<Arrow>();
            List<Arrow> removeArrows = new List<Arrow>();


            // Item 관련 변수 설정..
            Item[] items = 
            {
                new Item { X = 10, Y = 10, Effect = 0, Duration = 10, type = Item.Type.ReverseMove, isActive = true },
                new Item { X = 10, Y = 2, Effect = 1, Duration = 1, type = Item.Type.EasterEgg, isActive = true },
                new Item { X = 10, Y = 6, Effect = 5, Duration = 1, type = Item.Type.HPPosion, isActive = true },
                new Item { X = 20, Y = 7, Effect = 5, Duration = 1, type = Item.Type.MPPosion, isActive = true }
            };

            // 현재 타입에 따라 Image 와 Color 결정..
            for ( int itemIndex = 0; itemIndex < ITEM_COUNT; ++itemIndex )
            {
                int itemType = (int)(items[itemIndex].type);
                items[itemIndex].Image = initItemImage[itemType];
                items[itemIndex].Color = ITEM_COLOR[itemType];
            }
            // 플레이어가 사용중인 아이템 관련..
            int[] playerActiveItemIndex = new int[ITEM_COUNT];
            int activeItemCount = 0;

            #region 기타 변/상수 설정

            // 타이머 관련..
            const int FRAME_PER_SECOND = 20;
            Sokoban.Timer timer = new Sokoban.Timer( FRAME_PER_SECOND );
            // 렌더 관련( 그릴지 말지 )..
            bool isSkipRender = false;
            bool isConsoleClear = false;

            #endregion

            #endregion

            #region 시작 전 초기 작업

            // 시작 전에 맵 데이터에 플레이어 박스 위치 저장..
            Action updateMapSpace = () =>
            {
                // 플레이어 정보 갱신..
                map.ChangeSpaceType( player.PrevX, player.PrevY, Map.SpaceType.Pass );
                map.ChangeSpaceType( player.X, player.Y, Map.SpaceType.PlayerStand );

                // Box 정보 갱신..
                for ( int boxIndex = 0; boxIndex < BOX_COUNT; ++boxIndex )
                {
                    map.ChangeSpaceType( boxes[boxIndex].PrevX, boxes[boxIndex].PrevY, Map.SpaceType.Pass );
                    map.ChangeSpaceType( boxes[boxIndex].X, boxes[boxIndex].Y, Map.SpaceType.BoxStand );
                }

				// Portal 정보 갱신..
				for ( int portalIdx = 0; portalIdx < PORTAL_COUNT; ++portalIdx )
                {
                    for ( int gateIndex = 0; gateIndex < PORTAL_GATE_COUNT; ++gateIndex )
                    {
                        int curPortalX = portals[portalIdx].GatesX[gateIndex];
                        int curPortalY = portals[portalIdx].GatesY[gateIndex];

                        map.ChangeSpaceType( curPortalX, curPortalY, Map.SpaceType.Portal );
                    }
                }

				// 벽 정보 갱신..
				for ( int wallIdx = 0; wallIdx < WALL_COUNT; ++wallIdx )
                {
                    int wallX = walls[wallIdx].X;
                    int wallY = walls[wallIdx].Y;
                    Map.SpaceType changeSpaceType = map.GetCurStandSpaceType( wallX, wallY );

                    if ( true == walls[wallIdx].IsActive )
                    {
                        changeSpaceType = Map.SpaceType.DontPass;
                    }

                    map.ChangeSpaceType( wallX, wallY, changeSpaceType );
                }

				// Switch 정보 갱신..
				for ( int switchIndex = 0; switchIndex < SWITCH_COUNT; ++switchIndex )
                {
                    int switchX = switches[switchIndex].X;
                    int switchY = switches[switchIndex].Y;

                    map.ChangeSpaceType( switchX, switchY, Map.SpaceType.DontPass );
                }

				// Item 정보 갱신..
				for( int itemIndex = 0; itemIndex < ITEM_COUNT; ++itemIndex )
                {
                    int itemX = items[itemIndex].X;
                    int itemY = items[itemIndex].Y;
                    Map.SpaceType changeSpaceType = map.GetCurStandSpaceType( itemX, itemY );

                    if ( true == items[itemIndex].isActive )
                    {
                        changeSpaceType = Map.SpaceType.Item;
                    }

                    map.ChangeSpaceType( itemX, itemY, changeSpaceType );
                }

                // Trap 정보 갱신..
                for ( int trapIndex = 0; trapIndex < TRAP_COUNT; ++trapIndex )
                {
                    if ( traps[trapIndex].IsDestroy )
                    {
                        continue;
                    }

                    int trapX = traps[trapIndex].X;
                    int trapY = traps[trapIndex].Y;
                    Map.SpaceType changeSpaceType = Map.SpaceType.Trap;

                    map.ChangeSpaceType( trapX, trapY, changeSpaceType );
                }

                // Arrow 정보 갱신..
                for ( int arrowIndex = 0; arrowIndex < arrows.Count; ++arrowIndex )
                {
                    map.ChangeSpaceType( arrows[arrowIndex].PrevX, arrows[arrowIndex].PrevY, Map.SpaceType.Pass );
                    map.ChangeSpaceType( arrows[arrowIndex].X, arrows[arrowIndex].Y, Map.SpaceType.Arrow );
                }

                foreach ( var arrow in removeArrows )
                {
                    map.ChangeSpaceType( arrow.PrevX, arrow.PrevY, Map.SpaceType.Pass );
                }
            };

            map.Update( updateMapSpace );

            // 맵 외곽 통과 못하는 곳으로 설정..
            for ( int posX = 0; posX <= Game.MAP_WIDTH; ++posX )
            {
                map.ChangeSpaceType( posX, 0, Map.SpaceType.DontPass );
                map.ChangeSpaceType( posX, Game.MAP_HEIGHT, Map.SpaceType.DontPass );
            }
            for ( int posY = 0; posY <= Game.MAP_HEIGHT; ++posY )
            {
                map.ChangeSpaceType( 0, posY, Map.SpaceType.DontPass );
                map.ChangeSpaceType( Game.MAP_WIDTH, posY, Map.SpaceType.DontPass );
            }

            // 설명용 텍스트 설정..
            const int LOG_COUNT = 3;
            const int INFO_LOG_INDEX = 0;
            const int PLAYER_STATE_LOG_INDEX = 1;
            const int PLAYER_STATE_CLEAR_LOG_INDEX = 2;
            const int logStartX = 65;
            const int logStartY = 0;
            const int playerStateLogStartX = 0;
            const int playerStateLogStartY = Game.MAP_RANGE_MAX_Y + 2;

            Log[] logs = new Log[LOG_COUNT]
            {
                new Log( logStartX, logStartY ),
                new Log( playerStateLogStartX, playerStateLogStartY ),
                new Log( playerStateLogStartX, playerStateLogStartY )
            };

            logs[INFO_LOG_INDEX].AddLogMessage( 2, "============== 설명 ==============" );
            logs[INFO_LOG_INDEX].AddLogMessage( 1, $"{initPlayerImage} : 플레이어" );
            logs[INFO_LOG_INDEX].AddLogMessage( 1, $"{initBoxImage} : 박스" );
            logs[INFO_LOG_INDEX].AddLogMessage( 1, $"{initWallImage} : 벽" );
            logs[INFO_LOG_INDEX].AddLogMessage( 1, $"{initGoalImage} : 골" );
            logs[INFO_LOG_INDEX].AddLogMessage( 1, $"{SWITCH_IMAGE} : 스위치, {SWITCH_PUSH_IMAGE} : 버튼" );

            logs[INFO_LOG_INDEX].AddLogMessage( 2, "" );
            logs[INFO_LOG_INDEX].AddLogMessage( 2, "============ 아이템 설명 ============" );
            logs[INFO_LOG_INDEX].AddLogMessage( 1, $"반대로 움직임(10턴) : {initItemImage[(int)(Item.Type.ReverseMove)]}" );
            logs[INFO_LOG_INDEX].AddLogMessage( 1, $"이스터 에그 : {initItemImage[(int)(Item.Type.EasterEgg)]}" );
            logs[INFO_LOG_INDEX].AddLogMessage( 1, $"HP 포션 : {initItemImage[(int)(Item.Type.HPPosion)]}" );
            logs[INFO_LOG_INDEX].AddLogMessage( 1, $"MP 포션 : {initItemImage[(int)(Item.Type.MPPosion)]}" );

            logs[INFO_LOG_INDEX].AddLogMessage( 2, "" );
            logs[INFO_LOG_INDEX].AddLogMessage( 2, "======== 방향키 ========" );
            logs[INFO_LOG_INDEX].AddLogMessage( 1, "↑ ← ↓ → : 이동" );
            logs[INFO_LOG_INDEX].AddLogMessage( 1, "SpaceBar : 박스 잡기" );
            logs[INFO_LOG_INDEX].AddLogMessage( 1, "A : 박스 차기" );
            logs[INFO_LOG_INDEX].AddLogMessage( 1, "" );

            for ( int i = 0; i < 5; ++i )
            {
                logs[PLAYER_STATE_CLEAR_LOG_INDEX].AddLogMessage( 1, "                               " );
            }

            #endregion
            #endregion

            #region GameLoop
            while ( true )
            {
                // 실행 시간 계산..
                bool isCanStartGameUpdate = timer.Update();

                // 현재 실행 시간 로그 업데이트..
                logs[INFO_LOG_INDEX].RemoveLast();
                logs[INFO_LOG_INDEX].AddLogMessage( 1, $"실행 시간 : {timer.RunTime:F3}" );

                // Player 의 State 갱신..
                logs[PLAYER_STATE_LOG_INDEX].Clear();
                logs[PLAYER_STATE_LOG_INDEX].AddLogMessage( 1, "========== Player State ==========" );

                if ( isCanStartGameUpdate ) // 현재 지나간 시간이 Frame 간격보다 클 때 실행..
                {
                    // --------------------------------------------------------------- ProcessInput.. ---------------------------------------------------------------
                    // 입력한 키 가져오기..
                    ConsoleKey inputKey = ConsoleKey.NoName;
                    if ( Console.KeyAvailable )
                    {
                        inputKey = Console.ReadKey().Key;
                        isSkipRender = false;
                    }

                    if ( Game.EndingType.None == game.GameEndingType )
                        Update( inputKey );

                    // =========================================== Check Game Clear.. =========================================== //
                    // 게임이 끝났는지 검사..
                    if ( true == game.CheckGameEnding( ref goals, in boxes, in player, in timer ) )
                    {
                        break;
                    }

                    Render();
                }
            }
            #endregion

            // 게임이 종료되었으니 엔딩 종류에 따라 출력한다..
            game.PrintEndingMessage();

            #region Render Function

            /// <summary>
            /// 한 프레임을 그린다..
            /// </summary>
            void Render()
            {
                // 이전 프레임 지우기..
                if ( isConsoleClear )
                {
                    Console.Clear();
                    isConsoleClear = false;
                }

                // Log Render..
                logs[INFO_LOG_INDEX].Render();

                if ( isSkipRender ) // Render 를 스킵해야 한다면( 무한 깜빡임 방지 )..
                {
                    return;
                }

                isSkipRender = true;

                // Player State Render..
                logs[PLAYER_STATE_LOG_INDEX].AddLogMessage( 1, $"HP : {player.CurHp} / {player.MaxHp}" );
                logs[PLAYER_STATE_LOG_INDEX].AddLogMessage( 1, $"MP : {player.CurMp} / {player.MaxMp}" );

                logs[PLAYER_STATE_CLEAR_LOG_INDEX].Render();
                logs[PLAYER_STATE_LOG_INDEX].Render();

                logs[PLAYER_STATE_LOG_INDEX].Clear();

                // Render Switch..
                for ( int switchIndex = 0; switchIndex < SWITCH_COUNT; ++switchIndex )
                {
                    switches[switchIndex].Render();
                }

                // Render Portal..
                for ( int portalIndex = 0; portalIndex < PORTAL_COUNT; ++portalIndex )
                {
                    portals[portalIndex].Render();
                }

                // Render Wall..
                for ( int wallIndex = 0; wallIndex < WALL_COUNT; ++wallIndex )
                {
                    walls[wallIndex].Render();
                }


                // Render Arrow..
                foreach ( var arrow in arrows )
                {
                    arrow.Render();
                }

                foreach ( var arrow in removeArrows )
                {
                    Renderer.Render( arrow.PrevX, arrow.PrevY, " ", ConsoleColor.Black );
                }
                removeArrows.Clear();


                // Render Trap..
                for ( int trapIndex = 0; trapIndex < TRAP_COUNT; ++trapIndex )
                {
                    if ( traps[trapIndex].IsActive )
                    {
                        switch ( traps[trapIndex].MyType )
                        {
                            case Trap.TrapType.Bomb:
                                // 폭발 범위만큼 그린다..
                                BombTrap curTrap = (BombTrap)traps[trapIndex];

                                curTrap.Render();

                                break;
                            case Trap.TrapType.Trigger:

                                break;
                        }
                    }
                }

                // Render Item..
                for ( int itemIndex = 0; itemIndex < ITEM_COUNT; ++itemIndex )
                {
                    items[itemIndex].Render();
                }

                // Render Box..
                for ( int boxIndex = 0; boxIndex < BOX_COUNT; ++boxIndex )
                {
                    boxes[boxIndex].Render();
                }

                // Render Player..
                player.Render( map.GetCurStandSpaceType( player.PrevX, player.PrevY ) );

                // Render Goal..
                for ( int goalIndex = 0; goalIndex < GOAL_COUNT; ++goalIndex )
                {
                    goals[goalIndex].Render();
                }

                // Render Map BorderLine..
                map.Render();
            }

            #endregion

            /// <summary>
			/// 한 프레임의 업데이트..
			/// </summary>
            void Update( ConsoleKey inputKey )
            {
                #region Update
                // ------------------------------------------------------------------ Update.. ------------------------------------------------------------------
                // ================================= Player Update.. =================================
                player.Update( inputKey, boxes );

                // ================================= Item Update.. =================================
                for ( int index = 0; index < activeItemCount; ++index )
                {
                    int itemIndex = playerActiveItemIndex[index];

                    if ( -1 != items[itemIndex].Update( ref player ) )
                    {
                        isSkipRender = false;
                    }
                }

                // ================================= Box Update.. =================================
                for ( int i = 0; i < BOX_COUNT; ++i )
                {
                    if ( 1 == boxes[i].Update( in player ) )
                    {
                        isSkipRender = false;
                    }
                }

                #region Trap Update

                for ( int trapIndex = 0; trapIndex < TRAP_COUNT; ++trapIndex )
                {
                    if ( traps[trapIndex].IsBurst )
                    {
                        isSkipRender = false;
                        isConsoleClear = true;

                        switch ( traps[trapIndex].MyType )
                        {
                            case Trap.TrapType.Bomb:
                                {
                                    BombTrap curTrap = (BombTrap)traps[trapIndex];

                                    curTrap.Update( ref player );
                                }

                                break;

                            case Trap.TrapType.Trigger:
                                {
                                    TriggerTrap curTrap = (TriggerTrap)traps[trapIndex];

                                    curTrap.SpawnObject( ref player, ref arrows, initArrowImage, ARROW_COLOR );
                                }

                                break;
                        }
                    }
                }

                #endregion

                // ================================= Arrow Update.. =================================
                foreach ( var arrow in arrows )
                {
                    arrow.Update();

                    isSkipRender = false;
                }

                // ================================= Collision Update.. =================================
                #region Box Collision

                // 박스가 특정 물체와 겹쳤다( 박스 or 벽 or 맵 외곽 )..
                for ( int boxIndex = 0; boxIndex < BOX_COUNT; ++boxIndex )
                {
                    Map.SpaceType curStandSpaceType = map.GetCurStandSpaceType( boxes[boxIndex].X, boxes[boxIndex].Y );

                    // 박스가 현재 위치에 다른 물체가 있을 때는 이전 위치로 이동..
                    switch ( curStandSpaceType )
                    {
                        case Map.SpaceType.DontPass:
                            boxes[boxIndex].UndoPosState();

                            break;
                        case Map.SpaceType.PlayerStand:
                            if ( Box.State.GrabByPlayer != boxes[boxIndex].CurState )
                            {
                                boxes[boxIndex].UndoPosState();
                            }

                            break;
                        case Map.SpaceType.BoxStand:
                            for ( int otherBoxIndex = 0; otherBoxIndex < BOX_COUNT; ++otherBoxIndex )
                            {
                                // 현재 같은 박스를 검사하는 것이라면 continue..
                                if ( boxIndex == otherBoxIndex )
                                {
                                    continue;
                                }

                                // 충돌 시 다시 제자리로 돌려보내기..
                                if ( IsCollision( boxes[boxIndex].X, boxes[boxIndex].Y, boxes[otherBoxIndex].X, boxes[otherBoxIndex].Y ) )
                                {
                                    boxes[boxIndex].UndoPosState();
                                }

                                break;
                            }

                            break;
                        case Map.SpaceType.Portal:
                            // 포탈 다른 게이트로 이동..
                            PushPortal( portals, ref boxes[boxIndex].X, ref boxes[boxIndex].Y, boxes[boxIndex].PrevX, boxes[boxIndex].PrevY );

                            // 현재 플레이어에게 잡혀있는 상태라면 기본 상태로 변경..
                            if ( Box.State.GrabByPlayer == boxes[boxIndex].CurState )
                            {
                                boxes[boxIndex].UndoState();
                            }

                            // 이동한 지점에 다른 오브젝트가 한번 더 검사하려고..
                            --boxIndex;

                            break;
                        case Map.SpaceType.Trap:
                            for ( int trapIndex = 0; trapIndex < TRAP_COUNT; ++trapIndex )
                            {
                                if ( IsCollision( boxes[boxIndex].X, boxes[boxIndex].Y, traps[trapIndex].X, traps[trapIndex].Y ) )
                                {
                                    traps[trapIndex].Action();

                                    break;
                                }
                            }

                            break;
                        case Map.SpaceType.Arrow:
                            boxes[boxIndex].UndoState();

                            break;
                    }
                }

                #endregion

                #region Player Collision

                // 플레이어가 특정 물체와 겹쳤다( 박스 or 벽 등등 )..
                Map.SpaceType overlapSpaceType = map.GetCurStandSpaceType(player.X, player.Y ); // 여기서 받아옵니다..

                // 여기서 검사..
                switch ( overlapSpaceType )
                {
                    case Map.SpaceType.DontPass:
                        player.UndoPos();

                        break;
                    case Map.SpaceType.BoxStand:
                        for ( int boxIdx = 0; boxIdx < BOX_COUNT; ++boxIdx )
                        {
                            int boxX = boxes[boxIdx].X;
                            int boxY = boxes[boxIdx].Y;

                            // 만약 플레이어와 박스의 위치가 같을 때 이전 위치로..
                            // 맵 데이터가 갱신이 안되있는 상태기 때문에 이 검사를 하는 것..
                            if ( IsCollision( player.X, player.Y, boxX, boxY ) )
                            {
                                player.UndoPos();
                            }
                        }

                        break;
                    case Map.SpaceType.Portal:
                        PushPortal( portals, ref player.X, ref player.Y, player.PrevX, player.PrevY );

                        break;

                    case Map.SpaceType.Item:
                        for ( int itemIndex = 0; itemIndex < ITEM_COUNT; ++itemIndex )
                        {
                            if ( IsCollision( player.X, player.Y, items[itemIndex].X, items[itemIndex].Y ) )
                            {
                                playerActiveItemIndex[activeItemCount] = itemIndex;
                                ++activeItemCount;

                                // 밟은 아이템은 필드에서 제거( 안보이게 함 )..
                                items[itemIndex].isActive = false;

                                break;
                            }
                        }

                        break;

                    case Map.SpaceType.Trap:
                        for ( int trapIndex = 0; trapIndex < TRAP_COUNT; ++trapIndex )
                        {
                            if ( IsCollision( player.X, player.Y, traps[trapIndex].X, traps[trapIndex].Y ) )
                            {
                                traps[trapIndex].Action();

                                break;
                            }
                        }

                        break;
                }

                #endregion

                #region Switch Collision

                for ( int switchIdx = 0; switchIdx < SWITCH_COUNT; ++switchIdx )
                {
                    int switchButtonX = switches[switchIdx].X + switches[switchIdx].ButtonOffsetX;
                    int switchButtonY = switches[switchIdx].Y + switches[switchIdx].ButtonOffsetY;

                    Map.SpaceType curPushPosSpaceType = map.GetCurStandSpaceType( switchButtonX, switchButtonY );

                    // 스위치 실제 누르는 부분에 어떠한 오브젝트가 있다면 스위치 On..
                    if ( Map.SpaceType.Pass != curPushPosSpaceType )
                    {
                        switches[switchIdx].SetSwitchState( map, walls, true );

                        isSkipRender = false;
                    }
                    else    // 스위치 실제 누르는 부분에 아무것도 없다면 스위치 Off..
                    {
                        switches[switchIdx].SetSwitchState( map, walls, false );

                        isSkipRender = false;
                    }
                }

                #endregion

                #region Arrow Collision

                for ( int arrowIndex = 0; arrowIndex < arrows.Count; )
                {
                    Map.SpaceType curSpaceType = map.GetCurStandSpaceType( arrows[arrowIndex].X, arrows[arrowIndex].Y );

                    switch ( curSpaceType )
                    {
                        case Map.SpaceType.PlayerStand:
                            player.CurHp -= arrows[arrowIndex].Damage;
                            removeArrows.Add( arrows[arrowIndex] );
                            arrows.Remove( arrows[arrowIndex] );

                            break;

                        case Map.SpaceType.DontPass:
                            removeArrows.Add( arrows[arrowIndex] );
                            arrows.Remove( arrows[arrowIndex] );
                            break;

                        case Map.SpaceType.BoxStand:
                            removeArrows.Add( arrows[arrowIndex] );
                            arrows.Remove( arrows[arrowIndex] );

                            break;

                        default:
                            ++arrowIndex;

                            break;
                    }
                }

                #endregion

                // Map Update..
                map.Update( updateMapSpace );

                #endregion
            }


            /// <summary>
            /// 포탈 밟았을 때 다른 게이트로 이동시키는 기능..
            /// </summary>
            void PushPortal( in Portal[] portals, ref int curPosX, ref int curPosY, int prevPosX, int prevPosY )
            {
                bool isFindPortal = false;

                for ( int portalIdx = 0; portalIdx < PORTAL_COUNT; ++portalIdx )
                {
                    for ( int gateIndex = 0; gateIndex < PORTAL_GATE_COUNT; ++gateIndex )
                    {
                        int curPortalX = portals[portalIdx].GatesX[gateIndex];
                        int curPortalY = portals[portalIdx].GatesY[gateIndex];

                        if ( IsCollision( curPortalX, curPortalY, curPosX, curPosY ) )
                        {
                            // 다른 포탈 게이트 위치 계산..
                            portals[portalIdx].Teleport( gateIndex, ref curPortalX, ref curPortalY, in prevPosX, in prevPosY );

                            // 현재 포탈 이동한 지점에 다른 오브젝트가 있는지 검사..
                            Map.SpaceType curPosSpaceType = map.GetCurStandSpaceType( curPortalX, curPortalY );
                            if ( Map.SpaceType.Pass == curPosSpaceType || Map.SpaceType.Item == curPosSpaceType )
                            {
                                curPosX = curPortalX;
                                curPosY = curPortalY;
                            }
                            else
                            {
                                curPosX = prevPosX;
                                curPosY = prevPosY;
                            }

                            isFindPortal = true;

                            break;
                        }
                    }

                    if ( isFindPortal )
                    {
                        break;
                    }
                }
            }

            /// <summary>
            /// 충돌 했는가..
            /// </summary>
            bool IsCollision( int x, int y, int x2, int y2 )
            {
                if ( x == x2 && y == y2 )
                {
                    return true;
                }

                return false;
            }
        }
    }
}