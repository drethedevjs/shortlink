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
    [Route("api/[controller]")]
    public class ShortLinkController : ControllerBase
    {
        private readonly ApiContext apiContext;

        public ShortLinkController(ApiContext apiContext)
        {
            this.apiContext = apiContext;
        }

        [HttpPost, Route("encode")]
        public ActionResult<ShortLinkPair> Encode(string longLink)
        {
            try
            {
                Uri uri = null;
                var isValidUrl = Uri.TryCreate(longLink, UriKind.Absolute, out uri);
                if(!isValidUrl)
                    throw new InvalidOperationException("You did not enter a valid Url. Please try again.");
                    
                var pair = this.apiContext.GetPairByLongLink(longLink);
                
                if(pair != null)
                    return Ok(pair);

                pair = new ShortLinkPair() { LongLink = longLink, ShortenedLink = $"http://short.est/{RandomString(6)}" };
                
                this.apiContext.AddPair(pair);
                this.apiContext.SaveChanges();
                
                return pair;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("decode")]
        public ActionResult<ShortLinkPair> Decode(string shortenedLink)
        {
            try
            {
                var pair = this.apiContext.GetPairByShortenedLink(shortenedLink);
                return pair;
            }
            catch (Exception)
            {
                return BadRequest("That url is not recognized. Please enter a Url that has already been saved.");
            }
        }

        [HttpGet, Route("all")]
        public List<ShortLinkPair> GetAll()
        {
            return this.apiContext.GetPairs();
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
