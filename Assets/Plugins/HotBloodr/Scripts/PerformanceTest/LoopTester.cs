using UnityEngine;

namespace HotBloodr
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
