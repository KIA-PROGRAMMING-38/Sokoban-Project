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
            Console.Title = "능지 미로";
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Clear();

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

            Scene sceneType = Scene.None;

            SceneManager._sceneType = Scene.Stage1;

            while (true)
            {
                if (SceneManager._sceneType != sceneType)
                {
                    sceneType = SceneManager._sceneType;
                    switch (sceneType)
                    {
                        case Scene.Stage1:
                            SceneManager.InitStage1();
                            break;
                        case Scene.Stage2:
                            SceneManager.InitStage2();
                            break;
                        case Scene.Stage3:
                            SceneManager.InitStage3();
                            break;
                    }
                }

                switch (SceneManager._sceneType)
                {
                    case Scene.Stage1:
                        SceneManager.Stage1();
                        break;
                    case Scene.Stage2:
                        SceneManager.Stage2();
                        break;
                    case Scene.Stage3:
                        SceneManager.Stage3();
                        break;
                }

                if (SceneManager.isGameClear)
                {
                    break;
                }
            }

            Console.Clear();
            Console.WriteLine("축하합니다. 게임을 클리어 하셨습니다.");
            Console.ResetColor();
        }
    }
}