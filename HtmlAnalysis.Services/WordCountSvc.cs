using HtmlAgilityPack;
using HtmlAnalysis.Data;
using HtmlAnalysis.Services.Help;
using HtmlAnalysis.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HtmlAnalysis.Services
{
    public class WordCountSvc : IWordCountSvc
    {
        private readonly IWordCountRepository _wordCountRepository;

        public WordCountSvc(IWordCountRepository wordCountRepository)
        {
            _wordCountRepository = wordCountRepository;
        }

        public IList<DtoWordCount> GetAllWords(int pageIndex, int next)
        {
            //Get words from the database based on index of the page and number of rows to display on the page
            var wordList = _wordCountRepository.GetAllWords(pageIndex, next);
            return wordList.Select(x => new DtoWordCount()
            {
                Word = x.Word,
                Count = x.Count
            }).ToList();

        }

        public IDictionary<string, int> ReadTopHundredWords(string url)
        {
            if (!url.StartsWith("http"))
                url = $"http://{url}";

            //HtmlAgilityPack to extracts words from the DOM
            Uri Uri = new Uri(url);
            Dictionary<string, int> RepeatedWordCount = new Dictionary<string, int>();

            var doc = new HtmlWeb().Load(Uri);

            //Get all nodes in the body
            var nodes = doc.DocumentNode.SelectNodes("//text()");

            //Extract all words and split if there are any spaces
            //Add/Update dictionary with word and count
            var count = 0;
            foreach (var node in nodes)
            {
                var innerText = node.InnerText.Trim().Split(' ');

                if (innerText.Length == 1)
                {
                    //Filter only letters
                    var text = new string(innerText[0].Where(c => Char.IsLetter(c)).ToArray());

                    if (node.NodeType == HtmlNodeType.Text && node.ParentNode.Name != "script" && text.Length > 1)
                    {
                        if (RepeatedWordCount.ContainsKey(text))
                        {
                            int value = RepeatedWordCount[text];
                            RepeatedWordCount[text] = value + 1;
                        }
                        else
                        {
                            RepeatedWordCount.Add(text, 1);
                        }
                        count++;
                    }
                }
                else
                {
                    foreach (var word in innerText)
                    {
                        //Filter only letters
                        var text = new string(word.Where(c => Char.IsLetter(c)).ToArray());

                        if (node.NodeType == HtmlNodeType.Text && node.ParentNode.Name != "script" && text.Length > 1)
                        {
                            if (RepeatedWordCount.ContainsKey(text))
                            {
                                int value = RepeatedWordCount[text];
                                RepeatedWordCount[text] = value + 1;
                            }
                            else
                            {
                                RepeatedWordCount.Add(text, 1);
                            }
                            count++;
                        }
                    }
                }
            }

            //Take top 100 from the dictionary
            var orderedWordDict = RepeatedWordCount.OrderByDescending(x => x.Value).Take(100).ToDictionary(pair => pair.Key, pair => pair.Value); ;

            //Add or Update database with top 100 words
            foreach (KeyValuePair<string, int> wordCount in orderedWordDict)
            {
                var id = PrimaryKeyProcessor.GenerateHash(wordCount.Key);
                _wordCountRepository.AddOrUpdate(new Data.DbModels.WordCount() { Id = id, Word = wordCount.Key, Count = wordCount.Value });
            }
            return orderedWordDict;
        }
    }
}


