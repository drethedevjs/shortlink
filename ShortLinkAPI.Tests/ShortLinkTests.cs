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
        public void TestEncode()
        {
            // Arrange
            var longLink = "http://example.com/";

            // Act
            var shortLinkPair = this.shortLinkController.Encode(longLink);

            // Assert
            Assert.IsType<ShortLinkPair>(shortLinkPair);
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
