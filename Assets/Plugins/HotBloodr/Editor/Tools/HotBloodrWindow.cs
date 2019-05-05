using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HotBloodr.Editor
{
    public abstract class HotBloodrWindow : EditorWindow
    {
        protected const int WINDOW_WIDTH = 1000;
        protected const int ENUM_WIDTH = 100;
        protected const int BIG_BUTTON_WIDTH = 30;
        protected const int BUTTON_WIDTH = 20;
        protected readonly Color BUTTON_COLOR = Color.blue;
        protected readonly Color GRID_COLOR = Color.green;

        protected Vector2 m_scrollValue = Vector2.zero;
        protected int m_selectedIndex;
        protected GUIStyle m_toolbarStyle;
        protected Dictionary<Color, Texture2D> m_colorTextures = new Dictionary<Color, Texture2D>();

        private bool m_isInitialize;
        private Texture2D m_logo;

        #region Abstract methods

        protected abstract string Title
        {
            get;
        }

        protected abstract void OnWindowGUI();

        protected virtual string IconPath
        {
            get { return "Assets/Plugins/HotBloodr/Editor/Images/Icon.png"; }
        }

        protected virtual void OnInitialize() { }
        protected virtual void OnWindowToolbar() { }

        #endregion

        #region EditorWindow

        protected virtual void OnEnable()
        {
            m_isInitialize = false;
            Initialize();
        }

        private void OnGUI()
        {
            DrawToolBar();

            GUIHelper.HorizontalSplitter(1);

            m_scrollValue = EditorGUILayout.BeginScrollView(m_scrollValue);

            OnWindowGUI();

            EditorGUILayout.EndScrollView();
        }

        #endregion

        protected void DrawHorizontalSplitter()
        {
            GUILayout.Space(5);
            GUIHelper.HorizontalSplitter(1);
            GUILayout.Space(5);
        }

        private void Initialize()
        {
            if (m_isInitialize)
            {
                return;
            }

            m_logo = AssetDatabase.LoadAssetAtPath<Texture2D>(IconPath);

            OnInitialize();

            m_isInitialize = true;
        }

        private void DrawToolBar()
        {
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label(m_logo);
                GUIHelper.FontLabel(Title, 22, Color.cyan);
                GUILayout.FlexibleSpace();
            }
            GUILayout.EndHorizontal();

            OnWindowToolbar();
        }
    }
}
