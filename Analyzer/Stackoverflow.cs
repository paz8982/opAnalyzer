using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPAnalyzer.Analyzer
{
    public class Stackoverflow : DataSource, IExtractable, IAnalyzable
    {
        public override string APIurl { get; } = "https://api.stackexchange.com/2.2/tags/highcharts/faq?site=stackoverflow";

        public override string[] Analyze(string[] data, long analysisFlowId)
        {
            AnalysisService AnalysisService = new AnalysisService();
            return AnalysisService.AnalizeData(data, analysisFlowId);
        }

        public override string[] Extract(string Data)
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
