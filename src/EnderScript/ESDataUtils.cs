using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline.EnderScript
{
    public class ESDataUtils
    {
        public static string VarToES(string data)
        {
            return $"\"{data}\"";
        }
        public static string VarToES(int data)
        {
            return data.ToString();
        }
    }
}
