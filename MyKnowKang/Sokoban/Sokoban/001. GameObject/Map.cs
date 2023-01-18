using KMH_Sokoban;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sokoban
{
    internal class Map
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

        public string BorderLineImage;
        public ConsoleColor BorderLineColor;

        public Map( int minX, int minY, int maxX, int maxY, string newBorderLineImage, ConsoleColor newBorderLineColor )
        {
            Width = maxX - minX + 1;
            Height = maxY - minY + 1;

            BorderLineImage = newBorderLineImage;
            BorderLineColor = newBorderLineColor;

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

        public void Update( Action updateAction )
        {
            if ( null != updateAction )
            {
                updateAction.Invoke();
            }
        }

        public void Render()
        {
            for ( int i = Game.MAP_RANGE_MIN_X - 1; i < Game.MAP_RANGE_MAX_X; ++i )
            {
                Renderer.Render( i, Game.MAP_RANGE_MIN_Y - 1, BorderLineImage, BorderLineColor );
                Renderer.Render( i, Game.MAP_RANGE_MAX_Y - 1, BorderLineImage, BorderLineColor );
            }
            for ( int i = Game.MAP_RANGE_MIN_Y - 1; i < Game.MAP_RANGE_MAX_Y; ++i )
            {
                Renderer.Render( Game.MAP_RANGE_MIN_X - 1, i, BorderLineImage, BorderLineColor );
                Renderer.Render( Game.MAP_RANGE_MAX_X - 1, i, BorderLineImage, BorderLineColor );
            }
        }
    }
}
