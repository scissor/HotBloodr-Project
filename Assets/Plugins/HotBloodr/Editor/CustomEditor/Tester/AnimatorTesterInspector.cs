#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace HotBloodr.Editor
{
    [CustomEditor(typeof(AnimatorTester)), CanEditMultipleObjects]
    public class AnimatorTesterInspector : UnityEditor.Editor
    {
        private const int BUTTON_WIDTH = 20;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            DrawStates();

            var tester = target as AnimatorTester;

            GUILayout.BeginHorizontal();
            GUI.backgroundColor = Color.green;
            {
                if (GUILayout.Button("<<"))
                {
                    tester.Previous();
                }

                GUILayout.FlexibleSpace();
                var labelStyle = new GUIStyle(GUI.skin.label);
                labelStyle.fontSize = 18;
                GUILayout.Label(tester.State, labelStyle);
                GUILayout.FlexibleSpace();

                if (GUILayout.Button(">>"))
                {
                    tester.Next();
                }
            }
            GUILayout.EndHorizontal();

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