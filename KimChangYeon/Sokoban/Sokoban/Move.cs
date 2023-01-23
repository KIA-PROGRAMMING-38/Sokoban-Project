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

                GameScene.player.X = GameScene.player.X + 1;
                GameSet.playerDir = GameSet.PLAYER_DIRECTION.RIGHT;

                GameObject.move++;
            }

            else if (Input.key == ConsoleKey.RightArrow && GameObject.hChangeDir == true)
            {
                GameScene.player.X = GameScene.player.X - 1;
                GameSet.playerDir = GameSet.PLAYER_DIRECTION.LEFT;

                GameObject.hFunction--;
                GameObject.move++;
            }

            if (Input.key == ConsoleKey.D && GameObject.moveLimit != 0)
            {
                GameScene.changer.X = Math.Min(GameScene.changer.X + 1, GameSet.MAP_MAX_X + 5);

                GameObject.moveLimit--;
            }
            else if (Input.key == ConsoleKey.D && GameObject.moveLimit == 0)
            {
                GameScene.changer.X = Math.Min(GameScene.changer.X, GameScene.changer.X);
            }
        }

        static public void Left()
        {
            if (Input.key == ConsoleKey.LeftArrow && GameObject.hChangeDir == false)
            {
                GameScene.player.X = GameScene.player.X - 1;
                GameSet.playerDir = GameSet.PLAYER_DIRECTION.LEFT;

                GameObject.move++;
            }

            else if (Input.key == ConsoleKey.LeftArrow && GameObject.hChangeDir == true)
            {
                GameScene.player.X = GameScene.player.X + 1;
                GameSet.playerDir = GameSet.PLAYER_DIRECTION.RIGHT;

                GameObject.hFunction--;
                GameObject.move++;
            }

            if (Input.key == ConsoleKey.A && GameObject.moveLimit != 0)
            {
                GameScene.changer.X = Math.Max(GameScene.changer.X - 1, GameSet.MAP_MAX_X + 4);

                GameObject.moveLimit--;
            }
            else if (Input.key == ConsoleKey.A && GameObject.moveLimit == 0)
            {
                GameScene.changer.X = Math.Min(GameScene.changer.X, GameScene.changer.X);
            }
        }

        static public void Up()
        {
            if (Input.key == ConsoleKey.UpArrow && GameObject.vChangeDir == false)
            {
                GameScene.player.Y = GameScene.player.Y - 1;
                GameSet.playerDir = GameSet.PLAYER_DIRECTION.UP;

                GameObject.move++;
            }

            else if (Input.key == ConsoleKey.UpArrow && GameObject.vChangeDir == true)
            {
                GameScene.player.Y = GameScene.player.Y + 1;

                GameObject.vFunction--;
                GameObject.move++;
            }

            if (Input.key == ConsoleKey.W && GameObject.moveLimit != 0)
            {
                GameScene.changer.Y = Math.Max(GameScene.changer.Y - 1, GameSet.MAP_MAX_Y - 1);

                GameObject.moveLimit--;
            }
            else if (Input.key == ConsoleKey.W && GameObject.moveLimit == 0)
            {
                GameScene.changer.Y = Math.Min(GameScene.changer.Y, GameScene.changer.Y);
            }
        }

        static public void Down()
        {
            if (Input.key == ConsoleKey.DownArrow && GameObject.vChangeDir == false)
            {
                GameScene.player.Y = GameScene.player.Y + 1;
                GameSet.playerDir = GameSet.PLAYER_DIRECTION.DOWN;

                GameObject.move++;
            }

            else if (Input.key == ConsoleKey.DownArrow && GameObject.vChangeDir == true)
            {
                GameScene.player.Y = GameScene.player.Y - 1;
                GameSet.playerDir = GameSet.PLAYER_DIRECTION.UP;

                GameObject.vFunction--;
                GameObject.move++;
            }

            if (Input.key == ConsoleKey.S && GameObject.moveLimit != 0)
            {
                GameScene.changer.Y = Math.Min(GameScene.changer.Y + 1, GameSet.MAP_MAX_Y);

                GameObject.moveLimit--;
            }
            else if (Input.key == ConsoleKey.S && GameObject.moveLimit == 0)
            {
                GameScene.changer.Y = Math.Min(GameScene.changer.Y, GameScene.changer.Y);
            }
        }
    }
}
