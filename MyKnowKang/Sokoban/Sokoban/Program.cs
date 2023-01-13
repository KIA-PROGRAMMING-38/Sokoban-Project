using Sokoban;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace KMH_Sokoban
{
	public enum MapSpaceType
	{
		Pass            // 지나가는 곳..
		, DontPass      // 못지나가는 곳..
		, PlayerStand   // 플레이어가 있는 곳..
		, BoxStand      // 박스가 있는 곳..
		, Portal		// 포탈이 있는 곳..
	}

	class Program
	{

		static void Main()
		{
			// ------------------------------------------- 초기화(객체 생성 및 초기화).. -------------------------------------------
			#region Initialize
			#region 상수 초기화
			// ============================================================================================================================================
			// 상수 초기화..
			// ============================================================================================================================================

			// 초기 세팅 관련 상수 설정..
			#region 콘솔 세팅
			const bool CURSOR_VISIBLE = false;                      // 커서를 숨긴다..
			const string TITLE_NAME = "Welcome To Liverpool";       // 타이틀을 설정한다..
			const ConsoleColor BACKGROUND_COLOR = ConsoleColor.Black;   // Background 색을 설정한다..
			const ConsoleColor FOREGROUND_COLOR = ConsoleColor.White;     // 글꼴색을 설정한다..
			const ConsoleColor BORDERLINE_COLOR = ConsoleColor.DarkRed;
			#endregion

			#region Map Size
			// 맵 사이즈 관련 상수 설정..
			const int MAP_WIDTH = 50;
			const int MAP_HEIGHT = 22;
			const int MAP_RANGE_MIN_X = 1;
			const int MAP_RANGE_MIN_Y = 1;
			const int MAP_RANGE_MAX_X = MAP_RANGE_MIN_X + MAP_WIDTH;
			const int MAP_RANGE_MAX_Y = MAP_RANGE_MIN_Y + MAP_HEIGHT;
			#endregion

			#region Player
			// 플레이어 관련 상수 설정..
			const int INITIAL_PLAYER_X = 1;
			const int INITIAL_PLAYER_Y = 1;
			const string PLAYER_IMAGE = "♀";
			const ConsoleColor PLAYER_COLOR = ConsoleColor.Blue;
			#endregion

			#region Box
			// 박스 관련 상수 설정..
			const string BOX_IMAGE = "◎";
			const ConsoleColor BOX_COLOR = ConsoleColor.Yellow;
			#endregion

			#region Wall
			// 벽 관련 상수 설정..
			const char WALL_IMAGE = '▣';
			const ConsoleColor WALL_COLOR = ConsoleColor.DarkRed;
			#endregion

			#region Goal
			// 골인 지점 관련 상수..
			const string GOAL_IMAGE = "∏";
            const string GOALIN_IMAGE = "∏";
			const ConsoleColor GOAL_COLOR = ConsoleColor.Gray;
			const ConsoleColor GOALIN_COLOR = ConsoleColor.DarkGray;
			#endregion

			#region Portal
			const int PORTAL_GATE_COUNT = 2;	// 한 개의 포탈에 이동할 수 있는 게이트의 개수??( 입구 <-> 출구 로 이동하니까 2개 )
			const int PORTAL_COUNT = 8;

			const string PORTAL_IMAGE = "Ⅱ";
			#endregion

			#region Switch
			const int SWITCH_COUNT = 1;
			const ConsoleColor SWITCH_COLOR = ConsoleColor.DarkMagenta;
			const string SWITCH_IMAGE = "ㅣ";
			const string SWITCH_PUSH_IMAGE = "*";
			#endregion
			#endregion

			#region 변수 초기화
			// ============================================================================================================================================
			// 변수 초기화..
			// ============================================================================================================================================

			#region Player 관련 변수 설정
			// 플레이어 관련 변수 설정..
			Player player = new Player
			{
				X = INITIAL_PLAYER_X, Y = INITIAL_PLAYER_Y, PrevX = INITIAL_PLAYER_X, PrevY = INITIAL_PLAYER_Y,
				Image = PLAYER_IMAGE, Color = PLAYER_COLOR
			};
			#endregion

			#region Box 관련 변수 설정
			// 박스 관련 변수 설정..
			const int BOX_COUNT = 5;
			int[] INIT_BOXES_X = new int[BOX_COUNT] { 5, 5, 5, 5, 10 };
			int[] INIT_BOXES_Y = new int[BOX_COUNT] { 2, 3, 4, 5, 7 };

			Box[] boxes = new Box[BOX_COUNT];
			for( int boxIndex = 0; boxIndex < BOX_COUNT; ++boxIndex )
			{
				boxes[boxIndex] = new Box
				{
					X = INIT_BOXES_X[boxIndex], Y = INIT_BOXES_Y[boxIndex], PrevX = INIT_BOXES_X[boxIndex], PrevY = INIT_BOXES_Y[boxIndex],
					Image = BOX_IMAGE, Color = BOX_COLOR, CurState = Box.State.Idle, DirX = 0, DirY = 0
                };
			}
			#endregion

			#region Map 관련 변수 설정
			// 맵 관련 변수 설정..
			// 맵의 각 위치들의 데이터를 저장하는 룩업 테이블..
			MapSpaceType[,] mapDatas = new MapSpaceType[MAP_HEIGHT + 1, MAP_WIDTH + 1];
			#endregion

			#region Wall 관련 변수 설정
			// 벽 관련 변수 설정..
			const int WALL_COUNT = 15;
			int[] wallsX = new int[WALL_COUNT] { 3, 3, 3, 42, 42, 42, 42, 42, 43, 44, 45, 46, 47, 48, 49 };
			int[] wallsY = new int[WALL_COUNT] { 8, 7, 9, 18, 19, 20, 21, 17, 17, 17, 17, 17, 17, 17, 17 };

			// 현재 벽이 활성화되어있는가( default를 true 로 초기화 )..
			bool[] isWallActive = Enumerable.Repeat<bool>( true, WALL_COUNT ).ToArray<bool>();
			#endregion

			#region Goal 관련 변수 설정
			// 골인 지점 관련 변수 설정..
			const int GOAL_COUNT = BOX_COUNT;
			int[] goalsX = new int[GOAL_COUNT] { 49, 49, 49, 49, 40 };
			int[] goalsY = new int[GOAL_COUNT] { 18, 19, 20, 21, 7 };

			bool[] isGoalIn = new bool[GOAL_COUNT];
			#endregion

			#region Portal 관련 변수 설정
			int[,] portalX = new int[PORTAL_COUNT, 2] 
			{ { 35, 5 }, { 35, 5 }, { 35, 5 }, { 35, 5 }, { 4, 40 }, { 10, 20 }, { 20, 30 }, { 30, 10 } };
			int[,] portalY = new int[PORTAL_COUNT, 2] 
			{ { 2, 18 }, { 3, 19 }, { 4, 20 }, { 5, 21 }, { 6, 16 }, { 1, 12 }, { 1, 12 }, { 1, 12 } };

			ConsoleColor[] portalColor = new ConsoleColor[PORTAL_COUNT]
			{
				ConsoleColor.Green, ConsoleColor.DarkMagenta, ConsoleColor.Gray, ConsoleColor.Blue, ConsoleColor.Yellow
				, ConsoleColor.Green, ConsoleColor.Gray, ConsoleColor.Blue
			};
			#endregion

			#region Switch 관련 변수 설정
			int[] switchX = new int[SWITCH_COUNT] { 41 };
			int[] switchY = new int[SWITCH_COUNT] { 17 };
			int[] switchPushOffsetX = new int[SWITCH_COUNT] { -1 };
			int[] switchPushOffsetY = new int[SWITCH_COUNT] { 0 };

			bool[] isSwitchHolding = new bool[SWITCH_COUNT];	// 스위치 누르는 중이냐..
			int[][] openCloseWallIdx = new int[SWITCH_COUNT][];	// 스위치 누르거나 땔 때 열거나 닫는 벽 인덱스..
			openCloseWallIdx[0] = new int[4] { 3, 4, 5, 6 };
			#endregion

			#region 기타 변/상수 설정
			// 타이머 관련..
			const int FRAME_PER_SECOND = 90;
			double frameInterval = 1.0 / FRAME_PER_SECOND;
			double elaspedTime = 0.0;
			double runTime = 0.0;
			double prevRunTime = 0.0;
			Stopwatch stopwatch = new Stopwatch();
			// 렌더 관련( 그릴지 말지 )..
			bool isSkipRender = false;
			#endregion
			#endregion

			#region 디버깅 관련 변수,상수
			LinkedList<KeyValuePair<int, string>> logMessage = new LinkedList<KeyValuePair<int, string>>();
			int logStartX = 60;
			int logStartY = 0;

			logMessage.AddLast( new KeyValuePair<int, string>( 2, "============== 설명 ==============" ) );
			logMessage.AddLast( new KeyValuePair<int, string>( 1, $"{PLAYER_IMAGE} : 플레이어" ) );
			logMessage.AddLast( new KeyValuePair<int, string>( 1, $"{BOX_IMAGE} : 박스" ) );
			logMessage.AddLast( new KeyValuePair<int, string>( 1, $"{WALL_IMAGE} : 벽" ) );
			logMessage.AddLast( new KeyValuePair<int, string>( 1, $"{GOALIN_IMAGE} : 골" ) );
			logMessage.AddLast( new KeyValuePair<int, string>( 1, $"{SWITCH_IMAGE} : 스위치, {SWITCH_PUSH_IMAGE} : 버튼" ) );

			logMessage.AddLast( new KeyValuePair<int, string>( 2, "" ) );
			logMessage.AddLast( new KeyValuePair<int, string>( 2, "======== 방향키 ========" ) );
			logMessage.AddLast( new KeyValuePair<int, string>( 1, "↑ ← ↓ → : 이동" ) );
			logMessage.AddLast( new KeyValuePair<int, string>( 1, "SpaceBar : 박스 잡기" ) );
			logMessage.AddLast( new KeyValuePair<int, string>( 1, "A : 박스 차기" ) );
			#endregion

			#region 시작 전 초기 작업
			// Console 초기 세팅..
			Console.ResetColor();                           // 컬러를 초기화한다..
			Console.CursorVisible = CURSOR_VISIBLE;         // 커서를 숨긴다..
			Console.Title = TITLE_NAME;                     // 타이틀을 설정한다..
			Console.BackgroundColor = BACKGROUND_COLOR;     // Background 색을 설정한다..
			Console.ForegroundColor = FOREGROUND_COLOR;     // 글꼴색을 설정한다..
			Console.Clear();                                // 출력된 모든 내용을 지운다..


			// 시작 전에 맵 데이터에 플레이어 박스 위치 저장..
			mapDatas[player.Y, player.X] = MapSpaceType.PlayerStand;
			for ( int i = 0; i < BOX_COUNT; ++i )
			{
                mapDatas[boxes[i].Y, boxes[i].X] = MapSpaceType.BoxStand;
            }

			// 맵 데이터에 벽 위치 저장..
			for ( int i = 0; i < WALL_COUNT; ++i )
			{
				if ( isWallActive[i] )
				{
                    mapDatas[wallsY[i], wallsX[i]] = MapSpaceType.DontPass;
                }
				else
				{
                    mapDatas[wallsY[i], wallsX[i]] = MapSpaceType.Pass;
                }
			}

			// 맵 데이터에 포탈 위치 저장..
			for ( int i = 0; i < PORTAL_COUNT; ++i )
			{
				for( int j = 0; j < PORTAL_GATE_COUNT; ++j )
					mapDatas[portalY[i, j], portalX[i, j]] = MapSpaceType.Portal;
			}

			// 맵 데이터에 스위치 위치 저장..
			for ( int i = 0; i < SWITCH_COUNT; ++i )
				mapDatas[switchY[i], switchX[i]] = MapSpaceType.DontPass;

			// 맵 외곽 통과 못하는 곳으로 설정..
			for ( int i = 0; i <= MAP_WIDTH; ++i )
			{
				mapDatas[0, i] = MapSpaceType.DontPass;
				mapDatas[MAP_HEIGHT, i] = MapSpaceType.DontPass;
			}
			for ( int i = 0; i <= MAP_HEIGHT; ++i )
			{
				mapDatas[i, 0] = MapSpaceType.DontPass;
				mapDatas[i, MAP_WIDTH] = MapSpaceType.DontPass;
			}

			// 타이머 스타트..
			stopwatch.Start();
			#endregion
			#endregion

			#region GameLoop
			while ( true )
			{
				elaspedTime += runTime - prevRunTime;
				prevRunTime = runTime;
				runTime = stopwatch.Elapsed.TotalMilliseconds * 0.0001;

				if ( elaspedTime >= frameInterval )
				{
					elaspedTime = 0.0;

					#region ProcessInput
					// --------------------------------------------------------------- ProcessInput.. ---------------------------------------------------------------
					// 입력한 키 가져오기..
					ConsoleKey inputKey = ConsoleKey.NoName;
					if ( Console.KeyAvailable )
					{
						inputKey = Console.ReadKey().Key;
						isSkipRender = false;
					}
					#endregion

					#region Update
					// ------------------------------------------------------------------ Update.. ------------------------------------------------------------------
					// ================================= Player Update.. =================================
					#region Player Update
					// 플레이어 이전위치 갱신..
					player.PrevX = player.X;
					player.PrevY = player.Y;

					// 1. 입력 키 처리..
					switch ( inputKey )
					{
						case ConsoleKey.RightArrow:
						case ConsoleKey.LeftArrow:
							player.X += (int)inputKey - 38;

							break;
						case ConsoleKey.DownArrow:
						case ConsoleKey.UpArrow:
							player.Y += (int)inputKey - 39;

							break;
						case ConsoleKey.Spacebar:
							for ( int i = 0; i < BOX_COUNT; ++i )
							{
								int boxX = boxes[i].X;
								int boxY = boxes[i].Y;
								Box.State boxState = boxes[i].CurState;

								int xDist = Math.Abs( player.X - boxX );
								int yDist = Math.Abs( player.Y - boxY );

								if ( 1 == xDist + yDist )
								{
									if ( Box.State.GrabByPlayer == boxState )
                                        boxState = Box.State.Idle;
									else
                                        boxState = Box.State.GrabByPlayer;

									boxes[i].CurState = boxState;
                                }
							}

							break;
						case ConsoleKey.A:
							for ( int i = 0; i < BOX_COUNT; ++i )
							{
                                int boxX = boxes[i].X;
                                int boxY = boxes[i].Y;

                                int xDist = Math.Abs( player.X - boxX );
								int yDist = Math.Abs( player.Y - boxY );

								if ( 1 == xDist + yDist )
								{
									int curBoxIndex = i;

									// 박스 상태 변경 및 그와 관련된 값 설정..
									boxes[curBoxIndex].CurState = Box.State.Move;

									int dirX = player.X - boxX;
									int dirY = player.Y - boxY;

									boxes[curBoxIndex].DirX = dirX;
                                    boxes[curBoxIndex].DirY = dirY;
								}
							}

							break;
					}
					#endregion

					#region Box Update
					// ================================= Box Update.. =================================
					// 박스 업데이트..
					for ( int i = 0; i < BOX_COUNT; ++i )
					{
						// 박스 이전위치 갱신..
						boxes[i].PrevX = boxes[i].X;
						boxes[i].PrevY = boxes[i].Y;

						switch ( boxes[i].CurState )
						{
							case Box.State.Idle:
								if ( player.X == boxes[i].X && player.Y == boxes[i].Y )   // 플레이어와 박스가 같을 때..
								{
									// 박스가 이동할 위치를 계산( 현재위치 - 이전위치 = 이동할 방향 )..
									int boxMoveDirX = player.X - player.PrevX;
									int boxMoveDirY = player.Y - player.PrevY;

									// 박스 현재위치 갱신.. 
									boxes[i].X += boxMoveDirX;
									boxes[i].Y += boxMoveDirY;

									break;
								}

								break;
							case Box.State.Move:
								boxes[i].X -= boxes[i].DirX;
								boxes[i].Y -= boxes[i].DirY;

								isSkipRender = false;

								break;

							case Box.State.GrabByPlayer:
							{
								boxes[i].PrevX = boxes[i].X;
								boxes[i].PrevY = boxes[i].Y;

								// 박스가 이동할 위치를 계산( 현재위치 - 이전위치 = 이동할 방향 )..
								int boxMoveDirX = player.X - player.PrevX;
								int boxMoveDirY = player.Y - player.PrevY;

								// 박스 현재위치 갱신.. 
								boxes[i].X += boxMoveDirX;
                                boxes[i].Y += boxMoveDirY;
							}

								break;
						}
					}
					#endregion

					#region Collision
					// ================================= Player Box Update가 끝난 후 Overlap 처리??.. =================================
					#region Box Collision
					// 박스가 특정 물체와 겹쳤다( 박스 or 벽 or 맵 외곽 )..
					for ( int i = 0; i < BOX_COUNT; ++i )
					{
						MapSpaceType curStandSpaceType = mapDatas[boxes[i].Y, boxes[i].X];

						// 박스가 현재 위치에 다른 물체가 있을 때는 이전 위치로 이동..
						switch(curStandSpaceType)
						{
							case MapSpaceType.DontPass:
								boxes[i].X = boxes[i].PrevX;
								boxes[i].Y = boxes[i].PrevY;

								boxes[i].CurState = Box.State.Idle;

								break;
							case MapSpaceType.BoxStand:
								for ( int curBoxIdx = 0; curBoxIdx < BOX_COUNT; ++curBoxIdx )
								{
									if ( curBoxIdx == i )
										continue;
									if ( boxes[i].X != boxes[curBoxIdx].X || boxes[i].Y != boxes[curBoxIdx].Y )
										continue;

									boxes[i].X = boxes[i].PrevX;
                                    boxes[i].Y = boxes[i].PrevY;

                                    boxes[i].CurState = Box.State.Idle;

									break;
								}

								break;

							case MapSpaceType.PlayerStand:
								if( Box.State.GrabByPlayer != boxes[i].CurState )
								{
									boxes[i].X = boxes[i].PrevX;
                                    boxes[i].Y = boxes[i].PrevY;

                                    boxes[i].CurState = Box.State.Idle;
								}

								break;

							case MapSpaceType.Portal:
								bool isFindPortal = false;
								for ( int portalIdx = 0; portalIdx < PORTAL_COUNT; ++portalIdx )
								{
									for ( int portalGateIdx = 0; portalGateIdx < PORTAL_GATE_COUNT; ++portalGateIdx )
									{
										int curPortalX = portalX[portalIdx, portalGateIdx];
										int curPortalY = portalY[portalIdx, portalGateIdx];

										if ( curPortalX == boxes[i].X && curPortalY == boxes[i].Y )
										{
											curPortalX = portalX[portalIdx, (portalGateIdx + 1) % 2];
											curPortalY = portalY[portalIdx, (portalGateIdx + 1) % 2];

											int dirX = boxes[i].X - boxes[i].PrevX;
											int dirY = boxes[i].Y - boxes[i].PrevY;

											boxes[i].X = curPortalX + dirX;
                                            boxes[i].Y = curPortalY + dirY;

											if ( Box.State.GrabByPlayer == boxes[i].CurState )
                                                boxes[i].CurState = Box.State.Idle;

											i -= 1;

											isFindPortal = true;

											break;
										}
									}

									if ( isFindPortal )
										break;
								}

								break;
						}
					}
					#endregion

					#region Player Collision
					// 플레이어가 특정 물체와 겹쳤다( 박스 or 벽 등등 )..
					MapSpaceType overlapSpaceType = mapDatas[player.Y, player.X];
					switch(overlapSpaceType)
					{
						case MapSpaceType.DontPass:
							player.X = player.PrevX;
							player.Y = player.PrevY;

							break;
						case MapSpaceType.BoxStand:
							for( int boxIdx = 0; boxIdx < BOX_COUNT; ++boxIdx)
							{
                                int boxX = boxes[boxIdx].X;
                                int boxY = boxes[boxIdx].Y;

                                // 만약 플레이어와 박스의 위치가 같을 때 이전 위치로..
                                // 맵 데이터가 갱신이 안되있는 상태기 때문에 이 검사를 하는 것..
                                if (player.X == boxX && player.Y == boxY)
                                {
                                    player.X = player.PrevX;
                                    player.Y = player.PrevY;
                                }
                            }

							break;
						case MapSpaceType.Portal:
							for( int portalIdx = 0; portalIdx < PORTAL_COUNT; ++portalIdx )
							{
								for (int portalGateIdx = 0; portalGateIdx < PORTAL_GATE_COUNT; ++portalGateIdx)
								{
									int curPortalX = portalX[portalIdx, portalGateIdx];
									int curPortalY = portalY[portalIdx, portalGateIdx];

									if (curPortalX == player.X && curPortalY == player.Y)
									{
                                        curPortalX = portalX[portalIdx, (portalGateIdx + 1) % 2];
										curPortalY = portalY[portalIdx, (portalGateIdx + 1) % 2];

										int dirX = player.X - player.PrevX;
										int dirY = player.Y - player.PrevY;

										player.X = curPortalX + dirX;
										player.Y = curPortalY + dirY;

										overlapSpaceType = mapDatas[player.Y, player.X];
										if( MapSpaceType.Pass != overlapSpaceType )
										{
											player.X = player.PrevX;
											player.Y = player.PrevY;
										}

										break;
                                    }
								}
							}

							break;
					}
					#endregion

					#region Switch Collision
					for( int switchIdx = 0; switchIdx < SWITCH_COUNT; ++switchIdx )
					{
						int switchPushX = switchX[switchIdx] + switchPushOffsetX[switchIdx];
						int switchPushY = switchY[switchIdx] + switchPushOffsetY[switchIdx];

						MapSpaceType curPushPosSpaceType = mapDatas[switchPushY, switchPushX];
						if( MapSpaceType.Pass != curPushPosSpaceType )
						{
							if( false == isSwitchHolding[switchIdx] )
							{
								isSwitchHolding[switchIdx] = true;

								for( int wallIdx = 0; wallIdx < openCloseWallIdx[switchIdx].Length; ++wallIdx )
									isWallActive[openCloseWallIdx[switchIdx][wallIdx]] = false;

								isSkipRender = false;
							}
						}
						else
						{
							if( true == isSwitchHolding[switchIdx] )
							{
								isSwitchHolding[switchIdx] = false;

								for ( int wallIdx = 0; wallIdx < openCloseWallIdx[switchIdx].Length; ++wallIdx )
									isWallActive[openCloseWallIdx[switchIdx][wallIdx]] = true;

								isSkipRender = false;
							}
						}
					}
					#endregion
					#endregion

					#region Goal Update
					// 골인 지점과 박스 위치가 몇개나 같은지 비교하는 곳..
					int goalBoxCount = 0;
					for ( int i = 0; i < GOAL_COUNT; ++i )
					{
						isGoalIn[i] = false;

						for ( int j = 0; j < BOX_COUNT; ++j )
						{
							if ( goalsX[i] == boxesX[j] && goalsY[i] == boxesY[j] )
							{
								++goalBoxCount;
								isGoalIn[i] = true;

								break;
							}
						}
					}

					// 현재 골인지점과 박스위치가 전부 같다면( GG )..
					if ( goalBoxCount == GOAL_COUNT )
						break;
					#endregion

					#region Map Update
					// ================================= 전체적인 위치의 갱신이 끝났다면 맵에 데이터 저장.. =================================
					// 플레이어 정보 갱신..
					mapDatas[player.PrevY, player.PrevX] = MapSpaceType.Pass;
					mapDatas[player.Y, player.X] = MapSpaceType.PlayerStand;

					// Box 정보 갱신..
					for ( int i = 0; i < BOX_COUNT; ++i )
					{
						mapDatas[prevBoxesY[i], prevBoxesX[i]] = MapSpaceType.Pass;
						mapDatas[boxesY[i], boxesX[i]] = MapSpaceType.BoxStand;
					}

					// Portal 정보 갱신..
					for( int portalIdx = 0; portalIdx < PORTAL_COUNT; ++portalIdx )
					{
						for( int portalGateIdx = 0; portalGateIdx < PORTAL_GATE_COUNT; ++portalGateIdx )
						{
							int curPortalX = portalX[portalIdx, portalGateIdx];
							int curPortalY = portalY[portalIdx, portalGateIdx];
							mapDatas[curPortalY, curPortalX] = MapSpaceType.Portal;
						}
					}

					// 벽 정보 갱신..
					for( int wallIdx = 0; wallIdx < WALL_COUNT; ++wallIdx )
					{
						int wallX = wallsX[wallIdx];
						int wallY = wallsY[wallIdx];

						if ( true == isWallActive[wallIdx] )
							mapDatas[wallY, wallX] = MapSpaceType.DontPass;
						else
							mapDatas[wallY, wallX] = MapSpaceType.Pass;
					}

					// Switch 정보 갱신..
					for ( int i = 0; i < SWITCH_COUNT; ++i )
						mapDatas[switchY[i], switchX[i]] = MapSpaceType.DontPass;

                    #endregion

                    #endregion

                    if ( isSkipRender ) // Render 를 스킵해야 한다면( 무한 깜빡임 방지 )..
                        continue;

                    isSkipRender = true;

                    Render();
				}
			}
			#endregion

			#region Clear Message
			// 게임 클리어 했으니까 메시지 띄우기..
			Console.Clear();
			Console.SetCursorPosition( 0, 0 );

			Console.ForegroundColor = FOREGROUND_COLOR;
			Console.WriteLine( "게임을 클리어하셨습니다!!!!!\n" );
			Console.WriteLine( "L을 입력 시 그림 나옴" );
			Console.WriteLine( "글꼴을 돋움채로 하시고 전체화면으로 보시는 걸 추천합니다." );

			ConsoleKeyInfo KeyInfo = Console.ReadKey();

			if ( KeyInfo.Key == ConsoleKey.L )
			{
				Console.Clear();
				Console.BackgroundColor = ConsoleColor.Black;
				Console.SetCursorPosition( 0, 0 );
				Console.ForegroundColor = ConsoleColor.White;
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWNNNNXXXXXNNNNNNNNNXXXXXXXNNNNWMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWWWWWWNXXNXXXXXXXXXXXXXXNXXXXXXXNNXXNWWWMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWWWWWWWWMMMMMMMMMMMMMMMMMMMMMMMMMMMMWWWWMMMMWMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWWMWK0KNWNWNWNNWNNWNWNWNNNNWNWNWNNNNWWWNWNNNNNNNNWNWWNWNK0KNWWMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMNXNKKXXOkkKXKXKXKKKOKXKXKXKKXKXKXKXKKKKXKXKXKXKKKKXKXKXKKNKkkOXXKKXNWMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMXKNXKXXK00KNXNXXXXXKXNXNXNXXXXXXNXNXXXXXXNXNXNXXXXNXNXXXXWX000XXKXK0XMMWWMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWXXNWWWWWWWWWWWWWWWWWWWWWMWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWNXWMMMWWWNWWWNXXWMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWXKXNXXXXXXXXXKXXXXXXXXKXXXWXXXXXKXKXXXXXXKXKXXXXXXKXKXXXXXXXXKXKXWNXXWWXXXKXKXXNXKXWMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWNXXWMWKXKKKKXKXKXKXKKXKXKXKKWKKXKXKXKXKKKKXKXKXKXKKKKXKXKXKKKKXKXKXWMWXXXKXKKKKXKNMWXXNMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWNNNWWWMMWWWWWWWWWWWWWNXNWWWWWWWWWWWNXNWWWNNWWWWWWWNNNNWWWWWWWWWWWWWWWWMMWWNNXNWWWWWWMWWNNNNWMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWNXXNNNNNNNXNNXNXNXNWMWXXXXNNXNNNWWMNXXNXNWXKXNWNNNNWKO0XNXNNNNNNXWWNNNNXNNNNXNNKXWMWXNNNNNNXXKXWMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWNXXWMNKKKKKKKKKKKKKKKNXKXWMXKKKKKKNNXXNWMKKX0XXKNKKKXN0OKWXKXKKKXWXNXKKKKKKKKK0NWWNKXXKK0KKKKKWWXXXWMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWNWMMMWNNNNNNNNNNNNNXNNXWMWMWNNNNXXNNNWMMMWNXNWWNWNNNWNXXNMWNNNNNWMMMWNNNNNNNNNNWMMMWNNNNNNNNNNMMMWNNWWMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMNXXWWNWNWNWWNWNWNWNWMWXKNWNWNNNNMWNXXNNWWNWNWNXNWNNWNWNWNK0XWWNNNWNWNWMW00WWNWNNWNWNWNWNNNNMWNWWNWNNNNMXOKWMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMN0KKXXKXKXKXKKKKXKXKXNKXWMNKXKKXKNXKNWXOKXKXKKKKKXKKXKXKNXOOXMKKKKXKXKXWK00KNKKKXXKXKXKXKXKKWWXXWKXKKXXX000XMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMXXWXXXXNXNXXXXXXNXNXKXWMMMWXXXXXKXNWMMNKXNXNX0XWXXXXXXNXNX0XWMXXXXNXNXNNXWWXNXXXXXXNXNXNXXXXWMWWWXXXXXXXXMNXWMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWNNWWWWWWWWWWWWMMMWXXNWWWWWWMMMWXNNWWWWWWNWMMNXNWWWWWWWWWWNXXNWWWWWWWWWWWWWWMWXXWWWWWWWWWWWWWWWWWWWMNXMNXNWWMMXKWMMMMMMMMMMMMMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMNKKXXXXXXXXXXXXXWWNKXWXXXXXKNMWXXNNXXXXXXX0KWN0KXXKXXXXXXXNKO0NNXXKXXXXKXXXKNWXOOXWKXXXXXXKXXXXXXXXKWN0K0KNXNMN0OKWMMMMMMMMMMMMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWKKNKXKKKKXKXKXKKNXXWMWKKKKXKXXXNWMNKXKKKKX0KXKXXKXKXKKKKKXX0OKMXKXKXKKKKXKXKXNKNNKXKXKKKKXKXKXKKKKXKNMNOKWXKNNKXWKXMMMMMMMMMMMMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWWNWWWWWWWWWWWWWNNNWMMWWWWWWXXNNWWMMWWWWWWWWWNNNWWWWWWWWWWNWNXXNMWWWWWWWWWWWWWWWWMMWNNWWWWWWWWWWWWWWWWWWWNNWWNNWWWWWNNMMMMMMMMMMMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWKKXNNXNNNNNXNNNXKXNNNNXNMWNXXNNNNNNNNNNXNNNXOKXNNNNNNNNXNNNN0OKWNNNNNNNXNNNNNNNNXNWNOONWNNNNNNNNNNNNNNNNNN0XK0NNNXNN0OXMMMMMMMMMMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWK0XKKKKKKKKXKKK00XKKKKK0XNXXNMNKKKKKKKKKKXK0KKXKKKKKKKKK0XKKKOOXWKKKKKKXKXKKKKKKKKXN0XKKNKKKKKKKKXKXKKKKKKWXOOXWKXKKKKX0NMMMMMMMMMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWXNWNNNNNNNNNNNNKNMNNNNNNNXNMMMWNNNNNNNNNNNXXNMWNNNNNNNNNNNNNXKNWMNNNNNNNNNNNNNNNNNWNNMWNNNNNNNNNNNNNNNNNNNWWNNMWNNNNNWMNNMMMMMMMMMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWXKXWNNWNWNWNWNWNKNWWWWNWXXNWNWWWWNWNWWWWWMX0KNWXNWNWNWWWNWWNWN0KWWNWWWNWNWWNWWWNWWWWNWNWMN00WWNWWWNWWWWNWWWNNWNNXKKXNNNMMXOXMMMMMMMMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMXO0XWXKKKXKXKXXK0KKKXKXXK0KKXKXKKXKXKXKXKXN0OKWXOKXKXKXKKKKXKX0O0KXKXKXKKKKXKXKXKXKKXKXKNWK00KNKXKXXKXKXKXKXKKXKKkxkkKKKWN000NMMMMMMMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWWNWWMXXXXNXNXXX0XWXXXXNK0NNXNXXXXXXXXNXNXNXKKXMNKXXXNXXXXXXNXNK0KXXXNXNXXXXXXNXNXXXXXXNXNNXWNXNXXXXXXXXNXNXXXXXXNKXNKNXXWXNWXXMMMMMMMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWNKXWWWWWWWWWWWNXXNWWWWMMWNNWWWWWWWWWWWWWWMMWXKXWWWMWWWWWWWWWWWWWWXKWWWWWWWWWWWWWWWWWWWWWWWWWWMMNKNMWWMWWWWWWWWWWWWWWWNNWNWWWMMWKNMWXXWMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMNOOKWXXXXXXXKXN0kKWXXXKNNKKXNXXXXXKXXXXXXXWWXOOKWXXMXKXXXXXNKXXXXXKOKXKXXXXKXXXXXXXXXXXXXXXXXXWN0O0NXXNXXXXXXXXXXXXXXOkkOOKXKWWKO0NMNNWMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "WMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMKO0NMKKKKXKXKXKO0XMKKXKX0XNKNKKKKXKXKKKKKKNWKO0NWKXWXKXKXKXXKXKXKKKOKKKKKKKKXKXKXKKKKKKXKXKKKKNXKWKXXKNKKKKKKXKKKKKKNO0KOOKKKNXKWXKWMMNNMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWXXNWMWWWNWWWWNXXNWWWWWWNNWWWWWWWWWWWWWWWWWWNXXNMMWWWWWWWWWWWWWNWNWXKWNNWWWWWNWWNWWWWWWWWWWWWWWWWWMWNNWWWWWWWWWWWWWWWWWWWWWWWWWWWMWNNWMWWMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMXOOXWWXNNNNXWWKO0NWNXWWKKNNNNNNNNNNNNNNNNNWWKO0NWNNNNNNNNNNNNNNNNNNK0XNXNNNNNNNNNNNNNNNNNNNNNNNNXWWXO0NMNXNNNNNNNNNNNNXK0OKKXNNNNNNK0WMMMMNXWMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMN0OKWWX0XKKKKWXOOKWWK0XKKXKKKKKKKKXKXKKKKKKWX0OKWXKXKKKKKKKKKKKXKXKKKO0XKKKKKKXKXKKKKKKXKXKXKKKKK0NX0XKKWX0XKKXKXKKKKKKNkk0kkKKKXKKKO0WMMMMWXNMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMNXXNMMWNNNNNNWNKXNMWNNXKNWNNNNNNNNNNNNNNNNNWNKXNMNNNNNNNNNNNNNNNNNNNXKNNNNNNNNNNNNNNNNNNNNNNNNNNNNWNNWWNWWNNNNNNNNNXNNNWXNWNXNNNNNNNXXWMMMMMMMMMWWMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMNKKXMMMWNWNWMWX0XWWWWNNKKWWWWNWWWWWWNWWWNWWNWX0XWWWWNWWWWWWWNWNWWWWWWK0XWWXXWNNWWWNWWWNWWWWWWWWWWNWWNWNNXNNWWNWMMWNXNWNWMKKNWNWWWWWWWNKXMMMMMMMMMNXXWMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWKO0NMMMNKXKXWXOOKMXXNKX00XXKXKXKKXKXKXKXKXXKXOO0XKKXKXXKXKXKKXKXKXKXXk0K000KXKXXKXKXKXKXXKXKXKXKXKKXKX0OKKOKXKNWNXXWMWNXNKOKXKXKKXXXKX0XMMMMMMMMMMWXXXWMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMNK0XWMMMNXXXXWX0KNMNXNXXK0XXXNXXXXXXNXXXXXXXXN00KXXXNXXXXXXXXXXXNXXXXXOKWKKNXXXXXXXXXXXXXXXXXXXXXXXXXXN0XWNKXNXXXXWWMMMMWWX0XXXXXXXXNXXKXMMMMMMMMMMMMWXXNMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWXKXWMMMWXXWWWWXXWWWWWWWWXKWWWWWWWWWWWWWWWWWWWXKXWWWWWWWWWWWWWWWWWWWWWWXXMMNNMNXWWWWWWWWWWWWWWWWWWWWWWWWWWWMMWXXWWWMMMMMWXNKKWWWWWWWWWWNXNMMMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMXOOKMMMMWK0XXXX0OXXKXXXXXKOKXXXXXXXXXXXXXXXXXK00KXXXXXXXXXXXXXXXXXXXXXXKKWMX0K0KNXXXXXXXXXXXXXXXXXXXXXNXXXXWNXXWWWWWWMWWK0KO0XXXXXXXXXXX0XMMMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWKO0NMMMMWKOKXKK0OKKKXKKKKKO0XKKKKXKXKXKKKKX00XX00XKKKKXKKXKKKKKKXKXKKKK00WMWXOKWXKXKXKKKKKKXKXKKKKKKXKXKKK0XXWWWWWMWWMWXKNXkOKXKKKKKKKKK0XMMMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMNXXNMMMMMWXXWNNNKXWWWWWWWWXKNWWWWWWWWWWWWWWNXNNXKNWWWWWWWWWWWWWWWWWWWWWWKKWMMMNNWWNNNWWWWWWWWWWWWWWWWWWWWNNNWMMMWWWMWWWNNWWWKXWWNWXNWWWWNKXMMMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMN0OKWMMMMMWKKNNNN0KNNNNNNNNK0XNNNNNNNNNNNNWNKXMXO0KNNNNNNNXNNNNNNNNNNNNNNX0NMMMWKKNKXNNNNNNNNNNNNNNNNNNNNNKXWMWWMWWMWWNKXNWXNK0NNNN0ONNNNX0XMMMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMKOOXMMMMMMWKOKKKK0OKKKKKXKKKO0XKKKKXKXKXKXN0KWMXO0OKKKXKKKOKKKKKXKXKKKKKKK0NMMMMN0kKWKKKKXKXKKKKKKKKXKXKKKKWWMWWMWWMWN0KXKNKK0O0KKK0OX0XX0OXMMMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWXKXWMMMMMMWXKNNNNKKNNNNNNNNXKXNNNNNNNNNNNNNXWWMXKKXNNNNNNXKNNNNNNNNNNNNNNXXWMMMMWNXNMNNNNNNNNNNNNNNNNNNNKXWWWMWWMWWMWNXWNNWNNXKNNNNXNWNWNXKNMMMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWX0XWMMMMMMWKKWWNWKKWWNWWWWWXKNWWWWWWWWNWMNXWWXNXKWWNWWNNX00KNNWWWWNWWNNXKXNWMMMMMMWXXWMMMMMMMMWWWNWMMWNXNWMMMMMMMMMMXXWWWWNWWKKNWNWXXMMMWXXWMMMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWKOKWMMMMMMWKOXXKXK0XXKXKXKKK0KXKXKKXKXKXN0KWNKKOkKKO00OKK00KNXXKKXKXKX0kkk0WMMMMMMMWNXXWMMMMMMXKXKNWNXXWWWWWWWMWWWWN0XWKXXKNKk0KXKX0KWWWWWNXXNMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWK0KWMMMMMMWK0XXXXK0XXXNXXXXK0KNXXXXXXNXXKXWXKXX00NNXNNXNNXXXNXXXXXXNXNKKNKXMMMMMMMMMMWNXXXXXXXKXXXXXXWMMWWWWWWMWWMNKXWWXXNXNX0KXXXXKKWWWWWMWNXNMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWX0XWMMMMMMWXXWWWWXXWWWWWWWWNNMNXNMWXKK0XXXWWWMMXKNWXXMWWMWNNWWWWWWWWWNNNWWMMMMMMWNXXXXNWX0KXXXNWWNNWWMMMMMMMMMMMMMWKXWWWWWWWWKKWWWWKKMMMMMMMMWWWMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWKOKWMMMMMMWK0XXXXK0XXXXXXXXKKNKO0NNOkOOXK0WMMMMK0KX0KMXKXKO0XXXXXXXXKkkk0NMMMMMMMMMMMMMMWNXXNMNXX0XWWWWMWWMWWMWWWWN0XXXXXXXXXO0XXXXKKWMMMMWWWMX0NMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMN0OKWMMMMMMWKOKKKKKOKKKXKKKKKKKKNKKK00KXK00NNNWWKO0K0KMWKkKNKKKKXKXKX0OKO0WMMMMMMMMMMMMMMMMMNXXK00XWWMWWMWWMWWMWWWWXOKKKXKXKKKkO0KKK00WWNNNNWWWXKNMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWX0XWMMMMMMWXXWWWNKXWNWWNXXNNXNWMWWNWNNWNX0KXXXXXKKK00NWWXNMWWWWWNWWWWWWWWMMMMMMMMMMMMMMMMMMMWNNNNWWWMWWWWWMWWWWWWWWXKNNXNWWWNXXNNWWNXX0xOKXWMWWXNMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWKOKWMMMMMMWKKNNNN00NNXNXO0NWX00KWNXNNXNWXOXMMMMMNXXXXNWN0OKNMWNNNNNNWXKWMMMMMMMMMMMMMMMWWWNXNWXKWWMWWMWWWWWMWWWWWMWNKKKKNNNWKKNXXKXNXXX0KWWMWWWKKMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMW0OKWMMMMMMWKOKKKK0OKX0XX00NNOkk0X0k0kOK0OOXMMMMMMMMMMMW0KKO0NWKKKKXKXKXMMMMMMMMMMMMMMWWNXXXXNX0NWWWWWWWWMWWMWWWWWWWWKOOXNKXX0XWXKKKNWWMMMWWMWWWKKMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWXKXWMMMMMMMXKNNXNKKNNXNWNXNWXK0KX000O0XNXKKXXKXXWMMWWWNNWWWNNWNXNNNNXNMMMMMMMMMMMMMMWNNWMMMMWXNMWWMWWMWWMWWMWWMWWMWXXNNXXNNNNMWNNWMMMMMMMWWMWWWXXNNNNWMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWKKNWMMMMMMMWKXXKNXKWWWWWWWX00KKKNXXXXXXNWNNWKKNNWWXO000NNWWWWMWXXWXXWMMMMMMMMMMMMMMMMMMMMMMWXNMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWNXXXXXXXNWMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMW0OKWMMMMMMMMX0OXWK0XXKXXXXOkxO00NMMMMMX0K0XXOXX00XX0KXXKKOxONMMN000XMMMMMMMMMMMMMMMMMMMMMMWK0NWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMN0XMMMMMWNWWMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWK0XWMMMMMMMNXXXXXK0XXXXXXXKX0KWKXMMMMMNXNXXNKKKOKNK00KKKK00XMMMMN0XMMMMMMMMMMMMMMMMMMMMMMMNKNWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWWWWWKXMMMMMMMWNWMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWWMMMMMMMMMWWMMWNKXWWWWWWWWWNNNXKNMMMMMNXNNXWNKKNWMWXXNWNWMWXNMMMMMMMMMMMMMMMMMMMMMMMMMMMMNKNMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMNNMMMMMMMMWXKWMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMW00XXXXXXXXXXXXKk0WMMMMWK00KWNXXNWWNXXNK0WMWK0WMMMMMMMMMMMMMMMMMMMMMMMMMMMX0XMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWWWWWWMWWWMN0XWMWWMWWWWX0NMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWKOKKKXKKKKK0Ok000KNWMWNK00KKKKXNNXXWNXXNMMWKKWMMMMMMMMMMMMMMMMMMMMMMMMMMMX0XWWWMWWMWWMWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWN0XWWWWWWWWWWKONMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWXXWNWWWWNWWNXKXXXXNWMWXKXK0KXXXXNXXWNNWWWWWNWMMMMMMMMMMMMMMMMMMMMMMMMMMMMWKXWWWMWWMWWMWWWWWMWWMWWMWWWWWWWWMWWMWWWWWWWMWWWWNWWWWWWWWWMWXXWMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWKKNNNNNNNNMXOKWMWKXMWXXX000KWWKXMWXXWWXXWWWXXWWWWWWWWWMMMMMWX00KWWX00XWWXXXNMMMMMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMMXKWMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWKOKXKXKKKKX0XKKWKKWWKKNXKKKXNKKWWKKNWK0NWNK0NWNNNNNNNWWWWWWWKOk0NW0Ok0WXOKNWWMMMMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMX0NMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWXKNNXNNNNNNNWNXXXWWNXNWXXXXXXXWMNXNWNXNMNXXNMWWWWWWWWWWWWWWWWWWWMMWWWWMWNWWNXNMMMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWWXNMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWKXXXXKWWWWWWNNN0KMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMX0NMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMN0KWMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMW0KKKK0XKXKXX0KX0OXWWWMMMWWWWWWMMMMMMMMMMMMMMMMMMMMMMWWMMMMMMMMMWWWWWWWWWWWWWX0XWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWWWWWWWWWWWWWWWWMWNKO0XMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMNKNXNXKXXXXXX0OOKK0KXNMWMNXXXXWMMMMMMMMMMMMMMMMMMMMMMWWMMMMMMMMMWWWWWWWWWWWWWX0XWMWWMWWMWWMWWMWWMWWMWWMWWWWWWWWWWWWWWWWWWWWMWWWWWWWXXWNKWMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWWMMMWXKNN0KNXKXKKXNNXXWMWNNNNWMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMNXNMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWMWNWMMMNXNMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMN0KKkOX0ONNXK0NWXKXWMWWWMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMX0XMMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWMMWWWWN0KWWWWW0KWMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWWWWMMNO0K0KOONNXXXNMMWXXXXXWMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMN0XWMWWMWWMWWMWWMWWWWWMWWWWWWWWWWWWWWWWWWWWWWWWWWWNKKWMWWWW0KMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWWMMMMMWWNXXNMWNXXNKKKXWWWMWWMMMMWNXXNMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWWXKKKXXWNKXXXNNWWMWWMWWMWWMWWMWWMWMWWMMWMMWMMWWMWWWNNWMMWWMNNWMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMNXXXNWWKXMMMMMWKO0XKx0XXXNXXWMMMMMMMWXXXNWMMMMMMMMMMWWNNNNNNNNNWMMWWWWWWMMMMMMMMMMWXXNX000KWX000KNXXNWMMWMMWMMWMMMMMMMMMMWMMWWMWMMWWMMMMWWMWWMWKXMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWXK0KNK0NMMMMMWK0Kk0000O0XK0WMMMMMMMWXK0KNMMWMMMMMMMN00XXXXXXXXWMWXXXXXNWMMMMMMMWXXNWMNKKKXNNKKKXWMWXXNWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWNNKXWMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWWMMWXNWMWXNWXXXXXKXNWKNXXXNNNNNWMMMMMMMWXNWMMWNXNWMMMWWWNWMWWMMMMMMMMMMMMMNNWMMMMMNXNWMMWXXXXXXXXXXXNMMMWXNWWWWWWWMWWMWWMWWMWWMWWMWWMWWMWWMWWMWNXNMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWXXWMMMMMMMNNMWWK0KKKXXNK0NXO0XNWWNWMMMMMMMMMMMMWXXNMMMWXXXNWMNNWNXXXNMMMMMMMMWXKXXWNXKKNWXKXXWNXKKNWNKKXWWXXNXXWMWWMMMMMMMMMMMMMMMMMMMMMMMMMMWNNNNWMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWNXXWMMMMMMMMKKWMN0XMMMMMX000000XKKK0NMMMMMMMMMMMMMWNXXWMWWX0OKWMMWWWWWWMMWWWXKXN0kk0NNKKKXNXKKKNNKKKXWXKKXNNXKNWNXXNWWWWWWWWWWWWMWWWWWWWMWWMMWXKXWK0WMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWXXWMMMMMMMMMMX0XNXNWMMMMMNXN0KX0XXNXXWMMMMMMMMMMMMMMMWNXNWNXXNWMMMWXXXXXNMX0XXKKXKOOONNKKKXNXKXXNXKXKXNXKXXNNXXNWMWNXNWWWWWWWWWWWWWMMWWWWWWWMMX0KXNXXWMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWWWWMMMMMMMMMMMMMWX0XMMMMMMWWMMWXXWX0XWWWWMMMMMMMMMMMMMMMMWWNKXWMMMMMMMNNNNXXNWNXXXWNXKXNWNNXNWWNXXNWNNNXNWNXXNWWNXXWWNXXNNWMMMMMMMMMMMMMMMWMMMMMMMMMWNWMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMW0KMMMMMMMMMMMMMMMMKxKMMMMMX0NMMMX0K0x0XKKXWMMMMMMMMMMMMMMMMMWXXXXWMMWNXXWNXKXNWXXXXWNXXKNWNKKXNWXKXXWNXXXNWXXXXWNXXXNWXXNWXKXWMWWMWWMWWMWWMWWMWWMWWMWKKWMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWKKWMMMMMMMMMMMMMMMM0x0NXNNXXNMMMMWKOK00KXNKNMMMMMMMMMMMMMMMMMMMMWXXNNXXWMWNXXKXNXXXXNNXXXXNXXXXNNXXXXNXXXXNNXXXXNNXXXNNXXXWMWXKNNWMWWMWWMWWMWWMWWWWWWKKNMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMNXWMMMMMMMMMMMMMMMMWXKXXKXNNWWNWMMMWNXNKXWMWWWMMMMMMMMMMMMMMMMMMMMMWNNWWMMMWWWWWWWXXKXXNWWWWWWXKXXKXNNNWWWWWWWWWWWWWWWWMWWWMMWMWNNNWWWMWWMWWMWWMWWWNXXXWMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMX0WMMMMMMMMMMMMMMMMMXKNMMMMMMWKKWMMMX0X00WMXOKWMMMMMMMMMMMMMMMMMMWXXNXXXNWNXXXWMXKXXXXXNWWNNMWNXXXXNWWXXNWMWNXXXWWXXXNWNXXXWWXXXNWXKXWMMWMMWMMMMMWXXXXXNWMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMK0WMMMMMMMMMMMMMMMMMN0XMMMMMMMK0WMMMWKkONMN0X0KWMMMMMMMMMMMMMMMMMX0XNXXXNNXXXXNX0XMMWMMMMWNNWMMMMMMMMMMWXKNWXXXXNNXXXXNNXXXNNXXXXWMWXKXWWWWWWWWNXXXWMMWXXXWMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMXKWMMMMMMMMMMMMMMMMWXXWWXKXXXKKNWMMMMWKXWWNNMNNWWMMMMMMMMMMMMMMWNXNWNXXXNNNXXXNXNWNNWWWWMMMMMMMWWWMMMMMMMWXXXXXXNNXXXXNNXXXNNXXXXWMMMWXNWWWNXNWXXWMMMMMMWXNWMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMXKWMMMMMMMMMMMMMMMMMNX00KKKKKKKXXXWMMMMWXXWMMMMWKXMMMMMMMMMMWNXXKKNXXKNWNXXXWMWXXXXXXXXXNMMMMMMWNXNMMMMMMMMWNXNMMMNXXXNWXKXXWNXKXNWNKNNXKXKXWWMMMMMMMMMMMNKNMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMK0WMMMMMMMMMMMMMMMMMMMK0WMMMMMWWX0XMMMMMWNXXWMWKO0XMMMMMMMWNXXWMX0XXKKXWXKKXNWKKWMMMMMWNXXNMMMMMMNXXNMMMMMMMWNXXWWNXKKNWXKKXNNXXKXWNKXNXKX0KMMMMMMMMMMMMMX0NMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMXKWMMMMMMMMMMMMMMMMMMWKKWMMMMMMWXKWMMMMMMMWNXNXKWXKWMMMMMWXXWMMMX0KXKKXNXKKKNXKWMMMMMMMMWNXXWMMMMMMNXXWMMMMMMMMNXXXXXKXNXKXXNNXXXXNXKXXXXX0XMMMMMMMMMMMMMX0NMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMNXWMMMMMMMMMMMMWWMMMMWXXWMMMMMWNKKXXXXWWMMMMWWWXNWMMMMMMWNWMMMMWNNNXNNWWNNWMMXXMWXXXNWMMWWWWWNNWMMMMMWWMMMMMMMMMWWNXWMMMWNNNNWWNXNWWNNNNWWNNWMMMMMMMMMMMMNXWMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMX0WMMMMMMMMMMMWXXWMMMMWNXXWMMMX0KXXXXNWWXNMMMMMWXXNMMMMN0XMMMMMWXKXXKXWWNXK0X0KN0KXXNWXXWMMMMWXXXWMMMMMMMMMMMMMMMMWXXXWMWXXKXWNXXXNWXXXXWWKKWMMMMMMMMMMMW0KMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMX0WMMMMMMMMMMMMWNNMMMMMMWXKXNX0XWMMMMMMMNNWMMMMMMNXXNMW0KWMMMMMMX0KXXXNWNXXKN000KWMMMMWNWMMMMMMWXXNWWMMMMMMMMMMMMMMMWXKXNXXXXNXXXKNNXXXXNK0NMMMMMMMMMMMWKKWMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMXKWMMMMMMMMMMMMMMMMWNMMMMMWNXXXXXXXNNWMMMWNWMMMMMMMNNWNNWMMMMMMMWNWWNNWWWWMMMNKNWMWWMMMMMMWWWMMMMWNWMMMMMMMMMMMMMMMMMMWNNNNWWWWWWWWWWWWWNNWMMMMMMMMMMMMNNWMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMXKWMMMMMMMMMMMMMMMMXXMMMMMMMMMNXXXXXKKNMWWXKNMMMMMMMNXNMMMMMMMMMMWNXXWMMMMMMNKNMMMN0XMMMMMWNKXWMMMMMMMMMMMMMMMMMMMMMMMMMWNKXWMMNXNWNXNWNKNMMMMMMMMMMMMNKWMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMX0WMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMN0XMMMX0XMMMMMMW0KWMMMMMMMMWNXXNMMMMMMMW0KWMMMW00WWMMMMMWXXXWMMMMMMMMMMMMMMMMMMMMMMMMMWXKXWNXXNXXNN0KWMMMMMMMMMMMN0XWMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMXKWMMMMMMMMMMMMMMMMMWNWMWWNWWMMMMWWNXXWMMWXXXXXXXNMXXWMMMMMMMWNXXNMMMMMMMMMXXWMMMMNXNNWWWMMMMMWXNWMMMMMMMMMMMMMMMMMMMMMMMMMMWXNNXXNXXNXXWMMMMMMMMMMMWXXWMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMNKNMMMMMMMMMMMMMMMMMMWXXXXXXXXXXXXXXXXXXXXXXXXKKNWWNKNMMMMMMWXNWWMMMMMMMMMMMNXNWMMMMMWWMWXXWMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWWMMMNNMMMMMMMMMMMMMNNMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMK0WMMMMMMMMMMMMMMMMWNXXWWWMMMMW0ONMMMMMWWWWWWNOOWMW0KWMMMMWXXXWMMMMMMMMMMMMMMWXXNWMMMNXNWWXXXWMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMNXNWN0XMMMMMMMMMMMMX0NMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMNKNMMMMMMMMMMMMMMMWXXWMMMMMMMMWKXMMMMMMMMMMMMWKXMMWXKNMMWNXNWMMMMMMMMMMMMMMMMMMWXXWMMMWWMMMWXXNMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWWNKXWMMMMMMMMMMMNKNMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWXNMMMMMMMMMMMMMMMNXNXXXXXXWWMMNXXXNWMWWXXXXXXXWMWWWWWWNNWWMMMMMMMMMMMMMMMMMMMMMMWWWNWMMMWXXXKXNWMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWKXMMMMMMMMMMMMXXWMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMKKMMMMMMMMMMMMMMNKO0NNWWWNKKWMWXXXNWNKXNNWWNNKKWNXWWXKXWMMMMMMMMMMMMMMMMMMMMMMMMMMWNXXNWW0OXNNNXXWMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMXXMMMMMMMMMMWWNNMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMKKMMMMMMMMMMMMMWKKK0NMMMMMKOKNXXXXXNXOKMMMMMWKKWK0XXXWMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWNXXNKKWMMWWXXXWMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWWWMMMMMMMMMNWMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWXXMMMMMMMMMMMMMWNNNXNMMMMMWXXNNXNNXNK0XWMMMMWNNXNXNWWMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMNXNWMMMMWWWNWMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWXXWMMMMMMMMNNMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMN0NMMMMMMMMMMMMMMMX0NNNNNNNNNWNNNNNNWK00KNWNNNNNXNMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWXKNMMNXNNXXNWMMMMMMMMMMMMMMMMMMMMMMMMMMMXKWMMMMMMMMKKWMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMN0XMMMMMMMMMMMMMMMK0NWWWWWWWWWWWWWWWWX0O0WWWWWNKKWMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWWMWNXXXXO0MWXKXWNK00NMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMK0WMMMMMMMMMMMMMMM" );
				Console.WriteLine( "WMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMNKNMMMMMMMMMMMMMMMNKXWNNNNWNNNWNNNNNNNK0XWNWNNNXWMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWNWMMMWWWNNWNXNWWXXWMMMMMWNKXXXXWMMMMMMMMMMMMMMMWWMNNMMMMMMMXKWMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMXKWMMMMMMMMMMMMMMMNXNNNWNNNNNNNNWNNNWWK0KNWMNXWMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWNXNXKKXNXXMMMMWNXNNXXXWWXXXXNNXOKNWMMMMMMMMMMMMMMMMMMNKNMMMMMMXKWMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMK0WMMMMMMMMMMMMMMMKONNNWNWNWNNNNWNWNWN0O0NWWKKWMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWNXXWWXXKXK0NMMWNXXWWXXXXNNXXXXWWKO0XMMMMMMMMMMMMMMMMMMMWNWMMMMMMK0WMMMMMMMMMMMMMMM" );
				Console.WriteLine( "WMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMKKWMMMMMMMMMMMMMMMXXNNNNWNWNNNNNNWNWMNKOKWN0KWMMMMMMMMMMMMMMMMMMMMMWNXXXXXNWMMMWNXXWMMNXXXXXKXNXXXWMMWXKXXNNXXXXWXKWXKWMMMMMMMMMMMMMMMMMMMMNNWMMMMK0WMMMMMMMMMMMMMMM" );
				Console.WriteLine( "WMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWXXMMMMMMMMMMMMMMMWXXNNNNNNNNNNNNNWNWMNXXNNXNWMMMMMMMMMMMMMMMMMMMMWWNXXXXKKKXXXNWNXNNWWNNNNWWNKKXNNNNNWWNNNWWWNWWNNNMWXNMMMMMMMMMMMMMMMMMMMMN0XMMMMNKNMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMW0KMMMMMMMMMMMMMMMN0KWNNWNNNNNNNNMWNWMN0XX0NMMMMMMMMMMMMMMMMMMMMWNKXMMMMWKXWXXXNWNXKXNWXKXXWNXKXNWXXKXWNXKKNWXKNWXXNMXOONMMMMMMMMMMMMMMMMMMMWXNMMMMN0XMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMW0KMMMMMMMMMMMMMMMN0XWWWWWWWWWWWWWWWWWN000XMMMMMMMMMMMMMMMMMMMMWKOKWMMMWK0NXXXXXNXXXKNNXXXXNXKXXXNXXXXNNKXKXNXKXXXKNN0XK0NMMMMMMMMMMMMMMMMMMMMWWMMMN0XMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWKXMMMMMMMMMMMMMMMWXNNNWNNNWNNNNNNWNXNWXXNMMMMMMMMMMMMMMMMMMMMMNNNWMMMMWNWWNXXXXNWNNNWWWWWWWWNWNWWNNWNWWNWNWWWNWWNNWWNWWXXWWWMMMMMMMMMMMMMMMMMWNWMMNKNMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMNKNMMMMMMMMMMMMMMMMXNWNNNNNNWNWNNNNNXWXXWWMMMMMMMMMMMMMMMMMMMNXNMMMMMNXXNXXXWNXXXNWXXXXWWXXXNWNXXXWWXXXXWNXXXNWXXNNXXNNXK0XMMMMMMMMMMMMMMMMMMMWNWMMNKXMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMX0NMMMMMMMMMMMMMMMMKXMWWWWWWWWWWWWWMMN0XMMMMMMMMMMMMMMMMMMMMWKKWMMMMWKKNXXXXNNXXXXNXXXXNNXXXXNNXXXNNXXXXNNXXXNNXXXXXXX0O00XMMMMMMMMMMMMMMMMMMMNKNMMN0XMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMNKNMMMMMMMMMMMMMMMWNNNNNNNNWNNNWNNNNWXXMMMMMMMMMMMMMMMMMMMMMNXWMMMMMNXWWXXXXNNXXXNNXXXXNNXXXXNNXXXNNXXXXNNXXXNNXXXXXXNNNXKNMMMMMMMMMMMMMMMMMMMNXWMMNKNMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWMMMMMMMMMMMMMMMMMMMWWWWWWWWWWWWWWWMWWMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWMMMMMMMMMMMMMMMMMMMMMMMMMWWMMMMMMMMMMMMMMMM" );
				Console.WriteLine( "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM" );
			}

			#endregion

			void Render()
			{
                // 이전 프레임 지우기..
                Console.Clear();

                for ( int i = 0; i < SWITCH_COUNT; ++i )
                {
                    int posX = switchX[i];
                    int posY = switchY[i];

                    RenderObject( posX, posY, SWITCH_IMAGE, SWITCH_COLOR );

                    posX += switchPushOffsetX[i];
                    posY += switchPushOffsetY[i];

                    RenderObject( posX, posY, SWITCH_PUSH_IMAGE, SWITCH_COLOR );
                }

                for ( int curPortalIdx = 0; curPortalIdx < PORTAL_COUNT; ++curPortalIdx )
                {
                    for ( int curPortalGateIdx = 0; curPortalGateIdx < PORTAL_GATE_COUNT; ++curPortalGateIdx )
                    {
                        int portalGateX = portalX[curPortalIdx, curPortalGateIdx];
                        int portalGateY = portalY[curPortalIdx, curPortalGateIdx];
						ConsoleColor color = portalColor[curPortalIdx];

						RenderObject( portalGateX, portalGateY, PORTAL_IMAGE, color );
                    }
                }

                // 박스 출력하기..
                for ( int i = 0; i < BOX_COUNT; ++i )
                {
					RenderObject( boxes[i].X, boxes[i].Y, BOX_IMAGE, BOX_COLOR );
                }

                // 골인 지점 출력하기..
                for ( int i = 0; i < GOAL_COUNT; ++i )
                {
                    if ( isGoalIn[i] )
                    {
						RenderObject( goalsX[i], goalsY[i], GOALIN_IMAGE, GOALIN_COLOR );
                    }
                    else
                    {
                        RenderObject( goalsX[i], goalsY[i], GOAL_IMAGE, GOAL_COLOR );
                    }
                }

				// 플레이어 출력하기..
				RenderObject( player.X, player.Y, player.Image, player.Color );

                // 벽 출력하기..
                for ( int i = 0; i < WALL_COUNT; ++i )
                {
                    if ( false == isWallActive[i] )
                        continue;

                    Console.SetCursorPosition( wallsX[i], wallsY[i] );
                    Console.ForegroundColor = WALL_COLOR;
                    Console.Write( WALL_IMAGE );
                }


                // 맵 출력하기..
                Console.ForegroundColor = BORDERLINE_COLOR;
                for ( int i = MAP_RANGE_MIN_X - 1; i < MAP_RANGE_MAX_X; ++i )
                {
                    Console.SetCursorPosition( i, MAP_RANGE_MIN_Y - 1 );
                    Console.Write( '▦' );
                    Console.SetCursorPosition( i, MAP_RANGE_MAX_Y - 1 );
                    Console.Write( '▦' );
                }
                for ( int i = MAP_RANGE_MIN_Y - 1; i < MAP_RANGE_MAX_Y; ++i )
                {
                    Console.SetCursorPosition( MAP_RANGE_MIN_X - 1, i );
                    Console.Write( '▦' );
                    Console.SetCursorPosition( MAP_RANGE_MAX_X - 1, i );
                    Console.Write( '▦' );
                }

                // Log Render..
                int logYOffset = 0;
                foreach ( var message in logMessage )
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition( logStartX, logStartY + logYOffset );
                    Console.Write( message.Value );

                    logYOffset += message.Key;
                }
            }

            void RenderObject(int x, int y, string image, ConsoleColor color)
			{
				ConsoleColor prevColor = Console.ForegroundColor;

                Console.ForegroundColor = color;
                Console.SetCursorPosition( x, y );
				Console.Write( image );

				Console.ForegroundColor = prevColor;
			}
		}
	}
}