using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPAnalyzer.Analyzer
{
    public class Github : DataSource, IExtractable, IAnalyzable
    {
        public override string APIurl { get; } = "https://api.github.com/repos/highcharts/highcharts/commits";

        public override string[] Analyze(string[] data, long analysisFlowId)
        {
            AnalysisService AnalysisService = new AnalysisService();
            return AnalysisService.AnalizeData(data, analysisFlowId);
        }

        public override string[] Extract(string Data)
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
