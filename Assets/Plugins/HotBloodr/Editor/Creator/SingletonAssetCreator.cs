#if UNITY_EDITOR

using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace HotBloodr.Editor
{
    public class SingletonAssetCreator : EditorWindow
    {
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

            var loadPath = m_assetPath.Replace("Resources/", string.Empty);
            loadPath = Path.Combine(loadPath, m_name);

            var fileString = ScriptableObjectTemplate(loadPath);
            File.WriteAllText(file, fileString, Encoding.UTF8);

            AssetDatabase.Refresh();
            m_isNeedCreate = true;

            Debug.Log("Create ScriptObject: " + m_name);
            Debug.Log("Wait for compiling...");
        }

        private string ScriptableObjectTemplate(string path)
        {
            return string.Format(@"using UnityEngine;

public class {0} : ScriptableObject
{{
    public static {0} Instance
    {{
        get
        {{
            if (instance == null)
            {{
                instance = ({0})Resources.Load<{0}>(""{1}"");
            }}

            return instance;
        }}
    }}

    protected static {0} instance;
}}", m_name, path);
        }
    }
}

#endif
