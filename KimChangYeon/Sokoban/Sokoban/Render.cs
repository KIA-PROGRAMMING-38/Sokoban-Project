using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    internal class Render
    {
        public static void IsRender(ConsoleColor color, int x, int y, char symbol)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
        }

        public static void RenderExit()
        {
            IsRender(ConsoleColor.Cyan, GameScene.exitPoint.X, GameScene.exitPoint.Y, GameScene.exitPoint.Symbol);
        }

        public static void RenderStage(int stageNumber)
        {

            string[] stage = File.ReadAllLines(Path.Combine("Assets", "Stage", $"Stage{stageNumber:D2}.txt"));
            string[] Length = stage[stage.Length - 1].Split(" ");
            
            Console.Clear();
           
            
            int wallIndex = 0;
            int trapIndex = 0;
            

            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < stage.Length - 1; i++)
            {
                Console.WriteLine(stage[i]);
            }

            for (int y = 0; y < stage.Length; y++)
            {
                for (int x = 0; x < stage[y].Length; x++)
                {
                    switch (stage[y][x])
                    {
                        case '#':
                            Console.ForegroundColor = ConsoleColor.White;
                            GameScene.walls[wallIndex] = new GameObject.Wall { X = x, Y = y };
                            wallIndex++;
                            break;

                        case '♨':
                            Console.ForegroundColor = ConsoleColor.Red;
                            GameScene.traps[trapIndex] = new GameObject.Trap { X = x, Y = y };
                            trapIndex++;
                            break;

                    }
                }
            }
        }

        public static void RenderGoal()
        {
            for (int goalId = 0; goalId < GameScene.goals.Length; goalId++)
            {
                IsRender(GameScene.goals[goalId].Color, GameScene.goals[goalId].X, GameScene.goals[goalId].Y, GameScene.goals[goalId].Symbol);
            }
        }

        public static void RenderColorWall()
        {
            for (int wallId = 0; wallId < GameScene.colorWalls.Length; wallId++)
            {
                IsRender(GameScene.colorWalls[wallId].color, GameScene.colorWalls[wallId].X, GameScene.colorWalls[wallId].Y, GameScene.colorWalls[wallId].Symbol);
            }
        }

        public static void RenderChange()
        {
            for (int boxId = 0; boxId < GameScene.boxes.Length; boxId++)
            {
                if (GameScene.boxes[boxId].IsOnGoal)
                {
                    IsRender(GameScene.boxes[boxId].Color, GameScene.boxes[boxId].X, GameScene.boxes[boxId].Y, GameScene.goals[boxId].InSymbol);
                }
                else
                {
                    IsRender(GameScene.boxes[boxId].Color, GameScene.boxes[boxId].X, GameScene.boxes[boxId].Y, GameScene.boxes[boxId].Symbol);
                }
            }

        }

        public static void RenderPlayer()
        {
            IsRender(GameScene.player.Color, GameScene.player.X, GameScene.player.Y, GameScene.player.Symbol);
        }

        public static void RenderHitem()
        {
            for (int itemId = 0; itemId < GameScene.horizonItems.Length; itemId++)
            {
                IsRender(GameScene.horizonItems[itemId].Color, GameScene.horizonItems[itemId].X, GameScene.horizonItems[itemId].Y, GameScene.horizonItems[itemId].Symbol);
            }
        }

        public static void RenderVitem()
        {
            for (int itemId = 0; itemId < GameScene.verticalItems.Length; itemId++)
            {
                IsRender(GameScene.verticalItems[itemId].Color, GameScene.verticalItems[itemId].X, GameScene.verticalItems[itemId].Y, GameScene.verticalItems[itemId].Symbol);
            }
        }

        public static void RenderChanger()
        {
            IsRender(GameScene.changer.Color, GameScene.changer.X, GameScene.changer.Y, GameScene.changer.Symbol);
        }
        
        public static void RenderColorBox()
        {
            for (int boxId = 0; boxId < GameScene.colorBoxes.Length; boxId++)
            {
                IsRender(GameScene.colorBoxes[boxId].Color, GameScene.colorBoxes[boxId].X, GameScene.colorBoxes[boxId].Y, GameScene.colorBoxes[boxId].Symbol);
            }
        }

        public static void RenderPointItem()
        {
            for (int itemId = 0; itemId < GameScene.pointItems.Length; itemId++)
            {
                IsRender(ConsoleColor.Cyan, GameScene.pointItems[itemId].X, GameScene.pointItems[itemId].Y, GameScene.pointItems[itemId].Symbol);
            }
        }

        public static void RenderHp()
        {
            for (int i = 0; i < GameScene.playerHps.Length; i++)
            {
                IsRender(ConsoleColor.DarkRed, 40+ i, 23, GameScene.playerHps[i].Hp);  
            }
            
        }

        public static void RenderLosshp()
        {
            switch (GameObject.playerHpNumber)
            {
                case 4:
                    GameScene.playerHps[4].Hp = GameScene.playerHps[4].LoseHp;
                    break;
                case 3:
                    GameScene.playerHps[3].Hp = GameScene.playerHps[3].LoseHp;
                    break;
                case 2:
                    GameScene.playerHps[2].Hp = GameScene.playerHps[2].LoseHp;
                    break;
                case 1:
                    GameScene.playerHps[1].Hp = GameScene.playerHps[1].LoseHp;
                    break;
            }
        }

        public static void RenderTitle()
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
