using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
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

        public void Action()
        {
            if ( IsDestroy )
            {
                return;
            }

            IsBurst = true;
            IsActive = true;
            IsDestroy = true;
        }
    }

	internal class BombTrap : Trap
	{
        public int Damage;
        public int BurstRange;

        public int curBurstRange;
        public bool IsPlayerHit;

        public void Update( ref Player player )
        {
            if ( curBurstRange >= BurstRange )
            {
                IsActive = false;
                IsBurst = false;

                return;
            }

            // 플레이어에게 아직 데미지를 주지 못했다면 검사..
            if ( false == IsPlayerHit )
            {
                if ( IsInRange( player.X, player.Y, X, Y, curBurstRange ) )
                {
                    player.CurHp -= Damage;
                    IsPlayerHit = true;
                }
            }

            ++curBurstRange;
        }

        public void Render()
        {
            int loopStart = -curBurstRange;
            int loopEnd = curBurstRange;

            for ( int offsetX = loopStart; offsetX < loopEnd; ++offsetX )
            {
                int trapX = X + offsetX;
                int trapY = Y - curBurstRange;

                Renderer.Render( trapX, trapY, Image, Color );

                trapY = Y + curBurstRange;

                Renderer.Render( trapX, trapY, Image, Color );
            }

            for ( int offsetY = loopStart; offsetY < loopEnd; ++offsetY )
            {
                int trapX = X - curBurstRange;
                int trapY = Y - offsetY;

                Renderer.Render( trapX, trapY, Image, Color );

                trapX = X + curBurstRange;

                Renderer.Render( trapX, trapY, Image, Color );
            }
        }

        /// <summary>
        /// 범위 안에 있는가..
        /// </summary>
        private bool IsInRange( int x, int y, int x2, int y2, int range )
        {
            int xDistance = Math.Abs(x - x2);
            int yDistance = Math.Abs(y - y2);

            if ( xDistance + yDistance <= range )
            {
                return true;
            }

            return false;
        }
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

        public void SpawnObject( ref Player player, ref List<Arrow> arrows, string objectImage, ConsoleColor objectColor )
        {
            IsActive = false;
            IsBurst = false;

            for ( int spawnIndex = 0; spawnIndex < SpawnObjectCount; ++spawnIndex )
            {
                int x = SpawnObjectsX[spawnIndex];
                int y = SpawnObjectsY[spawnIndex];
                int dirX = SpawnObjectsDirX[spawnIndex];
                int dirY = SpawnObjectsDirY[spawnIndex];

                Arrow arrow = new Arrow
                {
                    X = x, Y = y, PrevX = x, PrevY = y, DirX = dirX, DirY = dirY,
                    Image = objectImage, Color = objectColor, IsActive = true, Damage = 5
                };

                arrow.ComputeImage();

                arrows.Add( arrow );
            }
        }
    }
}
