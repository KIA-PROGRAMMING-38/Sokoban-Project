using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    internal class Move
    {
        
        static public void Right()
        {
            if (Input.key == ConsoleKey.RightArrow && GameObject.hChangeDir == false)
            {

                GameObject.player.X = Math.Min(GameObject.player.X + 1, GameSet.MAP_MAX_X);
                GameSet.playerDir = GameSet.PLAYER_DIRECTION.RIGHT;

                GameObject.move++;
            }

            else if (Input.key == ConsoleKey.RightArrow && GameObject.hChangeDir == true)
            {
                GameObject.player.X = Math.Max(GameObject.player.X - 1, GameSet.MAP_MIN_X + 1);
                GameSet.playerDir = GameSet.PLAYER_DIRECTION.LEFT;

                GameObject.hFunction--;
                GameObject.move++;
            }

            if (Input.key == ConsoleKey.D && GameObject.moveLimit != 0)
            {
                GameObject.changer.X = Math.Min(GameObject.changer.X + 1, GameSet.MAP_MAX_X + 5);

                GameObject.moveLimit--;
            }
            else if (Input.key == ConsoleKey.D && GameObject.moveLimit == 0)
            {
                GameObject.changer.X = Math.Min(GameObject.changer.X, GameObject.changer.X);
            }
        }

        static public void Left()
        {
            if (Input.key == ConsoleKey.LeftArrow && GameObject.hChangeDir == false)
            {
                GameObject.player.X = Math.Max(GameObject.player.X - 1, GameSet.MAP_MIN_X + 1);
                GameSet.playerDir = GameSet.PLAYER_DIRECTION.LEFT;

                GameObject.move++;
            }

            else if (Input.key == ConsoleKey.LeftArrow && GameObject.hChangeDir == true)
            {
                GameObject.player.X = Math.Min(GameObject.player.X + 1, GameSet.MAP_MAX_X);
                GameSet.playerDir = GameSet.PLAYER_DIRECTION.RIGHT;

                GameObject.hFunction--;
                GameObject.move++;
            }

            if (Input.key == ConsoleKey.A && GameObject.moveLimit != 0)
            {
                GameObject.changer.X = Math.Max(GameObject.changer.X - 1, GameSet.MAP_MAX_X + 4);

                GameObject.moveLimit--;
            }
            else if (Input.key == ConsoleKey.A && GameObject.moveLimit == 0)
            {
                GameObject.changer.X = Math.Min(GameObject.changer.X, GameObject.changer.X);
            }
        }

        static public void Up()
        {
            if (Input.key == ConsoleKey.UpArrow && GameObject.vChangeDir == false)
            {
                GameObject.player.Y = Math.Max(GameObject.player.Y - 1, GameSet.MAP_MIN_Y + 1);
                GameSet.playerDir = GameSet.PLAYER_DIRECTION.UP;

                GameObject.move++;
            }

            else if (Input.key == ConsoleKey.UpArrow && GameObject.vChangeDir == true)
            {
                GameObject.player.Y = Math.Min(GameObject.player.Y + 1, GameSet.MAP_MAX_Y);
                GameSet.playerDir = GameSet.PLAYER_DIRECTION.DOWN;

                GameObject.vFunction--;
                GameObject.move++;
            }

            if (Input.key == ConsoleKey.W && GameObject.moveLimit != 0)
            {
                GameObject.changer.Y = Math.Max(GameObject.changer.Y - 1, GameSet.MAP_MAX_Y - 1);

                GameObject.moveLimit--;
            }
            else if (Input.key == ConsoleKey.W && GameObject.moveLimit == 0)
            {
                GameObject.changer.Y = Math.Min(GameObject.changer.Y, GameObject.changer.Y);
            }
        }

        static public void Down()
        {
            if (Input.key == ConsoleKey.DownArrow && GameObject.vChangeDir == false)
            {
                GameObject.player.Y = Math.Min(GameObject.player.Y + 1, GameSet.MAP_MAX_Y);
                GameSet.playerDir = GameSet.PLAYER_DIRECTION.DOWN;

                GameObject.move++;
            }

            else if (Input.key == ConsoleKey.DownArrow && GameObject.vChangeDir == true)
            {
                GameObject.player.Y = Math.Max(GameObject.player.Y - 1, GameSet.MAP_MIN_Y + 1);
                GameSet.playerDir = GameSet.PLAYER_DIRECTION.UP;

                GameObject.vFunction--;
                GameObject.move++;
            }

            if (Input.key == ConsoleKey.S && GameObject.moveLimit != 0)
            {
                GameObject.changer.Y = Math.Min(GameObject.changer.Y + 1, GameSet.MAP_MAX_Y);

                GameObject.moveLimit--;
            }
            else if (Input.key == ConsoleKey.S && GameObject.moveLimit == 0)
            {
                GameObject.changer.Y = Math.Min(GameObject.changer.Y, GameObject.changer.Y);
            }
        }
    }
}
