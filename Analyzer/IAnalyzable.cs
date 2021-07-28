using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPAnalyzer.Analyzer
{
    public interface IAnalyzable
    {
        string[] Analyze(string Data);
    }
}
