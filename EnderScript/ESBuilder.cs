using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline.EnderScript
{
    public class ESBuilder
    { 
        public string es;
        public List<ESObject> esValues;

        public ESBuilder(string es)
        {
            this.es = es;
            esValues = ParseES(es);
        }

        public string Get(string paramName, string defaultValue)
        {
            ESObject obj = esValues.Find(x => x.parameterName == paramName);
            return (obj == null)?defaultValue:obj.GetString();
        }
        public int Get(string paramName, int defaultValue)
        {
            ESObject obj = esValues.Find(x => x.parameterName == paramName);
            return (obj == null) ? defaultValue : obj.GetInt();
        }
        public float Get(string paramName, float defaultValue)
        {
            ESObject obj = esValues.Find(x => x.parameterName == paramName);
            return (obj == null) ? defaultValue : obj.GetFloat();
        }
        public bool Get(string paramName, bool defaultValue)
        {
            ESObject obj = esValues.Find(x => x.parameterName == paramName);
            return (obj == null) ? defaultValue : obj.GetBool();
        }

        public object[] GetArray(string paramName, object[] defaultValue)
        {
            ESObject obj = esValues.Find(x => x.parameterName == paramName);
            return (obj == null) ? defaultValue : obj.GetArray();
        }
        public T[] GetArray<T>(string paramName, T[] defaultValue)
        {
            ESObject obj = esValues.Find(x => x.parameterName == paramName);
            return (obj == null) ? defaultValue : obj.GetArray<T>();
        }

        private List<ESObject> ParseES(string es)
        {
            List<ESObject> esV = new List<ESObject>();
            
            List<string> lines = SmartSplit(es);
            
            //char[] c = es.ToCharArray();

            //List<string> lines = new List<string>();
            //string line = "";

            //for (int i = 0; i < c.Length; i++)
            //{
            //    if (c[i] == '#')
            //    {
            //        i = SkipUntilCommentEnd(es, i);
            //    }
            //    else if (c[i] == '"')
            //    {
            //        i = AppendUntilStringEnd(es, i, line, out line);
            //    }
            //    else if (c[i] == ',')
            //    {
            //        lines.Add(line);
            //        line = "";
            //    }
            //    else if (c[i] == '[')
            //        i = AppendUntilArrayEnd(es, i, line, out line);
            //    else
            //        line += c[i];
            //}

            //lines.Add(line);

            foreach (string l in lines)
            {
                string[] split = SplitBySeparator(l);
                ESObject esO = new ESObject(Trim(split[0]), Trim(split[1]));
                esV.Add(esO);
            }

            return esV;
        }

        public static List<string> SmartSplit(string s)
        {
            string line = "";
            List<string> lines = new List<string>();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '#')
                {
                    i = SkipUntilCommentEnd(s, i);
                }
                else if (s[i] == '"')
                {
                    i = AppendUntilStringEnd(s, i, line, out line);
                }
                else if (s[i] == ',')
                {
                    lines.Add(line);
                    line = "";
                }
                else if (s[i] == '[')
                    i = AppendUntilArrayEnd(s, i, line, out line);
                else
                    line += s[i];
            }
            lines.Add(line);

            return lines;
        }

        private static int SkipUntilCommentEnd(string s, int startInd)
        {
            for (int i = startInd; i < s.Length; i++)
            {
                if (s[i] == '\n')
                    return i;
            }
            return s.Length;
        }

        private static int AppendUntilStringEnd(string s,int startInd, string lineIn, out string lineOut, bool includeEscapeChar = false)
        {
            startInd++;
            lineIn += '"';
            lineOut = lineIn;
            for (int i = startInd; i < s.Length; i++)
            {
                if (s[i] == '\\')
                {
                    if(includeEscapeChar)
                        lineOut += s[i];
                    lineOut += s[i + 1];
                    i++;
                }
                else if (s[i] == '"')
                {
                    lineOut += s[i];
                    return i;
                }
                else
                    lineOut += s[i];
            }
            return s.Length;
        }

        private static int AppendUntilArrayEnd(string s, int startInd, string lineIn, out string lineOut, bool includeEscapeChar = false)
        {
            startInd++;
            lineIn += '[';
            lineOut = lineIn;
            int mod = 0;
            for(int i = startInd; i< s.Length; i++)
            {
                if (s[i] == '\\')
                {
                    if (includeEscapeChar)
                        lineOut += s[i];
                    lineOut += s[i + 1];
                    i++;
                }
                else if (s[i] == '"')
                {
                    i = AppendUntilStringEnd(s, i, lineOut, out lineOut);
                }
                else if (s[i] == '#')
                    i = SkipUntilCommentEnd(s, i);
                else if (s[i] == '[')
                {
                    mod++;
                    lineOut += s[i];
                }
                else if (s[i] == ']')
                {
                    lineOut += s[i];
                    if (mod == 0)
                        return i;
                    else
                        mod--;
                }
                else
                    lineOut += s[i];
            }
            return s.Length;
        }

        private string Trim(string s)
        {
            return s.Trim(new char[] { ' ', '\n','\r' });
        }

        private string[] SplitBySeparator(string es)
        {
            string[] result = new string[2];
            char[] c = es.ToCharArray();
            bool hasBeenSplit = false;
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == '|' && !hasBeenSplit)
                {
                    hasBeenSplit = true;
                    continue;
                }
                else
                {
                    if (hasBeenSplit) result[1] += c[i];
                    else result[0] += c[i];
                }
            }
            result[0] = Trim(result[0]);
            return result;
        }

    }
}
