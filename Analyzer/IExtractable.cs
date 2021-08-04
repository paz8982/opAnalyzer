using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPAnalyzer.Analyzer
{
    interface IExtractable
    {
        string APIurl { get; }
        string[] Extract(string Data);
    }
}
