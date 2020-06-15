using System;
using System.Collections.Generic;
using System.Text;

namespace HtmlAnalysis.Data.Queries
{
        public interface ICommandText
        {
            string GetWords { get; }
            string AddWord { get; }
            string UpdateWord { get; }
            string GetWordByText { get; }
        }

        public class CommandText : ICommandText
        {
            //public string GetWords => "Select * From WordCount";
            public string GetWords => (@" SELECT * FROM [WordCount] ORDER BY [Count] Desc OFFSET @Offset ROWS FETCH NEXT @Next ROWS ONLY");
            public string GetWordByText => "Select * From WordCount Where Word= @Word";    
            public string AddWord => "Insert Into  WordCount (Id, Word, Count) Values (@Id, @Word, @Count)";
            public string UpdateWord => "Update WordCount set Count = @Count Where Id =@Id";

        }
  
}
