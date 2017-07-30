using System;
using System.Diagnostics;
using UnityEngine;

namespace HotBloodr
{
    public class TestHelper
    {
        public static Stopwatch m_stopwatch = new Stopwatch();

        public static void MeasureByDateTime(Action action, string logName)
        {
            var startTime = DateTime.Now;

            action();

            var elapsed = (DateTime.Now - startTime).Milliseconds;

            UnityEngine.Debug.Log(logName + ": " + elapsed);
        }

        public static void MeasureByEnvironmentTickCount(Action action, string logName)
        {
            var startTick = Environment.TickCount;

            action();

            var elapsed = Environment.TickCount - startTick;

            UnityEngine.Debug.Log(logName + ": " + elapsed);
        }

        public static void MeasureByStopwatch(Action action, string logName)
        {
            m_stopwatch.Reset();
            m_stopwatch.Start();

            action();

            m_stopwatch.Stop();

            UnityEngine.Debug.Log(logName + ": " + m_stopwatch.ElapsedMilliseconds);
        }
    }
}
