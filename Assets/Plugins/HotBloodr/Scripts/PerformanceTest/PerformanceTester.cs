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
using System.Collections.Generic;
using UnityEngine;

namespace HotBloodr
{
    public abstract class PerformanceTester : MonoBehaviour
    {
        [SerializeField]
        protected int m_testTimes = 1000000;

        protected Dictionary<Action, string> m_testFunctions = new Dictionary<Action, string>();

        void Awake()
        {
            InitializeTestFunctions();
        }

        public void DrawGUI()
        {
            GUILayout.Label("=== " + Title + " ===");
            GUILayout.Label("TestTimes: " + m_testTimes);

            GUI.color = Color.green;
            {
                if (GUILayout.Button("Test All"))
                {
                    foreach (var pair in m_testFunctions)
                    {
                        TestHelper.MeasureByStopwatch(pair.Key, pair.Value);
                    }
                }
            }
            GUI.color = Color.white;

            foreach (var pair in m_testFunctions)
            {
                if (GUILayout.Button(pair.Value))
                {
                    TestHelper.MeasureByStopwatch(pair.Key, pair.Value);
                }
            }
        }

        protected abstract void InitializeTestFunctions();
        public abstract string Title
        {
            get;
        }
    }
}