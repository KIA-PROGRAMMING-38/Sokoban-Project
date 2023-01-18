using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    internal class GameRule
    {
        public static void SpawnBox()
        {
            for (int pItemId = 0; pItemId < GameObject.pItemLength; pItemId++)
            {
                if (IsCollide(GameObject.player.X, GameObject.pointItems[pItemId].X, GameObject.player.Y, GameObject.pointItems[pItemId].Y))
                {
                    GameObject.boxes[pItemId].X = 10;
                    GameObject.boxes[pItemId].Y = 6;
                    GameObject.boxes[pItemId].Symbol = '■';

                    break;
                }
            }
        }

        public static void ChangePlayerColor()
        {
            for (int boxId = 0; boxId < GameObject.colorBoxLength; boxId++)
            {
                if (IsCollide(GameObject.changer.X, GameObject.colorboxes[boxId].X, GameObject.changer.Y, GameObject.colorboxes[boxId].Y))
                {
                    GameObject.changer.Color = ConsoleColor.Red;
                    GameObject.player.Color = GameObject.colorboxes[boxId].Color;

                    break;
                }
                else
                {
                    GameObject.changer.Color = ConsoleColor.Black;
                }
            }
        }

        public static void AddPoint()
        {
            for (int pItemId = 0; pItemId < GameObject.pItemLength; pItemId++)
            {
                if (IsCollide(GameObject.player.X, GameObject.pointItems[pItemId].X, GameObject.player.Y, GameObject.pointItems[pItemId].Y))
                {
                    GameObject.point++;
                    GameObject.pointItems[pItemId].X = GameSet.MAP_MAX_X + 4;
                    GameObject.pointItems[pItemId].Y = GameSet.MAP_MAX_Y + 4;
                    GameObject.pointItems[pItemId].Symbol = ' ';

                    break;

                }
            }
        }

        public static void OpenWall()
        {
            for (int goalId = 0; goalId < GameObject.goalLength; goalId++)
            {
                for (int wallId = 0; wallId < GameObject.wallLength; wallId++)
                {
                    if (GameObject.boxes[goalId].IsOnGoal == true && SameColor(GameObject.goals[goalId].Color, GameObject.walls[wallId].Color))
                    {
                        GameObject.walls[wallId].X = 20;
                        GameObject.walls[wallId].Y = 20;
                        GameObject.walls[wallId].Symbol = ' ';

                    }
                    else if (GameObject.boxes[goalId].IsOnGoal == false && SameColor(GameObject.goals[goalId].Color, GameObject.walls[wallId].Color))
                    {
                        GameObject.walls[wallId].X = GameObject.walls[wallId].DefaultX;
                        GameObject.walls[wallId].Y = GameObject.walls[wallId].DefaultY;
                        GameObject.walls[wallId].Symbol = '#';
                    }


                }
            }
        }

        public static void GoalInJudge()
        {
            for (int boxId = 0; boxId < GameObject.boxLength; boxId++) // 골인 판정
            {
                GameObject.boxes[boxId].IsOnGoal = false;

                for (int goalId = 0; goalId < GameObject.goalLength; goalId++)
                {

                    if (IsCollide(GameObject.boxes[boxId].X, GameObject.goals[goalId].X, GameObject.boxes[boxId].Y, GameObject.goals[goalId].Y) && SameColor(GameObject.boxes[boxId].Color, GameObject.goals[goalId].Color))
                    {

                        GameObject.boxes[boxId].IsOnGoal = true;
                    }

                    else if (IsCollide(GameObject.boxes[boxId].X, GameObject.goals[goalId].X, GameObject.boxes[boxId].Y, GameObject.goals[goalId].Y) && !SameColor(GameObject.boxes[boxId].Color, GameObject.goals[goalId].Color))
                    {

                        switch (GameSet.playerDir)
                        {

                            case GameSet.PLAYER_DIRECTION.RIGHT: //right
                                GameObject.player.X = GameObject.player.X - 1;
                                GameObject.boxes[boxId].X = GameObject.boxes[boxId].X - 1;
                                break;
                            case GameSet.PLAYER_DIRECTION.LEFT: //left
                                GameObject.player.X = GameObject.player.X + 1;
                                GameObject.boxes[boxId].X = GameObject.boxes[boxId].X + 1;
                                break;
                            case GameSet.PLAYER_DIRECTION.DOWN: //down
                                GameObject.player.Y = GameObject.player.Y - 1;
                                GameObject.boxes[boxId].Y = GameObject.boxes[boxId].Y - 1;
                                break;
                            case GameSet.PLAYER_DIRECTION.UP: //up
                                GameObject.player.Y = GameObject.player.Y + 1;
                                GameObject.boxes[boxId].Y = GameObject.boxes[boxId].Y + 1;
                                break;
                        }
                    }

                }
            }
        }

        public static void JudgeClear()
        {
            if (IsCollide(GameObject.player.X, GameObject.exitPoint.X, GameObject.player.Y, GameObject.exitPoint.Y) && 0 < GameObject.move && GameObject.move <= 150) // 클리어 판정
            {
                Console.Clear();
                Console.WriteLine("★★★ Clear!");

                GameObject.clearJudge = false;

            }
            else if (IsCollide(GameObject.player.X, GameObject.exitPoint.X, GameObject.player.Y, GameObject.exitPoint.Y) && 150 < GameObject.move && GameObject.move <= 180)
            {
                Console.Clear();
                Console.WriteLine("★★☆ Clear!");


                GameObject.clearJudge = false;
            }
            else if (IsCollide(GameObject.player.X, GameObject.exitPoint.X, GameObject.player.Y, GameObject.exitPoint.Y) && 180 < GameObject.move)
            {
                Console.Clear();
                Console.WriteLine("★☆☆ Clear!");


                GameObject.clearJudge = false;
            }
        }

        static public bool IsCollide(int x1, int x2, int y1, int y2)
        {
            if (x1 == x2 && y1 == y2)
                return true;
            else
                return false;
        }

        static public bool SameColor(ConsoleColor color1, ConsoleColor color2)
        {
            if (color1 == color2)
                return true;
            else
                return false;
        }
    }
}
