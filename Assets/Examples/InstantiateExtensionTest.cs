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
using UnityEngine;

namespace HotBloodr
{
    public class InstantiateExtensionTest : MonoBehaviour
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
}