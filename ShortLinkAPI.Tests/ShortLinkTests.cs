using System;
using shortLink.Controllers;
using ShortLink.Models;
using Xunit;

namespace ShortLinkAPI.Tests
{
    public class ShortLinkTests
    {
        private readonly ShortLinkController shortLinkController;
        public ShortLinkTests(ShortLinkController shortLinkController)
        {
            this.shortLinkController = shortLinkController;
        }

        [Theory]
        [InlineData("http://example.com/", "someString")]
        public void TestEncode(string validLink, string invalidLink)
        {
            // Act
            var validShortLinkPair = this.shortLinkController.Encode(validLink);
            var invalidShortLinkPair = this.shortLinkController.Encode(invalidLink);

            // Assert
            Assert.IsType<ShortLinkPair>(validShortLinkPair);
            // Assert.Is
        }

        [Fact]
        public void TestDecode()
        {
            // Arrange
            var longLink = "http://example.com/";

            // Act
            var shortLinkPair = this.shortLinkController.Encode(longLink);
            var result = this.shortLinkController.Decode(shortLinkPair.ShortenedLink);

            // Assert

        }
    }
}
