using System;

namespace KMH_Sokoban
{
    public enum MapSpaceType
    {
        Pass            // 지나가는 곳..
        , DontPass      // 못지나가는 곳..
        , PlayerStand   // 플레이어가 있는 곳..
        , BoxStand      // 박스가 있는 곳..
    }

    class Program
    {
        static void Main()
        {
            // ------------------------------------------- 초기화(객체 생성 및 초기화).. -------------------------------------------

            #region 상수 초기화
            // ============================================================================================================================================
            // 상수 초기화..
            // ============================================================================================================================================

            // 초기 세팅 관련 상수 설정..
            const bool CURSOR_VISIBLE = false;                      // 커서를 숨긴다..
            const string TITLE_NAME = "Welcome To Liverpool";       // 타이틀을 설정한다..
            ConsoleColor BACKGROUND_COLOR = ConsoleColor.DarkRed;   // Background 색을 설정한다..
            ConsoleColor FOREGROUND_COLOR = ConsoleColor.White;     // 글꼴색을 설정한다..

            // 맵 사이즈 관련 상수 설정..
            const int MAP_WIDTH = 25;
            const int MAP_HEIGHT = 18;
            const int MAP_RANGE_MIN_X = 1;
            const int MAP_RANGE_MIN_Y = 1;
            const int MAP_RANGE_MAX_X = MAP_RANGE_MIN_X + MAP_WIDTH;
            const int MAP_RANGE_MAX_Y = MAP_RANGE_MIN_Y + MAP_HEIGHT;

            // 플레이어 관련 상수 설정..
            const int INITIAL_PLAYER_X = 1;
            const int INITIAL_PLAYER_Y = 1;
            const char PLAYER_IMAGE = 'K';

            // 박스 관련 상수 설정..
            const int BOX_COUNT = 5;
            const char BOX_IMAGE = 'B';
            #endregion


            #region 변수 초기화
            // ============================================================================================================================================
            // 변수 초기화..
            // ============================================================================================================================================

            // 플레이어 관련 변수 설정..
            int playerX = INITIAL_PLAYER_X, playerY = INITIAL_PLAYER_Y;
            int prevPlayerX = playerX, prevPlayerY = playerY;

            // 박스 관련 변수 설정..
            int[] boxesX = new int[BOX_COUNT] { 5, 6, 7, 8, 9 };
            int[] boxesY = new int[BOX_COUNT] { 5, 6, 7, 8, 9 };
            int[] prevBoxesX = new int[BOX_COUNT];
            int[] prevBoxesY = new int[BOX_COUNT];

            // 맵 관련 변수 설정..
            MapSpaceType[,] mapDatas = new MapSpaceType[MAP_HEIGHT + 1, MAP_WIDTH + 1];

            // 맵 외곽 통과 못하는 곳으로 설정..
            for (int i = 0; i <= MAP_WIDTH; ++i)
            {
                mapDatas[0, i] = MapSpaceType.DontPass;
                mapDatas[MAP_HEIGHT, i] = MapSpaceType.DontPass;
            }
            for (int i = 0; i <= MAP_HEIGHT; ++i)
            {
                mapDatas[i, 0] = MapSpaceType.DontPass;
                mapDatas[i, MAP_WIDTH] = MapSpaceType.DontPass;
            }
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
            mapDatas[playerY, playerX] = MapSpaceType.PlayerStand;
            for (int i = 0; i < BOX_COUNT; ++i)
                mapDatas[boxesY[i], boxesX[i]] = MapSpaceType.BoxStand;
            #endregion

            while (true)
            {
                #region Render
                // ------------------------------------------------------------------ Render.. ------------------------------------------------------------------
                // 이전 프레임 지우기..
                Console.Clear();

                // 플레이어 출력하기..
                Console.SetCursorPosition(playerX, playerY);
                Console.Write(PLAYER_IMAGE);

                // 박스 출력하기..
                for (int i = 0; i < BOX_COUNT; ++i)
                {
                    Console.SetCursorPosition(boxesX[i], boxesY[i]);
                    Console.Write(BOX_IMAGE);
                }

                // 맵 출력하기..
                for (int i = MAP_RANGE_MIN_X - 1; i < MAP_RANGE_MAX_X; ++i)
                {
                    Console.SetCursorPosition(i, MAP_RANGE_MIN_Y - 1);
                    Console.Write('-');
                    Console.SetCursorPosition(i, MAP_RANGE_MAX_Y - 1);
                    Console.Write('-');
                }
                for (int i = MAP_RANGE_MIN_Y - 1; i < MAP_RANGE_MAX_Y; ++i)
                {
                    Console.SetCursorPosition(MAP_RANGE_MIN_X - 1, i);
                    Console.Write('I');
                    Console.SetCursorPosition(MAP_RANGE_MAX_X - 1, i);
                    Console.Write('I');
                }
                #endregion

                // --------------------------------------------------------------- ProcessInput.. ---------------------------------------------------------------
                ConsoleKey inputKey = Console.ReadKey().Key;

                // ------------------------------------------------------------------ Update.. ------------------------------------------------------------------
                // ================================= Player Update.. =================================
                // 플레이어 이전위치 갱신..
                prevPlayerX = playerX;
                prevPlayerY = playerY;

                // 1. 이동 입력 값 처리..
                if (inputKey == ConsoleKey.RightArrow || inputKey == ConsoleKey.LeftArrow)
                    playerX += (int)inputKey - 38;
                if (inputKey == ConsoleKey.DownArrow || inputKey == ConsoleKey.UpArrow)
                    playerY += (int)inputKey - 39;

                // ================================= Box Update.. =================================
                // 박스 업데이트 할게 있나?
                // 일단 이전위치 갱신..
                for (int i = 0; i < BOX_COUNT; ++i)
                {
                    // 여기서 해야하나??
                    // 박스와 플레이어의 위치가 같을 경우 움직이는건데..
                    // 박스 이전위치 갱신..
                    prevBoxesX[i] = boxesX[i];
                    prevBoxesY[i] = boxesY[i];

                    if (playerX == boxesX[i] && playerY == boxesY[i])
                    {
                        // 박스가 이동할 위치를 계산..
                        int boxMoveDirX = playerX - prevPlayerX;
                        int boxMoveDirY = playerY - prevPlayerY;

                        // 박스 현재위치 갱신..
                        boxesX[i] += boxMoveDirX;
                        boxesY[i] += boxMoveDirY;
                    }
                }

                // ================================= Player Box Update가 끝난 후 Overlap 처리??.. =================================
                // 박스가 특정 물체와 겹쳤다( 박스 or 기타 오브젝트(벽) )
                for (int i = 0; i < BOX_COUNT; ++i)
                {
                    MapSpaceType curStandSpaceType = mapDatas[boxesY[i], boxesX[i]];

                    if (MapSpaceType.Pass == curStandSpaceType)
                        continue;

                    boxesX[i] = prevBoxesX[i];
                    boxesY[i] = prevBoxesY[i];

                    break;
                }

                // 플레이어가 특정 물체와 겹쳤다( 박스 or 벽 등등 )..
                MapSpaceType overlapSpaceType = mapDatas[playerY, playerX];
                if (overlapSpaceType == MapSpaceType.DontPass)     // 벽이랑 겹쳐있다( 못지나감 )..
                {
                    playerX = prevPlayerX;
                    playerY = prevPlayerY;
                }
                else if (overlapSpaceType == MapSpaceType.BoxStand)  // 박스랑 겹쳐있다..
                {
                    // 만약 플레이어와 박스가 겹쳐있다면..
                    for (int i = 0; i < BOX_COUNT; ++i)
                    {
                        if (boxesX[i] == playerX && boxesY[i] == playerY)
                        {
                            playerX = prevPlayerX;
                            playerY = prevPlayerY;
                        }
                    }
                }

                // ================================= 전체적인 위치의 갱신이 끝났다면 맵에 데이터 저장.. =================================
                mapDatas[prevPlayerY, prevPlayerX] = MapSpaceType.Pass;
                mapDatas[playerY, playerX] = MapSpaceType.PlayerStand;
                for (int i = 0; i < BOX_COUNT; ++i)
                {
                    mapDatas[prevBoxesY[i], prevBoxesX[i]] = MapSpaceType.Pass;
                    mapDatas[boxesY[i], boxesX[i]] = MapSpaceType.BoxStand;
                }
            }
        }
    }
}