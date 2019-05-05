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

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace HotBloodr.Editor
{
    public static class FileEnumCreator
    {
        private const string m_templateName = "FileEnum.txt";
        private static readonly string m_root = Application.dataPath + "/Scripts/Enum/";

        public static void OpenWindow()
        {
            string path = EditorUtility.OpenFolderPanel("Load...", "", "");
            if (path.Length != 0)
            {
                CreateEnumFile(path);
            }
        }

        private static List<string> GetFiles(string folder)
        {
            return Directory.GetFiles(folder).Where(f => !f.EndsWith(".meta")).ToList();
        }

        private static void CreateEnumFile(string folder)
        {
            var enumName = folder.Split('/').Last();
            var className = enumName + "Class";

            var enums = string.Empty;
            var files = GetFiles(folder);
            foreach (var file in files)
            {
                var fileName = file.Split('/').Last();
                fileName = fileName.Split('.').First();
                enums += fileName + ",\n\t";
            }

            var replacements = new Dictionary<string, string>()
            {
                { "$EnumName", enumName },
                { "$Enums", enums },
            };
            var fileString = ScriptWizard.Create(m_templateName, replacements);
            if (!Directory.Exists(m_root))
            {
                Directory.CreateDirectory(m_root);
            }

            File.WriteAllText(m_root + "/" + className + ".cs", fileString, Encoding.UTF8);
            AssetDatabase.Refresh();
        }
    }
}