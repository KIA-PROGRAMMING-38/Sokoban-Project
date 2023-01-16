using KMH_Sokoban;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Sokoban
{
	internal struct Item
	{
		public enum Type
		{
			ReverseMove,
			EasterEgg,
			HPPosion,
			MPPosion,
			END		// 몇 개인지 셀 용도로 씀( 맨 마지막에 있어야 함 )..
		}

		public const int ITEM_TYPE_COUNT = (int)(Type.END);

		public int X;
		public int Y;
		public Type type;
		public int Effect;
		public int Duration;
		public string Image;
		public ConsoleColor Color;
		public bool isActive;

		public int Update( ref Player player )
		{
            if ( Item.Type.END == type )
            {
                return -1;
            }

            switch ( type )
            {
                case Item.Type.ReverseMove:
                    // 현재 프레임에 플레이어가 움직인 거리 계산..
                    int moveDirX = player.X - player.PrevX;
                    int moveDirY = player.Y - player.PrevY;

                    if ( 0 != moveDirX || moveDirY != 0 )   // 움직임이 있는 경우에만 실행..
                    {
                        // 그 반대 방향으로 움직이게 함..
                        player.X = player.PrevX - moveDirX;
                        player.Y = player.PrevY - moveDirY;
                    }
                    else
                    {
                        ++Duration;
                    }
                    break;

                case Item.Type.EasterEgg:
                    Program.교수님죄송합니다();
                    break;

                case Item.Type.HPPosion:
                    player.CurHp = Math.Min( player.MaxHp, player.CurHp + Effect );
                    break;

                case Item.Type.MPPosion:
                    player.CurMp = Math.Min( player.MaxMp, player.CurMp + Effect );
                    break;
            }

            --Duration;

            if ( 0 == Duration )
            {
                type = Item.Type.END;
            }

            return 0;
        }
	}
}
