using System;
using System.Collections.Generic;
using System.Linq;

namespace HotBloodr
{
    public class EnumHelper
    {
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        public static List<string> GetStrings(Type type)
        {
            return Enum.GetNames(type).ToList();
        }

        public static T CreateRandom<T>()
        {
            var values = Enum.GetValues(typeof(T)).Cast<T>().ToList();
            int random = UnityEngine.Random.Range(0, values.Count);
            return values[random];
        }

        public static T First<T>()
        {
            return GetValues<T>().FirstOrDefault();
        }

        public static T Last<T>()
        {
            return GetValues<T>().LastOrDefault();
        }

        public static int Count<T>()
        {
            return GetValues<T>().Count();
        }

        public static T Next<T>(T now)
        {
            var values = GetValues<T>().ToList();
            int count = values.Count;
            for (int i = 0; i < count; ++i)
            {
                if ((int)(object)now != (int)(object)values[i])
                {
                    continue;
                }

                int next = i + 1;

                return next >= count ? now : values[next];
            }

            return now;
        }

        public static T CircularPrev<T>(T now)
        {
            List<T> values = GetValues<T>().ToList();

            int count = values.Count;
            for (int i = 0; i < count; ++i)
            {
                if ((int)(object)now != (int)(object)values[i])
                {
                    continue;
                }

                int index = i - 1;
                return index < 0 ? values.Last() : values[index];
            }

            return now;
        }

        public static T CircularNext<T>(T now)
        {
            List<T> values = GetValues<T>().ToList();

            int count = values.Count;
            for (int i = 0; i < count; ++i)
            {
                if ((int)(object)now != (int)(object)values[i])
                {
                    continue;
                }

                int index = i + 1;
                return index >= count ? values.First() : values[index];
            }

            return now;
        }

        public static bool Parse<T>(string value, ref T enumValue)
        {
            bool isSucceed = Enum.IsDefined(typeof(T), value);
            if (isSucceed)
            {
                T type = (T)Enum.Parse(typeof(T), value);
                enumValue = type;
            }

            return isSucceed;
        }

        public static T Parse<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }
    }
}
