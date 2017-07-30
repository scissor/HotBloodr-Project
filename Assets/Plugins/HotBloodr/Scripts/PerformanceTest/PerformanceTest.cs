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