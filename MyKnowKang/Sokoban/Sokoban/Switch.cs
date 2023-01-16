using System;
using System.Collections.Generic;
using System.Text;

namespace Sokoban
{
    internal struct Switch
    {
        public int X;
        public int Y;
        public int ButtonOffsetX;          // 버튼 위치(스위치로부터 상대 위치).. 
        public int ButtonOffsetY;          // 버튼 위치(스위치로부터 상대 위치).. 
        
        public bool IsHolding;           // 스위치 누르는 중이냐..
        public int[] OpenCloseWallIndex;	// 스위치 누르거나 땔 때 열거나 닫는 벽 인덱스..

        public void SetSwitchState( Wall[] walls, bool newIsHolding )
        {
            if ( newIsHolding != IsHolding )
            {
                IsHolding = newIsHolding;

                int loopCount = OpenCloseWallIndex.Length;
                for ( int loopIndex = 0; loopIndex < loopCount; ++loopIndex )
                {
                    int wallIndex = OpenCloseWallIndex[loopIndex];

                    walls[wallIndex].IsActive = !IsHolding;
                    walls[wallIndex].IsRender = !IsHolding;
                }
            }
        }
    }
}
