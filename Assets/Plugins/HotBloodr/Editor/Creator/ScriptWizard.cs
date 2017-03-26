using System.Collections.Generic;
using System.IO;

namespace HotBloodr.Editor
{
    public class ScriptWizard
    {
        private static readonly string m_templetePath = "Assets/Plugins/HotBloodr/Editor/Creator/Templates/";

        public static string Create(string name, Dictionary<string, string> replacements)
        {
            var path = m_templetePath + name;
            var script = File.ReadAllText(path);
            foreach (var pair in replacements)
            {
                script = script.Replace(pair.Key, pair.Value);
            }

            return script;
        }
    }
}
