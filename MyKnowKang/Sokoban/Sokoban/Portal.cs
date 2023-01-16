using KMH_Sokoban;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sokoban
{
    internal struct Portal
    {
        public int[] GatesX;
        public int[] GatesY;
        public string Image;
        public ConsoleColor Color;

        /// <summary>
        /// Portal 현재 서 있는 게이트가 아닌 다른 게이트의 위치를 계산..
        /// </summary>
        public void ComputeOtherPortalGatePosition( int ignoreIndex, out int gateX, out int gateY )
        {
            Random random = new Random();

            int curGateX = -1;
            int curGateY = -1;
            int curIndex = ignoreIndex;

            int gateCount = GatesX.Length;
            while ( curIndex == ignoreIndex )
            {
                curIndex = random.Next( gateCount );

                curGateX = GatesX[curIndex];
                curGateY = GatesY[curIndex];
            }

            gateX = curGateX;
            gateY = curGateY;
        }
    }
}
