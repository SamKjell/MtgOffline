using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline.EnderScript
{
    public class ESObject
    {
        public string data;
        public string parameterName;
        public string lineEnding;

        public ESObject(string parameterName, string data, string lineEnding = "\n")
        {
            this.parameterName = parameterName;
            this.data = data;
            this.lineEnding = lineEnding;
        }

        public virtual string GetES()
        {
            return $"{parameterName}|{data}";
        }


        public string GetString()
        {
            if (data.StartsWith("\"") && data.EndsWith("\""))
            {
                string s = data;
                s.Trim();
                s = s.Remove(s.Length - 1);
                s = s.Remove(0, 1);
                return s;
            }
            else
                throw new ESParseException("Failed to parse value as a string.");
        }
        public int GetInt()
        {
            string s = data;
            s.Trim();
            if (int.TryParse(s, out int value))
                return value;
            else
                throw new ESParseException("Failed to parse value as an int.");
        }
        public float GetFloat()
        {
            string s = data;
            s.Trim();
            if (float.TryParse(s, out float value))
                return value;
            else
                throw new ESParseException("Failed to parse value as a float.");
        }
        public bool GetBool()
        {
            string s = data;
            s.Trim();
            if (bool.TryParse(s, out bool value))
                return value;
            else
                throw new ESParseException("Failed to parse value as boolean.");
        }

        public object[] GetArray()
        {
            if (data.StartsWith("[") && data.EndsWith("]"))
            {
                string s = data;

                //Parse ES by each data type
                return GetArray(s);
            }
            else
                throw new ESParseException("Failed to parse value as an array.");
        }

        public T[] GetArray<T>()
        {
            object[] array = GetArray();
            T[] newArray = new T[array.Length];

            try
            {
                for (int i = 0; i < array.Length; i++)
                    newArray[i] = (T)array[i];
            }
            catch (InvalidCastException)
            {
                throw new ESParseException($"Not all of the elements in this list can be cast to the type: {typeof(T).ToString()}");
            }

            return newArray;
        }

        private object[] GetArray(string s)
        {
            s.Trim();
            s = s.Remove(s.Length - 1);
            s = s.Remove(0, 1);
            string[] objects = ESBuilder.SmartSplit(s).ToArray();
            object[] esArrayData = new object[objects.Length];
            for (int i = 0; i < objects.Length; i++)
            {
                string str = objects[i];
                str.Trim();

                if (str.StartsWith("\"") && str.EndsWith("\""))
                    esArrayData[i] = str.Remove(str.Length-1,1).Remove(0,1);
                else if (int.TryParse(str, out int v1))
                    esArrayData[i] = v1;
                else if (float.TryParse(str, out float v2))
                    esArrayData[i] = v2;
                else if (bool.TryParse(str, out bool v3))
                    esArrayData[i] = v3;
                else if (str.StartsWith("[") && str.EndsWith("]"))
                    esArrayData[i] = GetArray(str);
                else
                    esArrayData[i] = (object)str;
            }
            return esArrayData;
        }
    }

    public class ESComment : ESObject
    {
        public ESComment(string comment) : base(null, comment, "\n")
        {

        }
        public override string GetES()
        {
            return $"#{data}";
        }
    }
}
