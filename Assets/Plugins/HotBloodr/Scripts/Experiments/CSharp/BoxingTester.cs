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

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HotBloodr.Experiments
{
    public struct PlayerStruct
    {
        public string Name;

        public PlayerStruct(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class PlayerClass
    {
        public string Name;

        public PlayerClass(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class BoxingTester : MonoBehaviour
    {
        private List<PlayerStruct> m_structs = new List<PlayerStruct>();
        private List<PlayerClass> m_classes = new List<PlayerClass>();

        void Awake()
        {
            Debug.Log("Is PlayerStruct a value type: " + typeof(PlayerStruct).IsValueType);
            Debug.Log("Is PlayerClass a value type: " + typeof(PlayerClass).IsValueType);

            m_structs.Add(new PlayerStruct("Player"));
            m_classes.Add(new PlayerClass("Player"));

            // This is a copy created by .Net boxing mechanism
            var structPlayer = m_structs.First();
            structPlayer.Name = "New Player";

            var classPlayer = m_classes.First();
            classPlayer.Name = "New Player";

            // No change
            Debug.Log(m_structs.First().Name);
            Debug.Log(m_classes.First().Name);
        }
    }
}