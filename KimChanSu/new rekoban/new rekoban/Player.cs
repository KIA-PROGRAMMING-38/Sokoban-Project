using Rekonban;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace new_rekoban
{
    internal class Player
    {
        internal enum Direction
        {
            None,
            Left,
            Right,
            Up,
            Down
        }

        private int _x = 14;
        private int _y = 12;
        private string _symbol = "A";
        private Direction _moveDirection = Direction.None;

        public int GetX() => _x;
        public int GetY() => _y;

        public string GetSymbol() => _symbol;

        public void Move(ConsoleKey key)
        {
            if (key == ConsoleKey.LeftArrow)
            {
                int _y;
                isPlayerOnObj = true;

                while (isPlayerOnObj)
                {
                    for (int i = 0; i < Wall.count; ++i)
                    {
                        if (this._x == Wall._x[i] && this._y == Wall._y[i])
                        {
                            isPlayerOnObj = false;
                            break;
                        }
                    }

                    for (int i = 0; i < fenceCount; ++i)
                    {
                        if (this._x == fenceX[i] && this._y == fenceY[i])
                        {
                            isPlayerOnObj = false;
                            break;
                        }
                    }

                    if (false == isPlayerOnObj)
                    {
                        this._x += 1;
                        break;
                    }

                    this._x -= 1;
                }

            }

            if (key == ConsoleKey.RightArrow)
            {
                isPlayerOnObj = true;

                while (isPlayerOnObj)
                {
                    for (int i = 0; i < Wall.count; ++i)
                    {
                        if (_x == Wall._x[i] && _y == Wall._y[i])
                        {
                            isPlayerOnObj = false;
                            break;
                        }
                    }

                    for (int i = 0; i < fenceCount; ++i)
                    {
                        if (_x == fenceX[i] && _y == fenceY[i])
                        {
                            isPlayerOnObj = false;
                            break;
                        }
                    }

                    if (false == isPlayerOnObj)
                    {
                        playerX -= 1;
                        break;
                    }

                    playerX += 1;
                }
            }

            if (key == ConsoleKey.UpArrow)
            {
                isPlayerOnObj = true;

                while (isPlayerOnObj)
                {
                    for (int i = 0; i < wallCount; ++i)
                    {
                        if (playerX == wallPositionsX[i] && playerY == wallPositionsY[i])
                        {
                            isPlayerOnObj = false;
                            break;
                        }
                    }

                    for (int i = 0; i < fenceCount; ++i)
                    {
                        if (playerX == fenceX[i] && playerY == fenceY[i])
                        {
                            isPlayerOnObj = false;
                            break;
                        }
                    }

                    if (false == isPlayerOnObj)
                    {
                        playerY += 1;
                        break;
                    }

                    playerY -= 1;
                }
            }

            if (key == ConsoleKey.DownArrow)
            {
                isPlayerOnObj = true;

                while (isPlayerOnObj)
                {
                    for (int i = 0; i < wallCount; ++i)
                    {
                        if (playerX == wallPositionsX[i] && playerY == wallPositionsY[i])
                        {
                            isPlayerOnObj = false;
                            break;
                        }
                    }

                    for (int i = 0; i < fenceCount; ++i)
                    {
                        if (playerX == fenceX[i] && playerY == fenceY[i])
                        {
                            isPlayerOnObj = false;
                            break;
                        }
                    }

                    if (false == isPlayerOnObj)
                    {
                        playerY -= 1;
                        break;
                    }

                    playerY += 1;
                }
            }
        }
        static void RepeatCheckPlayerOnObject(Player, Wall, Wall, bool someBool)
        {
            for (int i = 0; i < Wall.count; ++i)
            {
                if (_x == Wall._x[i] && Player.GetY == Wall._y[i])
                {
                    someBool = false;
                    break;
                }
            }
        }
    }
}
