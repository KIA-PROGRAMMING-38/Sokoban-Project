using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    internal class GameRule
    {
        public static void SpawnBox(int Id, int spawnX, int spawnY)
        {
                if (IsCollide(GameScene.player.X, GameScene.pointItems[Id].X, GameScene.player.Y, GameScene.pointItems[Id].Y))
                {
                    GameScene.pointItems[Id].X = GameSet.hidePointX;
                    GameScene.pointItems[Id].Y = GameSet.hidePointY;
                    GameScene.pointItems[Id].Symbol = ' ';


                    GameScene.boxes[Id].X = spawnX;
                    GameScene.boxes[Id].Y = spawnY;
                    GameScene.boxes[Id].Symbol = '■';
                }
        }

        public static void ChangePlayerColor()
        {
            for (int boxId = 0; boxId < GameScene.colorBoxes.Length; boxId++)
            {
                if (IsCollide(GameScene.changer.X, GameScene.colorBoxes[boxId].X, GameScene.changer.Y, GameScene.colorBoxes[boxId].Y))
                {
                    GameScene.changer.Color = ConsoleColor.Red;
                    GameScene.player.Color = GameScene.colorBoxes[boxId].Color;

                    break;
                }
                else
                {
                    GameScene.changer.Color = ConsoleColor.Black;
                }
            }
        }

        public static void AddPoint()
        {
            for (int pItemId = 0; pItemId < GameScene.pointItems.Length; pItemId++)
            {
                if (IsCollide(GameScene.player.X, GameScene.pointItems[pItemId].X, GameScene.player.Y, GameScene.pointItems[pItemId].Y))
                {
                    GameObject.point++;
                    GameScene.pointItems[pItemId].X = GameSet.hidePointX;
                    GameScene.pointItems[pItemId].Y = GameSet.hidePointY;
                    GameScene.pointItems[pItemId].Symbol = ' ';

                    break;

                }
            }
        }

        public static void OpenWall()
        {
            for (int goalId = 0; goalId < GameScene.goals.Length; goalId++)
            {
                for (int wallId = 0; wallId < GameScene.colorWalls.Length; wallId++)
                {
                    if (GameScene.boxes[goalId].IsOnGoal == true && SameColor(GameScene.goals[goalId].Color, GameScene.colorWalls[wallId].color))
                    {
                        GameScene.colorWalls[wallId].X = 20;
                        GameScene.colorWalls[wallId].Y = 20;
                        GameScene.colorWalls[wallId].Symbol = ' ';

                    }
                    else if (GameScene.boxes[goalId].IsOnGoal == false && SameColor(GameScene.goals[goalId].Color, GameScene.colorWalls[wallId].color))
                    {
                        GameScene.colorWalls[wallId].X = GameScene.colorWalls[wallId].DefaultX;
                        GameScene.colorWalls[wallId].Y = GameScene.colorWalls[wallId].DefaultY;
                        GameScene.colorWalls[wallId].Symbol = '#';
                    }


                }
            }
        }

        public static void GoalInJudge()
        {
            for (int boxId = 0; boxId < GameScene.boxes.Length; boxId++) // 골인 판정
            {
                GameScene.boxes[boxId].IsOnGoal = false;

                for (int goalId = 0; goalId < GameScene.goals.Length; goalId++)
                {

                    if (IsCollide(GameScene.boxes[boxId].X, GameScene.goals[goalId].X, GameScene.boxes[boxId].Y, GameScene.goals[goalId].Y) && SameColor(GameScene.boxes[boxId].Color, GameScene.goals[goalId].Color))
                    {

                        GameScene.boxes[boxId].IsOnGoal = true;
                    }

                    else if (IsCollide(GameScene.boxes[boxId].X, GameScene.goals[goalId].X, GameScene.boxes[boxId].Y, GameScene.goals[goalId].Y) && !SameColor(GameScene.boxes[boxId].Color, GameScene.goals[goalId].Color))
                    {

                        switch (GameSet.playerDir)
                        {

                            case GameSet.PLAYER_DIRECTION.RIGHT: //right
                                GameScene.player.X = GameScene.player.X - 1;
                                GameScene.boxes[boxId].X = GameScene.boxes[boxId].X - 1;
                                break;
                            case GameSet.PLAYER_DIRECTION.LEFT: //left
                                GameScene.player.X = GameScene.player.X + 1;
                                GameScene.boxes[boxId].X = GameScene.boxes[boxId].X + 1;
                                break;
                            case GameSet.PLAYER_DIRECTION.DOWN: //down
                                GameScene.player.Y = GameScene.player.Y - 1;
                                GameScene.boxes[boxId].Y = GameScene.boxes[boxId].Y - 1;
                                break;
                            case GameSet.PLAYER_DIRECTION.UP: //up
                                GameScene.player.Y = GameScene.player.Y + 1;
                                GameScene.boxes[boxId].Y = GameScene.boxes[boxId].Y + 1;
                                break;
                        }
                    }

                }
            }
        }

        public static void JudgeClear()
        {
            if (IsCollide(GameScene.player.X, GameScene.exitPoint.X, GameScene.player.Y, GameScene.exitPoint.Y) && 0 < GameObject.move && GameObject.move <= 155) // 클리어 판정
            {
                Console.Clear();
                string[] clearScene = File.ReadAllLines(Path.Combine("Assets", "Scene", "clear.txt"));
                for (int i = 0; i < clearScene.Length; i++)
                {
                    Console.WriteLine(clearScene[i]);
                }
                Thread.Sleep(3000);

                Console.ReadLine();
                GameObject.clearJudge = false;

            }
            else if (IsCollide(GameScene.player.X, GameScene.exitPoint.X, GameScene.player.Y, GameScene.exitPoint.Y) && 150 < GameObject.move && GameObject.move <= 180)
            {
                Console.Clear();
                string[] clearScene = File.ReadAllLines(Path.Combine("Assets", "Scene", "clear.txt"));

                for (int i = 0; i < clearScene.Length; i++)
                {
                    Console.WriteLine(clearScene[i]);
                }

                Thread.Sleep(3000);

                Console.ReadLine();
                GameObject.clearJudge = false;
            }
            else if (IsCollide(GameScene.player.X, GameScene.exitPoint.X, GameScene.player.Y, GameScene.exitPoint.Y) && 180 < GameObject.move)
            {
                Console.Clear();

                string[] clearScene = File.ReadAllLines(Path.Combine("Assets", "Scene", "clear.txt"));
                for (int i = 0; i < clearScene.Length; i++)
                {
                    Console.WriteLine(clearScene[i]);
                }

                Thread.Sleep(3000);


                Console.ReadLine();
                GameObject.clearJudge = false;
               
            }
            if (GameObject.playerHpNumber == 0)
            {
                Console.Clear();
                Console.WriteLine("빡대가리 시군요");
                Thread.Sleep(2000);

                Console.ReadLine();
                GameObject.clearJudge = false;
            }
        }

        public static void Losshp()
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

        public static bool IsCollide(int x1, int x2, int y1, int y2)
        {
            if (x1 == x2 && y1 == y2)
                return true;
            else
                return false;
        }

        public static bool SameColor(ConsoleColor color1, ConsoleColor color2)
        {
            if (color1 == color2)
                return true;
            else
                return false;
        }
    }
}
