using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Utilities
{
    public class jsonReader
    {
        public jsonReader()
        {

        }

        public string extractData(string keyName)
        {
           var jsonData = File.ReadAllText(ConfigurationManager.AppSettings["jsonLocation"]);
           var jsonObject = JToken.Parse(jsonData);
           return jsonObject.SelectToken(keyName).Value<String>();
        }




    }
}
