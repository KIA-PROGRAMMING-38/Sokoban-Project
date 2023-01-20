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

        static public void RenderMap()
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

        static public void RenderGoal()
        {
            for (int goalId = 0; goalId < GameObject.goalLength; goalId++)
            {
                IsRender(GameObject.goals[goalId].Color, GameObject.goals[goalId].X, GameObject.goals[goalId].Y, GameObject.goals[goalId].Symbol);
            }
        }

        static public void RenderWall()
        {
            for (int wallId = 0; wallId < GameObject.wallLength; wallId++)
            {
                IsRender(GameObject.walls[wallId].Color, GameObject.walls[wallId].X, GameObject.walls[wallId].Y, GameObject.walls[wallId].Symbol);
            }
        }

        static public void RenderChange()
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

        static public void RenderPlayer()
        {
            IsRender(GameObject.player.Color, GameObject.player.X, GameObject.player.Y, GameObject.player.Symbol);
        }

        static public void RenderItem()
        {
            for (int itemId = 0; itemId < GameObject.itemLength; itemId++)
            {
                IsRender(GameObject.horizonItem[itemId].Color, GameObject.horizonItem[itemId].X, GameObject.horizonItem[itemId].Y, GameObject.horizonItem[itemId].Symbol);

                IsRender(GameObject.verticalItem[itemId].Color, GameObject.verticalItem[itemId].X, GameObject.verticalItem[itemId].Y, GameObject.verticalItem[itemId].Symbol);
            }
        }

        static public void RenderString()
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

            Console.SetCursorPosition(GameSet.MAP_MAX_X + 4, GameSet.MAP_MIN_Y + 1);
            Console.Write("HP : ");
            
        }

        static public void RenderChanger()
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

        static public void RenderTrap()
        {
            for (int i = 0; i < GameObject.traps.Length; i++)
            {
                IsRender(ConsoleColor.DarkRed, GameObject.traps[i].X, GameObject.traps[i].Y, GameObject.traps[i].Symbol);
            }
        }

        static public void RenderHp()
        {
            for (int i = 0; i < GameObject.playerHpNumber; i++)
            {
                IsRender(ConsoleColor.DarkRed, GameSet.MAP_MAX_X + 9 + i, GameSet.MAP_MIN_Y + 1, GameObject.playerHp[i].Hp);  
            }
            
        }

        static public void RenderLosshp()
        {
            switch (GameObject.playerHpNumber)
            {
                case 4:
                    GameObject.playerHp[4].Hp = GameObject.playerHp[4].LoseHp;
                    break;
                case 3:
                    GameObject.playerHp[3].Hp = GameObject.playerHp[3].LoseHp;
                    break;
                case 2:
                    GameObject.playerHp[2].Hp = GameObject.playerHp[2].LoseHp;
                    break;
                case 1:
                    GameObject.playerHp[1].Hp = GameObject.playerHp[1].LoseHp;
                    break;
            }
        }

        static public void RenderTitle()
        {
            
           
            Console.SetCursorPosition(93, 4);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("                                                                                    :::::::::");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("   ::::::::   ::::::::  :::    :::  ::::::::  :::::::::      :::     ::::    :::  :::     :::");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("  :+:    :+: :+:    :+: :+:   :+:  :+:    :+: :+:    :+:   :+: :+:   :+:+:   :+:  :+:     :+:");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  +:+        +:+    +:+ +:+  +:+   +:+    +:+ +:+    +:+  +:+   +:+  :+:+:+  +:+         +:+");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  +#++:++#++ +#+    +:+ +#++:++    +#+    +:+ +#++:++#+  +#++:++#++: +#+ +:+ +#+        +#+");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("         +#+ +#+    +#+ +#+  +#+   +#+    +#+ +#+    +#+ +#+     +#+ +#+  +#+#+#      +#+");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("  #+#    #+# #+#    #+# #+#   #+#  #+#    #+# #+#    #+# #+#     #+# #+#   #+#+#      +#+  ");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("   ########   ########  ###    ###  ########  #########  ###     ### ###    ####            ");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("                                                                                      ###");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("                                                                                      ###");

            Console.SetCursorPosition(39, 20);
            Console.WriteLine("Press F11 -> Enter");


            ConsoleKey key = Console.ReadKey().Key;
        }
        

        
    }
}
