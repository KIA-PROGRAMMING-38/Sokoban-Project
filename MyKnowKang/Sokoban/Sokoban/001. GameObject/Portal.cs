using KMH_Sokoban;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sokoban
{
    internal class Portal
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

        public void Teleport( int ignoreIndex, ref int posX, ref int posY, in int prevX, in int prevY )
        {
            // 다른 포탈 게이트 이동 시 현재 이동한 방향으로 1칸 이동한 위치에 순간이동시키기..
            int dirX = posX - prevX;
            int dirY = posY - prevY;

            ComputeOtherPortalGatePosition( ignoreIndex, out posX, out posY );

            posX = posX + dirX;
            posY = posY + dirY;
        }

        public void Render()
        {
            int gateCount = GatesX.Length;

            for( int gateIndex = 0; gateIndex < gateCount; ++gateIndex )
            {
                Renderer.Render( GatesX[gateIndex], GatesY[gateIndex], Image, Color );
            }
        }
    }
}
