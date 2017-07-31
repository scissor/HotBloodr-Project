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

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HotBloodr
{
    public class PerformanceTest : MonoBehaviour
    {
        private List<PerformanceTester> m_testers;
        private PerformanceTester m_tester;

        void Awake()
        {
            m_testers = GetComponentsInChildren<PerformanceTester>().ToList();
            m_tester = m_testers.First();
        }

        void OnGUI()
        {
            GUI.backgroundColor = Color.red;
            GUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Next"))
                {
                    m_tester = m_testers.CircularNext(m_tester);
                }

                if (GUILayout.Button("Previous"))
                {
                    m_tester = m_testers.CircularPrev(m_tester);
                }
            }
            GUILayout.EndHorizontal();
            GUI.backgroundColor = Color.white;

            m_tester.DrawGUI();
        }
    }
}