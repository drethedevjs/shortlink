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
            return Pairs.ToList<ShortLinkPair>();  
        } 

        public void AddPair(ShortLinkPair pair)
        {
            Pairs.Add(pair);
        } 

        public ShortLinkPair GetPairByShortenedLink(string shortLink)
        {
            return Pairs.Single(pair => pair.ShortenedLink == shortLink);
        }
    }  
}  