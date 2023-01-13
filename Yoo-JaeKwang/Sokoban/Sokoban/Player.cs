using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    internal class Player
    {
        internal enum Direction
        {
            None,
            Up,
            Down,
            Left,
            Right
        }

        private int _x = 8;
        private int _y = 4;
        private string _symbol = "P";
        private Direction _moveDirection = Direction.None;
        private int _pushedBoxIndex = 0;

        //접근자
        public int GetX() => _x;
        public int GetY() => _y;
        public string GetSymbol() => _symbol;
        public Direction GetMoveDirection() => _moveDirection;

        //설정자
        public void SetX(int newX) => _x = newX;
        public void SetY(int newY) => _y = newY;


        // method는 어떠한 기능을 수행함 => 이 class를 다루는 interface가 됨
        public void Move(ConsoleKey key)
        {
            if (key == ConsoleKey.UpArrow)
            {
                _y = Math.Max(Game.MAP_MIN_Y, _y - 1);
                _moveDirection = Direction.Up;
            }
            if (key == ConsoleKey.DownArrow)
            {
                _y = Math.Min(_y + 1, Game.MAP_MAX_Y);
                _moveDirection = Direction.Down;
            }
            if (key == ConsoleKey.LeftArrow)
            {
                _x = Math.Max(Game.MAP_MIN_X, _x - 1);
                _moveDirection = Direction.Left;
            }
            if (key == ConsoleKey.RightArrow)
            {
                _x = Math.Min(_x + 1, Game.MAP_MAX_X);
                _moveDirection = Direction.Right;
            }
        }
    }
}
