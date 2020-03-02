//
// Copyright (C) 2020 Scissor Lee
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

namespace HotBloodr
{
    public class OnTestEventNotified : IHotEvent
    {
        public string TestString;
        public int TestInt;

        public OnTestEventNotified(int testInt, string testString)
        {
            TestInt = testInt;
            TestString = testString;
        }
    }

    public class EventorTester : MonoBehaviour
    {
        private void OnEnable()
        {
            Eventor.Subscribe<OnTestEventNotified>(HandleTestEventRaised);
            Debug.Log("Subscribe");
        }

        private void OnDisable()
        {
            Eventor.Unsubscribe<OnTestEventNotified>(HandleTestEventRaised);
            Debug.Log("Unsubscribe");
        }

        void OnGUI()
        {
            GUI.backgroundColor = Color.red;
            GUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Enable"))
                {
                    gameObject.SetActive(true);
                }

                if (GUILayout.Button("Disable"))
                {
                    gameObject.SetActive(false);
                }

                if (GUILayout.Button("Notify"))
                {
                    Eventor.Notify(new OnTestEventNotified(999, "HotEvent Test"));
                }
            }
            GUILayout.EndHorizontal();
            GUI.backgroundColor = Color.white;
        }

        private void HandleTestEventRaised(OnTestEventNotified e)
        {
            Debug.Log("OnTestEventNotified");
            Debug.Log($"e.TestInt: {e.TestInt}");
            Debug.Log($"e.TestString: {e.TestString}");
        }
    }
}