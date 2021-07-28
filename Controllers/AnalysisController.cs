using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OPAnalyzer.Analyzer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OPAnalyzer.Controllers
{
    class DataSourceObj
    {
        public string DataSourceName;
    }

    [Route("api/[controller]")]
    [ApiController]
    public class AnalysisController : ControllerBase
    {
       /* [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello");
        }*/

        [HttpGet]
        public IActionResult Get(string DataSource)
        {
            string Data;
            string[] AnalyzedData;
            if(DataSource.ToLower() == "stackoverflow")
            {
                Data = GetDataFromAPI("https://api.stackexchange.com/2.2/tags/highcharts/faq?site=stackoverflow");
                StackoverflowAnalyzer Analyzer = new StackoverflowAnalyzer();
                AnalyzedData = Analyzer.Analyze(Data);
            }
            else
            {
                Data = GetDataFromAPI("https://api.github.com/repos/highcharts/highcharts/commits");
                GithubAnalyzer Analyzer = new GithubAnalyzer();
                AnalyzedData = Analyzer.Analyze(Data);
            }
            //check which dataSource we got
            //if datasource is stackoverflow 
            //get from stackoverflow API the data
            //analize this data and return

            //if datasource is github 
            //get from github API the data
            //analize this data and return
            return Ok(AnalyzedData);
        }

        private string GetDataFromAPI(string APIurl) //maybe extract this to abstract class that the analyzers will inherit
        {
            string html = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(APIurl);
            request.UserAgent = "Paz";
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            Console.WriteLine(html);
            return html;
        }

        /*private string[] AnalyzeStackoverflow(string Data)
        {
            JObject json = JObject.Parse(Data);
            var Items = json.GetValue("items");
            List<string> Titles = new List<string>();
            foreach(Object item in Items)
            {
                JObject jsonItem = JObject.Parse(item.ToString());
                Titles.Add(jsonItem.GetValue("title").ToString());
            }
            return Titles.ToArray();
        }

        private string[] AnalyzeGithub(string Data)
        {
            JArray json = JArray.Parse(Data);
            List<string> Messages = new List<string>();
            foreach (Object messageObject in json)
            {
                var commitObj = JObject.Parse(messageObject.ToString()).GetValue("commit");
                Messages.Add(JObject.Parse(commitObj.ToString()).GetValue("message").ToString());
            }
           
            return Messages.ToArray();
        }*/
    }
}
