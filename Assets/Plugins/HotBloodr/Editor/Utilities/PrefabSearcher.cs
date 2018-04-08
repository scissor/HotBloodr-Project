//
// Copyright (C) 2018 Scissor Lee
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
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace HotBloodr.Editor
{
    public class PrefabSearcher : EditorWindow
    {
        private const string PREFAB_EXT_NAME = "prefab";

        private string m_searchType;
        private string m_searchText;
        private Dictionary<string, string> m_searchedFiles = new Dictionary<string, string>();

        [MenuItem("HotBloodr/Utilities/PrefabSearcher")]
        public static void ShowWindow()
        {
            var window = EditorWindow.GetWindow(typeof(PrefabSearcher));
            window.titleContent = new GUIContent("PrefabSearcher");
        }

        public void OnGUI()
        {

            m_searchType = EditorGUILayout.TextField(m_searchType, GUILayout.Height(30));
            m_searchText = EditorGUILayout.TextField(m_searchText, GUILayout.Height(30));
            GUI.backgroundColor = Color.red;
            {
                if (GUILayout.Button("Search By Text"))
                {
                    m_searchedFiles.Clear();
                    SearchByText(PREFAB_EXT_NAME);
                }

                if (GUILayout.Button("Search By Component"))
                {
                    m_searchedFiles.Clear();
                    SearchByComponent(PREFAB_EXT_NAME);
                }
            }
            GUI.backgroundColor = Color.white;

            GUIHelper.HorizontalSplitter(1);
            foreach (var pair in m_searchedFiles)
            {
                var path = pair.Key;
                var fileName = pair.Value;

                GUILayout.BeginHorizontal();
                {
                    GUILayout.Label(fileName, GUILayout.Width(200));
                    GUI.backgroundColor = Color.cyan;
                    {
                        if (GUIHelper.Button("Select", 100, 20))
                        {
                            var obj = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
                            Selection.activeObject = obj;
                        }
                    }
                    GUI.backgroundColor = Color.white;
                }
                GUILayout.EndHorizontal();
            }
        }

        private void SearchByComponent(string extName)
        {
            if (Selection.activeObject == null)
            {
                return;
            }

            var type = ReflectionHelper.GetType(m_searchText);
            var directoryPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            var filePaths = GetFiles(directoryPath, extName);

            foreach (var path in filePaths)
            {
                var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                if (prefab.GetComponentInChildren(type) != null)
                {
                    var fileName = path.Split('/').Last();
                    fileName = fileName.Split('.').First();

                    m_searchedFiles.Add(path, fileName);
                }
            }
        }

        private void SearchByText(string extName)
        {
            if (Selection.activeObject == null)
            {
                return;
            }

            var directoryPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            var filePaths = GetFiles(directoryPath, extName);

            foreach (var path in filePaths)
            {
                var reader = new StreamReader(path);
                var text = reader.ReadToEnd();
                if (text.Contains(m_searchText))
                {
                    var fileName = path.Split('/').Last();
                    fileName = fileName.Split('.').First();

                    m_searchedFiles.Add(path, fileName);
                }
            }
        }

        private static List<string> GetFiles(string folder, string extName)
        {
            return Directory.GetFiles(folder, "*", SearchOption.AllDirectories).Where(f => !f.EndsWith(".meta") && f.Contains(extName)).ToList();
        }
    }
}

#endif
