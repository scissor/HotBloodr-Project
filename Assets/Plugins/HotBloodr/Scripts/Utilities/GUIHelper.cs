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

using UnityEngine;

namespace HotBloodr
{
    public class GUIHelper
    {
        public static string TextField(string title, string text, GUIStyle style)
        {
            GUILayout.Label(title, style);
            text = GUILayout.TextField(text);
            GUILayout.Space(10);
            return text;
        }

        public static float HorizontalSlider(string title, float value, float min, float max)
        {
            GUILayout.Label(title);
            value = GUILayout.HorizontalSlider(value, min, max);
            GUILayout.Space(10);
            return value;
        }

        public static bool Button(string title, float width, float height)
        {
            if (GUILayout.Button("Create", GUILayout.MaxWidth(width), GUILayout.MaxHeight(height)))
            {
                return true;
            }
            return false;
        }

        public static void HorizontalSplitter(int height)
        {
            GUILayout.Space(height);
            GUILayout.Box(string.Empty, new GUILayoutOption[]
            {
                GUILayout.ExpandWidth(true),
                GUILayout.Height(1)
            });
        }
        public static void HorizontalSplitter(string title, int height)
        {
            GUILayout.Space(height);
            GUILayout.Label(title);

            GUILayout.Box(string.Empty, new GUILayoutOption[]
            {
                GUILayout.ExpandWidth(true),
                GUILayout.Height(1)
            });
        }
    }
}
