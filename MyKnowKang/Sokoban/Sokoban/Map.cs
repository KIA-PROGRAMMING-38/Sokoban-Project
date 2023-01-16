using KMH_Sokoban;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sokoban
{
    internal struct Map
    {
        public enum SpaceType
        {
            Pass            // 지나가는 곳..
            , DontPass      // 못지나가는 곳..
            , PlayerStand   // 플레이어가 있는 곳..
            , BoxStand      // 박스가 있는 곳..
            , Portal        // 포탈이 있는 곳..
            , Item          // 아이템이 있는 곳..
            , Trap          // 트랩이 있는 곳..
            , Bomb          // 폭탄이 있는 곳..
            , Arrow         // 화살이 있는 곳..
        }

        public SpaceType[, ] SpaceData;
        public int Width;
        public int Height;

        public Map( int minX, int minY, int maxX, int maxY )
        {
            Width = maxX - minX + 1;
            Height = maxY - minY + 1;

            SpaceData = new SpaceType[Height, Width];
        }

        public void ChangeSpaceType( int x, int y, SpaceType newSpaceType )
        {
            SpaceData[y, x] = newSpaceType;
        }

        public SpaceType GetCurStandSpaceType( int x, int y )
        {
            return SpaceData[y, x];
        }
    }
}
