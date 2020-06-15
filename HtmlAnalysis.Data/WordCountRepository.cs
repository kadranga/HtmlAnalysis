using Dapper;
using HtmlAnalysis.Data.DbModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using HtmlAnalysis.Data.Queries;
using System.Linq;

namespace HtmlAnalysis.Data
{
    public class WordCountRepository : IWordCountRepository
    {
        private readonly ICommandText _commandText;
        private readonly string _connStr;

        public WordCountRepository(IConfiguration configuration, ICommandText commandText)
        {
            _commandText = commandText;
            _connStr = configuration.GetConnectionString("WordCountDbContext");
        }
        public void AddOrUpdate(WordCount word)
        {
            //Check if text exists
            var text = GetWordByText(word.Word);
            if(text != null)
            {
                //Add to existing count and update row
                var newCount = text.Count + word.Count;
                ExecuteCommand(_connStr, conn =>
                {
                    var query = conn.Query<WordCount>(_commandText.UpdateWord,
                        new { Count = newCount , Id = word.Id });
                });

            }
            else
            {
                //Insert new row
                ExecuteCommand(_connStr, conn => {
                    var query = conn.Query<WordCount>(_commandText.AddWord,
                        new { Id = word.Id, Word = word.Word, Count = word.Count });
                });

            }
        }

        public List<WordCount> GetAllWords(int pageIndex, int next)
        {
            //Get offset based on page index and number of rows on the page
            var offSet = (pageIndex - 1) * next;
            var query = ExecuteCommand(_connStr,
                conn => conn.Query<WordCount>(_commandText.GetWords, new { Offset = offSet, Next = next}));
            return query.ToList();
        }

        public WordCount GetWordByText(string text)
        {
            //Get word by text
            var query = ExecuteCommand<WordCount>(_connStr, conn =>
                conn.Query<WordCount>(_commandText.GetWordByText, new { @Word = text }).SingleOrDefault());
            return query;
        }

        #region Helpers

        private void ExecuteCommand(string connStr, Action<SqlConnection> task)
        {
            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();

                task(conn);
            }
        }
        private T ExecuteCommand<T>(string connStr, Func<SqlConnection, T> task)
        {
            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();

                return task(conn);
            }
        }
        #endregion
    }
}
