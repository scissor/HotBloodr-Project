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
