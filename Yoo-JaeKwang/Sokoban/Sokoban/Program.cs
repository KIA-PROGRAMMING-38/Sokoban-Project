// 초기 세팅
Console.ResetColor();                                   // 컬러를 초기화한다.
Console.CursorVisible = false;                          // 커서를 숨긴다.
Console.Title = "경이루 아카데미";                       // 타이틀을 설정한다.
Console.BackgroundColor = ConsoleColor.Magenta;         // 배경색을 설정한다.
Console.ForegroundColor = ConsoleColor.Yellow;         // 글꼴색을 설정한다.
Console.Clear();                                       // 출력된 모든 내용을 지운다.


const int DIRECTION_UP = 1;
const int DIRECTION_DOWN = 2;
const int DIRECTION_LEFT = 3;
const int DIRECTION_RIGHT = 4;
const int MAP_HEIGHT_MAX = 20;
const int MAP_HEIGHT_MIN = 0;
const int MAP_WIDTH_MAX = 30;
const int MAP_WIDTH_MIN = 0;
const string PLAYER_SYMBOL = "P";
const string BOX_SYMBOL = "B";
const int PLAYER_INITIAL_X_COORDINATE = 3;
const int PLAYER_INITIAL_Y_COORDINATE = 2;
const int BOX_INITIAL_X_COORDINATE = 10;
const int BOX_INITIAL_Y_COORDINATE = 5;
int playerX = PLAYER_INITIAL_X_COORDINATE;
int playerY = PLAYER_INITIAL_Y_COORDINATE;
int boxX = BOX_INITIAL_X_COORDINATE;
int boxY = BOX_INITIAL_Y_COORDINATE;
int playerDirection = default;

// 게임 루프 == 프레임(Frame)
while (true)
{
    Console.Clear();

    // -------------------------------------- Render ------------------------------------------------
    Console.SetCursorPosition(playerX, playerY);
    Console.Write(PLAYER_SYMBOL);
    Console.SetCursorPosition(boxX, boxY);
    Console.Write(BOX_SYMBOL);
    // -------------------------------------- ProcessInput ------------------------------------------------
    ConsoleKey playerKey = Console.ReadKey().Key; // ConsoleKeyInfo keyInfo = Console.ReadKey(); ConsoleKey key = keyInfo.Key;
    // -------------------------------------- Update ------------------------------------------------
    // 플레이어
    if (playerKey == ConsoleKey.UpArrow)
    {
        playerY = Math.Max(MAP_HEIGHT_MIN, playerY - 1);
        playerDirection = DIRECTION_UP;
    }
    if (playerKey == ConsoleKey.DownArrow)
    {
        playerY = Math.Min(playerY + 1, MAP_HEIGHT_MAX);
        playerDirection = DIRECTION_DOWN;
    }
    if (playerKey == ConsoleKey.LeftArrow)
    {
        playerX = Math.Max(MAP_WIDTH_MIN, playerX - 1);
        playerDirection = DIRECTION_LEFT;
    }
    if (playerKey == ConsoleKey.RightArrow)
    {
        playerX = Math.Min(playerX + 1, MAP_WIDTH_MAX);
        playerDirection = DIRECTION_RIGHT;
    }
    // 박스
    if (playerX == boxX && playerY == boxY)
    {
        switch (playerDirection)
        {
            case DIRECTION_UP:
                if (boxY == MAP_HEIGHT_MIN)
                {
                    playerY = MAP_HEIGHT_MIN + 1;
                }
                else
                {
                    --boxY;
                }
                break;
            case DIRECTION_DOWN:
                if (boxY == MAP_HEIGHT_MAX)
                {
                    playerY = MAP_HEIGHT_MAX - 1;
                }
                else
                {
                    ++boxY;
                }
                break;
            case DIRECTION_LEFT:
                if (boxX == MAP_WIDTH_MIN)
                {
                    playerX = MAP_WIDTH_MIN + 1;
                }
                else
                {
                    --boxX;
                }
                break;
            case DIRECTION_RIGHT:
                if (boxX == MAP_WIDTH_MAX)
                {
                    playerX = MAP_WIDTH_MAX - 1;
                }
                else
                {
                    ++boxX;
                }
                break;
            default:
                Console.Clear();
                Console.WriteLine($"[Error] 플레이어의 이동 방향이 잘못되었습니다.");

                return;
        }
    }
}