using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShortLink.Data;
using ShortLink.Models;

namespace shortLink.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShortLinkController : ControllerBase
    {
        private readonly ILogger<ShortLinkController> logger;
        private readonly ApiContext apiContext;

        public ShortLinkController(ILogger<ShortLinkController> logger, ApiContext apiContext)
        {
            this.logger = logger;
            this.apiContext = apiContext;
        }

        [HttpPost, Route("encode")]
        public ShortLinkPair Encode(string longLink)
        {
            var pair = new ShortLinkPair()
            {
                LongLink = longLink,
                ShortenedLink = $"http://short.est/{RandomString(6)}"
            };
            this.apiContext.AddPair(pair);
            this.apiContext.SaveChanges();
            
            var myList = this.apiContext.GetPairs();
            foreach(var x in myList)
            {
                Console.WriteLine($"{x.Id} {x.LongLink} | {x.ShortenedLink}");
            }
            return pair;
        }

        [HttpPost, Route("decode")]
        public IActionResult Decode(string shortenedLink)
        {
            Console.WriteLine("Hit!");
            var pair = this.apiContext.GetPairByShortenedLink(shortenedLink);
            Console.WriteLine($"Retrieved {pair.LongLink} using {shortenedLink}");
            return RedirectToAction("Index", pair);
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
