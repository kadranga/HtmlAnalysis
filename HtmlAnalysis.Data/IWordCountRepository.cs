using HtmlAnalysis.Data.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace HtmlAnalysis.Data
{
    public interface IWordCountRepository
    {
        List<WordCount> GetAllWords(int pageIndex, int next);
        void AddOrUpdate(WordCount word);
        WordCount GetWordByText(string text);
    }
}
