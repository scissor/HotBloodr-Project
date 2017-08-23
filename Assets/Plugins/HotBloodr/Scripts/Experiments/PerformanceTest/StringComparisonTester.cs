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
using UnityEngine;

namespace HotBloodr.Experiments
{
    public class StringComparisonTester : PerformanceTester
    {
        private string m_lString = "lString";
        private string m_rString = "rString";

        protected override void InitializeTestFunctions()
        {
            m_testFunctions.Add(EqualityOperator, "EqualityOperator");
            m_testFunctions.Add(StringEquals, "StringEquals");
            m_testFunctions.Add(StringEqualsByIgnoreCase, "StringEqualsByIgnoreCase");
            m_testFunctions.Add(AnimatorHash, "AnimatorHash");
        }

        public override string Title
        {
            get
            {
                return "String Comparison Test";
            }
        }

        private void EqualityOperator()
        {
            for (int i = 0; i < m_testTimes; i++)
            {
                if (m_lString == m_rString)
                {
                }
            }
        }

        private void StringEquals()
        {
            for (int i = 0; i < m_testTimes; i++)
            {
                if (string.Equals(m_lString, m_rString))
                {
                }
            }
        }

        private void StringEqualsByIgnoreCase()
        {
            for (int i = 0; i < m_testTimes; i++)
            {
                if (string.Equals(m_lString, m_rString, StringComparison.CurrentCultureIgnoreCase))
                {
                }
            }
        }

        private void AnimatorHash()
        {
            for (int i = 0; i < m_testTimes; i++)
            {
                var lHash = Animator.StringToHash(m_lString);
                var rHash = Animator.StringToHash(m_rString);
                if (lHash == rHash)
                {
                }
            }
        }
    }
}
