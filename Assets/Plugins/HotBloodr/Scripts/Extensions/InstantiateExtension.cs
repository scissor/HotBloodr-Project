using UnityEngine;

public static partial class ExtensionMethods
{
    public static GameObject AddChild(this GameObject parent, GameObject prefab)
    {
        var obj = Object.Instantiate(prefab) as GameObject;
        var t = obj.transform;

        if (parent != null)
        {
            t.SetParent(parent.transform);
            obj.layer = parent.layer;
        }

        t.localPosition = prefab.transform.localPosition;
        t.localRotation = prefab.transform.localRotation;
        t.localScale = prefab.transform.localScale;
        return obj;
    }

    public static GameObject AddChild(this GameObject prefab)
    {
        return AddChild(null, prefab);
    }

    public static T AddChild<T>(this GameObject prefab)
    {
        var obj = AddChild(null, prefab);
        var returnClass = obj.GetComponent<T>();

        Debug.Assert(returnClass != null, "AddChild Error: " + typeof(T) + " is null!");

        return returnClass;
    }

    public static T AddChild<T>(this GameObject parent, GameObject prefab)
    {
        var obj = AddChild(parent, prefab);
        var returnClass = obj.GetComponent<T>();

        Debug.Assert(returnClass != null, "AddChild Error: " + typeof(T) + " is null!");

        return returnClass;
    }

    public static void Destroy(this GameObject obj)
    {
        Object.Destroy(obj);
    }
}
