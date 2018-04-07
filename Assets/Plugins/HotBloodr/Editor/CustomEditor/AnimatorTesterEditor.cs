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

#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace HotBloodr.Editor
{
    [CustomEditor(typeof(AnimatorTester)), CanEditMultipleObjects]
    public class AnimatorTesterEditor : UnityEditor.Editor
    {
        private const int BUTTON_WIDTH = 20;
        private const int STATE_FONT_SIZE = 18;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            DrawStates();

            var tester = target as AnimatorTester;
            var labelStyle = new GUIStyle(GUI.skin.label);
            labelStyle.fontSize = STATE_FONT_SIZE;

            // Next & Previous Button
            GUILayout.BeginHorizontal();
            GUI.backgroundColor = Color.green;
            {
                if (GUILayout.Button("<<"))
                {
                    tester.Previous();
                }

                GUILayout.FlexibleSpace();
                GUILayout.Label(tester.State, labelStyle);
                GUILayout.FlexibleSpace();

                if (GUILayout.Button(">>"))
                {
                    tester.Next();
                }
            }
            GUILayout.EndHorizontal();

            // AutoTest Toggle
            GUI.backgroundColor = Color.white;
            GUIHelper.HorizontalSplitter(1);
            GUILayout.Space(5);
            tester.IsAuto = GUILayout.Toggle(tester.IsAuto, "AutoTest");
            GUILayout.Space(5);

            // Reset Button
            GUI.backgroundColor = Color.yellow;
            {
                if (GUILayout.Button("Reset"))
                {
                    tester.Initialize();
                }
            }
            GUI.backgroundColor = Color.white;
        }

        private void DrawStates()
        {
            var tester = target as AnimatorTester;
            if (tester.States != null)
            {
                foreach (var state in tester.States)
                {
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label(state);
                        GUI.backgroundColor = Color.red;
                        if (GUILayout.Button("▶", GUILayout.Width(BUTTON_WIDTH)))
                        {
                            tester.Play(state);
                        }
                        GUI.backgroundColor = Color.white;
                        GUILayout.EndHorizontal();
                    }
                }
                GUILayout.Space(10);
            }
        }
    }
}

#endif