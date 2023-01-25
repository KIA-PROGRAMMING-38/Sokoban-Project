using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace new_rekoban
{
    /// <summary>
    /// 울타리 관리
    /// </summary>
    internal class Fence
    {
        // 울타리의 좌표
        // int[] fenceX = new int[(Game.MAX_X + 1) * 2 + (Game.MAX_Y + 1) * 2];
        // int[] fenceY = new int[(Game.MAX_X + 1) * 2 + (Game.MAX_Y + 1) * 2];

        public static int[] _x = new int[(Game.MAX_X + 1) * 2 + (Game.MAX_Y + 1) * 2];
        public static int[] _y = new int[(Game.MAX_X + 1) * 2 + (Game.MAX_Y + 1) * 2];
        public static string _symbol = "@";
        public static int count = _x.Length;
    }
}
