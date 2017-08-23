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

using UnityEngine;

namespace HotBloodr.Experiments
{
    public class LoopTester : PerformanceTester
    {
        private class Player
        {
            public int Hp;
            public string Name;
        }

        private Player[] m_playerArray;

        protected override void InitializeTestFunctions()
        {
            m_testFunctions.Add(ForLoop, "ForLoop");
            m_testFunctions.Add(ForEach, "ForEach");

            m_playerArray = new Player[m_testTimes];
            for (int i = 0; i < m_testTimes; i++)
            {
                m_playerArray[i] = new Player();
            }
        }

        public override string Title
        {
            get
            {
                return "Loop Test";
            }
        }

        private void ForLoop()
        {
            for (int i = 0; i < m_testTimes; i++)
            {
                m_playerArray[i].Hp = 0;
            }
        }

        private void ForEach()
        {
            foreach (var player in m_playerArray)
            {
                player.Hp = 0;
            }
        }
    }
}
