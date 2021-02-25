using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ShortLink.Data;
using ShortLink.Models;

namespace ShortLink.Controllers
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
                // Check to see if the link to be encoded is a valid uri. If not, throw an exception.
                VerifyLinkValidity(longLink);
                var pair = GetPairByLongLink(longLink);

                if(pair != null)
                    return pair;

                // The long link isn't in the database so generate a shortened link for it to be saved to the database.
                var shortenedLink = GenerateShortLink(longLink);
                
                // Create a new pair and add it to the database.
                pair = new ShortLinkPair() { LongLink = longLink, ShortenedLink = shortenedLink };
                
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
                // Check to see if the link to be encoded is a valid uri and if the shortenedLink variable is formatted correctly. If not, throw an exception.
                VerifyLinkValidity(shortenedLink, isShortLink: true);

                // Attempts to get the link pair object that's in the database that's associated with the link that was passed in.
                var pair = this.apiContext.GetPairByShortenedLink(shortenedLink);
                return pair;
            }
            // Catches if link not found in database
            catch (InvalidOperationException)
            {
                return BadRequest("That link doesn't exist in your database. Please try again using a link you have already saved.");
            }
            // Catches if error found in VerifyLinkValidity method.
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("all")]
        public List<ShortLinkPair> GetAll()
        {
            // Gets all the link pairs that are stored in the database.
            return this.apiContext.GetPairs();
        }

        #region Helper Methods
        private ShortLinkPair GetPairByLongLink(string link)
        {
            // Checks to see if the link to be shortened is already in the database.
            return this.apiContext.GetPairByLongLink(link);
        }

        private string GenerateShortLink(string longLink)
        {
            // Generate short link and check to see if it's already in the database.
            // If so, generate another (and another etc.) until one is found that is not in the database.
            var linkCandidate = "";
            ActionResult<ShortLinkPair> result;
            do
            {
                linkCandidate = $"http://short.est/{RandomString(6)}";
                result = Decode(linkCandidate);
            }
            while(result.Value != null);

            return linkCandidate;
        }
        
        private void VerifyLinkValidity(string link, bool isShortLink = false)
        {
            Uri uri = null;
            var isValidUrl = Uri.TryCreate(link, UriKind.Absolute, out uri);
            if(!isValidUrl)
                throw new Exception("You did not enter a valid Url. Please try again.");
            
            if(isShortLink)
            {
                var hasShortLinkFormat = link.StartsWith("http://short.est/");
                if(!hasShortLinkFormat)
                    throw new Exception("This isn't a short link. Please check your spelling and try again.");
            }
        }

        private static Random random = new Random();
        private static string RandomString(int length)
        {
            // This method returns a random string for the trailing characters in the shortened links.
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
        #endregion
    }
}
