using System;
using System.Collections.Generic;
using System.Linq;

namespace HotBloodr
{
    public class CsvParser
    {
        public static List<string> Split(string s)
        {
            var strings = s.Split(',');
            var newStrings = new List<string>();
            var newString = string.Empty;
            var isDoubleQuote = false;
            for (int i = 0; i < strings.Length; i++)
            {
                var str = strings[i];

                if (str.Contains('"'))
                {
                    if (isDoubleQuote)
                    {
                        newString += "," + str.Replace('"', new char());
                        newStrings.Add(newString);
                        isDoubleQuote = false;
                    }
                    else
                    {
                        newString = str.Replace('"', new char());
                        isDoubleQuote = true;
                    }
                }
                else
                {
                    if (!isDoubleQuote)
                    {
                        newStrings.Add(str);
                    }
                    else
                    {
                        newString += str;
                    }
                }
            }

            return newStrings;
        }
    }
}
