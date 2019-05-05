//
// Copyright (C) 2019 Scissor Lee
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

using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace HotBloodr.Editor
{
    public class MaterialPropertyCleaner : HotBloodrWindow
    {
        private class Result
        {
            public string Path;
            public string Name;
            public List<string> Properties = new List<string>();
            public bool IsCollapsed = true;

            public Result(string path)
            {
                Path = path;
            }
        }

        private Dictionary<string, Result> m_results = new Dictionary<string, Result>();
        private string m_folderPath;
        private string m_searchPath;

        #region AkatsukiWindow

        protected override string Title => "Material Cleaner";

        protected override void OnWindowGUI()
        {
            DrawFields();

            GUIHelper.HorizontalSplitter(1, 5);

            DrawResult();
        }

        #endregion

        #region Draw

        private void DrawFields()
        {
            GUILayout.BeginHorizontal();
            {
                GUIHelper.FixedLabel(" Select Folder", 18, Color.yellow);

                if (GUIHelper.ColorButton("..", Color.cyan, 40, 20))
                {
                    m_folderPath = EditorUtility.OpenFolderPanel("Select Folder", Application.dataPath, "");
                    m_folderPath = "Assets" + m_folderPath.Substring(Application.dataPath.Length);
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            {
                m_folderPath = GUILayout.TextField(m_folderPath, GUILayout.Width(500));

                if (GUIHelper.ColorButton("Clean", Color.cyan))
                {
                    m_results.Clear();
                    CleanMaterials(m_folderPath);
                }
            }
            GUILayout.EndHorizontal();
        }

        private void DrawResult()
        {
            foreach (var result in m_results.Values)
            {
                GUILayout.BeginHorizontal();
                {
                    var collapsed = result.IsCollapsed ? "+" : "-";
                    if (GUIHelper.ColorButton(collapsed, Color.red, 20, 20))
                    {
                        result.IsCollapsed = !result.IsCollapsed;
                    }

                    GUILayout.Label(result.Name, GUILayout.Width(200));
                    GUI.backgroundColor = Color.cyan;
                    {
                        if (GUIHelper.Button("Select", 100, 20))
                        {
                            Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(result.Path);
                        }
                    }
                    GUI.backgroundColor = Color.white;
                }
                GUILayout.EndHorizontal();

                if (result.IsCollapsed)
                {
                    continue;
                }

                foreach (var property in result.Properties)
                {
                    GUILayout.Label(property);
                }

                GUIHelper.HorizontalSplitter(1, 3);
            }
        }

        #endregion

        private void CleanMaterials(string rootPath)
        {
            var files = GetFiles(rootPath, "mat");
            var index = 0;
            var total = files.Count;
            foreach (var filePath in files)
            {
                index++;
                var message = string.Format("Progressing: {0}/{1}", index, total);
                EditorUtility.DisplayProgressBar(message, filePath, index / (float)total);

                var mat = AssetDatabase.LoadMainAssetAtPath(filePath) as Material;
                if (mat == null)
                {
                    continue;
                }

                m_searchPath = filePath;

                var so = new SerializedObject(mat);
                var properties = so.FindProperty("m_SavedProperties");
                var texEnvs = properties.FindPropertyRelative("m_TexEnvs");

                CleanMaterialSerializedProperty(texEnvs, mat);
                so.ApplyModifiedProperties();

                if (m_results.ContainsKey(m_searchPath))
                {
                    EditorUtility.SetDirty(mat);
                }
            }

            EditorUtility.ClearProgressBar();
        }

        private void CleanMaterialSerializedProperty(SerializedProperty property, Material mat)
        {
            for (int i = property.arraySize - 1; i >= 0; i--)
            {
                var target = property.GetArrayElementAtIndex(i);
                var propertyName = target.displayName;

                if (mat.HasProperty(propertyName))
                {
                    continue;
                }

                property.DeleteArrayElementAtIndex(i);

                Result result;
                if (!m_results.ContainsKey(m_searchPath))
                {
                    result = new Result(m_searchPath);
                    result.Name = mat.name;
                    m_results.Add(m_searchPath, result);
                }
                else
                {
                    result = m_results[m_searchPath];
                }

                result.Properties.Add(propertyName);
            }
        }

        private List<string> GetFiles(string folder, string extName)
        {
            return Directory.GetFiles(folder, "*", SearchOption.AllDirectories).Where(f => f.EndsWith(".mat")).ToList();
        }
    }
}
