using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtgOffline.EnderScript
{
    public class ESBuffer
    {
        public bool suppressLastLineEnding = true;
        public List<ESObject> esValues = new List<ESObject>();

        public ESBuffer(){}
        public ESBuffer(ESBuilder builder)
        {
            esValues = builder.esValues;
        }

        public void Add(string parameterName, string data, string lineEnding = "\n")
        {
            ESObject esObject = new ESObject(parameterName, "\"" + data + "\"", lineEnding);
            Add(esObject);
        }
        public void Add(string parameterName, int data, string lineEnding = "\n")
        {
            ESObject esObject = new ESObject(parameterName, data.ToString(), lineEnding);
            Add(esObject);
        }
        public void Add(string parameterName, float data, string lineEnding = "\n")
        {
            ESObject esObject = new ESObject(parameterName, data.ToString(), lineEnding);
            Add(esObject);
        }
        public void Add(string parameterName, bool data, string lineEnding = "\n")
        {
            ESObject esObject = new ESObject(parameterName, data.ToString(), lineEnding);
            Add(esObject);
        }
        public void AddComment(string comment)
        {
            ESComment esObject = new ESComment(comment);
            Add(esObject);
        }

        private void Add(ESObject obj)
        {
            ESObject old = esValues.Find(x => x.parameterName == obj.parameterName);
            if (old != null)
                esValues[esValues.IndexOf(old)] = obj;
            else
                esValues.Add(obj);
        }

        public string GetES()
        {
            string es = "";
            for (int i = 0; i < esValues.Count; i++) {
                bool flag = i + 1 != esValues.Count;
                bool comment = esValues[i] is ESComment;

                es += esValues[i].GetES() + (!comment&&flag? "," : "") + (!flag&&suppressLastLineEnding?"":esValues[i].lineEnding);
            }
            return es;
        }

        public void WriteToPath(string path)
        {
            ESUtils.WriteToPath(path, GetES());
        }
    }

    public class ESParseException : Exception
    {
        public ESParseException()
        {

        }
        public ESParseException(string message) : base(message)
        {

        }
    }
}
