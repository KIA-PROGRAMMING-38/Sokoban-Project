using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    internal class Game
    {
        public const int MAP_MAX_Y = 23;
        public const int MAP_MIN_Y = 3;
        public const int MAP_MAX_X = 38;
        public const int MAP_MIN_X = 6;

        public void Initialize()
        {
            // 초기 세팅
            Console.ResetColor();                           // 컬러를 초기화한다.
            Console.CursorVisible = false;                  // 커서를 숨긴다.
            Console.Title = "경이루 아카데미";               // 타이틀을 설정한다.
            Console.BackgroundColor = ConsoleColor.Magenta; // 배경색을 설정한다.
            Console.ForegroundColor = ConsoleColor.Yellow; // 글꼴색을 설정한다.
            Console.Clear();                               // 출력된 모든 내용을 지운다.
        }
    }
}
