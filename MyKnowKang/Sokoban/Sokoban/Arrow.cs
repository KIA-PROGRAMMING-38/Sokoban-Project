using System;
using System.Collections.Generic;
using System.Text;

namespace Sokoban
{
    internal class Arrow
    {
        public int X;
        public int Y;
        public int PrevX;
        public int PrevY;
        public string Image;
        public ConsoleColor Color;
        public bool IsActive;

        public int DirX;
        public int DirY;

        public int Damage;

        public void ComputeImage()
        {
            if ( DirX == 1 )
                Image = Image[0].ToString();
            else if ( DirX == -1 )
                Image = Image[1].ToString();
            else if ( DirY == -1 )
                Image = Image[2].ToString();
            else
                Image = Image[3].ToString();
        }

        public void Update()
        {
            PrevX = X;
            PrevY = Y;

            X += DirX;
            Y += DirY;
        }
    }
}
