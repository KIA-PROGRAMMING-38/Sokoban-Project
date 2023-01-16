using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sokoban
{
    internal class Timer
    {
        // 타이머 관련..
        private float frameInterval = 0.0f;
        private float elaspedTime = 0.0f;
        private float runTime = 0.0f;
        private float prevRunTime = 0.0f;
        private Stopwatch stopwatch = null;

        public float RunTime { get { return runTime; } }

        public Timer(float FramePerSecond)
        {
            stopwatch = new Stopwatch();
            frameInterval = 1.0f / FramePerSecond;

            stopwatch.Start();
            runTime = stopwatch.ElapsedMilliseconds * 0.001f;
            prevRunTime = runTime;
        }

        public bool Update()
        {
            prevRunTime = runTime;
            runTime = stopwatch.ElapsedMilliseconds * 0.001f;

            elaspedTime += runTime - prevRunTime;

            if ( elaspedTime >= frameInterval )
            {
                elaspedTime = 0.0f;

                return true;
            }

            return false;
        }
    }
}
