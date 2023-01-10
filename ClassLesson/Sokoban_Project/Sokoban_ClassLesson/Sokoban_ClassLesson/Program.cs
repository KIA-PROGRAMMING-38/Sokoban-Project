using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Sokoban_ClassLesson
{
	public struct TestClass
	{
		long a;
		long b;
		long c;
	}

	internal class Sokoban
	{
		public enum Direction   // 방향을 저장하는 타입..
		{
			None
			, Left
			, Right
			, Up
			, Down
		}

		static void Main()
		{
			#region 주석
			//// a와 b중 최댓값을 구한다..
			//int Max( int a, int b )
			//{
			//	return (a > b) ? a : b;
			//}
			//
			//int VariadicMax( params int[] numbers )
			//{
			//	int result = int.MinValue;
			//
			//	for ( int index = 0; index < numbers.Length; ++index )
			//	{
			//		if ( result < numbers[index] )
			//			result = numbers[index];
			//	}
			//
			//	return result;
			//}
			//
			//int Min( int a, int b )
			//{
			//	return (a > b) ? b : a;
			//}
			//
			//void Swap( int a, int b )
			//{
			//	int tmp = a;
			//	a = b;
			//	b = tmp;
			//}
			//
			//void Test( int a, int b, int c, int d, int e, TestClass f )
			//{
			//	int tmp = a;
			//	a = b;
			//	b = tmp;
			//}
			//
			//void Test2( in int c, out int b, int d )
			//{
			//	b = 0;
			//	d = 60;
			//}
			//
			//int a = 10;
			//int b = 20;
			//int c = 30;
			//Swap( a, b );
			//
			//Test2( in a, out b, c );
			//
			//Console.WriteLine( $"a : {a}, b : {b}" );

			//return;
			#endregion

			#region Initialize
			#region 초기 세팅
			// ================== 초기 세팅 ==================
			Console.ResetColor();                               // 컬러를 초기화..
			Console.CursorVisible = false;                      // 커서를 숨긴다..
			Console.Title = "Welcome to Liverpool";             // 타이틀을 설정한다..
			Console.BackgroundColor = ConsoleColor.DarkGreen;   // 배경색을 설정한다..
			Console.ForegroundColor = ConsoleColor.Red;         // 글꼴색을 설정한다..
			Console.Clear();                                    // 기존의 콘솔창에 내용을 지운다..
			#endregion

			#region 플레이어 관련 변수 선언
			// 플레이어 데이터를 저장하기 위한 변수..
			int playerX = 0;
			int playerY = 0;
			Direction playerMoveDirection = Direction.None;
			#endregion

			#region 박스 관련 변수 선언
			// 박스 데이터를 저장하기 위한 변수..
			const int BOX_COUNT = 3;

			int[] boxX = new int[BOX_COUNT] { 3, 5, 3 };
			int[] boxY = new int[BOX_COUNT] { 2, 9, 6 };

			int pushedBoxID = 0;
			#endregion

			#region 벽 관련 변수 선언
			// 박스 데이터를 저장하기 위한 변수..
			const int WALL_COUNT = 3;

			int[] wallX = new int[WALL_COUNT] { 2, 5, 7 };
			int[] wallY = new int[WALL_COUNT] { 2, 5, 7 };
			#endregion

			#region 골 관련 변수 선언
			// 박스 데이터를 저장하기 위한 변수..
			const int GOAL_COUNT = BOX_COUNT;

			int[] goalX = new int[GOAL_COUNT] { 1, 6, 4 };
			int[] goalY = new int[GOAL_COUNT] { 1, 4, 9 };

			bool[] isBoxOnGoal = new bool[GOAL_COUNT];
			#endregion

			#region 인풋 관련 변수 선언
			ConsoleKey inputKey = ConsoleKey.NoName;
			#endregion
			#endregion

			#region GameLoop
			// 게임 루프 구성
			while ( true )
			{
				#region Render
				// ==================== Render ==================== //
				Render();
				#endregion

				#region Process Input
				// ================= Process Input ================ //
				inputKey = Console.ReadKey().Key;
				#endregion

				#region Update
				// ==================== Update ==================== //

				#region Collision
				#region Player Wall Collision
				// Player와 벽이 충돌했을 경우..
				for ( int i = 0; i < WALL_COUNT; ++i )
				{
					if ( playerX == wallX[i] && playerY == wallY[i] )
					{
						switch ( playerMoveDirection )
						{
							case Direction.Left:
								playerX = playerX + 1;
								break;
							case Direction.Right:
								playerX = playerX - 1;
								break;
							case Direction.Up:
								playerY = playerY + 1;
								break;
							case Direction.Down:
								playerY = playerY - 1;
								break;
							default:
								Console.Clear();
								Console.WriteLine( $"[Error] 플레이어 이동 방향 데이터가 오류입니다 : {playerMoveDirection}" );
								return;
						}

						break;
					}
				}
				#endregion

				#region Box Box Collision
				// 박스끼리 충돌..
				if ( -1 != pushedBoxID )    // 플레이어와 박스가 부딪혔을 때만 검사한다..
				{
					for ( int collideBoxID = 0; collideBoxID < BOX_COUNT; ++collideBoxID )
					{
						if ( pushedBoxID == collideBoxID )  // 같은 박스라면 검사할 필요가 없다..
							continue;

						if ( boxX[collideBoxID] == boxX[pushedBoxID] && boxY[collideBoxID] == boxY[pushedBoxID] )
						{
							switch ( playerMoveDirection )
							{
								case Direction.Left:
									boxX[pushedBoxID] = boxX[collideBoxID] + 1;
									playerX = boxX[pushedBoxID] + 1;

									break;
								case Direction.Right:
									boxX[pushedBoxID] = boxX[collideBoxID] - 1;
									playerX = boxX[pushedBoxID] - 1;

									break;
								case Direction.Up:
									boxY[pushedBoxID] = boxY[collideBoxID] + 1;
									playerY = boxY[pushedBoxID] + 1;

									break;
								case Direction.Down:
									boxY[pushedBoxID] = boxY[collideBoxID] - 1;
									playerY = boxY[pushedBoxID] - 1;

									break;
								default:
									Console.Clear();
									Console.WriteLine( $"[Error] 플레이어 이동 방향 데이터가 오류입니다 : {playerMoveDirection}" );
									return;
							}

							break;
						}
					}
				}
				#endregion

				#region Box Wall Collision
				// 박스와 벽 충돌..
				for ( int i = 0; i < BOX_COUNT; ++i )
				{
					for ( int j = 0; j < WALL_COUNT; ++j )
					{
						if ( boxX[i] == wallX[j] && boxY[i] == wallY[j] )
						{
							switch ( playerMoveDirection )
							{
								case Direction.Left:
									boxX[i] = wallX[j] + 1;
									playerX = boxX[i] + 1;
									break;
								case Direction.Right:
									boxX[i] = wallX[j] - 1;
									playerX = boxX[i] - 1;
									break;
								case Direction.Up:
									boxY[i] = wallY[j] + 1;
									playerY = boxY[i] + 1;
									break;
								case Direction.Down:
									boxY[i] = wallY[j] - 1;
									playerY = boxY[i] - 1;
									break;
								default:
									Console.Clear();
									Console.WriteLine( $"[Error] 플레이어 이동 방향 데이터가 오류입니다 : {playerMoveDirection}" );
									return;
							}
						}
					}
				}
				#endregion

				#region Compute Goal in Count
				// 골인 카운트 개수..
				int goalCount = 0;
				for ( int i = 0; i < GOAL_COUNT; ++i )
				{
					isBoxOnGoal[i] = false;
					for ( int j = 0; j < BOX_COUNT; ++j )
					{
						if ( goalX[i] == boxX[j] && goalY[i] == boxY[j] )
						{
							++goalCount;
							isBoxOnGoal[i] = true;

							break;
						}
					}
				}

				Console.Write( "N" );

				if ( goalCount == BOX_COUNT )
					break;
				#endregion
				#endregion
			}
			#endregion

			#region Render 관련 함수
			void Render()
			{
				Console.Clear();    // 이전 화면을 지운다..

				// 박스를 그린다..
				for ( int i = 0; i < BOX_COUNT; ++i )
					ObjectRender( boxX[i], boxY[i], "B" );

				// 골을 그린다..
				for ( int i = 0; i < GOAL_COUNT; ++i )
				{
					if ( isBoxOnGoal[i] )
						ObjectRender( goalX[i], goalY[i], "★" );
					else
						ObjectRender( goalX[i], goalY[i], "G" );
				}

				// 플레이어를 그린다..
				ObjectRender( playerX, playerY, "P" );

				// 벽을 그린다..
				for ( int i = 0; i < WALL_COUNT; ++i )
					ObjectRender( wallX[i], wallY[i], "W" );
			}

			void ObjectRender(int _x, int _y, string _image)
			{
				Console.SetCursorPosition( _x, _y );
				Console.Write( _image );
			}
			#endregion

			#region ProcessInput 관련 함수
			#endregion

			#region Update 관련 함수
			void Update()
			{
				// Player Update..
				playerMoveDirection = Direction.None;
				if ( inputKey == ConsoleKey.LeftArrow )
				{
					playerMoveDirection = Direction.Left;
					playerX = Math.Max( 0, playerX - 1 );
				}
				if ( inputKey == ConsoleKey.RightArrow )
				{
					playerMoveDirection = Direction.Right;
					playerX = Math.Min( 20, playerX + 1 );
				}
				if ( inputKey == ConsoleKey.UpArrow )
				{
					playerMoveDirection = Direction.Up;
					playerY = Math.Max( 0, playerY - 1 );
				}
				if ( inputKey == ConsoleKey.DownArrow )
				{
					playerMoveDirection = Direction.Down;
					playerY = Math.Min( 10, playerY + 1 );
				}
				#endregion

				#region Box Update
				// Box Update..
				pushedBoxID = -1;
				for ( int i = 0; i < boxX.Length; ++i )
				{
					if ( playerX == boxX[i] && playerY == boxY[i] )
					{
						pushedBoxID = i;

						switch ( playerMoveDirection )
						{
							case Direction.Left:
								boxX[i] = Math.Max( 0, boxX[i] - 1 );
								playerX = boxX[i] + 1;
								break;
							case Direction.Right:
								boxX[i] = Math.Min( 20, boxX[i] + 1 );
								playerX = boxX[i] - 1;
								break;
							case Direction.Up:
								boxY[i] = Math.Max( 0, boxY[i] - 1 );
								playerY = boxY[i] + 1;
								break;
							case Direction.Down:
								boxY[i] = Math.Min( 10, boxY[i] + 1 );
								playerY = boxY[i] - 1;
								break;
							default:
								Console.Clear();
								Console.WriteLine( $"[Error] 플레이어 이동 방향 데이터가 오류입니다 : {playerMoveDirection}" );
								return;
						}

						break;
					}
				}
				#endregion
			}
			#endregion

			#region Clear Message
			Console.Clear();
			Console.WriteLine( "ㅊㅊ" );
			#endregion
		}
	}
}
