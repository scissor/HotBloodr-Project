using System.Collections.Generic;
using HotBloodr;
using UnityEngine;

public class ExtensionsTest : MonoBehaviour
{
    [SerializeField]
    private GameObject m_circle = null;

    private List<GameObject> m_circles = new List<GameObject>();

    void OnGUI()
    {
        if (GUILayout.Button("Addchild on this GameObject"))
        {
            var obj = gameObject.AddChild(m_circle);
            obj.name = "Circle";
            obj.transform.localScale = Vector3.one * 2;
            AddCircle(obj);
        }

        if (GUILayout.Button("Addchild with no parent"))
        {
            var obj = m_circle.AddChild();
            obj.name = "Circle - No parent";
            obj.transform.localScale = Vector3.one * 5;
            AddCircle(obj);
        }

        if (GUILayout.Button("Clear"))
        {
            ClearCircles();
        }
    }

    private void AddCircle(GameObject obj)
    {
        obj.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
        obj.transform.localPosition = RandomHelper.ScreenPosition();
        m_circles.Add(obj);
    }

    private void ClearCircles()
    {
        foreach (var circle in m_circles)
        {
            circle.Destroy();
        }
    }
}
