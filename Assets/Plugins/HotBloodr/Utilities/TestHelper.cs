//
// Copyright (C) 2017 Scissor Lee
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//

using System;
using System.Diagnostics;
using UnityEngine;

namespace HotBloodr
{
    public static class TestHelper
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
