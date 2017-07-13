using System;
using System.Collections.Generic;
using System.Linq;

public static partial class ExtensionMethods
{
    public static T CircularNext<T>(this IList<T> list, T now)
    {
        var index = list.IndexOf(now);
        ++index;

        if (index >= list.Count)
        {
            return list.First();
        }
        return list[index];
    }

    public static T CircularPrev<T>(this IList<T> list, T now)
    {
        var index = list.IndexOf(now);
        --index;

        if (index < 0)
        {
            return list.Last();
        }
        return list[index];
    }
}
