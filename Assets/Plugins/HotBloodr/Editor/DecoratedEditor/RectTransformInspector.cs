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
    [CustomEditor(typeof(RectTransform)), CanEditMultipleObjects]
    public class RectTransformInspector : TransformResetter
    {
        SerializedProperty m_position;
        SerializedProperty m_positionZ;
        SerializedProperty m_rotation;
        SerializedProperty m_scale;

        public RectTransformInspector() : base("RectTransformEditor")
        {
        }

        void OnEnable()
        {
            m_position = serializedObject.FindProperty("m_AnchoredPosition");
            m_positionZ = serializedObject.FindProperty("m_LocalPosition.z");
            m_rotation = serializedObject.FindProperty("m_LocalRotation");
            m_scale = serializedObject.FindProperty("m_LocalScale");

            LoadCustomValues();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.BeginHorizontal();
            GUI.backgroundColor = new Color(0.75f, 1, 0);

            if (GUILayout.Button("Position", EditorStyles.miniButtonLeft))
            {
                m_position.vector2Value = m_resetPosition;
                m_positionZ.floatValue = m_resetPosition.z;
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

            DrawCustomValues();
        }
    }
}

#endif
