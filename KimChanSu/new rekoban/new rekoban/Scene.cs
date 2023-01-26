using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace new_rekoban
{
    /// <summary>
    /// 게임 스테이지 구현 씬 전환
    /// </summary>
    enum Scene
    {
        None,
        Lobby,
        Stage1,
        Stage2,
        Stage3
    }
    internal class SceneManager
    {
        public static Scene _sceneType;
        // 인스턴스(Instance)
        public static Player player = new Player();
        public static Renderer renderer = new Renderer();

        // 플레이어가 골 위에 있는지 판별할 수 있는 데이터 생성
        public static bool isPlayerOnGoal = false;
        public static bool isGameClear = false;

        public static void InitStage1()
        {
            Wall._x = new int[9] { 6, 11, 1, 14, 10, 3, 5, 14, 7 };
            Wall._y = new int[9] { 1, 4, 5, 5, 8, 9, 10, 10, 11 };
            Wall.count = Wall._x.Length;
        }

        public static void InitStage2()
        {
            Wall._x = new int[12] { 14, 8, 9, 13, 5, 14, 13, 8, 3, 14, 11, 9 };
            Wall._y = new int[12] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 9, 11, 12 };
            Wall.count = Wall._x.Length;
        }

        public static void InitStage3()
        {
            Wall._x = new int[13] { 9, 4, 10, 2, 1, 9, 14, 7, 3, 14, 8, 6, 10 };
            Wall._y = new int[13] { 1, 2, 3, 4, 5, 5, 6, 7, 8, 9, 10, 11, 11 };
            Wall.count = Wall._x.Length;
        }

        public static void Stage1()
        {
            Render();

            ConsoleKeyInfo keyinfo = Console.ReadKey();
            ConsoleKey key = keyinfo.Key;

            Update(key);

            if (isPlayerOnGoal)
            {
                player._x = 14;
                player._y = 12;
                _sceneType = Scene.Stage2;
                isPlayerOnGoal = false;
            }
        }

        public static void Stage2()
        {
            Render();

            ConsoleKeyInfo keyinfo = Console.ReadKey();
            ConsoleKey key = keyinfo.Key;

            Update(key);

            if (isPlayerOnGoal)
            {
                player._x = 14;
                player._y = 12;
                _sceneType = Scene.Stage3;
                isPlayerOnGoal = false;
            }
        }

        public static void Stage3()
        {
            Render();

            ConsoleKeyInfo keyinfo = Console.ReadKey();
            ConsoleKey key = keyinfo.Key;

            Update(key);

            if (isPlayerOnGoal)
            {
                isGameClear = true;
            }
        }
 
        public static void Render()
        {
            // 이전 프레임 지우기
            Console.Clear();

            // 플레이어 그리기
            Console.ForegroundColor = ConsoleColor.White;
            renderer.Render(player._x, player._y, player._symbol);
            Console.ForegroundColor = ConsoleColor.Magenta;

            // 출구 그리기
            Console.ForegroundColor = ConsoleColor.White;
            renderer.Render(Game.GOAL_X, Game.GOAL_Y, "E");
            Console.ForegroundColor = ConsoleColor.Magenta;

            // 벽 그리기
            for (int i = 0; i < Wall.count; ++i)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                renderer.Render(Wall._x[i], Wall._y[i], "*");
                Console.ForegroundColor = ConsoleColor.Magenta;
            }

            // 울타리 그리기
            for (int i = 0; i < Fence.count; ++i)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                renderer.Render(Fence._x[i], Fence._y[i], "@");
                Console.ForegroundColor = ConsoleColor.Magenta;
            }
        }

        public static void Update(ConsoleKey key)
        {
            // 플레이어의 이동 처리
            player.Move(key);

            // 플레이어가 골 위에 올라왔는지 확인
            if (player._x == Game.GOAL_X && player._y == Game.GOAL_Y)
            {
                isPlayerOnGoal = true;
            }
        }
    }
}