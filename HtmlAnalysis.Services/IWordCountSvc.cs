using HtmlAnalysis.Services.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HtmlAnalysis.Services
{
    public interface IWordCountSvc
    {
        IDictionary<string,int> ReadTopHundredWords(string url);
        IList<DtoWordCount> GetAllWords(int pageIndex, int next);
    }
}
