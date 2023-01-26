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

        public static int hidePointX = 0;
        public static int hidePointY = 0;


        public static void SetGame()
        {
            Console.ResetColor();                            // 컬러를 초기화 한다
            Console.CursorVisible = false;                   // 커서를 숨긴다
            Console.Title = "SOKOBAN?";               // 타이틀을 설정한다.
            
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            
            
            
        }

        
    }
}
