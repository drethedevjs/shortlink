using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShortLink.Data;
using ShortLink.Models;
using stake_code_challenge_3_bnpdup.Models;

namespace stake_code_challenge_3_bnpdup.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        List<ShortLinkPair> pairs = new List<ShortLinkPair>();
        private readonly ApiContext apiContext;
        public HomeController(ILogger<HomeController> logger, ApiContext apiContext)
        {
            this.logger = logger;
            this.apiContext = apiContext;
        }

        public IActionResult Index(ShortLinkPair pair)
        {
            return View(pair);
        }

        [HttpPost]
        public IActionResult Shorten(string longLink)
        {
            var pair = new ShortLinkPair()
            {
                LongLink = longLink,
                ShortenedLink = $"http://short.est/{RandomString(6)}"
            };
            // this.apiContext.
            pairs.Add(pair);
            foreach(var x in pairs)
            {
                Console.WriteLine(pair.LongLink);
            }
            Console.WriteLine("Hit!");
            return RedirectToAction("Index", pair);
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
