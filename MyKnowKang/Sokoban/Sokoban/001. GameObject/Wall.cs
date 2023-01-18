using System;
using System.Collections.Generic;
using System.Text;

namespace Sokoban
{
    internal class Wall
    {
        public int X;
        public int Y;
        public string Image;
        public ConsoleColor Color;
        public bool IsActive;  // 현재 벽이 활성화되어있는가( default = true )..
        public bool IsRender;

        public void Render()
        {
            string RenderImage = Image;
            ConsoleColor RenderColor = Color;

            if ( true == IsActive )
            {
                RenderImage = Image;
                RenderColor = Color;
            }
            else
            {
                RenderImage = " ";
                RenderColor = Game.FOREGROUND_COLOR;
            }

            Renderer.Render( X, Y, RenderImage, RenderColor );
        }
    }
}
