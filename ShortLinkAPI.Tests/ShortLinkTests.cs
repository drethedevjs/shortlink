using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using shortLink.Controllers;
using ShortLink.Data;
using ShortLink.Models;
using Xunit;
using Xunit.Abstractions;

namespace ShortLinkAPI.Tests
{
    public class ShortLinkTests : InMemoryDbTests
    {
        private readonly ITestOutputHelper outputHelper;
        ApiContext apiContext;
        // ShortLinkController shortLinkController;

        public ShortLinkTests(ITestOutputHelper outputHelper) : base(new DbContextOptionsBuilder<ApiContext>().UseInMemoryDatabase("ApiContext").Options)
        {
            this.outputHelper = outputHelper;

            // this.options = options;
            // this.apiContext = new ApiContext(this.options);
            // this.shortLinkController = new ShortLinkController(this.apiContext);
        }

        [Theory]
        [InlineData("http://example.com/", "someString")]
        public void TestEncode(string validLink, string invalidLink)
        {
            using(var context = new ApiContext(options))
            {
                var shortLinkController = new ShortLinkController(context);
                // Act
                var validShortLinkPair = shortLinkController.Encode(validLink);
                var invalidShortLinkPair = shortLinkController.Encode(invalidLink);

                this.outputHelper.WriteLine($"This is something: {invalidShortLinkPair}");

                // Assert
                Assert.IsType<ShortLinkPair>(validShortLinkPair);
                Assert.IsType<ContentResult>(invalidShortLinkPair);
            }
        }

        [Fact]
        public void TestDecode()
        {
            // Arrange
            var longLink = "http://example.com/";

            // Act
            // var shortLinkPair = this.shortLinkController.Encode(longLink);
            // var result = this.shortLinkController.Decode(shortLinkPair.);

            // Assert

        }
    }
}
