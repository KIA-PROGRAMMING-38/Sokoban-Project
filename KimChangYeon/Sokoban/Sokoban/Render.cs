using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    internal class Render
    {
        static public void IsRender(ConsoleColor color, int x, int y, char symbol)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
        }

        static public void RenderExit()
        {
            IsRender(ConsoleColor.Cyan, GameObject.exitPoint.X, GameObject.exitPoint.Y, GameObject.exitPoint.Symbol);
        }

        static public void MapRender()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("######################");

            for (int i = 0; i <= GameSet.MAP_MAX_Y - 1; i++)
            {
                Console.WriteLine("#                    #");
            }
            for (int j = 0; j <= GameSet.MAP_MAX_X + 1; j++)
            {
                Console.Write("#");
            }
        }

        static public void GoalRender()
        {
            for (int goalId = 0; goalId < GameObject.goalLength; goalId++)
            {
                IsRender(GameObject.goals[goalId].Color, GameObject.goals[goalId].X, GameObject.goals[goalId].Y, GameObject.goals[goalId].Symbol);
            }
        }

        static public void WallRender()
        {
            for (int wallId = 0; wallId < GameObject.wallLength; wallId++)
            {
                IsRender(GameObject.walls[wallId].Color, GameObject.walls[wallId].X, GameObject.walls[wallId].Y, GameObject.walls[wallId].Symbol);
            }
        }

        static public void ChangeRender()
        {
            for (int boxId = 0; boxId < GameObject.boxLength; boxId++)
            {
                if (GameObject.boxes[boxId].IsOnGoal)
                {
                    IsRender(GameObject.boxes[boxId].Color, GameObject.boxes[boxId].X, GameObject.boxes[boxId].Y, GameObject.goals[boxId].InSymbol);
                }
                else
                {
                    IsRender(GameObject.boxes[boxId].Color, GameObject.boxes[boxId].X, GameObject.boxes[boxId].Y, GameObject.boxes[boxId].Symbol);
                }
            }

        }

        static public void PlayerRender()
        {
            IsRender(GameObject.player.Color, GameObject.player.X, GameObject.player.Y, GameObject.player.Symbol);
        }

        static public void ItemRender()
        {
            for (int itemId = 0; itemId < GameObject.itemLength; itemId++)
            {
                IsRender(GameObject.horizonItem[itemId].Color, GameObject.horizonItem[itemId].X, GameObject.horizonItem[itemId].Y, GameObject.horizonItem[itemId].Symbol);

                IsRender(GameObject.verticalItem[itemId].Color, GameObject.verticalItem[itemId].X, GameObject.verticalItem[itemId].Y, GameObject.verticalItem[itemId].Symbol);
            }
        }

        static public void StringRender()
        {
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(GameSet.MAP_MAX_X + 4, 5);
            Console.Write($"↔ : {GameObject.hFunction}");

            Console.SetCursorPosition(GameSet.MAP_MAX_X + 4, 6);
            Console.Write($"↕ : {GameObject.vFunction}");

            Console.SetCursorPosition(GameSet.MAP_MAX_X + 4, GameSet.MAP_MAX_Y - 2);
            Console.Write($"CHANGER(WASD) : {GameObject.moveLimit}");

            Console.SetCursorPosition(GameSet.MAP_MAX_X + 4, 3);
            Console.Write($"* : {GameObject.point} / {GameObject.pItemLength}");

            Console.SetCursorPosition(GameSet.MAP_MAX_X + 4, GameSet.MAP_MAX_Y - 4);
            Console.Write($"MOVE : {GameObject.move}");
        }

        static public void ChangerRender()
        {
            IsRender(GameObject.changer.Color, GameObject.changer.X, GameObject.changer.Y, GameObject.changer.Symbol);
        }
        
        static public void RenderColorBox()
        {
            for (int boxId = 0; boxId < GameObject.colorBoxLength; boxId++)
            {
                IsRender(GameObject.colorboxes[boxId].Color, GameObject.colorboxes[boxId].X, GameObject.colorboxes[boxId].Y, GameObject.colorboxes[boxId].Symbol);
            }
        }

        static public void RenderPointItem()
        {
            for (int itemId = 0; itemId < GameObject.pItemLength; itemId++)
            {
                IsRender(ConsoleColor.Cyan, GameObject.pointItems[itemId].X, GameObject.pointItems[itemId].Y, GameObject.pointItems[itemId].Symbol);
            }
        }
    }
}
