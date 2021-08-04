using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OPAnalyzer.Analyzer
{
    public class DataSourceService
    {
        public DataSource GetDataSourceInstance(string DataSourceName)
        {
            if (DataSourceName.ToLower() == "stackoverflow")
            {
                return new Stackoverflow();
            }
            else
            {
                return new Github();
            }
        }

        public string FetchData(string APIurl)
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


            return html;
        }
    }
}
