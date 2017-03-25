using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace HotBloodr
{
    public static class RandomHelper
    {
        private static System.Random m_randomSeed = new System.Random();
        private static int m_seed = 0;

        public static int m_RandomSeed
        {
            set
            {
                m_seed = value;
                m_randomSeed = new System.Random(value);
            }
            get
            {
                return m_seed;
            }
        }

        public static void ResetSeed()
        {
            m_randomSeed = new System.Random(m_seed);
        }

        public static List<int> List(int count)
        {
            var numbers = new List<int>();

            for (int i = 0; i < count; ++i)
            {
                numbers.Add(i);
            }

            for (int i = 0; i < count; ++i)
            {
                int random = m_randomSeed.Next(0, count);
                int temp = numbers[i];
                numbers[i] = numbers[random];
                numbers[random] = temp;
            }

            return numbers;
        }

        public static List<int> List(int num1, int num2, int count)
        {
            List<int> numbers = new List<int>();

            bool isOrder = num1 <= num2;

            for (int i = 0; i < count; ++i)
            {
                numbers.Add(Number(num1, num2));
            }

            numbers.Sort();

            if (isOrder == false)
            {
                numbers.Reverse();
            }

            return numbers;
        }

        public static T Pick<T>(List<T> list)
        {
            int random = m_randomSeed.Next(0, list.Count);
            if (random >= list.Count)
            {
                return list.FirstOrDefault();
            }
            return list[random];
        }

        public static void Shuffle<T>(List<T> list)
        {
            int count = list.Count;

            for (int i = 0; i < count; ++i)
            {
                int random = m_randomSeed.Next(0, count);
                T temp = list[i];
                list[i] = list[random];
                list[random] = temp;
            }
        }

        public static string String(int size, bool lowerCase)
        {
            var randString = new StringBuilder(size);

            int start = lowerCase ? 97 : 65;

            for (int i = 0; i < size; i++)
            {
                randString.Append((char)((26 * m_randomSeed.NextDouble()) + start));
            }

            return randString.ToString();
        }

        public static int Next(int minimal, int maximal)
        {
            return Number(minimal, maximal);
        }

        public static int Number(int num1, int num2)
        {
            if (num1 <= num2)
            {
                return m_randomSeed.Next(num1, num2);
            }
            else
            {
                return m_randomSeed.Next(num2, num1);
            }
        }

        public static bool Bool()
        {
            return m_randomSeed.NextDouble() > 0.5;
        }

        public static float Float()
        {
            var buffer = new byte[sizeof(float)];
            m_randomSeed.NextBytes(buffer);
            return BitConverter.ToInt32(buffer, 0);
        }

        public static float Float(float minimal, float maximal)
        {
            var range = (maximal > minimal) ? maximal - minimal : minimal - maximal;
            if (Mathf.Approximately(range, 0))
            {
                return 0;
            }
            else if (Mathf.Approximately(range, 1))
            {
                return minimal;
            }

            float value = Math.Abs(Float());
            return (value % range) + minimal;
        }

        public static Vector3 ScreenPosition()
        {
            var x = Float(0, Screen.width);
            var y = Float(0, Screen.height);
            var position = Camera.main.ScreenToWorldPoint(new Vector3(x, y, Camera.main.farClipPlane / 2));
            return position;
        }

        public static Vector3 OutScreenPosition(float offsetW, float offsetH)
        {
            var isVertical = Bool();
            var width = Screen.width + offsetW;
            var height = Screen.height + offsetH;
            var x = 0f;
            var y = 0f;

            if (isVertical)
            {
                x = Float(-width, width);
            }
            else
            {
                x = Bool() ? -width : width;
            }

            if (isVertical)
            {
                y = Bool() ? -height : height;
            }
            else
            {
                y = Float(-height, height);
            }

            return new Vector3(x, y, 0);
        }
    }
}
