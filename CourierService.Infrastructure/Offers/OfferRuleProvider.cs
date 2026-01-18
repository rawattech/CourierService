using Microsoft.Extensions.Configuration;
using CourierService.Application.Interfaces;

namespace CourierService.Infrastructure.Offers
{
    public class OfferRuleProvider
    {
        public static IEnumerable<IOfferStrategy> Load(IConfiguration configuration)
        {
            if (configuration == null || !configuration.GetChildren().Any())
                throw new ArgumentException("Config missing");

            var rules = configuration
                .GetSection("Offers")
                .Get<List<OfferRule>>() ?? new();

            return rules.Select(r => new ConfigurableOfferStrategy(r));
        }
    }
}
