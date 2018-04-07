#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System;
using UnityEditorInternal;
using System.Reflection;

namespace Akatsuki.Editor
{
    public class EditorHelper
    {
        public static string FolderField(string title, string text, GUIStyle style)
        {
            GUILayout.Label(title, style);
            if (GUILayout.Button("Load..."))
            {
                text = EditorUtility.OpenFolderPanel("Load...", "", "");
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
                text = EditorUtility.OpenFilePanel("Load...", "", ".cs");
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
#endif
