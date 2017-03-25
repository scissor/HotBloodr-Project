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
