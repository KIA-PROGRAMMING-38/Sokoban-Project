using System;
using System.Collections.Generic;
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
		public int Duration;
		public string Image;
		public ConsoleColor Color;
		public bool isActive;
	}
}
