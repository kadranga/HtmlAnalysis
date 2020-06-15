using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAnalysis.Services;
using HtmlAnalysis.Services.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace HtmlAnalysis.Api.Controllers
{
    /// <summary>
    /// Html Analysis end points
    /// </summary>
    [ApiController]
    [Route("api/words")]
    public class WordsController : ControllerBase
    {
        private readonly ILogger<WordsController> _logger;
        private readonly IWordCountSvc _wordCountSvcy;

        /// <summary>
        /// WordsController
        /// </summary>
        public WordsController(ILogger<WordsController> logger, IWordCountSvc wordCountRepository)
        {
            _wordCountSvcy = wordCountRepository;
        }

        /// <summary>
        /// ProcessWords | Save and return top 100 words
        /// </summary>
        /// <param name="req"></param>        
        [HttpPost("")]
        public IEnumerable<DtoWordCount> ProcessWords([System.Web.Http.FromBody]WordProcessRequest req)
        {
            return _wordCountSvcy.ReadTopHundredWords(req.Url).Select(i=> new DtoWordCount() { Word = i.Key, Count = i.Value });
            //return new List<DtoWordCount>() { new DtoWordCount() { Word = "a", Count = 2, },
            //new DtoWordCount() { Word = "b", Count = 3, }};
        }

        /// <summary>
        /// GetWords | Get list of words and counts by Desc
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="next"></param>
        [HttpGet("")]
        public IEnumerable<DtoWordCount> GetWords([System.Web.Http.FromUri]int pageIndex=1, [System.Web.Http.FromBody] int next = 5)
        {
            return _wordCountSvcy.GetAllWords(pageIndex, next);
        }
    }
}
