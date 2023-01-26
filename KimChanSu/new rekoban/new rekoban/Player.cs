using Rekonban;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace new_rekoban
{
    /// <summary>
    /// 플레이어의 이동과 충돌 관리
    /// </summary>
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

        public int _x = 14; 
        public int _y = 12;
        public string _symbol = "P";
        public Direction _moveDirection = Direction.None;

        // 플레이어가 골이 아닌 오브젝트들 위에 있는지 판별
        bool isPlayerOnObj = false;
        public void Move(ConsoleKey key)
        {
            if (key == ConsoleKey.LeftArrow)
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

                    for (int i = 0; i < Fence.count; ++i)
                    {
                        if (_x == Fence._x[i] && _y == Fence._y[i])
                        {
                            isPlayerOnObj = false;
                            break;
                        }
                    }

                    if (false == isPlayerOnObj)
                    {
                        _x += 1;
                        break;
                    }

                    _x -= 1;
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

                    for (int i = 0; i < Fence.count; ++i)
                    {
                        if (_x == Fence._x[i] && _y == Fence._y[i])
                        {
                            isPlayerOnObj = false;
                            break;
                        }
                    }

                    if (false == isPlayerOnObj)
                    {
                        _x -= 1;
                        break;
                    }

                    _x += 1;
                }
            }

            if (key == ConsoleKey.UpArrow)
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

                    for (int i = 0; i < Fence.count; ++i)
                    {
                        if (_x == Fence._x[i] && _y == Fence._y[i])
                        {
                            isPlayerOnObj = false;
                            break;
                        }
                    }

                    if (false == isPlayerOnObj)
                    {
                        _y += 1;
                        break;
                    }

                    _y -= 1;
                }
            }

            if (key == ConsoleKey.DownArrow)
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

                    for (int i = 0; i < Fence.count; ++i)
                    {
                        if (_x == Fence._x[i] && _y == Fence._y[i])
                        {
                            isPlayerOnObj = false;
                            break;
                        }
                    }

                    if (false == isPlayerOnObj)
                    {
                        _y -= 1;
                        break;
                    }

                    _y += 1;
                }
            }
        }
    }
}