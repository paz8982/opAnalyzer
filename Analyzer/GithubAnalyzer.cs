using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPAnalyzer.Analyzer
{
    public class GithubAnalyzer : IAnalyzable
    {
        public string[] Analyze(string Data)
        {
            JArray json = JArray.Parse(Data);
            List<string> Messages = new List<string>();
            foreach (Object messageObject in json)
            {
                var commitObj = JObject.Parse(messageObject.ToString()).GetValue("commit");
                Messages.Add(JObject.Parse(commitObj.ToString()).GetValue("message").ToString());
            }

            return Messages.ToArray();
        }
    }
}
