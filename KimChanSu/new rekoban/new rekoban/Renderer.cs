using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace new_rekoban
{
    /// <summary>
    /// 콘솔 환경에 그려주는 걸 도와주는 클래스
    /// </summary>
    internal class Renderer
    {
        /// <summary>
        /// 콘솔 환경에 그립니다.
        /// </summary>
        /// <param name="x">좌표</param>
        /// <param name="y">좌표</param>
        /// <param name="symbol">그려질 기호</param>
        public void Render(int x, int y, string symbol)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
        }
    }
}