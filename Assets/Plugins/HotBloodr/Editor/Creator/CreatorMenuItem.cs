#if UNITY_EDITOR

using UnityEditor;

namespace HotBloodr.Editor
{
    public class CreatorMenuItem
    {
        [MenuItem("HotBloodr/Create/SingletonAsset", false, 0)]
        private static void OnClickAsset()
        {
            SingletonAssetCreator.OnClick();
        }

        [MenuItem("HotBloodr/Create/Enum", false, 0)]
        public static void OnClickEnum()
        {
            EnumCreator.OnClick();
        }
    }
}

#endif
