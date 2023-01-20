using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    internal class GameScene
    {
        public static void Scene1()
        {
            while (GameObject.clearJudge)
            {
                // --------------------------------------------- Render -------------------------------------------------------

                Render.RenderMap();
                Render.RenderGoal();
                Render.RenderWall();
                Render.RenderExit();
                Render.RenderChange();
                Render.RenderItem();
                Render.RenderColorBox();
                Render.RenderChanger();
                Render.RenderPointItem();
                Render.RenderTrap();
                Render.RenderString();
                Render.RenderHp();
                Render.RenderPlayer();

                // --------------------------------------------- ProcessInput -------------------------------------------------

                Input.InputKey();

                // --------------------------------------------- Update -------------------------------------------------------

                Move.Right();
                Move.Left();
                Move.Up();
                Move.Down();

                GameRule.SpawnBox();
                OnColision.WithPlayerBox();
                OnColision.WithBoxWall();
                OnColision.WithBoxBox();
                OnColision.WithPlayerWall();
                OnColision.WithPlayerHitem();
                OnColision.WithPlayerVitem();
                OnColision.WithPlayerTrap();

                GameRule.ChangePlayerColor();
                GameRule.Losshp();
                GameRule.GoalInJudge();
                GameRule.OpenWall();
                GameRule.AddPoint();
                GameRule.JudgeClear();
            }


        }
    }      
}

