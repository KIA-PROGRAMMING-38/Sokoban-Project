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
            for (int boxId = 0; boxId < GameObject.boxLength; boxId++)
            {
                if (IsCollide(GameObject.player.X, GameObject.boxes[boxId].X, GameObject.player.Y, GameObject.boxes[boxId].Y) && GameRule.SameColor(GameObject.player.Color, GameObject.boxes[boxId].Color)) // 외곽 벽을 만났을 떄
                {
                    GameObject.player.PushedBoxId = boxId;

                    switch (GameSet.playerDir)
                    {
                        case GameSet.PLAYER_DIRECTION.RIGHT: //right
                            GameObject.player.X = Math.Min(GameObject.player.X, GameSet.MAP_MAX_X - 1);
                            GameObject.boxes[boxId].X = Math.Min(GameObject.boxes[boxId].X + 1, GameSet.MAP_MAX_X);
                            break;
                        case GameSet.PLAYER_DIRECTION.LEFT: //left
                            GameObject.player.X = Math.Max(GameObject.player.X, GameSet.MAP_MIN_X + 2);
                            GameObject.boxes[boxId].X = Math.Max(GameObject.boxes[boxId].X - 1, GameSet.MAP_MIN_X + 1);
                            break;
                        case GameSet.PLAYER_DIRECTION.DOWN: //down
                            GameObject.player.Y = Math.Min(GameObject.player.Y, GameSet.MAP_MAX_Y - 1);
                            GameObject.boxes[boxId].Y = Math.Min(GameObject.boxes[boxId].Y + 1, GameSet.MAP_MAX_Y);
                            break;
                        case GameSet.PLAYER_DIRECTION.UP: //up
                            GameObject.player.Y = Math.Max(GameObject.player.Y, GameSet.MAP_MIN_Y + 2);
                            GameObject.boxes[boxId].Y = Math.Max(GameObject.boxes[boxId].Y - 1, GameSet.MAP_MIN_Y + 1);
                            break;
                    }

                }
                if (IsCollide(GameObject.player.X, GameObject.boxes[boxId].X, GameObject.player.Y, GameObject.boxes[boxId].Y) && !GameRule.SameColor(GameObject.player.Color, GameObject.boxes[boxId].Color))
                {
                    switch (GameSet.playerDir)
                    {
                        case GameSet.PLAYER_DIRECTION.RIGHT: //right
                            GameObject.player.X = Math.Min(GameObject.boxes[boxId].X - 1, GameObject.boxes[boxId].X - 1);
                            GameObject.boxes[boxId].X = Math.Min(GameObject.boxes[boxId].X, GameObject.boxes[boxId].X);
                            break;
                        case GameSet.PLAYER_DIRECTION.LEFT: //left
                            GameObject.player.X = Math.Max(GameObject.boxes[boxId].X + 1, GameObject.boxes[boxId].X + 1);
                            GameObject.boxes[boxId].X = Math.Max(GameObject.boxes[boxId].X, GameObject.boxes[boxId].X);
                            break;
                        case GameSet.PLAYER_DIRECTION.DOWN: //down
                            GameObject.player.Y = Math.Min(GameObject.boxes[boxId].Y - 1, GameObject.boxes[boxId].Y - 1);
                            GameObject.boxes[boxId].Y = Math.Min(GameObject.boxes[boxId].Y, GameObject.boxes[boxId].Y);
                            break;
                        case GameSet.PLAYER_DIRECTION.UP: //up
                            GameObject.player.Y = Math.Max(GameObject.boxes[boxId].Y + 1, GameObject.boxes[boxId].Y + 1);
                            GameObject.boxes[boxId].Y = Math.Max(GameObject.boxes[boxId].Y, GameObject.boxes[boxId].Y);
                            break;
                    }
                }
            }
        }

        public static void WithPlayerWall()
        {
            for (int wallId = 0; wallId < GameObject.wallLength; wallId++) //벽과 플레이어
            {

                if (IsCollide(GameObject.player.X, GameObject.walls[wallId].X, GameObject.player.Y, GameObject.walls[wallId].Y))
                {

                    switch (GameSet.playerDir)
                    {

                        case GameSet.PLAYER_DIRECTION.RIGHT: //right
                            GameObject.player.X = GameObject.walls[wallId].X - 1;
                            break;
                        case GameSet.PLAYER_DIRECTION.LEFT: //left
                            GameObject.player.X = GameObject.walls[wallId].X + 1;
                            break;
                        case GameSet.PLAYER_DIRECTION.DOWN: //down
                            GameObject.player.Y = GameObject.walls[wallId].Y - 1;
                            break;
                        case GameSet.PLAYER_DIRECTION.UP: //up
                            GameObject.player.Y = GameObject.walls[wallId].Y + 1;
                            break;

                    }
                }

            } //벽과 플레이어
        }

        public static void WithBoxWall()
        {

            for (int boxId = 0; boxId < GameObject.boxLength; boxId++) //벽과 박스
            {

                for (int wallId = 0; wallId < GameObject.wallLength; wallId++)
                {

                    if (IsCollide(GameObject.boxes[boxId].X, GameObject.walls[wallId].X, GameObject.boxes[boxId].Y, GameObject.walls[wallId].Y))
                    {

                        switch (GameSet.playerDir)
                        {

                            case GameSet.PLAYER_DIRECTION.RIGHT: //right
                                GameObject.player.X = GameObject.walls[wallId].X - 2;
                                GameObject.boxes[boxId].X = GameObject.walls[wallId].X - 1;
                                break;
                            case GameSet.PLAYER_DIRECTION.LEFT: //left
                                GameObject.player.X = GameObject.walls[wallId].X + 2;
                                GameObject.boxes[boxId].X = GameObject.walls[wallId].X + 1;
                                break;
                            case GameSet.PLAYER_DIRECTION.DOWN: //down
                                GameObject.player.Y = GameObject.walls[wallId].Y - 2;
                                GameObject.boxes[boxId].Y = GameObject.walls[wallId].Y - 1;
                                break;
                            case GameSet.PLAYER_DIRECTION.UP: //up
                                GameObject.player.Y = GameObject.walls[wallId].Y + 2;
                                GameObject.boxes[boxId].Y = GameObject.walls[wallId].Y + 1;
                                break;

                        }
                    }
                }
            } //벽과 박스
        }

        public static void WithBoxBox()
        {
            for (int boxId = 0; boxId < GameObject.boxLength; boxId++) //박스와 박스 충돌
            {
                for (int boxId2 = 0; boxId2 < GameObject.boxLength; boxId2++)
                {
                    if (boxId == boxId2)
                    {
                        continue;
                    }
                    if (GameObject.boxes[boxId].X == GameObject.boxes[boxId2].X && GameObject.boxes[boxId].Y == GameObject.boxes[boxId2].Y && GameObject.player.PushedBoxId == boxId)
                    {
                        switch (GameSet.playerDir)
                        {
                            case GameSet.PLAYER_DIRECTION.RIGHT: //right
                                GameObject.player.X = GameObject.player.X - 1;
                                GameObject.boxes[boxId2].X = GameObject.player.X + 2;
                                GameObject.boxes[boxId].X = GameObject.player.X + 1;
                                break;
                            case GameSet.PLAYER_DIRECTION.LEFT: //left
                                GameObject.player.X = GameObject.player.X + 1;
                                GameObject.boxes[boxId2].X = GameObject.player.X - 2;
                                GameObject.boxes[boxId].X = GameObject.player.X - 1;
                                break;
                            case GameSet.PLAYER_DIRECTION.DOWN: //down
                                GameObject.player.Y = GameObject.player.Y - 1;
                                GameObject.boxes[boxId2].Y = GameObject.player.Y + 2;
                                GameObject.boxes[boxId].Y = GameObject.player.Y + 1;
                                break;
                            case GameSet.PLAYER_DIRECTION.UP: //up
                                GameObject.player.Y = GameObject.player.Y + 1;
                                GameObject.boxes[boxId2].Y = GameObject.player.Y - 2;
                                GameObject.boxes[boxId].Y = GameObject.player.Y - 1;
                                break;
                        }
                    }
                }
            } //박스와 박스 충돌
        }

        public static void WithPlayerHitem()
        {
            for (int itemId = 0; itemId < GameObject.itemLength; itemId++)
            {
                if (IsCollide(GameObject.player.X, GameObject.horizonItem[itemId].X, GameObject.player.Y, GameObject.horizonItem[itemId].Y))
                {
                    GameObject.hFunction += 10;
                    GameObject.horizonItem[itemId].X = GameSet.MAP_MAX_X + 2;
                    GameObject.horizonItem[itemId].Y = GameSet.MAP_MAX_Y + 2;
                    GameObject.horizonItem[itemId].Symbol = ' ';
                    GameObject.hChangeDir = true;

                    break;
                }

                if (GameObject.hFunction == 0)
                {
                    GameObject.hChangeDir = false;
                }
            }

        }

        public static void WithPlayerVitem()
        {
            for (int itemId = 0; itemId < GameObject.itemLength; itemId++)
            {
                if (IsCollide(GameObject.player.X, GameObject.verticalItem[itemId].X, GameObject.player.Y, GameObject.verticalItem[itemId].Y))
                {
                    GameObject.vFunction += 10;  //효과 횟수
                    GameObject.verticalItem[itemId].X = GameSet.MAP_MAX_X + 2;
                    GameObject.verticalItem[itemId].Y = GameSet.MAP_MAX_Y + 2;
                    GameObject.verticalItem[itemId].Symbol = ' ';
                    GameObject.vChangeDir = true;

                    break;
                }

                if (GameObject.vFunction == 0)
                {
                    GameObject.vChangeDir = false;
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
