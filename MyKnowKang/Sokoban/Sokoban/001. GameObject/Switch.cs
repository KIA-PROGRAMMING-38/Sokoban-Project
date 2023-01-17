using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Sokoban
{
    internal class Switch
    {
        public int X;
        public int Y;
        public string SwitchImage;
        public ConsoleColor SwitchColor;
        public string PusherImage;
        public ConsoleColor PusherColor;

        public int ButtonOffsetX;          // 버튼 위치(스위치로부터 상대 위치).. 
        public int ButtonOffsetY;          // 버튼 위치(스위치로부터 상대 위치).. 
        
        public bool IsHolding;           // 스위치 누르는 중이냐..
        public int[] OpenCloseWallIndex;	// 스위치 누르거나 땔 때 열거나 닫는 벽 인덱스..

        public void SetSwitchState( Map map, Wall[] walls, bool newIsHolding )
        {
            if ( newIsHolding != IsHolding )
            {
                IsHolding = newIsHolding;

                Map.SpaceType changeSpaceType = (IsHolding) ? Map.SpaceType.Pass : Map.SpaceType.DontPass;

                int loopCount = OpenCloseWallIndex.Length;
                for ( int loopIndex = 0; loopIndex < loopCount; ++loopIndex )
                {
                    int wallIndex = OpenCloseWallIndex[loopIndex];

                    walls[wallIndex].IsActive = !IsHolding;
                    walls[wallIndex].IsRender = !IsHolding;

                    map.ChangeSpaceType( walls[wallIndex].X, walls[wallIndex].Y, changeSpaceType );
                }
            }
        }

        public void Render()
        {
            Renderer.Render( X, Y, SwitchImage, SwitchColor );

            if( !IsHolding )
            {
                Renderer.Render( X + ButtonOffsetX, Y + ButtonOffsetY, PusherImage, PusherColor );
            }
        }
    }
}
