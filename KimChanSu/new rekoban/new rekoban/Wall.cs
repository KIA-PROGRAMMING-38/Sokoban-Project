using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace new_rekoban
{
    /// <summary>
    /// 벽 관리
    /// </summary>
    internal class Wall
    {
        public static int[] _x = new int[13] { 9, 4, 10, 2, 1, 9, 14, 7, 3, 14, 8, 6, 10 };
        public static int[] _y = new int[13] { 1, 2, 3, 4, 5, 5, 6, 7, 8, 9, 10, 11, 11 };
        public static string _symbol = "*";
        public static int count = _x.Length;
    }
}
