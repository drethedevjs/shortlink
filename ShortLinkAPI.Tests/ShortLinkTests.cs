using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShortLink.Controllers;
using ShortLink.Data;
using ShortLink.Models;
using Xunit;

namespace ShortLinkAPI.Tests
{
    public class ShortLinkTests : InMemoryDbTests
    {
        ApiContext apiContext;

        public ShortLinkTests() : base(new DbContextOptionsBuilder<ApiContext>().UseInMemoryDatabase("ApiContext").Options)
        {
        }

        [Theory]
        [InlineData("http://example.com/", "someString")]
        public void TestEncode(string validLink, string invalidLink)
        {
            using(var context = new ApiContext(options))
            {
                // Arrange
                var shortLinkController = new ShortLinkController(context);

                // Act
                var validShortLinkPair = shortLinkController.Encode(validLink);
                var invalidShortLinkPair = shortLinkController.Encode(invalidLink);

                // Assert
                Assert.IsType<ShortLinkPair>(validShortLinkPair.Value);
                Assert.Equal("http://example.com/",validShortLinkPair.Value.LongLink);
                Assert.Null(invalidShortLinkPair.Value);
            }
        }

        [Theory]
        [InlineData("http://example.com/")]
        public void TestDecode(string link)
        {
            using(var context = new ApiContext(options))
            {
                // Arrange
                var shortLinkController = new ShortLinkController(context);

                // Act
                var shortLinkPair = shortLinkController.Encode(link);
                var decodedShortLinkPair = shortLinkController.Decode(shortLinkPair.Value.ShortenedLink);

                // Assert
                Assert.IsType<ShortLinkPair>(decodedShortLinkPair.Value);
                Assert.Equal("http://example.com/", decodedShortLinkPair.Value.LongLink);
            }
        }

        [Theory]
        [InlineData("http://example.com/")]
        public void TestShortLinkValidityError(string link)
        {
            using(var context = new ApiContext(options))
            {
                // Arrange
                var shortLinkController = new ShortLinkController(context);

                // Act 
                var decodedShortLinkPair = shortLinkController.Decode(link);
                var badResult = decodedShortLinkPair.Result as BadRequestObjectResult;

                // Assert
                Assert.Equal("This isn't a short link. Please check your spelling and try again.", badResult.Value);
            }

        }
    }
}
