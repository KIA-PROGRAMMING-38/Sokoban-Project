using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    internal class OnColision
    {
        
        public static void WithPlayerBox()
        {
            for (int boxId = 0; boxId < GameScene.boxes.Length; boxId++)
            {
                if (IsCollide(GameScene.player.X, GameScene.boxes[boxId].X, GameScene.player.Y, GameScene.boxes[boxId].Y) && GameRule.SameColor(GameScene.player.Color, GameScene.boxes[boxId].Color)) // 외곽 벽을 만났을 떄
                {
                    GameScene.player.PushedBoxId = boxId;

                    switch (GameSet.playerDir)
                    {
                        case GameSet.PLAYER_DIRECTION.RIGHT: //right
                            GameScene.boxes[boxId].X = GameScene.boxes[boxId].X + 1;
                            break;
                        case GameSet.PLAYER_DIRECTION.LEFT: //left
                            GameScene.boxes[boxId].X = GameScene.boxes[boxId].X - 1;
                            break;
                        case GameSet.PLAYER_DIRECTION.DOWN: //down
                            GameScene.boxes[boxId].Y = GameScene.boxes[boxId].Y + 1;
                            break;
                        case GameSet.PLAYER_DIRECTION.UP: //up
                            GameScene.boxes[boxId].Y = GameScene.boxes[boxId].Y - 1;
                            break;
                    }

                }
                if (IsCollide(GameScene.player.X, GameScene.boxes[boxId].X, GameScene.player.Y, GameScene.boxes[boxId].Y) && !GameRule.SameColor(GameScene.player.Color, GameScene.boxes[boxId].Color))
                {
                    switch (GameSet.playerDir)
                    {
                        case GameSet.PLAYER_DIRECTION.RIGHT: //right
                            GameScene.player.X = GameScene.boxes[boxId].X - 1;
                            GameScene.boxes[boxId].X = GameScene.boxes[boxId].X;
                            break;
                        case GameSet.PLAYER_DIRECTION.LEFT: //left
                            GameScene.player.X = GameScene.boxes[boxId].X + 1;
                            GameScene.boxes[boxId].X = GameScene.boxes[boxId].X;
                            break;
                        case GameSet.PLAYER_DIRECTION.DOWN: //down
                            GameScene.player.Y = GameScene.boxes[boxId].Y - 1;
                            GameScene.boxes[boxId].Y = GameScene.boxes[boxId].Y;
                            break;
                        case GameSet.PLAYER_DIRECTION.UP: //up
                            GameScene.player.Y = GameScene.boxes[boxId].Y + 1;
                            GameScene.boxes[boxId].Y = GameScene.boxes[boxId].Y;
                            break;
                    }
                }
            }
        }

        public static void WithPlayerWall()
        {
            for (int wallId = 0; wallId < GameScene.walls.Length; wallId++) //벽과 플레이어
            {

                if (IsCollide(GameScene.player.X, GameScene.walls[wallId].X, GameScene.player.Y, GameScene.walls[wallId].Y))
                {

                    switch (GameSet.playerDir)
                    {

                        case GameSet.PLAYER_DIRECTION.RIGHT: //right
                            GameScene.player.X = GameScene.walls[wallId].X - 1;
                            break;
                        case GameSet.PLAYER_DIRECTION.LEFT: //left
                            GameScene.player.X = GameScene.walls[wallId].X + 1;
                            break;
                        case GameSet.PLAYER_DIRECTION.DOWN: //down
                            GameScene.player.Y = GameScene.walls[wallId].Y - 1;
                            break;
                        case GameSet.PLAYER_DIRECTION.UP: //up
                            GameScene.player.Y = GameScene.walls[wallId].Y + 1;
                            break;

                    }
                }

            } //벽과 플레이어
        }

        public static void WithColorWall()
        {
            for (int wallId = 0; wallId < GameScene.colorWalls.Length; wallId++) //벽과 플레이어
            {

                if (IsCollide(GameScene.player.X, GameScene.colorWalls[wallId].X, GameScene.player.Y, GameScene.colorWalls[wallId].Y))
                {

                    switch (GameSet.playerDir)
                    {

                        case GameSet.PLAYER_DIRECTION.RIGHT: //right
                            GameScene.player.X = GameScene.colorWalls[wallId].X - 1;
                            break;
                        case GameSet.PLAYER_DIRECTION.LEFT: //left
                            GameScene.player.X = GameScene.colorWalls[wallId].X + 1;
                            break;
                        case GameSet.PLAYER_DIRECTION.DOWN: //down
                            GameScene.player.Y = GameScene.colorWalls[wallId].Y - 1;
                            break;
                        case GameSet.PLAYER_DIRECTION.UP: //up
                            GameScene.player.Y = GameScene.colorWalls[wallId].Y + 1;
                            break;

                    }
                }

            } //벽과 플레이어
        }
    
        public static void WithBoxWall()
        {

            for (int boxId = 0; boxId < GameScene.boxes.Length; boxId++) //벽과 박스
            {

                for (int wallId = 0; wallId < GameScene.walls.Length; wallId++)
                {

                    if (IsCollide(GameScene.boxes[boxId].X, GameScene.walls[wallId].X, GameScene.boxes[boxId].Y, GameScene.walls[wallId].Y))
                    {

                        switch (GameSet.playerDir)
                        {

                            case GameSet.PLAYER_DIRECTION.RIGHT: //right
                                GameScene.player.X = GameScene.walls[wallId].X - 2;
                                GameScene.boxes[boxId].X = GameScene.walls[wallId].X - 1;
                                break;
                            case GameSet.PLAYER_DIRECTION.LEFT: //left
                                GameScene.player.X = GameScene.walls[wallId].X + 2;
                                GameScene.boxes[boxId].X = GameScene.walls[wallId].X + 1;
                                break;
                            case GameSet.PLAYER_DIRECTION.DOWN: //down
                                GameScene.player.Y = GameScene.walls[wallId].Y - 2;
                                GameScene.boxes[boxId].Y = GameScene.walls[wallId].Y - 1;
                                break;
                            case GameSet.PLAYER_DIRECTION.UP: //up
                                GameScene.player.Y = GameScene.walls[wallId].Y + 2;
                                GameScene.boxes[boxId].Y = GameScene.walls[wallId].Y + 1;
                                break;

                        }
                    }
                }
            } //벽과 박스
        }

        public static void WithBoxBox()
        {
            for (int boxId = 0; boxId < GameScene.boxes.Length; boxId++) //박스와 박스 충돌
            {
                for (int boxId2 = 0; boxId2 < GameScene.boxes.Length; boxId2++)
                {
                    if (boxId == boxId2)
                    {
                        continue;
                    }
                    if (GameScene.boxes[boxId].X == GameScene.boxes[boxId2].X && GameScene.boxes[boxId].Y == GameScene.boxes[boxId2].Y && GameScene.player.PushedBoxId == boxId)
                    {
                        switch (GameSet.playerDir)
                        {
                            case GameSet.PLAYER_DIRECTION.RIGHT: //right
                                GameScene.player.X = GameScene.player.X - 1;
                                GameScene.boxes[boxId2].X = GameScene.player.X + 2;
                                GameScene.boxes[boxId].X = GameScene.player.X + 1;
                                break;
                            case GameSet.PLAYER_DIRECTION.LEFT: //left
                                GameScene.player.X = GameScene.player.X + 1;
                                GameScene.boxes[boxId2].X = GameScene.player.X - 2;
                                GameScene.boxes[boxId].X = GameScene.player.X - 1;
                                break;
                            case GameSet.PLAYER_DIRECTION.DOWN: //down
                                GameScene.player.Y = GameScene.player.Y - 1;
                                GameScene.boxes[boxId2].Y = GameScene.player.Y + 2;
                                GameScene.boxes[boxId].Y = GameScene.player.Y + 1;
                                break;
                            case GameSet.PLAYER_DIRECTION.UP: //up
                                GameScene.player.Y = GameScene.player.Y + 1;
                                GameScene.boxes[boxId2].Y = GameScene.player.Y - 2;
                                GameScene.boxes[boxId].Y = GameScene.player.Y - 1;
                                break;
                        }
                    }
                }
            } //박스와 박스 충돌
        }

        public static void WithPlayerHitem()
        {
            for (int itemId = 0; itemId < GameScene.horizonItems.Length; itemId++)
            {
                if (IsCollide(GameScene.player.X, GameScene.horizonItems[itemId].X, GameScene.player.Y, GameScene.horizonItems[itemId].Y) && GameObject.hChangeDir == false)
                {
                    GameScene.horizonItems[itemId].X = GameSet.hidePointX;
                    GameScene.horizonItems[itemId].Y = GameSet.hidePointY;
                    GameScene.horizonItems[itemId].Symbol = ' ';
                    GameObject.hChangeDir = true;

                    break;
                }
            }

            for (int itemId = 0; itemId < GameScene.horizonItems.Length; itemId++)
            {
                if (IsCollide(GameScene.player.X, GameScene.horizonItems[itemId].X, GameScene.player.Y, GameScene.horizonItems[itemId].Y) && GameObject.hChangeDir == true)
                {
                    GameScene.horizonItems[itemId].X = GameSet.hidePointX;
                    GameScene.horizonItems[itemId].Y = GameSet.hidePointY;
                    GameScene.horizonItems[itemId].Symbol = ' ';
                    GameObject.hChangeDir = false;

                    break;
                }
            }
        }

        public static void WithPlayerVitem()
        {
            for (int itemId = 0; itemId < GameScene.verticalItems.Length; itemId++)
            {
                if (IsCollide(GameScene.player.X, GameScene.verticalItems[itemId].X, GameScene.player.Y, GameScene.verticalItems[itemId].Y) && GameObject.vChangeDir == false)
                {
                    GameScene.verticalItems[itemId].X = GameSet.hidePointX;
                    GameScene.verticalItems[itemId].Y = GameSet.hidePointY;
                    GameScene.verticalItems[itemId].Symbol = ' ';
                    GameObject.vChangeDir = true;

                    break;
                }
            }
            for (int itemId = 0; itemId < GameScene.verticalItems.Length; itemId++)
            {
                if (IsCollide(GameScene.player.X, GameScene.verticalItems[itemId].X, GameScene.player.Y, GameScene.verticalItems[itemId].Y) && GameObject.vChangeDir == true)
                {
                    GameScene.verticalItems[itemId].X = GameSet.hidePointX;
                    GameScene.verticalItems[itemId].Y = GameSet.hidePointY;
                    GameScene.verticalItems[itemId].Symbol = ' ';
                    GameObject.vChangeDir = false;

                    break;
                }
            }


        }

        public static void WithPlayerTrap()
        {

            for (int trapId = 0; trapId < GameScene.traps.Length; trapId++)
            {
                if (IsCollide(GameScene.player.X, GameScene.traps[trapId].X, GameScene.player.Y, GameScene.traps[trapId].Y))
                {
                    GameObject.playerHpNumber--;
                }
            }
        }

        static bool IsCollide(int x1, int x2, int y1, int y2)
        {
            if (x1 == x2 && y1 == y2)
                return true;
            else
                return false;
        }
    }
}
