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

using System;
using UnityEngine;
using UnityEngine.Video;

namespace HotBloodr.Video
{
    public class CameraVideoPlayer : MonoBehaviour, IVideoController
    {
        [SerializeField]
        protected VideoPlayer m_videoPlayer;

        [SerializeField]
        protected AudioSource m_audioSource;

        [SerializeField]
        protected bool m_isDebug;

        protected bool m_isPlaying;
        protected string m_totalTimeString;
        protected Action m_finishedCallback;

        private void Awake()
        {
            m_videoPlayer.prepareCompleted += OnPrepareCompleted;
            m_videoPlayer.loopPointReached += OnLoopPointReached;
            m_videoPlayer.seekCompleted += OnSeekCompleted;
            m_videoPlayer.errorReceived += OnErrorReceived;
        }

        public string TotalTime
        {
            get { return m_totalTimeString; }
        }

        public string NowTime
        {
            get { return StringHelper.GetMinuteSecondTime((float)m_videoPlayer.time); }
        }

        public float Progress
        {
            get { return (float)(m_videoPlayer.frame) / (float)m_videoPlayer.frameCount; }
        }

        public void SetFinishedCallack(Action callback)
        {
            m_finishedCallback = callback;
        }

        public void PrepareByUrl(string url)
        {
            m_videoPlayer.source = VideoSource.Url;
            m_videoPlayer.url = url;
            m_videoPlayer.Prepare();
        }

        public void PrepareByClip(string path)
        {
            var clip = Resources.Load<VideoClip>(path);

            m_videoPlayer.source = VideoSource.VideoClip;
            m_videoPlayer.clip = clip;
            m_videoPlayer.Prepare();
        }

        #region VideoPlayer

        private void OnPrepareCompleted(VideoPlayer player)
        {
            Play();

            var totalTime = m_videoPlayer.frameCount / m_videoPlayer.frameRate;
            m_totalTimeString = StringHelper.GetMinuteSecondTime(totalTime);

            if (m_isDebug)
            {
                Debug.Log("OnPrepareCompleted");
                Debug.Log("FrameRate: " + m_videoPlayer.frameRate);
                Debug.Log("FrameCount: " + m_videoPlayer.frameCount);
                Debug.Log("Time: " + m_videoPlayer.frameCount / m_videoPlayer.frameRate);
            }
        }

        private void OnLoopPointReached(VideoPlayer player)
        {
            if (m_isDebug)
            {
                Debug.Log("OnLoopPointReached: " + m_videoPlayer.frame + "/" + m_videoPlayer.frameCount);
            }

            if ((int)m_videoPlayer.frame == (int)m_videoPlayer.frameCount)
            {
                if (m_finishedCallback != null)
                {
                    m_finishedCallback();
                }

                gameObject.Destroy();
            }
        }

        private void OnSeekCompleted(VideoPlayer player)
        {
            if (m_isDebug)
            {
                Debug.Log("OnSeekCompleted: " + m_videoPlayer.frame);
            }
        }

        private void OnErrorReceived(VideoPlayer player, string message)
        {
            if (m_isDebug)
            {
                Debug.Log("OnErrorReceived: " + message);
            }

            gameObject.Destroy();
        }

        #endregion

        #region IVideoController

        public bool IsPlaying
        {
            get { return m_isPlaying; }
        }

        public void Play()
        {
            m_videoPlayer.Play();
            m_audioSource.Play();

            m_isPlaying = true;
        }

        public void Pause()
        {
            m_videoPlayer.Pause();
            m_audioSource.Pause();

            m_isPlaying = false;
        }

        public void Stop()
        {
            m_videoPlayer.Stop();
            m_audioSource.Stop();

            m_isPlaying = false;
        }

        #endregion
    }
}
