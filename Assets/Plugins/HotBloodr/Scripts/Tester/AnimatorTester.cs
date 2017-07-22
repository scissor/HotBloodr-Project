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
    public class AnimatorTester : MonoBehaviour
    {
        [SerializeField]
        private string m_state;

        [SerializeField]
        private List<string> m_states;

        private Animator m_animator;


        void Awake()
        {
            Initialize();
        }

        void OnValidate()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (m_animator != null)
            {
                return;
            }

            m_animator = GetComponent<Animator>();

            if (m_animator == null)
            {
                return;
            }

            m_states = new List<string>();

            var ac = m_animator.runtimeAnimatorController as UnityEditor.Animations.AnimatorController;
            var layers = ac.layers;

            foreach (var layer in layers)
            {
                foreach (var state in layer.stateMachine.states)
                {
                    m_states.Add(state.state.name);
                }
            }

            m_state = m_states.First();
        }

        public void Play()
        {
            m_animator.Play(m_state);
        }

        public void Previous()
        {
            m_state = m_states.CircularNext(m_state);
            Play();
        }

        public void Next()
        {
            m_state = m_states.CircularNext(m_state);
            Play();
        }
    }
}


#if UNITY_EDITOR

#endif