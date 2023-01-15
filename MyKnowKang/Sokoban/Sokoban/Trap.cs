using System;
using System.Collections.Generic;
using System.Text;

namespace Sokoban
{
	internal struct Trap
	{
		public int X;
		public int Y;
		public int Damage;
		public string Image;
		public ConsoleColor Color;
		public bool IsBurst;
		public int BurstRange;

		public int curBurstRange;
		public bool IsActive;
		public bool IsPlayerHit;
	}
}
