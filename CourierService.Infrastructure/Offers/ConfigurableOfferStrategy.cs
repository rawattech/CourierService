using CourierService.Application.Interfaces;
using CourierService.Domain;

namespace CourierService.Infrastructure.Offers
{
    public class ConfigurableOfferStrategy : IOfferStrategy
    {
        private readonly OfferRule _rule;
        public ConfigurableOfferStrategy(OfferRule rule) 
        {
            _rule = rule;
        }

        public string Code => _rule.Code;

        public bool IsApplicable(Package package) =>
            package.Distance >= _rule.MinDistance && 
            package.Distance <= _rule.MaxDistance &&
            package.Weight >= _rule.MinWeight &&
            package.Weight <= _rule.MaxWeight;

        public decimal CalculateDiscount(decimal deliveryCost) => 
            deliveryCost * _rule.DiscountPercentage;
    }
}
