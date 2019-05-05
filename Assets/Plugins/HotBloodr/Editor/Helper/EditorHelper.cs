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
using System.Reflection;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace HotBloodr.Editor
{
    public static class EditorHelper
    {
        public static string FolderField(string title, string text, GUIStyle style)
        {
            GUILayout.Label(title, style);
            if (GUILayout.Button("Load..."))
            {
                text = EditorUtility.OpenFolderPanel("Load...", string.Empty, string.Empty);
            }
            text = GUILayout.TextField(text);
            GUILayout.Space(10);
            return text;
        }

        public static string FileField(string title, string text, GUIStyle style)
        {
            GUILayout.Label(title, style);
            if (GUILayout.Button("Load..."))
            {
                text = EditorUtility.OpenFilePanel("Load...", string.Empty, ".cs");
            }
            text = GUILayout.TextField(text);
            GUILayout.Space(10);
            return text;
        }

        public static string[] GetSortingLayerNames()
        {
            var utility = typeof(InternalEditorUtility);
            var property = utility.GetProperty("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);
            return (string[])property.GetValue(null, new object[0]);
        }

        public static void CheckGUIChanged(Action doAction, Action changedAction)
        {
            EditorGUI.BeginChangeCheck();

            doAction();

            if (EditorGUI.EndChangeCheck())
            {
                changedAction();
            }
        }
    }
}
