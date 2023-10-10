using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MtgOffline.EnderScript
{
    public static class ESUtils
    {
        public static T[] ToSpecificArray<T>(this object[] array)
        {
            T[] t = new T[array.Length];
            for (int i = 0; i < array.Length; i++)
                t[i] = (T)array[i];

            return t;
        }
        
        public static ESBuilder CreateBuilderFromPath(string path)
        {
            return new ESBuilder(File.ReadAllText(path));
        }

        internal static void WriteToPath(string path,string es)
        {
            File.WriteAllText(path, es);
        }
        public static string GetFormattedString(string raw)
        {
            string s = "";
            for (int i = 0; i < raw.Length; i++)
            {
                if (raw[i] == '"')
                    s += "\\\"";
                else if (raw[i] == '\\')
                    s += "\\\\";
                else
                    s += raw[i];
            }
            return s;
        }
    }
}
