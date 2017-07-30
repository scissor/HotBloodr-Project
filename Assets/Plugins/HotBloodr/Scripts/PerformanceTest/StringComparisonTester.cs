using System;
using UnityEngine;

namespace HotBloodr
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
