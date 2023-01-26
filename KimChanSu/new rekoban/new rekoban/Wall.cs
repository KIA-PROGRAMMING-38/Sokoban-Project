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
        public static int[] _x = new int[13];
        public static int[] _y = new int[13];
        public static string _symbol = "*";
        public static int count = _x.Length;
    }
}