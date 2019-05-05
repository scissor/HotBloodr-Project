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
