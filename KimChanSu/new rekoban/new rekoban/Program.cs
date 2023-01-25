using new_rekoban;
using System;
using System.Diagnostics.Metrics;
using System.Numerics;
using System.Reflection.Metadata;
using System.Security.Cryptography;

namespace Rekonban
{
    class Program
    {
        static void Main()
        {
            // 기초 세팅
            Console.ResetColor();
            Console.CursorVisible = false;
            Console.Title = "능지패턴";
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Clear();

            // 인스턴스(Instance)
            Player player = new Player();

            Renderer renderer = new Renderer();

            // 울타리의 좌표
            int index = 0;
            for (int i = 0; i <= Game.MAX_X; ++i)
            {
                Fence._x[index] = i;
                Fence._y[index] = Game.MIN_Y;
                ++index;

                Fence._x[index] = i;
                Fence._y[index] = Game.MAX_Y;
                ++index;
            }
            for (int i = 0; i <= Game.MAX_Y; ++i)
            {
                Fence._x[index] = Game.MIN_X;
                Fence._y[index] = i;
                ++index;

                Fence._x[index] = Game.MAX_X;
                Fence._y[index] = i;
                ++index;
            }

            // 플레이어가 골 위에 있는지 판별할 수 있는 데이터 생성
            bool isPlayerOnGoal = false;

            while (true)
            {
                // ========================== Render ==========================
                Render();

                // ========================== ProcessInput ==========================
                // 사용자로부터 입력을 받음
                ConsoleKeyInfo keyinfo = Console.ReadKey();
                ConsoleKey key = keyinfo.Key;


                // ========================== Update ==========================
                Update(key);

                if (isPlayerOnGoal)
                {
                    break;
                }
            }

            Console.Clear();
            Console.WriteLine("축하합니다. 게임을 클리어 하셨습니다.");

            // 게임이 끝났으니 콘솔 세팅을 원상복구
            Console.ResetColor();

            // ================================ 함수 ===========================================

            void Render()
            {
                // 이전 프레임 지우기
                Console.Clear();

                // 플레이어 그리기
                Console.ForegroundColor = ConsoleColor.White;
                renderer.Render(player.GetX(), player.GetY(), player.GetSymbol());
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

            void Update(ConsoleKey key)
            {
                // 플레이어의 이동 처리
                player.Move(key);

                // 플레이어가 골 위에 올라왔는지 확인
                if (player.GetX() == Game.GOAL_X && player.GetY() == Game.GOAL_Y)
                {
                    isPlayerOnGoal = true;
                }
            }
        }
    }
}