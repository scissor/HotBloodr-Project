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
    public abstract class TransformResetter : DecoratorEditor
    {
        private const string RESET_POSITION = "RESET_POSITION";
        private const string RESET_ROTATION = "RESET_ROTATION";
        private const string RESET_SCALE = "RESET_SCALE";

        protected bool m_unFold = false;

        protected static Vector3 m_resetPosition = Vector3.zero;
        protected static Vector3 m_resetRotation = Vector3.zero;
        protected static Vector3 m_resetScale = Vector3.one;

        public TransformResetter(string name) : base(name)
        {
        }

        protected void LoadCustomValues()
        {
            if (EditorPrefs.HasKey(RESET_POSITION))
            {
                m_resetPosition = JsonUtility.FromJson<Vector3>(EditorPrefs.GetString(RESET_POSITION));
            }

            if (EditorPrefs.HasKey(RESET_ROTATION))
            {
                m_resetRotation = JsonUtility.FromJson<Vector3>(EditorPrefs.GetString(RESET_ROTATION));
            }

            if (EditorPrefs.HasKey(RESET_SCALE))
            {
                m_resetScale = JsonUtility.FromJson<Vector3>(EditorPrefs.GetString(RESET_SCALE));
            }
        }

        protected void DrawCustomValues()
        {
            string originLabel;
            if (m_resetPosition != Vector3.zero || m_resetRotation != Vector3.zero || m_resetScale != Vector3.one)
            {
                originLabel = "Set Origin [Custom]";
            }
            else
            {
                originLabel = "Set Origin [Default]";
            }

            m_unFold = EditorGUILayout.Foldout(m_unFold, originLabel);
            if (m_unFold)
            {
                EditorGUI.BeginChangeCheck();
                if (GUILayout.Button("Clear Custom Origin", EditorStyles.miniButton))
                {
                    m_resetPosition = Vector3.zero;
                    m_resetRotation = Vector3.zero;
                    m_resetScale = Vector3.one;
                    GUI.FocusControl(null);
                }

                GUI.backgroundColor = Color.white;

                m_resetPosition = EditorGUILayout.Vector3Field("Position", m_resetPosition);
                m_resetRotation = EditorGUILayout.Vector3Field("Rotation", m_resetRotation);
                m_resetScale = EditorGUILayout.Vector3Field("Scale", m_resetScale);

                if (EditorGUI.EndChangeCheck())
                {
                    EditorPrefs.SetString(RESET_POSITION, EditorJsonUtility.ToJson(m_resetPosition));
                    EditorPrefs.SetString(RESET_ROTATION, EditorJsonUtility.ToJson(m_resetRotation));
                    EditorPrefs.SetString(RESET_SCALE, EditorJsonUtility.ToJson(m_resetScale));
                }
            }
        }
    }
}

#endif