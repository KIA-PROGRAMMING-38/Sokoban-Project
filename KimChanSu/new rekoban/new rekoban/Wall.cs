using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace new_rekoban
{
    internal class Wall
    {
        public static int[] _x = new int[14] { 9, 4, 10, 2, 1, 9, 14, 7, 3, 14, 8, 6, 10, 13 };
        public static int[] _y = new int[14] { 1, 2, 3, 4, 5, 5, 6, 7, 8, 9, 10, 11, 11, 12 };
        public static string _symbol = "*";
        public static int count = _x.Length;
    }
}
