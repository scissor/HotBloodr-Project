using System;
using System.Collections.Generic;
using UnityEngine;

namespace HotBloodr
{
    public class StringComparisonTest : MonoBehaviour
    {
        [SerializeField]
        private int m_compareTimes = 10000;

        private string m_lString = "lString";
        private string m_rString = "rString";

        private Dictionary<Action, string> m_testFunctions = new Dictionary<Action, string>();

        void Awake()
        {
            m_testFunctions.Add(EqualityOperator, "EqualityOperator");
            m_testFunctions.Add(StringEquals, "StringEquals");
            m_testFunctions.Add(StringEqualsByIgnoreCase, "StringEqualsByIgnoreCase");
            m_testFunctions.Add(AnimatorHash, "AnimatorHash");
        }

        void OnGUI()
        {
            GUILayout.Label("=== String Comparison Test ===");

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

        private void EqualityOperator()
        {
            for (int i = 0; i < m_compareTimes; i++)
            {
                if (m_lString == m_rString)
                {
                }
            }
        }

        private void StringEquals()
        {
            for (int i = 0; i < m_compareTimes; i++)
            {
                if (string.Equals(m_lString, m_rString))
                {
                }
            }
        }

        private void StringEqualsByIgnoreCase()
        {
            for (int i = 0; i < m_compareTimes; i++)
            {
                if (string.Equals(m_lString, m_rString, StringComparison.CurrentCultureIgnoreCase))
                {
                }
            }
        }

        private void AnimatorHash()
        {
            for (int i = 0; i < m_compareTimes; i++)
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
