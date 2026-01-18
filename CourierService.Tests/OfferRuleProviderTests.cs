using CourierService.Infrastructure.Offers;
using Microsoft.Extensions.Configuration;
using Xunit;
using System.Collections.Generic;

namespace CourierService.Tests
{
    public class OfferRuleProviderTests
    {
        [Fact]
        public void Load_ValidConfig_ReturnsOffers()
        {
            // Arrange
            var configData = new Dictionary<string, string>
            {
                ["Offers:0:Code"] = "OFR001",
                ["Offers:0:DiscountPercentage"] = "0.1",
                ["Offers:0:MinWeight"] = "0",
                ["Offers:0:MaxWeight"] = "200",
                ["Offers:0:MinDistance"] = "0",
                ["Offers:0:MaxDistance"] = "200"
            };

            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(configData)
                .Build();

            // Act
            var offers = OfferRuleProvider.Load(config);

            // Assert
            Assert.Single(offers);
        }

        [Fact]
        public void Load_MissingOffers_ThrowsException()
        {
            // Arrange
            var config = new ConfigurationBuilder().Build();

            // Act Assert
            Assert.Throws<ArgumentException>(() =>
                        OfferRuleProvider.Load(config));            
        }
    }
}
