using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShortLink.Models;
using stake_code_challenge_3_bnpdup.Models;

namespace stake_code_challenge_3_bnpdup.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        List<ShortLinkPair> pairs = new List<ShortLinkPair>();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Shorten(string longLink)
        {
            var pair = new ShortLinkPair()
            {
                LongLink = longLink,
                ShortenedLink = "http://short.est/GeAi9K"
            };
            Console.WriteLine("Hit!");
            Console.WriteLine(pair.LongLink);
            return RedirectToAction("Index");
        }

        public IActionResult ConvertedLink()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
