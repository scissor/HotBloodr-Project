//
// Copyright (C) 2018 Scissor Lee
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

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;

namespace HotBloodr
{
    public class TimelineTester : MonoBehaviour
    {
        [SerializeField]
        private List<PlayableAsset> m_playables;

        private PlayableDirector m_director;

        private PlayableAsset m_playable;

        private bool m_isAuto;

        public PlayableAsset Playable
        {
            get { return m_playable; }
        }

        public List<PlayableAsset> Playables
        {
            get { return m_playables; }
        }

        public bool IsAuto
        {
            get { return m_isAuto; }
            set
            {
                if (!m_isAuto && value)
                {
                    StartCoroutine(AutoTest());
                }

                m_isAuto = value;
            }
        }

        private void Awake()
        {
            m_director = gameObject.GetComponentInChildren<PlayableDirector>();

            Play(m_playables.First());
        }

        public void Previous()
        {
            Play(m_playables.CircularPrev(m_playable));
        }

        public void Next()
        {
            Play(m_playables.CircularNext(m_playable));
        }

        public void Play(PlayableAsset playable)
        {
            m_playable = playable;

            m_director.playableAsset = playable;

            m_director.enabled = false;
            m_director.enabled = true;
        }

        private IEnumerator AutoTest()
        {
            Next();

            yield return new WaitForSeconds((float)m_director.duration);

            if (m_isAuto)
            {
                StartCoroutine(AutoTest());
            }
        }
    }
}