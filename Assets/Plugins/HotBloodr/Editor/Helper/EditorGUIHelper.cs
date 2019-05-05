//
// Copyright (C) 2019 Scissor Lee
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
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace HotBloodr
{
    public static class EditorGUIHelper
    {
        public static int TitleIntField(string title, int number, params GUILayoutOption[] options)
        {
            GUIHelper.FixedLabel(title);
            return EditorGUILayout.IntField(number, options);
        }

        public static float TitleFloatField(string title, float number, params GUILayoutOption[] options)
        {
            GUIHelper.FixedLabel(title);
            return EditorGUILayout.FloatField(number, options);
        }

        public static string TitleTextField(string title, string text, params GUILayoutOption[] options)
        {
            GUIHelper.FixedLabel(title);
            return EditorGUILayout.TextField(text, options);
        }

        public static bool TitleToggle(string title, bool flag, params GUILayoutOption[] options)
        {
            GUIHelper.FixedLabel(title);
            return EditorGUILayout.Toggle(flag, options);
        }

        public static Enum EnumPopup(Enum type, int width, int height, int fontSize = 10)
        {
            var originFontSize = EditorStyles.popup.fontSize;
            var originHeight = EditorStyles.popup.fixedHeight;
            EditorStyles.popup.fontSize = fontSize;
            EditorStyles.popup.fixedHeight = height;

            var returnType = EditorGUILayout.EnumPopup(type, GUILayout.Width(width), GUILayout.Height(height));
            EditorStyles.popup.fontSize = originFontSize;
            EditorStyles.popup.fixedHeight = originHeight;

            return returnType;
        }

        public static bool DrawListWithAddRemoveButtons<T>(List<T> list, T item, Color color, Action<int> callback)
        {
            if (!list.Any())
            {
                GUIHelper.DrawAddRemoveButtons(list, item, -1, color);
            }

            for (int i = 0; i < list.Count; i++)
            {
                GUILayout.BeginHorizontal();
                {
                    if (GUIHelper.DrawAddRemoveButtons(list, item, i, color))
                    {
                        return true;
                    }

                    callback(i);
                }
                GUILayout.EndHorizontal();
            }

            return false;
        }
    }
}
