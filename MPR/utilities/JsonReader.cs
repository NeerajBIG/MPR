using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPR.utilities
{
    public class JsonReader
    {
        public JsonReader() 
        {
        }

        public string extractData(String tokenName)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

            String myJsonString = File.ReadAllText(projectDirectory+@"\utilities\testData.json"); 
            var jsonObject = JToken.Parse(myJsonString);
            return jsonObject.SelectToken(tokenName).Value<string>();
        }

        public string[] extractDataArray(String tokenName)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

            String myJsonString = File.ReadAllText(projectDirectory + @"\utilities\testData.json");
            var jsonObject = JToken.Parse(myJsonString);
            List<String> dataList = jsonObject.SelectTokens(tokenName).Values<string>().ToList();
            return dataList.ToArray();
        }
    }
}
