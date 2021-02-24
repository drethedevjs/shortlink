using System;

namespace ShortLink.Models
{
    public class ShortLinkPair
    {
        public int Id { get; set; }
        public string LongLink { get; set; }
        public string ShortenedLink { get; set; }
    }
}
