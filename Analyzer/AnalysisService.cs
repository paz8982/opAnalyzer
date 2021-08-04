using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OPAnalyzer.Analyzer
{
    public class AnalysisService
    {
        public string[] AnalizeData(string[] data, long DataFlowID)
        {
            string json = string.Empty;
            using (StreamReader r = new StreamReader("./Analyzer/AnalysisFlows.json"))
            {
                json = r.ReadToEnd();
            }

            string[] Steps = ExtractStepsFromJson(json, DataFlowID);
            foreach (string step in Steps)
            {
                JArray stepsArray = JArray.Parse(step.ToString());
                foreach (Object s in stepsArray)
                {
                    var st = JObject.Parse(s.ToString()).GetValue("Name").ToString();
                    data = CallFunctionForEachStep(st, data);
                }
                
            }
            return data;
        }
        private string[] ExtractStepsFromJson(string Data, long DataFlowID)
        {
            JArray json = JArray.Parse(Data);
            List<string> Messages = new List<string>();
            foreach (Object flow in json)
            {
                var flowsObj = JObject.Parse(flow.ToString()).GetValue("Steps");
                Messages.Add(flowsObj.ToString());
            }

            return Messages.ToArray();
        }

        private string[] CallFunctionForEachStep(string StepName, string[] Data)
        {
            switch (StepName)
            {
                case "Remove short items":
                    return RemoveShortItems(Data);
                case "Remove spaces":
                    Console.WriteLine("Case 2");
                    return RemoveSpaces(Data);
                default:
                    Debug.Print("step not found");
                    return null;
            }
        }

        private string[] RemoveShortItems(string[] data)
        {
            return data.Where(row => row.Length >= 5).ToArray();
        }

        private string[] RemoveSpaces(string[] data)
        {
            return data.Select(row => row.Replace(" ", "")).ToArray();
        }
    }
}
