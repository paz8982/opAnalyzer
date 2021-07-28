using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPAnalyzer.Analyzer
{
    public class StackoverflowAnalyzer : IAnalyzable
    {
        public string[] Analyze(string Data)
        {
            JObject json = JObject.Parse(Data);
            var Items = json.GetValue("items");
            List<string> Titles = new List<string>();
            foreach (Object item in Items)
            {
                JObject jsonItem = JObject.Parse(item.ToString());
                Titles.Add(jsonItem.GetValue("title").ToString());
            }
            return Titles.ToArray();
        }
    }
}
