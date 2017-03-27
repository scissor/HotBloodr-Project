using System;

namespace HotBloodr
{
    public class ReflectionHelper
    {
        public static Type GetType(string name)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.Name == name)
                    {
                        return type;
                    }
                }
            }

            return null;
        }
    }
}
