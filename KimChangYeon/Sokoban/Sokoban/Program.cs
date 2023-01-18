using Sokoban;
using System.ComponentModel;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Text.Encodings.Web;
using System.Xml;

namespace sokoban
{
    class Program
    {
        static void Main()
        {
            GameSet.SetGame();
            
            // 게임 루프 == 프레임(Frame)
            while (GameObject.clearJudge)
            {
                // --------------------------------------------- Render -------------------------------------------------------

                Render.MapRender();
                Render.GoalRender();
                Render.WallRender();
                Render.RenderExit();
                Render.ChangeRender();
                Render.ItemRender();
                Render.RenderColorBox();
                Render.ChangerRender();
                Render.RenderPointItem();
                Render.PlayerRender();
                Render.StringRender();

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
                GameRule.ChangePlayerColor();
                GameRule.GoalInJudge();
                GameRule.OpenWall();
                GameRule.AddPoint();

                GameRule.JudgeClear();
            }  
        }   
    }
}

