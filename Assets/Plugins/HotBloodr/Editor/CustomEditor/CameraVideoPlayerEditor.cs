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

using HotBloodr.Video;
using UnityEditor;
using UnityEngine;

namespace HotBloodr.Editor
{
    [CustomEditor(typeof(CameraVideoPlayer), true)]
    public class CameraVideoPlayerEditor : UnityEditor.Editor
    {
        private const int BUTTON_WIDTH = 40;

        private CameraVideoPlayer m_player;

        private void OnEnable()
        {
            m_player = target as CameraVideoPlayer;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (m_player == null)
            {
                Debug.Log("Player is null");
                return;
            }

            DrawTime();
            DrawProgressSlider();
            DrawControlButtons();

            Repaint();
        }

        private void DrawTime()
        {
            GUILayout.BeginHorizontal();
            {
                GUIHelper.FontLabel(m_player.NowTime + "/" + m_player.TotalTime, 18, Color.red);
            }
            GUILayout.EndHorizontal();
        }

        private void DrawProgressSlider()
        {
            GUILayout.BeginHorizontal();
            {
                GUILayout.HorizontalSlider(m_player.Progress, 0, 1);
            }
            GUILayout.EndHorizontal();
        }

        private void DrawControlButtons()
        {
            GUI.backgroundColor = Color.red;
            GUILayout.BeginHorizontal();
            {
                var playString = m_player.IsPlaying ? "▌▌" : "▶";
                if (GUIHelper.Button(playString, BUTTON_WIDTH, BUTTON_WIDTH))
                {
                    if (m_player.IsPlaying)
                    {
                        m_player.Pause();
                    }
                    else
                    {
                        m_player.Play();
                    }
                }
                if (GUIHelper.Button("▇", BUTTON_WIDTH, BUTTON_WIDTH))
                {
                    m_player.Stop();
                }
            }
            GUI.backgroundColor = Color.white;
            GUILayout.EndHorizontal();
        }
    }
}