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