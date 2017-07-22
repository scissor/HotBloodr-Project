#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace HotBloodr.Editor
{
    [CustomEditor(typeof(AnimatorTester)), CanEditMultipleObjects]
    public class AnimatorTesterInspector : UnityEditor.Editor
    {
        private const int SMALL_BUTTON_WIDTH = 20;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var tester = target as AnimatorTester;

            GUILayout.BeginHorizontal();
            {
                GUI.backgroundColor = Color.green;
                if (GUILayout.Button("<", GUILayout.Width(SMALL_BUTTON_WIDTH), GUILayout.Height(SMALL_BUTTON_WIDTH)))
                {
                    tester.Previous();
                }

                GUI.backgroundColor = Color.red;
                if (GUILayout.Button("Play", GUILayout.Height(SMALL_BUTTON_WIDTH)))
                {
                    tester.Play();
                }

                GUI.backgroundColor = Color.green;
                if (GUILayout.Button(">", GUILayout.Width(SMALL_BUTTON_WIDTH), GUILayout.Height(SMALL_BUTTON_WIDTH)))
                {
                    tester.Next();
                }
            }
            GUILayout.EndHorizontal();
            GUI.backgroundColor = Color.white;
        }
    }
}

#endif