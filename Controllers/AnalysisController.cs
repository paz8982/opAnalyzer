using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OPAnalyzer.Analyzer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public IActionResult Get(string DataSource, long analysisFlowId)
        {
            string Data;
            string[] AnalyzedData;
            DataSourceService fetchDataService = new DataSourceService();
            DataSource dataSource = fetchDataService.GetDataSourceInstance(DataSource);

            string data = fetchDataService.FetchData(dataSource.APIurl);
            string[] extractedData = dataSource.Extract(data);
            //return Ok(extractedData);

            string[] dataAfterAnalysis = dataSource.Analyze(extractedData, analysisFlowId);
            //string[] dataAfterAnalysis = analysisService.AnalizeData(extractedData, analysisFlowId);
            return Ok(dataAfterAnalysis);

        }

       //ozi recommended changing DataSource abtract class to interface - thing about it
    }
}
