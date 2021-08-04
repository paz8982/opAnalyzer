using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OPAnalyzer.Analyzer
{
    public abstract class DataSource: IExtractable, IAnalyzable
    {
        public abstract string APIurl { get; }
        
        public abstract string[] Extract(string Data);
        public abstract string[] Analyze(string[] data, long analysisFlowId);
    }
}
