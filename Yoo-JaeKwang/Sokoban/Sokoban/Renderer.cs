using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    internal class Renderer
    {
        /// <summary>
        /// 콘솔환경에 그림을 그려줍니다.
        /// </summary>
        /// <param name="x">x좌표</param>
        /// <param name="y">y좌표</param>
        /// <param name="icon">그려질 기호</param>
        public void Render(int x, int y, string icon)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(icon);
        }
    }
}
