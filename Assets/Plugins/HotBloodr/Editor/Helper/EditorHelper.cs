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
