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
