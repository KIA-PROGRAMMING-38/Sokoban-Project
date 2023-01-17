using System;
using System.Collections.Generic;
using System.Text;

namespace Sokoban
{
    internal class Renderer
    {
        /// <summary>
        /// x, y 해당 좌표에 color 색에 맞게 image 를 출력..
        /// </summary>
        public static void Render( int x, int y, string image, ConsoleColor color )
        {
            if ( Game.MAP_RANGE_MIN_X - 1 > x || x > Game.MAP_RANGE_MAX_X ||
                Game.MAP_RANGE_MIN_Y - 1 > y || y > Game.MAP_RANGE_MAX_Y )
                return;

            ConsoleColor prevColor = Console.ForegroundColor;

            Console.ForegroundColor = color;
            Console.SetCursorPosition( x, y );
            Console.Write( image );

            Console.ForegroundColor = prevColor;
        }
    }
}
