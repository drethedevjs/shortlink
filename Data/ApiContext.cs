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
            // LoadPairs();  
        }  
  
        public void LoadPairs()  
        {  
            Pairs.Add(new ShortLinkPair() { LongLink = "http://thisisalonglink.com", ShortenedLink = "http://shorterLink.com/123ABC" });  
            Pairs.Add(new ShortLinkPair() { LongLink = "http://thisisalonglink.com/2", ShortenedLink = "http://shorterLink.com/ABC123" });  
        }  
  
        public List<ShortLinkPair> GetPairs()  
        {  
            return Pairs.ToList<ShortLinkPair>();  
        } 

        public void AddPair(ShortLinkPair pair)
        {
            Pairs.Add(pair);
        } 

        public ShortLinkPair GetPair(string shortLink)
        {
            return Pairs.Single(pair => pair.ShortenedLink == shortLink);
        }
    }  
}  