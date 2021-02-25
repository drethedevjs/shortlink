using Microsoft.EntityFrameworkCore;
using ShortLink.Data;

namespace ShortLinkAPI.Tests
{
    public class InMemoryDbTests
    {
        protected DbContextOptions<ApiContext> options { get; }
        public InMemoryDbTests(DbContextOptions<ApiContext> options)
        {
            this.options = options;
        }
    }
}