using System;
using System.Collections.Generic;
using System.Text;

namespace Sokoban
{
    internal class Goal
    {
        public int X;
        public int Y;
        public string Image;
        public ConsoleColor Color;
        public ConsoleColor GoalInColor;
        public bool IsGoalIn;

        public void Render()
        {
            ConsoleColor renderColor = Color;

            if ( IsGoalIn )
            {
                renderColor = GoalInColor;
            }
            else
            {
                renderColor = Color;
            }

            Renderer.Render( X, Y, Image, renderColor );
        }

        public bool CheckOnBox(in Box[] boxes)
        {
            IsGoalIn = false;

            int boxCount = boxes.Length;

            for ( int j = 0; j < boxCount; ++j )
            {
                if ( X == boxes[j].X && Y == boxes[j].Y )
                {
                    IsGoalIn = true;

                    return true;
                }
            }

            return false;
        }
    }
}
