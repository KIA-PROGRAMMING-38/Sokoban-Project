using System;

class Program
{
    static void Main()
    {
        // 초기 세팅..
        Console.ResetColor();                               // 컬러를 초기화한다..
        Console.CursorVisible = false;                      // 커서를 숨긴다..
        Console.Title = "Welcome To Liverpool";             // 타이틀을 설정한다..
        Console.BackgroundColor = ConsoleColor.DarkRed;     // Background 색을 설정한다..
        Console.ForegroundColor = ConsoleColor.White;       // 글꼴색을 설정한다..
        Console.Clear();                                    // 출력된 모든 내용을 지운다..


        // 콘솔 최대 사이즈 119, 29..

        // 플레이어 관련 객체 설정..
        int playerX = 1, playerY = 1;
        int prevPlayerX = playerX, prevPlayerY = playerY;
        char playerImage = 'K';

        // 박스 관련 객체 설정..
        // 박스 정보 문자열에 들어갈 정보 == 박스개수 박스1X 박스1Y ... 박스nX, 박스nY
        string boxInfomationStr = "5 5 5 6 6 7 7 8 8 9 9";
        string[] boxInfoArray = boxInfomationStr.Split(' ');

        int boxCount = int.Parse(boxInfoArray[0]);
        int[] boxesX = new int[boxCount];
        int[] boxesY = new int[boxCount];
        int[] prevBoxesX = new int[boxCount];
        int[] prevBoxesY = new int[boxCount];
        char boxImage = 'B';

        // 박스 정보 문자열에 들어있는 정보 int로 파싱해서 Box 현재위치 저장하는 객체에 담기..
        for( int i = 0; i < boxCount; ++i )
        {
            boxesX[i] = int.Parse( boxInfoArray[i * 2 + 1] );
            boxesY[i] = int.Parse( boxInfoArray[i * 2 + 2] );
        }

        // 게임 루프 == 프레임(Frame)..
        // 25 * 20
        int mapWidth = 25;
        int mapHeight = 18;

        // 0 == 이동가능한 곳
        // 1 == 플레이어가 서있는 곳
        // 2~n == Box가 인덱스 번호대로( 2 == box[0], 3 == box[1] )
        // -1 == 벽 or 맵 외곽( 넌 못지나간다 )
        int[,] mapDatas = new int[mapHeight + 1, mapWidth + 1];

        // 맵 외곽 -1로 만들기..
        for( int i = 0; i <= mapWidth; ++i )
        {
            mapDatas[0, i] = -1;
            mapDatas[mapHeight, i] = -1;
        }

        for ( int i = 0; i <= mapHeight; ++i )
        {
            mapDatas[i, 0] = -1;
            mapDatas[i, mapWidth] = -1;
        }

        // 시작 전에 맵 데이터에 플레이어 박스 위치 저장..
        mapDatas[playerY, playerX] = 1;
        for ( int i = 0; i < boxCount; ++i )
            mapDatas[boxesY[i], boxesX[i]] = i + 2;

        while ( true )
        {
            // ------------------------------------------------------------------ Render.. ------------------------------------------------------------------
            // 이전 프레임 지우기..
            Console.Clear();

            // 플레이어 출력하기..
            Console.SetCursorPosition( playerX, playerY );
            Console.Write( playerImage );

            // 박스 출력하기..
            for ( int i = 0; i < boxCount; ++i )
            {
                Console.SetCursorPosition( boxesX[i], boxesY[i] );
                Console.Write( boxImage );
            }

            // 맵 출력하기..
            for( int i = 1; i <= mapWidth; ++i )
            {
                Console.SetCursorPosition( i, 0 );
                Console.Write( '-' );
                Console.SetCursorPosition( i, mapHeight );
                Console.Write( '-' );
            }

            for ( int i = 1; i <= mapHeight; ++i )
            {
                Console.SetCursorPosition( 0, i );
                Console.Write( 'I' );
                Console.SetCursorPosition( mapWidth, i );
                Console.Write( 'I' );
            }

            // ------------------------------------------------------------------ ProcessInput.. ------------------------------------------------------------------
            ConsoleKey inputKey = Console.ReadKey().Key;

            // ------------------------------------------------------------------ Update.. ------------------------------------------------------------------
            // ================================= Player Update.. =================================
            // 1. 이동 입력 값 처리..
            int moveDirX = 0;
            int moveDirY = 0;

            if ( inputKey == ConsoleKey.RightArrow || inputKey == ConsoleKey.LeftArrow )
                moveDirX = (int)inputKey - 38;
            if ( inputKey == ConsoleKey.DownArrow || inputKey == ConsoleKey.UpArrow )
                moveDirY = (int)inputKey - 39;

            // 이동이 있는 경우에만 연산 ㄱㄱ..
            if ( 0 != moveDirX || 0 != moveDirY )
            {
                int playerXAfterMove = playerX + moveDirX;
                int playerYAfterMove = playerY + moveDirY;

                // 2. 플레이어 이전위치 현재위치 갱신..
                prevPlayerX = playerX;
                prevPlayerY = playerY;

                playerX = playerXAfterMove;
                playerY = playerYAfterMove;

                //// 플레이어 맵 밖으로 나갔을 때 예외처리..
                //if ( playerXAfterMove <= mapWidth && playerXAfterMove >= 0
                //        && playerYAfterMove <= mapHeight && playerYAfterMove >= 0 )
                //{
                //    // 2. 플레이어 이전위치 현재위치 갱신..
                //    prevPlayerX = playerX;
                //    prevPlayerY = playerY;
                //
                //    playerX = playerXAfterMove;
                //    playerY = playerYAfterMove;
                //}
            }

            // ================================= Box Update.. =================================
            // 박스 업데이트 할게 있나?
            // 일단 이전위치 갱신..
            for( int i = 0; i < boxCount; ++i )
            {
                // 여기서 해야하나??
                // 박스와 플레이어의 위치가 같을 경우 움직이는건데..
                if ( playerX == boxesX[i] && playerY == boxesY[i] )
                {
                    int box1XAfterMove = boxesX[i] + moveDirX;
                    int box1YAfterMove = boxesY[i] + moveDirY;

                    // 박스 이전위치 현재위치 갱신..
                    prevBoxesX[i] = boxesX[i];
                    prevBoxesY[i] = boxesY[i];

                    boxesX[i] = box1XAfterMove;
                    boxesY[i] = box1YAfterMove;

                    //int box1XAfterMove = boxesX[i] + moveDirX;
                    //int box1YAfterMove = boxesY[i] + moveDirY;
                    //
                    //// 박스가 맵 밖으로 나가지 않았을 때 위치 갱신..
                    //if ( box1XAfterMove <= mapWidth && box1XAfterMove >= 0
                    //    && box1YAfterMove <= mapHeight && box1YAfterMove >= 0 )
                    //{
                    //    // 박스 위치 갱신..
                    //    boxesX[i] = box1XAfterMove;
                    //    boxesY[i] = box1YAfterMove;
                    //}
                }
            }

            // ================================= Player Box Update가 끝난 후 충돌 처리??.. =================================
            // 박스가 특정 물체와 겹쳤다( 박스 or 기타 오브젝트(벽) )
            for( int i = 0; i < boxCount; ++i )
            {
                int curBoxSpaceMapData = mapDatas[boxesY[i], boxesX[i]];

                if ( 0 == curBoxSpaceMapData || i + 2 == curBoxSpaceMapData )
                    continue;

                boxesX[i] = prevBoxesX[i];
                boxesY[i] = prevBoxesY[i];
            }

            // 플레이어가 특정 물체와 겹쳤다( 박스 or 기타 오브젝트(벽) )
            int overlapData = mapDatas[playerY, playerX];
            if( overlapData == -1 )     // 벽이랑 겹쳐있다( 못지나감 )..
            {
                playerX = prevPlayerX;
                playerY = prevPlayerY;
            }
            else if( overlapData > 1 )  // 박스랑 겹쳐있다..
            {
                overlapData -= 2;
                // 만약 플레이어와 박스가 겹쳐있다면
                if( boxesX[overlapData] == playerX && boxesY[overlapData] == playerY )
                {
                    playerX = prevPlayerX;
                    playerY = prevPlayerY;
                }
            }

            // ================================= 전체적인 위치의 갱신이 끝났다면 맵에 데이터 저장.. =================================
            mapDatas[prevPlayerY, prevPlayerX] = 0;
            mapDatas[playerY, playerX] = 1;
            for ( int i = 0; i < boxCount; ++i )
            {
                mapDatas[prevBoxesY[i], prevBoxesX[i]] = 0;
                mapDatas[boxesY[i], boxesX[i]] = i + 2;
            }
        }
    }
}