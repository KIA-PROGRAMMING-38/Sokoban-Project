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
        int playerX = 0, playerY = 0;
        char playerImage = 'K';

        // 박스 관련 객체 설정..
        int box1X = 5;
        int box1Y = 5;
        char boxImage = 'B';

        // 게임 루프 == 프레임(Frame)..
        // 25 * 20
        int mapWidth = 25;
        int mapHeight = 20;

        while(true)
        {
            // ------------------------------------------------------------------ Render.. ------------------------------------------------------------------
            // 이전 프레임 지우기..
            Console.Clear();
            
            // 플레이어 출력하기..
            Console.SetCursorPosition(playerX, playerY);
            Console.Write(playerImage);

            // 박스 출력하기..
            Console.SetCursorPosition(box1X, box1Y);
            Console.Write(boxImage);

            // ------------------------------------------------------------------ ProcessInput.. ------------------------------------------------------------------
            ConsoleKey inputKey = Console.ReadKey().Key;

            // ------------------------------------------------------------------ Update.. ------------------------------------------------------------------
            // 오른쪽 화살표키를 눌렀을 때 오른쪽으로 이동..
            int moveDirX = 0;
            int moveDirY = 0;

            // 오른쪽 왼쪽 이동 처리..
            if( inputKey == ConsoleKey.RightArrow || inputKey == ConsoleKey.LeftArrow)
                moveDirX = (int)inputKey - 38;
            // 위 아래(Like EXID) 이동 처리..
            if (inputKey == ConsoleKey.DownArrow || inputKey == ConsoleKey.UpArrow)
                moveDirY = (int)inputKey - 39;

            // 이동이 없을 경우 연산할 필요 없으므로 continue..
            if (0 == moveDirX && 0 == moveDirY)
                continue;

            // 플레이어 이동();..
            // if( 이동한 지점에 물체가 있는지 검사 )..
            //      없다면 이동..
            // else
            // {
            //      if(물체가 옮길수 있는애인가)
            //      {
            //          if(다음 위치에 물체가 있는지 검사)
            //              continue;
            //          else
            //              플레이어 이동(); 물체 이동();
            //      }
            //      else
            //          플레이어 이동(); 물체 이동();
            // }

            int playerXAfterMove = playerX + moveDirX;
            int playerYAfterMove = playerY + moveDirY;

            // 플레이어의 이동 지점이 박스의 위치와 같을 때..
            if (playerXAfterMove == box1X && playerYAfterMove == box1Y)
            {
                int box1XAfterMove = box1X + moveDirX;
                int box1YAfterMove = box1Y + moveDirY;

                // 박스가 맵 밖으로 나갔다면 continue..
                if (box1XAfterMove > mapWidth || box1XAfterMove < 0
                    || box1YAfterMove > mapHeight || box1YAfterMove < 0)
                    continue;

                // 박스 위치 갱신..
                box1X = box1XAfterMove;
                box1Y = box1YAfterMove;
            }

            // 플레이어 맵 밖으로 나갔을 때 예외처리..
            if (playerXAfterMove > mapWidth || playerXAfterMove < 0
                || playerYAfterMove > mapHeight || playerYAfterMove < 0)
                continue;

            // 플레이어 위치 갱신..
            playerX = playerXAfterMove;
            playerY = playerYAfterMove;
        }
    }
}