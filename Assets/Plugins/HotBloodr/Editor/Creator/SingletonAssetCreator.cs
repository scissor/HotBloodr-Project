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

using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace HotBloodr.Editor
{
    public class SingletonAssetCreator : EditorWindow
    {
        private string m_templateName = "SingletonAsset.txt";
        private string m_scriptPath = "Scripts/Settings/";
        private string m_assetPath = "Resources/Settings/";
        private string m_name = "GameSettings";

        private Object m_selection = null;
        private bool m_isNeedCreate = false;
        private bool m_isNeedFocus = false;

        public static void OnClick()
        {
            var window = EditorWindow.GetWindow(typeof(SingletonAssetCreator));
            window.titleContent = new GUIContent("AssetCreator");
            window.Show();
        }

        void Update()
        {
            if (!EditorApplication.isCompiling)
            {
                if (m_isNeedCreate)
                {
                    CreateAsset();
                    m_isNeedCreate = false;
                }

                if (m_isNeedFocus)
                {
                    EditorUtility.FocusProjectWindow();
                    Selection.activeObject = m_selection;
                    m_isNeedFocus = false;
                }
            }
        }

        void OnGUI()
        {
            GUILayout.Space(10);
            EditorGUILayout.BeginVertical(GUI.skin.box);

            var style = new GUIStyle();
            style.fontSize = 18;

            m_scriptPath = CreateField("Script Path", m_scriptPath, style);
            m_assetPath = CreateField("Asset Path", m_assetPath, style);
            m_name = CreateField("Name", m_name, style);

            if (GUILayout.Button(
                    "Create Asset",
                    GUILayout.MaxWidth(200),
                    GUILayout.MaxHeight(30)
                )
            )
            {
                CreateScriptableObject();
            }

            EditorGUILayout.EndVertical();
        }

        private string CreateField(string fieldName, string text, GUIStyle style)
        {
            GUILayout.Label(fieldName, style);
            text = GUILayout.TextField(text);
            GUILayout.Space(10);
            return text;
        }

        private void CreateAsset()
        {
            var directory = Path.Combine("Assets/", m_assetPath);
            var file = directory + m_name + ".asset";

            if (File.Exists(file))
            {
                Debug.Log("Asset already exists!");
                return;
            }

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var instance = ScriptableObject.CreateInstance(m_name);
            AssetDatabase.CreateAsset(instance, file);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            m_selection = instance;
            m_isNeedFocus = true;

            Debug.Log("CreateAsset: " + m_name);
        }

        private void CreateScriptableObject()
        {
            var directory = Path.Combine(Application.dataPath, m_scriptPath);
            var file = directory + m_name + ".cs";

            if (File.Exists(file))
            {
                Debug.Log("ScriptObject already exists!");
                CreateAsset();
                return;
            }

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var assetPath = m_assetPath.Replace("Resources/", string.Empty) + m_name;
            var replacements = new Dictionary<string, string>()
            {
                { "$ClassName", m_name },
                { "$AssetPath", assetPath },
            };
            var fileString = ScriptWizard.Create(m_templateName, replacements);
            File.WriteAllText(file, fileString, Encoding.UTF8);

            AssetDatabase.Refresh();
            m_isNeedCreate = true;

            Debug.Log("Create ScriptObject: " + m_name);
            Debug.Log("Wait for compiling...");
        }
    }
}

#endif
