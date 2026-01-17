using Microsoft.Extensions.Configuration;
using CourierService.Application.Interfaces;

namespace CourierService.Infrastructure.Offers
{
    public class OfferRuleProvider
    {
        public static IEnumerable<IOfferStrategy> Load(IConfiguration configuration)
        {
            var rules = configuration
                .GetSection("Offers")
                .Get<List<OfferRule>>() ?? new();

            return rules.Select(r => new ConfigurableOfferStrategy(r));
        }
    }
}
