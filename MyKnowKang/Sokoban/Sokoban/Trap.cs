using System;
using System.Collections.Generic;
using System.Text;

namespace Sokoban
{
	internal class Trap
	{
		public enum TrapType
		{
			None,
			Bomb,
			Trigger
		}

		public int X;
		public int Y;
		public string Image;
		public ConsoleColor Color;
        public bool IsActive;
        public bool IsBurst;
		public bool IsDestroy;
		public TrapType MyType;
    }

	internal class BombTrap : Trap
	{
        public int Damage;
        public int BurstRange;

        public int curBurstRange;
        public bool IsPlayerHit;
    }

	internal class TriggerTrap : Trap
	{
        public int SpawnObjectCount;

        public int[] SpawnObjectsX;
		public int[] SpawnObjectsY;

        public int[] SpawnObjectsDirX;
        public int[] SpawnObjectsDirY;

		public void CreateSpawnObjectArray(int newSpawnObjectCount)
		{
			SpawnObjectCount = newSpawnObjectCount;
			SpawnObjectsX = new int[SpawnObjectCount];
            SpawnObjectsY = new int[SpawnObjectCount];
            SpawnObjectsDirX = new int[SpawnObjectCount];
            SpawnObjectsDirY = new int[SpawnObjectCount];
        }
    }
}
