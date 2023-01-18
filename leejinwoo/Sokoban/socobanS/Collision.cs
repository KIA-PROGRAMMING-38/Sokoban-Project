using System;
using System.Collections.Generic;
using System.Text;

namespace socobanS
{
    class Collision
    {
        public static bool IsCollided(int x1, int y1, int x2, int y2)
        {
            if (x1 == x2 && y1 == y2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void OnCollision(Action action)
        {
            action();
        }
        public static void PushOut(Direction playerMoveDirection, ref int objX, ref int objY,
            in int collidedObjX, in int collidedObjY)
        {
            switch (playerMoveDirection)
            {
                case Direction.Left:
                    Move.MoveToRightOfTarget(out objX, in collidedObjX);

                    break;
                case Direction.Right:
                    Move.MoveToLeftOfTarget(out objX, in collidedObjX);

                    break;
                case Direction.Up:
                    Move.MoveToDownOfTarget(out objY, in collidedObjY);

                    break;
                case Direction.Down:
                    Move.MoveToUpOfTarget(out objY, in collidedObjY);

                    break;
            }
        }
    }
}
