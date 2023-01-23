using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    internal class GameSet
    {
        public enum PLAYER_DIRECTION
        {
            RIGHT,
            LEFT,
            DOWN,
            UP
        }

        static public PLAYER_DIRECTION playerDir = new PLAYER_DIRECTION();

        public const int MAP_MIN_X = 0;
        public const int MAP_MIN_Y = 0;
        public const int MAP_MAX_X = 20;
        public const int MAP_MAX_Y = 12;


        public static void SetGame()
        {
            Console.ResetColor();                            // 컬러를 초기화 한다
            Console.CursorVisible = false;                   // 커서를 숨긴다
            Console.Title = "홍성재의 썬더펀치";               // 타이틀을 설정한다.
            
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            
            
            
        }

        
    }
}
