using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HtmlAnalysis.Web.Models;
using System.Net;
using HtmlAgilityPack;

namespace HtmlAnalysis.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Admin()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult GetTopWords(string inputUrl)
        {

            Uri Uri = new Uri("http://www.stuff.co.nz");

            //HtmlWeb web = new HtmlWeb();
            //HtmlDocument doc1 = web.Load("http://www.stuff.co.nz");

            //var doc = new HtmlWeb().Load(Uri);
            //var nodes = doc.DocumentNode.SelectSingleNode("//body").DescendantsAndSelf();
            //Dictionary<string, int> RepeatedWordCount = new Dictionary<string, int>();
          
            //var count = 0;
            //foreach (var node in nodes)
            //{
            //    var text = node.ParentNode.Name.Trim();
            //    if (node.NodeType == HtmlNodeType.Text && text != "script")
            //    {
            //        if (RepeatedWordCount.ContainsKey(text)) {
            //            int value = RepeatedWordCount[text];
            //            RepeatedWordCount[text] = value + 1;
            //        }
            //        else
            //        {
            //            RepeatedWordCount.Add(text, 1);
            //        }
            //        count++;
            //    }                
            //}

           

            return View();
        }
    }
}
