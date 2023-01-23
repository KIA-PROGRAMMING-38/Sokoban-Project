using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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


        
        public static int LoadStage(int stageNumber)
        {
            string[] stage = File.ReadAllLines(Path.Combine("Assets", "Stage", $"Stage{stageNumber:D2}.txt"));
            int wallLength = int.Parse(stage[stage.Length - 1]);

            return wallLength;
        }


        

        public static void RenderStage(int stageNumber)
        {

            string[] stage = File.ReadAllLines(Path.Combine("Assets", "Stage", $"Stage{stageNumber:D2}.txt"));
            int wallLength = int.Parse(stage[stage.Length - 1]);
            
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            
           
            
            int wallsIndex = 0;
            
            for (int i = 0; i < stage.Length - 1; i++)
            {
                Console.WriteLine(stage[i]);
            }

            for (int y = 0; y < stage.Length; y++)
            {
                for (int x = 0; x < stage[y].Length; x++)
                {
                    if (stage[y][x] == '#')
                    {
                        GameScene.walls[wallsIndex] = new GameObject.Wall { X = x, Y = y };
                        wallsIndex++;
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

        public static void RenderItem()
        {
            for (int itemId = 0; itemId < GameScene.verticalItems.Length; itemId++)
            {
                IsRender(GameScene.horizonItems[itemId].Color, GameScene.horizonItems[itemId].X, GameScene.horizonItems[itemId].Y, GameScene.horizonItems[itemId].Symbol);

                IsRender(GameScene.verticalItems[itemId].Color, GameScene.verticalItems[itemId].X, GameScene.verticalItems[itemId].Y, GameScene.verticalItems[itemId].Symbol);
            }
        }

        public static void RenderString()
        {
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(GameSet.MAP_MAX_X + 10, 5);
            Console.Write($"↔ : {GameObject.hFunction}");

            Console.SetCursorPosition(GameSet.MAP_MAX_X + 10, 6);
            Console.Write($"↕ : {GameObject.vFunction}");

            Console.SetCursorPosition(GameSet.MAP_MAX_X + 10, GameSet.MAP_MAX_Y - 2);
            Console.Write($"CHANGER(WASD) : {GameObject.moveLimit}");

            Console.SetCursorPosition(GameSet.MAP_MAX_X + 10, 3);
            Console.Write($"* : {GameObject.point} / {GameScene.pointItems.Length}");

            Console.SetCursorPosition(GameSet.MAP_MAX_X + 10, GameSet.MAP_MAX_Y - 4);
            Console.Write($"MOVE : {GameObject.move}");

            Console.SetCursorPosition(GameSet.MAP_MAX_X + 10, GameSet.MAP_MIN_Y + 1);
            Console.Write("HP : ");
            
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

        public static void RenderTrap()
        {
            for (int i = 0; i < GameScene.traps.Length; i++)
            {
                IsRender(ConsoleColor.DarkRed, GameScene.traps[i].X, GameScene.traps[i].Y, GameScene.traps[i].Symbol);
            }
        }

        public static void RenderHp()
        {
            for (int i = 0; i < GameScene.pointItems.Length; i++)
            {
                IsRender(ConsoleColor.DarkRed, GameSet.MAP_MAX_X + 9 + i, GameSet.MAP_MIN_Y + 1, GameScene.playerHps[i].Hp);  
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
