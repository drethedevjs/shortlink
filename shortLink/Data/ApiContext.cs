using Microsoft.EntityFrameworkCore;
using ShortLink.Models;
using System.Collections.Generic;  
using System.Linq;  
  
namespace ShortLink.Data  
{  
    public class ApiContext : DbContext  
    {  
        public DbSet<ShortLinkPair> Pairs { get; set; }  
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)  
        {  
        }  
  
        public List<ShortLinkPair> GetPairs()  
        {  
            // Gets the list of all the pairs that the user has added to the database.
            return Pairs.ToList<ShortLinkPair>();  
        } 

        public void AddPair(ShortLinkPair pair)
        {
            // Adds a pair to the database.
            Pairs.Add(pair);
        } 

        public ShortLinkPair GetPairByShortenedLink(string shortLink)
        {
            // Takes in a shortened link and returns the pair associated with it.
            return Pairs.Single(pair => pair.ShortenedLink == shortLink);
        }

        public ShortLinkPair GetPairByLongLink(string longLink)
        {
            // Takes in a regular link and returns the pair associated with it.
            return Pairs.FirstOrDefault(pair => pair.LongLink == longLink);
        }
    }  
}  