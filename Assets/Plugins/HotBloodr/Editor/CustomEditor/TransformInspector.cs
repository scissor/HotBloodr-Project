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

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HotBloodr.Editor
{
    [CustomEditor(typeof(Transform)), CanEditMultipleObjects]
    public class TransformInspector : DecoratorEditor
    {
        private bool m_unFold = false;

        private static Vector3 m_resetPosition = Vector3.zero;
        private static Vector3 m_resetRotation = Vector3.zero;
        private static Vector3 m_resetScale = Vector3.one;

        private SerializedProperty m_position;
        private SerializedProperty m_rotation;
        private SerializedProperty m_scale;

        public TransformInspector() : base("TransformInspector")
        {
        }

        void OnEnable()
        {
            m_position = serializedObject.FindProperty("m_LocalPosition");
            m_rotation = serializedObject.FindProperty("m_LocalRotation");
            m_scale = serializedObject.FindProperty("m_LocalScale");

            if (EditorPrefs.HasKey("CustomOriginResetPosition"))
                m_resetPosition = StringToVector3(EditorPrefs.GetString("CustomOriginResetPosition"));
            if (EditorPrefs.HasKey("CustomOriginResetRotation"))
                m_resetRotation = StringToVector3(EditorPrefs.GetString("CustomOriginResetRotation"));
            if (EditorPrefs.HasKey("CustomOriginm_ResetScale"))
                m_resetScale = StringToVector3(EditorPrefs.GetString("CustomOriginm_ResetScale"));
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            GUI.backgroundColor = new Color(0.75f, 1, 0);

            if (GUILayout.Button("Position", EditorStyles.miniButtonLeft))
            {
                m_position.vector3Value = m_resetPosition;
                serializedObject.ApplyModifiedProperties();
                GUI.FocusControl(null);
            }
            if (GUILayout.Button("Rotation", EditorStyles.miniButtonMid))
            {
                m_rotation.quaternionValue = Quaternion.Euler(m_resetRotation);
                serializedObject.ApplyModifiedProperties();
                GUI.FocusControl(null);
            }
            if (GUILayout.Button("Scale", EditorStyles.miniButtonRight))
            {
                m_scale.vector3Value = m_resetScale;
                serializedObject.ApplyModifiedProperties();
                GUI.FocusControl(null);
            }

            EditorGUILayout.EndHorizontal();

            string originLabel;
            if (m_resetPosition != Vector3.zero || m_resetRotation != Vector3.zero || m_resetScale != Vector3.one)
                originLabel = "Set Origin [Custom]";
            else
                originLabel = "Set Origin [Default]";

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
                    EditorPrefs.SetString("CustomOriginResetPosition", m_resetPosition.ToString());
                    EditorPrefs.SetString("CustomOriginResetRotation", m_resetRotation.ToString());
                    EditorPrefs.SetString("CustomOriginResetScale", m_resetScale.ToString());
                }
            }
        }

        Vector3 StringToVector3(string sVector)
        {
            if (sVector.StartsWith("(") && sVector.EndsWith(")"))
            {
                sVector = sVector.Substring(1, sVector.Length - 2);
            }

            string[] sArray = sVector.Split(',');

            Vector3 result = new Vector3(
                float.Parse(sArray[0]),
                float.Parse(sArray[1]),
                float.Parse(sArray[2]));

            return result;
        }
    }
}

#endif
