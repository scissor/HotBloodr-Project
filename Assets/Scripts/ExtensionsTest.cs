using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtensionsTest : MonoBehaviour
{
    [SerializeField]
    private GameObject m_prefab = null;

    void OnGUI()
    {
        if (GUILayout.Button("Addchild on this GameObject"))
        {
            var obj = gameObject.AddChild(m_prefab);
            obj.GetComponent<SpriteRenderer>().color = ;
        }

        if (GUILayout.Button("Addchild with no parent"))
        {
            m_prefab.AddChild();
        }
    }
}
